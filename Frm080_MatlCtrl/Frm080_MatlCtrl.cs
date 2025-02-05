using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm080_MatlCtrl : Form
    {
        public Frm080_MatlCtrl()
        {
            InitializeComponent();
        }
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_MatlInvList_Click(object sender, EventArgs e)
        {
            Frm081_MatlInvList frm081 = new Frm081_MatlInvList();
            frm081.ShowDialog();
        }

        private void Btn_MatlOrder_Click(object sender, EventArgs e)
        {
            Frm082_MatlOrder frm082 = new Frm082_MatlOrder();
            frm082.ShowDialog();
        }

        private void Btn_MatlInsp_Click(object sender, EventArgs e)
        {
            Frm083_MatlInsp frm083 = new Frm083_MatlInsp();
            frm083.ShowDialog();
        }

        private void Btn_MatlInvList_Click_1(object sender, EventArgs e)
        {

        }

        private void Btn_MatlOrder_Click_1(object sender, EventArgs e)
        {

        }

        private void Btn_MatlInsp_Click_1(object sender, EventArgs e)
        {

        }
    }
}
