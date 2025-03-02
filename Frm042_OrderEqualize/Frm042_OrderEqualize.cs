using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Media;
using System.Reflection;
using System.Windows.Forms;
using System.Media;


namespace MPPPS
{
    public partial class Frm042_OrderEqualize : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private SoundPlayer player;

        // DataGridView エラーリスト生成
        List<DgvError> dgvErrors = new List<DgvError>();

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="common">共通クラス</param>
        /// <param name="sender">送信オジェクト</param>
        public Frm042_OrderEqualize(Common cmn, object sender)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_042 + ": " + Common.FRM_NAME_042 + ">";

            // 共通クラス
            this.cmn = cmn;

            // 初期設定
            SetInitialValues();
        }

        ///
        /// 共通
        /// 

        ///
        /// 共通： フォーム制御
        /// 

        /// <summary>
        /// フォームが閉じられるときのイベント処理
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Frm042_OrderEqualize_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // フォーム情報保存
            SaveFormInfo();
        }

        /// <summary>
        /// タブ コントロール選択変更時のイベント ハンドラー
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Tbc_FormType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 新しく選択されたタブを判定
            if (Tbc_FormType.SelectedIndex == Common.TBC1_INDEX_LIST) // リスト形式
            {
                // リスト形式
                cmn.RegForm = Common.REG_FORM_LIST;
                // 選択時のイベントで処理する

                cmn.SetControlStatus(this, Common.REG_FORM_LIST);

                // 更新系ボタン有効無効再設定
                SetEnableDisableUpdatingButtons();
            }
            else // それ以外
            {
                //　単票形式
                cmn.RegForm = Common.REG_FORM_SINGLE_SHEET;

                // コントロール状態設定
                cmn.SetControlStatus(this, cmn.RegForm);
            }
        }

        /// <summary>
        /// [クリア] ボタン クリック時のイベント ハンドラー ★
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            cmn.ButtonText = sender.ToString();

            // iPhone 着信音を止める
            player.Stop();
            player.Dispose();

            // 件数表示クリア
            Tsl_Msg.Text = null;

            // フォームの全コントロールをクリア
            cmn.ClearAllControls(this);

            // 初期値設定
            SetInitialValues();
        }

        /// <summary>
        /// [検索] ボタン クリック時のイベント ハンドラー
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            cmn.ButtonText = sender.ToString();

            // iPhone 着信音を鳴らす
            player = new SoundPlayer(@cmn.SoundFilePathOpening);
            player.Play();

            // 件数表示クリア
            Tsl_Msg.Text = null;

            // リスト形式データ取得
            GetCuttingProdPlanDatafromList(Common.MSG_ALERT_YES);
            GetCuttingProdScheduleDatafromList(Common.MSG_ALERT_YES);

            // 音を止める
            player.Stop();
            player.Dispose();
        }

        /// <summary>
        /// [登録/更新] ボタン クリック時のイベント ハンドラー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_InsUpd_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            cmn.ButtonText = sender.ToString();

            // 件数表示クリア
            Tsl_Msg.Text = null;

            // 再描画
            Dgv_MpSimOdrTbl.Refresh();

            // リスト形式データ併合
            MergeCuttingProdInfoList();
        }

        /// <summary>
        /// [削除] ボタン クリック時のイベント ハンドラー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            cmn.ButtonText = sender.ToString();

            // 件数表示クリア
            Tsl_Msg.Text = null;

            // リスト形式データ削除
            DeleteCuttingProdInfoList();
        }

        /// <summary>
        /// [閉じる] ボタン クリック時のイベント ハンドラー
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            cmn.ButtonText = sender.ToString();

            // 音を止める
            player.Stop();
            player.Dispose();

            // フォームを閉じる
            Close();
        }

        /// <summary>
        /// DataGridView 行コピー & ペースト時のイベント ハンドラー
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Dgv_MpSimOdrTbl_KeyUp(object sender, KeyEventArgs e)
        {
            ClipboardUtils.OnDataGridViewPaste(sender, e);
        }


        /////
        ///// 共通: 処理
        /////

        /// <summary>
        /// 初期値設定
        /// </summary>
        private void SetInitialValues()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // コントロール状態設定
            if ((cmn.RegForm != Common.REG_FORM_SINGLE_SHEET) && (cmn.RegForm != Common.REG_FORM_LIST)) // 起動直後
            {
                // 前回終了時の選択状態を復元
                LoadFormInfo();
            }
            else // 形式選択中
            {
                // 何もしない
            }
            // 選択中の形式に応じて設定
            cmn.SetControlStatus(this, cmn.RegForm);

            // 後続コントロール初期化
            InitSubseqCtrls((int)Common.Frm042InqKeyIdx.EdDt);

            // 切削オーダー平準化情報取得
            cmn.IkKD8430 = new IkKD8430();
            cmn.IkKD8440 = new IkKD8440();

            // 指定日時で検索
            cmn.IkKD8430.EdDt = Convert.ToDateTime(Dtp_EdDt.Value.ToShortDateString());
            cmn.IkKD8430.McGCd = "";
            cmn.IkKD8430.McCd = "";

            cmn.IkKD8440.EdDt = cmn.IkKD8430.EdDt;
            cmn.IkKD8440.McGCd = cmn.IkKD8430.McGCd;
            cmn.IkKD8440.McCd = cmn.IkKD8430.McCd;


            // 検索してコンボ ボックスに追加
            DataSet dataSet = new DataSet();
            if (cmn.Dba.GetOrderEqualizeInfo(ref dataSet, Common.KD8430_TARGET_MCGCD) > 0)
            {
                // グループ コードにグループ コード＋グループ名称を追加
                Cbx_McGCd.BeginUpdate();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    int rowIndex = dataSet.Tables[0].Rows.IndexOf(row);
                    string mcGCd = dataSet.Tables[0].Rows[rowIndex].ItemArray[0].ToString() + ": " + dataSet.Tables[0].Rows[rowIndex].ItemArray[1].ToString();
                    Cbx_McGCd.Items.Add(mcGCd);
                }
                Cbx_McGCd.EndUpdate();
            }

            // 更新系ボタン有効無効再設定
            SetEnableDisableUpdatingButtons();

            // 件数表示クリア
            Tsl_Msg.Text = null;
        }

        /// <summary>
        /// 更新系ボタン有効無効設定
        /// </summary>
        private void SetEnableDisableUpdatingButtons()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 件数表示クリア
            Tsl_Msg.Text = null;

            // 帳票形式による判定
            if (cmn.RegForm == Common.REG_FORM_LIST) // リスト形式
            {
                // DataGridView の追加行 ("*") を除いた行数で判定
                if (Dgv_CycleTmTbl.Rows.Count > 1) // データあり
                {
                    // [CSV 保存] ボタン可視 & 有効化
                    Btn_SaveCsvFile.Visible = Common.CTL_VISIBLE;
                    Btn_SaveCsvFile.Enabled = Common.CTL_ENABLED_ENABLE;

                    // [登録/更新] ボタン有効化
                    Btn_InsUpd.Enabled = Common.CTL_ENABLED_ENABLE;

                    // [削除] ボタン有効化
                    Btn_Delete.Enabled = Common.CTL_ENABLED_ENABLE;
                }
                else // データなし
                {
                    // [CSV 保存] ボタン可視 & 無効化
                    Btn_SaveCsvFile.Visible = Common.CTL_VISIBLE;
                    Btn_SaveCsvFile.Enabled = Common.CTL_ENABLED_DISABLE;

                    // [登録/更新] ボタン無効化
                    Btn_InsUpd.Enabled = Common.CTL_ENABLED_DISABLE;

                    // [削除] ボタン無効化
                    Btn_Delete.Enabled = Common.CTL_ENABLED_DISABLE;
                }
            }
            else // 単票形式
            {
                // [CSV 保存] ボタンは常時非可視 & 無効
                Btn_SaveCsvFile.Visible = Common.CTL_NON_VISIBLE;
                Btn_SaveCsvFile.Enabled = Common.CTL_ENABLED_DISABLE;

                // 切削オーダー データ有無チェック
                if (Dgv_MpCurOdrTbl.Rows.Count <= 0) // なし
                {
                    // [登録/更新] ボタン無効化
                    Btn_InsUpd.Enabled = Common.CTL_ENABLED_DISABLE;

                    // [削除] ボタン無効化
                    Btn_Delete.Enabled = Common.CTL_ENABLED_DISABLE;
                }
                else // あり
                {
                    // [登録/更新] ボタン有効化
                    Btn_InsUpd.Enabled = Common.CTL_ENABLED_ENABLE;

                    // [削除] ボタン有効化
                    Btn_Delete.Enabled = Common.CTL_ENABLED_ENABLE;
                }
            }
        }

        /// <summary>
        /// 処理済み件数メッセージ編集
        /// </summary>
        /// <param name="opeType">処理種別 ('I': 登録, 'U': 更新, 'D': 削除, 'Q': 検索)</param>
        /// <param name="rowCnt">処理件数の配列</param>
        private string EditProcResCntMsg(char opeType, int[] rowCnt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string ret = "";

            switch (opeType)
            {
                case Common.OPE_CD_INS: // 登録
                    ret = Common.MSG_TXT_LF
                        + Common.MSG_TXT_INS_NORMAL_END + rowCnt[Common.IDX_NORMAL_END] + Common.MSG_TXT_ROWS + ", "
                        + Common.MSG_TXT_INS_ABNORMAL_END + rowCnt[Common.IDX_ABNORMAL_END] + Common.MSG_TXT_ROWS;
                    break;
                case Common.OPE_CD_UPD: // 更新
                    ret = Common.MSG_TXT_LF
                        + Common.MSG_TXT_UPD_NORMAL_END + rowCnt[Common.IDX_NORMAL_END] + Common.MSG_TXT_ROWS + ", "
                        + Common.MSG_TXT_UPD_NO_DATA + rowCnt[Common.IDX_NO_DATA] + Common.MSG_TXT_ROWS + ", "
                        + Common.MSG_TXT_UPD_ABNORMAL_END + rowCnt[Common.IDX_ABNORMAL_END] + Common.MSG_TXT_ROWS;
                    break;
                case Common.OPE_CD_DEL: // 削除
                    ret = Common.MSG_TXT_LF
                        + Common.MSG_TXT_DEL_NORMAL_END + rowCnt[Common.IDX_NORMAL_END] + Common.MSG_TXT_ROWS + ", "
                        + Common.MSG_TXT_DEL_NO_DATA + rowCnt[Common.IDX_NO_DATA] + Common.MSG_TXT_ROWS + ", "
                        + Common.MSG_TXT_DEL_ABNORMAL_END + rowCnt[Common.IDX_ABNORMAL_END] + Common.MSG_TXT_ROWS;
                    break;
                default:
                    // 空文字を返す
                    break;
            }
            return ret;
        }

        /// <summary>
        /// フォーム情報読み出し
        /// </summary>
        private void LoadFormInfo()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // App.config からフォーム情報を読み込み各コントロールに設定 (表示) させる
            // 読み込む情報が (前回画面終了時に選択されていた値)
            // ・#042 切削オーダーの平準化.単票形式
            // ・#042 切削オーダーの平準化.リスト形式
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if ((config.AppSettings.Settings["Frm042Tbp1"].Value == "False") &&
                (config.AppSettings.Settings["Frm042Tbp2"].Value == "True"))
            {
                // リスト形式
                Tbc_FormType.SelectTab(Common.TBC1_INDEX_LIST);
                cmn.RegForm = Common.REG_FORM_LIST;
            }
            else
            {
                //　単票形式
                Tbc_FormType.SelectTab(Common.TBC1_INDEX_SINGLE_SHEET);
                cmn.RegForm = Common.REG_FORM_SINGLE_SHEET;
            }
            // 再表示
            Tbc_FormType.Refresh();
        }

        /// <summary>
        /// フォーム情報保存
        /// </summary>
        private void SaveFormInfo()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 各コントロールの選択値 (表示値) を App.config に書き込む
            // ・#042 切削オーダーの平準化.単票形式
            // ・#042 切削オーダーの平準化.リスト形式
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["Frm042Tbp1"].Value = Tbp_Slip.Text;
            config.AppSettings.Settings["Frm042Tbp2"].Value = Tbp_List.Text;
            config.Save();
        }

        private void Tbx_CurOpeTm_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Tbx_CurOpeRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tbx_CurOpeAvail_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tbx_SimOpeTm_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tbx_SimOpeRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tbx_SimOpeAvail_TextChanged(object sender, EventArgs e)
        {

        }

        private void Btn_Conf_Click(object sender, EventArgs e)
        {

        }

        private void Tbp_Slip_Click(object sender, EventArgs e)
        {

        }

        private void Tbx_McOnTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tbx_McSetupTm_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Btn_ReadCsvFile_Click(object sender, EventArgs e)
        {

        }
    }
}
