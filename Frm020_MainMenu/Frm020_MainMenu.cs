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
        private readonly Common cmn; // 共通クラス
        public Common Common { get; set; }

        /// <summary>
        /// デフォルト コンストラクタ
        /// </summary>
        public Frm020_MainMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Frm020_MainMenu(Interface form)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            this.Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            this.Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_020 + ": " + Common.FRM_NAME_020 + ">";

            // 共通クラスを取得
            form.GetCommonClass(ref cmn);

            // 利用者表示
            this.Lbl_UserName.Text = cmn.Ui.UserName + " (" + cmn.Ui.UserId + ")";

            // 生産計画結果保存先サーバーへ接続
            DateTime SW = DateTime.Now;
            Debug.WriteLine("[StopWatch] 開始 " + DateTime.Now.ToString("HH:mm:ss"));
            //Debug.WriteLine("[StopWatch] Initialize終了 " + DateTime.Now.ToString("HH:mm:ss") + " (" + DateTime.Now.Subtract(SW).TotalSeconds.ToString("F3") + "秒)");
            //Debug.WriteLine("[StopWatch] GetCommonClass終了 " + DateTime.Now.ToString("HH:mm:ss") + " (" + DateTime.Now.Subtract(SW).TotalSeconds.ToString("F3") + "秒)");

            Task.Run(() => cmn.Fa.ConnectSaveServer());

            Debug.WriteLine("[StopWatch] 終了 " + DateTime.Now.ToString("HH:mm:ss") + " (" + DateTime.Now.Subtract(SW).TotalSeconds.ToString("F3") + "秒)");
        }

        /// <summary>
        /// [マスタ メンテナンス] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_MasterMaint_Click(object sender, EventArgs e)
        {
            Frm030_MasterMaint frm030 = new Frm030_MasterMaint(cmn, sender);
            frm030.ShowDialog(this);
        }

        /// <summary>
        /// [オーダー管理] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_OrderCtrl_Click(object sender, EventArgs e)
        {
            Frm040_OrderCtrl frm040 = new Frm040_OrderCtrl(cmn, sender);
            frm040.ShowDialog(this);
        }

        /// <summary>
        /// [製造管理] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
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

        private void KM8400切削生産計画システム利用者マスタToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm031_CutProcUserMstMaint frm031 = new Frm031_CutProcUserMstMaint();
            frm031.ShowDialog(this);
        }

        private void KM8410切削刃具マスタ1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm032_ChipMstMaint frm032 = new Frm032_ChipMstMaint();
            frm032.ShowDialog();
        }

        private void KM8420切削設備マスタ2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm033_EqMstMaint frm033 = new Frm033_EqMstMaint();
            frm033.ShowDialog(this);
        }

        private void KM8430切削コード票マスタ3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm034_CodeSlipMstMaint frm034 = new Frm034_CodeSlipMstMaint();
            frm034.ShowDialog();
        }

        //private void バージョン情報VToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    Frm100_VerInfo frm100 = new Frm100_VerInfo();
        //    frm100.ShowDialog();
        //}
    }
}