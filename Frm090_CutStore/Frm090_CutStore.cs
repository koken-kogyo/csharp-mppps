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
    public partial class Frm090_CutStore : Form
    {
        public Frm090_CutStore()
        {
            InitializeComponent();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_CutStoreDelv_Click(object sender, EventArgs e)
        {
            Frm091_CutStoreDelv frm091 = new Frm091_CutStoreDelv();
            frm091.ShowDialog();
        }

        private void Btn_CutStoreInvInfo_Click(object sender, EventArgs e)
        {
            Frm092_CutStoreInvInfo frm092 = new Frm092_CutStoreInvInfo();
            frm092.ShowDialog();
        }

        private void Btn_CutStoreDelv_Click_1(object sender, EventArgs e)
        {

        }

        private void Btn_CutStoreInvInfo_Click_1(object sender, EventArgs e)
        {

        }
    }
}
