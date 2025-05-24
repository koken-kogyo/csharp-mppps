using System;
using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm090_CutStore : Form
    {
        // 共通クラス
        private readonly Common cmn;

        public Frm090_CutStore(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            // [切削設備登録] ボタンからの起動のとき
            Text = " <" + Common.FRM_ID_090 + ": " + Common.FRM_NAME_090 + ">";

            // 共通クラス
            this.cmn = cmn;
        }

        private void Frm090_CutStore_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        // 計画出庫データ作成
        private void Btn_CutStoreDelv_Click(object sender, EventArgs e)
        {
            Frm091_CutStoreDelv frm091 = new Frm091_CutStoreDelv(cmn, sender);
            this.Hide();
            frm091.Show();
        }

        // 切削ストア在庫情報
        private void Btn_CutStoreInvInfo_Click(object sender, EventArgs e)
        {
            Frm092_CutStoreInvInfo frm092 = new Frm092_CutStoreInvInfo(cmn);
            this.Hide();
            frm092.Show();
        }

        // 閉じる
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void Frm090_CutStore_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Hide();
        }
        private void Frm090_CutStore_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        // フォームアクティブ時にマウスポインタの位置をトップボタンの中央にする
        private void Frm090_CutStore_Activated(object sender, EventArgs e)
        {
            // フォーム上のクライアント座標を、画面座標に変換する
            Point sp = this.PointToScreen(new Point(Btn_CutStoreDelv.Left, Btn_CutStoreDelv.Top));

            // マウスポインタの位置をトップボタンに設定
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(sp.X + 10,
                sp.Y + (Btn_CutStoreDelv.Height / 2));
        }

    }
}

