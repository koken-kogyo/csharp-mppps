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
        /// 戻り値リテラル取得
        /// </summary>
        /// <param name="retCd">戻り値</param>
        /// <returns>戻り値リテラル</returns>
        public string GetReturnCodeText(int retCd)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string ret;

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

            bool ret;
            string buf;
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




    }
}
