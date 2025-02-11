using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm020_MainMenu : Form
    {
        private readonly Common cmn; // ���ʃN���X
        public Common Common { get; set; }

        /// <summary>
        /// �f�t�H���g �R���X�g���N�^
        /// </summary>
        public Frm020_MainMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public Frm020_MainMenu(Interface form)
        {
            InitializeComponent();

            // �t�H�[���̃A�C�R����ݒ肷��
            this.Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // �t�H�[���̃^�C�g����ݒ肷��
            this.Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_020 + ": " + Common.FRM_NAME_020 + ">";

            // ���ʃN���X���擾
            form.GetCommonClass(ref cmn);

            // ���p�ҕ\��
            this.Lbl_UserName.Text = cmn.Ui.UserName + " (" + cmn.Ui.UserId + ")";

            // ���Y�v�挋�ʕۑ���T�[�o�[�֐ڑ�
            DateTime SW = DateTime.Now;
            Debug.WriteLine("[StopWatch] �J�n " + DateTime.Now.ToString("HH:mm:ss"));
            //Debug.WriteLine("[StopWatch] Initialize�I�� " + DateTime.Now.ToString("HH:mm:ss") + " (" + DateTime.Now.Subtract(SW).TotalSeconds.ToString("F3") + "�b)");
            //Debug.WriteLine("[StopWatch] GetCommonClass�I�� " + DateTime.Now.ToString("HH:mm:ss") + " (" + DateTime.Now.Subtract(SW).TotalSeconds.ToString("F3") + "�b)");

            Task.Run(() => cmn.Fa.ConnectSaveServer());

            Debug.WriteLine("[StopWatch] �I�� " + DateTime.Now.ToString("HH:mm:ss") + " (" + DateTime.Now.Subtract(SW).TotalSeconds.ToString("F3") + "�b)");
        }

        /// <summary>
        /// [�}�X�^ �����e�i���X] �{�^�� �N���b�N
        /// </summary>
        /// <param name="sender">���M�I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g����</param>
        private void Btn_MasterMaint_Click(object sender, EventArgs e)
        {
            Frm030_MasterMaint frm030 = new Frm030_MasterMaint(cmn, sender);
            frm030.ShowDialog(this);
        }

        /// <summary>
        /// [�I�[�_�[�Ǘ�] �{�^�� �N���b�N
        /// </summary>
        /// <param name="sender">���M�I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g����</param>
        private void Btn_OrderCtrl_Click(object sender, EventArgs e)
        {
            Frm040_OrderCtrl frm040 = new Frm040_OrderCtrl(cmn, sender);
            frm040.ShowDialog(this);
        }

        /// <summary>
        /// [�����Ǘ�] �{�^�� �N���b�N
        /// </summary>
        /// <param name="sender">���M�I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g����</param>
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_MfgCtrl_Click(object sender, EventArgs e)
        {
            Frm050_MfgCtrl frm050 = new Frm050_MfgCtrl();
            frm050.ShowDialog(this);
        }

        private void Btn_ReceiptCtrl_Click(object sender, EventArgs e)
        {
            Frm070_ReceiptCtrl frm070 = new Frm070_ReceiptCtrl();
            frm070.ShowDialog(this);
        }

        private void Btn_MatlCtrl_Click(object sender, EventArgs e)
        {
            Frm080_MatlCtrl frm080 = new Frm080_MatlCtrl();
            frm080.ShowDialog(this);
        }

        private void Btn_CutStore_Click(object sender, EventArgs e)
        {
            Frm090_CutStore frm090 = new Frm090_CutStore(cmn, sender);
            frm090.ShowDialog(this);
        }

        private void KM8400�؍퐶�Y�v��V�X�e�����p�҃}�X�^ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm031_CutProcUserMstMaint frm031 = new Frm031_CutProcUserMstMaint();
            frm031.ShowDialog(this);
        }

        private void KM8410�؍�n��}�X�^1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm032_ChipMstMaint frm032 = new Frm032_ChipMstMaint();
            frm032.ShowDialog();
        }

        private void KM8420�؍�ݔ��}�X�^2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm033_EqMstMaint frm033 = new Frm033_EqMstMaint();
            frm033.ShowDialog(this);
        }

        private void KM8430�؍�R�[�h�[�}�X�^3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm034_CodeSlipMstMaint frm034 = new Frm034_CodeSlipMstMaint();
            frm034.ShowDialog();
        }

        //private void �o�[�W�������VToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    Frm100_VerInfo frm100 = new Frm100_VerInfo();
        //    frm100.ShowDialog();
        //}
    }
}