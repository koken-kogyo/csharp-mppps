using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm093_TanaChecker : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DataTable tanaDt = new DataTable(); // タナコン

        public Frm093_TanaChecker(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = " <" + Common.FRM_ID_093 + ": " + Common.FRM_NAME_093 + ">";

            // 共通クラス
            this.cmn = cmn;

            // 初期設定
            SetInitialValues();
        }

        // コントロールの初期化
        private async void SetInitialValues()
        {
            // 初期化
            toolStripStatusLabel1.Text = string.Empty;
            lbl_Result.BackColor = Color.DarkGray;
            lbl_Result.ForeColor = Color.DimGray;
            lbl_Result.Text = "手配番号を\n入力して下さい";

            // タナコンから空データを取得
            int ret = 0;
            await Task.Run(() => ret = cmn.Dba.GetTLOCSTOCK(ref tanaDt, "Initial Read."));
            if (ret < 0)
            {
                toolStripStatusLabel1.Text = "タナコンサーバーへのアクセスに失敗しました．";
                return;
            }

            // DataGridViewの初期設定
            Dgv_InvInfo.DataSource = tanaDt;

            // DataGridViewのヘッダー背景色を設定
            Dgv_InvInfo.EnableHeadersVisualStyles = false;
            Dgv_InvInfo.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            Dgv_InvInfo.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;

            //DataGridViewの画面ちらつきをおさえるため、DoubleBufferedを有効にする
            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(Dgv_InvInfo, true, null);

            // データグリッドビューの個別設定
            DataGridDetailSetting();

            // 初期フォーカス
            txtQRCD.Focus();
        }

        private void DataGridDetailSetting()
        {
            Dictionary<string, MultipleValues> dic = new Dictionary<string, MultipleValues>
            {
                { "品番",         new MultipleValues { JPNAME = "品番",         Width = 180 } },
                { "ロケーション", new MultipleValues { JPNAME = "ロケーション", Width = 140, StyleAlignment = DataGridViewContentAlignment.MiddleCenter } },
                { "在庫入庫日",   new MultipleValues { JPNAME = "在庫入庫日",   Width = 140, Format = "yyyy/MM/dd", StyleAlignment = DataGridViewContentAlignment.MiddleCenter } },
                { "出庫可能数",   new MultipleValues { JPNAME = "出庫可能数",   Width = 140, StyleAlignment = DataGridViewContentAlignment.MiddleCenter } },
                { "品名",         new MultipleValues { JPNAME = "品名",         Width = 400 } },
                { "MPUPDTID",   new MultipleValues { JPNAME = "MP更新者",       Width = 100 } },
                { "MPUPDTDT",   new MultipleValues { JPNAME = "MP更新日時",     Width = 100, Format = "MM/dd HH:mm" } }
            };

            for (int c = 0; c < Dgv_InvInfo.Columns.Count; c++)
            {
                try
                {
                    var headerName = Dgv_InvInfo.Columns[c].HeaderText;
                    Dgv_InvInfo.Columns[c].HeaderText = dic[headerName].JPNAME;
                    Dgv_InvInfo.Columns[c].Width = dic[headerName].Width;
                    Dgv_InvInfo.Columns[c].HeaderCell.Style.Alignment = dic[headerName].StyleAlignment;
                    Dgv_InvInfo.Columns[c].DefaultCellStyle.Alignment = dic[headerName].StyleAlignment;
                    Dgv_InvInfo.Columns[c].DefaultCellStyle.Format = dic[headerName].Format;
                }
                catch
                {
                    Dgv_InvInfo.Columns[c].HeaderText = Dgv_InvInfo.Columns[c].HeaderText;
                }
            }
            // 行高さ
            Dgv_InvInfo.RowTemplate.Height = 30;

        }

        private void TextQRCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SearchQRCD(txtQRCD.Text);
        }

        private void TextHMCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SearchHMCD(txtHMCD.Text);
        }

        private void TextHMCD_TextChanged(object sender, EventArgs e)
        {
            var selpos = txtHMCD.SelectionStart;
            var sellen = txtHMCD.SelectionLength;
            txtHMCD.Text = txtHMCD.Text.ToUpper();
            txtHMCD.SelectionStart = selpos;
            txtHMCD.SelectionLength = sellen;
        }

        private void txtLikeHMCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SearchHMCD("%" + txtLikeHMCD.Text + "%");
        }

        private void txtLikeHMCD_TextChanged(object sender, EventArgs e)
        {
            var selpos = txtLikeHMCD.SelectionStart;
            var sellen = txtLikeHMCD.SelectionLength;
            txtLikeHMCD.Text = txtLikeHMCD.Text.ToUpper();
            txtLikeHMCD.SelectionStart = selpos;
            txtLikeHMCD.SelectionLength = sellen;
        }

        private void Frm093_TanaChecker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        // 手配Noから品番と数量を取得
        private void SearchQRCD(string qrcd)
        {
            DataTable kd8430 = new DataTable();
            bool ret = cmn.Dba.GetMpQRCD(ref kd8430, qrcd);
            if (ret && kd8430.Rows.Count == 1)
            {
                string hmcd = kd8430.Rows[0]["HMCD"].ToString();
                string odrsts = kd8430.Rows[0]["ODRSTS"].ToString();
                int odrqty = Convert.ToInt32(kd8430.Rows[0]["ODRQTY"].ToString());
                string sts = 
                    (odrsts == "1") ? "追加" : 
                    (odrsts == "2") ? "確定" : 
                    (odrsts == "3") ? "着手" : 
                    (odrsts == "4") ? "完了" : 
                    (odrsts == "9") ? "取消" : "";
                lblHMCD.Text = hmcd;
                lblODRQTY.Text = odrqty.ToString();
                lblODRSTS.Text = sts;
                SearchHMCD(hmcd, odrqty);
            }
            else
            {
                lblHMCD.Text = string.Empty;
                lblODRQTY.Text = string.Empty;
                lblODRSTS.Text = string.Empty;
                lbl_Result.BackColor = Color.Red;
                lbl_Result.ForeColor = Color.Yellow;
                lbl_Result.Text = "手配なし";
                toolStripStatusLabel1.Text = "手配QRが見つかりませんでした．";
                txtQRCD.SelectAll();
                txtQRCD.Focus();
            }
        }

        // タナコンサーバー検索
        private async void SearchHMCD(string hmcd, int odrqty = 7171)
        {
            // タナコンからデータを取得
            int ret = 0;
            tanaDt.Clear();
            toolStripStatusLabel1.Text = string.Empty;
            await Task.Run(() => ret = cmn.Dba.GetTLOCSTOCK(ref tanaDt, hmcd));
            if (ret == -9)
            {
                lbl_Result.BackColor = Color.Red;
                lbl_Result.ForeColor = Color.Yellow;
                lbl_Result.Text = "未登録";
                toolStripStatusLabel1.Text = $"品番[{hmcd}]がタナコンに登録されていません．";
            }
            else if (tanaDt.Rows.Count > 0)
            {
                Dgv_InvInfo.DataSource = null;
                Dgv_InvInfo.DataSource = tanaDt;
                DataGridDetailSetting();
                if (odrqty != 7171)
                {
                    int qty = tanaDt.AsEnumerable().Select(c => Convert.ToInt32(c["出庫可能数"].ToString())).Sum();
                    if (qty >= odrqty)
                    {
                        lbl_Result.BackColor = Color.LightGreen;
                        lbl_Result.ForeColor = Color.Blue;
                        lbl_Result.Text = "出庫可能";
                    }
                    else
                    {
                        lbl_Result.BackColor = Color.Orange;
                        lbl_Result.ForeColor = Color.Red;
                        lbl_Result.Text = "手配不足";
                    }
                }
                else
                {
                    lblHMCD.Text = string.Empty;
                    lblODRQTY.Text = string.Empty;
                    lblODRSTS.Text = string.Empty;
                    lbl_Result.BackColor = Color.LightGreen;
                    lbl_Result.ForeColor = Color.Blue;
                    lbl_Result.Text = "登録あり";
                }
            }
            else
            {
                if (odrqty == 7171)
                {
                    lblHMCD.Text = string.Empty;
                    lblODRQTY.Text = string.Empty;
                    lblODRSTS.Text = string.Empty;
                }
                lbl_Result.BackColor = Color.Red;
                lbl_Result.ForeColor = Color.Yellow;
                lbl_Result.Text = "在庫なし";
            }
            // 次にカーソルセット
            if (hmcd.Contains("%"))
            {
                txtLikeHMCD.SelectAll();
                txtLikeHMCD.Focus();
            } 
            else if (odrqty == 7171) {
                txtHMCD.SelectAll();
                txtHMCD.Focus();
            }
            else
            {
                txtQRCD.SelectAll();
                txtQRCD.Focus();
            }
        }

    }
}
