using System;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm050_MfgCtrl : Form
    {
        // 共通クラス
        private readonly Common cmn;

        public Frm050_MfgCtrl(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            // [切削設備登録] ボタンからの起動のとき
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                        + " <" + Common.FRM_ID_050 + ": " + Common.FRM_NAME_050 + ">";

            // 共通クラス
            this.cmn = cmn;
        }

        private void Frm050_MfgCtrl_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        // 加工進捗情報表示
        private void Btn_MfgProgress_Click(object sender, EventArgs e)
        {
            Frm051_OrderDirection frm051 = new Frm051_OrderDirection();
            frm051.ShowDialog();
        }

        // 帳票出力
        private void Btn_Printing_Click(object sender, EventArgs e)
        {
            Frm052_FormsPrinting frm052 = new Frm052_FormsPrinting(cmn);
            frm052.ShowDialog();
        }

        // 閉じる
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Frm050_MfgCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

    }
}
