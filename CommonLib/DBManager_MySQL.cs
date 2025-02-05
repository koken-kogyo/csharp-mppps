using MySql.Data.MySqlClient;
using DecryptPassword;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace MPPPS
{
    /// <summary>
	/// データベース管理者クラス (共通メソッド, システム テーブル)
    /// </summary>
    public partial class DBManager
    {
        // 共通クラス
        private readonly Common cmn;

        /// <summary>
        /// デフォルト コンストラクタ
        /// </summary>
        public DBManager()
        {
        }

        public DBManager(Common cmn)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 共通クラス
            this.cmn = cmn;
        }

        /// <summary>
        /// MySQL データベース スキーマ接続成否
        /// </summary>
        /// <param name="mpCnn">MySql データベースへの接続クラス</param>
        /// <returns>結果 (false: 失敗, true: 成功)</returns>
        public bool IsConnectMySqlSchema(ref MySqlConnection mpCnn)
        {
            Common.s_Logger.Debug("{0} Method started", MethodBase.GetCurrentMethod().Name);

            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret;

            try
            {
                // パスワード復号化
                var dpc = new DecryptPasswordClass();
                dpc.DecryptPassword(cmn.DbCd[Common.DB_CONFIG_MP].EncPasswd, out string decPasswd);

                // MySQL への接続情報
                string server   = cmn.DbCd[Common.DB_CONFIG_MP].Host;
                string database = cmn.DbCd[Common.DB_CONFIG_MP].Schema;
                int    port     = cmn.DbCd[Common.DB_CONFIG_MP].Port;
                string uid      = cmn.DbCd[Common.DB_CONFIG_MP].User;
                string pwd      = decPasswd;
                string charset  = cmn.DbCd[Common.DB_CONFIG_MP].CharSet;

                string connectionString = string.Format("Server={0};Database={1};Port={2};Uid={3};Pwd={4};Charset={5}", server, database, port, uid, pwd, charset);

                // MySQL への接続
                mpCnn = new MySqlConnection(connectionString);
                mpCnn.Open();	// 接続
                Trace.WriteLine("MySQL に接続しました。");
                Trace.WriteLine(mpCnn.ServerVersion);
                ret = true;
            }
            catch (MySqlException me)
            {
                Trace.WriteLine("Exception Source = " + me.Source);
                Trace.WriteLine("Exception Message = " + me.Message);
                mpCnn.Close();	// 接続の解除
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// MySQL データベース スキーマからの切断
        /// </summary>
        /// <param name="mpCnn">MySQL データベースへの接続クラス</param>
        public void CloseMySqlSchema(MySqlConnection mpCnn)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 接続を閉じる
            if (mpCnn != null)
            {
                mpCnn.Close();
                Trace.WriteLine("MySQL から切断しました。");
            }
        }

        /// <summary>
    	/// MySQL テーブル情報取得 (USER_TAB_COLUMNS, USER_COL_COMMENTS)
        /// </summary>
        /// <param name="dataSet">データセット</param>
        /// <param name="tableID">テーブル ID</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int GetMySqlTableInfo(ref DataSet dataSet, string tableID)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                // テーブル情報取得 SQL 構文編集
                string sql = EditMySqlTableInfoQuerySql(tableID);
                using (MySqlCommand myCmd = new MySqlCommand(sql,cnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
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
            cmn.Dbm.CloseMySqlSchema(cnn);
            return ret;
        }

        /// <summary>
        /// MySQL テーブル情報取得 SQL 構文編集 (USER_TAB_COLUMNS, USER_COL_COMMENTS)
        /// </summary>
        /// <param name="tableID">テーブル ID</param>
        /// <returns>SQL 構文</returns>
        private string EditMySqlTableInfoQuerySql(string tableID)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string sql = "select distinct "
                       + "TABLE_NAME as TABLE_NAME, "
                       + "COLUMN_NAME as COLUMN_NAME, "
                       + "DATA_TYPE as DATA_TYPE, "
                       + "CHARACTER_OCTET_LENGTH as DATA_LENGTH, "
                       + "NUMERIC_PRECISION as DATA_PRECISION, "
                       + "NUMERIC_SCALE as DATA_SCALE, "
                       + "IS_NULLABLE as NULLABLE, "
                       + "ORDINAL_POSITION as COLUMN_ID, "
                       + "COLUMN_COMMENT as COMMENTS "
                       + "from "
                       + "INFORMATION_SCHEMA.COLUMNS "
                       + "where "
                       + "TABLE_NAME = '" + tableID + "' "
                       + "order by "
                       + "TABLE_NAME, "
                       + "COLUMN_ID";
            return sql;
        }

        /// <summary>
    	/// MySQL テーブル列名一覧取得 (USER_TAB_COLUMNS)
        /// </summary>
        /// <param name="tableName">テーブル名称</param>
        /// <param name="dataSet">データセット</param>
        /// <returns>結果 (0: 正常、1: 異常)</returns>
        public int GetMySqlTableColumnNameList(string tableName, ref DataSet dataSet)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = -1;
            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);
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

                using (MySqlCommand myCmd = new MySqlCommand(sql,cnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
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
                        ret = 1;
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
                ret = -1;
            }
            // 接続を閉じる
            cmn.Dbm.CloseMySqlSchema(cnn);
            return ret;
        }
    }
}
