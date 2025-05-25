using Mysqlx.Crud;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace MPPPS
{
    /// <summary>
	/// データベース アクセス クラス (EM テーブル)
    /// </summary>
    public partial class DBAccessor
    {
        /// <summary>
    	/// EM 利用権限確認 (M0010 担当者マスター)
        /// </summary>
        /// <param name="isPasswdFree">パスワード不要か</param>
        /// <param name="tanNm">担当者名称</param>
        /// <param name="atgCd">権限グループコード</param>
        /// <returns>権限あり</returns>
        public bool IsAuthrizedEMUser(bool isPasswdFree, ref string tanNm, ref string atgCd)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            OracleConnection cnn = null;

            try
            {
                // EM データベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref cnn);

                string sql;
                if (isPasswdFree)
                {
                    sql = "SELECT "
                        + "TANCD, "
                        + "TANNM, "
                        + "PASSWD, "
                        + "ATGCD "
                        + "FROM "
                        + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0010 + " "
                        + "WHERE "
                        + "TANCD = '" + cmn.IkM0010.TanCd + "'"
                        ;
                }
                else
                {
                    sql = "SELECT "
                        + "TANCD, "
                        + "TANNM, "
                        + "PASSWD, "
                        + "ATGCD "
                        + "FROM "
                        + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0010 + " "
                        + "WHERE "
                        + "TANCD = '" + cmn.IkM0010.TanCd + "' AND "
                        + "PASSWD = '" + cmn.IkM0010.Passwd + "'"
                        ;
                }
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
                                Debug.Write("TANCD = " + dr[0] + ", ");
                                Debug.Write("TANNM = " + dr[1] + ", ");
                                Debug.Write("PASSWD = " + dr[2] + ", ");
                                Debug.Write("ATGCD = " + dr[3]);
                                Debug.WriteLine("");
                                tanNm = dr[1].ToString();
                                atgCd = dr[3].ToString();
                                ret = true;
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
                ret = false;
            }
            // 接続を閉じる
            cmn.Dbm.CloseOraSchema(cnn);
            return ret;
        }

        /// <summary>
        /// 担当者情報取得 (M0010 担当者マスター)
        /// </summary>
        /// <param name="dataSet">データセット</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int GetUserInfo(ref DataSet dataSet)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            OracleConnection cnn = null;

            try
            {
                // EM データベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref cnn);

                // SQL 構文を編集
                string sql = EditUserInfoQuery();
                // 検索
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
                                Debug.Write("TANCD = " + dr[0] + ", ");
                                Debug.Write("TANNM = " + dr[1] + ", ");
                                Debug.Write("ATGCD = " + dr[3] + ", ");
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
        /// 担当者情報取得 SQL 構文編集 (M0010 担当者マスター) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditUserInfoQuery()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string sql = "SELECT "
                       + "TANCD, "
                       + "TANNM, "
                       + "PASSWD, "
                       + "ATGCD "
                       + "FROM "
                       + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0010 + " "
                       + "WHERE "
                       + "TANCD = '" + cmn.PkM0010.TanCd + "'"
                       ;

                return sql;
        }

        /// <summary>
        /// 手配先情報取得 (M0300 手配先名称マスター)
        /// </summary>
        /// <param name="dataSet">データセット</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int GetOrderInfo(ref DataSet dataSet)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            OracleConnection cnn = null;

            try
            {
                // EM データベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref cnn);

                // SQL 構文を編集
                string sql = EditOrderInfoQuery();
                // 検索
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
                                Debug.Write("ODCD = "   + dr[0]  + ", ");
                                Debug.Write("ODRNM = "  + dr[3]  + ", ");
                                Debug.Write("IOKBNM = " + dr[11] + ", ");
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
        /// 手配先情報取得 SQL 構文編集 (M0300 手配先名称マスター) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditOrderInfoQuery()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string sql;
            if (cmn.IkM0300.IsTiedToKt) // 工程への紐付けあり
            {
                // 手配先情報取得 SQL 構文編集 (一部)
                sql = EditOrderInfoQueryPartial();
            }
            else // 全件
            {
                // 手配先情報取得 SQL 構文編集 (全件)
                sql = EditOrderInfoQueryAll();
            }
            return sql;
        }

        /// 手配先情報取得 SQL 構文編集 (一部) (M0300 手配先名称マスター) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditOrderInfoQueryPartial()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string sql;
            if (!cmn.IkM0300.IsInclusionSubCo) // 子会社を含まない
            {
                sql = "select distinct "
                    + Common.TABLE_ID_M0300 + ".ODCD, "
                    + Common.TABLE_ID_M0300 + ".ODNM1, "
                    + Common.TABLE_ID_M0300 + ".ODNM2, "
                    + Common.TABLE_ID_M0300 + ".ODRNM, "
                    + Common.TABLE_ID_M0300 + ".ODTANNM, "
                    + Common.TABLE_ID_M0300 + ".ZIP, "
                    + Common.TABLE_ID_M0300 + ".ADD1, "
                    + Common.TABLE_ID_M0300 + ".ADD2, "
                    + Common.TABLE_ID_M0300 + ".TEL, "
                    + Common.TABLE_ID_M0300 + ".FAX, "
                    + Common.TABLE_ID_M0300 + ".MAIL, "
                    + Common.TABLE_ID_M0300 + ".IOKBN, "
                    + Common.TABLE_ID_M0300 + ".INSTID, "
                    + Common.TABLE_ID_M0300 + ".INSTDT, "
                    + Common.TABLE_ID_M0300 + ".UPDTID, "
                    + Common.TABLE_ID_M0300 + ".UPDTDT "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0300 + ", "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0410 + " "
                    + "where "
                    + Common.TABLE_ID_M0300 + ".ODCD like '" + cmn.IkM0300.OdCd + "' and "
                    + Common.TABLE_ID_M0300 + ".IOKBN like '" + cmn.IkM0300.IOKbn + "' and "
                    + Common.TABLE_ID_M0410 + ".ODCD = " + Common.TABLE_ID_M0300 + ".ODCD "
                    + "order by " + Common.TABLE_ID_M0300 + ".ODCD "
                    ;
            }
            else // 子会社を含む
            {
                sql = "select distinct "
                    + Common.TABLE_ID_M0300 + ".ODCD, "
                    + Common.TABLE_ID_M0300 + ".ODNM1, "
                    + Common.TABLE_ID_M0300 + ".ODNM2, "
                    + Common.TABLE_ID_M0300 + ".ODRNM, "
                    + Common.TABLE_ID_M0300 + ".ODTANNM, "
                    + Common.TABLE_ID_M0300 + ".ZIP, "
                    + Common.TABLE_ID_M0300 + ".ADD1, "
                    + Common.TABLE_ID_M0300 + ".ADD2, "
                    + Common.TABLE_ID_M0300 + ".TEL, "
                    + Common.TABLE_ID_M0300 + ".FAX, "
                    + Common.TABLE_ID_M0300 + ".MAIL, "
                    + Common.TABLE_ID_M0300 + ".IOKBN, "
                    + Common.TABLE_ID_M0300 + ".INSTID, "
                    + Common.TABLE_ID_M0300 + ".INSTDT, "
                    + Common.TABLE_ID_M0300 + ".UPDTID, "
                    + Common.TABLE_ID_M0300 + ".UPDTDT "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0300 + " " + ", "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0410 + " " + " "
                    + "where "
                    + Common.TABLE_ID_M0300 + ".ODCD like '" + cmn.IkM0300.OdCd + "' and "
                    + Common.TABLE_ID_M0300 + ".IOKBN like '" + cmn.IkM0300.IOKbn + "' and "
                    + Common.TABLE_ID_M0410 + ".ODCD = " + Common.TABLE_ID_M0300 + ".ODCD "
                    + "union all "
                    + "select "
                    + Common.TABLE_ID_M0300 + ".ODCD, "
                    + Common.TABLE_ID_M0300 + ".ODNM1, "
                    + Common.TABLE_ID_M0300 + ".ODNM2, "
                    + Common.TABLE_ID_M0300 + ".ODRNM, "
                    + Common.TABLE_ID_M0300 + ".ODTANNM, "
                    + Common.TABLE_ID_M0300 + ".ZIP, "
                    + Common.TABLE_ID_M0300 + ".ADD1, "
                    + Common.TABLE_ID_M0300 + ".ADD2, "
                    + Common.TABLE_ID_M0300 + ".TEL, "
                    + Common.TABLE_ID_M0300 + ".FAX, "
                    + Common.TABLE_ID_M0300 + ".MAIL, "
                    + Common.TABLE_ID_M0300 + ".IOKBN, "
                    + Common.TABLE_ID_M0300 + ".INSTID, "
                    + Common.TABLE_ID_M0300 + ".INSTDT, "
                    + Common.TABLE_ID_M0300 + ".UPDTID, "
                    + Common.TABLE_ID_M0300 + ".UPDTDT "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0300 + " "
                    + "where "
                    + Common.TABLE_ID_M0300 + ".ODCD = '" + Common.M0300_ODCD_ISHIGURO_MFG + "' " // ㈱石黒製作
                    + "order by ODCD "
                    ;
            }
            return sql;
        }

        /// 手配先情報取得 SQL 構文編集 (全件) (M0300 手配先名称マスター) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditOrderInfoQueryAll()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string sql;
            if (!cmn.IkM0300.IsInclusionSubCo) // 子会社を含まない
            {
                sql = "select distinct "
                    + "ODCD, "
                    + "ODNM1, "
                    + "ODNM2, "
                    + "ODRNM, "
                    + "ODTANNM, "
                    + "ZIP, "
                    + "ADD1, "
                    + "ADD2, "
                    + "TEL, "
                    + "FAX, "
                    + "MAIL, "
                    + "IOKBN, "
                    + "INSTID, "
                    + "INSTDT, "
                    + "UPDTID, "
                    + "UPDTDT "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0300 + " "
                    + "where "
                    + "ODCD like '" + cmn.IkM0300.OdCd + "' and "
                    + "IOKBN like '" + cmn.IkM0300.IOKbn + "' "
                    + "order by ODCD "
                    ;
            }
            else // 子会社を含む
            {
                sql = "select "
                    + "ODCD, "
                    + "ODNM1, "
                    + "ODNM2, "
                    + "ODRNM, "
                    + "ODTANNM, "
                    + "ZIP, "
                    + "ADD1, "
                    + "ADD2, "
                    + "TEL, "
                    + "FAX, "
                    + "MAIL, "
                    + "IOKBN, "
                    + "INSTID, "
                    + "INSTDT, "
                    + "UPDTID, "
                    + "UPDTDT "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0300 + " "
                    + "where "
                    + "ODCD like '" + cmn.IkM0300.OdCd + "' and "
                    + "IOKBN like '" + cmn.IkM0300.IOKbn + "' "
                    + "union all "
                    + "select "
                    + "ODCD, "
                    + "ODNM1, "
                    + "ODNM2, "
                    + "ODRNM, "
                    + "ODTANNM, "
                    + "ZIP, "
                    + "ADD1, "
                    + "ADD2, "
                    + "TEL, "
                    + "FAX, "
                    + "MAIL, "
                    + "IOKBN, "
                    + "INSTID, "
                    + "INSTDT, "
                    + "UPDTID, "
                    + "UPDTDT "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0300 + " "
                    + "where "
                    + "ODCD = '" + Common.M0300_ODCD_ISHIGURO_MFG + "' " // ㈱石黒製作
                    + "order by ODCD "
                    ;
            }
            return sql;
        }

        /// <summary>
        /// 工程情報取得 (M0410 工程マスター)
        /// </summary>
        /// <param name="dataSet">データセット</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int GetKtInfo(ref DataSet dataSet)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            OracleConnection cnn = null;

            try
            {
                // EM データベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref cnn);

                // SQL 構文を編集
                string sql = EditKtInfoQuery();
                // 検索
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
                                Debug.Write("KTCD = "   + dr[0] + ", ");
                                Debug.Write("KTNM = "   + dr[1] + ", ");
                                Debug.Write("KTGCD = "  + dr[2] + ", ");
                                Debug.Write("ODCD = "   + dr[3] + ", ");
                                Debug.Write("ODRKBN = " + dr[6] + ", ");
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
        /// 工程情報取得 SQL 構文編集 (M0410 工程マスター) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditKtInfoQuery()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string sql;
            if (cmn.IkM0410.IsTiedToOd) // 手配先への紐付けあり
            {
                // 工程情報取得 SQL 構文編集 (一部)
                sql = EditKtInfoQueryPartial();
            }
            else // 全件
            {
                // 工程情報取得 SQL 構文編集 (全件)
                sql = EditKtInfoQueryAll();
            }
            return sql;
        }

        /// 工程情報取得 SQL 構文編集 (一部) (M0410 工程マスター) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditKtInfoQueryPartial()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string sql;
            if (!cmn.IkM0410.IsInclusionSubCo) // 子会社を含まない
            {
                sql = "select distinct "
                    + Common.TABLE_ID_M0410 + ".KTCD, "
                    + Common.TABLE_ID_M0410 + ".KTNM, "
                    + Common.TABLE_ID_M0410 + ".KTGCD, "
                    + Common.TABLE_ID_M0410 + ".ODCD, "
                    + Common.TABLE_ID_M0410 + ".SHINDO, "
                    + Common.TABLE_ID_M0410 + ".TENKAI, "
                    + Common.TABLE_ID_M0410 + ".ODRKBN, "
                    + Common.TABLE_ID_M0410 + ".LOTKBN, "
                    + Common.TABLE_ID_M0410 + ".ODANLT, "
                    + Common.TABLE_ID_M0410 + ".TRIALQTY, "
                    + Common.TABLE_ID_M0410 + ".UNITQTY, "
                    + Common.TABLE_ID_M0410 + ".UNITNM, "
                    + Common.TABLE_ID_M0410 + ".HUNITNM, "
                    + Common.TABLE_ID_M0410 + ".BFLT, "
                    + Common.TABLE_ID_M0410 + ".AFLT, "
                    + Common.TABLE_ID_M0410 + ".IDANLT, "
                    + Common.TABLE_ID_M0410 + ".ODRLT, "
                    + Common.TABLE_ID_M0410 + ".SAFELT, "
                    + Common.TABLE_ID_M0410 + ".MOLT, "
                    + Common.TABLE_ID_M0410 + ".QCLT, "
                    + Common.TABLE_ID_M0410 + ".YOLT, "
                    + Common.TABLE_ID_M0410 + ".JIKBN, "
                    + Common.TABLE_ID_M0410 + ".QKSKBN, "
                    + Common.TABLE_ID_M0410 + ".INSTID, "
                    + Common.TABLE_ID_M0410 + ".INSTDT, "
                    + Common.TABLE_ID_M0410 + ".UPDTID, "
                    + Common.TABLE_ID_M0410 + ".UPDTDT, "
                    + Common.TABLE_ID_M0410 + ".BUHIN, "
                    + Common.TABLE_ID_M0410 + ".CPKTCD, "
                    + Common.TABLE_ID_M0410 + ".KTPRICE "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0410 + ", "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0300 + " "
                    + "where "
                    + Common.TABLE_ID_M0410 + ".KTCD like '" + cmn.IkM0410.KtCd + "' and "
                    + Common.TABLE_ID_M0410 + ".ODCD like '" + cmn.IkM0410.OdCd + "' and "
                    + Common.TABLE_ID_M0410 + ".ODRKBN like '" + cmn.IkM0410.OdrKbn + "' and "
                    + Common.TABLE_ID_M0300 + ".ODCD = " + Common.TABLE_ID_M0410 + ".ODCD "
                    + "order by " + Common.TABLE_ID_M0410 + ".KTCD "
                    ;
            }
            else // 子会社を含む
            {
                sql = "select distinct "
                    + Common.TABLE_ID_M0410 + ".KTCD, "
                    + Common.TABLE_ID_M0410 + ".KTNM, "
                    + Common.TABLE_ID_M0410 + ".KTGCD, "
                    + Common.TABLE_ID_M0410 + ".ODCD, "
                    + Common.TABLE_ID_M0410 + ".SHINDO, "
                    + Common.TABLE_ID_M0410 + ".TENKAI, "
                    + Common.TABLE_ID_M0410 + ".ODRKBN, "
                    + Common.TABLE_ID_M0410 + ".LOTKBN, "
                    + Common.TABLE_ID_M0410 + ".ODANLT, "
                    + Common.TABLE_ID_M0410 + ".TRIALQTY, "
                    + Common.TABLE_ID_M0410 + ".UNITQTY, "
                    + Common.TABLE_ID_M0410 + ".UNITNM, "
                    + Common.TABLE_ID_M0410 + ".HUNITNM, "
                    + Common.TABLE_ID_M0410 + ".BFLT, "
                    + Common.TABLE_ID_M0410 + ".AFLT, "
                    + Common.TABLE_ID_M0410 + ".IDANLT, "
                    + Common.TABLE_ID_M0410 + ".ODRLT, "
                    + Common.TABLE_ID_M0410 + ".SAFELT, "
                    + Common.TABLE_ID_M0410 + ".MOLT, "
                    + Common.TABLE_ID_M0410 + ".QCLT, "
                    + Common.TABLE_ID_M0410 + ".YOLT, "
                    + Common.TABLE_ID_M0410 + ".JIKBN, "
                    + Common.TABLE_ID_M0410 + ".QKSKBN, "
                    + Common.TABLE_ID_M0410 + ".INSTID, "
                    + Common.TABLE_ID_M0410 + ".INSTDT, "
                    + Common.TABLE_ID_M0410 + ".UPDTID, "
                    + Common.TABLE_ID_M0410 + ".UPDTDT, "
                    + Common.TABLE_ID_M0410 + ".BUHIN, "
                    + Common.TABLE_ID_M0410 + ".CPKTCD, "
                    + Common.TABLE_ID_M0410 + ".KTPRICE "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0410 + ", "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0300 + " "
                    + "where "
                    + Common.TABLE_ID_M0410 + ".KTCD like '" + cmn.IkM0410.KtCd + "' and "
                    + Common.TABLE_ID_M0410 + ".ODCD like '" + cmn.IkM0410.OdCd + "' and "
                    + Common.TABLE_ID_M0410 + ".ODRKBN like '" + cmn.IkM0410.OdrKbn + "' and "
                    + Common.TABLE_ID_M0300 + ".ODCD = " + Common.TABLE_ID_M0410 + ".ODCD "
                    + "union all "
                    + "select "
                    + Common.TABLE_ID_M0410 + ".KTCD, "
                    + Common.TABLE_ID_M0410 + ".KTNM, "
                    + Common.TABLE_ID_M0410 + ".KTGCD, "
                    + Common.TABLE_ID_M0410 + ".ODCD, "
                    + Common.TABLE_ID_M0410 + ".SHINDO, "
                    + Common.TABLE_ID_M0410 + ".TENKAI, "
                    + Common.TABLE_ID_M0410 + ".ODRKBN, "
                    + Common.TABLE_ID_M0410 + ".LOTKBN, "
                    + Common.TABLE_ID_M0410 + ".ODANLT, "
                    + Common.TABLE_ID_M0410 + ".TRIALQTY, "
                    + Common.TABLE_ID_M0410 + ".UNITQTY, "
                    + Common.TABLE_ID_M0410 + ".UNITNM, "
                    + Common.TABLE_ID_M0410 + ".HUNITNM, "
                    + Common.TABLE_ID_M0410 + ".BFLT, "
                    + Common.TABLE_ID_M0410 + ".AFLT, "
                    + Common.TABLE_ID_M0410 + ".IDANLT, "
                    + Common.TABLE_ID_M0410 + ".ODRLT, "
                    + Common.TABLE_ID_M0410 + ".SAFELT, "
                    + Common.TABLE_ID_M0410 + ".MOLT, "
                    + Common.TABLE_ID_M0410 + ".QCLT, "
                    + Common.TABLE_ID_M0410 + ".YOLT, "
                    + Common.TABLE_ID_M0410 + ".JIKBN, "
                    + Common.TABLE_ID_M0410 + ".QKSKBN, "
                    + Common.TABLE_ID_M0410 + ".INSTID, "
                    + Common.TABLE_ID_M0410 + ".INSTDT, "
                    + Common.TABLE_ID_M0410 + ".UPDTID, "
                    + Common.TABLE_ID_M0410 + ".UPDTDT, "
                    + Common.TABLE_ID_M0410 + ".BUHIN, "
                    + Common.TABLE_ID_M0410 + ".CPKTCD, "
                    + Common.TABLE_ID_M0410 + ".KTPRICE "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0410 + " "
                    + "where "
                    + Common.TABLE_ID_M0410 + ".ODCD = '" + Common.M0410_ODCD_ISHIGURO_MFG + "' " // ㈱石黒製作
                    + "order by ODCD "
                    ;
            }
            return sql;
        }

        /// 工程情報取得 SQL 構文編集 (全件) (M0410 工程マスター) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditKtInfoQueryAll()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string sql;
            if (!cmn.IkM0410.IsInclusionSubCo) // 子会社を含まない
            {
                sql = "select distinct "
                    + Common.TABLE_ID_M0410 + ".KTCD, "
                    + Common.TABLE_ID_M0410 + ".KTNM, "
                    + Common.TABLE_ID_M0410 + ".KTGCD, "
                    + Common.TABLE_ID_M0410 + ".ODCD, "
                    + Common.TABLE_ID_M0410 + ".SHINDO, "
                    + Common.TABLE_ID_M0410 + ".TENKAI, "
                    + Common.TABLE_ID_M0410 + ".ODRKBN, "
                    + Common.TABLE_ID_M0410 + ".LOTKBN, "
                    + Common.TABLE_ID_M0410 + ".ODANLT, "
                    + Common.TABLE_ID_M0410 + ".TRIALQTY, "
                    + Common.TABLE_ID_M0410 + ".UNITQTY, "
                    + Common.TABLE_ID_M0410 + ".UNITNM, "
                    + Common.TABLE_ID_M0410 + ".HUNITNM, "
                    + Common.TABLE_ID_M0410 + ".BFLT, "
                    + Common.TABLE_ID_M0410 + ".AFLT, "
                    + Common.TABLE_ID_M0410 + ".IDANLT, "
                    + Common.TABLE_ID_M0410 + ".ODRLT, "
                    + Common.TABLE_ID_M0410 + ".SAFELT, "
                    + Common.TABLE_ID_M0410 + ".MOLT, "
                    + Common.TABLE_ID_M0410 + ".QCLT, "
                    + Common.TABLE_ID_M0410 + ".YOLT, "
                    + Common.TABLE_ID_M0410 + ".JIKBN, "
                    + Common.TABLE_ID_M0410 + ".QKSKBN, "
                    + Common.TABLE_ID_M0410 + ".INSTID, "
                    + Common.TABLE_ID_M0410 + ".INSTDT, "
                    + Common.TABLE_ID_M0410 + ".UPDTID, "
                    + Common.TABLE_ID_M0410 + ".UPDTDT, "
                    + Common.TABLE_ID_M0410 + ".BUHIN, "
                    + Common.TABLE_ID_M0410 + ".CPKTCD, "
                    + Common.TABLE_ID_M0410 + ".KTPRICE "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0410 + " "
                    + "where "
                    + "KTCD like '" + cmn.IkM0410.KtCd + "' and "
                    + "ODRKBN like '" + cmn.IkM0410.OdrKbn + "' "
                    + "order by " + Common.TABLE_ID_M0410 + ".KTCD "
                    ;
            }
            else // 子会社を含む
            {
                sql = "select "
                    + Common.TABLE_ID_M0410 + ".KTCD, "
                    + Common.TABLE_ID_M0410 + ".KTNM, "
                    + Common.TABLE_ID_M0410 + ".KTGCD, "
                    + Common.TABLE_ID_M0410 + ".ODCD, "
                    + Common.TABLE_ID_M0410 + ".SHINDO, "
                    + Common.TABLE_ID_M0410 + ".TENKAI, "
                    + Common.TABLE_ID_M0410 + ".ODRKBN, "
                    + Common.TABLE_ID_M0410 + ".LOTKBN, "
                    + Common.TABLE_ID_M0410 + ".ODANLT, "
                    + Common.TABLE_ID_M0410 + ".TRIALQTY, "
                    + Common.TABLE_ID_M0410 + ".UNITQTY, "
                    + Common.TABLE_ID_M0410 + ".UNITNM, "
                    + Common.TABLE_ID_M0410 + ".HUNITNM, "
                    + Common.TABLE_ID_M0410 + ".BFLT, "
                    + Common.TABLE_ID_M0410 + ".AFLT, "
                    + Common.TABLE_ID_M0410 + ".IDANLT, "
                    + Common.TABLE_ID_M0410 + ".ODRLT, "
                    + Common.TABLE_ID_M0410 + ".SAFELT, "
                    + Common.TABLE_ID_M0410 + ".MOLT, "
                    + Common.TABLE_ID_M0410 + ".QCLT, "
                    + Common.TABLE_ID_M0410 + ".YOLT, "
                    + Common.TABLE_ID_M0410 + ".JIKBN, "
                    + Common.TABLE_ID_M0410 + ".QKSKBN, "
                    + Common.TABLE_ID_M0410 + ".INSTID, "
                    + Common.TABLE_ID_M0410 + ".INSTDT, "
                    + Common.TABLE_ID_M0410 + ".UPDTID, "
                    + Common.TABLE_ID_M0410 + ".UPDTDT, "
                    + Common.TABLE_ID_M0410 + ".BUHIN, "
                    + Common.TABLE_ID_M0410 + ".CPKTCD, "
                    + Common.TABLE_ID_M0410 + ".KTPRICE "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0410 + " "
                    + "where "
                    + "KTCD like '" + cmn.IkM0410.KtCd + "' and "
                    + "ODRKBN like '" + cmn.IkM0410.OdrKbn + "' "
                    + "union all "
                    + "select "
                    + Common.TABLE_ID_M0410 + ".KTCD, "
                    + Common.TABLE_ID_M0410 + ".KTNM, "
                    + Common.TABLE_ID_M0410 + ".KTGCD, "
                    + Common.TABLE_ID_M0410 + ".ODCD, "
                    + Common.TABLE_ID_M0410 + ".SHINDO, "
                    + Common.TABLE_ID_M0410 + ".TENKAI, "
                    + Common.TABLE_ID_M0410 + ".ODRKBN, "
                    + Common.TABLE_ID_M0410 + ".LOTKBN, "
                    + Common.TABLE_ID_M0410 + ".ODANLT, "
                    + Common.TABLE_ID_M0410 + ".TRIALQTY, "
                    + Common.TABLE_ID_M0410 + ".UNITQTY, "
                    + Common.TABLE_ID_M0410 + ".UNITNM, "
                    + Common.TABLE_ID_M0410 + ".HUNITNM, "
                    + Common.TABLE_ID_M0410 + ".BFLT, "
                    + Common.TABLE_ID_M0410 + ".AFLT, "
                    + Common.TABLE_ID_M0410 + ".IDANLT, "
                    + Common.TABLE_ID_M0410 + ".ODRLT, "
                    + Common.TABLE_ID_M0410 + ".SAFELT, "
                    + Common.TABLE_ID_M0410 + ".MOLT, "
                    + Common.TABLE_ID_M0410 + ".QCLT, "
                    + Common.TABLE_ID_M0410 + ".YOLT, "
                    + Common.TABLE_ID_M0410 + ".JIKBN, "
                    + Common.TABLE_ID_M0410 + ".QKSKBN, "
                    + Common.TABLE_ID_M0410 + ".INSTID, "
                    + Common.TABLE_ID_M0410 + ".INSTDT, "
                    + Common.TABLE_ID_M0410 + ".UPDTID, "
                    + Common.TABLE_ID_M0410 + ".UPDTDT, "
                    + Common.TABLE_ID_M0410 + ".BUHIN, "
                    + Common.TABLE_ID_M0410 + ".CPKTCD, "
                    + Common.TABLE_ID_M0410 + ".KTPRICE "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0410 + " "
                    + "where "
                    + "ODCD = '" + Common.M0410_ODCD_ISHIGURO_MFG + "' " // ㈱石黒製作
                    + "order by " + Common.TABLE_ID_M0410 + ".KTCD "
                    ;
            }
            return sql;
        }

        /// <summary>
        /// 品目情報取得 (M0500 品目マスター)
        /// </summary>
        /// <param name="dataSet">データセット</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int GetItemInfo(ref DataSet dataSet)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            OracleConnection cnn = null;

            try
            {
                // EM データベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref cnn);

                // SQL 構文を編集
                string sql = EditItemInfoQuery();
                // 検索
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
                                Debug.Write("HMCD = " + dr[0] + ", ");
                                Debug.Write("HMRNM = " + dr[1] + " ");
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
        /// 品目情報取得 SQL 構文編集 (M0500 品目マスター) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditItemInfoQuery()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string sql = "select "
                       + "HMCD,"
                       + "HMRNM "
                       + "from "
                       + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0500 + " "
                       + "where "
                       + "HMCD in (" + cmn.PkM0500.HmCd + ") "
                       + "order by "
                       + "HMCD "
                       ;

            return sql;
        }

        /// <summary>
        /// 「M0500 品目マスター」検索
        /// </summary>
        /// <param name="dataSet">データセット</param>
        /// <param name="tableName">テーブル名称</param>
        public void SelectProdMaster(ref DataSet dataSet, string tableName, string Tbx_HmCd)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            OracleConnection cnn = null;

            try
            {
                // EM データベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref cnn);

                // 「M0500 品目マスター」テーブル検索　SQL
                // DBAccessor.csでSQL実行コーディング（try catch）　⇒　Frm31_InqProdInfo.csで呼び出す。

                //var Tbx1HmCd = Tbx_HmCd.Text;
                //var Tbx1HmCd = Tbx_HmCd;

                string sql = "SELECT "
                           + "HMCD, "
                           + "HMNM, "
                           + "HMRNM, "

                           //【NG】+ "--プルダウンメニュー項目［品目分類］【必須○】【1:製品 2:最終組立品 3:組立品 4:部品 5:原材料 6:消耗品 7:購入品 8:その他】 "
                           //--プルダウンメニュー項目［品目分類］【必須○】【1:製品 2:最終組立品 3:組立品 4:部品 5:原材料 6:消耗品 7:購入品 8:その他】
                           //+ "HMTYPE, " //【←不要★CASE式の時はItemArray[n]のカラム番号がずれる為】= dataSet.Tables[0].Rows[0].ItemArray[n].ToString();
                           + "CASE "
                           + "WHEN HMTYPE = 1    THEN '製品' "
                           + "WHEN HMTYPE = 2    THEN '最終組立品' "
                           + "WHEN HMTYPE = 3    THEN '組立品' "
                           + "WHEN HMTYPE = 4    THEN '部品' "
                           + "WHEN HMTYPE = 5    THEN '原材料' "
                           + "WHEN HMTYPE = 6    THEN '消耗品' "
                           + "WHEN HMTYPE = 7    THEN '購入品' "
                           + "WHEN HMTYPE = 8    THEN 'その他' "
                           //【NG】+ "ELSE                    '不正な値！' -- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値 "
                           //【NG】+ "ELSE                    '不正な値！' //-- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値 "
                           + "ELSE                    '不正な値！' " //-- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値
                           + "END, "

                           //　ラジオボタン項目［下位有無(BOMKBN)］【1:有 2:無】【有(_1)／無(_0)】
                           //+ "BOMKBN, " //【←不要★CASE式の時はItemArray[n]のカラム番号がずれる為】= dataSet.Tables[0].Rows[0].ItemArray[n].ToString();
                           + "CASE "
                           + "WHEN BOMKBN = 1    THEN '有' "
                           + "WHEN BOMKBN = 2    THEN '無' "
                           + "ELSE                    '★不正な値！' " //-- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値
                           + "END, "

                           //　ラジオボタン項目［手順有無(PROCESSKBN)］【1:有 2:無】【有(_1)／無(_0)】
                           //+ "PROCESSKBN, " //【←不要★CASE式の時はItemArray[n]のカラム番号がずれる為】= dataSet.Tables[0].Rows[0].ItemArray[n].ToString();
                           + "CASE "
                           + "WHEN PROCESSKBN = 1    THEN '有' "
                           + "WHEN PROCESSKBN = 2    THEN '無' "
                           + "ELSE                    '★不正な値！' " //-- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値
                           + "END, "

                           + "MAKER, "
                           + "HMKIND, "
                           + "MODEL, "
                           + "ZUBAN, "

                           //【NG】+ "--プルダウンメニュー項目［自動手配区分］【必須×】【1:する 2:しない】 "
                           //--プルダウンメニュー項目［自動手配区分］【必須×】【1:する 2:しない】
                           //+ "HTKBN, " //【←不要★ CASE式の時はItemArray[n]のカラム番号がずれる為】= dataSet.Tables[0].Rows[0].ItemArray[n].ToString();
                           + "CASE "
                           + "WHEN HTKBN = 1    THEN 'する' "
                           + "WHEN HTKBN = 2    THEN 'しない' "
                           //【NG】+ "ELSE                    '不正な値！' -- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値 "
                           + "ELSE                    '不正な値！' " //-- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値
                           + "END, "

                           //【NG】+ "--プルダウンメニュー項目［鋼材使用区分］【必須○】【1:する 2:しない 3:ｶｯﾄ材自身 4:母材自身】 "
                           //--プルダウンメニュー項目［鋼材使用区分］【必須○】【1:する 2:しない 3:ｶｯﾄ材自身 4:母材自身】
                           //+ "KZAIKBN, " //【←不要★ CASE式の時はItemArray[n]のカラム番号がずれる為】= dataSet.Tables[0].Rows[0].ItemArray[n].ToString();
                           + "CASE "
                           + "WHEN KZAIKBN = 1    THEN 'する' "
                           + "WHEN KZAIKBN = 2    THEN 'しない' "
                           + "WHEN KZAIKBN = 3    THEN 'ｶｯﾄ材自身' "
                           + "WHEN KZAIKBN = 4    THEN '母材自身' "
                           //【NG】+ "ELSE                    '不正な値！' -- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値 "
                           + "ELSE                    '不正な値！' " //-- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値
                           + "END, "

                           //【NG】+ "--プルダウンメニュー項目［手配区分］【必須×】【1:受給 2:自己手配 3:外注手配】 "
                           //--プルダウンメニュー項目［手配区分］【必須×】【1:受給 2:自己手配 3:外注手配】
                           //+ "ODRKBN, " //【←不要★ CASE式の時はItemArray[n]のカラム番号がずれる為】= dataSet.Tables[0].Rows[0].ItemArray[n].ToString();
                           + "CASE "
                           + "WHEN ODRKBN = 1    THEN '受給' "
                           + "WHEN ODRKBN = 2    THEN '自己手配' "
                           + "WHEN ODRKBN = 3    THEN '外注手配' "
                           //【NG】+ "ELSE                    '不正な値！' -- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値 "
                           + "ELSE                    '不正な値！' " //-- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値
                           + "END, "

                           + "ODCD1, "
                           + "ODCD2, "
                           + "BUCD, "
                           + "BOXCD, "
                           + "BOXQTY, "
                           + "UKCD, "

                           //　ラジオボタン項目［展開要否(TENKAI)］【1:要 2:否】【要(_1)／否(_0)】
                           //+ "TENKAI, "//【←不要★CASE式の時はItemArray[n]のカラム番号がずれる為】= dataSet.Tables[0].Rows[0].ItemArray[n].ToString();
                           + "CASE "
                           + "WHEN TENKAI = 1    THEN '要' "
                           + "WHEN TENKAI = 2    THEN '否' "
                           + "ELSE                    '★不正な値！' " //-- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値
                           + "END, "

                           //　ラジオボタン項目［指示書要否(SHIJI)］【1:要 2:否】【要(_1)／否(_0)】
                           //+ "SHIJI, "//【←不要★CASE式の時はItemArray[n]のカラム番号がずれる為】= dataSet.Tables[0].Rows[0].ItemArray[n].ToString();
                           + "CASE "
                           + "WHEN SHIJI = 1    THEN '要' "
                           + "WHEN SHIJI = 2    THEN '否' "
                           + "ELSE                    '★不正な値！' " //-- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値
                           + "END, "

                           //【NG】+ "--プルダウンメニュー項目［ﾛｯﾄ方式区分］【必須○】【1:EOQ 2:FPR 3:非指定】 "
                           //--プルダウンメニュー項目［ﾛｯﾄ方式区分］【必須○】【1:EOQ 2:FPR 3:非指定】
                           //+ "LOTKBN, " //【←不要★ CASE式の時はItemArray[n]のカラム番号がずれる為】= dataSet.Tables[0].Rows[0].ItemArray[n].ToString();
                           + "CASE "
                           + "WHEN LOTKBN = 1    THEN 'EOQ' "
                           + "WHEN LOTKBN = 2    THEN 'FPR' "
                           + "WHEN LOTKBN = 3    THEN '非指定' "
                           //【NG】+ "ELSE                    '不正な値！' -- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値 "
                           + "ELSE                    '不正な値！' " //-- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値
                           + "END, "

                           + "LOTQTY, "
                           + "TRIALQTY, "
                           + "CUTLT, "
                           + "FIXLT, "
                           + "HENLT, "
                           + "TKCD, "
                           + "QCNOTE, "
                           + "NOTE, "
                           + "SAFEQTY, "

                           //　ラジオボタン項目［内示分割(NJSEPKBN)］【1:要 2:否】【要(_1)／否(_0)】
                           //+ "NJSEPKBN, "//【←不要★CASE式の時はItemArray[n]のカラム番号がずれる為】= dataSet.Tables[0].Rows[0].ItemArray[n].ToString();
                           + "CASE "
                           + "WHEN NJSEPKBN = 1    THEN '要' "
                           + "WHEN NJSEPKBN = 2    THEN '否' "
                           + "ELSE                    '★不正な値！' " //-- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値
                           + "END, "

                           + "WKNOTE, "
                           + "WKCOMMENT, "
                           + "UKICD, "

                           //　ラジオボタン項目［YGW印刷区分(YGWKBN)］【1:要 2:否】【要(_1)／否(_0)】
                           //+ "YGWKBN, "//【←不要★CASE式の時はItemArray[n]のカラム番号がずれる為】= dataSet.Tables[0].Rows[0].ItemArray[n].ToString();
                           + "CASE "
                           + "WHEN YGWKBN = 1    THEN '要' "
                           + "WHEN YGWKBN = 2    THEN '否' "
                           + "ELSE                    '★不正な値！' " //-- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値
                           + "END, "

                           + "SKBOXCD, "
                           + "SKBOXQTY, "
                           + "SKBUCD, "
                           + "SKHIASU, "
                           + "SKNIS, "
                           + "SKWEIGHT, "
                           + "SKTNOTE1, "
                           + "SKTNOTE2, "
                           + "SKNOTE, "
                           + "SOOD, "
                           + "SOTC, "
                           + "SOLEN, "
                           + "WEIGHT, "
                           + "ZAINM, "
                           + "KJNM, "
                           + "SETULEN, "
                           + "SPOU1, "
                           + "SPOU2, "
                           + "SPOU3, "
                           + "INSTID, "
                           + "INSTDT, "
                           + "UPDTID, "
                           + "UPDTDT, "

                           //　ラジオボタン項目［ｼｭﾐﾚｰｼｮﾝ単価重量換算区分(WEIGHTKBN)］【1:要 2:否】【有(_1)／無(_0)】
                           //+ "WEIGHTKBN "//【←不要★CASE式の時はItemArray[n]のカラム番号がずれる為】= dataSet.Tables[0].Rows[0].ItemArray[n].ToString();
                           + "CASE "
                           + "WHEN WEIGHTKBN = 1    THEN '要' "
                           + "WHEN WEIGHTKBN = 2    THEN '否' "
                           + "ELSE                    '★不正な値！' " //-- 品目マスター（M0500）「Table定義書020 マスター.xls」に定義されていない値
                                                                 //+ "END, "
                           + "END "

                           + "FROM "
                           //+ "M0500 " //共通部品指定テーブル定義【[ConfigDB.xmlの<Schema>].[Common.csのテーブル定義]】を呼び出す
                           + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0500 + " "
                           + "WHERE "
                           + "HMCD = '" + Tbx_HmCd + "'";


                using (OracleCommand myCmd = new OracleCommand(sql, cnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataSet: " + tableName);
                        using (DataSet myDs = new DataSet())
                        {
                            // 結果取得
                            myDa.Fill(myDs, "emp");
                            dataSet = myDs;
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
        /// カレンダーマスタの本社稼働日を取得 (S0820 カレンダーマスタ)
        /// </summary>
        /// <param name="dataTable">データセット</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int GetWorkDays(ref DataTable dataTable)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            OracleConnection cnn = null;

            try
            {
                // EM データベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref cnn);

                // SQL 構文を編集
                string sql = "select "
                           + "YMD "
                           + "from "
                           + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_S0820 + " "
                           + "where "
                           + "CALTYP = '00001' "
                           + "and WKKBN = '1' "
                           + "and YMD between ADD_MONTHS(SYSDATE, -6) and ADD_MONTHS(SYSDATE, 9) "
                           ;
                // 検索
                using (OracleCommand myCmd = new OracleCommand(sql, cnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataSet:");
                        using (DataTable myDt = new DataTable())
                        {
                            // 結果取得
                            myDa.Fill(myDt);
                            dataTable = myDt;
                        }
                    }
                }
                ret = dataTable.Rows.Count;
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
        /// 注文情報データ取得
        /// </summary>
        /// <param name="orderDt">注文情報データ</param>
        /// <param name="firstDayOfMonth">検査対象月</param>
        /// <returns>注文情報データ</returns>
        public bool GetEmOrderSummaryInfo(ref DataTable orderDt, DateTime firstDayOfMonth)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            OracleConnection emCnn = null;

            try
            {
                // EMデータベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref emCnn);

                string yyyyMMdd = firstDayOfMonth.ToString("yyyy/MM/dd");
                string yyMM = firstDayOfMonth.AddMonths(-6).ToString("yyMM");
                string sql;
                sql = "SELECT "
                    + "EDDT "
                    + ", to_char(sum(case when ODRSTS = '2' then 1 else 0 end)) \"EM2確定件数\" "
                    + ", to_char(sum(case when ODRSTS = '3' then 1 else 0 end)) \"EM3着手件数\" "
                    + ", to_char(sum(case when ODRSTS = '4' then 1 else 0 end)) \"EM4完了件数\" "
                    + ", to_char(sum(case when ODRSTS = '9' then 1 else 0 end)) \"EM9取消件数\" "
                    + ", to_char(sum(case when ODRSTS in ('2','3','4','9') then 1 else 0 end)) \"EM合計件数\" "
                    + ", to_char(sum(case when ODRSTS = '2' then ODRQTY else 0 end)) \"EM2確定本数\" "
                    + ", to_char(sum(case when ODRSTS = '3' then ODRQTY-JIQTY else 0 end)) \"EM3着手本数\" "
                    + ", to_char(sum(case when ODRSTS = '4' then ODRQTY else 0 end)) \"EM4完了本数\" "
                    + ", to_char(sum(case when ODRSTS = '9' then ODRQTY else 0 end)) \"EM9取消本数\" "
                    + ", to_char(sum(case when ODRSTS in ('2','3','4','9') then ODRQTY else 0 end)) \"EM合計本数\" "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_D0410 + " "
                    + "WHERE "
                    + $"ODRNO > {yyMM}000000 " // EDDTにインデックスが貼ってないので検索対象をまず絞ってから抽出する
                    + "and ODCD like '6060%' "
                    + $"and EDDT between trunc(to_date('{yyyyMMdd}','YYYY/MM/DD'), 'MONTH') - 7 "
                    + $"and last_day(to_date('{yyyyMMdd}','YYYY/MM/DD')) + 14 "
                    + "GROUP BY EDDT "
                    + "ORDER BY EDDT "
                ;
                using (OracleCommand myCmd = new OracleCommand(sql, emCnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        using (DataTable myDt = new DataTable())
                        {
                            // 結果取得
                            myDa.Fill(myDt);
                            orderDt = myDt;
                            ret = true;
                        }
                    }
                }
                // 接続を閉じる
                cmn.Dbm.CloseOraSchema(emCnn);
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
            cmn.Dbm.CloseOraSchema(emCnn);
            return ret;
        }

        /// <summary>
        /// 注文情報データ取得
        /// </summary>
        /// <param name="emOrderDt">注文情報データ</param>
        /// <param name="whereIn">対象日</param>
        /// <returns>注文情報データ</returns>
        public bool GetEmOrder(ref DataTable emOrderDt, string eddt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            OracleConnection emCnn = null;

            try
            {
                // EMデータベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref emCnn);

                string yyMM = DateTime.Now.AddMonths(-6).ToString("yyMM");
                string sql;
                sql = "SELECT * "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_D0410 + " "
                    + "WHERE "
                    + $"ODRNO > {yyMM}000000 " // EDDTにインデックスが貼ってないので検索対象をまず絞ってから抽出する
                    + "and ODCD like '6060%' "
                    + $"and EDDT = '{eddt}' "
                ;
                using (OracleCommand myCmd = new OracleCommand(sql, emCnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        using (DataTable myDt = new DataTable())
                        {
                            // 結果取得
                            myDa.Fill(myDt);
                            emOrderDt = myDt;
                            ret = true;
                        }
                    }
                }
                // 接続を閉じる
                cmn.Dbm.CloseOraSchema(emCnn);
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
            cmn.Dbm.CloseOraSchema(emCnn);
            return ret;
        }

        /// <summary>
        /// 内示情報データ取得
        /// </summary>
        /// <param name="planDt">内示情報データ</param>
        /// <param name="firstDayOfMonth">検査対象月</param>
        /// <returns>注文情報データ</returns>
        public bool GetEmPlanSummaryInfo(ref DataTable planDt, DateTime firstDayOfMonth)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            OracleConnection emCnn = null;

            try
            {
                // EMデータベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref emCnn);

                string yyyyMMdd = firstDayOfMonth.AddMonths(3).ToString("yyyy/MM/dd");
                string sql;
                sql = "SELECT "
                    + "NEXT_DAY(EDDT - 7, 2) WEEKEDDT "
                    + ", HMCD "
                    + ", SUM(ODRQTY) \"EM本数\" "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_D0440 + " "
                    + "WHERE "
                        + "ODCD like '6060%' "
                        + $"and EDDT < to_date('{yyyyMMdd}','YYYY/MM/DD') "
                    + "GROUP BY "
                        + "NEXT_DAY(EDDT - 7, 2) ,HMCD "
                    + "ORDER BY "
                        + "NEXT_DAY(EDDT - 7, 2) ,HMCD "
                ;
                using (OracleCommand myCmd = new OracleCommand(sql, emCnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        using (DataTable myDt = new DataTable())
                        {
                            // 結果取得
                            myDa.Fill(myDt);
                            planDt = myDt;
                            ret = true;
                        }
                    }
                }
                // 接続を閉じる
                cmn.Dbm.CloseOraSchema(emCnn);
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
            cmn.Dbm.CloseOraSchema(emCnn);
            return ret;
        }

        /// <summary>
        /// 内示情報データ取得
        /// </summary>
        /// <param name="emPlanDt">注文情報データ</param>
        /// <returns>注文情報データ</returns>
        public bool GetEmPlan(ref DataTable emPlanDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            OracleConnection emCnn = null;

            try
            {
                // EMデータベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref emCnn);

                string sql;
                sql = "SELECT "
                    + Common.TABLE_ID_D0440 + ".*, NEXT_DAY(EDDT - 7, 2) WEEKEDDT "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_D0440 + " "
                    + "WHERE "
                    + "ODCD like '6060%' "
                    + "ORDER BY EDDT "
                ;
                using (OracleCommand myCmd = new OracleCommand(sql, emCnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        using (DataTable myDt = new DataTable())
                        {
                            // 結果取得
                            myDa.Fill(myDt);
                            emPlanDt = myDt;
                            ret = true;
                        }
                    }
                }
                // 接続を閉じる
                cmn.Dbm.CloseOraSchema(emCnn);
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
            cmn.Dbm.CloseOraSchema(emCnn);
            return ret;
        }

        /// <summary>
        /// 切削在庫データ取得（品目手順マスタの手配先コードが60600のもの）
        /// </summary>
        /// <param name="invInfoEMDt">在庫情報データ</param>
        /// <returns>注文情報データ</returns>
        public bool GetInvInfoEMDt(ref DataTable invInfoEMDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            OracleConnection emCnn = null;

            try
            {
                // EMデータベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref emCnn);

                string sql;
                sql = "SELECT a.HMCD, b.KTCD, NVL(b.ZAIQTY, 0) \"EMQTY\" "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0510 + " a "
                    + "LEFT OUTER JOIN "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_D0520 + " b "
                    + "ON "
                        + "b.HMCD = a.HMCD and "
                        + "b.ZAIQTY > 0 and "
                        + "(b.KTCD is NULL or (b.KTCD = a.KTCD)) " // ﾚｰｻﾞｰﾀﾚﾊﾟﾝは抜く  and b.KTCD <> 'MPCTLZ'
                    + "WHERE "
                    + "a.ODCD = '60600' and "
                    + "MOD(a.KTSEQ, 10) = 0 and "
                    + "a.VALDTF = (SELECT MAX(tmp.VALDTF) FROM "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0510 + " tmp "
                    + "WHERE tmp.HMCD = a.HMCD and tmp.VALDTF < SYSDATE) "
                ;
                using (OracleCommand myCmd = new OracleCommand(sql, emCnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(invInfoEMDt);
                        ret = true;
                    }
                }
                // 接続を閉じる
                cmn.Dbm.CloseOraSchema(emCnn);
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
            cmn.Dbm.CloseOraSchema(emCnn);
            return ret;
        }

        /// <summary>
        /// 注文情報データ手配状態取得
        /// </summary>
        /// <param name="orderDt">注文情報データ</param>
        /// <returns>注文情報データ</returns>
        public bool GetD0410ODRSTS(ref DataTable orderDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            string yyMM = DateTime.Now.AddMonths(-3).ToString("yyMM");
            string from = DateTime.Now.AddDays(-14).ToString("yyyy/MM/dd");
            string to = DateTime.Now.AddDays(14).ToString("yyyy/MM/dd");
            OracleConnection emCnn = null;

            try
            {
                // EMデータベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref emCnn);

                string sql;
                sql = "SELECT ODRNO, ODRSTS, JIQTY "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_D0410 + " "
                    + "WHERE "
                    + $"ODRNO > {yyMM}000000 " // EDDTにインデックスが貼ってないので検索対象をまず絞ってから抽出する
                    + "and ODCD like '6060%' "
                    + "and ODRSTS in ('3','4','9') "
                    + $"and EDDT between '{from}' and '{to}' "
                ;
                using (OracleCommand myCmd = new OracleCommand(sql, emCnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        using (DataTable myDt = new DataTable())
                        {
                            // 結果取得
                            myDa.Fill(orderDt);
                            ret = true;
                        }
                    }
                }
                // 接続を閉じる
                cmn.Dbm.CloseOraSchema(emCnn);
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
            cmn.Dbm.CloseOraSchema(emCnn);
            return ret;
        }

        /// <summary>
        /// 促進データ抽出
        /// </summary>
        /// <param name="dataTable">データセット</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int 促進データ抽出(ref DataTable dataTable)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            OracleConnection cnn = null;
            string sql = string.Empty;
            int ret = 0;

            try
            {
                // EM データベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref cnn);

                string from = string.Empty;
                string to = string.Empty;

                // 当日日付を取得（調整し易いようPCの日付を利用）
                DateTime today = DateTime.Today; //.AddDays(-15); // Debug用 ⇒ .AddDays(-xx)で月曜日にしてテスト

                // 今週の火曜日を計算
                DateTime thisTuesday = today.AddDays((DayOfWeek.Tuesday - today.DayOfWeek) % 7);

                // 0:日曜日、1:月曜日の場合は今週の火曜日を先週に巻き戻す
                if (today.DayOfWeek < DayOfWeek.Tuesday)
                {
                    thisTuesday = thisTuesday.AddDays(-7);

                    // 先週に稼働日があるか判定
                    from = thisTuesday.AddDays(-1).ToString("yyyy/MM/dd");
                    to = thisTuesday.AddDays(3).ToString("yyyy/MM/dd");
                    sql =
                        "select COUNT(*) as WKCNT "
                        + "from "
                        + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_S0820 + " "
                        + "where CALTYP='00001' "
                        + "and WKKBN='1' "
                        + $"and YMD between '{from}' and '{to}'"
                    ;
                    using (OracleCommand myCmd = new OracleCommand(sql, cnn))
                    {
                        using (OracleDataReader reader = myCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // 先週が長期休み等で稼働日が無い場合は１週間前にずらす
                                if (reader["WKCNT"].ToString() == "0") thisTuesday = thisTuesday.AddDays(-7);
                            }
                        }
                    }
                }

                // 来週の日付を取得
                DateTime oneWeeksLater = thisTuesday.AddDays(7);

                // 来週の金曜日を取得
                DateTime nextFriday = oneWeeksLater.AddDays((DayOfWeek.Friday - oneWeeksLater.DayOfWeek) % 7);

                // 来週に稼働日があるか判定
                from = nextFriday.AddDays(-4).ToString("yyyy/MM/dd");
                to = nextFriday.ToString("yyyy/MM/dd");
                sql =
                    "select COUNT(*) as WKCNT "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_S0820 + " "
                    + "where CALTYP='00001' "
                    + "and WKKBN='1' "
                    + $"and YMD between '{from}' and '{to}'"
                ;
                using (OracleCommand myCmd = new OracleCommand(sql, cnn))
                {
                    using (OracleDataReader reader = myCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // 来週が長期休み等で稼働日が無い場合は１週間先に延ばす
                            if (reader["WKCNT"].ToString() == "0") nextFriday = nextFriday.AddDays(7);
                        }
                    }
                }

                // 主キー検索での高速化
                string yyMM = today.AddMonths(-3).ToString("yyMM");

                // 遅れ手配データを取得し、今週の月曜日に集約させる
                sql = $"select a.HMCD \"品番\", b.HMRNM \"品目略称\", '{thisTuesday.AddDays(-1).ToString("yyyy/MM/dd")}' \"完了予定日\""
                    + ", sum(a.ODRQTY) \"手配数\", sum(a.JIQTY) \"実績数\", count(*) \"件数\", sum(a.ODRQTY-a.JIQTY) \"S\" "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_D0410 + " a, "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0500 + " b "
                    + "where "
                    + $"a.ODRNO > {yyMM}000000 " // EDDTにインデックスが貼ってないので検索対象をまず絞ってから抽出する
                    + "and a.HMCD = b.HMCD "
                    + "and a.ODRSTS in ('2', '3') "
                    + "and a.KTCD like 'MP%' "
                    + "and a.ODCD like '6060%' "
                    + $"and a.EDDT < '{thisTuesday.ToString("yyyy/MM/dd")}' "
                    + "group by a.HMCD, b.HMRNM "
                ;
                using (OracleCommand myCmd = new OracleCommand(sql, cnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            myDa.Fill(dt);
                            dataTable.Merge(dt);
                        }
                    }
                }

                // 今週来週の手配データ取得
                from = thisTuesday.ToString("yyyy/MM/dd");
                to = nextFriday.ToString("yyyy/MM/dd");
                sql = "select a.HMCD \"品番\", b.HMRNM \"品目略称\", to_char(a.EDDT,'YYYY/MM/DD') \"完了予定日\""
                    + ", sum(a.ODRQTY) \"手配数\", sum(a.JIQTY) \"実績数\", count(*) \"件数\", sum(a.ODRQTY-a.JIQTY) \"S\" "
                    + "from "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_D0410 + " a, "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_M0500 + " b "
                    + "where "
                    + $"a.ODRNO > {yyMM}000000 " // EDDTにインデックスが貼ってないので検索対象をまず絞ってから抽出する
                    + "and a.HMCD = b.HMCD "
                    + "and a.ODRSTS in ('2', '3') "
                    + "and a.KTCD like 'MP%' "
                    + "and a.ODCD like '6060%' "
                    + $"and a.EDDT between '{from}' and '{to}' "
                    + "group by a.HMCD, b.HMRNM, a.EDDT "
                ;
                using (OracleCommand myCmd = new OracleCommand(sql, cnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            myDa.Fill(dt);
                            dataTable.Merge(dt);
                        }
                    }
                }
                ret = dataTable.Rows.Count;
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




    }
}
