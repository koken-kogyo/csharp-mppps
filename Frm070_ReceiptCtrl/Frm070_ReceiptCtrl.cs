using System;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm070_ReceiptCtrl : Form
    {
        public Frm070_ReceiptCtrl()
        {
            InitializeComponent();
        }

        private void Frm070_ReceiptCtrl_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        // 切削ストア受入実績処理
        private void Btn_ReceiptProc_Click(object sender, EventArgs e)
        {
            Frm071_ReceiptProc frm071 = new Frm071_ReceiptProc();
            frm071.Show();
        }

        // 切削ストア受入実績情報表示
        private void Btn_ReceiptInfo_Click(object sender, EventArgs e)
        {
            Frm072_ReceiptInfo frm072 = new Frm072_ReceiptInfo();
            frm072.Show();
        }

        // EM への実績入力
        private void Btn_EntryShipResults_Click(object sender, EventArgs e)
        {
            Frm073_EntryShipRes frm073 = new Frm073_EntryShipRes();
            frm073.Show();
        }

        // 閉じる
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Frm070_ReceiptCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Hide();
        }

    }
}
