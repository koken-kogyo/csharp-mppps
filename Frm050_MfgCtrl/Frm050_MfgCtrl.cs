using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
            Text = " <" + Common.FRM_ID_050 + ": " + Common.FRM_NAME_050 + ">";

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
            frm051.Show();
        }

        private async void btn_All_Print_Click(object sender, EventArgs e)
        {
            Frm052_PrintSettings frm052 = new Frm052_PrintSettings(cmn);
            frm052.Show();
        }

        // 閉じる
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void Frm050_MfgCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Hide();
        }
        private void Frm050_MfgCtrl_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        // フォームアクティブ時にマウスポインタの位置をトップボタンの中央にする
        private void Frm050_MfgCtrl_Activated(object sender, EventArgs e)
        {
            // フォーム上のクライアント座標を、画面座標に変換する
            Point sp = this.PointToScreen(new Point(Btn_MfgProgress.Left, Btn_MfgProgress.Top));

            // マウスポインタの位置をトップボタンに設定
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(sp.X + 10,
                sp.Y + (Btn_MfgProgress.Height / 2));
        }

    }
}
