using System;
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
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                        + " <" + Common.FRM_ID_040 + ": " + Common.FRM_NAME_040 + ">";

            // 共通クラス
            this.cmn = cmn;
        }

        // 手配情報作成
        private void Btn_ImportOrder_Click(object sender, EventArgs e)
        {
            Frm041_ImportOrder frm041 = new Frm041_ImportOrder(cmn);
            frm041.ShowDialog();
        }

        // 手配情報表示
        private void Btn_InformationOrder_Click(object sender, EventArgs e)
        {
            Frm042_InformationOrder frm042 = new Frm042_InformationOrder(cmn);
            frm042.ShowDialog();
        }

        // 追加オーダーの作成
        private void Btn_CreateAddOrder_Click(object sender, EventArgs e)
        {
            Frm043_CreateAddOrder frm043 = new Frm043_CreateAddOrder();
            frm043.ShowDialog();
        }

        // 内示情報作成
        private void Btn_ImportPlan_Click(object sender, EventArgs e)
        {
            Frm044_ImportPlan frm044 = new Frm044_ImportPlan(cmn);
            frm044.ShowDialog();
        }

        // 内示情報表示
        private void Btn_InformationPlan_Click(object sender, EventArgs e)
        {
            Frm045_InformationPlan frm045 = new Frm045_InformationPlan();
            frm045.ShowDialog();
        }

        // 閉じる
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
