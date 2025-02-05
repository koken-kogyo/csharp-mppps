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
    public partial class Frm050_MfgCtrl : Form
    {
        public Frm050_MfgCtrl()
        {
            InitializeComponent();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_CreateOrder_Click(object sender, EventArgs e)
        {
            Frm051_OrderDirection frm051 = new Frm051_OrderDirection();
            frm051.ShowDialog();
        }

        private void Btn_OrderEqualize_Click(object sender, EventArgs e)
        {
            Frm052_FormsPrinting frm052 = new Frm052_FormsPrinting();
            frm052.ShowDialog();
        }

        private void Btn_CreateOrder_Click_1(object sender, EventArgs e)
        {

        }

        private void Btn_OrderEqualize_Click_1(object sender, EventArgs e)
        {

        }
    }
}
