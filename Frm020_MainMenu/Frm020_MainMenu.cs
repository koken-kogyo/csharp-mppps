using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm020_MainMenu : Form
    {
        private readonly Common cmn; // 共通クラス

        // ポップアップ関連
        int waitTime = 1300;
        bool popupFlg30;
        bool popupFlg40;
        bool popupFlg50;
        bool popupFlg70;
        bool popupFlg80;
        bool popupFlg90;
        bool popupCancel = true;

        // サブフォーム
        Frm030_MasterMaint frm030;
        Frm040_OrderCtrl frm040;
        Frm050_MfgCtrl frm050;
        Frm070_ReceiptCtrl frm070;
        Frm080_MatlCtrl frm080;
        Frm090_CutStore frm090;

        /// <summary>
        /// デフォルト コンストラクタ
        /// </summary>
        public Frm020_MainMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Frm020_MainMenu(Interface form)
        {
            InitializeComponent();

            // ログインメニューだけはTopMost、それ以外は通常に戻す
            this.TopMost = false;

            // フォームのアイコンを設定する
            this.Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            this.Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_020 + ": " + Common.FRM_NAME_020 + ">";

            // 共通クラスを取得
            form.GetCommonClass(ref cmn);

            // サブフォーム
            frm030 = new Frm030_MasterMaint(cmn);
            frm040 = new Frm040_OrderCtrl(cmn);
            frm050 = new Frm050_MfgCtrl(cmn);
            frm070 = new Frm070_ReceiptCtrl();
            frm080 = new Frm080_MatlCtrl();
            frm090 = new Frm090_CutStore(cmn);

            // 利用者表示
            this.Lbl_UserName.Text = cmn.Ui.UserName + " (" + cmn.Ui.UserId + ")";

            // データベース関連情報をステータスストリップに表示
            toolStripStatusLabel1.Text = "OracleEM : " + cmn.DbCd[Common.DB_CONFIG_EM].Schema;
            toolStripStatusLabel1.Text += " / 内製 : " + cmn.DbCd[Common.DB_CONFIG_KK].Schema;
            toolStripStatusLabel2.Text = "MySQL切削 : " + cmn.DbCd[Common.DB_CONFIG_MP].Schema;

            // 生産計画結果保存先サーバーへ接続確認
            Task.Run(() => cmn.Fa.ConnectSaveServer());

            // フォーム起動直後のポップアップは禁止する
            Task.Run(async () =>
            {
                await Task.Delay(3000);
                // ポップアップ廃止 popupCancel = false; 
            });

        }

        /// <summary>
        /// [マスタ メンテナンス] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_MasterMaint_Click(object sender, EventArgs e)
        {
            frm030.Top = this.Top + this.Btn_MasterMaint.Top + 10;
            frm030.Left = this.Left + this.Btn_MasterMaint.Width * 2 / 3;
            subFormHide();
            popupCancel = true;
            frm030.Show();
        }

        /// <summary>
        /// [オーダー管理] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_OrderCtrl_Click(object sender, EventArgs e)
        {
            frm040.Top = this.Top + this.Btn_OrderCtrl.Top + 10;
            frm040.Left = this.Left + this.Btn_OrderCtrl.Width * 2 / 3;
            subFormHide();
            popupCancel = true;
            frm040.Show();
        }

        /// <summary>
        /// [製造管理] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_MfgCtrl_Click(object sender, EventArgs e)
        {
            frm050.Top = this.Top + this.Btn_MfgCtrl.Top + 10;
            frm050.Left = this.Left + this.Btn_MfgCtrl.Width * 2 / 3;
            subFormHide();
            popupCancel = true;
            frm050.Show();
        }

        /// <summary>
        /// [実績管理] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_ReceiptCtrl_Click(object sender, EventArgs e)
        {
            frm070.Top = this.Top + this.Btn_ReceiptCtrl.Top + 10;
            frm070.Left = this.Left + this.Btn_ReceiptCtrl.Width * 2 / 3;
            subFormHide();
            popupCancel = true;
            frm070.Show();
        }

        /// <summary>
        /// [材料管理] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_MatlCtrl_Click(object sender, EventArgs e)
        {
            frm080.Top = this.Top + this.Btn_MatlCtrl.Top + 10;
            frm080.Left = this.Left + this.Btn_MatlCtrl.Left + this.Btn_MatlCtrl.Width * 2 / 3;
            subFormHide();
            popupCancel = true;
            frm080.Show();
        }

        /// <summary>
        /// [切削ストア] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_CutStore_Click(object sender, EventArgs e)
        {
            frm090.Top = this.Top + this.Btn_CutStore.Top + 10;
            frm090.Left = this.Left + this.Btn_CutStore.Left + this.Btn_CutStore.Width * 2 / 3;
            subFormHide();
            popupCancel = true;
            frm090.Show();
        }

        /// <summary>
        /// [閉じる] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        // キーボードショートカット
        private void Frm020_MainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("終了しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) Close();
            }
            // Ctrl + 2 : 設備マスタ
            if (e.Control && (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2))
            {
                Frm033_EqMstMaint frm033 = new Frm033_EqMstMaint(cmn);
                frm033.Show();
            }
            // Ctrl + 3 : コード票マスタ
            if (e.Control && (e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.D3))
            {
                Frm034_CodeSlipMstMaint frm034 = new Frm034_CodeSlipMstMaint(cmn);
                frm034.Show();
            }
            if (e.Control && e.KeyCode == Keys.S) 保存先設定SToolStripMenuItem_Click(sender, e);
            if (e.Control && e.KeyCode == Keys.H) バージョン情報VToolStripMenuItem_Click(sender, e);

        }

        private void subFormHide()
        {
            frm030.Hide();
            frm040.Hide();
            frm050.Hide();
            frm070.Hide();
            frm080.Hide();
            frm090.Hide();
            subFormCancel();
        }

        private void subFormCancel()
        {
            popupFlg30 = false;
            popupFlg40 = false;
            popupFlg50 = false;
            popupFlg70 = false;
            popupFlg80 = false;
            popupFlg90 = false;
        }

        private bool isSubFormVisible()
        {
            return (frm030.Visible || frm040.Visible || frm050.Visible || frm070.Visible || frm080.Visible || frm090.Visible);
        }

        // ボタン以外にマウスが移動したらポップアップさせない
        private void Frm020_MainMenu_MouseEnter(object sender, EventArgs e)
        {
            if (popupCancel) return;
            subFormCancel();
        }

        private async void Btn_MasterMaint_MouseEnter(object sender, EventArgs e)
        {
            if (popupCancel) return;
            if (popupCancel && isSubFormVisible()) return; else popupCancel = false;
            subFormCancel();
            popupFlg30 = true;
            await Task.Delay(waitTime);
            if (!popupFlg30) return;
            subFormHide();
            frm030.Top = this.Top + this.Btn_MasterMaint.Top + 10;
            frm030.Left = this.Left + this.Btn_MasterMaint.Width * 2 / 3; ;
            if (frm030.IsDisposed) return;
            if (!frm030.Visible) frm030.Show();
        }

        private async void Btn_OrderCtrl_MouseEnter(object sender, EventArgs e)
        {
            if (popupCancel) return;
            if (popupCancel && isSubFormVisible()) return; else popupCancel = false;
            subFormCancel();
            popupFlg40 = true;
            await Task.Delay(waitTime);
            if (!popupFlg40) return;
            subFormHide();
            frm040.Top = this.Top + this.Btn_OrderCtrl.Top + 10;
            frm040.Left = this.Left + this.Btn_OrderCtrl.Width * 2 / 3; ;
            if (frm040.IsDisposed) return;
            if (!frm040.Visible) frm040.Show();
        }

        private async void Btn_MfgCtrl_MouseEnter(object sender, EventArgs e)
        {
            if (popupCancel) return;
            if (popupCancel && isSubFormVisible()) return; else popupCancel = false;
            subFormCancel();
            popupFlg50 = true;
            await Task.Delay(waitTime);
            if (!popupFlg50) return;
            subFormHide();
            frm050.Top = this.Top + this.Btn_MfgCtrl.Top + 10;
            frm050.Left = this.Left + this.Btn_MfgCtrl.Width * 2 / 3; ;
            if (frm050.IsDisposed) return;
            if (!frm050.Visible) frm050.Show();
        }

        private async void Btn_ReceiptCtrl_MouseEnter(object sender, EventArgs e)
        {
            if (popupCancel) return;
            if (popupCancel && isSubFormVisible()) return; else popupCancel = false;
            subFormCancel();
            popupFlg70 = true;
            await Task.Delay(waitTime);
            if (!popupFlg70) return;
            subFormHide();
            frm070.Top = this.Top + this.Btn_ReceiptCtrl.Top + 10;
            frm070.Left = this.Left + this.Btn_ReceiptCtrl.Width * 2 / 3; ;
            if (frm070.IsDisposed) return;
            if (!frm070.Visible) frm070.Show();
        }

        private async void Btn_MatlCtrl_MouseEnter(object sender, EventArgs e)
        {
            if (popupCancel) return;
            if (popupCancel && isSubFormVisible()) return; else popupCancel = false;
            subFormCancel();
            popupFlg80 = true;
            await Task.Delay(waitTime);
            if (!popupFlg80) return;
            subFormHide();
            frm080.Top = this.Top + this.Btn_MatlCtrl.Top + 10;
            frm080.Left = this.Left + this.Btn_MatlCtrl.Left + this.Btn_MatlCtrl.Width * 2 / 3;
            if (frm080.IsDisposed) return;
            if (!frm080.Visible) frm080.Show();
        }

        private async void Btn_CutStore_MouseEnter(object sender, EventArgs e)
        {
            if (popupCancel) return;
            if (popupCancel && isSubFormVisible()) return; else popupCancel = false;
            subFormCancel();
            popupFlg90 = true;
            await Task.Delay(waitTime);
            if (!popupFlg90) return;
            subFormHide();
            frm090.Top = this.Top + this.Btn_CutStore.Top + 10;
            frm090.Left = this.Left + this.Btn_CutStore.Left + this.Btn_CutStore.Width * 2 / 3;
            if (frm090.IsDisposed) return;
            if (!frm090.Visible) frm090.Show();
        }

        private void 保存先設定SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm020_FileSettings frm020 = new Frm020_FileSettings();
            frm020.ShowDialog();
        }

        private void バージョン情報VToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm100_VerInfo frm100 = new Frm100_VerInfo(cmn);
            frm100.ShowDialog();
        }

        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Lbl_UserName_Click(object sender, EventArgs e)
        {
            subFormHide();
        }

        private void Frm020_MainMenu_Click(object sender, EventArgs e)
        {
            subFormHide();
        }

        private void Lbl_User_Click(object sender, EventArgs e)
        {
            subFormHide();
        }
    }
}