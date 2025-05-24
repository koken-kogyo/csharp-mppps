using System;
using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm070_ReceiptCtrl : Form
    {
        public Frm070_ReceiptCtrl()
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            // [切削設備登録] ボタンからの起動のとき
            Text = " <" + Common.FRM_ID_070 + ": " + Common.FRM_NAME_070 + ">";

        }

        private void Frm070_ReceiptCtrl_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        // 切削ストア受入実績処理
        private void Btn_ReceiptProc_Click(object sender, EventArgs e)
        {
            Frm071_ReceiptProc frm071 = new Frm071_ReceiptProc();
            frm071.Show();
        }

        // 切削ストア受入実績情報表示
        private void Btn_ReceiptInfo_Click(object sender, EventArgs e)
        {
            Frm072_ReceiptInfo frm072 = new Frm072_ReceiptInfo();
            frm072.Show();
        }

        // EM への実績入力
        private void Btn_EntryShipResults_Click(object sender, EventArgs e)
        {
            Frm073_EntryShipRes frm073 = new Frm073_EntryShipRes();
            frm073.Show();
        }

        // 閉じる
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Frm070_ReceiptCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Hide();
        }
        private void Frm070_ReceiptCtrl_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        // フォームアクティブ時にマウスポインタの位置をトップボタンの中央にする
        private void Frm070_ReceiptCtrl_Activated(object sender, EventArgs e)
        {
            // フォーム上のクライアント座標を、画面座標に変換する
            Point sp = this.PointToScreen(new Point(Btn_ReceiptProc.Left, Btn_ReceiptProc.Top));

            // マウスポインタの位置をトップボタンに設定
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(sp.X + 10,
                sp.Y + (Btn_ReceiptProc.Height / 2));
        }

    }
}
