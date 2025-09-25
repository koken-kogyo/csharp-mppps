using DecryptPassword;
using Npgsql;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace MPPPS
{
    /// <summary>
	/// データベース管理クラス (共通メソッド, システム テーブル)
    /// </summary>
    public partial class DBManager
    {
        /// <summary>
        /// PostgreSQL データベース スキーマ接続成否
        /// </summary>
        /// <param name="dbConf">データベース設定</param>
        /// <param name="pgCnn">EM データベースへの接続クラス</param>
        /// <returns>結果 (false: 失敗, true: 成功)</returns>
        public bool IsConnectPostgreSQL(int dbConf, ref NpgsqlConnection pgCnn)
        {
            Common.s_Logger.Debug("{0} Method started", MethodBase.GetCurrentMethod().Name);

            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret;

            string user = cmn.DbCd[dbConf].User; // ユーザー

            // パスワード復号化
            var dpc = new DecryptPasswordClass();
            dpc.DecryptPassword(cmn.DbCd[dbConf].EncPasswd, out string decPasswd);

            // データソース
            string ds = "Host=" + cmn.DbCd[dbConf].Host + ";"
                    + "Port=" + cmn.DbCd[dbConf].Port + ";"
                    + "Database=" + cmn.DbCd[dbConf].Schema + ";";

            // PostgreSQL 接続文字列を組み立てる
            string connectString = ds
                    + "Username=" + user + "; "
                    + "Password=" + decPasswd + "; ";
            try
            {
                pgCnn = new NpgsqlConnection(connectString);

                // PostreSQL へのコネクションの確立
                pgCnn.Open();
                Trace.WriteLine("PostgreSQL に接続しました。");
                Trace.WriteLine(pgCnn.ServerVersion);
                ret = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("DBManager:\n" + e.Message);

                Trace.WriteLine("Exception Source = " + e.Source);
                Trace.WriteLine("Exception Message = " + e.Message);

                // 接続を閉じる
                ClosePgSchema(pgCnn);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// PostgreSQL データベース スキーマからの切断
        /// </summary>
        /// <param name="pgCnn">PostgreSQL データベースへの接続クラス</param>
        public void ClosePgSchema(NpgsqlConnection pgCnn)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 接続を閉じる
            if (pgCnn != null)
            {
                pgCnn.Close();
                Trace.WriteLine("PostgreSQL から切断しました。");
            }
        }


    }
}
