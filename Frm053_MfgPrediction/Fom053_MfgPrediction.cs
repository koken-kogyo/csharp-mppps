using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm053_MfgPrediction : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DataTable km8420 = new DataTable();     // 設備マスタ

        // コンストラクタ
        public Frm053_MfgPrediction(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_053 + ": " + Common.FRM_NAME_053 + ">";
            // 共通クラス
            this.cmn = cmn;

            // 初期設定
            SetInitialValues();

            // イベント登録
            dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;           // 行番号と矢印
        }

        // 初期設定
        private void SetInitialValues()
        {
            // データ読み込み
            if (cmn.Dba.GetEquipMstDt(ref km8420)==false) Close();
            DataTable xt2OrderDt = new DataTable();
            bool ret = cmn.Dba.ReadXT2(ref xt2OrderDt);

            // 設備グループドロップダウン設定
            List<string> mcgcds = km8420.AsEnumerable()
                .OrderBy(x => x["MCGSEQ"])
                .GroupBy(grp => new {
                    MCGCD = grp["MCGCD"].ToString()
                })
                .Select(row => row.Key.MCGCD)
                .ToList();
            cmb_MCGCD.Items.AddRange(mcgcds.ToArray());
            cmb_MCGCD.Text = "MS";  // デフォルト値を設定＆イベントを発生させる

            // DataGridView のフォントを全部 MS ゴシック 10pt にする
            dataGridView1.Font = new Font("MS Gothic", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("MS Gothic", 9);
            dataGridView1.RowHeadersDefaultCellStyle.Font = new Font("MS Gothic", 10);
            // DataGridViewの明細を2行毎に背景色設定
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            //DataGridViewの画面ちらつきをおさえるため、DoubleBufferedを有効にする
            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(dataGridView1, true, null);

        }


        // 行番号を付ける（1 から始める）
        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (e.RowIndex == dgv.NewRowIndex) return; // ★ 新規行は行番号を描画しない、「※」は描画

            // ★ 通常行は矢印を上書きして消す
            Rectangle rect = new Rectangle(
                e.RowBounds.Left + 1,
                e.RowBounds.Top + 1,
                dgv.RowHeadersWidth - 2,
                e.RowBounds.Height - 2);
            e.Graphics.FillRectangle(SystemBrushes.Control, rect);

            // 行番号を描画
            string rowNumber = (e.RowIndex + 1).ToString();
            var headerBounds = new Rectangle(
                e.RowBounds.Left,
                e.RowBounds.Top,
                dataGridView1.RowHeadersWidth,
                e.RowBounds.Height);
            TextRenderer.DrawText(
                e.Graphics,
                rowNumber,
                dataGridView1.Font,
                headerBounds,
                Color.Black,
                TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }

        // ショートカットキー
        private void Frm053_MfgPrediction_KeyDown(object sender, KeyEventArgs e)
        {
            // 「閉じる」
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            // 「検索」
            else if (e.KeyCode == Keys.F5)
            {
                BtnSearch_Click(sender, e);
            }
        }

        // 設備グループ変更イベント
        private void Cmb_MCGCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_MCCD.Items.Clear();
            var findstring = cmb_MCGCD.Text;
            List<string> mccds = km8420.AsEnumerable()
                .Where(x => x["MCGCD"].ToString().StartsWith(findstring) && x["MCSEQ"].ToString().Length < 3)
                .OrderBy(x => x["MCSEQ"])
                .Select(c => c["MCCD"].ToString())
                .ToList();
            cmb_MCCD.Items.AddRange(mccds.ToArray());
            cmb_MCCD.SelectedIndex = 0;  // デフォルト値を設定
        }

        // Excelにエクスポート
        private void ButtonExcelExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在実装中です．しばらくお待ちください。", "手動でExcelにコピペして！Ctrl+A > C > V", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private DateTime? SafeGetDate(object v)
        {
            if (v == DBNull.Value) return null;

            if (v is DateTime dt) return dt;

            if (v is string s && DateTime.TryParse(s, out var dt2))
                return dt2;

            return null;
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("データの取得には数十秒かかりますがよろしいですか？", "確認"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            this.UseWaitCursor = true;   // ★ フォーム全体を処理中カーソルにする
            btnSearch.Enabled = false;   // ★ 誤操作防止（任意）

            bool ret = false;
            string mcgcd = cmb_MCGCD.Text;
            string mccd = cmb_MCCD.Text;
            string hmcdList = cmn.Dba.GetHMCDListFromMySQL(mcgcd, mccd);
            DataTable src = new DataTable(); // Oracleから取得した縦持ちデータ
            await Task.Run(() => ret = cmn.Dba.おかむーリストデータ取得(src, hmcdList));

            this.UseWaitCursor = false;  // ★ カーソルを戻す
            btnSearch.Enabled = true;
            if (ret == false) return;

            // ピボット用の結果テーブル
            DataTable pivot = new DataTable();
            pivot.Columns.Add("切削品番");
            pivot.Columns.Add("区分");

            // 月一覧を動的に抽出（DBNull・文字列混在でも絶対に落ちない）
            var months = src.AsEnumerable()
                .Select(r =>
                {
                    var v = r["集計月"];

                    // DBNull → null
                    if (v == DBNull.Value) return (DateTime?)null;

                    // 文字列の場合（Oracleからの縦持ちでよくある）
                    if (v is string s)
                    {
                        if (DateTime.TryParse(s, out var dt))
                            return (DateTime?)dt;
                        return null; // パース不可 → 無視
                    }

                    // DateTime の場合
                    if (v is DateTime dt2)
                        return (DateTime?)dt2;

                    return null;
                })
                .Where(dt => dt.HasValue)
                .Select(dt => dt.Value)
                .Distinct()
                .OrderBy(d => d);

            foreach (var m in months)
            {
                pivot.Columns.Add(m.ToString("yyyy-MM"), typeof(decimal));
            }

            // ピボット化
            // 「切削品番 × 区分」で行を作る
            var kohmcdList = src.AsEnumerable()
                .Select(r => new
                {
                    品番 = r.Field<string>("切削品番"),
                    区分 = r.Field<string>("区分")   // ★ Oracle側の列名に合わせて
                })
                .Distinct()
                .OrderBy(x => x.品番)
                .ThenBy(x => x.区分 == "内示" ? 0 : 1);
            string prev品番 = null;
            foreach (var item in kohmcdList)
            {
                DataRow row = pivot.NewRow();
                if (item.品番 != prev品番)
                    row["切削品番"] = item.品番;
                else
                    row["切削品番"] = ""; // ★ 品番が前行と同じなら空白にする
                row["区分"] = item.区分;
                foreach (var m in months)
                {
                    var qty = src.AsEnumerable()
                       .Where(r =>
                           r.Field<string>("切削品番") == item.品番 &&
                           r.Field<string>("区分") == item.区分 &&
                           SafeGetDate(r["集計月"]) == m &&
                           !r.IsNull("注文数")
                       )
                       .Select(r => r.Field<decimal>("注文数"))
                       .FirstOrDefault();
                    if (qty != 0) row[m.ToString("yyyy-MM")] =  qty;
                }
                pivot.Rows.Add(row);
                prev品番 = item.品番;
            }

            // DataGridView に表示
            dataGridView1.DataSource = pivot;

            // 列幅調整
            dataGridView1.Columns["切削品番"].Width = 130;   // 先頭列は広め
            dataGridView1.Columns["区分"].Width = 40;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Name != "切削品番" && col.Name != "区分")
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.Width = 50;   // 月列は狭く
                }
            }
        }

        private void btnHMCDPaste_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在実装中です．しばらくお待ちください。", "ごめん！", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnFilterClear_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在実装中です．しばらくお待ちください。", "ごめん！", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("現在実装中です．しばらくお待ちください。", "ごめん！", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("現在実装中です．しばらくお待ちください。", "ごめん！", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
