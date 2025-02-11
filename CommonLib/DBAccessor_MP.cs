using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace MPPPS
{
    /// <summary>
	/// データベース アクセス クラス (切削工程所管テーブル)
    /// </summary>
    public partial class DBAccessor
    {
        // 共通クラス
        private readonly Common cmn;

        /// <summary>
        /// デフォルト コンストラクタ
        /// </summary>
        public DBAccessor()
        {
        }

        public DBAccessor(Common cmn)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 共通クラス
            this.cmn = cmn;
        }

        /// <summary>
        /// 切削生産計画システム利用権限確認 (km8400 切削生産計画システム利用者マスター)★
        /// </summary>
        /// <param name="active">有効フラグ</param>
        /// <param name="authLv">権限レベル</param>
        /// <returns>権限あり</returns>
        public bool IsAuthrizedMPPPUser(ref string active, ref string authLv)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                string sql;
                sql = "SELECT "
                    + "USERID, "
                    + "ACTIVE, "
                    + "AUTHLV "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8400 + " "
                    + "WHERE "
                    + "USERID = '" + cmn.PkKM8400.UserId + "' "
                    ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
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
                                Debug.Write("USERID = " + dr[0] + ", ");
                                Debug.Write("ACTIVE = " + dr[1] + ", ");
                                Debug.Write("AUTHLV = " + dr[2]);
                                Debug.WriteLine("");
                                active = dr[1].ToString();
                                authLv = dr[2].ToString();
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
            cmn.Dbm.CloseMySqlSchema(cnn);
            return ret;
        }

        /// <summary>
        /// 切削生産計画システム利用者情報取得 (km8400 切削生産計画システム利用者マスター)
        /// </summary>
        /// <param name="dataSet">データセット</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int GetMPPPUserInfo(ref DataSet dataSet)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                string sql = EditMPPPUserInfoQuery();
                using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
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
                                Debug.Write("USERID = " + dr[0] + ", ");
                                Debug.Write("ACTIVE = " + dr[1] + ", ");
                                Debug.Write("AUTHLV = " + dr[2]);
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
        /// 切削生産計画システム利用者情報取得 SQL 構文編集 (KM8400 切削生産計画システム利用者マスター) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditMPPPUserInfoQuery()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 登録形式により抽出対象が異なる
            string sql = "SELECT "
                       + "USERID, "
                       + "ACTIVE, "
                       + "AUTHLV "
                       + "FROM "
                       + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8400 + " "
                       + "WHERE "
                       + "USERID = '" + cmn.PkKM8400.UserId + "' "
                       ;
            return sql;
        }

        /// <summary>
        /// 切削刃具情報取得 (KM8410 切削刃具マスター)
        /// </summary>
        /// <param name="dataSet">データセット</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int GetMpChipInfo(ref DataSet dataSet)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                // 切削刃具情報検索 SQL 編集
                string sql = EditMpChipInfoQuery();
                using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
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
                                Debug.Write(" TKCD = "      + dr[0] + ", ");
                                Debug.Write(" HMCD = "      + dr[1] + ", ");
                                Debug.Write(" KTCD = "      + dr[2] + ", ");
                                Debug.Write(" TOOLNO = "    + dr[3] + ", ");
                                Debug.Write(" VALDTF = "    + dr[4] + ", ");
                                Debug.Write(" VALDTT = "    + dr[5] + ", ");
                                Debug.Write(" TOOLNM = "    + dr[6] + ", ");
                                Debug.Write(" TOOLLIFE = "  + dr[7] + ", ");
                                Debug.Write(" SPINDLENO = " + dr[8] + ", ");
                                Debug.Write(" CUTWORK = "   + dr[9] + ", ");
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
        /// 切削刃具情報取得 SQL 構文編集 (KM8410 切削刃具マスター) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditMpChipInfoQuery()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 登録形式により抽出対象が異なる
            string sql = "select "
                       + "TKCD, "
                       + "HMCD, "
                       + "KTCD, "
                       + "TOOLNO, "
                       + "VALDTF, "
                       + "VALDTT, "
                       + "TOOLNM, "
                       + "TOOLLIFE, "
                       + "SPINDLENO, "
                       + "CUTWORK, "
                       + "INSTID, "
                       + "INSTDT, "
                       + "UPDTID, "
                       + "UPDTDT "
                       + "from "
                       + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8410 + " "
                       + "where "
                       + "TKCD like '"   + cmn.PkKM8410.TkCd + "' and "
                       + "HMCD like '"   + cmn.PkKM8410.HmCd + "' and "
                       + "KTCD like '"   + cmn.PkKM8410.KtCd + "' and "
                       + "TOOLNO like '" + cmn.PkKM8410.ToolNo + "' and "
                       + "VALDTF like '" + cmn.PkKM8410.ValDtF + "' and "
                       + "VALDTT like '" + cmn.PkKM8410.ValDtT + "' "
                       + "order by "
                       + "TKCD, "
                       + "HMCD, "
                       + "KTCD, "
                       + "TOOLNO, "
                       + "VALDTF, "
                       + "VALDTT "
                       ;
            return sql;

        }

        /// <summary>
        /// 切削オーダー平準化情報取得 (d0415 切削生産計画ファイル, D0445 切削生産計画日程ファイル, KM8420 切削設備マスタ)
        /// </summary>
        /// <param name="dataSet">データセット</param>
        /// <param name="searchTarget">検索対象 (0: 現状全件, 1: 確定全件 2: 特定データ, 3: グループのみ, 4: 設備のみ)</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int GetOrderEqualizeInfo(ref DataSet dataSet, int searchTarget = Common.D0415_TARGET_SPECIFIC)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            MySqlConnection cnn = null;
                
            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                // 切削オーダー平準化情報取得 SQL 構文編集
                string sql = EditOrderEqualizeInfoQuery(searchTarget);
                using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataSet:");
                        using (DataSet myDs = new DataSet())
                        {
                            // 結果取得
                            myDa.Fill(myDs, "emp");
                            switch (searchTarget)
                            {
                                case Common.D0415_TARGET_CUR_ALL:
                                    {
                                        foreach (DataRow dr in myDs.Tables[0].Rows)
                                        {
                                            Debug.Write("EDDT = "      + dr[1] + ", ");
                                            Debug.Write("EDTIM = "     + dr[2] + ", ");
                                            Debug.Write("MCCD = "      + dr[3] + ", ");
                                            Debug.Write("ODRQTY = "    + dr[4] + ", ");
                                            Debug.Write("SPLITSEQ = "  + dr[5] + ", ");
                                            Debug.Write("ODCD = "      + dr[6] + ", ");
                                            Debug.Write("PLNNO = "     + dr[7] + ", ");
                                            Debug.Write("ODRNO = "     + dr[8] + ", ");
                                            Debug.Write("KTSEQ = "     + dr[9] + ", ");
                                            Debug.Write("MCGCD = "     + dr[10] + ", ");
                                            Debug.Write("ONTIME = "    + dr[11] + ", ");
                                            Debug.Write("HMCD = "      + dr[12] + ", ");
                                            Debug.Write("SEQ = "       + dr[13] + ", ");
                                            Debug.Write("KT_CT_SUM = " + dr[14] + ", ");
                                            Debug.Write("KT_DT_SUM = " + dr[15] + ", ");
                                            Debug.Write("KT_OT_SUM = " + dr[16] + ", ");
                                            Debug.Write("TBLID = "     + dr[17]);
                                            Debug.WriteLine("");
                                        }
                                        break;
                                    }
                                case Common.D0415_TARGET_SIM_ALL:
                                    {
                                        foreach (DataRow dr in myDs.Tables[0].Rows)
                                        {
                                            Debug.Write("KEDDT = "     + dr[1] + ", ");
                                            Debug.Write("KEDTIM = "    + dr[2] + ", ");
                                            Debug.Write("KMCCD = "     + dr[3] + ", ");
                                            Debug.Write("KODRQTY = "   + dr[4] + ", ");
                                            Debug.Write("SPLITSEQ = "  + dr[5] + ", ");
                                            Debug.Write("ODCD = "      + dr[6] + ", ");
                                            Debug.Write("PLNNO = "     + dr[7] + ", ");
                                            Debug.Write("ODRNO = "     + dr[8] + ", ");
                                            Debug.Write("KTSEQ = "     + dr[9] + ", ");
                                            Debug.Write("MCGCD = "     + dr[10] + ", ");
                                            Debug.Write("ONTIME = "    + dr[11] + ", ");
                                            Debug.Write("HMCD = "      + dr[12] + ", ");
                                            Debug.Write("SEQ = "       + dr[13] + ", ");
                                            Debug.Write("KT_CT_SUM = " + dr[14] + ", ");
                                            Debug.Write("KT_DT_SUM = " + dr[15] + ", ");
                                            Debug.Write("KT_OT_SUM = " + dr[16] + ", ");
                                            Debug.Write("TBLID = "     + dr[17]);
                                            Debug.WriteLine("");
                                        }
                                        break;
                                    }
                                case Common.D0415_TARGET_SPECIFIC:
                                default:
                                    {
                                        foreach (DataRow dr in myDs.Tables[0].Rows)
                                        {
                                            Debug.Write("ODCD = "     + dr[0] + ", ");
                                            Debug.Write("PLNNO = "    + dr[1] + ", ");
                                            Debug.Write("ODRNO = "    + dr[2] + ", ");
                                            Debug.Write("KTSEQ = "    + dr[3] + ", ");
                                            Debug.Write("SEQ = "      + dr[4] + ", ");
                                            Debug.Write("MCSEQ = "    + dr[5] + ", ");
                                            Debug.Write("SPLITSEQ = " + dr[6] + ", ");
                                            Debug.Write("KEDDT = "    + dr[7] + ", ");
                                            Debug.Write("KEDTIM = "   + dr[8] + ", ");
                                            Debug.Write("KORDQTY = "  + dr[9] + ", ");
                                            Debug.Write("KMCCD = "    + dr[10] + ", ");
                                            Debug.Write("TBLID = "    + dr[11]);
                                            Debug.WriteLine("");
                                        }
                                        break;
                                    }
                                case Common.D0415_TARGET_MCGCD:
                                    {
                                        foreach (DataRow dr in myDs.Tables[0].Rows)
                                        {
                                            Debug.Write("MCGCD = " + dr[0] + ", ");
                                            Debug.Write("MCGNM = " + dr[1]);
                                            Debug.WriteLine("");
                                        }
                                        break;
                                    }
                                case Common.D0415_TARGET_MCCD:
                                    {
                                        foreach (DataRow dr in myDs.Tables[0].Rows)
                                        {
                                            Debug.Write("MCCD = "     + dr[0] + ", ");
                                            Debug.Write("MCNM = "     + dr[1]);
                                            Debug.WriteLine("");
                                        }
                                        break;
                                    }
                                case Common.D0415_TARGET_MCTM:
                                    {
                                        foreach (DataRow dr in myDs.Tables[0].Rows)
                                        {
                                            Debug.Write("MCCD = "     + dr[0] + ", ");
                                            Debug.Write("MCNM = "     + dr[1] + ", ");
                                            Debug.Write("ONTIME = "   + dr[2] + ", ");
                                            Debug.Write("SETUPTM1 = " + dr[3] + ", ");
                                            Debug.Write("SETUPTM2 = " + dr[4] + ", ");
                                            Debug.Write("SETUPTM3 = " + dr[5]);
                                            Debug.WriteLine("");
                                        }
                                        break;
                                    }
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
        /// 切削オーダー平準化情報取得 SQL 構文編集 (d0415 切削生産計画ファイル, D0445 切削生産計画日程ファイル, KM8420 切削設備マスタ)
        /// </summary>
        /// <param name="searchTarget">検索対象 (0: 全件, 1: 特定データ, 2: グループ コードのみ)</param>
        /// <returns>SQL 構文</returns>
        private string EditOrderEqualizeInfoQuery(int searchTarget)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 登録形式により抽出対象が異なる

            string sql;
            switch (searchTarget)
            {
                case Common.D0415_TARGET_CUR_ALL:
                    {
                        sql = "select distinct "
                            + "a.EDDT, "
                            + "a.EDTIM, "
                            + "a.MCCD, "
                            + "a.ODRQTY, "
                            + "a.SPLITSEQ, "
                            + "a.ODCD, "
                            + "a.PLNNO, "
                            + "a.ODRNO, "
                            + "a.KTSEQ, "
                            + "a.MCGCD, "
                            + "b.ONTIME as \"" + Common.KM8420_ONTIME + "\", "
                            + "(b.SETUPTM1 + b.SETUPTM2 + b.SETUPTM3) as \"" + Common.KM8420_SETUPTM + "\", "
                            + "a.HMCD, "
                            + "a.SEQ, "
                            + "a.MCSEQ, "
                            + "ifnull((c.KT1CT + c.KT2CT + c.KT3CT + c.KT4CT + c.KT5CT + c.KT6CT), 0) as \"" + Common.KM8430_KT_CT_SUM + "\", "
                            + "ifnull((c.KT1DT + c.KT2DT + c.KT3DT + c.KT4DT + c.KT5DT + c.KT6DT), 0) as \"" + Common.KM8430_KT_DT_SUM + "\", "
                            + "ifnull((c.KT1OT + c.KT2OT + c.KT3OT + c.KT4OT + c.KT5OT + c.KT6OT), 0) as \"" + Common.KM8430_KT_OT_SUM + "\", "
                            + "a.TBLNM as \"テーブル名\" "
                            + "from "
                            + "("
                            + "select distinct "
                            + "EDDT, "
                            + "EDTIM, "
                            + "MCCD, "
                            + "ODRQTY, "
                            + "SPLITSEQ, "
                            + "ODCD, "
                            + "'' as PLNNO, "
                            + "ODRNO, "
                            + "KTSEQ, "
                            + "MCGCD, "
                            + "'0' as ONTIME, "
                            + "'0' as SETUPTM, "
                            + "HMCD, "
                            + "'1' as SEQ, "
                            + "MCSEQ, "
                            + "'" + Common.TABLE_ID_D0415 + "' as TBLNM "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0415 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkD0415.EdDt + "' and "
                                         + "'" + cmn.IkD0415.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkD0415.McGCd + "' and "
                            + "MCCD = '" + cmn.IkD0415.McCd + "' "
                            + "union "
                            + "select distinct "
                            + "EDDT, "
                            + "EDTM as EDTIM, "
                            + "MCCD, "
                            + "ODRQTY, "
                            + "SPLITSEQ, "
                            + "ODCD, "
                            + "PLNNO, "
                            + "ODRNO, "
                            + "KTSEQ, "
                            + "MCGCD, "
                            + "'0' as ONTIME, "
                            + "'0' as SETUPTM, "
                            + "HMCD, "
                            + "SEQ, "
                            + "MCSEQ, "
                            + "'" + Common.TABLE_ID_D0445 + "' as TBLNM "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0445 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkD0445.EdDt + "' and "
                                         + "'" + cmn.IkD0445.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkD0445.McGCd + "' and "
                            + "MCCD = '" + cmn.IkD0445.McCd + "' "
                            + ") a, "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420 + " b, "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " c "
                            + "where "
                            + "b.MCGCD = a.MCGCD and "
                            + "b.MCCD = a.MCCD and "
                            + "c.HMCD = a.HMCD and "
                            + "date_format(c.VALDTF, '%Y-%m-%d') <= date_format(a.EDDT, '%Y-%m-%d') "
                            + "order by "
                            + "a.EDDT, "
                            + "a.MCGCD, "
                            + "a.MCCD, "
                            + "a.HMCD, "
                            + "a.ODRNO, "
                            + "a.SPLITSEQ "
                            ;
                        break;
                    }
                case Common.D0415_TARGET_SIM_ALL:
                    {
                        sql = "select distinct "
                            + "a.KEDDT, "
                            + "a.KEDTIM, "
                            + "a.KMCCD, "
                            + "a.KODRQTY, "
                            + "a.SPLITSEQ, "
                            + "a.ODCD, "
                            + "a.PLNNO, "
                            + "a.ODRNO, "
                            + "a.KTSEQ, "
                            + "a.MCGCD, "
                            + "b.ONTIME as \"" + Common.KM8420_ONTIME + "\", "
                            + "(b.SETUPTM1 + b.SETUPTM2 + b.SETUPTM3) as \"" + Common.KM8420_SETUPTM + "\", "
                            + "a.HMCD, "
                            + "a.SEQ, "
                            + "a.MCSEQ, "
                            + "ifnull((c.KT1CT + c.KT2CT + c.KT3CT + c.KT4CT + c.KT5CT + c.KT6CT), 0) as \"" + Common.KM8430_KT_CT_SUM + "\", "
                            + "ifnull((c.KT1DT + c.KT2DT + c.KT3DT + c.KT4DT + c.KT5DT + c.KT6DT), 0) as \"" + Common.KM8430_KT_DT_SUM + "\", "
                            + "ifnull((c.KT1OT + c.KT2OT + c.KT3OT + c.KT4OT + c.KT5OT + c.KT6OT), 0) as \"" + Common.KM8430_KT_OT_SUM + "\", "
                            + "a.TBLNM as \"テーブル名\" "
                            + "from "
                            + "("
                            + "select distinct "
                            + "KEDDT, "
                            + "KEDTIM, "
                            + "KMCCD, "
                            + "KODRQTY, "
                            + "SPLITSEQ, "
                            + "ODCD, "
                            + "'' as PLNNO, "
                            + "ODRNO, "
                            + "KTSEQ, "
                            + "MCGCD, "
                            + "'0' as ONTIME, "
                            + "'0' as SETUPTM, "
                            + "HMCD, "
                            + "'1' as SEQ, "
                            + "MCSEQ, "
                            + "'" + Common.TABLE_ID_D0415 + "' as TBLNM "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0415 + " "
                            + "where "
                            + "KEDDT between '" + cmn.IkD0415.EdDt + "' and "
                                         + "'" + cmn.IkD0415.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkD0415.McGCd + "' and "
                            + "KMCCD = '" + cmn.IkD0415.McCd + "' "
                            + "union "
                            + "select distinct "
                            + "KEDDT, "
                            + "KEDTM as KEDTIM, "
                            + "KMCCD, "
                            + "KODRQTY, "
                            + "SPLITSEQ, "
                            + "ODCD, "
                            + "PLNNO, "
                            + "ODRNO, "
                            + "KTSEQ, "
                            + "MCGCD, "
                            + "'0' as ONTIME, "
                            + "'0' as SETUPTM, "
                            + "HMCD, "
                            + "SEQ, "
                            + "MCSEQ, "
                            + "'" + Common.TABLE_ID_D0445 + "' as TBLNM "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0445 + " "
                            + "where "
                            + "KEDDT between '" + cmn.IkD0445.EdDt + "' and "
                                         + "'" + cmn.IkD0445.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkD0445.McGCd + "' and "
                            + "KMCCD = '" + cmn.IkD0445.McCd + "' "
                            + ") a, "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420 + " b, "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " c "
                            + "where "
                            + "b.MCGCD = a.MCGCD and "
                            + "b.MCCD = a.KMCCD and "
                            + "c.HMCD = a.HMCD and "
                            + "date_format(c.VALDTF, '%Y-%m-%d') <= date_format(a.KEDDT, '%Y-%m-%d') "
                            + "order by "
                            + "a.KEDDT, "
                            + "a.MCGCD, "
                            + "a.KMCCD, "
                            + "a.HMCD, "
                            + "a.ODRNO, "
                            + "a.SPLITSEQ "
                            ;
                        break;
                    }
                case Common.D0415_TARGET_SPECIFIC:
                default:
                    {
                        sql = "select "
                            + "'' as ODCD, "
                            + "'' as PLNNO, "
                            + "ODRNO, "
                            + "KTSEQ, "
                            + "0 as SEQ, "
                            + "MCSEQ, "
                            + "SPLITSEQ, "
                            + "KEDDT, "
                            + "KEDTIM, "
                            + "KODRQTY, "
                            + "KMCCD, "
                            + "'" + Common.TABLE_ID_D0415 + "' as TBLNM "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0415 + " "
                            + "where "
                            + "KEDDT between '" + cmn.IkD0415.EdDt + "' and "
                                         + "'" + cmn.IkD0415.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkD0415.McGCd + "' and "
                            + "KMCCD = '" + cmn.IkD0415.McCd + "' "
                            + "union all "
                            + "select "
                            + "ODCD, "
                            + "PLNNO, "
                            + "ODRNO, "
                            + "KTSEQ, "
                            + "SEQ, "
                            + "MCSEQ, "
                            + "SPLITSEQ, "
                            + "KEDDT, "
                            + "KEDTM as EDDTIM, "
                            + "KODRQTY, "
                            + "KMCCD, "
                            + "'" + Common.TABLE_ID_D0445 + "' as TBLNM "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0445 + " "
                            + "where "
                            + "KEDDT between '" + cmn.IkD0445.EdDt + "' and "
                                         + "'" + cmn.IkD0445.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkD0445.McGCd + "' and "
                            + "KMCCD = '" + cmn.IkD0445.McCd + "' "
                            + "order by "
                            + "KEDDT, "
                            + "KMCCD, "
                            + "ODRNO "
                            ;
                        break;
                    }
                case Common.D0415_TARGET_MCGCD:
                    {
                        sql = "select distinct "
                            + "a.MCGCD, "
                            + "b.MCGNM "
                            + "from "
                            + "("
                            + "select "
                            + "MCGCD "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0415 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkD0415.EdDt + "' and "
                                         + "'" + cmn.IkD0415.EdDt + "' "
                            + "union all "
                            + "select "
                            + "MCGCD "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0445 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkD0445.EdDt + "' and "
                                         + "'" + cmn.IkD0445.EdDt + "' "
                            + ") a, "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420 + " b "
                            + "where "
                            + "b.MCGCD = a.MCGCD "
                            + "order by "
                            + "a.MCGCD "
                            ;
                        break;
                    }
                case Common.D0415_TARGET_MCCD:
                    {
                        sql = "select distinct "
                            + "a.MCCD, "
                            + "b.MCNM "
                            + "from "
                            + "("
                            + "select "
                            + "MCGCD, "
                            + "MCCD "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0415 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkD0415.EdDt + "' and "
                                         + "'" + cmn.IkD0415.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkD0415.McGCd + "' "
                            + "union all "
                            + "select "
                            + "MCGCD, "
                            + "MCCD "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0445 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkD0445.EdDt + "' and "
                                         + "'" + cmn.IkD0445.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkD0445.McGCd + "' "
                            + ") a, "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420 + " b "
                            + "where "
                            + "b.MCGCD = a.MCGCD and "
                            + "b.MCCD = a.MCCD "
                            + "order by "
                            + "a.MCCD "
                            ;
                        break;
                    }
                case Common.D0415_TARGET_MCTM:
                    {
                        sql = "select distinct "
                            + "a.MCCD, "
                            + "b.MCNM, "
                            + "b.ONTIME, "
                            + "b.SETUPTM1, "
                            + "b.SETUPTM2, "
                            + "b.SETUPTM3 "
                            + "from "
                            + "("
                            + "select "
                            + "MCGCD, "
                            + "MCCD "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0415 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkD0415.EdDt + "' and "
                                         + "'" + cmn.IkD0415.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkD0415.McGCd + "' and "
                            + "MCCD = '" + cmn.IkD0415.McCd + "' "
                            + "union all "
                            + "select "
                            + "MCGCD, "
                            + "MCCD "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0445 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkD0445.EdDt + "' and "
                                         + "'" + cmn.IkD0445.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkD0445.McGCd + "' and "
                            + "MCCD = '" + cmn.IkD0445.McCd + "' "
                            + ") a, "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420 + " b "
                            + "where "
                            + "b.MCGCD = a.MCGCD and "
                            + "b.MCCD = a.MCCD "
                            + "order by "
                            + "a.MCCD "
                            ;
                        break;
                    }
            }
            return sql;
        }

        ///// <summary>
        ///// 切削設備情報検索 (KM8420 切削設備マスター)
        ///// </summary>
        ///// <param name="dataSet">データセット</param>
        ///// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        //public int GetMpEquipInfo(ref DataSet dataSet)
        //{
        //    Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

        //    int ret = 0;
        //    MySqlConnection cnn = null;

        //    try
        //    {
        //        // 切削生産計画システム データベースへ接続
        //        cmn.Dbm.IsConnectMySqlSchema(ref cnn);

        //        // 切削設備情報検索 SQL 構文編集
        //        string sql = EditMpEquipInfoQuery(searchTarget);
        //        using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
        //        {
        //            using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
        //            {
        //                Debug.WriteLine("Read from DataSet:");
        //                using (DataSet myDs = new DataSet())
        //                {
        //                    // 結果取得
        //                    myDa.Fill(myDs, "emp");
        //                    foreach (DataRow dr in myDs.Tables[0].Rows)
        //                    {
        //                        Debug.Write("MCGSEQ = "   + dr[0] + ", ");
        //                        Debug.Write("MCGCD = "    + dr[1] + ", ");
        //                        Debug.Write("MCSEQ = "    + dr[2] + ", ");
        //                        Debug.Write("MCCD = "     + dr[3] + ", ");
        //                        Debug.Write("MCGNM = "    + dr[4] + ", ");
        //                        Debug.Write("MCNM = "     + dr[5] + ", ");
        //                        Debug.Write("ONTIME = "   + dr[6] + ", ");
        //                        Debug.Write("FLG1 = "     + dr[7] + ", ");
        //                        Debug.Write("FLG2 = "     + dr[8] + ", ");
        //                        Debug.Write("SETUPNM1 = " + dr[9] + ", ");
        //                        Debug.Write("SETUPTM1 = " + dr[10] + ", ");
        //                        Debug.Write("SETUPNM2 = " + dr[11] + ", ");
        //                        Debug.Write("SETUPTM2 = " + dr[12] + ", ");
        //                        Debug.Write("SETUPNM3 = " + dr[13] + ", ");
        //                        Debug.Write("SETUPTM3 = " + dr[14] + ", ");
        //                        Debug.WriteLine("");
        //                    }
        //                    dataSet = myDs;
        //                }
        //            }
        //        }
        //        ret = dataSet.Tables[0].Rows.Count;
        //    }
        //    catch (Exception ex)
        //    {
        //        // エラー
        //        string msg = "Exception Source = " + ex.Source + ", Message = " + ex.Message;
        //        if (AssemblyState.IsDebug) Debug.WriteLine(msg);

        //        Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
        //        cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_802, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);
        //        ret = -1;
        //    }
        //    // 接続を閉じる
        //    cmn.Dbm.CloseMySqlSchema(cnn);
        //    return ret;
        //}

        ///// <summary>
        ///// 切削設備情報検索 SQL 構文編集 (KM8420 切削設備マスター) 
        ///// </summary>
        ///// <param name="searchTarget">検索対象 (0: 全件, 1: 特定データ, 2: 品番のみ)</param>
        ///// <returns>SQL 構文</returns>
        //private string EditMpEquipInfoQuery(int searchTarget)
        //{
        //    Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

        //    string sql;
        //    switch (searchTarget)
        //    {
        //        case Common.KM8420_TARGET_ALL:
        //        {
        //            sql = "select "
        //                + "ODCD, "
        //                + "WKGRCD, "
        //                + "HMCD, "
        //                + "VALDTF, "
        //                + "WKSEQ, "
        //                + "WORK, "
        //                + "trim(to_char(SETUPTMMP, '9990.99')) as SETUPTMMP, "
        //                + "trim(to_char(SETUPTMSP, '9990.99')) as SETUPTMSP, "
        //                + "NOTE, "
        //                + "INSTID, "
        //                + "INSTDT, "
        //                + "UPDTID, "
        //                + "UPDTDT "
        //                + "from "
        //                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420 + " "
        //                + "order by "
        //                + "ODCD, "
        //                + "WKGRCD, "
        //                + "HMCD, "
        //                + "VALDTF, "
        //                + "WKSEQ "
        //                ;
        //            break;
        //        }
        //        case Common.KM8420_TARGET_SPECIFIC:
        //        default:
        //        {
        //                sql = "select "
        //                    + "ODCD, "
        //                    + "WKGRCD, "
        //                    + "HMCD, "
        //                    + "VALDTF, "
        //                    + "WKSEQ, "
        //                    + "WORK, "
        //                    + "trim(to_char(SETUPTMMP, '9990.99')) as SETUPTMMP, "
        //                    + "trim(to_char(SETUPTMSP, '9990.99')) as SETUPTMSP, "
        //                    + "NOTE, "
        //                    + "INSTID, "
        //                    + "INSTDT, "
        //                    + "UPDTID, "
        //                    + "UPDTDT "
        //                    + "from "
        //                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420 + " "
        //                    + "where "
        //                    + "ODCD like '%" + cmn.PkKM8420.OdCd + "%' and "
        //                    + "WKGRCD like '%" + cmn.PkKM8420.WkGrCd + "%' and "
        //                    + "HMCD like '%" + cmn.PkKM8420.HmCd + "%' and "
        //                    + "VALDTF between to_timestamp('" + cmn.PkKM8420.ValDtFF + "') and "
        //                    +                "to_timestamp('" + cmn.PkKM8420.ValDtFT + "') and "
        //                    + "WKSEQ like '%" + cmn.PkKM8420.WkSeq + "%' "
        //                    + "order by "
        //                    + "ODCD, "
        //                    + "WKGRCD, "
        //                    + "HMCD, "
        //                    + "VALDTF, "
        //                    + "WKSEQ "
        //                    ;
        //            break;
        //        }
        //        case Common.KM8420_TARGET_HMCD:
        //        {
        //            sql = "select "
        //                + "HMCD "
        //                + "from "
        //                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420 + " "
        //                + "where "
        //                + "ODCD like '%" + cmn.PkKM8420.OdCd + "%' and "
        //                + "WKGRCD like '%" + cmn.PkKM8420.WkGrCd + "%' and "
        //                + "HMCD like '%" + cmn.PkKM8420.HmCd + "%' and "
        //                + "VALDTF between to_timestamp('" + cmn.PkKM8420.ValDtFF + "') and "
        //                +                "to_timestamp('" + cmn.PkKM8420.ValDtFT + "') and "
        //                + "WKSEQ like '%" + cmn.PkKM8420.WkSeq + "%' "
        //                + "group by "
        //                + "HMCD "
        //                + "order by "
        //                + "HMCD "
        //                ;
        //            break;
        //        }
        //    }
        //    return sql;
        //}

        ///// <summary>
        ///// 切削設備情報削除 (KM8420 切削設備マスター)
        ///// </summary>
        ///// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        //public int DeleteSetupInfo()
        //{
        //    Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

        //    int ret = 0;

        //    MySqlConnection cnn = null;

        //    try
        //    {
        //        // 切削生産計画システム データベースへ接続
        //        cmn.Dbm.IsConnectMySqlSchema(ref cnn);

        //        // 切削設備情報削除 SQL 構文編集
        //        string sql = EditSetupInfoDeleteSql();

        //        // 削除
        //        using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
        //        {
        //            using (MySqlTransaction txn = cnn.BeginTransaction())
        //            {
        //                try
        //                {
        //                    int res = myCmd.ExecuteNonQuery();
        //                    if (res >= 1)
        //                    {
        //                        txn.Commit();
        //                        Debug.WriteLine(Common.TABLE_ID_KM8420 + " table delete succeed and commited.");
        //                    }
        //                    ret = res;
        //                }
        //                catch (Exception e)
        //                {
        //                    txn.Rollback();
        //                    Debug.WriteLine(Common.TABLE_ID_KM8420 + " table no data deleted.");

        //                    Debug.WriteLine("Exception Source = " + e.Source);
        //                    Debug.WriteLine("Exception Message = " + e.Message);
        //                    if (cnn != null)
        //                    {
        //                        // 接続を閉じる
        //                        cnn.Close();
        //                    }
        //                    ret = -1;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // エラー
        //        string msg = "Exception Source = " + ex.Source + ", Message = " + ex.Message;
        //        if (AssemblyState.IsDebug) Debug.WriteLine(msg);

        //        Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
        //        cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_802, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);
        //        ret = -1;
        //    }
        //    // 接続を閉じる
        //    cmn.Dbm.CloseMySqlSchema(cnn);
        //    return ret;
        //}

        ///// <summary>
        ///// 切削設備情報削除 SQL 構文編集 (KM8420 切削設備マスター) 
        ///// </summary>
        ///// <returns>SQL 構文</returns>
        //private string EditSetupInfoDeleteSql()
        //{
        //    Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

        //    // 画面指定されたキーで削除
        //    string sql = "delete "
        //               + "from "
        //               + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420 + " "
        //               + "where "
        //               + "( "
        //               + "ODCD, "
        //               + "WKGRCD, "
        //               + "HMCD, "
        //               + "VALDTF, "
        //               + "WKSEQ "
        //               + ") "
        //               + "in "
        //               + "( "
        //               + "select "
        //               + "ODCD, "
        //               + "WKGRCD, "
        //               + "HMCD, "
        //               + "VALDTF, "
        //               + "WKSEQ "
        //               + "from "
        //               + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420 + " "
        //               + "where "
        //               + "ODCD like '%"      + cmn.PkKM8420.OdCd    + "%' and "
        //               + "WKGRCD like '%" + cmn.PkKM8420.WkGrCd + "%' and "
        //               + "HMCD like '%" + cmn.PkKM8420.HmCd + "%' and "
        //               + "VALDTF between to_timestamp('" + cmn.PkKM8420.ValDtFF + "') and "
        //               +                "to_timestamp('" + cmn.PkKM8420.ValDtFT + "') and "
        //               + "WKSEQ like '%"     + cmn.PkKM8420.WkSeq   + "%' "
        //               + ") "
        //               ;

        //    return sql;
        //}

        ///// <summary>
        ///// 切削設備情報登録/更新 (KM8420 切削設備マスター)
        ///// </summary>
        ///// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        //public int MergeSetupInfo()
        //{
        //    Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

        //    int ret = 0;
        //    MySqlConnection cnn = null;

        //    try
        //    {
        //        // 切削生産計画システム データベースへ接続
        //        cmn.Dbm.IsConnectMySqlSchema(ref cnn);

        //        // 切削設備マスター登録/更新 SQL 構文編集
        //        string sql = EditSetupInfoMergeSql();

        //        using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
        //        {
        //            using (MySqlTransaction txn = cnn.BeginTransaction())
        //            {
        //                try
        //                {
        //                    int res = myCmd.ExecuteNonQuery();
        //                    if (res >= 1)
        //                    {
        //                        txn.Commit();
        //                        Debug.WriteLine(Common.TABLE_ID_KM8420 + " table data insert/update succeed and commited.");
        //                    }
        //                    ret = res;
        //                }
        //                catch (Exception e)
        //                {
        //                    txn.Rollback();
        //                    Debug.WriteLine(Common.TABLE_ID_KM8420 + " table no data inserted/updated.");

        //                    Debug.WriteLine("Exception Source = " + e.Source);
        //                    Debug.WriteLine("Exception Message = " + e.Message);
        //                    if (cnn != null)
        //                    {
        //                        // 接続を閉じる
        //                        cnn.Close();
        //                    }
        //                    ret = -1;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // エラー
        //        string msg = "Exception Source = " + ex.Source + ", Message = " + ex.Message;
        //        if (AssemblyState.IsDebug) Debug.WriteLine(msg);

        //        Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
        //        cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_802, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);
        //        ret = -1;
        //    }
        //    // 接続を閉じる
        //    cmn.Dbm.CloseMySqlSchema(cnn);
        //    return ret;
        //}

        ///// <summary>
        ///// 切削設備情報登録/更新 SQL 構文編集 (KM8420 切削設備マスター) 
        ///// </summary>
        ///// <returns>SQL 構文</returns>
        //public string EditSetupInfoMergeSql()
        //{
        //    Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

        //    // 登録形式により抽出対象が異なる
        //    // MySql の DATE 型列に値を代入するときは、その列が時刻を持っているか否かに関わらず、必ず to_datetime('<代入元>') メソッドで変換してから代入する必要がある
        //    // 代入元が定数か変数化に関わらずシングル クォーテーション括りは必須
        //    // 代入元に書式 'YYYY/MM/DD HH24:MI:SS' 等の記述は不要、MySql が適切に合わせ込んで登録してくれる
        //    // この変換を怠ると「ORA-01861: リテラルが書式文字列と一致しません」の例外が発生する
        //    string sql = "merge "
        //               + "into "
        //               + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420 + " k "
        //               + "using "
        //               + "("
        //               + "select "
        //               + "'" + cmn.PkKM8420.OdCd + "' " + "ODCD, "
        //               + "'" + cmn.PkKM8420.WkGrCd + "' " + "WKGRCD, "
        //               + "'" + cmn.PkKM8420.HmCd + "' " + "HMCD, "
        //               + "to_timestamp('" + cmn.PkKM8420.ValDtFF + "') " + "VALDTF, "
        //               + "'" + cmn.PkKM8420.WkSeq + "' " + "WKSEQ, "
        //               + "'" + cmn.DrKM8420.Work + "' " + "WORK, "
        //               + "'" + cmn.DrKM8420.SetupTmMP + "' " + "SETUPTMMP, "
        //               + "'" + cmn.DrKM8420.SetupTmSP + "' " + "SETUPTMSP, "
        //               + "'" + cmn.DrKM8420.Note + "' " + "NOTE, "
        //               + "'" + cmn.DrCommon.InstID + "' " + "INSTID, "
        //               + "'" + cmn.DrCommon.UpdtID + "' " + "UPDTID "
        //               + "from "
        //               + "DUAL"
        //               + ") d "
        //               + "on "
        //               + "("
        //               + "k.ODCD = d.ODCD and "
        //               + "k.WKGRCD = d.WKGRCD and "
        //               + "k.HMCD = d.HMCD and "
        //               + "k.VALDTF = d.VALDTF and "
        //               + "k.WKSEQ = d.WKSEQ "
        //               + ") "
        //               + "when matched then "
        //               + "update "
        //               + "set "
        //               + "k.WORK = d.WORK, "
        //               + "k.SETUPTMMP = d.SETUPTMMP, "
        //               + "k.SETUPTMSP = d.SETUPTMSP, "
        //               + "k.NOTE = d.NOTE, "
        //               + "k.UPDTID = d.UPDTID, "
        //               + "k.UPDTDT = SYSDATE "
        //               + "when not matched then "
        //               + "insert "
        //               + "("
        //               + "k.ODCD, "
        //               + "k.WKGRCD, "
        //               + "k.HMCD, "
        //               + "k.VALDTF, "
        //               + "k.WKSEQ, "
        //               + "k.WORK, "
        //               + "k.SETUPTMMP, "
        //               + "k.SETUPTMSP, "
        //               + "k.NOTE, "
        //               + "k.INSTID, "
        //               + "k.INSTDT, "
        //               + "k.UPDTID, "
        //               + "k.UPDTDT"
        //               + ") "
        //               + "values "
        //               + "("
        //               + "d.ODCD, "
        //               + "d.WKGRCD, "
        //               + "d.HMCD, "
        //               + "d.VALDTF, "
        //               + "d.WKSEQ, "
        //               + "d.WORK, "
        //               + "d.SETUPTMMP, "
        //               + "d.SETUPTMSP, "
        //               + "d.NOTE, "
        //               + "d.INSTID, "
        //               + "SYSDATE, "
        //               + "d.UPDTID, "
        //               + "SYSDATE"
        //               + ")"
        //               ;
        //    return sql;
        //}

        /// <summary>
        /// 切削コード票情報検索 (KM8430 切削コード票マスター)
        /// </summary>
        /// <param name="dataSet">データセット</param>
        /// <param name="searchTarget">検索対象 (0: 全件, 1: 特定データ, 2: 品番のみ)</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int GetCycleTimeInfo(ref DataSet dataSet, int searchTarget = Common.KM8430_TARGET_SPECIFIC)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                // 切削コード票情報検索 SQL 構文編集
                string sql = EditCycleTimeInfoQuery(searchTarget);
                using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
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
                                if (searchTarget == Common.KM8430_TARGET_HMCD)
                                {
                                    Debug.Write("HMCD = " + dr[0] + ", ");
                                }
                                else
                                {
                                    Debug.Write("ODCD = " + dr[0] + ", ");
                                    Debug.Write("WKGRCD = " + dr[1] + ", ");
                                    Debug.Write("HMCD = " + dr[2] + ", ");
                                    Debug.Write("VALDTF = " + dr[3] + ", ");
                                    Debug.Write("WKSEQ = " + dr[4] + ", ");
                                }
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
        /// 切削コード票情報検索 SQL 構文編集 (KM8430 切削コード票マスター) 
        /// </summary>
        /// <param name="searchTarget">検索対象 (0: 全件, 1: 特定データ, 2: 品番のみ)</param>
        /// <returns>SQL 構文</returns>
        private string EditCycleTimeInfoQuery(int searchTarget)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string sql;
            switch (searchTarget)
            {
                case Common.KM8430_TARGET_ALL:
                    {
                        sql = "select "
                            + "ODCD, "
                            + "WKGRCD, "
                            + "HMCD, "
                            + "VALDTF, "
                            + "WKSEQ, "
                            + "trim(to_char(CT, '9990.99')) as CT, "
                            + "NOTE, "
                            + "INSTID, "
                            + "INSTDT, "
                            + "UPDTID, "
                            + "UPDTDT "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " "
                            + "order by "
                            + "ODCD, "
                            + "WKGRCD, "
                            + "HMCD, "
                            + "VALDTF, "
                            + "WKSEQ "
                            ;
                        break;
                    }
                case Common.KM8430_TARGET_SPECIFIC:
                default:
                    {
                        sql = "select "
                            + "ODCD, "
                            + "WKGRCD, "
                            + "HMCD, "
                            + "VALDTF, "
                            + "WKSEQ, "
                            + "trim(to_char(CT, '9990.99')) as CT, "
                            + "NOTE, "
                            + "INSTID, "
                            + "INSTDT, "
                            + "UPDTID, "
                            + "UPDTDT "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " "
                            + "where "
                            + "ODCD like '%" + cmn.PkKM8430.OdCd + "%' and "
                            + "WKGRCD like '%" + cmn.PkKM8430.WkGrCd + "%' and "
                            + "HMCD like '%" + cmn.PkKM8430.HmCd + "%' and "
                            + "VALDTF between to_timestamp('" + cmn.PkKM8430.ValDtFF + "') and "
                            + "to_timestamp('" + cmn.PkKM8430.ValDtFT + "') and "
                            + "WKSEQ like '%" + cmn.PkKM8430.WkSeq + "%' "
                            + "order by "
                            + "ODCD, "
                            + "WKGRCD, "
                            + "HMCD, "
                            + "VALDTF, "
                            + "WKSEQ "
                            ;
                        break;
                    }
                case Common.KM8430_TARGET_HMCD:
                    {
                        sql = "select "
                            + "HMCD "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " "
                            + "where "
                            + "ODCD like '%" + cmn.PkKM8430.OdCd + "%' and "
                            + "WKGRCD like '%" + cmn.PkKM8430.WkGrCd + "%' and "
                            + "HMCD like '%" + cmn.PkKM8430.HmCd + "%' and "
                            + "VALDTF between to_timestamp('" + cmn.PkKM8430.ValDtFF + "') and "
                            + "to_timestamp('" + cmn.PkKM8430.ValDtFT + "') and "
                            + "WKSEQ like '%" + cmn.PkKM8430.WkSeq + "%' "
                            + "group by "
                            + "HMCD "
                            + "order by "
                            + "HMCD "
                            ;
                        break;
                    }
            }
            return sql;
        }

        /// <summary>
        /// 切削生産計画情報削除 (D0415 切削生産計画ファイル)
        /// </summary>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int DeleteCuttingProdPlanInfo()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                // 切削生産計画ファイル削除 SQL 構文編集
                string sql = EditCuttingProdPlanInfoDeleteQuery();

                // 削除
                using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
                {
                    using (MySqlTransaction txn = cnn.BeginTransaction())
                    {
                        try
                        {
                            int res = myCmd.ExecuteNonQuery();
                            if (res >= 1)
                            {
                                txn.Commit();
                                Debug.WriteLine(Common.TABLE_ID_D0415 + " table delete succeed and commited.");
                            }
                            ret = res;
                        }
                        catch (Exception e)
                        {
                            txn.Rollback();
                            Debug.WriteLine(Common.TABLE_ID_D0415 + " table no data deleted.");

                            Debug.WriteLine("Exception Source = " + e.Source);
                            Debug.WriteLine("Exception Message = " + e.Message);
                            if (cnn != null)
                            {
                                // 接続を閉じる
                                cnn.Close();
                            }
                            ret = -1;
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

        /// <summary>
        /// 切削生産計画情報削除 SQL 構文編集 (D0415 切削生産計画ファイル) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditCuttingProdPlanInfoDeleteQuery()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 画面指定されたキーで削除
            string sql = "delete "
                       + "from "
                       + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0415 + " "
                       + "where "
                       + "ODRNO like '%" + cmn.PkD0415.OdrNo + "%' and "
                       + "MCSEQ like '%" + cmn.PkD0415.McSeq + "%' and "
                       + "SPLITSEQ like '%" + cmn.PkD0415.SplitSeq + "%' "
                       ;

            return sql;
        }

        /// <summary>
        /// 切削生産計画情報登録/更新 (D0415 切削生産計画ファイル)
        /// </summary>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int MergeCuttingProdPlanInfo()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;

            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                // 切削生産計画ファイル登録/更新 SQL 構文編集
                string sql = EditCuttingProdPlanInfoMergeQuery();

                using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
                {
                    using (MySqlTransaction txn = cnn.BeginTransaction())
                    {
                        try
                        {
                            int res = myCmd.ExecuteNonQuery();
                            if (res >= 1)
                            {
                                txn.Commit();
                                Debug.WriteLine(Common.TABLE_ID_D0415 + " table data insert/update succeed and commited.");
                            }
                            ret = res;
                        }
                        catch (Exception e)
                        {
                            txn.Rollback();
                            Debug.WriteLine(Common.TABLE_ID_D0415 + " table no data inserted/updated.");

                            Debug.WriteLine("Exception Source = " + e.Source);
                            Debug.WriteLine("Exception Message = " + e.Message);
                            if (cnn != null)
                            {
                                // 接続を閉じる
                                cnn.Close();
                            }
                            ret = -1;
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

        /// <summary>
        /// 切削生産計画情報登録/更新 SQL 構文編集 (D0415 切削生産計画ファイル) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        public string EditCuttingProdPlanInfoMergeQuery()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 登録形式により抽出対象が異なる
            // MySql の DATE 型列に値を代入するときは、その列が時刻を持っているか否かに関わらず、必ず to_datetime('<代入元>') メソッドで変換してから代入する必要がある
            // 代入元が定数か変数かに関わらずシングル クォーテーション括りは必須
            // 代入元に書式 'YYYY/MM/DD HH24:MI:SS' 等の記述は不要、MySql が適切に合わせ込んで登録してくれる
            // この変換を怠ると「ORA-01861: リテラルが書式文字列と一致しません」の例外が発生する
            string sql = "insert into "
                       + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0415
                       + "("
                       + "ODRNO, "
                       + "MCSEQ, "
                       + "SPLITSEQ, "
                       + "KTSEQ, "
                       + "HMCD, "
                       + "KTCD, "
                       + "ODRQTY, "
                       + "ODCD, "
                       + "NEXTODCD, "
                       + "LTTIME, "
                       + "STDT, "
                       + "STTIM, "
                       + "EDDT, "
                       + "EDTIM, "
                       + "ODRSTS, "
                       + "QRCD, "
                       + "JIQTY, "
                       + "DENPYOKBN, "
                       + "DENPYODT, "
                       + "NOTE, "
                       + "WKNOTE, "
                       + "WKCOMMENT, "
                       + "DATAKBN, "
                       + "INSTID, "
                       + "INSTDT, "
                       + "UPDTID, "
                       + "UPDTDT, "
                       + "UKCD, "
                       + "NAIGAIKBN, "
                       + "RETKTCD, "
                       + "MCGCD, "
                       + "MCCD, "
                       + "KEDDT, "
                       + "KEDTIM, "
                       + "KODRQTY, "
                       + "KMCCD, "
                       + "WKDTDT, "
                       + "WKSTDT, "
                       + "WKEDDT"
                       + ") "
                       + "values "
                       + "("
                       + "'" + cmn.PkD0415.OdrNo + "', "
                       + "'" + cmn.PkD0415.McSeq + "', "
                       + "'" + cmn.PkD0415.SplitSeq + "', "
                       + "'" + cmn.DrD0415.KtSeq + "', "
                       + "'" + cmn.DrD0415.HmCd + "', "
                       + "'" + cmn.DrD0415.KtCd + "', "
                       + "'" + cmn.DrD0415.OdrQty + "', "
                       + "'" + cmn.DrD0415.OdCd + "', "
                       + "'" + cmn.DrD0415.NextOdCd + "', "
                       + "'" + cmn.DrD0415.LtTime + "', "
                       + "cast('" + cmn.DrD0415.StDt + "' as datetime), "
                       + "'" + cmn.DrD0415.StTim + "', "
                       + "cast('" + cmn.DrD0415.EdDt + "' as datetime), "
                       + "'" + cmn.DrD0415.EdTim + "', "
                       + "'" + cmn.DrD0415.OdrSts + "', "
                       + "'" + cmn.DrD0415.QrCd + "', "
                       + "'" + cmn.DrD0415.JiQty + "', "
                       + "'" + cmn.DrD0415.DenpyoKbn + "', "
                       + "cast('" + cmn.DrD0415.DenpyoDt + "' as datetime), "
                       + "'" + cmn.DrD0415.Note + "', "
                       + "'" + cmn.DrD0415.WkNote + "', "
                       + "'" + cmn.DrD0415.WkComment + "', "
                       + "'" + cmn.DrD0415.DataKbn + "', "
                       + "'" + cmn.DrD0415.Instid + "', "
                       + "cast('" + cmn.DrD0415.InstDt + "' as datetime), "
                       + "'" + cmn.DrD0415.UpdtId + "', "
                       + "cast('" + cmn.DrD0415.UpdtDt + "' as datetime), "
                       + "'" + cmn.DrD0415.UkCd + "', "
                       + "'" + cmn.DrD0415.NaigaiKbn + "', "
                       + "'" + cmn.DrD0415.RetKtCd + "', "
                       + "'" + cmn.DrD0415.McGCd + "', "
                       + "'" + cmn.DrD0415.McCd + "', "
                       + "cast('" + cmn.DrD0415.KEdDt + "' as datetime), "
                       + "'" + cmn.DrD0415.KEdTim + "', "
                       + "'" + cmn.DrD0415.KOdrQty + "', "
                       + "'" + cmn.DrD0415.KMcCd + "', "
                       + "cast('" + cmn.DrD0415.WkDtDt + "' as datetime), "
                       + "cast('" + cmn.DrD0415.WkStDt + "' as datetime), "
                       + "cast('" + cmn.DrD0415.WkEdDt + "' as datetime)"
                       + ") "
                       + "on duplicate key update "
                       + "KEDDT = cast('" + cmn.DrD0415.KEdDt + "' as datetime), "
                       + "KEDTIM = '" + cmn.DrD0415.KEdTim + "', "
                       + "KODRQTY = '" + cmn.DrD0415.KOdrQty + "', "
                       + "KMCCD = '" + cmn.DrD0415.KMcCd + "' "
                       ;

            return sql;
        }

        /// <summary>
        /// 切削生産計画日程情報削除 (D0445 切削生産計画日程ファイル)
        /// </summary>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int DeleteCuttingProdScheduleInfo()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                // 切削生産計画日程ファイル削除 SQL 構文編集
                string sql = EditCuttingProdScheduleInfoDeleteQuery();

                // 削除
                using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
                {
                    using (MySqlTransaction txn = cnn.BeginTransaction())
                    {
                        try
                        {
                            int res = myCmd.ExecuteNonQuery();
                            if (res >= 1)
                            {
                                txn.Commit();
                                Debug.WriteLine(Common.TABLE_ID_D0445 + " table delete succeed and commited.");
                            }
                            ret = res;
                        }
                        catch (Exception e)
                        {
                            txn.Rollback();
                            Debug.WriteLine(Common.TABLE_ID_D0445 + " table no data deleted.");

                            Debug.WriteLine("Exception Source = " + e.Source);
                            Debug.WriteLine("Exception Message = " + e.Message);
                            if (cnn != null)
                            {
                                // 接続を閉じる
                                cnn.Close();
                            }
                            ret = -1;
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

        /// <summary>
        /// 切削生産計画日程情報削除 SQL 構文編集 (D0445 切削生産計画日程ファイル) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditCuttingProdScheduleInfoDeleteQuery()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 画面指定されたキーで削除
            string sql = "delete "
                       + "from "
                       + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0445 + " "
                       + "where "
                       + "ODCD like '%" + cmn.UqD0445.OdCd + "%' and "
                       + "PLNNO like '%" + cmn.UqD0445.PlnNo + "%' and "
                       + "ODRNO like '%" + cmn.UqD0445.OdrNo + "%' and "
                       + "KTSEQ like '%" + cmn.UqD0445.KtSeq + "%' and "
                       + "SEQ like '%" + cmn.UqD0445.Seq + "%' and "
                       + "MCSEQ like '%" + cmn.UqD0445.McSeq + "%' and "
                       + "SPLITSEQ like '%" + cmn.UqD0445.SplitSeq + "%' "
                       ;

            return sql;
        }

        /// <summary>
        /// 切削生産計画日程情報登録/更新 (D0445 切削生産計画日程ファイル)
        /// </summary>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int MergeCuttingProdScheduleInfo()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;

            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                // 切削生産計画日程ファイル登録/更新 SQL 構文編集
                string sql = EditCuttingProdScheduleInfoMergeQuery();

                using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
                {
                    using (MySqlTransaction txn = cnn.BeginTransaction())
                    {
                        try
                        {
                            int res = myCmd.ExecuteNonQuery();
                            if (res >= 1)
                            {
                                txn.Commit();
                                Debug.WriteLine(Common.TABLE_ID_D0445 + " table data insert/update succeed and commited.");
                            }
                            ret = res;
                        }
                        catch (Exception e)
                        {
                            txn.Rollback();
                            Debug.WriteLine(Common.TABLE_ID_D0445 + " table no data inserted/updated.");

                            Debug.WriteLine("Exception Source = " + e.Source);
                            Debug.WriteLine("Exception Message = " + e.Message);
                            if (cnn != null)
                            {
                                // 接続を閉じる
                                cnn.Close();
                            }
                            ret = -1;
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

        /// <summary>
        /// 切削生産計画日程情報登録/更新 SQL 構文編集 (D0445 切削生産計画日程ファイル) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        public string EditCuttingProdScheduleInfoMergeQuery()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 登録形式により抽出対象が異なる
            // MySql の DATE 型列に値を代入するときは、その列が時刻を持っているか否かに関わらず、必ず to_datetime('<代入元>') メソッドで変換してから代入する必要がある
            // 代入元が定数か変数化に関わらずシングル クォーテーション括りは必須
            // 代入元に書式 'YYYY/MM/DD HH24:MI:SS' 等の記述は不要、MySql が適切に合わせ込んで登録してくれる
            // この変換を怠ると「ORA-01861: リテラルが書式文字列と一致しません」の例外が発生する
            string sql = "insert into "
                       + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0445
                       + "("
                       + "ODCD, "
                       + "PLNNO, "
                       + "ODRNO, "
                       + "KTSEQ, "
                       + "MCSEQ, "
                       + "SPLITSEQ, "
                       + "HMCD, "
                       + "KTCD, "
                       + "UKCD, "
                       + "DATAKBN, "
                       + "HMSTKBN, "
                       + "ODRQTY, "
                       + "TRIALQTY, "
                       + "STDT, "
                       + "STTM, "
                       + "EDDT, "
                       + "EDTM, "
                       + "KJUNO, "
                       + "REASONKBN, "
                       + "NOTE, "
                       + "TKCD, "
                       + "TKCTLNO, "
                       + "TKHMCD, "
                       + "RQMNNO, "
                       + "RQSEQNO, "
                       + "RQBNO, "
                       + "NEXTKTCD, "
                       + "NEXTODCD, "
                       + "DVRQNO, "
                       + "ODRSTS, "
                       + "SEPDAY, "
                       + "WKNOTE, "
                       + "WKCOMMENT, "
                       + "KTKBN, "
                       + "KKTKKBN, "
                       + "SODKBN, "
                       + "NAIGAIKBN, "
                       + "INSTID, "
                       + "INSTDT, "
                       + "UPDTID, "
                       + "UPDTDT, "
                       + "JIQTY, "
                       + "SEQ, "
                       + "MCGCD, "
                       + "MCCD, "
                       + "KEDDT, "
                       + "KEDTM, "
                       + "KODRQTY, "
                       + "KMCCD, "
                       + "WKDTDT, "
                       + "WKSTDT, "
                       + "WKEDDT"
                       + ") "
                       + "values "
                       + "("
                       + "'" + cmn.UqD0445.OdCd + "', "
                       + "'" + cmn.UqD0445.PlnNo + "', "
                       + "'" + cmn.UqD0445.OdrNo + "', "
                       + "'" + cmn.UqD0445.KtSeq + "', "
                       + "'" + cmn.UqD0445.McSeq + "', "
                       + "'" + cmn.UqD0445.SplitSeq + "', "
                       + "'" + cmn.DrD0445.HmCd + "', "
                       + "'" + cmn.DrD0445.KtCd + "', "
                       + "'" + cmn.DrD0445.UkCd + "', "
                       + "'" + cmn.DrD0445.DataKbn + "', "
                       + "'" + cmn.DrD0445.HmStKbn + "', "
                       + "'" + cmn.DrD0445.OdrQty + "', "
                       + "'" + cmn.DrD0445.TrialQty + "', "
                       + "cast('" + cmn.DrD0445.StDt + "' as datetime), "
                       + "'" + cmn.DrD0445.AtTm + "', "
                       + "cast('" + cmn.DrD0445.EdDt + "' as datetime), "
                       + "'" + cmn.DrD0445.EdTm + "', "
                       + "'" + cmn.DrD0445.KjuNo + "', "
                       + "'" + cmn.DrD0445.ReasonKbn + "', "
                       + "'" + cmn.DrD0445.Note + "', "
                       + "'" + cmn.DrD0445.TkCd + "', "
                       + "'" + cmn.DrD0445.TkCtlNo + "', "
                       + "'" + cmn.DrD0445.TkHmCd + "', "
                       + "'" + cmn.DrD0445.RqMnNo + "', "
                       + "'" + cmn.DrD0445.RqSeqNo + "', "
                       + "'" + cmn.DrD0445.RqbNo + "', "
                       + "'" + cmn.DrD0445.NextKtCd + "', "
                       + "'" + cmn.DrD0445.NextOdCd + "', "
                       + "'" + cmn.DrD0445.DvRqNo + "', "
                       + "'" + cmn.DrD0445.OdrSts + "', "
                       + "'" + cmn.DrD0445.SepDay + "', "
                       + "'" + cmn.DrD0445.WkNote + "', "
                       + "'" + cmn.DrD0445.WkComment + "', "
                       + "'" + cmn.DrD0445.KtKbn + "', "
                       + "'" + cmn.DrD0445.KkTkKbn + "', "
                       + "'" + cmn.DrD0445.SodKbn + "', "
                       + "'" + cmn.DrD0445.NaigaiKbn + "', "
                       + "'" + cmn.DrD0445.InstId + "', "
                       + "cast('" + cmn.DrD0445.InstDt + "' as datetime), "
                       + "'" + cmn.DrD0445.UpdtId + "', "
                       + "cast('" + cmn.DrD0445.UpdtDt + "' as datetime), "
                       + "'" + cmn.DrD0445.JiQty + "', "
                       + "'" + cmn.DrD0445.Seq + "', "
                       + "'" + cmn.DrD0445.McGCd + "', "
                       + "'" + cmn.DrD0445.McCd + "', "
                       + "cast('" + cmn.DrD0445.KEdDt + "' as datetime), "
                       + "'" + cmn.DrD0445.KEdTm + "', "
                       + "'" + cmn.DrD0445.KOdrQty + "', "
                       + "'" + cmn.DrD0445.KMcCd + "', "
                       + "cast('" + cmn.DrD0445.WkDtDt + "' as datetime), "
                       + "cast('" + cmn.DrD0445.WkStDt + "' as datetime), "
                       + "cast('" + cmn.DrD0445.WkEdDt + "' as datetime)"
                       + ") "
                       + "on duplicate key update "
                       + "KEDDT = cast('" + cmn.DrD0445.KEdDt + "' as datetime), "
                       + "KEDTM = '" + cmn.DrD0445.KEdTm + "', "
                       + "KODRQTY = '" + cmn.DrD0445.KOdrQty + "', "
                       + "KMCCD = '" + cmn.DrD0445.KMcCd + "' "
                       ;

            return sql;
        }

        ///// <summary>
        ///// 切削コード票情報削除 SQL 構文編集 (KM8430 切削コード票マスター) 
        ///// </summary>
        ///// <returns>SQL 構文</returns>
        //private string EditCycleTimeInfoDeleteSql()
        //{
        //    Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

        //    // 画面指定されたキーで削除
        //    string sql = "delete "
        //               + "from "
        //               + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " "
        //               + "where "
        //               + "( "
        //               + "ODCD, "
        //               + "WKGRCD, "
        //               + "HMCD, "
        //               + "VALDTF, "
        //               + "WKSEQ "
        //               + ") "
        //               + "in "
        //               + "( "
        //               + "select "
        //               + "ODCD, "
        //               + "WKGRCD, "
        //               + "HMCD, "
        //               + "VALDTF, "
        //               + "WKSEQ "
        //               + "from "
        //               + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " "
        //               + "where "
        //               + "ODCD like '%" + cmn.D0445.OdCd + "%' and "
        //               + "WKGRCD like '%" + cmn.D0445.WkGrCd + "%' and "
        //               + "HMCD like '%" + cmn.D0445.HmCd + "%' and "
        //               + "VALDTF between to_timestamp('" + cmn.D0445.ValDtFF + "') and "
        //               + "to_timestamp('" + cmn.D0445.ValDtFT + "') and "
        //               + "WKSEQ like '%" + cmn.D0445.WkSeq + "%' "
        //               + ") "
        //               ;

        //    return sql;
        //}

        /// <summary>
        /// 切削コード票情報登録/更新 (KM8430 切削コード票マスター)
        /// </summary>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int MergeCycleTimeInfo()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;

            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                // 切削コード票マスター登録/更新 SQL 構文編集
                string sql = EditCycleTimeInfoMergeSql();

                using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
                {
                    using (MySqlTransaction txn = cnn.BeginTransaction())
                    {
                        try
                        {
                            int res = myCmd.ExecuteNonQuery();
                            if (res >= 1)
                            {
                                txn.Commit();
                                Debug.WriteLine(Common.TABLE_ID_KM8430 + " table data insert/update succeed and commited.");
                            }
                            ret = res;
                        }
                        catch (Exception e)
                        {
                            txn.Rollback();
                            Debug.WriteLine(Common.TABLE_ID_KM8430 + " table no data inserted/updated.");

                            Debug.WriteLine("Exception Source = " + e.Source);
                            Debug.WriteLine("Exception Message = " + e.Message);
                            if (cnn != null)
                            {
                                // 接続を閉じる
                                cnn.Close();
                            }
                            ret = -1;
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

        /// <summary>
        /// 切削コード票情報登録/更新 SQL 構文編集 (KM8430 切削コード票マスター) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        public string EditCycleTimeInfoMergeSql()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 登録形式により抽出対象が異なる
            // MySql の DATE 型列に値を代入するときは、その列が時刻を持っているか否かに関わらず、必ず to_datetime('<代入元>') メソッドで変換してから代入する必要がある
            // 代入元が定数か変数化に関わらずシングル クォーテーション括りは必須
            // 代入元に書式 'YYYY/MM/DD HH24:MI:SS' 等の記述は不要、MySql が適切に合わせ込んで登録してくれる
            // この変換を怠ると「ORA-01861: リテラルが書式文字列と一致しません」の例外が発生する
            string sql = "merge "
                       + "into "
                       + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " k "
                       + "using "
                       + "("
                       + "select "
                       + "'" + cmn.PkKM8430.OdCd + "' " + "ODCD, "
                       + "'" + cmn.PkKM8430.WkGrCd + "' " + "WKGRCD, "
                       + "'" + cmn.PkKM8430.HmCd + "' " + "HMCD, "
                       + "to_timestamp('" + cmn.PkKM8430.ValDtFF + "') " + "VALDTF, "
                       + "'" + cmn.PkKM8430.WkSeq + "' " + "WKSEQ, "
                       + "'" + cmn.DrKM8430.CT + "' " + "CT, "
                       + "'" + cmn.DrKM8430.Note + "' " + "NOTE, "
                       + "'" + cmn.DrCommon.InstID + "' " + "INSTID, "
                       + "'" + cmn.DrCommon.UpdtID + "' " + "UPDTID "
                       + "from "
                       + "DUAL"
                       + ") d "
                       + "on "
                       + "("
                       + "k.ODCD = d.ODCD and "
                       + "k.WKGRCD = d.WKGRCD and "
                       + "k.HMCD = d.HMCD and "
                       + "k.VALDTF = d.VALDTF and "
                       + "k.WKSEQ = d.WKSEQ "
                       + ") "
                       + "when matched then "
                       + "update "
                       + "set "
                       + "k.CT = d.CT, "
                       + "k.NOTE = d.NOTE, "
                       + "k.UPDTID = d.UPDTID, "
                       + "k.UPDTDT = SYSDATE "
                       + "when not matched then "
                       + "insert "
                       + "("
                       + "k.ODCD, "
                       + "k.WKGRCD, "
                       + "k.HMCD, "
                       + "k.VALDTF, "
                       + "k.WKSEQ, "
                       + "k.CT, "
                       + "k.NOTE, "
                       + "k.INSTID, "
                       + "k.INSTDT, "
                       + "k.UPDTID, "
                       + "k.UPDTDT"
                       + ") "
                       + "values "
                       + "("
                       + "d.ODCD, "
                       + "d.WKGRCD, "
                       + "d.HMCD, "
                       + "d.VALDTF, "
                       + "d.WKSEQ, "
                       + "d.CT, "
                       + "d.NOTE, "
                       + "d.INSTID, "
                       + "SYSDATE, "
                       + "d.UPDTID, "
                       + "SYSDATE"
                       + ")"
                       ;
            return sql;
        }

        /// <summary>
        /// 注文情報データ取得
        /// </summary>
        /// <param name="mpDt">注文情報データ</param>
        /// <param name="firstDayOfMonth">検査対象月</param>
        /// <returns>注文情報データ</returns>
        public bool GetMPOrderInfo(ref DataTable mpDt, DateTime firstDayOfMonth)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string yyyyMMdd = firstDayOfMonth.ToString("yyyy/MM/dd");
                string sql = "SELECT "
                    + "EDDT "
                    + ", concat('',sum(case when ODRSTS = '2' then 1 else 0 end)) \"MP2確定\" "
                    + ", concat('',sum(case when ODRSTS = '3' then 1 else 0 end)) \"MP3着手\" "
                    + ", concat('',sum(case when ODRSTS = '4' then 1 else 0 end)) \"MP4完了\" "
                    + ", concat('',sum(case when ODRSTS = '9' then 1 else 0 end)) \"MP9取消\" "
                    + ", concat('',sum(case when ODRSTS in ('2','3','4','9') then 1 else 0 end)) \"取込合計\" "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0410 + " "
                    + "WHERE "
                    + "ODCD like '6060%' "
                    + "and EDDT between "
                    + $"adddate(date_format(str_to_date('{yyyyMMdd}', '%Y/%m/%d'), '%Y-%m-01'), interval -7 day) "
                    + $"and adddate(last_day(str_to_date('{yyyyMMdd}', '%Y/%m/%d')), interval 7 day) "
                    + "GROUP BY EDDT "
                    + "ORDER BY EDDT "
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        // 結果取得
                        myDa.Fill(mpDt);
                        ret = true;
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
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return ret;
        }

        /// <summary>
        /// 注文情報取込
        /// </summary>
        /// <param name="eddts">取込対象の日付をinで使えるカンマ区切り形式で指定</param>
        /// <returns>注文情報取込</returns>
        public bool OrderImport(String eddts)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            //OracleConnection emCnn = null;
            MySqlConnection mpCnn = null;
            MySqlTransaction transaction = null;

            try
            {
                // EMシステム、切削生産計画システム データベースへ接続
                //cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref emCnn);

                // toolStripStatusLabel.Text = "Oracle Database の 読み込み中";
                DataTable dtOra = new DataTable();
                DateTime pcDate = DateTime.Now.AddMonths(-1);
                string yyMM = pcDate.ToString("yyMM");
                string sql;
                sql = "SELECT * "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_D0410 + " "
                    + "WHERE "
                    + $"ODRNO > {yyMM}000000 " // EDDTにインデックスが貼ってないので検索対象をまず絞ってから抽出する
                    + "and ODCD like '6060%' "
                    + "and EDDT in " + eddts
                ;
                //using (OracleCommand myCmd = new OracleCommand(sql, emCnn))
                //{
                //    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                //    {
                //        Debug.WriteLine("Read from DataTable:");
                //        using (DataTable myDt = new DataTable())
                //        {
                //            // 結果取得
                //            myDa.Fill(myDt);
                //            if (myDt.Rows.Count == 0)
                //            {
                //                Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
                //                cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_802, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);
                //                ret = false;
                //            }
                //            else
                //            {
                //                dtOra = myDt;
                //            }
                //        }
                //    }
                //}

                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);
                // トランザクション開始
                transaction = mpCnn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mpCnn;

                var insCount = 0;
                foreach (DataRow row in dtOra.Rows)
                {
                    sql = "insert into D0410 values ('" + row["ODRNO"].ToString() + "'," + row["KTSEQ"] + "," + "'" + row["HMCD"].ToString() + "'," +
                        "'" + row["KTCD"].ToString() + "'," + row["ODRQTY"] + "," + "'" + row["ODCD"].ToString() + "'," +
                        "'" + row["NEXTODCD"].ToString() + "'," + row["LTTIME"] + "," + "'" + row["STDT"] + "'," +
                        "'" + row["STTIM"].ToString() + "'," + "'" + row["EDDT"] + "'," + "'" + row["EDTIM"].ToString() + "'," +
                        "'" + row["ODRSTS"].ToString() + "'," + "'" + row["QRCD"].ToString() + "'," + row["JIQTY"] + "," +
                        "'" + row["DENPYOKBN"].ToString() + "'," +
                        (row["DENPYODT"].ToString() == "" ? "null" : "'" + row["DENPYODT"] + "'") + "," +
                        "'" + row["NOTE"].ToString() + "'," +
                        "'" + row["WKNOTE"].ToString() + "'," + "'" + row["WKCOMMENT"].ToString() + "'," + "'" + row["DATAKBN"].ToString() + "'," +
                        "'" + row["INSTID"].ToString() + "'," + "'" + row["INSTDT"] + "'," + "'" + row["UPDTID"].ToString() + "'," +
                        "'" + row["UPDTDT"] + "'," + "'" + row["UKCD"].ToString() + "'," + "'" + row["NAIGAIKBN"].ToString() + "'," +
                        "'" + row["RETKTCD"].ToString() + "')";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    insCount++;
                }

                // トランザクション終了
                transaction.Commit();
                ret = true;
            }
            catch (Exception ex)
            {
                // ロールバック
                if (transaction != null) transaction.Rollback();

                // エラー
                string msg = "Exception Source = " + ex.Source + ", Message = " + ex.Message;
                if (AssemblyState.IsDebug) Debug.WriteLine(msg);

                Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
                cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_802, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);
                ret = false;
            }
            // 接続を閉じる
            //cmn.Dbm.CloseOraSchema(emCnn);
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return ret;
        }

        /// <summary>
        /// 計画出庫データの取得
        /// </summary>
        /// <param name="dt">計画出庫データ</param>
        /// <param name="eddt">出庫予定日</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int GetShipmentPlanInfo(ref DataTable mpDt, DateTime eddt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            MySqlConnection mpCnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                // 三進金属様：入出庫データ連携フォーマット一覧より (出力ファイル名：OutboundInfo.csv)
                string sql =
                "SELECT "
                    + "DATE_FORMAT(EDDT, '%Y%m%d') AS 'yyyyMMdd' "
                    + ", SPACE(15) as '伝票番号' "
                    + ", '01' as '出庫区分' "
                    + ", 'C000000001' as '出荷先コード' " // （今の所、仮のコード 本社:C000000001, EWU:C000000002, 協力会社:C000000010)
                    + ", REPEAT('　', 40) as '出荷先名' "
                    + ", SPACE(10) as '荷主コード' " // （今の所、仮のコード 本社:C000000001, EWU:C000000002, 協力会社:C000000010)
                    + ", REPEAT('　', 40) as '荷主名' "
                    + ", RPAD(a.HMCD, 28, SPACE(1)) as '商品コード' "
                    + ", RPAD(f_han2zen(IFNULL(b.HMNM, '')), 40, '　') as '商品名' "
                    + ", RPAD(IFNULL(b.BOXQTY, ''), 100, SPACE(1)) as '管理項目１' "
                    + ", SPACE(100) as '管理項目２' "
                    + ", SPACE(100) as '管理項目３' "
                    + ", SPACE(100) as '管理項目４' "
                    + ", LPAD(ODRQTY, 6, '0') as '出庫予定数量' "
                    + ", REPEAT('　', 50) as '備考' "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0410 + " a, "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_M0500 + " b " // 仮　本来は Common.TABLE_ID_KM8430だけどメンテしてないのでM0500にしておく
                    + "WHERE "
                    + "a.HMCD = b.HMCD "
                    + "and ODRSTS <> '9' "
                    + "and ODCD like '6060%' "
                    + $"and EDDT = '{eddt}' "
                    + $"and DENPYODT is null " // 仮で DENPYODT を利用させてもらっている
                    + "ORDER BY ODRNO"
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(mpDt);
                        ret = mpDt.Rows.Count;
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
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return ret;
        }

        /// <summary>
        /// 対象月の出庫予定データ出力済みの一覧を取得
        /// </summary>
        /// <param name="mpDt">出庫予定データ出力済み一覧</param>
        /// <param name="targerMonth">対象月</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int GetShipmentPlanDays(ref DataTable mpDt, DateTime targerMonth)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            MySqlConnection mpCnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                /* 土日の完了予定日がない事が前提のシステムが大丈夫な事を証明するOracle側のSQL文
                select eddt, to_char(eddt, 'DY'), count(*) from d0410 
                where odrsts <>'9' and odcd like '6060%' 
                and eddt between '2009/01/01' 
                             and '2025/12/31'
                group by eddt
                having to_char(eddt, 'DY') in ('土', '日', 'ｘ')
                order by eddt
                */
                string sql =
                "SELECT "
                    + "EDDT "
                    + ", COUNT(*) as '手配件数' "
                    + ", COUNT(DENPYODT) as '発行済件数' " // 仮でDENPYODT項目を無断使用している
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0410 + " "
                    + "WHERE "
                    + "ODRSTS <> '9' "
                    + "and ODCD like '6060%' "
                    + $"and EDDT between '{targerMonth.AddMonths(-1).ToString()}' and '{targerMonth.AddMonths(2).ToString()}' "
                    + "GROUP BY EDDT "
                    + "ORDER BY EDDT "
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(mpDt);
                        ret = mpDt.Rows.Count;
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
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return ret;
        }

        /// <summary>
        /// 計画出庫データ出力済みに更新
        /// </summary>
        /// <param name="planDay">検査対象月</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int UpdateShipmentPlanDay(DateTime planDay)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;

            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                // 計画出庫データ出力済みに更新 SQL
                string sql = 
                "UPDATE "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0410 + " "
                + "SET "
                + "DENPYODT = now(), " // 仮でDENPYODT項目を更新してみる
                + "UPDTID = '" + cmn.DrCommon.UpdtID + "' "
                + "WHERE "
                + "ODRSTS<>'9' and "
                + "ODCD like '6060%' and "
                + "EDDT = '" + planDay.ToString() + "' and "
                + "DENPYODT is NULL "
                ;

                using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
                {
                    using (MySqlTransaction txn = cnn.BeginTransaction())
                    {
                        try
                        {
                            int res = myCmd.ExecuteNonQuery();
                            if (res >= 1)
                            {
                                txn.Commit();
                                Debug.WriteLine(Common.TABLE_ID_D0440 + " table data update succeed and commited.");
                            }
                            ret = res;
                        }
                        catch (Exception e)
                        {
                            txn.Rollback();
                            Debug.WriteLine(Common.TABLE_ID_D0440 + " table no data inserted/updated.");

                            Debug.WriteLine("Exception Source = " + e.Source);
                            Debug.WriteLine("Exception Message = " + e.Message);
                            if (cnn != null)
                            {
                                // 接続を閉じる
                                cnn.Close();
                            }
                            ret = -1;
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

        /// <summary>
        /// 検査対象月（前後プラス１か月）の内、カード発行済みの日付一覧を取得
        /// </summary>
        /// <param name="mpDt">カード発行済み日付一覧</param>
        /// <param name="targerMonth">検査対象月</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int GetCardPrintDays(ref DataTable mpDt, DateTime targerMonth)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            MySqlConnection mpCnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql =
                "SELECT "
                    + "EDDT "
                    + ", COUNT(*) as '手配件数' "
                    + ", COUNT(DENPYODT) as '発行済件数' "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_D0410 + " "
                    + "WHERE "
                    + "ODRSTS <> '9' "
                    + "and ODCD like '6060%' "
                    + $"and EDDT between '{targerMonth.AddMonths(-1).ToString()}' and '{targerMonth.AddMonths(1).ToString()}' "
                    + "GROUP BY EDDT "
                    + "ORDER BY EDDT "
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(mpDt);
                        ret = mpDt.Rows.Count;
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
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return ret;
        }




    }
}
