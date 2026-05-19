using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace MPPPS
{
    /// <summary>
	/// データベース アクセス クラス (切削XT工程専用)
    /// </summary>
    public partial class DBAccessor
    {
        // コード票マスタを行列変換して活用（必要な列だけに絞ってあるので注意）
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
        public bool ReadXT2(ref System.Data.DataTable xt2OrderDt)
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
                    + "a.MCGCD = 'XT' and a.MCCD = 'XT2' and a.ODRSTS <> '9' "
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


        /// <summary>
        /// 計画データ登録
        /// </summary>
        public bool SavePlanOrder(ref DataGridView dgv, int STARTCOL)
        {
            MySqlConnection mpCnn = null;
            MySqlTransaction transaction = null;
            string sql;
            int insertCount = 0;
            int updateCount = 0;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);
                string schema = cmn.DbCd[Common.DB_CONFIG_MP].Schema;

                // トランザクション開始
                transaction = mpCnn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = mpCnn
                };

                // ①インサート処理
                StringBuilder sb = new StringBuilder(); // Bulk Insert用
                string odrmaxno = GetLastOrderNo(ref mpCnn); // YYMM900000
                if (odrmaxno == "") return false;
                string yymm = odrmaxno.Substring(0, 4);
                int seq = int.Parse(odrmaxno.Substring(4, 6));
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    for (int col = STARTCOL; col < dgv.Columns.Count; col++)
                    {
                        string hmcd = dgv[0, row].Value.ToString();
                        if (dgv[col, row].Value != null)
                        {
                            Order o = (Order)dgv[col, row].Value;
                            if (o.EditStatus != "" && o.EditStatus.Substring(0, 1) == "1")// 1:NEWORDER
                            {
                                DateTime eddt = ConvertHeaderToDate(dgv.Columns[col].HeaderText);
                                int odrqty = o.Qty;
                                string note = o.Note;

                                // 見込手配登録処理
                                seq++;
                                string newOdrno = $"{yymm}{seq:000000}";
                                insertCount += InsertKD8430KD8450(ref mpCnn, schema, newOdrno, hmcd, eddt, odrqty, note);

                            }
                        }
                    }
                }

                // ②更新処理
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    for (int col = STARTCOL; col < dgv.Columns.Count; col++)
                    {
                        string hmcd = dgv[0, row].Value.ToString();
                        if (dgv[col, row].Value != null)
                        {
                            Order o = (Order)dgv[col, row].Value;
                            string newsts = ((o.EditStatus == "") ? "0" : o.EditStatus).Substring(0, 1);
                            if (newsts == "2" || newsts == "9") // 2:MODIFY, 9:CANCEL
                            {
                                string odrno = o.OrderNo;
                                DateTime eddt = ConvertHeaderToDate(dgv.Columns[col].HeaderText);
                                int odrqty = o.Qty;
                                string note = o.Note;

                                // KD8450:切削オーダーファイルの更新
                                sql = PlanOrderUpdateSql(newsts, odrno, eddt, odrqty, note);
                                cmd.CommandText = sql;
                                updateCount += cmd.ExecuteNonQuery();

                            }
                        }
                    }
                }


                // トランザクション終了
                transaction.Commit();

                cmn.Dbm.CloseMySqlSchema(mpCnn);
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
                catch (Exception rollBackEx)
                {
                    MessageBox.Show("トランザクションのロールバックに失敗しました．" + rollBackEx.Message);
                }
                return false;
            }
            return true;
        }
        
        // データグリッドのヘッダーテキストから日付型を取得
        private DateTime ConvertHeaderToDate(string header)
        {
            // ヘッダーを月日としてパース
            DateTime md = DateTime.ParseExact(header, "M/d", null);

            // まずは基準日の年を使う
            DateTime result = new DateTime(DateTime.Today.Year, md.Month, md.Day);

            // もし基準日より前の日付なら翌年扱い
            if (result < DateTime.Today.Date)
            {
                result = result.AddYears(1);
            }

            return result;
        }

        // 最終手配Noを取得
        public string GetLastOrderNo(ref MySqlConnection mpCnn)
        {
            var sql = $@"
                select ifnull(max(odrno),
                    concat(date_format(now() - interval 0 month, '%y'), date_format(now() - interval 0 month, '%m'), '900000')) as odrno
                from {cmn.DbCd[Common.DB_CONFIG_MP].Schema}.kd8430 where odrno between
                    concat(date_format(now() - interval 0 month, '%y'), date_format(now() - interval 0 month, '%m'), '900000') and
                    concat(date_format(now() - interval 0 month, '%y'), date_format(now() - interval 0 month, '%m'), '999999')";
            // Debug用に interval 0 は残しておく（-1でテストは行う）
            string odrno;
            using (MySqlCommand cmd = new MySqlCommand(sql, mpCnn))
            {
                odrno = cmd.ExecuteScalar().ToString();// ExecuteScalar():１件１項目の場合に使用できるメソッド
            }
            return odrno;
        }

        /// <summary>
        /// 見込手配登録処理
        /// </summary>
        /// <param name="row">登録データ</param>
        /// <returns>登録件数</returns>
        public int InsertKD8430KD8450(ref MySqlConnection mpCnn, string schema
            , string newOdrno, string hmcd, DateTime eddt, int odrqty, string note)
        {
            string insertSQL = "";
            int insertCount = 0;
            try
            {
                // ①品目手順マスタから登録に必要な情報を取得
                var sql = $@"
                    select m.KTSEQ, m.KTCD, m.ODCD, m.ODRLT, m.WKNOTE, m.WKCOMMENT
                        ,KTSU, KT1MCGCD, KT1MCCD, KT2MCGCD, KT2MCCD,KT3MCGCD, KT3MCCD
                        ,KT4MCGCD, KT4MCCD, KT5MCGCD, KT5MCCD, KT6MCGCD, KT6MCCD
                    from {schema}.m0510 m
                    join (
                        select HMCD, KTSEQ, max(VALDTF) AS VALDTF
                        from {schema}.m0510
                        where HMCD='{hmcd}' and KTCD like 'MP%' and ODCD like '6060%' and JIKBN = '1'
                        group by HMCD, KTSEQ
                    ) x on m.HMCD = x.HMCD AND m.VALDTF = x.VALDTF and m.KTSEQ = x.KTSEQ
                    inner join KM8430 km on km.HMCD = m.HMCD
                    order by m.KTSEQ limit 1
                ";
                using (MySqlCommand cmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();  // 1行だけあればいいかな
                        int ktseq = reader.GetInt32("KTSEQ");
                        string ktcd = reader.GetString("KTCD");
                        string odcd = reader.GetString("ODCD");
                        int lttime = reader.GetInt32("ODRLT");                  // M0520.ODRLT：製造購買LT
                        string wknote = reader.IsDBNull(reader.GetOrdinal("WKNOTE")) ? ""
                            : reader.GetString(reader.GetOrdinal("WKNOTE"));
                        string wkcomment = reader.IsDBNull(reader.GetOrdinal("WKCOMMENT")) ? ""
                            : reader.GetString("WKCOMMENT");
                        reader.Close();
                        // ②KD8430:切削手配の登録
                        insertSQL = InsertMpOrderSQL(newOdrno, ktseq, hmcd, ktcd, odrqty, odcd, lttime, eddt, wknote, wkcomment);
                        cmd.CommandText = insertSQL;
                        insertCount += cmd.ExecuteNonQuery();
                        // ③KD8450:切削オーダーファイルの登録
                        insertSQL = DivideMpOrderSQL(newOdrno, 1, "XT", "XT2", hmcd, eddt, odrqty);
                        cmd.CommandText = insertSQL;
                        cmd.ExecuteNonQuery();
                    }
                }
                return insertCount;
            }
            catch
            {
                MessageBox.Show($"手配登録に失敗しました\n{insertSQL}");
                return -1;
            }
        }
        /// <summary>
        /// SQL 構文編集 (KD8430 切削手配ファイル) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string InsertMpOrderSQL(string newOdrno, int ktseq, string hmcd, string ktcd, decimal odrqty, string odcd, int lttime
            , DateTime eddt, string wknote, string wkcomment)
        {
            wknote = string.IsNullOrEmpty(wknote) ? "null" : "'" + wknote + "'";
            wkcomment = string.IsNullOrEmpty(wkcomment) ? "null" : "'" + wkcomment + "'";
            string sql = $@"insert into kd8430 (
                ODRNO,KTSEQ,HMCD,KTCD,ODRQTY,
                ODCD,NEXTODCD,LTTIME,STDT,STTIM,
                EDDT,EDTIM,ODRSTS,QRCD,JIQTY,
                DENPYOKBN,DENPYODT,NOTE,WKNOTE,WKCOMMENT,
                DATAKBN,INSTID,INSTDT,UPDTID,UPDTDT,
                UKCD,NAIGAIKBN,RETKTCD,MPCARDDT,MPINSTID,
                MPUPDTID) values (
                '{newOdrno}',{ktseq},'{hmcd}','{ktcd}',{odrqty},
                '{odcd}',null,{lttime},null,null,
                '{eddt}','08:10','2',null,0,
                '1',null,null, {wknote} ,{wkcomment},
                '1','YAKAN','{DateTime.Now}','{cmn.IkM0010.TanCd}','{DateTime.Now}',
                '','1','{ktcd}',null,'{cmn.IkM0010.TanCd}',
                'YAKAN')";
            return sql;
        }
        /// <summary>
        /// SQL 構文編集 (KD8450 切削オーダーファイル) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private static string DivideMpOrderSQL(string newOdrno, int mpseq, string mcgcd, string mccd, string hmcd, DateTime eddt, decimal odrqty)
        {
            string sql = $@"insert into kd8450 (
                ODRNO,MPSEQ,MCGCD,MCCD,HMCD,
                EDDT,ODRQTY,JIQTY,ODRSTS,MPINSTID,
                MPUPDTID) values (
                '{newOdrno}',{mpseq},'{mcgcd}','{mccd}','{hmcd}',
                '{eddt}',{odrqty},0,'2','YAKAN',
                'YAKAN')";
            return sql;
        }
        /// <summary>
        /// SQL 構文編集 KD8450：切削オーダーファイル
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string PlanOrderUpdateSql(string newsts, string odrno, DateTime eddt, int odrqty, string note)
        {
            string newnote = (string.IsNullOrEmpty(note)) ? "null" : "'" + note + "'";

            string sql = "update "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8450 + " "
                + $"set EDDT='{eddt}',ODRQTY={odrqty},ODRSTS='{newsts}',NOTE={newnote},MPUPDTID='{cmn.IkM0010.TanCd}'"
                + $" where ODRNO='{odrno}'";
            return sql;
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
                if (cmn.Fa.IsWorkbookOpen(f.FileName.Replace("雛型", $"{DateTime.Now:yyyyMMdd}")))
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
        public void ExportFromDgvToExcel(
            string templateFullPath
            , string outputFullPath
            , DataGridView dgv
            , System.Windows.Forms.DataVisualization.Charting.Chart chart1)
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
                int colCount = map.Values.Max() + 1 + 1; // 設定値の中の最大値（0スタートなので + 1個 + 備考の1個）

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
                            if (!string.IsNullOrEmpty(oc.Note))
                                data[r, 34] = oc.Note;      // 備考
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

                // チャートの貼り付け
                string imagePath = @"C:\temp\chart.png";
                chart1.SaveImage(imagePath, ChartImageFormat.Png);
                Excel.Range cell = xlSheet.Cells[startRow + rowCount + 5, 2];
                float left = (float)cell.Left;
                float top = (float)cell.Top;
                // 2. 画像サイズを取得、0.5倍率で貼り付け
                using (Image img = Image.FromFile(imagePath))
                {
                    float w = img.Width * 0.5f;
                    float h = img.Height * 0.5f;
                    xlSheet.Shapes.AddPicture(
                        imagePath,
                        Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoCTrue,
                        left,
                        top,
                        w,
                        h
                    );
                }

                // 名前を付けて保存
                xlApp.DisplayAlerts = false;
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
