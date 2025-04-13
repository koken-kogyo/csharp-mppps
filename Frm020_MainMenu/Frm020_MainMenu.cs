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
            Frm030_MasterMaint frm030 = new Frm030_MasterMaint(cmn, sender);
            frm030.StartPosition = FormStartPosition.CenterParent;
            frm030.ShowDialog(this);
        }

        /// <summary>
        /// [オーダー管理] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_OrderCtrl_Click(object sender, EventArgs e)
        {
            Frm040_OrderCtrl frm040 = new Frm040_OrderCtrl(cmn);
            frm040.StartPosition = FormStartPosition.CenterParent;
            frm040.ShowDialog(this);
        }

        private void Btn_MfgCtrl_Click(object sender, EventArgs e)
        {
            Frm050_MfgCtrl frm050 = new Frm050_MfgCtrl(cmn);
            frm050.StartPosition = FormStartPosition.CenterParent;
            frm050.ShowDialog(this);
        }

        private void Btn_ReceiptCtrl_Click(object sender, EventArgs e)
        {
            Frm070_ReceiptCtrl frm070 = new Frm070_ReceiptCtrl();
            frm070.StartPosition = FormStartPosition.CenterParent;
            frm070.ShowDialog(this);
        }

        private void Btn_MatlCtrl_Click(object sender, EventArgs e)
        {
            Frm080_MatlCtrl frm080 = new Frm080_MatlCtrl();
            frm080.StartPosition = FormStartPosition.CenterParent;
            frm080.ShowDialog(this);
        }

        private void Btn_CutStore_Click(object sender, EventArgs e)
        {
            Frm090_CutStore frm090 = new Frm090_CutStore(cmn, sender);
            frm090.StartPosition = FormStartPosition.CenterParent;
            frm090.ShowDialog(this);
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
    }
}