using System;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm030_MasterMaint : Form
    {
        // 共通クラス
        private readonly Common cmn;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public Frm030_MasterMaint(Common cmn)
        {

            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            // [切削設備登録] ボタンからの起動のとき
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                        + " <" + Common.FRM_ID_030 + ": " + Common.FRM_NAME_030 + ">";

            // 共通クラス
            this.cmn = cmn;
        }

        // 初期ロード後に画面をアクティブ化
        private void Frm030_MasterMaint_Load(object sender, EventArgs e)
        {
            // 一時的に最前面に表示しフォーカスを奪ったりと苦労したけど
            // ロートイベントに１行記述すれば済んだ
            // （コンストラクタで色々試していたけど全然効かなかった・・・）
            //this.TopMost = true;
            //this.TopMost = false;
            this.Activate();
        }

        // KM8400 : 利用者マスタメンテ
        private void Btn_CutProcUserMstMaint_Click(object sender, EventArgs e)
        {
            Frm031_CutProcUserMstMaint frm031 = new Frm031_CutProcUserMstMaint();
            frm031.ShowDialog();
        }

        // KM8410 : 刃具マスタメンテ
        private void Btn_ChipMstMaint_Click(object sender, EventArgs e)
        {
            Frm032_ChipMstMaint frm032 = new Frm032_ChipMstMaint();
            frm032.ShowDialog();
        }

        // KM8420 : 設備マスタメンテ
        private void Btn_EqMstMaint_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Frm033_EqMstMaint frm033 = new Frm033_EqMstMaint(cmn);
            frm033.ShowDialog();
            this.WindowState = FormWindowState.Normal;
        }

        // KM8430 : コード票マスタメンテ
        private void Btn_CodeSlipMstMaint_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Frm034_CodeSlipMstMaint frm034 = new Frm034_CodeSlipMstMaint(cmn);
            frm034.ShowDialog();
            this.WindowState = FormWindowState.Normal;
        }

        // 閉じる
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Frm030_MasterMaint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

    }
}
