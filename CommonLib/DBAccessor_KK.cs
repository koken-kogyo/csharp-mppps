//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace MPPPS
{
    /// <summary>
	/// データベース アクセス クラス (内製システム共通テーブル)
    /// </summary>
    public partial class DBAccessor
    {
        /// <summary>
        /// ホスト情報取得 (KS0010 ホスト マスター)
        /// </summary>
        /// <param name="dataSet">データセット</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int GetHostInfo(ref DataSet dataSet)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            OracleConnection cnn = null;

            try
            {
                // 内製プログラム データベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_KK, ref cnn);

                // 変換対象の得意先管理番号と得意先略称 を全件検索
                string sql = EditHostInfoQuery();

                using (OracleCommand myCmd = new OracleCommand(sql, cnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataSet:");
                        using (DataSet myDs = new DataSet())
                        {
                            // 結果取得
                            myDa.Fill(myDs, "emp");
                            foreach (DataRow dr in myDs.Tables[0].Rows)
                            {
                                Debug.Write("HOSTNM = "    + dr[0] + ", ");
                                Debug.Write("IPADDR = "    + dr[1] + ", ");
                                Debug.Write("PURPOSE = "   + dr[2] + ", ");
                                Debug.Write("OS = "        + dr[3] + ", ");
                                Debug.Write("SHARENM = "   + dr[4] + ", ");
                                Debug.Write("USERID = "    + dr[5] + ", ");
                                Debug.Write("ENCPASSWD = " + dr[6]);
                                Debug.WriteLine("");
                            }
                            dataSet = myDs;
                        }
                    }
                }
                ret = dataSet.Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
                // エラー
                string msg = "Exception Source = " + ex.Source + ", Message = " + ex.Message;
                if (AssemblyState.IsDebug) Debug.WriteLine(msg);

                Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
                cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_802, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);
                ret = -1;
            }
            // 接続を閉じる
            cmn.Dbm.CloseOraSchema(cnn);

            return ret;
        }

        /// <summary>
        /// ホスト情報取得 SQL 構文編集 (KS0010 ホストマスタ) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditHostInfoQuery()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string sql = "select "
                       + "HOSTNM, "
                       + "IPADDR, "
                       + "PURPOSE, "
                       + "OS, "
                       + "SHARENM, "
                       + "USERID, "
                       + " ENCPASSWD "
                       + "from "
                       + cmn.DbCd[Common.DB_CONFIG_KK].Schema + "." + Common.TABLE_ID_KS0010 + " "
                       + "where "
                       + "HOSTNM = '" + cmn.PkKS0010.HostNm + "' ";
            return sql;
        }

        /// <summary>
        /// メッセージ取得 (KS0040 メッセージ マスター)
        /// </summary>
        /// <param name="pgmId">プログラム ID</param>
        /// <param name="msgCd">メッセージ コード</param>
        /// <param name="msgType">メッセージ種別</param>
        /// <param name="msgTitle">メッセージ タイトル</param>
        /// <param name="msgBody">メッセージ本文</param>
        public void GetErrorMsg(string pgmId, string msgType, string msgCd, ref string msgTitle, ref string msgBody)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            OracleConnection cnn = null;

            try
            {
                // 内製プログラム データベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_KK, ref cnn);

                // メッセージ取得クエリー編集
                string sql = EditErrorMsgQuery(pgmId, msgCd);

                using (OracleCommand myCmd = new OracleCommand(sql, cnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataSet:");
                        using (DataSet myDs = new DataSet())
                        {
                            // 結果取得
                            myDa.Fill(myDs, "emp");
                            foreach (DataRow dr in myDs.Tables[0].Rows)
                            {
                                Debug.Write("PGMID = " + dr[0] + ", ");
                                Debug.Write("MSGCD = " + dr[1] + ", ");
                                Debug.Write("MSGTYPE = " + dr[2] + ", ");
                                Debug.Write("MSGTITLE = " + dr[3] + ", ");
                                Debug.Write("MSGBODY = " + dr[4]);
                                Debug.WriteLine("");
                                msgTitle = dr[3].ToString();
                                string str = dr[4].ToString();

                                // 復改文字を文字列 (\\n) から制御コード (\n) に変換
                                msgBody = cmn.CorrectLineFeed(str);

                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // エラー
                string msg = "Exception Source = " + ex.Source + ", Message = " + ex.Message;
                if (AssemblyState.IsDebug) Debug.WriteLine(msg);

                Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
                cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_802, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);
            }

            // 接続を閉じる
            cmn.Dbm.CloseOraSchema(cnn);
        }

        /// <summary>
        /// メッセージ取得クエリー編集 (KS0040 メッセージ マスター)
        /// </summary>
        /// <param name="pgmId">プログラム ID</param>
        /// <param name="msgCd">メッセージ コード</param>
        /// <returns>SQL 構文</returns>
        private string EditErrorMsgQuery(string pgmId, string msgCd)
        {
            return "SELECT "
                + "PGMID, "
                + "MSGCD, "
                + "MSGTYPE, "
                + "MSGTITLE, "
                + "MSGBODY "
                + "FROM "
                + cmn.DbCd[Common.DB_CONFIG_KK].Schema + "." + Common.TABLE_ID_KS0040 + " "
                + "WHERE "
                + "PGMID = '" + pgmId + "' AND "
                + "MSGCD = '" + msgCd + "'";
        }
    }
}
