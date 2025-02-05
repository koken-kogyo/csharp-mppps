using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MPPPS
{
    /// <summary>
    /// 共通クラス
    /// </summary>
    public partial class Common
    {
        /// <summary>
        /// メソッド定義
        /// </summary>

        // この文字コードで扱う場合のバイト数で入力文字数を制限する
        private Encoding ENCODE_BYTECHK = Encoding.GetEncoding("Shift_JIS");

        /// <summary>
        /// 復改文字修正
        /// </summary>
        /// <param name="str">文字列</param>
        /// <returns>修正後文字列</returns>
        public string CorrectLineFeed(string str)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string msgBody;

            // 文字列として埋め込まれている復改 (\\n; 0x5C + 0x5C + 0x6E) を制御コード (\n) に置換する
            string pattern = "\\\\n";
            string[] arr = Regex.Split(str, pattern); // 復改ごとに分割
            msgBody = string.Join("\n", arr); // 復改の制御コードで再度連結
            return msgBody;
        }



        /// <summary>
        /// 数値チェック
        /// </summary>
        /// <param name="srcStr">入力文字列</param>
        /// <param name="isUnsigned">符号あり</param>
        /// <param name="dataPrecision">データ精度</param>
        /// <param name="dataScale">小数点以下有効桁数</param>
        /// <param name="dstStr">出力文字列</param>
        /// <returns>結果 (0: 正常 (数値) , 1:正常(null、空文字または空白文字のみ),
        ///               -1: 異常 (数値ではない), -2: 異常 (負数), -3: 異常 (桁溢れ))</returns>
        public int CheckNumericalNumber(string srcStr, bool isSigned, int dataPrecision, int dataScale, out string dstStr)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = RET_CD_OK_NUMERICAL_NUMBER;
            dstStr = "";

            while (true)
            {
                // null、空文字または空白文字のみかチェック
                if (string.IsNullOrWhiteSpace(srcStr))
                {
                    // null、空文字または空白文字のみなら数値チェックしない
                    ret = RET_CD_OK_NULL_OR_WHITESPC;
                    break;
                }

                // 数値かチェック
                if (double.TryParse(srcStr, out double d) == false)
                {
                    // 数値ではない
                    ret = RET_CD_NG_NOT_NUM;
                    break;
                }

                // 符号なしのときに負数が来ないかチェック
                if (isSigned == false)
                {
                    if (d < 0)
                    {
                        // 負数
                        ret = RET_CD_NG_MINUS;
                        break;
                    }
                }

                string[] arr = new string[] { "0", "0" };

                // 小数書式化
                if (FormatDecimalNumber(srcStr, dataScale, ref arr) == false)
                {
                    // 不正な書式
                    ret = RET_CD_NG_FORMAT;
                    break;
                }

                // 整数部の不要文字を削除
                string intPart;
                string trimmedStr = arr[0].TrimStart(' ', '0');
                if (trimmedStr == "")
                {
                    intPart = "0";
                }
                else
                {
                    intPart = trimmedStr;
                }

                // 桁溢れチェック
                if (intPart.Length > (dataPrecision - dataScale))
                {
                    // 桁溢れ
                    ret = RET_CD_NG_OVERFLOW;
                    break;
                }

                // 整形した文字列を連結
                if (dataScale > 0)
                {
                    dstStr = intPart + Common.DECIMAL_POINT + arr[1].Substring(0, dataScale);
                }
                else
                {
                    dstStr = intPart;
                }
                ret = RET_CD_OK_NUMERICAL_NUMBER;
                break;
            }
            return ret;
        }

        /// <summary>
        /// 小数書式化
        /// </summary>
        /// <param name="srcStr">入力文字列</param>
        /// <param name="dataScale">小数桁数</param>
        /// <param name="arr">配列</param>
        /// <returns>結果 (false: 失敗, true: 成功)</returns>
        private static bool FormatDecimalNumber(string srcStr, int dataScale, ref string[] arr)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;

            try
            {
                if (srcStr.IndexOf('.') == srcStr.Length - 1)
                {
                    // 小数点が末尾にあるときは整数部のみを抽出
                    arr[0] = srcStr.Substring(0, (srcStr.Length - 1));
                    ret = true;
                }
                else if (srcStr.IndexOf('.') >= 0)
                {
                    // 小数点があるときは整数部と小数部に分離
                    arr = srcStr.Split('.');
                }
                else
                {
                    // 小数点がないときは整数部に設定
                    arr[0] = srcStr;
                }

                // 桁合わせ
                if (arr[1].Length < dataScale)
                {
                    // 小数桁数に満たないときは末尾に "0" を付加
                    int j = dataScale - arr[1].Length;
                    for (int i = 0; i < j; i++)
                    {
                        arr[1] = arr[1] + "0";
                    }
                    ret = true;
                }
                else if (arr[1].Length > dataScale)
                {
                    // 小数桁数超過
                    ret = false;
                }
                else
                {
                    // 小数桁数と同一
                    ret = true;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Exception Source = " + e.Source);
                Trace.WriteLine("Exception Message = " + e.Message);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 戻り値リテラル取得
        /// </summary>
        /// <param name="retCd">戻り値</param>
        /// <returns>戻り値リテラル</returns>
        public string GetReturnCodeText(int retCd)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string ret = "";

            switch (retCd)
            {
                case RET_CD_OK_NUMERICAL_NUMBER: // 正常 (数値)
                    ret = RET_TXT_OK_NUMERICAL_NUMBER;
                    break;

                case RET_CD_OK_NULL_OR_WHITESPC: // 正常 (null または空白)
                    ret = RET_TXT_OK_NULL_OR_WHITESPC;
                    break;

                case RET_CD_NG_NOT_NUM: // 異常 (数値ではない)
                    ret = RET_TXT_NG_NOT_NUM;
                    break;

                case RET_CD_NG_MINUS: // 異常 (負数)
                    ret = RET_TXT_NG_MINUS;
                    break;

                case RET_CD_NG_FORMAT: // 異常 (不正な書式)
                    ret = RET_TXT_NG_FORMAT;
                    break;

                case RET_CD_NG_OVERFLOW: // 異常 (桁溢れ)
                    ret = RET_TXT_NG_OVERFLOW;
                    break;

                default: // 異常 (その他)
                    ret = RET_TXT_NG_OTHER;
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 16 進 → 10 進変換
        /// </summary>
        /// <param name="hex">16 進数</param>
        /// <param name="dec">10 進数</param>
        /// <returns>結果 (false: 失敗, true: 成功)</returns>
        public static bool ConvertHexToDec(string hex, out int dec)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            string buf = "";
            dec = 0;

            try
            {
                if (hex.Length <= 0)
                {
                    ret = false;
                }
                else
                {
                    if (hex.Substring(0, 2) == "0x")
                    {
                        buf = hex.Substring(2);
                    }
                    else
                    {
                        buf = hex;
                    }

                    // 進数変換
                    dec = Convert.ToInt32(buf, 16);
                    Debug.WriteLine("HEX = " + hex + ", " + "DEC = " + dec);
                    ret = true;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Exception Source = " + e.Source);
                Trace.WriteLine("Exception Message = " + e.Message);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 10 進 → 16 進 (32bit) 変換
        /// </summary>
        /// <param name="dec">10 進数</param>
        /// <returns>16 進数</returns>
        public string ConvertDecToHex(int dec)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 16 進数 (32but) に変換
            return dec.ToString("x8");
        }

        /// <summary>
        /// 文字列の末尾から指定した長さの文字列を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="len">長さ</param>
        /// <returns>取得した文字列</returns>
        public string Right(string str, int len)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            if (len < 0)
            {
                throw new ArgumentException("引数'len'は0以上でなければなりません。");
            }
            if (str == null)
            {
                return "";
            }
            if (str.Length <= len)
            {
                return str;
            }
            return str.Substring(str.Length - len, len);
        }

        /// <summary>
        /// メッセージ ボックス表示
        /// </summary>
        /// <param name="pgmID">プログラム ID</param>
        /// <param name="msgCd">メッセージ コード</param>
        /// <param name="msgType">メッセージ種別</param>
        /// <param name="buttons">表示ボタン</param>
        /// <param name="msgBoxTxt">メッセージ ボックス テキスト</param>
        /// <param name="icon">アイコン</param>
        /// <param name="defaultButton">既定のボタン (既定値: Button1)</param>
        /// <param name="msgBodyExtStr">メッセージ本文拡張文字列 (既定値: "")</param>
        /// <returns></returns>
        public DialogResult ShowMessageBox(string pgmID, string msgCd, string msgType, MessageBoxButtons buttons,
            string msgBoxTxt, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1,
            string msgBodyExtStr = "")
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string msgTitle = "";
            string msgBody = "";
            Dba.GetErrorMsg(pgmID, msgType, msgCd, ref msgTitle, ref msgBody);
            return MessageBox.Show(MSG_CD_PREFIX + msgType + msgCd + MSG_CD_SUFFIX + msgTitle + '\n' + '\n' +
                                   msgBody + '\n' + msgBodyExtStr, msgBoxTxt, buttons, icon, defaultButton);
        }

        /// <summary>
        /// メッセージ ボックス表示 (拡張メッセージ本文付き)
        /// </summary>
        /// <param name="msgCd">メッセージ コード</param>
        /// <param name="msgType">メッセージ種別</param>
        /// <param name="button">ボタン クラス</param>
        /// <param name="msgBoxTxt">メッセージ ボックス テキスト</param>
        /// <param name="icon">アイコン クラス</param>
        /// <param name="extMsgBodyTxt">拡張メッセージ本文</param>
        /// <returns></returns>
        public DialogResult ShowMessageBox(string pgmID, string msgCd, string msgType, MessageBoxButtons Btn, string msgBoxTxt, MessageBoxIcon icon, string extMsgBodyTxt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string msgTitle = "";
            string msgBody = "";
            Dba.GetErrorMsg(pgmID, msgType, msgCd, ref msgTitle, ref msgBody);
            return MessageBox.Show(MSG_CD_PREFIX + msgType + msgCd + MSG_CD_SUFFIX + msgTitle + '\n' + '\n' +
                                   msgBody + '\n' + extMsgBodyTxt, msgBoxTxt, Btn, icon);
        }


        /// <summary>
        /// すべてのコントロールを取得
        /// https://www.atmarkit.co.jp/fdotnet/dotnettips/224controls/controls.html
        /// </summary>
        /// <param name="top">親コントロール</param>
        /// <returns></returns>
        public Control[] GetAllControls(Control top)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            ArrayList buf = new ArrayList();
            foreach (Control c in top.Controls)
            {
                buf.Add(c);
                buf.AddRange(GetAllControls(c));
            }
            return (Control[])buf.ToArray(typeof(Control));
        }

        /// <summary>
        /// すべてのコントロールをクリア ★
        /// </summary>
        /// <param name="top">親コントロール</param>
        public void ClearAllControls(Control top)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            try
            {
                ArrayList buf = new ArrayList();

                // 登録形式ごとの処理
                if (RegForm == Common.REG_FORM_SINGLE_SHEET) // 単票形式
                {
                    foreach (Control c in top.Controls)
                    {
                        // 子にコントロールを持つコントロールは、再帰的にクリア処理を呼び出す
                        if (c.HasChildren)
                        {
                            ClearAllControls(c);
                        }

                        // コントロールが無効のときはクリアしない
                        if (!c.Enabled)
                        {
                            continue;
                        }

                        // フォーム上に直接配置されたコントロール
                        // テキスト ボックスおよび派生クラス
                        if (c is TextBoxBase)
                        {
                            c.Text = string.Empty;  // 空文字
                        }
                        // タブ コントロール
                        else if (c is TabControl)
                        {
                            // 変更しない
                        }
                        // ラジオ ボタン
                        else if (c is RadioButton)
                        {
                            RadioButton rb;
                            rb = (RadioButton)c;    // キャストして型変換
                                                    //                        // 登録形式選択肢はクリアしない
                                                    //if ((rb.Text == Tbp_TXT_SINGLE_SHEET) || (rb.Text == Tbp_TXT_LIST))
                                                    //{
                                                    //    // 変更しない
                                                    //}
                                                    //else
                                                    //{
                            rb.Checked = false;     // チェック オフ
                            //}
                        }
                        // コンボ ボックス
                        else if (c is ComboBox)
                        {
                            ComboBox cb;
                            cb = (ComboBox)c;       // キャストして型変換
                            cb.Text = string.Empty; // 表示文字列を消去
                            cb.Items.Clear();       // アイテムを削除
                        }
                        // デイトタイム ピッカー
                        else if (c is DateTimePicker)
                        {
                            DateTimePicker dtp;
                            dtp = (DateTimePicker)c;    // キャストして型変換
                            dtp.Value = DateTime.Now;   // 現在日時
                        }
                        else // その他
                        {
                            // 何もしない
                        }
                    }
                }
                else // リスト形式
                {
                    foreach (Control c in top.Controls)
                    {
                        // 子にコントロールを持つコントロールは、再帰的にクリア処理を呼び出す
                        if (c.HasChildren)
                        {
                            ClearAllControls(c);
                        }

                        // コントロールが無効のときはクリアしない
                        if (!c.Enabled)
                        {
                            continue;
                        }

                        // フォーム上に直接配置されたコントロール
                        // タブ コントロール
                        if (c is TabControl)
                        {
                            // 変更しない
                        }
                        // DataGridView
                        else if (c is DataGridView)
                        {
                            DataGridView dgv;
                            dgv = (DataGridView)c;  // キャストして型変換

                            // 全行削除
                            RemoveDagaGridViewRows(dgv);

                        }
                        else // その他
                        {
                            // 何もしない
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Exception Source = " + e.Source);
                Trace.WriteLine("Exception Message = " + e.Message);
            }
        }

        /// <summary>
        /// DataGridView 全行削除
        /// </summary>
        /// <param name="dgv"></param>
        public void RemoveDagaGridViewRows(DataGridView dgv)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int rowNum = dgv.Rows.Count - 1;

            try
            {
                dgv.DataSource = null;
                //dgv.Rows.Clear(); // ★
                //// すべての行を削除
                //for (int i = 0; i < rowNum; i++)
                //{
                //    if (dgv.CurrentRow.IsNewRow)
                //    {
                //        // 新しい行 ("*" のある行) なら読み飛ばし
                //    }
                //    else
                //    {
                //        // 既存行なら削除
                //        dgv.Rows.RemoveAt(0);
                //    }
                //}
            }
            catch (Exception e)
            {
                Trace.WriteLine("Exception Source = " + e.Source);
                Trace.WriteLine("Exception Message = " + e.Message);
            }
        }

        /// <summary>
        /// コントロール状態設定
        /// </summary>
        /// <param name="top">親コントロール</param>
        /// <param name="regType">登録形式 (S:単票形式、L:リスト形式)</param>
        public void SetControlStatus(Control top, char regType)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool sCtlVisible;
            bool lCtlVisible;
            bool sCtlEnabled;
            bool lCtlEnabled;

            // 登録形式退避
            RegForm = regType;

            // 有効/無効設定
            if (regType == REG_FORM_LIST) // リスト形式
            {
                sCtlVisible = CTL_NON_VISIBLE;
                lCtlVisible = CTL_VISIBLE;
                sCtlEnabled = CTL_ENABLED_DISABLE;
                lCtlEnabled = CTL_ENABLED_ENABLE;
            }
            else // 単票形式または未設定
            {
                sCtlVisible = CTL_VISIBLE;
                lCtlVisible = CTL_NON_VISIBLE;
                sCtlEnabled = CTL_ENABLED_ENABLE;
                lCtlEnabled = CTL_ENABLED_DISABLE;
            }

            try
            {
                ArrayList buf = new ArrayList();
                foreach (Control c in top.Controls)
                {
                    // 子にコントロールを持つコントロールは、再帰的にコントロール状態設定処理を呼び出す
                    if (c.HasChildren)
                    {
                        SetControlStatus(c, regType);
                    }

                    // フォーム上に直接配置されたコントロール
                    // テキスト ボックスおよび派生クラス
                    if (c is TextBoxBase)
                    {
                        c.Enabled = sCtlEnabled;
                    }
                    // タブ コントロール
                    else if (c is TabControl)
                    {
                        // 常時有効
                        c.Enabled = CTL_ENABLED_ENABLE;
                    }
                    // ラジオ ボタン
                    else if (c is RadioButton)
                    {
                        //// 登録形式選択ボタンは対象外
                        //if ((c.Text == Rbt_TXT_SINGLE_SHEET) || (c.Text == Rbt_TXT_LIST)) // 登録形式
                        //{
                        //    // 常時有効
                        //    c.Enabled = CTL_ENABLED_ENABLE;
                        //}
                        //else // その他
                        //{
                        // 他の登録形式とのトグル動作
                        c.Enabled = sCtlEnabled;
                        //}
                    }
                    // コンボ ボックス
                    else if (c is ComboBox)
                    {
                        c.Enabled = sCtlEnabled;
                    }
                    // デイトタイム ピッカー
                    else if (c is DateTimePicker)
                    {
                        c.Enabled = sCtlEnabled;
                    }
                    // DataGridView
                    else if (c is DataGridView)
                    {
                        // リスト形式のときのみ有効
                        c.Enabled = lCtlEnabled;
                    }
                    // ボタン
                    else if (c is Button)
                    {
                        if ((c.Text == BTN_TXT_READ_CSV_FILE) || (c.Text == BTN_TXT_SAVE_CSV_FILE)) // [CSV 読込] または [CSV 保存]
                        {
                            // 単票形式のとき非可視 & 無効、リスト形式のとき可視 & 有効
                            c.Visible = lCtlVisible;
                            c.Enabled = lCtlEnabled;
                        }
                        else // その他
                        {
                            // 常時有効
                            c.Enabled = CTL_ENABLED_ENABLE;
                        }
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Exception Source = " + e.Source);
                Trace.WriteLine("Exception Message = " + e.Message);
            }
        }

        /// <summary>
        /// データセットにデータはあるか
        /// </summary>
        /// <param name="dataSet">データ セット</param>
        /// <returns>結果 (false: なし, true: あり)</returns>
        public bool IsDataExisting(DataSet dataSet)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret;
            if (dataSet.Tables.Count <= 0)
            {
                // 該当データなし
                ret = false;
            }
            else
            {
                if (dataSet.Tables[0].Rows.Count <= 0)
                {
                    // 該当データなし
                    ret = false;
                }
                else
                {
                    // 該当データあり
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>
        /// データ テーブルの列名置換
        /// </summary>
        /// <param name="dataTable">データ テーブル</param>
        /// <param name="numColIdxList">数値列インデックス リスト</param>
        public void ReplaceDataSetColumnName(int dbConf, string tableID, ref DataTable dataTable, out List<int> numColIdxList)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            List<int> colIdx = new List<int>();

            // テーブル情報取得
            DataSet dataSetTblInfo = new DataSet();
            if (dbConf == Common.DB_CONFIG_MP)
            {
                // 切削生産計画システム
                Dbm.GetMySqlTableInfo(ref dataSetTblInfo, tableID);
            }
            else
            {
                // それ以外
                Dbm.GetOraTableInfo(ref dataSetTblInfo, tableID);
            }

            for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
            {
                for (int rowIndex = 0; rowIndex <  dataSetTblInfo.Tables[0].Rows.Count; rowIndex++)
                {
                    // 列の物理名が不一致なら読み飛ばし
                    DataRow dr = dataSetTblInfo.Tables[0].Rows[rowIndex];
                    if (dataTable.Columns[columnIndex].ColumnName != dr.Field<string>(Common.USER_COL_COMMENTS_INDEX_COLUNM_NAME))
                    {
                        continue;
                    }

                    // 列のデータ型が数値ならリストに追加
                    string dataType = dataSetTblInfo.Tables[0].Rows[rowIndex].ItemArray[Common.USER_TAB_COLUMNS_INDEX_DATA_TYPE].ToString();
                    if (dbConf == Common.DB_CONFIG_MP)
                    {
                        // 
                        if (dataType.ToUpper() == Common.USER_TAB_COLUMNS_DATA_TYPE_INT)
                        {
                            colIdx.Add(columnIndex);
                        }
                    }
                    else
                    {
                        if (dataType.ToUpper() == Common.USER_TAB_COLUMNS_DATA_TYPE_NUMBER)
                        {
                            colIdx.Add(columnIndex);
                        }
                    }

                    // 列に論理名があれば物理名と置換
                    if (!string.IsNullOrWhiteSpace(dr.Field<string>(Common.USER_COL_COMMENTS_INDEX_COMMENTS)))
                    {
                        string[] cmtArr = dr.Field<string>(Common.USER_COL_COMMENTS_INDEX_COMMENTS).ToString().Split(':');
                        if (cmtArr[0].Length > 0)
                        {
                            // 論理名で置換してループを抜ける
                            dataTable.Columns[columnIndex].ColumnName = cmtArr[0];
                            break;
                        }
                        else
                        {
                            // 次を探す
                            ;
                        }
                    }
                    else
                    {
                        // 次を探す
                        ;
                    }
                }

            }
            // 数値列インデックス リストにコピー
            numColIdxList = colIdx;
        }

        /// <summary>
        /// テキスト ボックス入力文字数制限
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="eventHandler">イベント ハンドラー</param>
        /// <param name="msgCd">メッセージ コード</param>
        public void ReduceTextBoxBytes(object sender, EventHandler eventHandler, string msgCd)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            var txtBox = sender as TextBox;

            try
            {
                txtBox.TextChanged -= eventHandler;

                // NULL または空文字かチェック
                if (!string.IsNullOrEmpty(txtBox.Text))
                {
                    var value = txtBox.Text.Trim();
                    if (ENCODE_BYTECHK.GetByteCount(value) > txtBox.MaxLength)
                    {
                        // 入力可能桁数を超えているので今入力されたものを無効にしてしまう

                        int currentPoint = txtBox.SelectionStart;
                        var leftStr = value.Substring(0, currentPoint > txtBox.MaxLength ? txtBox.MaxLength : currentPoint);
                        var rightStr = leftStr.Length >= txtBox.MaxLength ? "" : value.Substring(currentPoint);
                        txtBox.Text = leftStr + rightStr;
                        txtBox.SelectionStart = currentPoint;

                        // 入力可能桁数超過
                        Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
                        ShowMessageBox(Common.MY_PGM_ID, msgCd, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);
                    }
                }
            }
            finally
            {
                txtBox.TextChanged += eventHandler;
            }

        }

        /// <summary>
        /// メンバー名 (メソッド名またはプロパティ名) 取得
        /// </summary>
        /// <param name="memberName">メンバー名</param>
        /// <returns></returns>
        public static string GetMethodName([CallerMemberName] string memberName = "")
        {
            if (AssemblyState.IsDebug) Debug.WriteLine(MethodBase.GetCurrentMethod().Name, Common.DBG_CAT_METHOD);

            return memberName;
        }

        /// <summary>
        /// ソース ファイル名取得
        /// </summary>
        /// <param name="path"></param>
        public static string GetPgmFileName([CallerFilePath] string pgmFilePath = "")
        {
            if (AssemblyState.IsDebug) Debug.WriteLine(MethodBase.GetCurrentMethod().Name, Common.DBG_CAT_METHOD);

            return Path.GetFileName(pgmFilePath);
        }

        /// <summary>
        /// ソース行番号取得
        /// </summary>
        /// <param name="pgmLineNumber"></param>
        public static int GetPgmLineNum([CallerLineNumber] int pgmLineNumber = 0)
        {
            if (AssemblyState.IsDebug) Debug.WriteLine(MethodBase.GetCurrentMethod().Name, Common.DBG_CAT_METHOD);

            return pgmLineNumber;
        }
    }
}
