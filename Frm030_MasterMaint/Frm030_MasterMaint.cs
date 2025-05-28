using System;
using System.Drawing;
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
            Text = " <" + Common.FRM_ID_030 + ": " + Common.FRM_NAME_030 + ">";

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

        // KM8400 : 利用者マスタメンテ起動
        private void Btn_CutProcUserMstMaint_Click(object sender, EventArgs e)
        {
            Frm031_CutProcUserMstMaint frm031 = new Frm031_CutProcUserMstMaint();
            this.Hide();
            frm031.Show();
        }

        // KM8410 : 刃具マスタメンテ起動
        private void Btn_ChipMstMaint_Click(object sender, EventArgs e)
        {
            Frm032_ChipMstMaint frm032 = new Frm032_ChipMstMaint();
            this.Hide();
            frm032.Show();
        }

        // KM8420 : 設備マスタメンテ起動
        private void Btn_EqMstMaint_Click(object sender, EventArgs e)
        {
            Frm033_EqMstMaint frm033 = new Frm033_EqMstMaint(cmn);
            this.Hide();
            frm033.Show();
        }

        // KM8430 : コード票マスタメンテ起動
        private void Btn_CodeSlipMstMaint_Click(object sender, EventArgs e)
        {
            Frm034_CodeSlipMstMaint frm034 = new Frm034_CodeSlipMstMaint(cmn);
            this.Hide();
            frm034.Show();
        }

        // 閉じる
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void Frm030_MasterMaint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Hide();
        }
        private void Frm030_MasterMaint_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        // フォームアクティブ時にマウスポインタの位置をトップボタンの中央にする
        private void Frm030_MasterMaint_Activated(object sender, EventArgs e)
        {
            // フォーム上のクライアント座標を、画面座標に変換する
            Point sp = this.PointToScreen(new Point(Btn_EqMstMaint.Left, Btn_EqMstMaint.Top));

            // マウスポインタの位置をトップボタンに設定
            //System.Windows.Forms.Cursor.Position = new System.Drawing.Point(sp.X + 10, 
            //    sp.Y + (Btn_EqMstMaint.Height / 2));
        }

    }
}
