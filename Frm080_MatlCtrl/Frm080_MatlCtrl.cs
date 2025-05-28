using System;
using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm080_MatlCtrl : Form
    {
        public Frm080_MatlCtrl()
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            // [切削設備登録] ボタンからの起動のとき
            Text = " <" + Common.FRM_ID_080 + ": " + Common.FRM_NAME_080 + ">";

        }

        private void Frm080_MatlCtrl_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        // 材料在庫一覧
        private void Btn_MatlInvList_Click(object sender, EventArgs e)
        {
            Frm081_MatlInvList frm081 = new Frm081_MatlInvList();
            frm081.Show();
        }

        // 材料発注処理
        private void Btn_MatlOrder_Click(object sender, EventArgs e)
        {
            Frm082_MatlOrder frm082 = new Frm082_MatlOrder();
            frm082.Show();
        }

        // 材料検収
        private void Btn_MatlInsp_Click(object sender, EventArgs e)
        {
            Frm083_MatlInsp frm083 = new Frm083_MatlInsp();
            frm083.Show();
        }

        // 閉じる
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void Frm080_MatlCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Hide();
        }
        private void Frm080_MatlCtrl_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        // フォームアクティブ時にマウスポインタの位置をトップボタンの中央にする
        private void Frm080_MatlCtrl_Activated(object sender, EventArgs e)
        {
            // フォーム上のクライアント座標を、画面座標に変換する
            Point sp = this.PointToScreen(new Point(Btn_MatlInvList.Left, Btn_MatlInvList.Top));

            // マウスポインタの位置をトップボタンに設定
            //System.Windows.Forms.Cursor.Position = new System.Drawing.Point(sp.X + 10,
            //    sp.Y + (Btn_MatlInvList.Height / 2));
        }

    }
}
