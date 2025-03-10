﻿using LumenWorks.Framework.IO.Csv;
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
using System.Drawing.Drawing2D;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms.VisualStyles;
using static System.Net.WebRequestMethods;
using System.Diagnostics.Contracts;
using Microsoft.Win32;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Drawing;
using System.Threading;

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
        public Excel.Application oXls;     // Excel オブジェクト
        public Excel.Workbook oWBook;      // Workbook オブジェクト
        public Excel.Worksheet oWSheet;    // Worksheet オブジェクト

        // QRCoder
        public QRCodeGenerator oQRGenerator;

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
            oXls.Visible = cmn.FsCd[0].VisibleExcel;  // Excelのウィンドウの表示/非表示を設定ファイルから取得

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
        /// <param name="dtbl">データ テーブル</param>
        /// <param name="path">パス</param>
        /// <param name="filenm">ファイル名称</param>
        public void ExportExcel(System.Data.DataTable dtbl, string path, string filenm)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            try
            {
                oXls.DisplayAlerts = false;

                oWSheet = oWBook.Sheets[1];
                oWSheet.Select(Type.Missing);

                for (int col = 0; col < dtbl.Columns.Count; col++)
                {
                    object[,] obj = new object[dtbl.Rows.Count + 1, 1];
                    obj[0, 0] = dtbl.Columns[col].ColumnName;
                    for (int row = 0; row < dtbl.Rows.Count; row++)
                    {
                        obj[row + 1, 0] = dtbl.Rows[row][col].ToString();
                    }

                    var rgn = oWSheet.Range[oWSheet.Cells[1, col + 1], oWSheet.Cells[dtbl.Rows.Count + 1, col + 1]];
                    rgn.Font.Size = 10;
                    rgn.Font.Name = "ＭＳ Ｐゴシック";
                    DataColumn dtcol = dtbl.Columns[col];
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
                string fullPath = Path.Combine(path, filenm);
                oWBook.SaveAs(fullPath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                oXls.Quit();
            }
        }

        // https://mitosuya.net/excel-high-performance
        public void ReadExcelToDatatble2() 
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            dynamic xlApp = null;
            dynamic xlWbooks = null;
            dynamic xlWbook = null;
            dynamic xlSheets = null;
            dynamic xlSheet = null;
            dynamic xlRange = null;
            dynamic xlCell = null;
            try
            {
                Type objectClassType = Type.GetTypeFromProgID("Excel.Application");
                xlApp = Activator.CreateInstance(objectClassType);
                xlApp.ScreenUpdating = false; // screenUpdating;
                xlApp.EnableEvents = false; // enableEvents;

                xlWbooks = xlApp.Workbooks;
                xlWbook = xlWbooks.Open(@"C:\Users\watuw\Desktop\遅延(EM-Y版).xlsm");
                xlApp.Calculation = false; // calculation;

                xlSheets = xlWbook.Worksheets;
                xlSheet = xlSheets.Item("コード票");
                var isBulk =true;
                if (isBulk) //一括の場合
                {
                    object[,] values = new object[100, 100];
                    //for (int iRow = 0; iRow < 100; iRow++)
                    //{
                    //    for (int iCol = 0; iCol < 100; iCol++)
                    //    {
                    //        values[iRow, iCol] = (iRow + 1).ToString() + (iCol + 1).ToString();
                    //    }
                    //}

                    try
                    {
                        xlRange = xlSheet.Range("A1:CV100");
                        values = xlRange.Value;
                        //values = xlSheet.Range("A1:CV100");
                    }
                    finally
                    {
                        Marshal.ReleaseComObject(xlRange);
                    }
                }
                else //1セルずつの場合
                {
                    for (int iRow = 1; iRow <= 100; iRow++)
                    {
                        for (int iCol = 1; iCol <= 100; iCol++)
                        {
                            try
                            {
                                xlCell = xlSheet.Cells(iRow, iCol);
                                xlCell.Value = (iRow).ToString() + (iCol).ToString();
                            }
                            finally
                            {
                                Marshal.ReleaseComObject(xlCell);
                            }
                        }
                    }
                }

                xlApp.Calculation = true; // CALCULATION_DEFAULT;
//                xlWbook.Save();
            }
            finally
            {
                if (xlWbook != null)
                {
                    //xlWbook.Saved = true;
                }
                xlApp.EnableEvents = true;
                xlApp.ScreenUpdating = true;

                //COMオブジェクトの開放
                Marshal.ReleaseComObject(xlSheet);
                Marshal.ReleaseComObject(xlSheets);
                Marshal.ReleaseComObject(xlWbook);
                Marshal.ReleaseComObject(xlWbooks);
                //Excelアプリケーション終了
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);

                //計測結果表示
                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                //txtResult.Text = $"{ts.TotalSeconds:0.00}";
            }
        }

        /// <summary>
        /// Excel コード票をデータテーブルへインポート
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

        // コピペだと遅いなぁ
        /*
* Visible = true;
[StopWatch] Header開始 16:28:50
[StopWatch] Header終了 16:28:54 (4.578秒)
[StopWatch] Header開始 16:28:55
[StopWatch] Header終了 16:28:58 (2.492秒) * ページ数
[StopWatch] 終了 16:29:30 (41.910秒)
* Visible = false;
[StopWatch] Header開始 16:45:14
[StopWatch] Header終了 16:45:16 (2.095秒)
[StopWatch] Header開始 16:45:16
[StopWatch] Header終了 16:45:16 (0.423秒) * ページ数
[StopWatch] 終了 16:45:27 (14.835秒) 50件で
         */

        // コピペじゃないパターン時間かけて作ったけど更に遅くなった^^;
        /*
* Visible = true;
[StopWatch] 開始 20:37:31
[StopWatch] 終了 20:39:27 (115.663秒)
* Visible = false;
[StopWatch] 開始 20:42:55
[StopWatch] 終了 20:43:48 (52.037秒)
                    // 書式設定
                    // 品番T
                    oWSheet.Range[oWSheet.Cells[row + 0, col - 1], oWSheet.Cells[row + 2, col - 1]].Merge(); 
                    oWSheet.Range[oWSheet.Cells[row + 0, col - 1], oWSheet.Cells[row + 2, col - 1]].Font.Size = oWSheet.Range["A1:A3"].Font.Size;
                    // 品番
                    oWSheet.Range[oWSheet.Cells[row + 0, col + 0], oWSheet.Cells[row + 2, col + 5]].Merge();
                    oWSheet.Range[oWSheet.Cells[row + 0, col + 0], oWSheet.Cells[row + 2, col + 5]].Font.Size = oWSheet.Range["B1:G3"].Font.Size;
                    oWSheet.Range[oWSheet.Cells[row + 0, col + 0], oWSheet.Cells[row + 2, col + 5]].Font.Bold = oWSheet.Range["B1:G3"].Font.Bold;
                    oWSheet.Range[oWSheet.Cells[row + 0, col + 0], oWSheet.Cells[row + 2, col + 5]].HorizontalAlignment = oWSheet.Range["B1:G3"].HorizontalAlignment;
                    // ｽﾏｰﾄﾀﾅｺﾝ用QR
                    oWSheet.Range[oWSheet.Cells[row + 0, col + 6], oWSheet.Cells[row + 2, col + 6]].Merge();
                    oWSheet.Range[oWSheet.Cells[row + 0, col + 6], oWSheet.Cells[row + 2, col + 6]].Font.Size = oWSheet.Range["H1:H3"].Font.Size;
                    oWSheet.Range[oWSheet.Cells[row + 0, col + 6], oWSheet.Cells[row + 2, col + 6]].Font.Color = oWSheet.Range["H1:H3"].Font.Color;
                    oWSheet.Range[oWSheet.Cells[row + 0, col + 6], oWSheet.Cells[row + 2, col + 6]].VerticalAlignment = oWSheet.Range["H1:H3"].VerticalAlignment;
                    // 品名
                    oWSheet.Range[oWSheet.Cells[row + 3, col + 0], oWSheet.Cells[row + 3, col + 6]].Merge();
                    oWSheet.Range[oWSheet.Cells[row + 3, col + 0], oWSheet.Cells[row + 3, col + 6]].Font.Size = oWSheet.Range["B4:H4"].Font.Size;
                    // 注文番号、その他情報
                    for (int i = 4; i <= 6; i++)
                    {
                        oWSheet.Range[oWSheet.Cells[row + i, col + 0], oWSheet.Cells[row + i, col + 2]].Merge();
                        oWSheet.Range[oWSheet.Cells[row + i, col + 3], oWSheet.Cells[row + i, col + 4]].Merge();
                        oWSheet.Range[oWSheet.Cells[row + i, col + 5], oWSheet.Cells[row + i, col + 6]].Merge();
                    }
                    oWSheet.Range[oWSheet.Cells[row + 5, col + 0], oWSheet.Cells[row + 5, col + 2]].Font.Bold = oWSheet.Range["B6:D6"].Font.Bold;
                    oWSheet.Range[oWSheet.Cells[row + 6, col + 0], oWSheet.Cells[row + 6, col + 2]].Font.Bold = oWSheet.Range["B7:D7"].Font.Bold;
                    // 設備名T
                    oWSheet.Range[oWSheet.Cells[row + 7, col + 0], oWSheet.Cells[row + 7, col + 1]].Merge();
                    oWSheet.Range[oWSheet.Cells[row + 7, col + 0], oWSheet.Cells[row + 7, col + 1]].Font.Size = oWSheet.Range["B8:C8"].Font.Size;
                    // 加工時間T
                    oWSheet.Range[oWSheet.Cells[row + 7, col + 2], oWSheet.Cells[row + 7, col + 3]].Merge();
                    oWSheet.Range[oWSheet.Cells[row + 7, col + 2], oWSheet.Cells[row + 7, col + 3]].Font.Size = oWSheet.Range["D8:E8"].Font.Size;
                    oWSheet.Range[oWSheet.Cells[row + 7, col + 2], oWSheet.Cells[row + 7, col + 3]].ShrinkToFit = oWSheet.Range["D8:E8"].ShrinkToFit;
                    // 数量T
                    oWSheet.Range[oWSheet.Cells[row + 7, col + 4], oWSheet.Cells[row + 7, col + 5]].Merge();
                    oWSheet.Range[oWSheet.Cells[row + 7, col + 4], oWSheet.Cells[row + 7, col + 5]].Font.Size = oWSheet.Range["F8:G8"].Font.Size;
                    // 納期T
                    oWSheet.Range[oWSheet.Cells[row + 7, col + 6], oWSheet.Cells[row + 7, col + 6]].Font.Size = oWSheet.Range["H8:H8"].Font.Size;
                    for (int i = 8; i <= 18; i = i + 2)
                    {
                        oWSheet.Range[oWSheet.Cells[row + i, col - 1], oWSheet.Cells[row + i + 1, col - 1]].Merge();
                        // 設備名
                        oWSheet.Range[oWSheet.Cells[row + i, col + 0], oWSheet.Cells[row + i + 1, col + 1]].Merge();
                        oWSheet.Range[oWSheet.Cells[row + i, col + 0], oWSheet.Cells[row + i + 1, col + 1]].Font.Size = oWSheet.Range["B9:C10"].Font.Size;
                        oWSheet.Range[oWSheet.Cells[row + i, col + 2], oWSheet.Cells[row + i + 1, col + 3]].Merge();
                        oWSheet.Range[oWSheet.Cells[row + i, col + 2], oWSheet.Cells[row + i + 1, col + 3]].Font.Size = oWSheet.Range["D9:E10"].Font.Size;
                        oWSheet.Range[oWSheet.Cells[row + i, col + 4], oWSheet.Cells[row + i + 1, col + 5]].Merge();
                        oWSheet.Range[oWSheet.Cells[row + i, col + 4], oWSheet.Cells[row + i + 1, col + 5]].Font.Size = oWSheet.Range["F9:G10"].Font.Size;
                        oWSheet.Range[oWSheet.Cells[row + i, col + 6], oWSheet.Cells[row + i + 1, col + 6]].Merge();
                        oWSheet.Range[oWSheet.Cells[row + i, col + 6], oWSheet.Cells[row + i + 1, col + 6]].Font.Size = oWSheet.Range["H9:H10"].Font.Size;
                    }
                    oWSheet.Range[oWSheet.Cells[row + 18, col - 1], oWSheet.Cells[row + 19, col - 1]].Merge();
                    // 備考
                    oWSheet.Range[oWSheet.Cells[row + 18, col + 0], oWSheet.Cells[row + 19, col + 5]].Merge();
                    // i-Reporter用
                    oWSheet.Range[oWSheet.Cells[row + 18, col + 6], oWSheet.Cells[row + 19, col + 6]].Merge();
                    oWSheet.Range[oWSheet.Cells[row + 18, col + 6], oWSheet.Cells[row + 19, col + 6]].Font.Size = oWSheet.Range["H19:H20"].Font.Size;
                    oWSheet.Range[oWSheet.Cells[row + 18, col + 6], oWSheet.Cells[row + 19, col + 6]].Font.Color = oWSheet.Range["H19:H20"].Font.Color;
                    oWSheet.Range[oWSheet.Cells[row + 18, col + 6], oWSheet.Cells[row + 19, col + 6]].VerticalAlignment = oWSheet.Range["H19:H20"].VerticalAlignment;

                    // 罫線
                    var rng = oWSheet.Range[oWSheet.Cells[row + 0, col - 1], oWSheet.Cells[row + 19, col + 6]];
                    rng.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                    rng.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                    rng.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    rng.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                    rng.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                    rng.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                    rng.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;

                    // タイトル
                    oWSheet.Cells[row + 0, col - 1].Value = "品番";
                    oWSheet.Cells[row + 0, col + 6].Value = "ｽﾏｰﾄﾀﾅｺﾝ用";
                    oWSheet.Cells[row + 3, col - 1].Value = "品名";
                    oWSheet.Cells[row + 4, col - 1].Value = "注文番号";
                    oWSheet.Cells[row + 4, col + 3].Value = "ｻｲｽﾞx全長";
                    oWSheet.Cells[row + 5, col - 1].Value = "注文日付";
                    oWSheet.Cells[row + 5, col + 3].Value = "収容数";
                    oWSheet.Cells[row + 6, col - 1].Value = "注文数量";
                    oWSheet.Cells[row + 6, col + 3].Value = "協力工場";
                    oWSheet.Cells[row + 7, col + 0].Value = "設備名";
                    oWSheet.Cells[row + 7, col + 2].Value = "加工時間(分)";
                    oWSheet.Cells[row + 7, col + 4].Value = "数量";
                    oWSheet.Cells[row + 7, col + 6].Value = "納期";
                    oWSheet.Cells[row + 16, col - 1].Value = "検査";
                    oWSheet.Cells[row + 18, col - 1].Value = "備考";
                    oWSheet.Cells[row + 18, col + 6].Value = "i-Reporter用";
         * */

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

        // 1カード作成（DataRow1件分を作成）
        public void SetOrderCard(ref DateTime cardDay, ref DataRow r, ref int row, ref int col)
        {
            if (row > 40) // 2頁目以降に判定を行う
            {
                if ((row - 1) % 42 == 0 && col <= 2) // 各頁の最初のカード作成前に1頁目の雛形書式をコピー
                {
                    // A1:Q42をコピペ
                    var range = oWSheet.Range[oWSheet.Cells[1, 1], oWSheet.Cells[42, 17]];
                    range.Copy(oWSheet.Cells[row, 1]);
                    // 必要ない値のみクリア
                    ClearCardByPage(ref row);
                    // 行の高さはコピペされないのでRows(1:42)を複製
                    for (int y = 1; y <= 42; y++)
                        oWSheet.Rows[row + y - 1].RowHeight = oWSheet.Rows[y].RowHeight;
                }
            }

            // 値を設定
            oWSheet.Cells[row + 0, col].Value = r["HMCD"].ToString();
            oWSheet.Cells[row + 3, col].Value = r["HMNM"].ToString();
            oWSheet.Cells[row + 4, col].Value = r["ODRNO"].ToString();
            oWSheet.Cells[row + 5, col].Value = cardDay.ToString("yyyy.MM.dd");
            oWSheet.Cells[row + 6, col].Value = r["ODRQTY"].ToString();
            oWSheet.Cells[row + 4, col + 5].Value = r["MATESIZE"].ToString() + r["LENGTH"].ToString() != "" ? " x " + r["LENGTH"] : "";
            oWSheet.Cells[row + 5, col + 5].Value = r["BOXQTY"].ToString();
            oWSheet.Cells[row + 6, col + 5].Value = r["PARTNER"].ToString();
            if (r["KT1MCGCD"].ToString() == "")
            {
                oWSheet.Cells[row + 8, col - 1].Value = "";
            }
            else 
            {
                oWSheet.Cells[row + 8, col - 1].Value = "工程①";
                oWSheet.Cells[row + 8, col].Value = r["KT1MCGCD"].ToString() + "-" + r["KT1MCCD"].ToString();
            }
            if (r["KT2MCGCD"].ToString() == "")
            {
                oWSheet.Cells[row + 10, col - 1].Value = "";
            }
            else
            {
                oWSheet.Cells[row + 10, col - 1].Value = "工程②";
                oWSheet.Cells[row + 10, col].Value = r["KT2MCGCD"].ToString() + "-" + r["KT2MCCD"].ToString();
            }
            if (r["KT3MCGCD"].ToString() == "")
            {
                oWSheet.Cells[row + 12, col - 1].Value = "";
            }
            else
            {
                oWSheet.Cells[row + 12, col - 1].Value = "工程③";
                oWSheet.Cells[row + 12, col].Value = r["KT3MCGCD"].ToString() + "-" + r["KT3MCCD"].ToString();
            }
            if (r["KT4MCGCD"].ToString() == "")
            {
                oWSheet.Cells[row + 14, col - 1].Value = "";
            }
            else
            {
                oWSheet.Cells[row + 14, col - 1].Value = "工程④";
                oWSheet.Cells[row + 14, col].Value = r["KT4MCGCD"].ToString() + "-" + r["KT4MCCD"].ToString();
            }
            if (r["KT5MCGCD"].ToString() != "")
            {
                oWSheet.Cells[row + 16, col].Value = r["KT5MCGCD"].ToString() + "-" + r["KT5MCCD"].ToString();
            }
            oWSheet.Cells[row + 18, col].Value = r["NOTE"].ToString();

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
            var fn = @"\\kmtsvr\共有SVEM02\Koken\切削生産計画システム\雛形\QR.bmp";// こちらサンプル画像

            // スマート棚コン用QRコードをExcelに作成
            Excel.Range rng = oWSheet.Cells[row, col + 6];
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
            rng = oWSheet.Cells[row + 18, col + 6];
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


    }
}
