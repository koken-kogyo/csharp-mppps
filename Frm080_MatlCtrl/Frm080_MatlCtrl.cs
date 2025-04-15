using System;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm080_MatlCtrl : Form
    {
        public Frm080_MatlCtrl()
        {
            InitializeComponent();
        }

        // 材料在庫一覧
        private void Btn_MatlInvList_Click(object sender, EventArgs e)
        {
            Frm081_MatlInvList frm081 = new Frm081_MatlInvList();
            frm081.ShowDialog();
        }

        // 材料発注処理
        private void Btn_MatlOrder_Click(object sender, EventArgs e)
        {
            Frm082_MatlOrder frm082 = new Frm082_MatlOrder();
            frm082.ShowDialog();
        }

        // 材料検収
        private void Btn_MatlInsp_Click(object sender, EventArgs e)
        {
            Frm083_MatlInsp frm083 = new Frm083_MatlInsp();
            frm083.ShowDialog();
        }

        // 閉じる
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Frm080_MatlCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }
    }
}
