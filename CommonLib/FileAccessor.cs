using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

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

        public FileAccessor(Common cmn)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);
            
            // 共通クラス
            this.cmn = cmn;
        }

        /// <summary>
        /// 原価計算雛形ファイル参照先サーバーへの接続
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
            formNetReform.lpRemoteName = @cmn.FsCd[0].ShareName;
            formNetReform.lpProvider = "";

            int ret = 0;
            try
            {
                // 既に接続している場合があるので一旦切断する
                ret = WNetCancelConnection2(@cmn.FsCd[0].ShareName, 0, true);
                // 認証情報を使って共有フォルダに接続
                ret = WNetAddConnection2(ref formNetReform, cmn.FsCd[0].EncPasswd, cmn.FsCd[0].UserId, 0);
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
        /// 原価管理計算結果保存先サーバーへの接続
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
                // 既に接続している場合があるので一旦切断する
                ret = WNetCancelConnection2(@cmn.FsCd[1].ShareName, 0, true);
                // 認証情報を使って共有フォルダに接続
                ret = WNetAddConnection2(ref saveNetResource, cmn.FsCd[1].EncPasswd, cmn.FsCd[1].UserId, 0);
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
        public int ReadCSVFile(string path, Encoding encoding, bool isTitled, string tableName, ref DataTable dataTable)
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
            oXls.Visible = cmn.FsCd[1].VisibleExcel;  // Excelのウィンドウの表示/非表示を設定ファイルから取得

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
        public void ExportExcel(DataTable dtbl, string path, string filenm)
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

        /// <summary>
        /// Excel シートからデータテーブルへインポート
        /// </summary>
        /// <param name="worksheetName">ワークシート名称</param>
        /// <param name="saveAsLocation">保存先</param>
        /// <param name="ReporType">レポート種別</param>
        /// <param name="HeaderLine">見出し行</param>
        /// <param name="ColumnStart">開始列</param>
        /// <returns></returns>
        public DataTable ReadExcelToDatatble(Excel.Workbook excelworkBook, 
            string worksheetName, string saveAsLocation, string ReporType, int HeaderLine, int ColumnStart)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            DataTable dataTable = new DataTable();
            Excel.Worksheet excelSheet;
            Excel.Range range;
            try
            {
                // Workk sheet
                excelSheet = (Excel.Worksheet)
                                      excelworkBook.Worksheets.Item[worksheetName];
                range = excelSheet.UsedRange;
                int cl = range.Columns.Count;

                // loop through each row and add values to our sheet
                int rowcount = range.Rows.Count;

                //create the header of table
                for (int j = ColumnStart; j <= cl; j++)
                {
                    dataTable.Columns.Add(Convert.ToString
                                         (range.Cells[HeaderLine, j].Value2), typeof(string));
                }

                //filling the table from  excel file                
                for (int i = HeaderLine + 1; i <= rowcount; i++)
                {
                    DataRow dr = dataTable.NewRow();
                    for (int j = ColumnStart; j <= cl; j++)
                    {

                        dr[j - ColumnStart] = Convert.ToString(range.Cells[i, j].Value2);
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
                range = null;
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
        public int WriteDataTableToExcel(DataTable dataTable, Excel.Workbook excelworkBook, 
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
        public int WriteDataTableToExcel(DataTable dataTable, Excel.Workbook excelworkBook,
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
    }
}
