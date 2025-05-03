using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm020_MainMenu : Form
    {
        private readonly Common cmn; // 共通クラス

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

            // フォームのアイコンを設定する
            this.Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            this.Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_020 + ": " + Common.FRM_NAME_020 + ">";

            // 共通クラスを取得
            form.GetCommonClass(ref cmn);

            // 利用者表示
            this.Lbl_UserName.Text = cmn.Ui.UserName + " (" + cmn.Ui.UserId + ")";

            // データベース関連情報をステータスストリップに表示
            toolStripStatusLabel1.Text = "OracleEM : " + cmn.DbCd[Common.DB_CONFIG_EM].Schema;
            toolStripStatusLabel1.Text += " / 内製 : " + cmn.DbCd[Common.DB_CONFIG_KK].Schema;
            toolStripStatusLabel2.Text = "MySQL切削 : " + cmn.DbCd[Common.DB_CONFIG_MP].Schema;

            // 生産計画結果保存先サーバーへ接続確認
            Task.Run(() => cmn.Fa.ConnectSaveServer());

        }

        /// <summary>
        /// [マスタ メンテナンス] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_MasterMaint_Click(object sender, EventArgs e)
        {
            Frm030_MasterMaint frm030 = new Frm030_MasterMaint(cmn);
            frm030.Top = this.Top;
            frm030.Left = this.Left;
            this.Hide();
            frm030.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// [オーダー管理] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_OrderCtrl_Click(object sender, EventArgs e)
        {
            Frm040_OrderCtrl frm040 = new Frm040_OrderCtrl(cmn);
            frm040.Top = this.Top;
            frm040.Left = this.Left;
            this.Hide();
            frm040.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// [製造管理] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_MfgCtrl_Click(object sender, EventArgs e)
        {
            Frm050_MfgCtrl frm050 = new Frm050_MfgCtrl(cmn);
            frm050.Top = this.Top;
            frm050.Left = this.Left;
            this.Hide();
            frm050.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// [実績管理] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_ReceiptCtrl_Click(object sender, EventArgs e)
        {
            Frm070_ReceiptCtrl frm070 = new Frm070_ReceiptCtrl();
            frm070.Top = this.Top;
            frm070.Left = this.Left;
            this.Hide();
            frm070.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// [材料管理] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_MatlCtrl_Click(object sender, EventArgs e)
        {
            Frm080_MatlCtrl frm080 = new Frm080_MatlCtrl();
            frm080.Top = this.Top;
            frm080.Left = this.Left;
            this.Hide();
            frm080.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// [切削ストア] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_CutStore_Click(object sender, EventArgs e)
        {
            Frm090_CutStore frm090 = new Frm090_CutStore(cmn, sender);
            frm090.Top = this.Top;
            frm090.Left = this.Left;
            this.Hide();
            frm090.ShowDialog();
            this.Show();
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

        private void Frm020_MainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }
    }
}