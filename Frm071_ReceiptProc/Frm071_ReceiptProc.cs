using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm071_ReceiptProc : Form
    {
        // 共通クラス
        private readonly Common cmn;

        // サウンドプレイヤーの初期化
        private readonly SoundPlayer ok;
        private readonly SoundPlayer ng;

        public Frm071_ReceiptProc(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = " <" + Common.FRM_ID_071 + ": " + Common.FRM_NAME_071 + ">";

            // 共通クラス
            this.cmn = cmn;

            // 画面初期化
            ClearForm();

            // サウンド設定初期化
            string baseDir = Directory.GetParent(Application.StartupPath).FullName;
            string soundDir = Path.Combine(baseDir, "sounds");
            ok = new SoundPlayer(Path.Combine(soundDir, "correct.wav"));
            ng = new SoundPlayer(Path.Combine(soundDir, "wrong.wav"));
            ok.Load();
            ng.Load();

            // 初期フォーカス
            txtQRCD.Focus();
        }

        // キーボードショートカット
        private void Frm071_ReceiptProc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        // 画面初期化
        private void ClearForm()
        {
            txtTKRNM.Text = string.Empty;
            txtHMCD.Text = string.Empty;
            txtHMNM.Text = string.Empty;
            txtHMRNM.Text = string.Empty;
            txtEDDT.Text = string.Empty;
            txtODRQTY.Text = string.Empty;
            txtJIQTY.Text = string.Empty;
            txtODRSTS.Text = string.Empty;

            lblResult.BackColor = Color.DarkGray;
            lblResult.ForeColor = Color.DimGray;
            lblResult.Text = "手配番号QRを\nスキャンして下さい";

            // 工程進捗状況のクリア
            ClearKTCD();

            toolStripStatusLabel1.Text = string.Empty;
        }

        // 工程進捗状況のクリア
        private void ClearKTCD()
        {
            for (int i = 1; i <= 6; i++)
            {
                Label lblKT = this.Controls.Find($"lblKT{i}", true).FirstOrDefault() as Label;
                if (lblKT != null)
                {
                    lblKT.Text = "";
                    lblKT.BackColor = SystemColors.Control;
                    lblKT.ForeColor = SystemColors.ControlText;
                }
                Label lblDT = this.Controls.Find($"lblDT{i}", true).FirstOrDefault() as Label;
                if (lblDT != null)
                {
                    lblDT.Text = "";
                    lblDT.BackColor = SystemColors.Control;
                    lblDT.ForeColor = SystemColors.ControlText;
                    lblDT.Tag = ""; // URLをクリア
                    lblDT.Cursor = Cursors.Default; // カーソルをデフォルトに戻す
                }
            }
        }

        // TagにURLを設定しておき、クリックイベントで処理する
        private void CustomeURLJump(object sender, EventArgs e)
        {
            Label lbl = (Label)sender; 
            string url = lbl.Tag as string;
            if (!string.IsNullOrEmpty(url))
            {
                Process.Start(url);
            }
        }

        // QRコードキーイベント
        private void txtQRCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;   // ポン防止
                string qrcd = txtQRCD.Text.Trim();
                if (!string.IsNullOrEmpty(qrcd))
                {
                    SearchQRCD(qrcd);
                }
            }
        }

        // QRコード検索
        // 手配情報を取得して表示する
        private void SearchQRCD(string qrcd)
        {
            DataTable kd8430 = new DataTable();
            bool ret = cmn.Dba.GetMpQRCD(ref kd8430, qrcd);
            if (ret && kd8430.Rows.Count == 1)
            {
                string tkrnm = kd8430.Rows[0]["TKRNM"].ToString();
                string hmcd = kd8430.Rows[0]["HMCD"].ToString();
                string hmnm = kd8430.Rows[0]["HMNM"].ToString();
                string hmrnm = kd8430.Rows[0]["HMRNM"].ToString();
                DateTime eddt = (DateTime)kd8430.Rows[0]["EDDT"];
                int odrqty = Convert.ToInt32(kd8430.Rows[0]["ODRQTY"].ToString());
                int jiqty = Convert.ToInt32(kd8430.Rows[0]["JIQTY"].ToString());
                string odrsts = kd8430.Rows[0]["ODRSTS"].ToString();
                string sts =
                    (odrsts == "1") ? "追加" :
                    (odrsts == "2") ? "確定" :
                    (odrsts == "3") ? "着手" :
                    (odrsts == "4") ? "完了" :
                    (odrsts == "9") ? "取消" : "";
                txtTKRNM.Text = tkrnm;
                txtHMCD.Text = hmcd;
                txtHMNM.Text = hmnm;
                txtHMRNM.Text = hmrnm;
                txtEDDT.Text = eddt.ToString("M/d");
                txtODRQTY.Text = odrqty.ToString();
                txtJIQTY.Text = jiqty.ToString();
                txtODRSTS.Text = sts;

                // 明細処理
                SearchQRCDDetail(qrcd);
            }
            else
            {
                ClearForm();
                lblResult.BackColor = Color.Red;
                lblResult.ForeColor = Color.Yellow;
                lblResult.Text = "手配なし";
                toolStripStatusLabel1.Text = "手配QRが見つかりませんでした．";
            }
            // QRコード入力欄にフォーカスを戻す
            txtQRCD.SelectAll();
            txtQRCD.Focus();
        }

        // 工程進捗状況を取得して表示する
        private void SearchQRCDDetail(string qrcd)
        {
            DataTable kd8450 = new DataTable();
            bool ret = cmn.Dba.GetMpQRDetail(ref kd8450, qrcd);
            if (!ret)
            {
                MessageBox.Show("異常が発生しました．\n情報システム課にお問い合わせください", "処理異常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool isOK = true;
            ClearKTCD();
            foreach (DataRow r in kd8450.Rows)
            {
                // 各種データを変数に格納
                int mpseq = Convert.ToInt32(r["MPSEQ"].ToString());
                int mcseq = Convert.ToInt32(r["MCSEQ"].ToString());
                string mcgcd = r["MCGCD"].ToString();
                string mccd = r["MCCD"].ToString();
                string mc = (int.TryParse(mccd, out _)) ? mcgcd + "-" + mccd : mccd;
                long repid = (r["REPID"] is DBNull)
                    ? 0
                    : Convert.ToInt64(r["REPID"]);

                // 実績日付を取得（WKEDDTはJIDTを兼任）
                DateTime? jidt = (r["WKEDDT"] is DBNull)
                    ? (DateTime?)null
                    : (DateTime)r["WKEDDT"];

                // 背景色を設定
                Color backcolor = Color.LightYellow;
                Color forecolor = Color.Black;
                if (mcgcd == "EX" || mcgcd == "MD" || mcgcd == "D")
                {
                    backcolor = Color.DarkGray;
                    forecolor = Color.DimGray;
                }
                else if (jidt is null)
                {
                    backcolor = Color.LightCoral;
                    forecolor = Color.Yellow;
                    isOK = false;
                }
                else
                {
                    backcolor = Color.PaleGreen;
                    forecolor = Color.RoyalBlue;
                }

                // 工程名を設定
                Label lblKT = this.Controls.Find($"lblKT{mpseq}", true).FirstOrDefault() as Label;
                if (lblKT != null)
                {
                    lblKT.Text = mc;
                    lblKT.BackColor = backcolor;
                    lblKT.ForeColor = forecolor;
                }

                // 実績日を設定
                Label lblDT = this.Controls.Find($"lblDT{mpseq}", true).FirstOrDefault() as Label;
                if (lblDT != null)
                {
                    lblDT.Text = (jidt == null) ? "-" : jidt.Value.ToString("M/d"); // nullの場合は"-"を表示
                    lblDT.BackColor = backcolor;
                    lblDT.ForeColor = forecolor;
                    if (mcgcd != "EX" && mcgcd != "MD" && mcgcd != "D")
                    {
                        lblDT.Tag = (repid != 0) ?
                            $"jp.co.cimtops.ireporter.openreport://repid={repid}" :
                            $"https://nabev2:53030/mp/order/{mcgcd}#jump{mcseq}";
                        lblDT.Cursor = Cursors.Hand;
                    }
                    else
                    {
                        lblDT.Tag = string.Empty;
                        lblDT.Cursor = Cursors.Default;
                    }
                }
            }

            // 最終判定結果
            if (isOK)
            {
                lblResult.BackColor = Color.LightGreen;
                lblResult.ForeColor = Color.Blue;
                lblResult.Text = "出荷可能";
                toolStripStatusLabel1.Text = "出荷可能です．";
                ok.Play(); // OK音
            }
            else
            {
                lblResult.BackColor = Color.Orange;
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "出荷停止！";
                toolStripStatusLabel1.Text = "チェックシートを確認してください．";
                ng.Play(); // NG音
            }

        }

        private void txtHMCD_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtHMCD.Text);
            toolStripStatusLabel1.Text = "品目コードをクリップボードにコピーしました．";
        }
    }
}
