using DecryptPassword;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace MPPPS
{
    /// <summary>
	/// データベース管理クラス (共通メソッド, システム テーブル)
    /// </summary>
    public partial class DBManager
    {
        /// <summary>
        /// Oracle データベース スキーマ接続成否
        /// </summary>
        /// <param name="dbConf">データベース設定</param>
        /// <param name="oraCnn">EM データベースへの接続クラス</param>
        /// <returns>結果 (false: 失敗, true: 成功)</returns>
        public bool IsConnectOraSchema(int dbConf, ref OracleConnection oraCnn)
        {
            Common.s_Logger.Debug("{0} Method started", MethodBase.GetCurrentMethod().Name);

            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret;

            string user = cmn.DbCd[dbConf].User; // ユーザー

            // パスワード復号化
            var dpc = new DecryptPasswordClass();
            dpc.DecryptPassword(cmn.DbCd[dbConf].EncPasswd, out string decPasswd);

            // データソース
            string ds = "(DESCRIPTION="
                        + "(ADDRESS="
                          + "(PROTOCOL=" + cmn.DbCd[dbConf].Protocol + ")"
                          + "(HOST="     + cmn.DbCd[dbConf].Host     + ")"
                          + "(PORT="     + cmn.DbCd[dbConf].Port     + ")"
                        + ")"
                        + "(CONNECT_DATA="
                          + "(SERVICE_NAME=" + cmn.DbCd[dbConf].ServiceName + ")"
                        + ")"
                      + ")";  // Oracle Client を使用せず直接接続する

            // Oracle 接続文字列を組み立てる
            string connectString = "User Id="     + user + "; "
                                 + "Password="    + decPasswd + "; "
                                 + "Data Source=" + ds;
            try
            {
                oraCnn = new OracleConnection(connectString);

                // Oracle へのコネクションの確立
                oraCnn.Open();
                Trace.WriteLine("Oracle に接続しました。");
                Trace.WriteLine(oraCnn.ServerVersion);
                ret = true;
            }
            catch (Exception e)
            {
                Trace.WriteLine("Exception Source = " + e.Source);
                Trace.WriteLine("Exception Message = " + e.Message);

                // 接続を閉じる
                CloseOraSchema(cmn.OraCnn[dbConf]);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Oracle データベース スキーマからの切断
        /// </summary>
        /// <param name="oraCnn">Oracle データベースへの接続クラス</param>
        public void CloseOraSchema(OracleConnection oraCnn)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 接続を閉じる
            if (oraCnn != null)
            {
                oraCnn.Close();
                Trace.WriteLine("Oracle から切断しました。");
            }
        }

        /// <summary>
    	/// Oracle テーブル情報取得 (USER_TAB_COLUMNS, USER_COL_COMMENTS)
        /// </summary>
        /// <param name="dataSet">データセット</param>
        /// <param name="tableID">テーブル ID</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int GetOraTableInfo(ref DataSet dataSet, string tableID)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            OracleConnection cnn = null;

            try
            {
                // 内製プログラム データベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_KK, ref cnn);

                // テーブル情報取得 SQL 構文編集
                string sql = EditOraTableInfoQuerySql(tableID);
                using (OracleCommand myCmd = new OracleCommand(sql, cmn.OraCnn[Common.DB_CONFIG_EM]))
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
                                Debug.Write("TABLE_NAME = "     + dr[0] + ", ");
                                Debug.Write("COLUMN_NAME = "    + dr[1] + ", ");
                                Debug.Write("DATA_TYPE = "      + dr[2] + ", ");
                                Debug.Write("DATA_LENGTH = "    + dr[3] + ", ");
                                Debug.Write("DATA_PRECISION = " + dr[4] + ", ");
                                Debug.Write("DATA_SCALE = "     + dr[5] + ", ");
                                Debug.Write("NULLABLE = "       + dr[6] + ", ");
                                Debug.Write("COLUMN_ID = "      + dr[7] + ", ");
                                Debug.Write("COMMENTS = "       + dr[8] + ", ");
                                Debug.WriteLine("");
                            }
                            dataSet = myDs;
                            ret = 0;
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
        /// Oracle テーブル情報取得 SQL 構文編集 (USER_TAB_COLUMNS, USER_COL_COMMENTS)
        /// </summary>
        /// <param name="tableID">テーブル ID</param>
        /// <returns>SQL 構文</returns>
        private string EditOraTableInfoQuerySql(string tableID)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string sql = "select "
                       + "A.TABLE_NAME, "
                       + "A.COLUMN_NAME, "
                       + "A.DATA_TYPE, "
                       + "A.DATA_LENGTH, "
                       + "A.DATA_PRECISION, "
                       + "A.DATA_SCALE, "
                       + "A.NULLABLE, "
                       + "A.COLUMN_ID, "
                       + "B.COMMENTS "
                       + "from "
                       + "USER_TAB_COLUMNS A, "
                       + "USER_COL_COMMENTS B "
                       + "where "
                       + "A.TABLE_NAME = '" + tableID + "' and "
                       + "B.TABLE_NAME = A.TABLE_NAME and "
                       + "B.COLUMN_NAME = A.COLUMN_NAME "
                       + "order by "
                       + "A.TABLE_NAME ,"
                       + "A.COLUMN_ID"
                       ;
            return sql;
        }

        /// <summary>
    	/// Oracle テーブル列名一覧取得 (USER_TAB_COLUMNS)
        /// </summary>
        /// <param name="tableName">テーブル名称</param>
        /// <param name="dataSet">データセット</param>
        /// <returns>結果 (0: 正常、1: 異常)</returns>
        public bool GetOraTableColumnNameList(string tableName, ref DataSet dataSet)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            OracleConnection cnn = null;

                try
                {
                    // 内製プログラム データベースへ接続
                    cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_KK, ref cnn);

                    string sql = "SELECT "
                        + "USER_TAB_COLUMNS.COLUMN_ID, "
                        + "USER_TAB_COLUMNS.COLUMN_NAME "
                        + "FROM "
                        + "USER_TAB_COLUMNS "
                        + "LEFT JOIN USER_TAB_COMMENTS ON USER_TAB_COLUMNS.TABLE_NAME = USER_TAB_COMMENTS.TABLE_NAME "
                        + "LEFT JOIN USER_COL_COMMENTS ON USER_TAB_COLUMNS.TABLE_NAME = USER_COL_COMMENTS.TABLE_NAME "
                        + "AND USER_TAB_COLUMNS.COLUMN_NAME = USER_COL_COMMENTS.COLUMN_NAME "
                        + "WHERE "
                        + "1 = 1 "
                        + "AND USER_TAB_COLUMNS.TABLE_NAME IN ('" + tableName + "') "
                        + "ORDER BY "
                        + "USER_TAB_COLUMNS.COLUMN_ID";

                using (OracleCommand myCmd = new OracleCommand(sql, cmn.OraCnn[Common.DB_CONFIG_EM]))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Write to DataSet: " + dataSet.DataSetName);
                        using (DataSet myDs = new DataSet())
                        {
                            // 結果取得
                            myDa.Fill(myDs, "emp");
                            foreach (DataRow dr in myDs.Tables[0].Rows)
                            {
                                Debug.WriteLine("myDs.Tables[0].Rows.IndexOf(dr) = " + myDs.Tables[0].Rows.IndexOf(dr).ToString());
                            }
                            dataSet = myDs;
                            ret = true;
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
                ret = false;
            }
            // 接続を閉じる
            cmn.Dbm.CloseOraSchema(cnn);
            return ret;
        }
    }
}
