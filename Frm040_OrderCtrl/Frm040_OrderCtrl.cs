using System;
using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm040_OrderCtrl : Form
    {
        // 共通クラス
        private readonly Common cmn;

        /// <summary>
        /// デフォルト コンストラクター
        /// </summary>
        public Frm040_OrderCtrl(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            // [切削設備登録] ボタンからの起動のとき
            Text = " <" + Common.FRM_ID_040 + ": " + Common.FRM_NAME_040 + ">";

            // 共通クラス
            this.cmn = cmn;
        }

        private void Frm040_OrderCtrl_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        // 手配情報作成
        private void Btn_ImportOrder_Click(object sender, EventArgs e)
        {
            Frm041_ImportOrder frm041 = new Frm041_ImportOrder(cmn);
            this.Hide();
            frm041.Show();
        }

        // 手配情報表示
        private void Btn_InformationOrder_Click(object sender, EventArgs e)
        {
            Frm042_InformationOrder frm042 = new Frm042_InformationOrder(cmn);
            this.Hide();
            frm042.Show();
        }

        // 追加オーダーの作成
        private void Btn_CreateAddOrder_Click(object sender, EventArgs e)
        {
            Frm043_CreateAddOrder frm043 = new Frm043_CreateAddOrder();
            this.Hide();
            frm043.Show();
        }

        // 内示情報作成
        private void Btn_ImportPlan_Click(object sender, EventArgs e)
        {
            Frm044_ImportPlan frm044 = new Frm044_ImportPlan(cmn);
            this.Hide();
            frm044.Show();
        }

        // 内示情報表示
        private void Btn_InformationPlan_Click(object sender, EventArgs e)
        {
            Frm045_InformationPlan frm045 = new Frm045_InformationPlan();
            frm045.Show();
        }

        // 閉じる
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void Frm040_OrderCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Hide();
        }
        private void Frm040_OrderCtrl_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        // フォームアクティブ時にマウスポインタの位置をトップボタンの中央にする
        private void Frm040_OrderCtrl_Activated(object sender, EventArgs e)
        {
            // フォーム上のクライアント座標を、画面座標に変換する
            Point sp = this.PointToScreen(new Point(Btn_ImportOrder.Left, Btn_ImportOrder.Top));

            // マウスポインタの位置をトップボタンに設定
            //System.Windows.Forms.Cursor.Position = new System.Drawing.Point(sp.X + 10,
            //    sp.Y + (Btn_ImportOrder.Height / 2));

        }

    }
}
