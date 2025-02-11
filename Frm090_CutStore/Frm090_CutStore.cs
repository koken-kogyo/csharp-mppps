using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm090_CutStore : Form
    {
        // 共通クラス
        private readonly Common cmn;

        public Frm090_CutStore(Common cmn, object sender)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            // [切削設備登録] ボタンからの起動のとき
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                        + " <" + Common.FRM_ID_040 + ": " + Common.FRM_NAME_040 + ">";

            // 共通クラス
            this.cmn = cmn;

        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_CutStoreDelv_Click(object sender, EventArgs e)
        {
            Frm091_CutStoreDelv frm091 = new Frm091_CutStoreDelv(cmn, sender);
            frm091.ShowDialog();
        }

        private void Btn_CutStoreInvInfo_Click(object sender, EventArgs e)
        {
            Frm092_CutStoreInvInfo frm092 = new Frm092_CutStoreInvInfo();
            frm092.ShowDialog();
        }

        private void Btn_CutStoreInvInfo_Click_1(object sender, EventArgs e)
        {

        }
    }
}
