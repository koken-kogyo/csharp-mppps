using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace MPPPS
{
    public partial class Frm092_CutStoreInvInfo : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DataTable invInfoEMDt = new DataTable(); // EM在庫ファイルを保持
        private DataTable invInfoMPDt = new DataTable(); // MP在庫ファイルを保持
        private bool loadedFlg = false;

        public Frm092_CutStoreInvInfo(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_041 + ": " + Common.FRM_NAME_041 + ">";

            // 共通クラス
            this.cmn = cmn;

            // 初期設定
            SetInitialValues();
        }

        // コントロールの初期化
        private async void SetInitialValues()
        {
            // 全画面表示
            this.WindowState = FormWindowState.Maximized;

            // データベースからマスタを取得するタスクを登録
            bool retEM = false;
            bool retMP = false;
            var taskEM = Task.Run(() => retEM = cmn.Dba.GetInvInfoEMDt(ref invInfoEMDt));
            var taskMP = Task.Run(() => retMP = cmn.Dba.GetInvInfoMPDt(ref invInfoMPDt));
            await Task.WhenAll(taskEM, taskMP);
            if (retEM == false || retMP == false) return;
            // DataGridViewの初期設定
            Dgv_InvInfo.DataSource = invInfoMPDt;
            // DataGridViewのヘッダー背景色を設定
            Dgv_InvInfo.EnableHeadersVisualStyles = false;
            Dgv_InvInfo.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            Dgv_InvInfo.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            // DataGridViewの明細を2行毎に背景色設定
            Dgv_InvInfo.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;

            //DataGridViewの画面ちらつきをおさえるため、DoubleBufferedを有効にする
            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(Dgv_InvInfo, true, null);

            // データグリッドビューの個別設定
            DataGridDetailSetting();

            // EMの在庫数を追加で表示
            addEMQTY(ref invInfoEMDt);

            loadedFlg = true;

        }

        // データグリッドビューの個別設定
        private void DataGridDetailSetting()
        {
            // 列ヘッダーの文字列を文字位置を設定
            var offset = 0;
            string[] s1 = {
                "品番",
                "在庫場所",
                "設備ｺｰﾄﾞ",
                "在庫数",
                "最終入庫日時",
                "最終出庫日時",
                "登録ID",
                "登録日時",
                "更新ID",
                "更新日時",
            };
            for (int i = 0; i < s1.Length; i++)
            {
                Dgv_InvInfo.Columns[i + offset].HeaderText = s1[i];
            }
            offset += s1.Length;

            // 在庫数 AlignRight設定
            Dgv_InvInfo.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Dgv_InvInfo.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            // 在庫数のカンマ区切りフォーマット
            Dgv_InvInfo.Columns[3].DefaultCellStyle.Format = "#,0";

            // DataGridViewの幅を個別設定
            Dgv_InvInfo.Columns[0].Width = 230;         // 品番

        }

        // EMの在庫数を追記
        private void addEMQTY(ref DataTable invInfoEMDt)
        {
            //DataGridViewTextBoxColumn列を作成する
            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            // データソースの"Column1"をバインドする
            textColumn.DataPropertyName = "EMQTY";
            // 名前とヘッダーを設定する
            textColumn.Name = "EMQTY";
            textColumn.HeaderText = "EM在庫";
            // 初回起動時のみ列を追加する
            if (Dgv_InvInfo.Columns.Count == invInfoMPDt.Columns.Count)
            {
                Dgv_InvInfo.Columns.Add(textColumn);
                // AlignRight設定
                Dgv_InvInfo.Columns[Dgv_InvInfo.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                Dgv_InvInfo.Columns[Dgv_InvInfo.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                // EM在庫数のカンマ区切りフォーマット
                Dgv_InvInfo.Columns[Dgv_InvInfo.Columns.Count - 1].DefaultCellStyle.Format = "#,0";
            }
            foreach (DataGridViewRow r in Dgv_InvInfo.Rows)
            {
                DataRow[] em = invInfoEMDt.Select($"HMCD='{r.Cells["HMCD"].Value}'");
                if (em.Length == 1)
                {
                    r.Cells["EMQTY"].Value = em[0]["EMQTY"];
                }
                else
                {
                    var t = invInfoEMDt.Select($"HMCD='{r.Cells["HMCD"].Value}'")
                        .Where(w => w["KTCD"].ToString().StartsWith("MP"))
                        .Sum(s => int.Parse(s["EMQTY"].ToString()));
                    r.Cells["EMQTY"].Value = t + "(+)";
                }
            }
        }

        private void Dgv_InvInfo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // 行ヘッダのセル領域を、行番号を描画する長方形とする
            // （ただし右端に4ドットのすき間を空ける）
            Rectangle rect = new Rectangle(
              e.RowBounds.Location.X,
              e.RowBounds.Location.Y,
              Dgv_InvInfo.RowHeadersWidth - 4,
              e.RowBounds.Height);

            // 上記の長方形内に行番号を縦方向中央＆右詰めで描画する
            // フォントや前景色は行ヘッダの既定値を使用する
            TextRenderer.DrawText(
              e.Graphics,
              (e.RowIndex + 1).ToString(),
              Dgv_InvInfo.RowHeadersDefaultCellStyle.Font,
              rect,
              Dgv_InvInfo.RowHeadersDefaultCellStyle.ForeColor,
              TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void Dgv_InvInfo_Sorted(object sender, EventArgs e)
        {
            addEMQTY(ref invInfoEMDt);
        }

        private void btnHMCDClear_Click(object sender, EventArgs e)
        {
            txtHMCD.Text = string.Empty;
        }

        private void txtHMCD_TextChanged(object sender, EventArgs e)
        {
            var selpos = txtHMCD.SelectionStart;
            var sellen = txtHMCD.SelectionLength;
            txtHMCD.Text = txtHMCD.Text.ToUpper();
            txtHMCD.SelectionStart = selpos;
            txtHMCD.SelectionLength = sellen;
            myFilter();
        }

        // 検索条件を設定
        private void myFilter()
        {
            var filter = "";
            filter = (txtHMCD.Text.Length == 0) ? string.Empty :
                $"HMCD LIKE '{txtHMCD.Text}*'";
            //if (filter != string.Empty && cmbMaterial.SelectedIndex > 0)
            //    filter += " and ";
            //filter += (cmbMaterial.SelectedIndex <= 0) ? string.Empty :
            //    $"MATESIZE LIKE '{cmbMaterial.Text}*'";
            // 複数検索条件を設定
            invInfoMPDt.DefaultView.RowFilter = filter;
            addEMQTY(ref invInfoEMDt);
        }

        private void Frm092_CutStoreInvInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }
    }
}
