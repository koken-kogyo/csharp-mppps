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
    public partial class Frm070_ReceiptCtrl : Form
    {
        public Frm070_ReceiptCtrl()
        {
            InitializeComponent();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_ReceiptProc_Click(object sender, EventArgs e)
        {
            Frm071_ReceiptProc frm071 = new Frm071_ReceiptProc();
            frm071.ShowDialog();
        }

        private void Btn_ReceiptInfo_Click(object sender, EventArgs e)
        {
            Frm072_ReceiptInfo frm072 = new Frm072_ReceiptInfo();
            frm072.ShowDialog();
        }

        private void Btn_EntryShipResults_Click(object sender, EventArgs e)
        {
            Frm073_EntryShipRes frm073 = new Frm073_EntryShipRes();
            frm073.ShowDialog();
        }

        private void Btn_ReceiptProc_Click_1(object sender, EventArgs e)
        {

        }

        private void Btn_ReceiptInfo_Click_1(object sender, EventArgs e)
        {

        }

        private void Btn_EntryShipResults_Click_1(object sender, EventArgs e)
        {

        }
    }
}
