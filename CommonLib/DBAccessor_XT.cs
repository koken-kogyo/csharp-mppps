using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace MPPPS
{
    /// <summary>
	/// データベース アクセス クラス (切削XT工程専用)
    /// </summary>
    public partial class DBAccessor
    {

        private readonly string vmst = @"
            with vmst as 
            (
            select 1 as SEQ, KT1MCGCD as MCGCD, KT1MCCD as MCCD, ifnull(KT1CT,0) as CT, HMCD, HMNM, MATESIZE, LENGTH, KTSU 
            from km8430 where KT1MCGCD is not null and KT1MCGCD <> '' and KT1MCGCD <> 'EX'
            union
            select 2 as SEQ, KT2MCGCD as MCGCD, KT2MCCD as MCCD, ifnull(KT2CT,0) as CT, HMCD, HMNM, MATESIZE, LENGTH, KTSU 
            from km8430 where KT2MCGCD is not null and KT2MCGCD <> '' and KT2MCGCD <> 'EX'
            union
            select 3 as SEQ, KT3MCGCD as MCGCD, KT3MCCD as MCCD, ifnull(KT3CT,0) as CT, HMCD, HMNM, MATESIZE, LENGTH, KTSU 
            from km8430 where KT3MCGCD is not null and KT3MCGCD <> '' and KT3MCGCD <> 'EX'
            union
            select 4 as SEQ, KT4MCGCD as MCGCD, KT4MCCD as MCCD, ifnull(KT4CT,0) as CT, HMCD, HMNM, MATESIZE, LENGTH, KTSU 
            from km8430 where KT4MCGCD is not null and KT4MCGCD <> '' and KT4MCGCD <> 'EX'
            union
            select 5 as SEQ, KT5MCGCD as MCGCD, KT5MCCD as MCCD, ifnull(KT5CT,0) as CT, HMCD, HMNM, MATESIZE, LENGTH, KTSU 
            from km8430 where KT5MCGCD is not null and KT5MCGCD <> '' and KT5MCGCD <> 'EX'
            union
            select 6 as SEQ, KT6MCGCD as MCGCD, KT6MCCD as MCCD, ifnull(KT6CT,0) as CT, HMCD, HMNM, MATESIZE, LENGTH, KTSU 
            from km8430 where KT6MCGCD is not null and KT6MCGCD <> '' and KT6MCGCD <> 'EX'
            )
        ";

        /// <summary>
        /// 内示データ(xt2)取得
        /// </summary>
        /// <param name="xt2OrderDt">内示データ</param>
        /// <returns>注文情報データ</returns>
        public bool ReadXT2(ref DataTable xt2OrderDt)
        {
            bool ret;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = vmst +
                    "SELECT a.ODRNO, a.HMCD, a.EDDT, a.ODRQTY, a.NOTE, v.HMNM, v.MATESIZE, v.CT, v.LENGTH, v.KTSU "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8450 + " a "
                    + "INNER JOIN vmst v on "
                        + "v.HMCD = a.HMCD and v.MCGCD = a.MCGCD "
                    + "WHERE "
                    + "a.MCGCD = 'XT' and a.MCCD = 'XT2' "
                    + $"and EDDT between '2026/6/1' and '2026/6/30'"
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        myDa.Fill(dataTable: xt2OrderDt);
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("内示データの取得に失敗しました。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ret = false;
            }
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return ret;
        }


        /// <summary>
        /// D0470切削母材情報をサマリーしてマスターとして使用
        /// </summary>
        public Dictionary<string, decimal> GetD0470Dictionary()
        {
            var dict = new Dictionary<string, decimal>();
            OracleConnection cnn = null;
            cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref cnn);
            string sql = "select HMCD,SOLEN from D0470 group by HMCD,SOLEN order by HMCD";
            using (var cmd = cnn.CreateCommand())
            {
                cmd.CommandText = sql;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string hmcd = reader.GetString(0);
                        decimal solen = reader.IsDBNull(1) ? 0m : reader.GetDecimal(1);
                        if (!dict.ContainsKey(hmcd)) dict.Add(hmcd, solen);
                    }
                }
            }
            cmn.Dbm.CloseOraSchema(cnn);
            return dict;
        }


    }







    /// <summary>
	/// ファイル アクセス クラス (切削XT工程専用)
    /// </summary>
    public partial class FileAccessor
    {
        public bool 製造部計画表出力ファイルチェック(out string templateFullPath, out string outputFullPath)
        {
            var f = cmn.FsCd[7];
            templateFullPath = Path.Combine(f.RootPath, f.FileName);

            // 雛形ファイルの存在チェック
            if (File.Exists(@templateFullPath) == false)
            {
                MessageBox.Show("雛形ファイルが存在しません．\n" + cmn.FsCd[7], "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                outputFullPath = null;
                return false;
            }

            // 出力ファイルのチェック
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            outputFullPath = Path.Combine(userProfile, f.FileName.Replace("雛型", $"{DateTime.Now:yyyyMMdd}"));
            if (File.Exists(outputFullPath))
            {
                // Excelブックが開いているかどうか確認
                if (cmn.Fa.IsWorkbookOpen(f.FileName))
                {
                    MessageBox.Show("Excelブックが開かれています。書き込み出来ません．", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                // 読み取り専用確認
                FileInfo fileInfo = new FileInfo(outputFullPath);
                if ((fileInfo.Exists && fileInfo.IsReadOnly))
                {
                    MessageBox.Show("読み取り専用で書き込み出来ません．", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                // 上書き保存確認
                if (MessageBox.Show("上書き保存してもよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    == DialogResult.No) return false;
            }
            return true;
        }




        // データグリッドビューの内容をExcelに出力する（高速版）
        public void ExportFromDgvToExcel(string templateFullPath, string outputFullPath, ref DataGridView dgv)
        {
            Excel.Application xlApp = null;
            Excel.Workbook xlBook = null;
            Excel.Worksheet xlSheet = null;

            try
            {
                xlApp = new Excel.Application();
                xlApp.Visible = true;
                xlBook = xlApp.Workbooks.Open(templateFullPath);
                xlSheet = xlBook.Worksheets[1];


                // ★ 列順対応表（DGV列 → Object配列(0:行番号からスタート)）
                var map = new Dictionary<int, int>
                {
                    { 0, 1 },      // 品番
                    { 1, 2 },      // 品名
                    { 2, 29 },     // 頭
                    { 3, 31 },     // 材料
                    { 4, 33 },     // CT 
                    { 5, 32 },     // 製品長さ
                    { 6, 28 },     // 工程数
                    {  7,  3 },{  8,  4 },{  9,  5 },{ 10,  6 },{ 11,  7 },
                    { 12,  8 },{ 13,  9 },{ 14, 10 },{ 15, 11 },{ 16, 12 },
                    { 17, 13 },{ 18, 14 },{ 19, 15 },{ 20, 16 },{ 21, 17 },
                    { 22, 18 },{ 23, 19 },{ 24, 20 },{ 25, 21 },{ 26, 22 },
                    { 27, 23 },{ 28, 24 }
                };
                /*

                 */
                int rowCount = dgv.Rows.Count;
                int colCount = map.Values.Max() + 1; // 設定値の中の最大値（0スタートなので + 1個）

                // ★ 5行目と6行目の間に (件数 - 2) 行を挿入
                int insertCount = Math.Max(rowCount - 2, 0);
                if (insertCount > 0)
                {
                    Excel.Range insertPos = xlSheet.Rows[6]; // 6行目の前に挿入
                    xlSheet.Rows[6].Resize[insertCount].Insert();
                }

                // ★ DataGridView → object[,] に変換（高速）
                object[,] data = new object[rowCount, colCount]; // ※Excel[rowCount行, 50列]、配列番号が 1からスタートするので注意！

                for (int r = 0; r < rowCount; r++)
                {
                    data[r, 0] = r + 1;         // 行番号
                    foreach (var kv in map)
                    {
                        var cellValue = dgv.Rows[r].Cells[kv.Key].Value;
                        if (cellValue is Order oc)
                        {
                            data[r, kv.Value] = oc.Qty;     // 画面に表示されている数量だけを入れる
                        }
                        else
                        {
                            data[r, kv.Value] = cellValue;  // DataGridの値をObjectにセット
                        }
                    }
                }

                // ★ Excel の書き込み開始位置（Rangeの配列番号は1:A列スタート）
                int startRow = 5;
                int startCol = 1;

                Excel.Range range = xlSheet.Range[
                    xlSheet.Cells[startRow, startCol],
                    xlSheet.Cells[startRow + rowCount - 1, startCol + colCount - 1]
                ];

                // ★ 一括書き込み
                try
                {
                    range.Value = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Excelへの書き込みに失敗しました。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                xlApp.DisplayAlerts = false;   // ★ 上書き確認を出さない
                xlBook.SaveAs(outputFullPath);
            }
            finally
            {
                // アプリケーションの終了前に破棄可能なオブジェクトを破棄します。
                if (xlBook != null)
                {
                    xlBook.Close(SaveChanges: false);
                    Marshal.ReleaseComObject(xlBook);
                    xlBook = null;
                }
                // アプリケーションを終了します。
                if (xlApp != null)
                {
                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlApp);
                    xlApp = null;
                }
                // Application オブジェクトのガベージ コレクトを強制します。
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }



    }

}
