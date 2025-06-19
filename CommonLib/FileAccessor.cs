using LumenWorks.Framework.IO.Csv;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using DecryptPassword;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System.Drawing;
using System.Threading;
using System.Windows.Documents;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MPPPS
{
    /// <summary>
    /// ファイル管理クラス
    /// </summary>
    public class FileAccessor
    {
        // 共通クラス
        private readonly Common cmn;

        // 接続切断する Win32 API を宣言
        [DllImport("mpr.dll", EntryPoint = "WNetCancelConnection2", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern int WNetCancelConnection2(string lpName, Int32 dwFlags, bool fForce);

        // 認証情報を使って接続する Win32 API 宣言
        [DllImport("mpr.dll", EntryPoint = "WNetAddConnection2", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern int WNetAddConnection2(ref NETRESOURCE lpNetResource, string lpPassword, string lpUsername, Int32 dwFlags);

        // WNetAddConnection2 に渡す接続の詳細情報の構造体
        [StructLayout(LayoutKind.Sequential)]

        // 接続情報
        internal struct NETRESOURCE
        {
            public int dwScope; // 列挙の範囲
            public int dwType; // リソースタイプ
            public int dwDisplayType; // 表示オブジェクト
            public int dwUsage; // リソースの使用方法
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpLocalName; // ローカルデバイス名。使わないなら NULL
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpRemoteName; // リモートネットワーク名。使わないなら NULL
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpComment; // ネットワーク内の提供者に提供された文字列
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpProvider; // リソースを所有しているプロバイダ名
        }

        // Interop.Excel 
        public Excel.Application oXls;      // Excel オブジェクト
        public Excel.Workbook oWBook;       // Workbook オブジェクト
        public Excel.Worksheet oWSheet;     // Worksheet オブジェクト
        public Excel.Range oRange;          // Range オブジェクト

        // QRCoder
        public QRCodeGenerator oQRGenerator;

        // 製造指示カード雛形オブジェクト
        private object[,] templateOrderCardObject = new object[20, 8]; // ※Excel[20行, 8列]、配列番号が 1からスタートするので注意！

        // 内示カード雛形オブジェクト
        private object[,] templatePlanCardObject = new object[10, 8]; // ※Excel[10行, 8列]、配列番号が 1からスタートするので注意！

        public FileAccessor(Common cmn)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 共通クラス
            this.cmn = cmn;
        }

        /// <summary>
        /// 切削生産計画システム雛形ファイル参照先サーバーへの接続
        /// </summary>
        public void ConnectFormServer()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 接続情報を設定
            NETRESOURCE formNetReform = new NETRESOURCE();
            formNetReform.dwScope = 0;
            formNetReform.dwType = 1;
            formNetReform.dwDisplayType = 0;
            formNetReform.dwUsage = 0;
            formNetReform.lpLocalName = ""; // ネットワーク ドライブにする場合は "z:" などドライブレター設定  
            formNetReform.lpRemoteName = @cmn.FsCd[1].ShareName;
            formNetReform.lpProvider = "";

            int ret = 0;
            try
            {
                // 既に接続している場合があるので一旦切断する
                ret = WNetCancelConnection2(@cmn.FsCd[1].ShareName, 0, true);
                // 認証情報を使って共有フォルダに接続
                ret = WNetAddConnection2(ref formNetReform, cmn.FsCd[1].EncPasswd, cmn.FsCd[1].UserId, 0);
            }
            // 例外発生時
            catch (Exception ex)
            {
                // エラー処理
                Debug.WriteLine("Exception Message = " + ex.Message);
            }
            Debug.WriteLine("ConnectformServer ret = " + ret);
        }


        /// <summary>
        /// 切削生産計画システム結果保存先サーバーへの接続
        /// </summary>
        public void ConnectSaveServer()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 接続情報を設定  
            NETRESOURCE saveNetResource = new NETRESOURCE();
            saveNetResource.dwScope = 0;
            saveNetResource.dwType = 1;
            saveNetResource.dwDisplayType = 0;
            saveNetResource.dwUsage = 0;
            saveNetResource.lpLocalName = ""; // ネットワークドライブにする場合は "z:" などドライブレター設定  
            saveNetResource.lpRemoteName = @cmn.FsCd[1].ShareName;
            saveNetResource.lpProvider = "";

            int ret = 0;
            try
            {
                // パスワード復号化
                var dpc = new DecryptPasswordClass();
                dpc.DecryptPassword(cmn.FsCd[1].EncPasswd, out string decPasswd);
                // 既に接続している場合があるので一旦切断する
                ret = WNetCancelConnection2(@cmn.FsCd[1].ShareName, 0, true);
                // 認証情報を使って共有フォルダに接続
                ret = WNetAddConnection2(ref saveNetResource, decPasswd, cmn.FsCd[1].UserId, 0);
            }
            // 例外発生時
            catch (Exception ex)
            {
                // エラー処理
                Debug.WriteLine("Exception Message = " + ex.Message);
            }
            Debug.WriteLine("ConnectSaveServer ret = " + ret);
        }

        /// <summary>
        /// ファイル情報作成
        /// </summary>
        /// <param name="cmn">共通クラス</param>
        /// <returns>上書き要否 (0: しない、1: する)</returns>
        public bool CreateFileInfo()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 現在のパスを取得
            string dirCurrentPath = Directory.GetCurrentDirectory();
            Debug.WriteLine(dirCurrentPath);

            // データベース設定ファイル存在チェック
            var iniFi = new FileInfo(@cmn.CnfFilePathDb);
            if (!iniFi.Exists)
            {
                Debug.WriteLine(Common.MSG_NO_CONFIG_FILE);
                cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_701, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error); // 設定ファイルなし
                return Common.FILE_INFO_NG;
            }

            // ファイル システム設定ファイル存在チェック
            iniFi = new FileInfo(@cmn.CnfFilePathFs);
            if (!iniFi.Exists)
            {
                Debug.WriteLine(Common.MSG_NO_CONFIG_FILE);
                cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_701, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error); // 設定ファイルなし
                return Common.FILE_INFO_NG;
            }

            //// 原価計算雛形ファイル存在チェック
            //var ptnFi = new FileInfo(@cmn.FsCd[1].FileName);
            //if (!ptnFi.Exists)
            //{
            //    Debug.WriteLine(Common.MSG_NO_PATTERN_FILE);
            //    cmn.ShowMessageBox(Common.MY_PGM_ID, Common.MSG_CD_701, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error); // 原価計算雛形ファイルなし
            //    return Common.FILE_INFO_NG;
            //}
            return Common.FILE_INFO_OK;
        }

        /// <summary>
        /// 親ディレクトリ取得
        /// </summary>
        /// <param name="path">フルパス</param>
        /// <returns>親ディレクトリへのパス</returns>
        public string GetParentDirectory(string path)
        {
            if (path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                path = path.Substring(0, path.Length - 1);
            }

            return Path.GetDirectoryName(path);
        }

        /// <summary>
        /// データベース設定ファイルの逆シリアライズ
        /// </summary>
        /// <returns>データベース設定データ配列</returns>
        public DBConfigData[] ReserializeDBConfigFile()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 設定ファイル名
            string fileName = @cmn.CnfFilePathDb;

            // XmlSerializerオブジェクトを作成
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(DBConfigData[]));

            // 読み込むファイルを開く
            System.IO.StreamReader sr = new System.IO.StreamReader(
                fileName, new System.Text.UTF8Encoding(false));

            // XMLファイルから読み込み、逆シリアル化する
            DBConfigData[] loadAry = (DBConfigData[])serializer.Deserialize(sr);

            // ファイルを閉じる
            sr.Close();

            return loadAry;
        }

        /// <summary>
        /// ファイル システム設定ファイルの逆シリアライズ
        /// </summary>
        /// <returns>ファイル システム設定データ配列</returns>
        public FSConfigData[] ReserializeFSConfigFile()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 設定ファイル名
            string fileName = @cmn.CnfFilePathFs;

            // XmlSerializerオブジェクトを作成
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(FSConfigData[]));

            // 読み込むファイルを開く
            System.IO.StreamReader sr = new System.IO.StreamReader(
                fileName, new System.Text.UTF8Encoding(false));

            // XMLファイルから読み込み、逆シリアル化する
            FSConfigData[] loadAry = (FSConfigData[])serializer.Deserialize(sr);

            // ファイルを閉じる
            sr.Close();

            return loadAry;
        }

        /// <summary>
        /// CSV ファイルを DataGridView に読み込み
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="encoding">エンコード</param>
        /// <param name="isTitled">ヘッダーあり (false: なし, true: あり)</param>
        /// <param name="tableName">テーブル名</param>
        /// <param name="dataTable">データ テーブル</param>
        /// <returns>結果 (≦ 0: 保存成功 (保存件数), -2: 先頭行の項目数が異なる)</returns>
        public int ReadCSVFile(string path, Encoding encoding, bool isTitled, string tableName, ref System.Data.DataTable dataTable)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;

            // ファイル情報作成
            if (CreateFileInfo() == Common.FILE_INFO_NG)
            {
                if (cmn.Dba != null)
                {
                    if (cmn.MySqlCnn != null)
                    {
                        // 切削生産計画システム データベースから切断
                        cmn.Dbm.CloseMySqlSchema(cmn.MySqlCnn);
                    }

                    if (cmn.OraCnn[Common.DB_CONFIG_EM] != null)
                    {
                        // EM データベースから切断
                        cmn.Dbm.CloseOraSchema(cmn.OraCnn[Common.DB_CONFIG_EM]);
                    }

                    if (cmn.OraCnn[Common.DB_CONFIG_KK] != null)
                    {
                        // 内製プログラム データベースから切断
                        cmn.Dbm.CloseOraSchema(cmn.OraCnn[Common.DB_CONFIG_KK]);
                    }
                }
            }

            // CSV ファイルを DataGridView に読み込み
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (TextReader sr = new StreamReader(fs, encoding))
            // CSV の先頭行を読む
            using (CsvReader vs = new CsvReader(sr, isTitled))
            {
                Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

                int fieldCount = 0;
                try
                {
                    fieldCount = vs.FieldCount; // 変換元項目数
                }
                catch
                {
                    // 「タイトル行ありの得意先」と「タイトル行なしの変換元ファイル」を選択したときに発生する例外の回避策
                    // ファイルの先頭行がタイトル行と解釈されるので先頭行が重複するらしい
                    // 例外は無視して次の項目数チェックでエラーにする
                }

                // 先頭行の項目数チェック
                // 問題点: 同一得意先にファイル名称フィルターが同一の識別が複数あり、識別がファイル名から取得できない場合、
                //         IsTargetFile() メソッド内で KM0850 を検索すると複数件が返される。
                //         その後、ファイル名のパターン チェックにおいて、必ず先頭データのファイル名称フィルターと一致
                //         してしまうため、実際の識別が 2 件目以降だった場合は誤ったファイル書式が取得されてしまう。
                //         取得された書式のうち項目数が実際の識別の項目数と異なる場合、ConvertFileFormatAsync() メソッド内
                //         の項目数チェックでエラーと判定されてしまう。
                //
                // 対策:   KM0850 から複数件が返った場合、とりあえず先頭の識別の文字コードとタイトル行の有無を使って
                //         変換元 CSV の先頭行を読んで識別を取得したあと、再度 IsTargetFile() メソッドを呼ぶことで
                //         KM0850 からユニークなデータを検索し、正しい情報を取得して項目数の判定に使用することで回避する。

                // インポート先となるテーブルのカラム数を取得
                DataSet dataSetColumnsCmment = new DataSet();
                int tableColumnsCount = cmn.Dbm.GetMySqlTableInfo(ref dataSetColumnsCmment, tableName);

                // 項目数比較
                if (fieldCount != tableColumnsCount) // 先頭行の項目数が異なる
                {
                    Trace.WriteLine("項目数が間違っています。(CSV ファイル = " + fieldCount + " 件, インポート先テーブル = " + tableColumnsCount + "件)");
                    return -2; // 先頭行の項目数が異なる
                }

                // データ テーブルに追加
                // 列を追加
                for (int cnt = 0; cnt < vs.FieldCount; cnt++)
                {
                    Debug.WriteLine("cnt = " + Convert.ToString(cnt));
                    dataTable.Columns.Add();
                }

                // CSV の次行を読む
                while (vs.ReadNextRecord())
                {
                    // データ行を作成
                    DataRow dataRow = dataTable.NewRow();

                    // 作成したデータ行に列の値を設定
                    for (int cnt = 0; cnt < vs.FieldCount; cnt++)
                    {
                        dataRow[cnt] = vs[cnt];
                    }
                    // データ行をデータ テーブルに追加
                    dataTable.Rows.Add(dataRow);
                }
            }
            ret = dataTable.Rows.Count;
            return ret;
        }

        /// <summary>
        /// DatGridView を CSV ファイルに保存
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="path">保存先パス</param>
        /// <returns>結果 (0: 保存成功 (保存件数), -1: 保存失敗, -2: 認証失敗)</returns>
        public int SaveCSVFile(DataGridView dgv, string path)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;

            try
            {
                // 保存用のファイルを開く
                using (StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("shift_jis")))
                {
                    int rowCount = dgv.Rows.Count;

                    // ユーザによる行追加が許可されている場合は、最後の新規入力用の
                    // 1 行分を差し引く
                    if (dgv.AllowUserToAddRows == true)
                    {
                        rowCount = rowCount - 1;
                    }

                    // 行
                    for (int i = -1; i < rowCount; i++)
                    {
                        // リストの初期化
                        List<string> strList = new List<string>();

                        // dgv
                        if (i < 0)
                        {
                            // ヘッダー項目
                            for (int j = 0; j < dgv.Columns.Count; j++)
                            {
                                strList.Add("\"" + dgv.Columns[j].HeaderText + "\"");
                            }
                        }
                        else
                        {
                            // データ項目
                            for (int j = 0; j < dgv.Columns.Count; j++)
                            {
                                strList.Add("\"" + dgv[j, i].Value.ToString() + "\"");
                            }

                        }
                        string[] strArray = strList.ToArray(); // 配列へ変換

                        // CSV 形式に変換
                        string strCsvData = string.Join(",", strArray);

                        writer.WriteLine(strCsvData);
                    }
                    ret = rowCount;
                }
            }
            // 保存先サーバーの認証に失敗すると UnauthorizedAccessException が発生する
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine("UnauthorizedAccessException Message = " + ex.Message);

                // 保存先へ再接続
                ConnectSaveServer();
                ret = Common.SFD_RET_AUTH_FAILED;
            }
            // ファイルの保存に失敗すると Exception が発生する
            catch (Exception e)
            {
                Debug.WriteLine("Exception Source = " + e.Source);
                Debug.WriteLine("Exception Message = " + e.Message);

                // 戻り値でエラー種別を判定
                if (cmn.ConvertDecToHex(e.HResult) == Common.HRESULT_FILE_IN_USE)
                {
                    // ファイル使用中
                    ret = Common.SFD_RET_FILE_IN_USE;
                }
                else
                {
                    // それ以外
                    ret = Common.SFD_RET_SAVE_FAILED;
                }
            }
            return ret;
        }

        /// <summary>
        /// Excel ファイルを開く
        /// </summary>
        /// <param name="cmn">共通クラス</param>
        /// <param name="path">パス</param>
        public void OpenExcelFile(string path)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string excelName = @path;

            oXls = new Excel.Application();
            oXls.Visible = true;

            // Excelファイルをオープンする//
            oWBook = (Excel.Workbook)(oXls.Workbooks.Open(
              excelName,  // オープンするExcelファイル名
              Type.Missing, // （省略可能）UpdateLinks (0 / 1 / 2 / 3)
              Type.Missing, // （省略可能）ReadOnly (True / False )
              Type.Missing, // （省略可能）Format
                            // 1:タブ / 2:カンマ (,) / 3:スペース / 4:セミコロン (;)
                            // 5:なし / 6:引数 Delimiterで指定された文字
              Type.Missing, // （省略可能）Password
              Type.Missing, // （省略可能）WriteResPassword
              Type.Missing, // （省略可能）IgnoreReadOnlyRecommended
              Type.Missing, // （省略可能）Origin
              Type.Missing, // （省略可能）Delimiter
              Type.Missing, // （省略可能）Editable
              Type.Missing, // （省略可能）Notify
              Type.Missing, // （省略可能）Converter
              Type.Missing, // （省略可能）AddToMru
              Type.Missing, // （省略可能）Local
              Type.Missing  // （省略可能）CorruptLoad
            ));
        }

        /// <summary>
        /// Excel ファイルを閉じる
        /// </summary>
        public void CloseExcelFile()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // アプリケーションの終了前に破棄可能なオブジェクトを破棄します。
            if (oWBook != null)
            {
                Marshal.ReleaseComObject(oWBook);
                oWBook = null;
            }

            // アプリケーションの終了前にガベージ コレクトを強制します。
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // アプリケーションを終了します。
            if (oXls != null)
            {
                oXls.Quit();

                // Application オブジェクトを破棄します。
                Marshal.ReleaseComObject(oXls);
                oXls = null;
            }

            // Application オブジェクトのガベージ コレクトを強制します。
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        /// <summary>
        /// Excel を立ち上げる（オプションでアプリの表示／非表示を切り替える）
        /// </summary>
        /// <param name="idx">ファイルシステム設定ファイルのインデックス番号</param>
        public void OpenExcel2(int idx)
        {
            oXls = new Excel.Application();
            oXls.Visible = cmn.FsCd[idx].VisibleExcel;  // Excelのウィンドウの表示/非表示を設定ファイルから取得

            oQRGenerator = new QRCodeGenerator();
        }

        // Debug用にExcelアプリケーションを表示モードにする
        public void ExcelDebug()
        {
            oXls.Visible = true;
        }

        /// <summary>
        /// Excel ファイルを開く
        /// </summary>
        /// <param name="cmn">共通クラス</param>
        /// <param name="path">パス</param>
        public void OpenExcelFile2(string path)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string excelName = @path;

            // Excelファイルをオープンする//
            oWBook = (Excel.Workbook)(oXls.Workbooks.Open(
              excelName,  // オープンするExcelファイル名
              Type.Missing, // （省略可能）UpdateLinks (0 / 1 / 2 / 3)
              ReadOnly: true, // （省略可能）ReadOnly (True / False )
              Type.Missing, // （省略可能）Format
                            // 1:タブ / 2:カンマ (,) / 3:スペース / 4:セミコロン (;)
                            // 5:なし / 6:引数 Delimiterで指定された文字
              Type.Missing, // （省略可能）Password
              Type.Missing, // （省略可能）WriteResPassword
              Type.Missing, // （省略可能）IgnoreReadOnlyRecommended
              Type.Missing, // （省略可能）Origin
              Type.Missing, // （省略可能）Delimiter
              Type.Missing, // （省略可能）Editable
              Type.Missing, // （省略可能）Notify
              Type.Missing, // （省略可能）Converter
              Type.Missing, // （省略可能）AddToMru
              Type.Missing, // （省略可能）Local
              Type.Missing  // （省略可能）CorruptLoad
            ));
            // oXls オブジェクトのアクティブプリンターを [Microsoft XPS Document Writer] に変更
            SetActivePrinter();
            oWSheet = oWBook.Sheets[1];
        }

        /// <summary>
        /// Excel ファイルを閉じる
        /// </summary>
        public void CloseExcelFile2(bool saveChanges)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // アプリケーションの終了前に破棄可能なオブジェクトを破棄します。
            if (oWBook != null)
            {
                oWBook.Close(SaveChanges: saveChanges);
                Marshal.ReleaseComObject(oWBook);
                oWBook = null;
            }

            // アプリケーションの終了前にガベージ コレクトを強制します。
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        /// <summary>
        /// Excel アプリケーションを閉じる
        /// </summary>
        public void CloseExcel2()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // アプリケーションの終了前に破棄可能なオブジェクトを破棄します。
            if (oQRGenerator != null)
            {
                oQRGenerator.Dispose();
            }

            // アプリケーションの終了前に破棄可能なオブジェクトを破棄します。
            if (oWBook != null)
            {
                oWBook.Close(SaveChanges: false);
                Marshal.ReleaseComObject(oWBook);
                oWBook = null;
            }

            // アプリケーションの終了前にガベージ コレクトを強制します。
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // アプリケーションを終了します。
            if (oXls != null)
            {
                oXls.Quit();

                // Application オブジェクトを破棄します。
                Marshal.ReleaseComObject(oXls);
                oXls = null;
            }

            // Application オブジェクトのガベージ コレクトを強制します。
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        /// <summary>
        /// Excel を立ち上げる（オプションでアプリの表示／非表示を切り替える）
        /// </summary>
        /// <param name="idx">ファイルシステム設定ファイルのインデックス番号</param>
        public void ExcelApplication(bool flg)
        {
            oXls = new Excel.Application();
            oXls.Visible = flg;
        }

        /// <summary>
        /// Excel ブックにシートを追加する
        /// </summary>
        /// <param name="num">シート番号</param>
        public void AddNewBook()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);
            oWBook = oXls.Workbooks.Add();
        }

        /// <summary>
        /// Excel ブックにシートを追加する
        /// </summary>
        /// <param name="num">シート番号</param>
        public void AddNewWorksheet(int num)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            oWSheet = oWBook.Sheets[1]; // 先頭シート
            oWBook.Worksheets.Add(oWSheet, Type.Missing, num); // 直前に追加
        }

        /// <summary>
        /// Excel シートの名前を変更する
        /// </summary>
        /// <param name="num">シート番号</param>
        /// <param name="sheetName">シート名称</param>
        public void ChangeWorksheetName(int num, string sheetName)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            oWBook.Worksheets[num].Name = sheetName;
        }

        /// <summary>
        /// Excel ファイル出力
        /// </summary>
        /// <param name="exportDt">データ テーブル</param>
        /// <param name="filepath">ファイルパス</param>
        public void ExportExcel(System.Data.DataTable exportDt, string filepath)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            try
            {
                oXls.DisplayAlerts = false;

                oWSheet = oWBook.Sheets[1];
                oWSheet.Select(Type.Missing);

                for (int col = 0; col < exportDt.Columns.Count; col++)
                {
                    object[,] obj = new object[exportDt.Rows.Count + 1, 1];
                    obj[0, 0] = exportDt.Columns[col].ColumnName;
                    for (int row = 0; row < exportDt.Rows.Count; row++)
                    {
                        obj[row + 1, 0] = exportDt.Rows[row][col].ToString();
                    }

                    var rgn = oWSheet.Range[oWSheet.Cells[1, col + 1], oWSheet.Cells[exportDt.Rows.Count + 1, col + 1]];
                    rgn.Font.Size = 10;
                    rgn.Font.Name = "ＭＳ ゴシック";
                    DataColumn dtcol = exportDt.Columns[col];
                    if (dtcol.DataType.Equals(typeof(string)))
                    {
                        rgn.NumberFormatLocal = "@";
                        rgn.Value2 = obj;
                    }
                    else
                    {
                        rgn.Value2 = obj;
                    }
                }
                // タイトル行を固定
                var rng = oWSheet.Range[oWSheet.Cells[1, 1], oWSheet.Cells[1, exportDt.Columns.Count]];
                rng.Interior.Color = Color.FromArgb(135, 231, 173); // A5SQLMk2.Color
                oWSheet.Cells[2, 1].Select();
                oXls.ActiveWindow.FreezePanes = true;

                // 列幅自動調整
                oWSheet.Columns.AutoFit();
                oWBook.SaveAs(filepath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Excel コード票をデータテーブルへインポート
        //    https://mitosuya.net/excel-high-performance
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns></returns>
        public System.Data.DataTable ReadExcelToDatatble2(string path)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine($"計測開始" + sw);

            dynamic xlApp = null;
            dynamic xlWbooks = null;
            dynamic xlWbook = null;
            dynamic xlSheets = null;
            dynamic xlSheet = null;
            dynamic xlRange = null;
            dynamic xlCell = null;
            System.Data.DataTable dataTable = new System.Data.DataTable();
            try
            {
                Type objectClassType = Type.GetTypeFromProgID("Excel.Application");
                xlApp = Activator.CreateInstance(objectClassType);
                xlApp.ScreenUpdating = false; // screenUpdating;
                xlApp.EnableEvents = false; // enableEvents;
                xlApp.Visible = false;

                /*
                 * 今回ここでは使用しない
                // 再計算の停止
                // https://qiita.com/kob58im/items/780d5b7ddb854b57e98b
                Excel.XlCalculation calcModeBackup = xlApp.Calculation;
                xlApp.Calculation = Excel.XlCalculation.xlCalculationManual;
                xlApp.Calculation = CALCULATION_DEFAULT;
                xlApp.Calculation = false; // calculation;
                */

                xlWbooks = xlApp.Workbooks;
                xlWbook = xlApp.Workbooks.Open(
                    @path,
                    Type.Missing,   // （省略可能）不明
                    ReadOnly: true  // （省略可能）ReadOnly (True / False )
                );

                xlSheets = xlWbook.Worksheets;
                foreach (Excel.Worksheet sheet in xlSheets)
                {
                    if (sheet.Name == "コード票" || sheet.Name == "コード表" ||
                        sheet.Name == "ｺｰﾄﾞ票" || sheet.Name == "ｺｰﾄﾞ表")
                    {
                        xlSheet = sheet;
                        break;
                    }
                }

                // 使用列数と行数を取得
                int rowCount = xlSheet.UsedRange.Rows.Count;
                int colCount = xlSheet.UsedRange.Columns.Count;             // 例）38列

                // ExcelRangeをオブジェクト配列にコピー
                object[,] values = new object[rowCount, colCount - 1];      // 例）0～37配列
                try
                {
                    // ※※※ 4行目がタイトルである事が前提 ※※※
                    xlRange = xlSheet.Range($"A4:{GetCellA1(colCount)}{rowCount}");
                    values = xlRange.Value;
                }
                finally
                {
                    Marshal.ReleaseComObject(xlRange);
                }

                // オブジェクトの１行目からデータテーブルヘッダーを作成
                for (int j = 1; j <= colCount; j++)
                {
                    dataTable.Columns.Add(values[1, j].ToString());
                }

                // オブジェクトの２行目からデータテーブルを作成
                for (int row = 2; row <= rowCount; row++)
                {
                    if (values[row, 1] is null) break;

                    string s = values[row, 1].ToString();   // 品番列の文字列
                    if (s != "" && s != "0" && s != "z")    // 変な品番は対象外にする
                    {
                        DataRow dr = dataTable.NewRow();
                        for (int col = 1; col <= colCount; col++)
                        {
                            if (values[row, col] != null)
                            {
                                dr[col - 1] = values[row, col].ToString();
                            }
                        }
                        dataTable.Rows.InsertAt(dr, dataTable.Rows.Count + 1);
                    }
                }
            }
            catch
            {
                dataTable = null;
            }
            finally
            {
                xlApp.EnableEvents = true;
                xlApp.ScreenUpdating = true;
                // xlApp.Visible = true;

                //COMオブジェクトの開放
                if (xlSheet != null) Marshal.ReleaseComObject(xlSheet);
                if (xlSheets != null) Marshal.ReleaseComObject(xlSheets);
                if (xlWbook != null)
                {
                    xlApp.DisplayAlerts = false;
                    xlWbook.Close();
                    // Marshal.ReleaseComObject(xlWbook); Closeするとオブジェクトがなくなる？？？
                    xlWbook = null;
                }
                if (xlWbooks != null) Marshal.ReleaseComObject(xlWbooks);

                //Excelアプリケーション終了
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
                xlApp = null;

                // アプリケーションの終了前にガベージ コレクトを強制します。
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                //計測結果表示
                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                Console.WriteLine($"{ts.TotalSeconds:0.00}");
            }
            //
            return dataTable;
        }

        /// <summary>
        /// 列番号からエクセルの列名を得る　(例 5 → "E"）
        /// https://nonbiri-dotnet.blogspot.com/2016/12/blog-post_10.html
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        static public string GetCellA1(int c)
        {
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string s = "";

            for (; c > 0; c = (c - 1) / 26)
            {
                int n = (c - 1) % 26;
                s = alpha.Substring(n, 1) + s;
            }
            return s;
        }

        /// <summary>
        /// Excel コード票をデータテーブルへインポート
        /// COMアクセスが遅すぎて廃止
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable ReadExcelToDatatble()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            System.Data.DataTable dataTable = new System.Data.DataTable();
            Excel.Worksheet excelSheet = new Excel.Worksheet();
            //Excel.Range range;
            try
            {
                // コード票シートを検索
                foreach (Excel.Worksheet s in oWBook.Worksheets)
                {
                    if (s.Name == "コード票" || s.Name == "ｺｰﾄﾞ票")
                    {
                        excelSheet = s;
                        break;
                    }
                }
                //range = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[9999, 37]];
                //range = excelSheet.UsedRange;
                int cl = 37;    // 最終列 （旧：range.Columns.Count）
                int hl = 4;     // タイトル行番号

                // loop through each row and add values to our sheet
                //int rowcount = range.Rows.Count;

                //create the header of table
                for (int j = 1; j <= cl; j++)
                {
                    dataTable.Columns.Add(Convert.ToString
                                         (excelSheet.Cells[hl, j].Value2), typeof(string));
                }

                //filling the table from  excel file                
                for (int row = 7; row <= 9999; row++)
                {
                    string s = Convert.ToString(excelSheet.Cells[row, 1].Value2);
                    if (s == "" || s == "z") break;

                    DataRow dr = dataTable.NewRow();
                    for (int col = 1; col <= cl; col++)
                    {
                        dr[col - 1] = Convert.ToString(excelSheet.Cells[row, col].Value2);
                    }
                    dataTable.Rows.InsertAt(dr, dataTable.Rows.Count + 1);
                }

                //now close the workbook and make the function return the data table
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                excelSheet = null;
            }
        }

        /// <summary>
        /// データテーブルから Excel シートへエクスポート
        /// </summary>
        /// <param name="dataTable">データ テーブル</param>
        /// <param name="excelworkBook">ワークブック オブジェクト</param>
        /// <param name="worksheetName">ワークシート名称</param>
        /// <param name="saveAsLocation">保存先</param>
        /// <param name="ReportType">レポート種別</param>
        /// <param name="copyColumns">コピー列</param>
        /// <param name="odrCount">設備数</param>
        /// <returns></returns>
        public int WriteDataTableToExcel(System.Data.DataTable dataTable, Excel.Workbook excelworkBook,
            string worksheetName, string saveAsLocation, string ReportType, int copyColumns, ref int[] odrCount)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            Excel.Worksheet excelSheet;
            int ret = 0;

            try
            {
                // Work sheet
                oXls.ActiveWorkbook.Sheets.Add();
                excelSheet = (Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Name = worksheetName;

                // loop through each row and add values to our sheet
                int rowCount = 0;
                var flg_odcd2 = Convert.ToInt32(dataTable.Rows[0][3]);

                foreach (DataRow datarow in dataTable.Rows)
                {
                    rowCount++; // セルの行数 (= DataTable の行数 + 1)

                    // 設備ごとの品番数取得
                    if (Convert.ToInt32(datarow[3]) != flg_odcd2)
                    {
                        odrCount[flg_odcd2 - 70000 - 1] = Convert.ToInt32(excelSheet.Cells[(rowCount - copyColumns), 2].Value);
                        flg_odcd2 = Convert.ToInt32(datarow[3]);
                    }

                    // Filling the excel file 
                    for (int colCount = 1; colCount <= dataTable.Columns.Count; colCount++)
                    {
                        excelSheet.Cells[rowCount, colCount] = Convert.ToString(datarow[colCount - 1]);
                    }
                }
                // 最終設備の品番数取得
                rowCount++;
                odrCount[flg_odcd2 - 70000 - 1] = Convert.ToInt32(excelSheet.Cells[(rowCount - copyColumns), 2].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret = -1;
            }
            finally
            {
                excelSheet = null;
            }
            return ret;
        }

        /// <summary>
        /// データテーブルから Excel シートへエクスポート
        /// </summary>
        /// <param name="dataTable">データ テーブル</param>
        /// <param name="excelworkBook">ワークブック オブジェクト</param>
        /// <param name="worksheetName">ワークシート名称</param>
        /// <param name="saveAsLocation">保存先</param>
        /// <param name="ReportType">レポート種別</param>
        /// <param name="copyColumns">コピー列</param>
        /// <returns></returns>
        public int WriteDataTableToExcel(System.Data.DataTable dataTable, Excel.Workbook excelworkBook,
            string worksheetName, string saveAsLocation, string ReportType, int copyColumns)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            Excel.Worksheet excelSheet;
            int ret = 0;

            try
            {
                // Work sheet
                oXls.ActiveWorkbook.Sheets.Add();
                excelSheet = (Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Name = worksheetName;

                // loop through each row and add values to our sheet
                int rowCount = 0;
                //var flg_mcno = Convert.ToString(dataTable.Rows[0][3]);

                foreach (DataRow datarow in dataTable.Rows)
                {
                    rowCount++; // セルの行数 (= DataTable の行数 + 1)

                    // Filling the excel file 
                    for (int colCount = 1; colCount <= dataTable.Columns.Count; colCount++)
                    {
                        excelSheet.Cells[rowCount, colCount] = Convert.ToString(datarow[colCount - 1]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret = -1;
            }
            finally
            {
                excelSheet = null;
            }
            return ret;
        }

        /// <summary>
        /// 列削除 (1 列)
        /// </summary>
        public void DeleteColumn(string sheetName, string column)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            oWBook.Worksheets[sheetName].Select(true);
            oXls.ActiveSheet.Columns(column + ":" + column).Select();
            oXls.Selection.Delete(Excel.XlDirection.xlToLeft);
        }

        /// <summary>
        /// 列削除 (連続する複数列)
        /// </summary>
        public void DeleteColumn(string sheetName, string fromColumn, string toColumn)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            oWBook.Worksheets[sheetName].Select(true);
            oXls.ActiveSheet.Columns(fromColumn + ":" + toColumn).Select();
            oXls.Selection.Delete(Excel.XlDirection.xlToLeft);
        }

        /// <summary>
        /// シート削除
        /// </summary>
        /// <param name="sheetName">シート名称</param>
        public void DeleteSheet(string sheetName)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            oXls.DisplayAlerts = false;
            oWBook.Worksheets[sheetName].Select(true);
            oWBook.ActiveSheet.Delete();
            oXls.DisplayAlerts = true;
        }

        /// <summary>
        /// 最終行列取得
        /// </summary>
        /// <param name="LastRow">最終行</param>
        /// <param name="LastColumn">最終列</param>
        public void GetLastRowAndColumn(out dynamic LastRow, out dynamic LastColumn)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            Debug.WriteLine("var activeSheet = oXls.ActiveWorkbook.ActiveSheet");
            var activeSheet = oXls.ActiveWorkbook.ActiveSheet;
            LastRow = oWBook.ActiveSheet.UsedRange.Rows.Count;
            LastColumn = oWBook.ActiveSheet.UsedRange.Columns.Count;
        }


        /// <summary>
        /// DatTable を CSV ファイルに保存
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="path">保存先パス</param>
        /// <param name="encoding">ファイル出力形式 ("SJIS":shift_jis, "UFT8":utf-8(BOM付き))</param>
        /// 
        /// <returns>結果 (0: 保存成功 (保存件数), -1: 保存失敗, -2: 認証失敗)</returns>
        public int SaveCSVFile(System.Data.DataTable dt, string path, string encoding)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;

            try
            {
                // 保存用のファイルを開く
                var sjisEncoding = Encoding.GetEncoding("shift_jis");
                var utf8Encoding = Encoding.UTF8;
                if (encoding == "SJIS" && encoding == "UTF8")
                {
                    throw new Exception("パラメーター異常\n内部異常が発生しました．");
                }
                using (StreamWriter writer = new StreamWriter(path, false, (encoding == "SJIS" ? sjisEncoding : utf8Encoding)))
                {
                    int rowCount = dt.Rows.Count;

                    // リストの初期化
                    List<string> strList = new List<string>();

                    // ヘッダー項目
                    foreach (DataColumn dc in dt.Columns)
                    {
                        strList.Add("\"" + dc.ColumnName + "\"");
                    }
                    string[] strArray = strList.ToArray(); // 配列へ変換
                    string strCsvData = string.Join(",", strArray);// CSV 形式に変換
                                                                   //writer.WriteLine(strCsvData);

                    // データ項目行
                    foreach (DataRow dr in dt.Rows)
                    {
                        // 列列挙
                        strList.Clear();
                        foreach (DataColumn column in dt.Columns)
                        {
                            // strList.Add("\"" + dr[column].ToString() + "\""); ダブルクォーテーション("")が必要な場合はこれ
                            strList.Add(dr[column].ToString());
                        }
                        strArray = strList.ToArray(); // 配列へ変換

                        // CSV 形式に変換
                        strCsvData = string.Join(",", strArray);

                        writer.WriteLine(strCsvData);

                    }
                    ret = rowCount;
                }
            }
            // 保存先サーバーの認証に失敗すると UnauthorizedAccessException が発生する
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine("UnauthorizedAccessException Message = " + ex.Message);

                // 保存先へ再接続
                ConnectSaveServer();
                ret = Common.SFD_RET_AUTH_FAILED;
            }
            // ファイルの保存に失敗すると Exception が発生する
            catch (Exception e)
            {
                Debug.WriteLine("Exception Source = " + e.Source);
                Debug.WriteLine("Exception Message = " + e.Message);

                // 戻り値でエラー種別を判定
                if (cmn.ConvertDecToHex(e.HResult) == Common.HRESULT_FILE_IN_USE)
                {
                    // ファイル使用中
                    ret = Common.SFD_RET_FILE_IN_USE;
                }
                else
                {
                    // それ以外
                    ret = Common.SFD_RET_SAVE_FAILED;
                }
            }
            return ret;
        }

        // oXls オブジェクトのアクティブプリンターを [Microsoft XPS Document Writer] に変更
        public void SetActivePrinter()
        {
            string printerName = "Microsoft XPS Document Writer";
            string printerPort = "";
            // プリンターポート番号を取得する
            string keyAddress = @"Software\Microsoft\Windows NT\CurrentVersion\Devices";
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(keyAddress))
            {
                if (registryKey != null)
                {
                    // 例）"winspool,Ne00:"
                    object value = registryKey.GetValue(printerName);
                    if (value != null)
                    {
                        string[] split = value.ToString().Split(',');
                        if (split.Length >= 2)
                        {
                            printerPort = split[1];
                        }
                    }
                }
            }
            // アクティブプリンターを設定する
            if ((!oXls.ActivePrinter.StartsWith(printerName)) && (printerPort != ""))
            {
                oXls.ActivePrinter = $"{printerName} on {printerPort}";
            }
        }

        // Excelコピペした後に前のデータをクリアする
        private void ClearCardByPage(ref int rowbase)
        {
            for (int row = rowbase; row <= rowbase + 21; row = row + 21)
            {
                for (int col = 2; col <= 11; col = col + 9)
                {
                    oWSheet.Cells[row + 0, col].Value = String.Empty;
                    oWSheet.Cells[row + 3, col].Value = String.Empty;
                    oWSheet.Cells[row + 4, col].Value = String.Empty;
                    oWSheet.Cells[row + 5, col].Value = String.Empty;
                    oWSheet.Cells[row + 6, col].Value = String.Empty;
                    oWSheet.Cells[row + 4, col + 5].Value = String.Empty;
                    oWSheet.Cells[row + 5, col + 5].Value = String.Empty;
                    oWSheet.Cells[row + 6, col + 5].Value = String.Empty;
                    oWSheet.Cells[row + 8, col - 1].Value = String.Empty;
                    oWSheet.Cells[row + 8, col].Value = String.Empty;
                    oWSheet.Cells[row + 10, col - 1].Value = String.Empty;
                    oWSheet.Cells[row + 10, col].Value = String.Empty;
                    oWSheet.Cells[row + 12, col - 1].Value = String.Empty;
                    oWSheet.Cells[row + 12, col].Value = String.Empty;
                    oWSheet.Cells[row + 14, col - 1].Value = String.Empty;
                    oWSheet.Cells[row + 14, col].Value = String.Empty;
                    oWSheet.Cells[row + 16, col].Value = String.Empty;
                    oWSheet.Cells[row + 18, col].Value = String.Empty;
                }
            }

        }

        // 製造指示書カードテンプレートオブジェクトの作成 [A1:H20]
        public void CreateTemplateOrderCard()
        {
            oRange = oWSheet.Range[oWSheet.Cells[1, 1], oWSheet.Cells[20, 8]]; // ※20行8列
            templateOrderCardObject = oRange.Value;
            //// 以下デバッグ用（Range を Object に代入してコンソールで確認）
            //templateOrderCardObject[9, 1] = null;    // 工程①
            //templateOrderCardObject[11, 1] = null;   // 工程②
            //templateOrderCardObject[13, 1] = null;   // 工程③
            //templateOrderCardObject[15, 1] = null;   // 工程④
            //for (int row = 1; row <= templateOrderCardObject.GetLength(0); row++)
            //{
            //    for (int col = 1; col <= templateOrderCardObject.GetLength(1); col++)
            //    {
            //        if (templateOrderCardObject[row, col] != null)  // ※セルの結合している場所はnullになっていた
            //            Console.WriteLine(templateOrderCardObject[row, col].ToString());
            //    }
            //}
        }
        // 最終頁の残り分のデータをクリア
        public void ClearZanOrderCard(int cardCnt)
        {
            if (cardCnt == 0) return;
            var rowoff = templateOrderCardObject.GetLength(0);
            var coloff = templateOrderCardObject.GetLength(1);
            int remain;
            int pagesu = Math.DivRem(cardCnt, 4, out remain);
            int row = (pagesu * (rowoff + 1) * 2) + 1;
            int col = 1;

            // 右上
            if (remain <= 1)
            {
                oRange = oWSheet.Range[oWSheet.Cells[row, col + coloff + 1]
                    , oWSheet.Cells[row + rowoff - 1, col + coloff + coloff]];
                oRange.Value = templateOrderCardObject;
            }
            // 左下
            if (remain <= 2)
            {
                oRange = oWSheet.Range[oWSheet.Cells[row + rowoff + 1, col]
                , oWSheet.Cells[row + rowoff + 1 + rowoff - 1, col + coloff - 1]];
                oRange.Value = templateOrderCardObject;
            }
            // 右下
            if (remain <= 3)
            {
                oRange = oWSheet.Range[oWSheet.Cells[row + rowoff + 1, col + coloff + 1]
                , oWSheet.Cells[row + rowoff + 1 + rowoff - 1, col + coloff + coloff]];
                oRange.Value = templateOrderCardObject;
            }
        }

        // 製造指示カードに記載するEX:検査工程の工程名を加工して返却
        private string getEXKTName(ref string mccd)
        {
            if (mccd.StartsWith("BT"))
            {
                return "ﾊﾞﾘ取り";
            }
            else if (mccd.StartsWith("MT"))
            {
                return "面取り";
            }
            else if (mccd.StartsWith("F"))
            {
                return "平行度";
            }
            else if (mccd.StartsWith("MK"))
            {
                return "検査";
            }
            return mccd;
        }

        // 製造指示カードに記載するEX:検査工程の設備名を加工して返却
        private string getEXMCName(ref string mccd)
        {
            if (mccd.StartsWith("F1"))
            {
                return "手動測定器";
            }
            else if (mccd.StartsWith("F2"))
            {
                return "画像測定器";
            }
            else if (mccd.StartsWith("MK"))
            {
                return "目視検査";
            }
            return mccd;
        }

        // 製造指示カードに記載する出荷先の工程名を加工して返却
        private string getSTKTName(ref string store)
        {
            if (store == "")
            {
                return "";
            }
            else if (store.EndsWith("ﾀﾅｺﾝ") || store.EndsWith("タナコン"))
            {
                return "入庫";
            }
            else
            {
                return "出荷";
            }
        }
        // 製造指示カードに記載する出荷先の設備名を加工して返却
        private string getSTMCName(ref string store)
        {
            if (store == "")
            {
                return "";
            }
            else if (store.EndsWith("ﾀﾅｺﾝ") || store.EndsWith("タナコン"))
            {
                return "ﾀﾅｺﾝ";
            }
            else
            {
                return "事務所前";
            }
        }

        // 1カード作成（DataRow1件分を作成）
        public void SetOrderCard(ref DataRow r, ref int row, ref int col, int loopCnt, int loopMax)
        {
            // テンプレートオブジェクトをクローン
            object[,] obj = templateOrderCardObject.Clone() as object[,];
            var rowoff = templateOrderCardObject.GetLength(0);
            var coloff = templateOrderCardObject.GetLength(1);

            if (row > 40) // 2頁目以降は1頁目の雛形書式をコピペ＆行の高さも1ページ目に合わせる
            {
                if ((row - 1) % 42 == 0 && col <= 2)
                {
                    // A1:Q42を対象の行にコピペ
                    Excel.Range range = oWSheet.Range[oWSheet.Cells[1, 1], oWSheet.Cells[42, 17]];
                    range.Copy(oWSheet.Cells[row, 1]);
                    // 毎回クリア処理を入れるのはかなり遅いので廃止 ⇒ ClearCardByPage(ref row);
                    // COMアクセスを減らす為、42回ループは廃止 ⇒ 行の高さはコピペされないのでRows(1:42)を複製
                    //for (int y = 1; y <= 42; y++)
                    //    oWSheet.Rows[row + y - 1].RowHeight = oWSheet.Rows[y].RowHeight;
                    double rh1 = oWSheet.Rows[1].RowHeight;
                    double rh4 = oWSheet.Rows[4].RowHeight;
                    double rh9 = oWSheet.Rows[9].RowHeight;
                    double rh19 = oWSheet.Rows[19].RowHeight;
                    for (int z = 0; z <= 21; z += 21) // A4用紙の上段、下段
                    {
                        // 品番1行目～3行目
                        range = oWSheet.Range[oWSheet.Rows[row + z + 0], oWSheet.Rows[row + z + 2]];
                        range.RowHeight = rh1;
                        // 品名4行目～注文数量7行目
                        range = oWSheet.Range[oWSheet.Rows[row + z + 3], oWSheet.Rows[row + z + 6]];
                        range.RowHeight = rh4;
                        // 工程①9行目～検査18行目
                        range = oWSheet.Range[oWSheet.Rows[row + z + 8], oWSheet.Rows[row + z + 17]];
                        range.RowHeight = rh9;
                        // 備考19行目～20行目
                        range = oWSheet.Range[oWSheet.Rows[row + z + 18], oWSheet.Rows[row + z + 19]];
                        range.RowHeight = rh19;
                    }
                    // 行タイトル8行目を上段、下段に複製
                    double rh8 = oWSheet.Rows[8].RowHeight;
                    oWSheet.Rows[row + 7].RowHeight = rh8;
                    oWSheet.Rows[row + 21 + 7].RowHeight = rh8;
                    // 最終行（調整用）21行目を上段、下段に複製
                    double rh21 = oWSheet.Rows[21].RowHeight;
                    oWSheet.Rows[row + 20].RowHeight = rh21;
                    oWSheet.Rows[row + 21 + 20].RowHeight = rh21;

                }
            }

            // 値を設定
            obj[1, 2] = r["HMCD"].ToString();
            obj[4, 2] = r["HMNM"].ToString();
            obj[5, 2] = r["ODRNO"].ToString();
            // 手配完了日付はExcel雛形側でフォーマットを変えているのでFull日付でデータはセット
            obj[6, 2] = DateTime.Parse(r["EDDT"].ToString()).ToString("yyyy/MM/dd");
            obj[7, 2] = r["ODRQTY"].ToString();
            obj[5, 7] = r["MATESIZE"].ToString();
            obj[6, 7] = r["LENGTH"].ToString();
            // 収容数関連
            var boxcd1 = (r["BOXCD"].ToString() != "" && r["BOXQTY"].ToString() != "") ? r["BOXCD"].ToString() + "(" : "";
            var boxcd2 = (r["BOXCD"].ToString() != "" && r["BOXQTY"].ToString() != "") ? ")" : "";
            var boxinfo = (loopMax > 1) ? $"({loopCnt} / {loopMax})" : "";
            obj[7, 7] = boxcd1 + r["BOXQTY"].ToString() + boxcd2 + boxinfo;
            // タナコン関連
            var store = (r["STORE"].ToString() == "") ? "調査開始" : r["STORE"].ToString();
            // 各工程順を設定
            if (r["KT1MCGCD"].ToString() == "")
            {
                obj[9, 1] = getSTKTName(ref store);
                obj[9, 2] = getSTMCName(ref store);
                store = "";
            }
            else
            {
                var mccd = r["KT1MCCD"].ToString();
                if (r["KT1MCGCD"].ToString() == "EX")
                {
                    obj[9, 1] = getEXKTName(ref mccd);
                    obj[9, 2] = getEXMCName(ref mccd);
                }
                else
                {
                    obj[9, 1] = "工程①";
                    obj[9, 2] = RemoveDuplicates(r["KT1MCGCD"].ToString(), mccd, 1);
                }
            }
            if (r["KT2MCGCD"].ToString() == "")
            {
                obj[11, 1] = getSTKTName(ref store);
                obj[11, 2] = getSTMCName(ref store);
                store = "";
            }
            else
            {
                var mccd = r["KT2MCCD"].ToString();
                if (r["KT2MCGCD"].ToString() == "EX")
                {
                    obj[11, 1] = getEXKTName(ref mccd);
                    obj[11, 2] = getEXMCName(ref mccd);
                }
                else
                {
                    obj[11, 1] = "工程②";
                    obj[11, 2] = RemoveDuplicates(r["KT2MCGCD"].ToString(), mccd, 1);
                }
            }
            if (r["KT3MCGCD"].ToString() == "")
            {
                obj[13, 1] = getSTKTName(ref store);
                obj[13, 2] = getSTMCName(ref store);
                store = "";
            }
            else
            {
                var mccd = r["KT3MCCD"].ToString();
                if (r["KT3MCGCD"].ToString() == "EX")
                {
                    obj[13, 1] = getEXKTName(ref mccd);
                    obj[13, 2] = getEXMCName(ref mccd);
                }
                else
                {
                    obj[13, 1] = "工程③";
                    obj[13, 2] = RemoveDuplicates(r["KT3MCGCD"].ToString(), mccd, 1);
                }
            }
            if (r["KT4MCGCD"].ToString() == "")
            {
                obj[15, 1] = getSTKTName(ref store);
                obj[15, 2] = getSTMCName(ref store);
                store = "";
            }
            else
            {
                var mccd = r["KT4MCCD"].ToString();
                if (r["KT4MCGCD"].ToString() == "EX")
                {
                    obj[15, 1] = getEXKTName(ref mccd);
                    obj[15, 2] = getEXMCName(ref mccd);
                }
                else
                {
                    obj[15, 1] = "工程④";
                    obj[15, 2] = RemoveDuplicates(r["KT4MCGCD"].ToString(), mccd, 1);
                }
            }
            if (r["KT5MCGCD"].ToString() == "")
            {
                obj[17, 1] = getSTKTName(ref store);
                obj[17, 2] = getSTMCName(ref store);
                store = "";
            }
            else
            {
                var mccd = r["KT5MCCD"].ToString();
                if (r["KT5MCGCD"].ToString() == "EX")
                {
                    obj[17, 1] = getEXKTName(ref mccd);
                    obj[17, 2] = getEXMCName(ref mccd);
                }
                else
                {
                    obj[17, 1] = "工程⑤";
                    obj[17, 2] = RemoveDuplicates(r["KT5MCGCD"].ToString(), r["KT5MCCD"].ToString(), 1);
                }
            }
            var fontsize = "NORMAL";
            if (r["KT6MCGCD"].ToString() == "")
            {
                if (store != "")
                {
                    obj[17, 1] += "\n" + getSTKTName(ref store);
                    obj[17, 2] += "\n" + getSTMCName(ref store);
                }
            }
            else
            {
                var mccd = r["KT6MCCD"].ToString();
                var tmp = RemoveDuplicates(r["KT6MCGCD"].ToString(), mccd, 1);
                if (tmp.StartsWith("EX-MT"))
                {
                    obj[17, 1] += "\n面取り";
                    obj[17, 2] += "\n" + mccd;
                }
                else if (tmp.StartsWith("EX-BT"))
                {
                    obj[17, 1] += "\nﾊﾞﾘ取り";
                    obj[17, 2] += "\n" + mccd;
                }
                else if (tmp.StartsWith("EX-F"))
                {
                    obj[17, 1] += "\n平行度";
                    obj[17, 2] += "\n" + tmp;
                }
                else
                {
                    obj[17, 1] += "\n" + "工程⑥";
                    obj[17, 2] += "\n" + tmp;
                }
                if (store != "")
                {
                    fontsize = "SMALLER";
                    obj[17, 1] += "\n" + getSTKTName(ref store);
                    obj[17, 2] += "\n" + getSTMCName(ref store);
                }
            }
            var note = r["NOTE"].ToString();
            if (r["PARTNER"].ToString() != "") note += "\n" + r["PARTNER"].ToString();
            obj[19, 2] = note;

            // 設定したオブジェクトをレンジに貼り付け
            oRange = oWSheet.Range[oWSheet.Cells[row, col], oWSheet.Cells[row + rowoff - 1, col + coloff - 1]];
            oRange.Value = obj;

            // フォントサイズの変更
            if (fontsize == "NORMAL")
            {
                oWSheet.Cells[row + 16, col + 0].Font.Size = 12;
                oWSheet.Cells[row + 16, col + 1].Font.Size = 16;
            }
            else if (fontsize == "SMALLER")
            {
                oWSheet.Cells[row + 16, col + 0].Font.Size = 10;
                oWSheet.Cells[row + 16, col + 1].Font.Size = 10;
            }

            // スマート棚コン用QRコード画像ファイルの作成と保存
            string tempFile1 = @Path.GetTempPath() + Common.QR_HMCD_IMG;
            using (QRCodeData qrCodeData = oQRGenerator.CreateQrCode(
                $"{r["HMCD"].ToString()}", QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeImage = qrCode.GetGraphic(20);
                using (MemoryStream ms = new MemoryStream(qrCodeImage))
                {
                    // QRコードを作成しビットマップにした後ファイルに保存
                    using (Bitmap qrBmp = new Bitmap(ms))
                    {
                        qrBmp.Save(tempFile1);
                    }
                }
            }
            // 画像が保存できたか確認 
            if (!System.IO.File.Exists(tempFile1)) Thread.Sleep(100);
            // テスト用のQRコード画像
            //var fn = @"\\kmtsvr\共有SVEM02\Koken\切削生産計画システム\雛形\QR.bmp";// こちらサンプル画像
            // スマート棚コン用QRコードをExcelに作成
            Excel.Range rng = oWSheet.Cells[row, col + 7];
            Shape shp = oWSheet.Shapes.AddPicture2(
                tempFile1
                , Office.MsoTriState.msoFalse   // LinkToFile           図を作成元のファイルにリンクするかどうか
                , Office.MsoTriState.msoTrue    // SaveWithDocument     上記がFalseの場合、こちらをTrueにしないと落ちる（ハマった）
                , rng.Left + 8                  // Left [ Single ]
                , rng.Top + 12                  // Top [Single]
                , 42                            // Width [Single]  (旧：rng.Width - 14）
                , 44                            // Height [Single]（旧：rng.Height - 14）
                , Office.MsoPictureCompress.msoPictureCompressFalse //Compress 画像を挿入するときに圧縮するかどうか
            );
            shp.Left = (float)rng.Left + 8;
            shp.Top = (float)rng.Top + 12;
            shp.Placement = XlPlacement.xlFreeFloating; // セルに合わせて移動やサイズ変更しない



            // i-Reporter用QRコード画像ファイルの作成と保存
            string tempFile2 = @Path.GetTempPath() + Common.QR_ODRNO_IMG;
            using (QRCodeData qrCodeData = oQRGenerator.CreateQrCode(
                $"{r["ODRNO"].ToString()}\t{r["HMCD"].ToString()}", QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeImage = qrCode.GetGraphic(20);
                using (MemoryStream ms = new MemoryStream(qrCodeImage))
                {
                    // QRコードを作成しビットマップにした後ファイルに保存
                    using (Bitmap qrBmp = new Bitmap(ms))
                    {
                        qrBmp.Save(tempFile2);
                    }
                }
            }
            //画像を保存できたかの確認 
            if (!System.IO.File.Exists(tempFile2)) Thread.Sleep(100);
            // i-Reporter用QRコード作成
            rng = oWSheet.Cells[row + 18, col + 7];
            shp = oWSheet.Shapes.AddPicture2(
                tempFile2
                , Office.MsoTriState.msoFalse   // LinkToFile           図を作成元のファイルにリンクするかどうか
                , Office.MsoTriState.msoTrue    // SaveWithDocument     上記がFalseの場合、こちらをTrueにしないと落ちる（ハマった）
                , rng.Left + 8                  // Left [ Single ]
                , rng.Top + 12                  // Top [Single]
                , 42                            // Width [Single]  (旧：rng.Width - 14）
                , 44                            // Height [Single]（旧：rng.Height - 14）
                , Office.MsoPictureCompress.msoPictureCompressFalse //Compress 画像を挿入するときに圧縮するかどうか
            );
            shp.Left = (float)rng.Left + 8;
            shp.Top = (float)rng.Top + 12;
            shp.Placement = XlPlacement.xlFreeFloating; // セルに合わせて移動やサイズ変更しない

        }

        /// <summary>
        /// 現在開いているブックをPDFに変換して保存
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        public void ExportExcelToPDF(string filePath)
        {
            oWBook.ExportAsFixedFormat(
                Type: Excel.XlFixedFormatType.xlTypePDF,
                Filename: $@"{filePath}",
                Quality: Excel.XlFixedFormatQuality.xlQualityStandard
            );

            // PDFを標準アプリケーションで開く
            Process.Start($@"{filePath}");
        }

        // ①工程グループコードと設備コードがダブっている場合は１つに集約
        // ②ダブっていない場合は[-]で連結
        // ③工程①(mode==1)以外の場合は、先頭に連結文字[ > ]を挿入し返却
        private string RemoveDuplicates(string mcgcd, string mccd, int modeConnect)
        {
            string prefix = string.Empty;
            if (mcgcd == null) return string.Empty;
            if (modeConnect != 1) prefix += " > ";
            if (mcgcd != string.Empty)
            {
                if (mcgcd == mccd) return prefix + mcgcd;
                if (mcgcd == "SK" && mccd == "XW") return prefix + "XW"; // 設備名の個別対応
                return prefix + $"{mcgcd}-{mccd}";
            }
            return string.Empty;
        }

        // 内示カードテンプレートオブジェクトの作成 [A1:H10]
        public void CreateTemplatePlanCard()
        {
            oRange = oWSheet.Range[oWSheet.Cells[1, 1], oWSheet.Cells[10, 8]]; // ※10行8列
            templatePlanCardObject = oRange.Value;
        }

        // 最終頁の残り分のデータをクリア
        public void ClearZanPlanCard(int cardCnt)
        {
            if (cardCnt == 0) return;
            var rowoff = templatePlanCardObject.GetLength(0);
            var coloff = templatePlanCardObject.GetLength(1);
            int remain;
            int pagesu = Math.DivRem(cardCnt, 10, out remain);
            int startrow = (pagesu) * ((rowoff + 1) * 5) + 1; // 端数頁の開始行番号
            // クリア開始行を特定 -1して/2してroundupして * rowoff
            int row = startrow + ((int)Math.Ceiling((remain - 1) / 2d) * (rowoff + 1)); // 余りが終わった以降の行番号
            for (int clearCnt = remain; clearCnt < 10; clearCnt++)
            {
                // ←左
                if (clearCnt % 2 == 0)
                {
                    oRange = oWSheet.Range[oWSheet.Cells[row, 1]
                    , oWSheet.Cells[row + rowoff - 1, coloff]];
                    oRange.Value = templatePlanCardObject;
                }
                // 右→
                else
                {
                    oRange = oWSheet.Range[oWSheet.Cells[row, coloff + 2]
                        , oWSheet.Cells[row + rowoff - 1, coloff + 2 + coloff - 1]];
                    oRange.Value = templatePlanCardObject;
                    row += rowoff + 1;
                }
            }
        }

        // 1カード作成（DataRow1件分を作成）
        public void SetPlanCard(ref DateTime cardDay, ref DataRow r, ref int row, ref int col
            , ref System.Data.DataTable materialDt)
        {
            // テンプレートオブジェクトをクローン
            object[,] obj = templatePlanCardObject.Clone() as object[,];
            var rowoff = templatePlanCardObject.GetLength(0);
            var coloff = templatePlanCardObject.GetLength(1);

            if (row > 50) // 2頁目以降に1頁目の雛形書式をコピペ＆行の高さも1ページ目に合わせる
            {
                if ((row - 1) % 55 == 0 && col <= 2)
                {
                    // A1:Q55をコピペ
                    var range = oWSheet.Range[oWSheet.Cells[1, 1], oWSheet.Cells[55, 17]];
                    range.Copy(oWSheet.Cells[row, 1]);
                    // COMアクセスが遅いので廃止 ⇒ ClearCardByPage(ref row);
                    // COMアクセスが遅いので廃止 ⇒ 行の高さはコピペされないのでRows(1:55)を複製
                    //for (int y = 1; y <= 55; y++)
                    //    oWSheet.Rows[row + y - 1].RowHeight = oWSheet.Rows[y].RowHeight;
                    double rh1 = oWSheet.Rows[1].RowHeight;     // 全行の高さ
                    double rh11 = oWSheet.Rows[11].RowHeight;   // 調整用行の高さ
                    // 全行の高さを一旦設定、雛形シート1行目～54行目をコピー
                    range = oWSheet.Range[oWSheet.Rows[row + 0], oWSheet.Rows[row + 53]];
                    range.RowHeight = rh1;
                    // 調整用の行高さを各カードの余白行に設定、雛形シートの55行目をコピー
                    double rh21 = oWSheet.Rows[21].RowHeight;
                    oWSheet.Rows[row + 10].RowHeight = rh11;
                    oWSheet.Rows[row + 21].RowHeight = rh11;
                    oWSheet.Rows[row + 32].RowHeight = rh11;
                    oWSheet.Rows[row + 43].RowHeight = rh11;
                    oWSheet.Rows[row + 54].RowHeight = oWSheet.Rows[55].RowHeight;
                }
            }
            // バー材使用本数を計算
            string mateStr = string.Empty;
            if (r["LENGTH"].ToString() != "")
            {
                try
                {
                    // 個別の本数は必要ないでしょう
                    //double len = Convert.ToDouble(r["LENGTH"].ToString());
                    //double thickness = Convert.ToDouble(r["CUTTHICKNESS"].ToString());
                    //int qty = Convert.ToInt32(r["ODRQTY"].ToString());
                    //ans =((len + thickness) * qty) / material;
                    DataRow[] m = materialDt.Select($"MATESIZE='{r["MATESIZE"].ToString()}'");
                    double necessarylen = Convert.ToDouble(m[0]["NECESSARYLEN"].ToString());
                    mateStr = $"{necessarylen.ToString("F1")} mm";
                    int material = Convert.ToInt32(r["MATERIALLEN"].ToString()); // 材料長さがコード票に記載されていない
                    double ans = necessarylen / material;
                    mateStr += $"\n({ans.ToString("F1")} 本)"; // 小数点以下1桁
                }
                catch
                {
                    mateStr += "\n(計算ｴﾗｰ)";    // 型変換エラーは頻発するでしょう
                }
            }
            // 値を設定
            obj[1, 2] = r["HMCD"].ToString();
            obj[3, 2] = r["HMNM"].ToString();
            obj[5, 2] = DateTime.Parse(r["EDDT"].ToString()).ToString("M/d") + "(" + r["ODRQTY"].ToString() + ")";
            obj[5, 8] = r["MATESIZE"].ToString();
            obj[7, 8] = r["LENGTH"].ToString();
            obj[9, 8] = mateStr;
            var tmp = "";
            tmp += RemoveDuplicates(r["KT1MCGCD"].ToString(), r["KT1MCCD"].ToString(), 1);
            tmp += RemoveDuplicates(r["KT2MCGCD"].ToString(), r["KT2MCCD"].ToString(), 2);
            tmp += RemoveDuplicates(r["KT3MCGCD"].ToString(), r["KT3MCCD"].ToString(), 3);
            tmp += RemoveDuplicates(r["KT4MCGCD"].ToString(), r["KT4MCCD"].ToString(), 4);
            tmp += RemoveDuplicates(r["KT5MCGCD"].ToString(), r["KT5MCCD"].ToString(), 5);
            obj[7, 2] = tmp;
            obj[9, 2] = r["NOTE"].ToString();

            // 設定したオブジェクトをレンジに貼り付け
            oRange = oWSheet.Range[oWSheet.Cells[row, col], oWSheet.Cells[row + rowoff - 1, col + coloff - 1]];
            oRange.Value = obj;

            // スマート棚コン用QRコード画像ファイルの作成と保存
            string tempFile1 = @Path.GetTempPath() + Common.QR_HMCD_IMG;
            using (QRCodeData qrCodeData = oQRGenerator.CreateQrCode(
                $"{r["HMCD"].ToString()}", QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeImage = qrCode.GetGraphic(20);
                using (MemoryStream ms = new MemoryStream(qrCodeImage))
                {
                    // QRコードを作成しビットマップにした後ファイルに保存
                    using (Bitmap qrBmp = new Bitmap(ms))
                    {
                        qrBmp.Save(tempFile1);
                    }
                }
            }
            // 画像が保存できたか確認 
            if (!System.IO.File.Exists(tempFile1)) Thread.Sleep(100);
            // テスト用のQRコード画像
            //var fn = @"\\kmtsvr\共有SVEM02\Koken\切削生産計画システム\雛形\QR.bmp";// こちらサンプル画像
            // スマート棚コン用QRコードをExcelに作成
            Excel.Range rng = oWSheet.Cells[row, col + 7];
            Shape shp = oWSheet.Shapes.AddPicture2(
                tempFile1
                , Office.MsoTriState.msoFalse   // LinkToFile           図を作成元のファイルにリンクするかどうか
                , Office.MsoTriState.msoTrue    // SaveWithDocument     上記がFalseの場合、こちらをTrueにしないと落ちる（ハマった）
                , rng.Left + 8                  // Left [ Single ]
                , rng.Top + 12                  // Top [Single]
                , 42                            // Width [Single]  (旧：rng.Width - 14）
                , 44                            // Height [Single]（旧：rng.Height - 14）
                , Office.MsoPictureCompress.msoPictureCompressFalse //Compress 画像を挿入するときに圧縮するかどうか
            );
            shp.Left = (float)rng.Left + 8;
            shp.Top = (float)rng.Top + 12;
            shp.Placement = XlPlacement.xlFreeFloating; // セルに合わせて移動やサイズ変更しない（コピペした時にQRが複製されないようにしておく）

        }


        // ******************************************************************* 促進用

        public void 促進データPivot集計保存()
        {
            try
            {
                // ピボットテーブルの作成
                var pivotSheet = (Excel.Worksheet)oWBook.Worksheets.Add();
                pivotSheet.Name = "Pivot";
                var pivotTableRange = oWSheet.UsedRange;
                var pivotTableName = "PivotTable1";
                var pivotTableDestination = pivotSheet.Cells[1, 1];
                var pivotCache = oWBook.PivotCaches().Create(
                    Excel.XlPivotTableSourceType.xlDatabase,
                    pivotTableRange,
                    Excel.XlPivotTableVersionList.xlPivotTableVersion15
                );
                var pivotTable = pivotCache.CreatePivotTable(
                    pivotTableDestination,
                    pivotTableName
                );
                
                // 小計を非表示に設定
                var subtotal = new object[] {false, false, false, false, false, false, false, false, false, false, false, false};                
                
                // レポートのレイアウトを表形式に設定
                pivotTable.RowAxisLayout(Excel.XlLayoutRowType.xlTabularRow);

                // ピボットテーブルのフィールド設定
                pivotTable.PivotFields("品番").Orientation = Excel.XlPivotFieldOrientation.xlRowField;
                pivotTable.PivotFields("品番").Subtotals = subtotal;
                pivotTable.PivotFields("品目略称").Orientation = Excel.XlPivotFieldOrientation.xlRowField;
                pivotTable.PivotFields("品目略称").Subtotals = subtotal;
                pivotTable.PivotFields("完了予定日").Orientation = Excel.XlPivotFieldOrientation.xlColumnField;
                pivotTable.PivotFields("S").Orientation = Excel.XlPivotFieldOrientation.xlDataField;

                // 総計を非表示に設定
                pivotTable.RowGrand = false;        // 行ラベルの総計を非表示
                pivotTable.ColumnGrand = false;     // 列ラベルの総計を非表示

                // 集計シートを作成
                var valueSheet = (Excel.Worksheet)oWBook.Worksheets.Add();
                valueSheet.Name = "集計";
                valueSheet.Columns["A"].NumberFormat = "@"; // "@"は文字列型を意味する

                // コピーするデータを準備
                object[,] data = (System.Object[,])pivotSheet.UsedRange.Value2;

                // 書き込む範囲を指定する
                Microsoft.Office.Interop.Excel.Range OutputRange = valueSheet.Range[valueSheet.Cells[1, 1], valueSheet.Cells[data.GetLength(0), data.GetLength(1)]];

                // 指定された範囲にオブジェクト型配列の値を書き込む
                OutputRange.Value2 = data;
                OutputRange.Font.Size = 10;
                OutputRange.Font.Name = "ＭＳ ゴシック";

                // 先頭行を削除しタイトル名を調整
                valueSheet.Rows[1].Delete();
                valueSheet.Cells[1, 3].Value2 = "遅れ";
                valueSheet.Range[valueSheet.Cells[1, 4], valueSheet.Cells[1, 12]].NumberFormat = "m/d";

                // !DUMMY行を削除
                int dummyRow = getRowNo(ref valueSheet, "!DUMMY", 1);
                valueSheet.Rows[dummyRow].Delete();

                // 列幅自動調整
                valueSheet.Columns.AutoFit();

                // 保存
                oWBook.Save();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }
        public System.Object[,] getWorkSheetUsedRange(string sheetName)
        {
            var sheet = (Excel.Worksheet)oWBook.Worksheets[sheetName];
            return (System.Object[,])sheet.UsedRange.Value2;
        }

        public void 印刷用促進データ編集保存(System.Object[,] data, string filePath, ref ToolStripStatusLabel toolStripStatusLabel)
        {
            // 工程シート選択
            var ktSheet = (Excel.Worksheet)oWBook.Worksheets["工程"];
            ktSheet.Select();

            // 工程シートのNoの最終行をEndメソッドを使用して取得
            int endRow = ktSheet.Cells[5, 1].End(Excel.XlDirection.xlDown).Row;

            // 書き込む範囲を指定する（タイトル付きで入ってくるのでタイトルも直接書き込む）
            int rowCount = data.GetLength(0);
            int columnCount = data.GetLength(1);
            Microsoft.Office.Interop.Excel.Range OutputRange = ktSheet.Range[ktSheet.Cells[4, 2], ktSheet.Cells[rowCount + 4 - 1, columnCount + 2 - 1]];

            // 指定された範囲にオブジェクト型配列の値を書き込む
            OutputRange.Value2 = data;


            // 工程シートのVLOOKUP列で#N/Aが存在するかチェック（17:協力工場列で検証）
            string errHMCD = string.Empty;
            Excel.Range range = ktSheet.Range["A1", ktSheet.Cells[endRow, 37]];
            object[,] values = (object[,])range.Value2; // 2次元配列として取得
            for (int row = 5; row < endRow; row++)
            {
                if (values[row, 2] != null)//参照型オブジェクトのnull判定（NullReferenceException発生の為）
                {
                    if (values[row, 17].ToString() == "-2146826246" && values[row, 17].ToString()!="!DUMMY") // デバッグで#N/Aを調べたら"-2146826246"だった
                    {
                        errHMCD += (errHMCD == string.Empty) ?
                            values[row, 2].ToString() : "\n" + values[row, 2].ToString();
                    }
                }
            }
            if (errHMCD != string.Empty)
            {
                if (MessageBox.Show($"以下の品番がコード票に存在しません．\n処理を続行しますか？\n\n" + errHMCD
                    ,"確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    == DialogResult.No) { return; }
            }


            //cmn.Fa.ExcelDebug();


            // ①TN工程
            toolStripStatusLabel.Text = "[TN] 工程 (1/22) を作成中...";
            FilterCopyPasteSort(ref OutputRange, "TN", 36
                , 36, XlSortOrder.xlAscending
                , 2, XlSortOrder.xlAscending
                , 0, XlSortOrder.xlAscending
            );
            // ②SW工程
            toolStripStatusLabel.Text = "[SW] 工程 (2/22) を作成中...";
            FilterCopyPasteSort(ref OutputRange, "SW", 20
                , 20, XlSortOrder.xlAscending
                , 15, XlSortOrder.xlDescending
                , 2, XlSortOrder.xlAscending
            );
            // ③SS工程
            toolStripStatusLabel.Text = "[SS] 工程 (3/22) を作成中...";
            FilterCopyPasteSort(ref OutputRange, "SS", 21
                , 21, XlSortOrder.xlDescending
                , 2, XlSortOrder.xlAscending
                , 0, XlSortOrder.xlAscending
            );
            // ④CN工程
            toolStripStatusLabel.Text = "[CN] 工程 (4/22) を作成中...";
            FilterCopyPasteSort(ref OutputRange, "CN", 23
                , 23, XlSortOrder.xlAscending
                , 15, XlSortOrder.xlAscending
                , 2, XlSortOrder.xlAscending
            );
            // ⑤MS工程
            toolStripStatusLabel.Text = "[MS] 工程 (5/22) を作成中...";
            FilterCopyPasteSort(ref OutputRange, "MS", 24
                , 24, XlSortOrder.xlAscending
                , 15, XlSortOrder.xlAscending
                , 2, XlSortOrder.xlAscending
            );
            // ⑥XT工程
            toolStripStatusLabel.Text = "[XT] 工程 (6/22) を作成中...";
            FilterCopyPasteSort(ref OutputRange, "XT", 22
                , 22, XlSortOrder.xlDescending
                , 2, XlSortOrder.xlAscending
                , 0, XlSortOrder.xlAscending
            );
            // ⑦ON工程
            toolStripStatusLabel.Text = "[ON] 工程 (7/22) を作成中...";
            FilterCopyPasteSort(ref OutputRange, "ON", 27
                , 27, XlSortOrder.xlDescending
                , 2, XlSortOrder.xlAscending
                , 0, XlSortOrder.xlAscending
            );
            // ⑧MD工程
            toolStripStatusLabel.Text = "[MD] 工程 (8/22) を作成中...";
            FilterCopyPasteSort(ref OutputRange, "MD", 30
                , 29, XlSortOrder.xlDescending
                , 30, XlSortOrder.xlDescending
                , 2, XlSortOrder.xlAscending
            );
            // ⑨MC工程
            toolStripStatusLabel.Text = "[MC] 工程 (9/22) を作成中...";
            FilterCopyPasteSort(ref OutputRange, "MC", 31
                , 31, XlSortOrder.xlAscending
                , 2, XlSortOrder.xlAscending
                , 0, XlSortOrder.xlAscending
            );
            // ⑩3BP工程
            toolStripStatusLabel.Text = "[3BP] 工程 (10/22) を作成中...";
            FilterCopyPasteSort(ref OutputRange, "3BP", 28
                , 28, XlSortOrder.xlAscending
                , 2, XlSortOrder.xlAscending
                , 0, XlSortOrder.xlAscending
            );
            // ⑪ G工程（G32列とTP33列）
            toolStripStatusLabel.Text = "[G] 工程 (11/22) を作成中...";
            FilterCopyPasteSort(ref OutputRange, "G", 32
                , 32, XlSortOrder.xlDescending
                , 33, XlSortOrder.xlDescending
                , 2, XlSortOrder.xlAscending
            );
            // ⑫SK工程
            toolStripStatusLabel.Text = "[SK] 工程 (12/22) を作成中...";
            FilterCopyPasteSort(ref OutputRange, "SK", 34
                , 34, XlSortOrder.xlAscending
                , 2, XlSortOrder.xlAscending
                , 0, XlSortOrder.xlAscending
            );
            // ⑬LF工程
            toolStripStatusLabel.Text = "[LF] 工程 (13/22) を作成中...";
            FilterCopyPasteSort(ref OutputRange, "LF", 35
                , 35, XlSortOrder.xlDescending
                , 2, XlSortOrder.xlAscending
                , 0, XlSortOrder.xlAscending
            );
            // ⑭ TP工程（G32列とTP33列）（LFの後にTPをくっつけるので実行する場所に注意！）
            toolStripStatusLabel.Text = "[TP] 工程 (14/22) を作成中...";
            FilterCopyPasteSort(ref OutputRange, "TP", 33
                , 35, XlSortOrder.xlDescending
                , 33, XlSortOrder.xlDescending
                , 2, XlSortOrder.xlAscending
            );
            // ⑮NC工程
            toolStripStatusLabel.Text = "[NC] 工程 (15/22) を作成中...";
            FilterCopyPasteSort2(ref OutputRange, ref toolStripStatusLabel);
            // ⑯TN(2)シートの作成
            toolStripStatusLabel.Text = "[TN(2)] 工程 (16/22) を作成中...";
            FilterCopyPaste3("TN(2)", 36
                , new string[] {"TN1", "TN3", "TN4", "TN" }
                , new string[] { "1", "3", "4", "TN" }
                , ref toolStripStatusLabel);
            // ⑰SS(2)シートの作成
            toolStripStatusLabel.Text = "[SS(2)] 工程 (17/22) を作成中...";
            FilterCopyPaste3("SS(2)", 21
                , new string[] { "SS" }
                , new string[] { "SS" }
                , ref toolStripStatusLabel);
            // ⑱CN(2)シートの作成
            toolStripStatusLabel.Text = "[CN(2)] 工程 (18/22) を作成中...";
            FilterCopyPaste3("CN(2)", 23
                , new string[] { "CN1", "CN2", "CN3", "CN4" }
                , new string[] { "CN1", "CN2", "CN3", "CN4" }
                , ref toolStripStatusLabel);
            // ⑲MS(2)シートの作成
            toolStripStatusLabel.Text = "[MS(2)] 工程 (19/22) を作成中...";
            FilterCopyPaste3("MS(2)", 24
                , new string[] { "MS1", "MS2", "MS3", "MS4", "MS5", "MS6" }
                , new string[] { "1", "2", "3", "4", "5", "6" }
                , ref toolStripStatusLabel);
            // ⑳XT(2)シートの作成
            toolStripStatusLabel.Text = "[XT(2)] 工程 (20/22) を作成中...";
            FilterCopyPaste3("XT(2)", 22
                , new string[] { "XT" }
                , new string[] { "XT" }
                , ref toolStripStatusLabel);
            // ㉑NC(2)シートの作成
            toolStripStatusLabel.Text = "[NC(2)] 工程 (21/22) を作成中...";
            DirectCopyPaste4(ref toolStripStatusLabel);
            //cmn.Fa.ExcelDebug();
            // ㉒SK(2)シートの作成
            toolStripStatusLabel.Text = "[SK(2)] 工程 (22/22) を作成中...";
            FilterCopyPaste3("SK(2)", 34
                , new string[] { "SK", "XW" }
                , new string[] { "SK*", "XW" }
                , ref toolStripStatusLabel);



            // 工程シートのフィルタを解除
            ktSheet.AutoFilterMode = false;

            // 工程シートの余分な行を非表示にする
            ktSheet.Rows[$"{rowCount + 4}:{endRow}"].Hidden = true;

            // 最終体裁
            ktSheet.Activate();
            ktSheet.Cells[1, 1].Select();

            // 保存
            //oXls.DisplayAlerts = false; // アラートメッセージ非表示設定
            oWBook.SaveAs(filePath);

            // COMオブジェクトの解放
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ktSheet);
        }

        /// <summary>
        /// 工程シートをフィルターし対象シートへ貼り付けた後ソートを行い最後に罫線を引く関数
        /// </summary>
        /// <param name="OutputRange">入力データのRange</param>
        /// <param name="sheetName">対象のシート名</param>
        /// <param name="filCol">フィルターをかける列番号</param>
        /// <param name="filCol">フィルターをかける列番号</param>
        /// <param name="keyCol1">並び替え列番号</param>
        /// <param name="order1">並び替え列番号</param>
        private void FilterCopyPasteSort(ref Microsoft.Office.Interop.Excel.Range OutputRange, string sheetName, int filCol
            , int keyCol1, Excel.XlSortOrder order1
            , int keyCol2, Excel.XlSortOrder order2
            , int keyCol3, Excel.XlSortOrder order3)
        {
            int headerRow = 4;
            Excel.Worksheet sheet;
            if (sheetName == "TP")
            {
                sheet = (Excel.Worksheet)oWBook.Worksheets["LF"]; // TP工程はLF工程の下に張り付ける
            }
            else
            {
                sheet = (Excel.Worksheet)oWBook.Worksheets[sheetName];
            }
            if (sheetName == "TP") headerRow = sheet.Cells[headerRow, 2].End(Excel.XlDirection.xlDown).Row + 1; // TP工程はG工程の下に張り付ける
            sheet.Activate();

            // フィルタ
            //if (sheetName == "MC") 2025.06.17 ON3列を廃止し3BP列に用途変更した為処理廃止
            //{
            //    OutputRange.AutoFilter(Field: filCol
            //        , Criteria1: "<>"
            //        , Operator: XlAutoFilterOperator.xlAnd
            //        , Criteria2: "<>3B?"
            //    );
            //}
            //else if (sheetName == "3BP")
            //{
            //    OutputRange.AutoFilter(Field: filCol
            //        , Criteria1: new string[] { "3B?", "4N" }
            //        , Operator: Excel.XlAutoFilterOperator.xlFilterValues
            //    );
            //}
            //else
            //{
                OutputRange.AutoFilter(filCol, Criteria1: "<>");                    // １．col列の空白以外をフィルタ
            //}
            int filteredCount = getFilteredRowCount(ref OutputRange);
            if (filteredCount > 0)
            {
                // コピペ
                OutputRange.SpecialCells(XlCellType.xlCellTypeVisible).Copy();      // ２．フィルタされた範囲をコピー
                sheet.Cells[headerRow, 2].PasteSpecial(XlPasteType.xlPasteValues);  // ３．シートに値のみ貼り付け
                int endNoRow = sheet.Cells[headerRow + 1, 1].End(Excel.XlDirection.xlDown).Row;
                // LF工程の下に張り付けたTP工程ヘッダーを削除し行を調整
                if (sheetName == "TP")
                {
                    sheet.Range[sheet.Cells[headerRow, 2], sheet.Cells[headerRow, 37 + 3]]
                        .Delete(Excel.XlDeleteShiftDirection.xlShiftUp);            // ヘッダーは削除
                    sheet.Range[sheet.Cells[endNoRow - 1, 2], sheet.Cells[endNoRow - 1, 37 + 3]]
                        .Insert(XlInsertShiftDirection.xlShiftDown);                // 削除した分空行を挿入
                    headerRow = 4;                                                  // ヘッダー行番号を元に戻す
                }
                // ソート
                int endRow = sheet.Cells[headerRow, 2].End(Excel.XlDirection.xlDown).Row;
                if (endRow < 65535)
                {
                    sheet.Activate();
                    Excel.Range sortRange = sheet.Range[sheet.Cells[headerRow, 2], sheet.Cells[endRow, 37 + 3]]; // 並び替える対象列は少し多めに取っておく
                    var sort = sheet.Sort;                                          // ４．ソートの設定
                    sort.SortFields.Clear();
                    if (keyCol1 > 0) sort.SortFields.Add(Key: sheet.Cells[headerRow, keyCol1], Order: order1);
                    if (keyCol2 > 0) sort.SortFields.Add(Key: sheet.Cells[headerRow, keyCol2], Order: order2);
                    if (keyCol3 > 0) sort.SortFields.Add(Key: sheet.Cells[headerRow, keyCol3], Order: order3);
                    //sheet.Cells[4, keyCol1].Interior.Color = Color.FromArgb(135, 231, 173); // Debug用に色を付ける
                    //sheet.Cells[4, keyCol2].Interior.Color = Color.FromArgb(231, 135, 173); // Debug用に色を付ける
                    sort.SetRange(sortRange);
                    sort.Header = Excel.XlYesNoGuess.xlYes; // ヘッダー行あり
                    sort.MatchCase = false;
                    sort.Orientation = Excel.XlSortOrientation.xlSortColumns;
                    sort.Apply();
                    // 並び替え条件（MCを一番上にする）
                    if (sheetName == "MC")
                    {
                        int currentRow = headerRow + 1; //挿入開始行番号
                        for (int i = headerRow + 1; i <= endRow; i++)
                        {
                            Range cell = sheet.Cells[i, filCol];
                            if (cell.Value != null && cell.Value.ToString().Contains("MC"))
                            {
                                // 行を先頭に移動
                                sheet.Range[sheet.Cells[i, 2], sheet.Cells[i, 37 + 3]].Cut();
                                sheet.Range[sheet.Cells[currentRow, 2], sheet.Cells[currentRow, 37 + 3]]
                                    .Insert(XlInsertShiftDirection.xlShiftDown);
                                currentRow++;
                            }
                        }
                    }
                    // 罫線
                    for (int i = headerRow + 1; i < endRow; i++)                    // ５．中太罫線を入れる
                    {
                        var currentValue = sheet.Cells[i, filCol].Value2;
                        var nextValue = sheet.Cells[i + 1, filCol].Value2;
                        try // 文字列と数値型の比較で異常が発生してしまうので罫線は無視する
                        {
                            if (currentValue != nextValue) 
                            {
                                sheet.Range[sheet.Cells[i, 2], sheet.Cells[i, filCol]]
                                    .Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlMedium; // 太罫線xlThick;
                            }
                        }
                        catch { continue; }
                    }
                    // LF工程の下にTP工程が来るので行削除しない
                    if (sheetName != "LF")
                    {
                        sheet.Rows[$"{endRow + 1}:{endNoRow}"].Delete();            // ６．余分な行を削除する
                    }
                    sheet.Cells[1, 1].Select();
                }
            }
            else if (sheetName == "TP") 
            {
                // TPフィルター後件数が0件の時
                headerRow = 4;                                                      // ヘッダー行番号を元に戻す
                int endNoRow = sheet.Cells[headerRow + 1, 1].End(Excel.XlDirection.xlDown).Row;
                int endRow = sheet.Cells[headerRow, 2].End(Excel.XlDirection.xlDown).Row;
                sheet.Rows[$"{endRow + 1}:{endNoRow}"].Delete();                    // 余分な行を削除する
            }
            else
            {
                sheet.Cells[5, 2].Value2 = "対象データなし";
            }
            OutputRange.AutoFilter(filCol); // フィルターの解除ではなく抽出条件をクリア

            // COMオブジェクトの解放
            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
        }

        /// <summary>
        /// NC工程シートの作成（専用）
        /// </summary>
        /// <param name="OutputRange">入力データのRange</param>
        private void FilterCopyPasteSort2(ref Microsoft.Office.Interop.Excel.Range OutputRange
            , ref ToolStripStatusLabel toolStripStatusLabel)
        {
            int headerRow = 4;
            int startRow = 0;
            int endRow = 0;
            int rowCount = 0;
            Excel.Worksheet sheet;
            sheet = (Excel.Worksheet)oWBook.Worksheets["NC"];
            sheet.Activate();

            // ON工程を処理
            toolStripStatusLabel.Text = "[ON] 工程 (14/14) を作成中...";
            Excel.Worksheet sheetON;
            sheetON = (Excel.Worksheet)oWBook.Worksheets["ON"];
            endRow = sheetON.Cells[headerRow, 2].End(Excel.XlDirection.xlDown).Row;
            rowCount = endRow - headerRow;
            if (endRow < 65535)
            {
                // 設備名の行番号を取得
                startRow = getRowNo(ref sheet,"ON", 2);

                // 貼り付ける枠を確保するための行挿入
                // 上手く行っているので保存しておく
                //for (int i = 1; i <= rowCount - 2; i++)
                //{
                //    sheet.Rows[startRow + 3].Insert(XlInsertShiftDirection.xlShiftDown);
                //}
                // 貼り付ける枠を確保するための行挿入
                if (rowCount > 2)
                {
                    sheet.Rows[$"{startRow + 3}:{startRow + rowCount}"].Insert(XlInsertShiftDirection.xlShiftDown);
                    // 空き行に式をコピー
                    sheet.Range[sheet.Cells[startRow + 2, 14], sheet.Cells[startRow + 2, 37]].Copy();
                    sheet.Range[sheet.Cells[startRow + 3, 14], sheet.Cells[startRow + rowCount + 1, 37]]
                        .PasteSpecial(XlPasteType.xlPasteFormulas);
                }
                // ONシートから値のみコピー
                sheetON.Range[sheetON.Cells[headerRow, 2], sheetON.Cells[headerRow + rowCount, 13]].Copy();
                sheet.Cells[headerRow, 2].PasteSpecial(XlPasteType.xlPasteValues);
                if (rowCount == 1) sheet.Rows[startRow + 3].Delete();
                // 行番号の振り直し
                for (int i = 1; i <= rowCount; i++) sheet.Cells[startRow + 1 + i, 1].Value2 = i;
            }

            // NC1～NC8工程まで処理をループ
            for (int idx = 1; idx <= 9; idx++)
            {
                string target = (idx == 9) ? "" : idx.ToString();
                toolStripStatusLabel.Text = $"[NC{target}] 工程 (14/21) を作成中...";
                startRow = getRowNo(ref sheet, $"NC{target}", 2);
                OutputRange.AutoFilter(26, Criteria1: (idx == 9) ? "NC" : idx.ToString());
                rowCount = getFilteredRowCount(ref OutputRange);
                endRow = startRow + rowCount;
                if (rowCount > 2)
                {
                    //for (int i = 1; i <= rowCount - 2; i++) sheet.Rows[startRow + 3].Insert(XlInsertShiftDirection.xlShiftDown);
                    sheet.Rows[$"{startRow + 3}:{startRow + rowCount}"].Insert(XlInsertShiftDirection.xlShiftDown);
                    sheet.Range[sheet.Cells[startRow + 2, 14], sheet.Cells[startRow + 2, 37]].Copy();
                    sheet.Range[sheet.Cells[startRow + 3, 14], sheet.Cells[startRow + rowCount + 1, 37]].PasteSpecial(XlPasteType.xlPasteFormulas);
                }
                OutputRange.SpecialCells(XlCellType.xlCellTypeVisible).Copy();
                sheet.Cells[startRow + 1, 2].PasteSpecial(XlPasteType.xlPasteValues);
                if (rowCount == 1) sheet.Rows[startRow + 3].Delete();
                for (int i = 1; i <= rowCount; i++) sheet.Cells[startRow + 1 + i, 1].Value2 = i;
                OutputRange.AutoFilter(26);     // フィルターの解除ではなく抽出条件をクリア
            }
            // その他
            sheet.AutoFilterMode = false;       // フィルター自体を解除
            sheet.Activate();
            sheet.Cells[1, 1].Select();

            // COMオブジェクトの解放
            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheetON);
        }

        /// <summary>
        /// 各工程(2)シートの作成（汎用）
        /// </summary>
        /// <param name="OutputRange">入力データのRange</param>
        private void FilterCopyPaste3(string sheetName, int filCol
            , string[] ktNames
            , string[] filNames
            , ref ToolStripStatusLabel toolStripStatusLabel)
        {
            int headerRow = 4;
            int refEndRow = 0;      // 参照先フィルタ前の終了行（データ存在チェックとレンジ設定で使用）
            int refStartRow = 0;    // 参照先フィルタ後の開始行
            int startRow = 0;       // 貼り付け先の開始行
            int rowCount = 0;       // フィルター後の対象件数
            Excel.Worksheet refSheet;
            refSheet = (Excel.Worksheet)oWBook.Worksheets[sheetName.Replace("(2)","")];
            refEndRow = refSheet.Cells[headerRow, 2].End(Excel.XlDirection.xlDown).Row;
            int refEndNoRow = refSheet.Cells[headerRow + 1, 1].End(Excel.XlDirection.xlDown).Row;
            if (refEndRow > 65535) return;
            Excel.Range refRange = refSheet.Range[refSheet.Cells[headerRow, 1], refSheet.Cells[refEndRow, 37 + 3]];
            Excel.Worksheet destSheet;
            destSheet = (Excel.Worksheet)oWBook.Worksheets[sheetName];

            // 設備対象で処理をループ
            for (int idx = 0; idx < ktNames.Length; idx++)
            {
                if (filNames[idx] == "CN1")
                {
                    refRange.AutoFilter(Field: filCol
                        , Criteria1: new string[] { "CN", "CN1" }
                        , Operator: Excel.XlAutoFilterOperator.xlFilterValues
                    );
                }
                else if (filNames[idx] == "SS")
                {
                    refRange.AutoFilter(Field: filCol
                        , Criteria1: new string[] { "SS", "CT" }
                        , Operator: Excel.XlAutoFilterOperator.xlFilterValues
                    );
                }
                else
                {
                    refRange.AutoFilter(filCol, Criteria1: filNames[idx]);
                }
                refStartRow = getFilteredFirstRow(ref refRange);
                rowCount = getFilteredRowCount(ref refRange);
                startRow = getRowNo(ref destSheet, $"{ktNames[idx]}", 2);
                if (rowCount > 2)
                {
                    // for (int i = 1; i <= rowCount - 2; i++) destSheet.Rows[startRow + 3].Insert(XlInsertShiftDirection.xlShiftDown);
                    destSheet.Rows[$"{startRow + 3}:{startRow + rowCount}"].Insert(XlInsertShiftDirection.xlShiftDown);
                    destSheet.Range[destSheet.Cells[startRow + 2, 9], destSheet.Cells[startRow + 2, 10]].Copy();
                    destSheet.Range[destSheet.Cells[startRow + 3, 9], destSheet.Cells[startRow + rowCount + 1, 10]].PasteSpecial(XlPasteType.xlPasteFormulas);
                    destSheet.Range[destSheet.Cells[startRow + 2, 16], destSheet.Cells[startRow + 2, 23]].Copy();
                    destSheet.Range[destSheet.Cells[startRow + 3, 16], destSheet.Cells[startRow + rowCount + 1, 23]].PasteSpecial(XlPasteType.xlPasteFormulas);
                }
                // 前半週のヘッダーのコピペ
                refSheet.Range[refSheet.Cells[headerRow, 2], refSheet.Cells[headerRow, 8]]
                    .SpecialCells(XlCellType.xlCellTypeVisible).Copy();
                destSheet.Cells[startRow + 1, 2].PasteSpecial(XlPasteType.xlPasteValues);
                // 前半週のフィルタデータのコピペ
                refSheet.Range[refSheet.Cells[refStartRow, 2],refSheet.Cells[refStartRow + rowCount, 8]]
                    .SpecialCells(XlCellType.xlCellTypeVisible).Copy();
                destSheet.Cells[startRow + 2, 2].PasteSpecial(XlPasteType.xlPasteValues);
                // 後半週のヘッダーのコピペ
                refSheet.Range[refSheet.Cells[headerRow, 9], refSheet.Cells[headerRow, 13]]
                    .SpecialCells(XlCellType.xlCellTypeVisible).Copy();
                destSheet.Cells[startRow + 1, 11].PasteSpecial(XlPasteType.xlPasteValues);
                // 後半週のフィルタデータのコピペ
                refSheet.Range[refSheet.Cells[refStartRow, 9], refSheet.Cells[refStartRow + rowCount, 13]]
                    .SpecialCells(XlCellType.xlCellTypeVisible).Copy();
                destSheet.Cells[startRow + 2, 11].PasteSpecial(XlPasteType.xlPasteValues);
                // 1件の場合は空白行を削除
                if (rowCount == 1) destSheet.Rows[startRow + 3].Delete();
                // No振り直し
                for (int i = 1; i <= rowCount; i++) destSheet.Cells[startRow + 1 + i, 1].Value2 = i;
                // フィルターの解除ではなく抽出条件をクリア
                refRange.AutoFilter(filCol);
            }
            // その他
            refSheet.AutoFilterMode = false;    // フィルター自体を解除
            destSheet.Activate();
            destSheet.Cells[1, 1].Select();

            // COMオブジェクトの解放
            System.Runtime.InteropServices.Marshal.ReleaseComObject(refRange);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(refSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(destSheet);
        }

        /// <summary>
        /// NCシートのNC4からのデータをNC(2)シートに複写（専用）
        /// </summary>
        /// <param name="OutputRange">入力データのRange</param>
        private void DirectCopyPaste4(ref ToolStripStatusLabel toolStripStatusLabel)
        {
            Excel.Worksheet refSheet;
            refSheet = (Excel.Worksheet)oWBook.Worksheets["NC"];
            Excel.Worksheet destSheet;
            destSheet = (Excel.Worksheet)oWBook.Worksheets["NC(2)"];
            // 一旦全部コピペ
            refSheet.Range[refSheet.Cells[1, 1], refSheet.Cells[150, 18]].Copy(); // 150行くらいコピペしとけば大丈夫じゃね
            destSheet.Cells[1, 1].PasteSpecial(Excel.XlPasteType.xlPasteAll);
            // ON～NC3までを削除
            int refStartRow = getRowNo(ref refSheet, "NC4", 2);
            destSheet.Rows[$"3:{refStartRow - 1}"].Delete();
            destSheet.Activate();
            destSheet.Cells[1, 1].Select();
            // コピペ後の行高さ調整
            int row = 0;
            int nextrow = 0;
            string[] mccds = {"NC4", "NC5", "NC6", "NC7", "NC8", "NC" };
            for (int idx = 0; idx < mccds.Length; idx++)
            {
                row = getRowNo(ref destSheet, mccds[idx], 2); // タイトル12.0、見出し9.6、データ15.6
                destSheet.Rows[row + 0].RowHeight = 12.0d;
                destSheet.Rows[row + 1].RowHeight = 9.6d;
                if (idx == mccds.Length - 1)
                {
                    nextrow = destSheet.Cells[row, 2].End(Excel.XlDirection.xlDown).Row + 1;
                }
                else
                {
                    nextrow = getRowNo(ref destSheet, mccds[idx + 1], 2);
                }
                destSheet.Rows[$"{row + 2}:{nextrow - 1}"].RowHeight = 15.6d;
            }
            destSheet.Rows[$"{nextrow}:{nextrow + 2}"].RowHeight = 12.0d;   // 本数サマリー
            destSheet.Rows[$"{nextrow + 3}"].RowHeight = 15.6d;             // 工数サマリー
            destSheet.Rows[$"{nextrow + 4}"].RowHeight = 12.0d;             // おまけ解説

            // COMオブジェクトの解放
            System.Runtime.InteropServices.Marshal.ReleaseComObject(refSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(destSheet);
        }

        // フィルタ後のデータの先頭行番号を取得
        private int getFilteredFirstRow(ref Microsoft.Office.Interop.Excel.Range OutputRange)
        {
            // フィルター後の可視セルを取得
            var visibleCells = OutputRange.SpecialCells(Excel.XlCellType.xlCellTypeVisible);

            // 可視セルを調査
            foreach (Excel.Range cell in visibleCells.Rows)
            {
                // ヘッダー以外のデータの開始行を取得
                if (((object[,])cell.Value2)[1, 2].ToString() != "品番") return cell.Row;
            }
            return 5; // 不明の場合は初期値を返却
        }

        // フィルタ後の件数を取得
        private int getFilteredRowCount(ref Microsoft.Office.Interop.Excel.Range OutputRange)
        {
            // フィルター後の可視セルを取得
            var visibleCells = OutputRange.SpecialCells(Excel.XlCellType.xlCellTypeVisible);

            // 可視セルの行数をカウント
            int visibleRowCount = 0;
            foreach (Excel.Range cell in visibleCells.Rows)
            {
                // Debug用 Console.WriteLine(((object[,])cell.Value2)[1, 1].ToString());
                visibleRowCount++;
            }
            // 行ヘッダー分を引いて返却
            return visibleRowCount - 1;
        }

        // 文字列を検索し行番号を返却
        private int getRowNo(ref Excel.Worksheet sheet, string findstring, int column)
        {
            Excel.Range foundCell = sheet.Columns[column].Find(
                What: findstring.Replace("'",""),
                LookIn: Excel.XlFindLookIn.xlValues,
                LookAt: Excel.XlLookAt.xlWhole,     // 完全一致
                SearchOrder: Excel.XlSearchOrder.xlByRows,
                MatchCase: true                     // 大文字小文字を区別
            );
            if (foundCell == null)
            {
                throw new Exception($"文字列[{findstring}]が列{column}で見つかりませんでした.");
            }
            return foundCell.Row;
        }

        // Excelブックが開いているかどうか判定
        public bool IsWorkbookOpen(string workbookName)
        {
            Excel.Application excelApp = null;
            try
            {
                excelApp = (Excel.Application)Marshal.GetActiveObject("Excel.Application");
                foreach (Excel.Workbook workbook in excelApp.Workbooks)
                {
                    if (workbook.Name.Equals(workbookName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            catch (COMException)
            {
                // Excelが起動していない場合
            }
            finally
            {
                if (excelApp != null)
                {
                    Marshal.ReleaseComObject(excelApp);
                }
            }
            return false;
        }

        /// <summary>
        /// コード票シートのA6アドレスに最新データを貼り付ける
        /// </summary>
        /// <param name="dataTable">コード票マスタデータテーブル</param>
        /// <param name="copyColumns">コピーする最終列</param>
        /// <returns></returns>
        public void WriteDataTableToExcel(System.Data.DataTable codeSlipDt, int copyColumns)
        {
            // コード票マスタオブジェクト
            object[,] codeSlipObj = new object[codeSlipDt.Rows.Count, copyColumns];
            try
            {
                int rowCount = 0;
                foreach (DataRow r in codeSlipDt.Rows)
                {
                    for (int colCount = 0; colCount < copyColumns; colCount++)
                    {
                        codeSlipObj[rowCount, colCount] = Convert.ToString(r[colCount]);
                    }
                    rowCount++;
                }
                // 設定したオブジェクトをレンジに貼り付け
                oRange = oWSheet.Range[oWSheet.Cells[6, 1], oWSheet.Cells[6 + rowCount - 1, 1 + copyColumns - 1]];
                oRange.Value = codeSlipObj;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
        }



    }
}
