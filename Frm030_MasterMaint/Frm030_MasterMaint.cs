using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm030_MasterMaint : Form
    {
        // 共通クラス
        private readonly Common cmn;

        // DataGridView エラーリスト生成
        List<DgvError> dgvErrors = new List<DgvError>();

        /// <summary>
        /// デフォルト コンストラクター
        /// </summary>
        public Frm030_MasterMaint()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        public Frm030_MasterMaint(Common cmn, object sender)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            // [切削設備登録] ボタンからの起動のとき
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                        + " <" + Common.FRM_ID_030 + ": " + Common.FRM_NAME_030 + ">";

            // 共通クラス
            this.cmn = cmn;

            // 初期設定
            SetInitialValues();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        //★private void Btn_CutProcUserMstMaint_Click(object sender, EventArgs e)
        //★{
        //★    Frm031_CutProcUserMstMaint frm031 = new Frm031_CutProcUserMstMaint();
        //★    frm031.ShowDialog();
        //★}
        private void Btn_ChipMstMaint_Click(object sender, EventArgs e)
        {
            Frm032_ChipMstMaint frm032 = new Frm032_ChipMstMaint();
            frm032.ShowDialog();
        }
        private void Btn_EqMstMaint_Click(object sender, EventArgs e)
        {
            Frm033_EqMstMaint frm033 = new Frm033_EqMstMaint();
            frm033.ShowDialog();
        }
        private void Btn_CodeSlipMstMaint_Click(object sender, EventArgs e)
        {
            Frm034_CodeSlipMstMaint frm034 = new Frm034_CodeSlipMstMaint();
            frm034.ShowDialog();
        }
        ///
        /// 共通： 処理
        /// 

        /// <summary>
        /// 初期値設定
        /// </summary>
        private void SetInitialValues()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            //// コントロール状態設定
            //if ((cmn.RegForm != Common.REG_FORM_SINGLE_SHEET) && (cmn.RegForm != Common.REG_FORM_LIST)) // 起動直後
            //{
            //    // 前回終了時の選択状態を復元
            //    LoadFormInfo();
            //}
            //else // 形式選択中
            //{
            //    // 何もしない
            //}
            //// 選択中の形式に応じて設定
            //cmn.SetControlStatus(this, cmn.RegForm);

            //// 登録済日付を選択
            //Rbt_RegDt.Checked = true;

            //// 適用年月日のコントロール状態を上書き
            //SetApplicableDateStatus();

            //// 手配先情報取得
            //cmn.IkM0300 = new IkM0300();
            //cmn.IkM0300.OdCd = "%";
            //cmn.IkM0300.IOKbn = Common.M0300_IOKBN_INHOUSE;
            //cmn.IkM0300.IsInclusionSubCo = true;
            //cmn.IkM0300.IsTiedToKt = true;

            //// 検索してコンボ ボックスに追加
            //DataSet dataSet = new DataSet();
            //cmn.Dba.GetOrderInfo(ref dataSet);

            //// 手配先コードと手配先略称を並べて追加
            //Cbx_Od.BeginUpdate();
            //foreach (DataRow row in dataSet.Tables[0].Rows)
            //{
            //    int rowIndex = dataSet.Tables[0].Rows.IndexOf(row);
            //    string od = dataSet.Tables[0].Rows[rowIndex].ItemArray[0] + " " + dataSet.Tables[0].Rows[rowIndex].ItemArray[3];
            //    Cbx_Od.Items.Add(od);
            //}
            //Cbx_Od.EndUpdate();

            //// 更新系ボタン有効無効再設定
            //SetEnableDisableUpdatingButtons();

            //// 件数表示クリア
            //Tsl_Msg.Text = null;
        }


    }
}
