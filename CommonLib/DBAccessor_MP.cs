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
        /// 切削オーダー平準化情報取得 (KD8430 切削生産計画ファイル, KD8440 切削生産計画日程ファイル, KM8420 切削設備マスタ)
        /// </summary>
        /// <param name="dataSet">データセット</param>
        /// <param name="searchTarget">検索対象 (0: 現状全件, 1: 確定全件 2: 特定データ, 3: グループのみ, 4: 設備のみ)</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int GetOrderEqualizeInfo(ref DataSet dataSet, int searchTarget = Common.KD8430_TARGET_SPECIFIC)
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
                                case Common.KD8430_TARGET_CUR_ALL:
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
                                case Common.KD8430_TARGET_SIM_ALL:
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
                                case Common.KD8430_TARGET_SPECIFIC:
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
                                case Common.KD8430_TARGET_MCGCD:
                                    {
                                        foreach (DataRow dr in myDs.Tables[0].Rows)
                                        {
                                            Debug.Write("MCGCD = " + dr[0] + ", ");
                                            Debug.Write("MCGNM = " + dr[1]);
                                            Debug.WriteLine("");
                                        }
                                        break;
                                    }
                                case Common.KD8430_TARGET_MCCD:
                                    {
                                        foreach (DataRow dr in myDs.Tables[0].Rows)
                                        {
                                            Debug.Write("MCCD = "     + dr[0] + ", ");
                                            Debug.Write("MCNM = "     + dr[1]);
                                            Debug.WriteLine("");
                                        }
                                        break;
                                    }
                                case Common.KD8430_TARGET_MCTM:
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
        /// 切削オーダー平準化情報取得 SQL 構文編集 (kd8430 切削生産計画ファイル, kd8440 切削生産計画日程ファイル, KM8420 切削設備マスタ)
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
                case Common.KD8430_TARGET_CUR_ALL:
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
                            + "'" + Common.TABLE_ID_KD8430 + "' as TBLNM "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkKD8430.EdDt + "' and "
                                         + "'" + cmn.IkKD8430.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkKD8430.McGCd + "' and "
                            + "MCCD = '" + cmn.IkKD8430.McCd + "' "
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
                            + "'" + Common.TABLE_ID_KD8440 + "' as TBLNM "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkKD8440.EdDt + "' and "
                                         + "'" + cmn.IkKD8440.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkKD8440.McGCd + "' and "
                            + "MCCD = '" + cmn.IkKD8440.McCd + "' "
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
                case Common.KD8430_TARGET_SIM_ALL:
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
                            + "'" + Common.TABLE_ID_KD8430 + "' as TBLNM "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                            + "where "
                            + "KEDDT between '" + cmn.IkKD8430.EdDt + "' and "
                                         + "'" + cmn.IkKD8430.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkKD8430.McGCd + "' and "
                            + "KMCCD = '" + cmn.IkKD8430.McCd + "' "
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
                            + "'" + Common.TABLE_ID_KD8440 + "' as TBLNM "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " "
                            + "where "
                            + "KEDDT between '" + cmn.IkKD8440.EdDt + "' and "
                                         + "'" + cmn.IkKD8440.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkKD8440.McGCd + "' and "
                            + "KMCCD = '" + cmn.IkKD8440.McCd + "' "
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
                case Common.KD8430_TARGET_SPECIFIC:
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
                            + "'" + Common.TABLE_ID_KD8430 + "' as TBLNM "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                            + "where "
                            + "KEDDT between '" + cmn.IkKD8430.EdDt + "' and "
                                         + "'" + cmn.IkKD8430.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkKD8430.McGCd + "' and "
                            + "KMCCD = '" + cmn.IkKD8430.McCd + "' "
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
                            + "'" + Common.TABLE_ID_KD8440 + "' as TBLNM "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " "
                            + "where "
                            + "KEDDT between '" + cmn.IkKD8440.EdDt + "' and "
                                         + "'" + cmn.IkKD8440.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkKD8440.McGCd + "' and "
                            + "KMCCD = '" + cmn.IkKD8440.McCd + "' "
                            + "order by "
                            + "KEDDT, "
                            + "KMCCD, "
                            + "ODRNO "
                            ;
                        break;
                    }
                case Common.KD8430_TARGET_MCGCD:
                    {
                        sql = "select distinct "
                            + "a.MCGCD, "
                            + "b.MCGNM "
                            + "from "
                            + "("
                            + "select "
                            + "MCGCD "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkKD8430.EdDt + "' and "
                                         + "'" + cmn.IkKD8430.EdDt + "' "
                            + "union all "
                            + "select "
                            + "MCGCD "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkKD8440.EdDt + "' and "
                                         + "'" + cmn.IkKD8440.EdDt + "' "
                            + ") a, "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420 + " b "
                            + "where "
                            + "b.MCGCD = a.MCGCD "
                            + "order by "
                            + "a.MCGCD "
                            ;
                        break;
                    }
                case Common.KD8430_TARGET_MCCD:
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
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkKD8430.EdDt + "' and "
                                         + "'" + cmn.IkKD8430.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkKD8430.McGCd + "' "
                            + "union all "
                            + "select "
                            + "MCGCD, "
                            + "MCCD "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkKD8440.EdDt + "' and "
                                         + "'" + cmn.IkKD8440.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkKD8440.McGCd + "' "
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
                case Common.KD8430_TARGET_MCTM:
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
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkKD8430.EdDt + "' and "
                                         + "'" + cmn.IkKD8430.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkKD8430.McGCd + "' and "
                            + "MCCD = '" + cmn.IkKD8430.McCd + "' "
                            + "union all "
                            + "select "
                            + "MCGCD, "
                            + "MCCD "
                            + "from "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " "
                            + "where "
                            + "EDDT between '" + cmn.IkKD8440.EdDt + "' and "
                                         + "'" + cmn.IkKD8440.EdDt + "' and "
                            + "MCGCD = '" + cmn.IkKD8440.McGCd + "' and "
                            + "MCCD = '" + cmn.IkKD8440.McCd + "' "
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
        /// 切削生産計画情報削除 (KD8430 切削生産計画ファイル)
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
                                Debug.WriteLine(Common.TABLE_ID_KD8430 + " table delete succeed and commited.");
                            }
                            ret = res;
                        }
                        catch (Exception e)
                        {
                            txn.Rollback();
                            Debug.WriteLine(Common.TABLE_ID_KD8430 + " table no data deleted.");

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
        /// 切削生産計画情報削除 SQL 構文編集 (KD8430 切削生産計画ファイル) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditCuttingProdPlanInfoDeleteQuery()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 画面指定されたキーで削除
            string sql = "delete "
                       + "from "
                       + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                       + "where "
                       + "ODRNO like '%" + cmn.PkKD8430.OdrNo + "%' and "
                       + "MCSEQ like '%" + cmn.PkKD8430.McSeq + "%' and "
                       + "SPLITSEQ like '%" + cmn.PkKD8430.SplitSeq + "%' "
                       ;

            return sql;
        }

        /// <summary>
        /// 切削生産計画情報登録/更新 (KD8430 切削生産計画ファイル)
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
                                Debug.WriteLine(Common.TABLE_ID_KD8430 + " table data insert/update succeed and commited.");
                            }
                            ret = res;
                        }
                        catch (Exception e)
                        {
                            txn.Rollback();
                            Debug.WriteLine(Common.TABLE_ID_KD8430 + " table no data inserted/updated.");

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
        /// 切削生産計画情報登録/更新 SQL 構文編集 (KD8430 切削生産計画ファイル) 
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
                       + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430
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
                       + "'" + cmn.PkKD8430.OdrNo + "', "
                       + "'" + cmn.PkKD8430.McSeq + "', "
                       + "'" + cmn.PkKD8430.SplitSeq + "', "
                       + "'" + cmn.DrKD8430.KtSeq + "', "
                       + "'" + cmn.DrKD8430.HmCd + "', "
                       + "'" + cmn.DrKD8430.KtCd + "', "
                       + "'" + cmn.DrKD8430.OdrQty + "', "
                       + "'" + cmn.DrKD8430.OdCd + "', "
                       + "'" + cmn.DrKD8430.NextOdCd + "', "
                       + "'" + cmn.DrKD8430.LtTime + "', "
                       + "cast('" + cmn.DrKD8430.StDt + "' as datetime), "
                       + "'" + cmn.DrKD8430.StTim + "', "
                       + "cast('" + cmn.DrKD8430.EdDt + "' as datetime), "
                       + "'" + cmn.DrKD8430.EdTim + "', "
                       + "'" + cmn.DrKD8430.OdrSts + "', "
                       + "'" + cmn.DrKD8430.QrCd + "', "
                       + "'" + cmn.DrKD8430.JiQty + "', "
                       + "'" + cmn.DrKD8430.DenpyoKbn + "', "
                       + "cast('" + cmn.DrKD8430.DenpyoDt + "' as datetime), "
                       + "'" + cmn.DrKD8430.Note + "', "
                       + "'" + cmn.DrKD8430.WkNote + "', "
                       + "'" + cmn.DrKD8430.WkComment + "', "
                       + "'" + cmn.DrKD8430.DataKbn + "', "
                       + "'" + cmn.DrKD8430.Instid + "', "
                       + "cast('" + cmn.DrKD8430.InstDt + "' as datetime), "
                       + "'" + cmn.DrKD8430.UpdtId + "', "
                       + "cast('" + cmn.DrKD8430.UpdtDt + "' as datetime), "
                       + "'" + cmn.DrKD8430.UkCd + "', "
                       + "'" + cmn.DrKD8430.NaigaiKbn + "', "
                       + "'" + cmn.DrKD8430.RetKtCd + "', "
                       + "'" + cmn.DrKD8430.McGCd + "', "
                       + "'" + cmn.DrKD8430.McCd + "', "
                       + "cast('" + cmn.DrKD8430.KEdDt + "' as datetime), "
                       + "'" + cmn.DrKD8430.KEdTim + "', "
                       + "'" + cmn.DrKD8430.KOdrQty + "', "
                       + "'" + cmn.DrKD8430.KMcCd + "', "
                       + "cast('" + cmn.DrKD8430.WkDtDt + "' as datetime), "
                       + "cast('" + cmn.DrKD8430.WkStDt + "' as datetime), "
                       + "cast('" + cmn.DrKD8430.WkEdDt + "' as datetime)"
                       + ") "
                       + "on duplicate key update "
                       + "KEDDT = cast('" + cmn.DrKD8430.KEdDt + "' as datetime), "
                       + "KEDTIM = '" + cmn.DrKD8430.KEdTim + "', "
                       + "KODRQTY = '" + cmn.DrKD8430.KOdrQty + "', "
                       + "KMCCD = '" + cmn.DrKD8430.KMcCd + "' "
                       ;

            return sql;
        }

        /// <summary>
        /// 切削生産計画日程情報削除 (KD8440 切削生産計画日程ファイル)
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
                                Debug.WriteLine(Common.TABLE_ID_KD8440 + " table delete succeed and commited.");
                            }
                            ret = res;
                        }
                        catch (Exception e)
                        {
                            txn.Rollback();
                            Debug.WriteLine(Common.TABLE_ID_KD8440 + " table no data deleted.");

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
        /// 切削生産計画日程情報削除 SQL 構文編集 (KD8440 切削生産計画日程ファイル) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string EditCuttingProdScheduleInfoDeleteQuery()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 画面指定されたキーで削除
            string sql = "delete "
                       + "from "
                       + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " "
                       + "where "
                       + "ODCD like '%" + cmn.UqKD8440.OdCd + "%' and "
                       + "PLNNO like '%" + cmn.UqKD8440.PlnNo + "%' and "
                       + "ODRNO like '%" + cmn.UqKD8440.OdrNo + "%' and "
                       + "KTSEQ like '%" + cmn.UqKD8440.KtSeq + "%' and "
                       + "SEQ like '%" + cmn.UqKD8440.Seq + "%' and "
                       + "MCSEQ like '%" + cmn.UqKD8440.McSeq + "%' and "
                       + "SPLITSEQ like '%" + cmn.UqKD8440.SplitSeq + "%' "
                       ;

            return sql;
        }

        /// <summary>
        /// 切削生産計画日程情報登録/更新 (KD8440 切削生産計画日程ファイル)
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
                                Debug.WriteLine(Common.TABLE_ID_KD8440 + " table data insert/update succeed and commited.");
                            }
                            ret = res;
                        }
                        catch (Exception e)
                        {
                            txn.Rollback();
                            Debug.WriteLine(Common.TABLE_ID_KD8440 + " table no data inserted/updated.");

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
        /// 切削生産計画日程情報登録/更新 SQL 構文編集 (KD8440 切削生産計画日程ファイル) 
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
                       + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440
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
                       + "'" + cmn.UqKD8440.OdCd + "', "
                       + "'" + cmn.UqKD8440.PlnNo + "', "
                       + "'" + cmn.UqKD8440.OdrNo + "', "
                       + "'" + cmn.UqKD8440.KtSeq + "', "
                       + "'" + cmn.UqKD8440.McSeq + "', "
                       + "'" + cmn.UqKD8440.SplitSeq + "', "
                       + "'" + cmn.DrKD8440.HmCd + "', "
                       + "'" + cmn.DrKD8440.KtCd + "', "
                       + "'" + cmn.DrKD8440.UkCd + "', "
                       + "'" + cmn.DrKD8440.DataKbn + "', "
                       + "'" + cmn.DrKD8440.HmStKbn + "', "
                       + "'" + cmn.DrKD8440.OdrQty + "', "
                       + "'" + cmn.DrKD8440.TrialQty + "', "
                       + "cast('" + cmn.DrKD8440.StDt + "' as datetime), "
                       + "'" + cmn.DrKD8440.AtTm + "', "
                       + "cast('" + cmn.DrKD8440.EdDt + "' as datetime), "
                       + "'" + cmn.DrKD8440.EdTm + "', "
                       + "'" + cmn.DrKD8440.KjuNo + "', "
                       + "'" + cmn.DrKD8440.ReasonKbn + "', "
                       + "'" + cmn.DrKD8440.Note + "', "
                       + "'" + cmn.DrKD8440.TkCd + "', "
                       + "'" + cmn.DrKD8440.TkCtlNo + "', "
                       + "'" + cmn.DrKD8440.TkHmCd + "', "
                       + "'" + cmn.DrKD8440.RqMnNo + "', "
                       + "'" + cmn.DrKD8440.RqSeqNo + "', "
                       + "'" + cmn.DrKD8440.RqbNo + "', "
                       + "'" + cmn.DrKD8440.NextKtCd + "', "
                       + "'" + cmn.DrKD8440.NextOdCd + "', "
                       + "'" + cmn.DrKD8440.DvRqNo + "', "
                       + "'" + cmn.DrKD8440.OdrSts + "', "
                       + "'" + cmn.DrKD8440.SepDay + "', "
                       + "'" + cmn.DrKD8440.WkNote + "', "
                       + "'" + cmn.DrKD8440.WkComment + "', "
                       + "'" + cmn.DrKD8440.KtKbn + "', "
                       + "'" + cmn.DrKD8440.KkTkKbn + "', "
                       + "'" + cmn.DrKD8440.SodKbn + "', "
                       + "'" + cmn.DrKD8440.NaigaiKbn + "', "
                       + "'" + cmn.DrKD8440.InstId + "', "
                       + "cast('" + cmn.DrKD8440.InstDt + "' as datetime), "
                       + "'" + cmn.DrKD8440.UpdtId + "', "
                       + "cast('" + cmn.DrKD8440.UpdtDt + "' as datetime), "
                       + "'" + cmn.DrKD8440.JiQty + "', "
                       + "'" + cmn.DrKD8440.Seq + "', "
                       + "'" + cmn.DrKD8440.McGCd + "', "
                       + "'" + cmn.DrKD8440.McCd + "', "
                       + "cast('" + cmn.DrKD8440.KEdDt + "' as datetime), "
                       + "'" + cmn.DrKD8440.KEdTm + "', "
                       + "'" + cmn.DrKD8440.KOdrQty + "', "
                       + "'" + cmn.DrKD8440.KMcCd + "', "
                       + "cast('" + cmn.DrKD8440.WkDtDt + "' as datetime), "
                       + "cast('" + cmn.DrKD8440.WkStDt + "' as datetime), "
                       + "cast('" + cmn.DrKD8440.WkEdDt + "' as datetime)"
                       + ") "
                       + "on duplicate key update "
                       + "KEDDT = cast('" + cmn.DrKD8440.KEdDt + "' as datetime), "
                       + "KEDTM = '" + cmn.DrKD8440.KEdTm + "', "
                       + "KODRQTY = '" + cmn.DrKD8440.KOdrQty + "', "
                       + "KMCCD = '" + cmn.DrKD8440.KMcCd + "' "
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
        //               + "ODCD like '%" + cmn.KD8440.OdCd + "%' and "
        //               + "WKGRCD like '%" + cmn.KD8440.WkGrCd + "%' and "
        //               + "HMCD like '%" + cmn.KD8440.HmCd + "%' and "
        //               + "VALDTF between to_timestamp('" + cmn.KD8440.ValDtFF + "') and "
        //               + "to_timestamp('" + cmn.KD8440.ValDtFT + "') and "
        //               + "WKSEQ like '%" + cmn.KD8440.WkSeq + "%' "
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
        public bool GetMpOrderSummaryInfo(ref DataTable mpDt, DateTime firstDayOfMonth)
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
                    + ", concat('',sum(case when ODRSTS = '2' then 1 else 0 end)) \"MP2確定件数\" "
                    + ", concat('',sum(case when ODRSTS = '3' then 1 else 0 end)) \"MP3着手件数\" "
                    + ", concat('',sum(case when ODRSTS = '4' then 1 else 0 end)) \"MP4完了件数\" "
                    + ", concat('',sum(case when ODRSTS = '9' then 1 else 0 end)) \"MP9取消件数\" "
                    + ", concat('',sum(case when ODRSTS in ('2','3','4','9') then 1 else 0 end)) \"MP取込件数\" "
                    + ", concat('',sum(case when ODRSTS = '2' then ODRQTY else 0 end)) \"MP2確定本数\" "
                    + ", concat('',sum(case when ODRSTS = '3' then ODRQTY-JIQTY else 0 end)) \"MP3着手本数\" "
                    + ", concat('',sum(case when ODRSTS = '4' then ODRQTY else 0 end)) \"MP4完了本数\" "
                    + ", concat('',sum(case when ODRSTS = '9' then ODRQTY else 0 end)) \"MP9取消本数\" "
                    + ", concat('',sum(case when ODRSTS in ('2','3','4','9') then ODRQTY else 0 end)) \"MP取込本数\" "
                    + ", concat('',sum(case when MPCARDDT is not NULL then 1 else 0 end)) \"MP印刷件数\" "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                    + "WHERE "
                    + "ODCD like '6060%' "
                    + "and EDDT between "
                    + $"adddate(date_format(str_to_date('{yyyyMMdd}', '%Y/%m/%d'), '%Y-%m-01'), interval -7 day) "
                    + $"and adddate(last_day(str_to_date('{yyyyMMdd}', '%Y/%m/%d')), interval 14 day) "
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
        /// 注文情報データ取得
        /// </summary>
        /// <param name="mpOrderDt">注文情報データ</param>
        /// <param name="eddt">検査対象月</param>
        /// <returns>注文情報データ</returns>
        public bool GetMpOrder(ref DataTable mpOrderDt, string eddt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "SELECT * "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                    + "WHERE "
                    + "ODCD like '6060%' "
                    + $"and EDDT = '{eddt} '"
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        using (DataTable myDt = new DataTable())
                        {
                            // 結果取得
                            myDa.Fill(myDt);
                            mpOrderDt = myDt;
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
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return ret;
        }

        /// <summary>
        /// 注文情報データ取得
        /// </summary>
        /// <param name="mpOrderDt">注文情報データ</param>
        /// <param name="select">検索条件WHERE文から指定</param>
        /// <param name="where">検索条件WHERE文から指定</param>
        /// <returns>注文情報データ</returns>
        public bool FindMpOrder(ref DataTable mpOrderDt, string select, string where)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "SELECT " + select + " "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + ", "
                    + "(SELECT HMCD as HMKEY, MATERIALCD, KTKEY FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + ") " + Common.TABLE_ID_KM8430 + " "
                    + "WHERE "
                    + "HMCD=HMKEY and "
                    + where
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(mpOrderDt);
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
        /// 仕掛り在庫データ取得
        /// </summary>
        /// <param name="mpZaikoDt">仕掛り在庫データ</param>
        /// <returns>仕掛り在庫データ</returns>
        public bool GetMpZaiko(ref DataTable mpZaikoDt, string mcgcds)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "SELECT * "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8460 + " "
                    + "WHERE "
                    + $"MCGCD in ({mcgcds})"
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(mpZaikoDt);
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
        /// <param name="exceptDt">EMとの差分の登録すべきデータテーブル</param>
        /// <param name="zaikoDt">仕掛り在庫（手配登録時に消込）</param>
        /// <returns>挿入件数、失敗時-1</returns>
        public int ImportMpOrder(ref DataTable exceptDt, ref DataTable codeDt ,ref DataTable zaikoDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int insCount = 0;
            MySqlConnection mpCnn = null;
            MySqlTransaction transaction = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                // トランザクション開始
                transaction = mpCnn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mpCnn;

                string sql = string.Empty;
                foreach (DataRow r in exceptDt.Rows)
                {
                    string odrno = r["ODRNO"].ToString();
                    string hmcd = r["HMCD"].ToString();

                    // KM8430:コード票マスタの品番抽出
                    DataRow[] cr = codeDt.Select($"HMCD='{hmcd}'");
                    if (cr.Length == 0)
                    {
                        System.Windows.Forms.MessageBox.Show($"EM手配中の{hmcd}がコード票マスタに存在しません。\nマスタ登録後再度実行してください");
                        Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name + ":" + hmcd);
                        continue;
                    }

                    // ODRNOが他の手配日付に存在したら削除しておく（手配日付がコロコロ変えられる対応）
                    // KD8450:切削オーダーの削除
                    cmd.CommandText = DeleteDuplicateOrderDetailSql(odrno);
                    cmd.ExecuteNonQuery();
                    // KD8430:切削手配ファイルの削除
                    cmd.CommandText = DeleteDuplicateOrderSql(odrno);
                    cmd.ExecuteNonQuery();

                    // KD8430:切削手配ファイルの登録
                    sql = ImportMpOrderSql(r);
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    // KD8450:切削オーダーの登録（工程数分をループ）
                    int ktsu = Convert.ToInt32(cr[0]["KTSU"].ToString());
                    for (int kt = 1; kt <= ktsu; kt++)
                    {
                        string mcgcd = cr[0][$"KT{kt}MCGCD"].ToString();
                        string mccd = cr[0][$"KT{kt}MCCD"].ToString();
                        // KD8460:切削在庫ファイルの取得
                        DataRow[] zr = zaikoDt.Select(
                            $"HMCD='{hmcd}' and MCGCD='{mcgcd}' and MCCD='{mccd}' " +
                            "and ZAIQTY>0");
                        int odrqty = Convert.ToInt32(r["ODRQTY"].ToString());
                        int odrjiq = Convert.ToInt32(r["JIQTY"].ToString());
                        int zaiko = (zr.Length == 0) ? 0 :
                            Convert.ToInt32(zr[0]["ZAIQTY"].ToString());
                        // 実績数とステータス算出
                        int jiqty = ((odrqty - odrjiq) <= zaiko) ? (odrqty - odrjiq) : zaiko;
                        string odrsts = (jiqty == 0) ? "2" : (odrqty == jiqty) ? "4" : "3";
                        // KD8450:切削オーダーファイルの登録（各設備毎に分解）
                        sql = DivideMpOrderSql(odrno, jiqty, kt, mcgcd, mccd, odrsts);
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        // KD8460:在庫ファイル仕掛り在庫数を減算
                        if (jiqty > 0)
                        {
                            sql = UpdateZaikoSql(hmcd, mcgcd, mccd, jiqty);
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    insCount++;
                }

                // トランザクション終了
                transaction.Commit();
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
                insCount = -1;
            }
            // 接続を閉じる
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return insCount;
        }

        private string DeleteDuplicateOrderSql(string odrno)
        {
            string sql = "DELETE FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                    + $"WHERE ODRNO = '{odrno}'";
            return sql;
        }

        private string DeleteDuplicateOrderDetailSql(string odrno)
        {
            string sql = "DELETE FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8450 + " "
                    + $"WHERE ODRNO = '{odrno}'";
            return sql;
        }

        /// <summary>
        /// 在庫ファイル仕掛り在庫から実績数を引き落とす処理
        /// </summary>
        /// <param name="odrno">手配番号</param>
        /// <returns>SQL 構文</returns>
        public string UpdateZaikoSql(string hmcd, string mcgcd, string mccd, int jiqty)
        {
            string sql = "update "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8460 + " "
                + "SET "
                + $"ZAIQTY = ZAIQTY - {jiqty},"
                + "OUTDT = now(),"
                + $"MPUPDTID = '{cmn.IkM0010.TanCd}' "
                + "WHERE "
                + $"HMCD = '{hmcd}' and "
                + $"MCGCD = '{mcgcd}' and "
                + $"MCCD = '{mccd}'"
            ;
            return sql;
        }


        /// <summary>
        /// 切削オーダーファイルインサート用SQL文の作成
        /// </summary>
        /// <param name="odrno">手配番号</param>
        /// <returns>SQL 構文</returns>
        public string DivideMpOrderSql(string odrno, int jiqty, int kt, string mcgcd, string mccd, string odrsts)
        {
            string sql = "insert into "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8450 + " "
                + "("
                + "ODRNO,"
                + "MPSEQ,"
                + "MCGCD,"
                + "MCCD,"
                + "HMCD,"
                + "EDDT,"
                + "ODRQTY,"
                + "JIQTY,"
                + "ODRSTS,"
                + "MPINSTID,"
                + "MPUPDTID"
                + ") "
                +  "select "
                +  "a.ODRNO,"
                +  $"{kt} as MPSEQ,"
                +  $"'{mcgcd}' as MCGCD,"
                +  $"'{mccd}' as MCCD,"
                + "a.HMCD,"
                + "a.EDDT,"
                + "a.ODRQTY,"
                + $"a.JIQTY+{jiqty},"
                + $"'{odrsts}',"
                + $"'{cmn.IkM0010.TanCd}' as MPINSTDT,"
                + $"'{cmn.IkM0010.TanCd}' as MPUPDTDT "
                +  "from "
                +   cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " a "
                +  "where "
                + $"ODRNO='{odrno}' "
                ;
            return sql;
        }


        /// <summary>
        /// 切削オーダーファイルインサート用SQL文の作成
        /// </summary>
        /// <param name="odrno">手配番号</param>
        /// <returns>SQL 構文</returns>
        public string DivideMpOrderSql_Backup(string odrno)
        {
            string sql = "insert into "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8450 + " "
                + "("
                + "ODRNO,"
                + "MPSEQ,"
                + "MCGCD,"
                + "MCCD,"
                + "HMCD,"
                + "EDDT,"
                + "ODRQTY,"
                + "JIQTY,"
                + "ODRSTS,"
                + "MPINSTID,"
                + "MPUPDTID"
                + ") "
                + "select "
                + "a.ODRNO,"
                + "1 as MPSEQ,"
                + "b.KT1MCGCD as MCGCD,"
                + "b.KT1MCCD as MCCD,"
                + "a.HMCD,"
                + "a.EDDT,"
                + "a.ODRQTY,"
                + "a.JIQTY,"
                + "a.ODRSTS,"
                + $"{cmn.IkM0010.TanCd} as MPINSTDT,"
                + $"{cmn.IkM0010.TanCd} as MPUPDTDT "
                + "from "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " a, "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " b "
                + "where "
                + $"a.HMCD = b.HMCD and a.ODRNO='{odrno}' and "
                + "b.KT1MCCD is not NULL "
                + "union "
                + "select "
                + "a.ODRNO,"
                + "2 as MPSEQ,"
                + "b.KT2MCGCD as MCGCD,"
                + "b.KT2MCCD as MCCD,"
                + "a.HMCD,"
                + "a.EDDT,"
                + "a.ODRQTY,"
                + "a.JIQTY,"
                + "a.ODRSTS,"
                + $"{cmn.IkM0010.TanCd} as MPINSTDT,"
                + $"{cmn.IkM0010.TanCd} as MPUPDTDT "
                + "from "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " a, "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " b "
                + "where "
                + $"a.HMCD = b.HMCD and a.ODRNO='{odrno}' and "
                + "b.KT2MCCD is not NULL "
                + "union "
                + "select "
                + "a.ODRNO,"
                + "3 as MPSEQ,"
                + "b.KT3MCGCD as MCGCD,"
                + "b.KT3MCCD as MCCD,"
                + "a.HMCD,"
                + "a.EDDT,"
                + "a.ODRQTY,"
                + "a.JIQTY,"
                + "a.ODRSTS,"
                + $"{cmn.IkM0010.TanCd} as MPINSTDT,"
                + $"{cmn.IkM0010.TanCd} as MPUPDTDT "
                + "from "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " a, "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " b "
                + "where "
                + $"a.HMCD = b.HMCD and a.ODRNO='{odrno}' and "
                + "b.KT3MCCD is not NULL "
                + "union "
                + "select "
                + "a.ODRNO,"
                + "4 as MPSEQ,"
                + "b.KT4MCGCD as MCGCD,"
                + "b.KT4MCCD as MCCD,"
                + "a.HMCD,"
                + "a.EDDT,"
                + "a.ODRQTY,"
                + "a.JIQTY,"
                + "a.ODRSTS,"
                + $"{cmn.IkM0010.TanCd} as MPINSTDT,"
                + $"{cmn.IkM0010.TanCd} as MPUPDTDT "
                + "from "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " a, "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " b "
                + "where "
                + $"a.HMCD = b.HMCD and a.ODRNO='{odrno}' and "
                + "b.KT4MCCD is not NULL "
                + "union "
                + "select "
                + "a.ODRNO,"
                + "5 as MPSEQ,"
                + "b.KT5MCGCD as MCGCD,"
                + "b.KT5MCCD as MCCD,"
                + "a.HMCD,"
                + "a.EDDT,"
                + "a.ODRQTY,"
                + "a.JIQTY,"
                + "a.ODRSTS,"
                + $"{cmn.IkM0010.TanCd} as MPINSTDT,"
                + $"{cmn.IkM0010.TanCd} as MPUPDTDT "
                + "from "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " a, "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " b "
                + "where "
                + $"a.HMCD = b.HMCD and a.ODRNO='{odrno}' and "
                + "b.KT5MCCD is not NULL "
                ;
            return sql;
        }


        /// <summary>
        /// 切削設備情報登録/更新 SQL 構文編集 (KM8420 切削設備マスター) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        public string ImportMpOrderSql(DataRow r)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 登録形式により抽出対象が異なる
            // MySql の DATE 型列に値を代入するときは、その列が時刻を持っているか否かに関わらず、必ず to_datetime('<代入元>') メソッドで変換してから代入する必要がある
            // 代入元が定数か変数化に関わらずシングル クォーテーション括りは必須
            // 代入元に書式 'YYYY/MM/DD HH24:MI:SS' 等の記述は不要、MySql が適切に合わせ込んで登録してくれる
            // この変換を怠ると「ORA-01861: リテラルが書式文字列と一致しません」の例外が発生する
            string sql = "insert into "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                + "("
                + "ODRNO, "
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
                + "MPINSTID, "
                + "MPUPDTID "
                + ") "
                + "values "
                + "("
                + "'" + r["ODRNO"].ToString() + "',"
                +       r["KTSEQ"] + ","
                + "'" + r["HMCD"].ToString() + "',"
                + "'" + r["KTCD"].ToString() + "',"
                +       r["ODRQTY"] + ","
                + "'" + r["ODCD"].ToString() + "',"
                + "'" + r["NEXTODCD"].ToString() + "',"
                +       r["LTTIME"] + ","
                + "'" + r["STDT"] + "',"
                + "'" + r["STTIM"].ToString() + "',"
                + "'" + r["EDDT"] + "',"
                + "'" + r["EDTIM"].ToString() + "',"
                + "'" + r["ODRSTS"].ToString() + "',"
                + "'" + r["QRCD"].ToString() + "',"
                +       r["JIQTY"] + ","
                + "'" + r["DENPYOKBN"].ToString() + "',"
                + (r["DENPYODT"].ToString() == "" ? "null," : "'" + r["DENPYODT"] + "',")
                + "'" + r["NOTE"].ToString() + "',"
                + "'" + r["WKNOTE"].ToString() + "',"
                + "'" + r["WKCOMMENT"].ToString() + "',"
                + "'" + r["DATAKBN"].ToString() + "',"
                + "'" + r["INSTID"].ToString() + "',"
                + "'" + r["INSTDT"] + "',"
                + "'" + r["UPDTID"].ToString() + "',"
                + "'" + r["UPDTDT"] + "',"
                + "'" + r["UKCD"].ToString() + "',"
                + "'" + r["NAIGAIKBN"].ToString() + "',"
                + "'" + r["RETKTCD"].ToString() + "',"
                + "'" + cmn.IkM0010.TanCd + "',"
                + "'" + cmn.IkM0010.TanCd + "'"
                + ")"
                ;
            return sql;
        }

        /// <summary>
        /// 注文情報削除
        /// </summary>
        /// <param name="exceptDt">削除対象のデータテーブル</param>
        /// <returns>削除件数、失敗時-1</returns>
        public int DeleteMpOrder(ref DataTable exceptDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int delCount = 0;
            MySqlConnection mpCnn = null;
            MySqlTransaction transaction = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                // トランザクション開始
                transaction = mpCnn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mpCnn;

                foreach (DataRow r in exceptDt.Rows)
                {
                    // KD8430:切削手配ファイルの削除
                    string sql1 = "delete from "
                        + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                        + "where "
                        + "ODRNO="
                        + "'" + r["ODRNO"].ToString() + "'"
                    ;
                    cmd.CommandText = sql1;
                    cmd.ExecuteNonQuery();

                    // KD8450:切削オーダーファイルの削除
                    string sql2 = "delete from "
                        + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8450 + " "
                        + "where "
                        + "ODRNO="
                        + "'" + r["ODRNO"].ToString() + "'"
                    ;
                    cmd.CommandText = sql2;
                    cmd.ExecuteNonQuery();

                    delCount++;
                }

                // トランザクション終了
                transaction.Commit();
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
                delCount = -1;
            }
            // 接続を閉じる
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return delCount;
        }

        /// <summary>
        /// 手配日程ファイル取込
        /// </summary>
        /// <param name="exceptDt">取込対象のデータテーブル</param>
        /// <returns>可否</returns>
        public bool ImportMpPlan(ref DataTable exceptDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            MySqlConnection mpCnn = null;
            MySqlTransaction transaction = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                // トランザクション開始
                transaction = mpCnn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mpCnn;

                string sql = string.Empty;

                // KD8440:切削手配日程ファイルの削除
                sql = "delete from "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                var insCount = 0;
                foreach (DataRow r in exceptDt.Rows)
                {
                    // KD8440:切削手配日程ファイルの挿入
                    sql = ImportMpPlanSql(r);
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
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return ret;
        }

        /// <summary>
        /// SQL 構文編集 (KM8440 切削手配日程ファイル) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        public string ImportMpPlanSql(DataRow r)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            string sql = "insert into "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " "
                + "("
                + "ODCD, "
                + "PLNNO, "
                + "ODRNO, "
                + "KTSEQ, "
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
                + "WEEKEDDT "
                + ") "
                + "values "
                + "("
                + "'" + r["ODCD"].ToString() + "',"
                + (r["PLNNO"].ToString() == "" ? "null," : "'" + r["PLNNO"].ToString() + "',")
                + (r["ODRNO"].ToString() == "" ? "null," : "'" + r["ODRNO"].ToString() + "',")
                + r["KTSEQ"] + ","
                + (r["HMCD"].ToString() == "" ? "null," : "'" + r["HMCD"].ToString() + "',")
                + (r["KTCD"].ToString() == "" ? "null," : "'" + r["KTCD"].ToString() + "',")
                + (r["UKCD"].ToString() == "" ? "null," : "'" + r["UKCD"].ToString() + "',")
                + (r["DATAKBN"].ToString() == "" ? "null," : "'" + r["DATAKBN"].ToString() + "',")
                + (r["HMSTKBN"].ToString() == "" ? "null," : "'" + r["HMSTKBN"].ToString() + "',")
                + r["ODRQTY"] + ","
                +(r["TRIALQTY"].ToString() == "" ? "null," : r["TRIALQTY"] + ",")
                + "'" + r["STDT"] + "',"
                + "'" + r["STTM"].ToString() + "',"
                + "'" + r["EDDT"] + "',"
                + "'" + r["EDTM"].ToString() + "',"
                + (r["KJUNO"].ToString() == "" ? "null," : "'" + r["KJUNO"] + "',")
                + (r["REASONKBN"].ToString() == "" ? "null," : "'" + r["REASONKBN"] + "',")
                + (r["NOTE"].ToString() == "" ? "null," : "'" + r["NOTE"] + "',")
                + (r["TKCD"].ToString() == "" ? "null," : "'" + r["TKCD"] + "',")
                + (r["TKCTLNO"].ToString() == "" ? "null," : "'" + r["TKCTLNO"] + "',")
                + (r["TKHMCD"].ToString() == "" ? "null," : "'" + r["TKHMCD"] + "',")
                + (r["RQMNNO"].ToString() == "" ? "null," : "'" + r["RQMNNO"] + "',")
                + (r["RQSEQNO"].ToString() == "" ? "null," : r["RQSEQNO"] + ",")
                + (r["RQBNO"].ToString() == "" ? "null," : r["RQBNO"] + ",")
                + (r["NEXTKTCD"].ToString() == "" ? "null," : "'" + r["NEXTKTCD"] + "',")
                + (r["NEXTODCD"].ToString() == "" ? "null," : "'" + r["NEXTODCD"] + "',")
                + (r["DVRQNO"].ToString() == "" ? "null," : "'" + r["DVRQNO"] + "',")
                + (r["ODRSTS"].ToString() == "" ? "null," : "'" + r["ODRSTS"] + "',")
                + (r["SEPDAY"].ToString() == "" ? "null," : r["SEPDAY"] + ",")
                + (r["WKNOTE"].ToString() == "" ? "null," : "'" + r["WKNOTE"] + "',")
                + (r["WKCOMMENT"].ToString() == "" ? "null," : "'" + r["WKCOMMENT"] + "',")
                + (r["KTKBN"].ToString() == "" ? "null," : "'" + r["KTKBN"] + "',")
                + (r["KKTKKBN"].ToString() == "" ? "null," : "'" + r["KKTKKBN"] + "',")
                + (r["SODKBN"].ToString() == "" ? "null," : "'" + r["SODKBN"] + "',")
                + (r["NAIGAIKBN"].ToString() == "" ? "null," : "'" + r["NAIGAIKBN"] + "',")
                + "'" + r["INSTID"].ToString() + "',"
                + "'" + r["INSTDT"] + "',"
                + "'" + r["UPDTID"].ToString() + "',"
                + "'" + r["UPDTDT"] + "',"
                + r["JIQTY"] + ","
                + r["SEQ"] + ","
                + "'" + r["WEEKEDDT"] + "'"
                + ")"
                ;
            return sql;
        }





        // ここからスマート棚コン関連

        /// <summary>
        /// 対象月の出庫予定データ出力済みの一覧を取得
        /// </summary>
        /// <param name="mpDt">出庫予定データ出力済み一覧</param>
        /// <param name="targerMonth">対象月</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int GetShipmentPlanSummaryInfo(ref DataTable mpDt, DateTime targerMonth)
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
                    + ", COUNT(MPTANADT) as '発行済件数' "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
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
                    + "DATE_FORMAT(EDDT, '%Y%m%d') AS '出庫予定日' "
                    + ", '' as '伝票番号' "
                    + ", '01' as '出庫区分' "
                    + ", 'C000000001' as '出荷先コード' " // （今の所、仮のコード 本社:C000000001, EWU:C000000002, 協力会社:C000000010)
                    + ", '' as '出荷先名' "
                    + ", '' as '荷主コード' " // （今の所、仮のコード 本社:C000000001, EWU:C000000002, 協力会社:C000000010)
                    + ", '' as '荷主名' "
                    + ", a.HMCD as '商品コード' "
                    + ", f_han2zen(IFNULL(b.HMNM, '')) as '商品名' "
                    + ", '' as '管理項目１' "
                    + ", '' as '管理項目２' "
                    + ", '' as '管理項目３' "
                    + ", '' as '管理項目４' "
                    + ", LPAD(ODRQTY, 6, '0') as '出庫予定数量' "
                    + ", '' as '備考' "
                    + ", '' "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " a, "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " b "
                    + "WHERE "
                    + "a.HMCD = b.HMCD "
                    + "and ODRSTS <> '9' "
                    + "and ODCD like '6060%' "
                    + $"and EDDT = '{eddt}' "
                    + "and MPTANADT is null " // 棚コンデータ作成日
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
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                + "SET "
                + "MPTANADT = now(), "
                + "UPDTID = '" + cmn.DrCommon.UpdtID + "' "
                + "WHERE "
                + "ODRSTS<>'9' and "
                + "ODCD like '6060%' and "
                + "EDDT = '" + planDay.ToString() + "' and "
                + "MPTANADT is NULL " // 棚コンデータ作成日
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
                                Debug.WriteLine(Common.TABLE_ID_KD8430 + " table data update succeed and commited.");
                            }
                            ret = res;
                        }
                        catch (Exception e)
                        {
                            txn.Rollback();
                            Debug.WriteLine(Common.TABLE_ID_KD8430 + " table no data inserted/updated.");

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

        // ここから製造指示カード発行関連

        /// <summary>
        /// 製造指示カード発行済み一覧の取得
        /// </summary>
        /// <param name="mpDt">カード発行済み日付一覧</param>
        /// <param name="targerMonth">検査対象月</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int GetCardPrintSummaryInfo(ref DataTable mpDt, DateTime targerMonth)
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
                    + ", COUNT(MPCARDDT) as '発行済件数' "
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

        /// <summary>
        /// 製造指示カードに印刷するデータの取得
        /// </summary>
        /// <param name="dt">製造指示カードデータ</param>
        /// <param name="eddtFrom">開始完了予定日（月曜日）</param>
        /// <param name="eddtTo">開始完了予定日（月曜日）</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int GetOrderCardPrintInfo(ref DateTime eddtFrom, ref DateTime eddtTo, ref DataTable mpDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            MySqlConnection mpCnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                // 並び替え順のパターンごとにデータテーブルを取得
                string sql = GetOrderCardPrintInfoSQL(ref eddtFrom, ref eddtTo);

                // 1.品番番で印刷するパターン（SW）
                string sqlSW = sql +
                    "and b.KT1MCGCD = 'SW' " +
                    "order by a.HMCD, a.EDDT"
                ;
                using (DataTable patternSW = new DataTable())
                {
                    using (MySqlCommand myCmd = new MySqlCommand(sqlSW, mpCnn))
                    {
                        using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                        {
                            Debug.WriteLine("Read from DataTable:");
                            // 結果取得
                            myDa.Fill(patternSW);
                            mpDt.Merge(patternSW);
                        }
                    }
                }

                // 2.納期の昇順で印刷するパターン（CN）
                string sqlCN = sql + 
                    "and b.KT1MCGCD = 'CN' " + 
                    "order by a.EDDT, a.HMCD"
                ;
                using (DataTable patternCN = new DataTable())
                {
                    using (MySqlCommand myCmd = new MySqlCommand(sqlCN, mpCnn))
                    {
                        using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                        {
                            Debug.WriteLine("Read from DataTable:");
                            // 結果取得
                            myDa.Fill(patternCN);
                            mpDt.Merge(patternCN);
                        }
                    }
                }

                // 3.設備番号順で印刷するパターン
                string sqlOT = sql +
                    "and b.KT1MCGCD<>'SW' and b.KT1MCGCD <> 'CN' " +
                    "order by b.KT1MCGCD, a.HMCD, a.EDDT"
                ;
                using (DataTable patternOT = new DataTable())
                {
                    using (MySqlCommand myCmd = new MySqlCommand(sqlOT, mpCnn))
                    {
                        using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                        {
                            Debug.WriteLine("Read from DataTable:");
                            // 結果取得
                            myDa.Fill(patternOT);
                            mpDt.Merge(patternOT);
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
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return ret;
        }

        /// <summary>
        /// 製造指示カードに印刷するデータの取得（個別明細指定）
        /// </summary>
        /// <param name="targetDt">データグリッドビューで指定した印刷対象</param>
        /// <param name="cardDt">製造指示カードデータ</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int GetOrderCardPrintInfo(DataTable targetDt, ref DataTable cardDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            MySqlConnection mpCnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = GetOrderCardPrintInfoSQL(targetDt);

                using (DataTable patternSW = new DataTable())
                {
                    using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                    {
                        using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                        {
                            Debug.WriteLine("Read from DataTable:");
                            // 結果取得
                            myDa.Fill(cardDt);
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
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return ret;
        }


        private string GetOrderCardPrintInfoSQL(ref DateTime eddtFrom, ref DateTime eddtTo)
        {
            string sql = GetOrderCardPrintInfoBaseSQL()
                + $"and a.EDDT between '{eddtFrom}' and '{eddtTo}' "
                + "and MPCARDDT is null " // 製造指示カード発行日
            ;
            return sql;
        }

        private string GetOrderCardPrintInfoSQL(DataTable targetDt)
        {
            // 手配Noを設定
            string addWhere = "and a.ODRNO in (";
            foreach (DataRow dr in targetDt.Rows)
            {
                addWhere += "'" + dr["手配No"].ToString() + "',"; // DataGridViewのタイトル名で来るので注意！
            }
            addWhere += "'x')";
            string sql = GetOrderCardPrintInfoBaseSQL()
                + addWhere
            ;
            return sql;
        }

        private string GetOrderCardPrintInfoBaseSQL()
        {
            string sql = "SELECT "
                + "a.ODRNO "
                + ",a.EDDT "
                + ",a.ODRQTY "
                + ",a.HMCD "
                + ",b.HMNM "
                + ",b.MATESIZE "
                + ",b.LENGTH "
                + ",b.BOXQTY "
                + ",b.BOXCD "
                + ",b.PARTNER "
                + ",b.NOTE"
                + ",b.STORE"
                + ",b.KT1MCGCD "
                + ",b.KT1MCCD "
                + ",b.KT1CT "
                + ",b.KT2MCGCD "
                + ",b.KT2MCCD "
                + ",b.KT2CT "
                + ",b.KT3MCGCD "
                + ",b.KT3MCCD "
                + ",b.KT3CT "
                + ",b.KT4MCGCD "
                + ",b.KT4MCCD "
                + ",b.KT4CT "
                + ",b.KT5MCGCD "
                + ",b.KT5MCCD "
                + ",b.KT5CT "
                + ",b.KT6MCGCD "
                + ",b.KT6MCCD "
                + ",b.KT6CT "
                + "FROM "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " a, "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " b "
                + "WHERE "
                + "a.HMCD = b.HMCD "
                + "and a.ODRSTS <> '9' "
                + "and a.ODCD like '6060%' "
            ;
            return sql;
        }


        /// <summary>
        /// 製造指示カード発行済みに更新
        /// </summary>
        /// <param name="eddtFrom">手配開始日</param>
        /// <param name="eddtTo">手配終了日</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int UpdatePrintOrderCardDay(ref DateTime eddtFrom, ref DateTime eddtTo)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;

            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                // 製造指示カード発行済みに更新 SQL
                string sql =
                    "UPDATE "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                    + "SET "
                    + "MPCARDDT = now(), "
                    + "UPDTID = '" + cmn.DrCommon.UpdtID + "' "
                    + "WHERE "
                    + "ODRSTS<>'9' and "
                    + "ODCD like '6060%' and "
                    + "MPCARDDT is NULL and " // 製造指示カード発行日
                    + $"EDDT between '{eddtFrom}' and '{eddtTo}'"
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
                                Debug.WriteLine(Common.TABLE_ID_KD8430 + " table data update succeed and commited.");
                            }
                            ret = res;
                        }
                        catch (Exception e)
                        {
                            txn.Rollback();
                            Debug.WriteLine(Common.TABLE_ID_KD8430 + " table no data inserted/updated.");

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
        /// 製造指示カード発行済みに更新（個別明細指定）
        /// </summary>
        /// <param name="targetDt">手配日</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int UpdatePrintOrderCardDay(ref DataTable targetDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;

            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                // 手配Noを設定
                string addWhere = "ODRNO in (";
                foreach (DataRow dr in targetDt.Rows)
                {
                    addWhere += "'" + dr["ODRNO"].ToString() + "',"; // ここに来たときはDataGridViewのタイトル名では無いので注意！
                }
                addWhere += "'x')";

                // 製造指示カード発行済みに更新 SQL
                string sql =
                    "UPDATE "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                    + "SET "
                    + "MPCARDDT = now(), "
                    + "UPDTID = '" + cmn.DrCommon.UpdtID + "' "
                    + "WHERE "
                    + "ODRSTS<>'9' and "
                    + "ODCD like '6060%' and "
                    + addWhere
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
                                Debug.WriteLine(Common.TABLE_ID_KD8430 + " table data update succeed and commited.");
                            }
                            ret = res;
                        }
                        catch (Exception e)
                        {
                            txn.Rollback();
                            Debug.WriteLine(Common.TABLE_ID_KD8430 + " table no data inserted/updated.");

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
        /// 内示サマリー情報取得
        /// </summary>
        /// <param name="mpDt">内示情報データ</param>
        /// <param name="firstDayOfMonth">検査対象月</param>
        /// <returns>注文情報データ</returns>
        public bool GetMpPlanSummaryInfo(ref DataTable mpDt, DateTime firstDayOfMonth)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string yyyyMMdd = firstDayOfMonth.AddMonths(2).ToString("yyyy/MM/dd");
                string sql = "SELECT "
                    + "a.WEEKEDDT "
                    + ", a.HMCD"
                    + ", SUM(ODRQTY) \"MP本数\" "
                    + ", MAX(IFNULL(PLANCARDDT, CONVERT('1900/01/01', DATETIME))) \"内示カード出力日時\" "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " a "
                    + "LEFT OUTER JOIN "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8470 + " b "
                    + "ON "
                        + "b.HMCD = a.HMCD and "
                        + "b.WEEKEDDT = a.WEEKEDDT "
                    + "WHERE "
                    + "ODCD like '6060%' "
                    + $"and EDDT < convert('{yyyyMMdd}', date) "
                    + "GROUP BY a.WEEKEDDT, a.HMCD "
                    + "ORDER BY a.WEEKEDDT, a.HMCD "
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
        /// 内示情報データ取得
        /// </summary>
        /// <param name="mpPlanDt">注文情報データ</param>
        /// <param name="whereIn">検査対象月</param>
        /// <returns>注文情報データ</returns>
        public bool GetMpPlan(ref DataTable mpPlanDt, string whereIn)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "SELECT * "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " "
                    + "WHERE "
                    + "ODCD like '6060%' "
                    + $"and EDDT in {whereIn} "
                    + "GROUP BY EDDT "
                    + "ORDER BY EDDT "
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        using (DataTable myDt = new DataTable())
                        {
                            // 結果取得
                            myDa.Fill(myDt);
                            mpPlanDt = myDt;
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
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return ret;
        }

        /// <summary>
        /// 内示カード(SW工程)に印刷するデータの取得
        /// </summary>
        /// <param name="dt">内示カードデータ</param>
        /// <param name="weekeddt">週初完了予定日</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int GetPlanCardPrintInfo(DateTime weekeddt, ref DataTable mpDt)
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
                    + "a.EDDT "
                    + ",a.ODRQTY "
                    + ",a.HMCD "
                    + ",b.HMNM "
                    + ",b.MATESIZE "
                    + ",b.LENGTH "
                    + ",b.BOXQTY "
                    + ",b.PARTNER "
                    + ",b.MATERIALLEN "
                    + ",b.NOTE"
                    + ",b.KT1MCGCD "
                    + ",b.KT1MCCD "
                    + ",b.KT1CT "
                    + ",b.KT2MCGCD "
                    + ",b.KT2MCCD "
                    + ",b.KT2CT "
                    + ",b.KT3MCGCD "
                    + ",b.KT3MCCD "
                    + ",b.KT3CT "
                    + ",b.KT4MCGCD "
                    + ",b.KT4MCCD "
                    + ",b.KT4CT "
                    + ",b.KT5MCGCD "
                    + ",b.KT5MCCD "
                    + ",b.KT5CT "
                    + ",c.CUTTHICKNESS "
                    + ",c.SCRAPLEN "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " a, "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " b, "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420 + " c "
                    + "WHERE "
                    + "a.HMCD = b.HMCD "
                    + "and a.ODRSTS <> '9' "
                    + "and a.ODCD like '6060%' "
                    + $"and a.WEEKEDDT = '{weekeddt}' "
                    + "and b.KT1MCGCD = c.MCGCD "
                    + "and b.KT1MCCD = c.MCCD "
                    + "and b.KT1MCGCD = 'SW' "
                    + "ORDER BY "
                    + "c.MCSEQ "
                    + ", b.MATESIZE "
                    + ", a.HMCD "
                    + ", a.EDDT"
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
        /// 内示カード発行済み登録
        /// </summary>
        /// <param name="weekEddt">検査対象月</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int InsertPlanCard(DateTime weekEddt)
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
                "INSERT INTO "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8470 + " "
                + "("
                + "HMCD "
                + ", WEEKEDDT "
                + ", PLANQTY "
                + ", PLANCARDDT "
                + ", MPINSTID "
                + ", MPINSTDT "
                + ", MPUPDTID "
                + ", MPUPDTDT "
                + ") "
                + "SELECT "
                + "HMCD "
                + ", WEEKEDDT "
                + ", SUM(ODRQTY) "
                + ", now() "
                + ", '" + cmn.DrCommon.UpdtID + "' "
                + ", now() "
                + ", '" + cmn.DrCommon.UpdtID + "' "
                + ", now() "
                + "FROM "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " "
                + "WHERE "
                + $"WEEKEDDT = '{weekEddt.ToString()}' "
                + "GROUP BY "
                + "HMCD "
                + "ORDER BY "
                + "HMCD "
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
                                Debug.WriteLine(Common.TABLE_ID_KD8430 + " table data update succeed and commited.");
                            }
                            ret = res;
                        }
                        catch (Exception e)
                        {
                            txn.Rollback();
                            Debug.WriteLine(Common.TABLE_ID_KD8430 + " table no data inserted/updated.");

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





        // ********** 設備マスタ関連 **********
        /// <summary>
        /// 設備マスタ取得
        /// </summary>
        /// <param name="equipMstDt">設備マスタ</param>
        /// <returns>可否</returns>
        public bool GetEquipMstDt(ref DataTable equipMstDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "SELECT * "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420 + " "
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(equipMstDt);
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
        /// KM8420: 設備マスタ更新
        /// </summary>
        /// <param name="dgvDt">DataGridView</param>
        /// <returns>注文情報データ</returns>
        public bool UpdateEquipMst(ref DataTable dgvDt)
        {

            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                DataTable dtUpdate = new DataTable();
                var countInsert = 0;
                var countUpdate = 0;
                var countDelete = 0;
                var countModify = 0;

                using (MySqlTransaction txn = mpCnn.BeginTransaction())
                {
                    using (var adapter = new MySqlDataAdapter())
                    {
                        string sql = "SELECT * FROM "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8420;
                        adapter.SelectCommand = new MySqlCommand(sql, mpCnn);
                        using (var buider = new MySqlCommandBuilder(adapter))
                        {
                            // 全件読み込み
                            adapter.Fill(dtUpdate);

                            // DataGridView上に新規変更削除があるかチェック
                            foreach (DataRow dgv in dgvDt.Rows)
                            {
                                if (dgv.RowState == DataRowState.Added)
                                {
                                    dgv["INSTID"] = cmn.Ui.UserId;
                                    dgv["INSTDT"] = DateTime.Now.ToString();
                                    dgv["UPDTID"] = cmn.Ui.UserId;
                                    dgv["UPDTDT"] = DateTime.Now.ToString();
                                    dtUpdate.ImportRow(dgv);
                                    countInsert++;
                                }
                                else if (dgv.RowState == DataRowState.Deleted)
                                {
                                    int mcgseq = Convert.ToInt32(dgv["MCGSEQ", DataRowVersion.Original].ToString());
                                    string mcgcd = dgv["MCGCD", DataRowVersion.Original].ToString();
                                    int mcseq = Convert.ToInt32(dgv["MCSEQ", DataRowVersion.Original].ToString());
                                    string mccd = dgv["MCCD", DataRowVersion.Original].ToString();
                                    DataRow[] r = dtUpdate.Select($"MCGSEQ={mcgseq} and MCGCD='{mcgcd}' and MCSEQ={mcseq} and MCCD='{mccd}'");
                                    if (r.Length == 1)
                                    {
                                        r[0].Delete();
                                        countDelete++;
                                    }
                                }
                                else if (dgv.RowState == DataRowState.Modified)
                                {
                                    int mcgseqOrg = Convert.ToInt32(dgv["MCGSEQ", DataRowVersion.Original].ToString());
                                    string mcgcdOrg = dgv["MCGCD", DataRowVersion.Original].ToString();
                                    int mcseqOrg = Convert.ToInt32(dgv["MCSEQ", DataRowVersion.Original].ToString());
                                    string mccdOrg = dgv["MCCD", DataRowVersion.Original].ToString();
                                    int mcgseq = Convert.ToInt32(dgv["MCGSEQ", DataRowVersion.Current].ToString());
                                    string mcgcd = dgv["MCGCD", DataRowVersion.Current].ToString();
                                    int mcseq = Convert.ToInt32(dgv["MCSEQ", DataRowVersion.Current].ToString());
                                    string mccd = dgv["MCCD", DataRowVersion.Current].ToString();
                                    DataRow[] r = dtUpdate.Select($"MCGSEQ={mcgseq} and MCGCD='{mcgcd}' and MCSEQ={mcseq} and MCCD='{mccd}'");
                                    // 主キーに変更が無い更新の場合
                                    if (mcgseqOrg == mcgseq && mcgcdOrg == mcgcd && 
                                        mcseqOrg == mcseq && mccdOrg == mccd && r.Length == 1)
                                    { 
                                        int change = 0;
                                        for (int col = 0; col < dtUpdate.Columns.Count; col++)
                                        {
                                            // 変更あり
                                            if (r[0][col].ToString() != dgv[col].ToString())
                                            {
                                                r[0][col] = dgv[col];
                                                change++;
                                            }
                                        }
                                        if (change > 0)
                                        {
                                            r[0]["UPDTID"] = cmn.Ui.UserId;
                                            r[0]["UPDTDT"] = DateTime.Now.ToString();
                                            countUpdate++;
                                        }
                                    }
                                }
                            }
                            // 追加更新削除があればコマンドビルダーにて一括更新
                            if (countInsert + countUpdate + countDelete > 0)
                            {
                                try
                                {
                                    adapter.Update(dtUpdate);
                                }
                                catch (Exception ex)
                                {
                                    txn.Rollback();
                                    throw ex;
                                }
                                // 結果をデバッグ上にだけ表示
                                Debug.WriteLine("新規件数：" + String.Format("{0:#,0}", countInsert) + " 件");
                                Debug.WriteLine("更新件数：" + String.Format("{0:#,0}", countUpdate) + " 件");
                                Debug.WriteLine("削除件数：" + String.Format("{0:#,0}", countDelete) + " 件");
                            }
                            else
                            {
                                Debug.WriteLine("更新はありませんでした．".PadLeft(18));
                            }
                        }
                    }
                    // オプティミスティック コンカレンシー
                    // 主キーの更新
                    foreach (DataRow dgv in dgvDt.Rows)
                    {
                        if (dgv.RowState == DataRowState.Modified)
                        {
                            int mcgseqOrg = Convert.ToInt32(dgv["MCGSEQ", DataRowVersion.Original].ToString());
                            string mcgcdOrg = dgv["MCGCD", DataRowVersion.Original].ToString();
                            int mcseqOrg = Convert.ToInt32(dgv["MCSEQ", DataRowVersion.Original].ToString());
                            string mccdOrg = dgv["MCCD", DataRowVersion.Original].ToString();
                            int mcgseq = Convert.ToInt32(dgv["MCGSEQ", DataRowVersion.Current].ToString());
                            string mcgcd = dgv["MCGCD", DataRowVersion.Current].ToString();
                            int mcseq = Convert.ToInt32(dgv["MCSEQ", DataRowVersion.Current].ToString());
                            string mccd = dgv["MCCD", DataRowVersion.Current].ToString();
                            // 主キーが変更された場合
                            if (mcgseqOrg != mcgseq || mcgcdOrg != mcgcd ||
                                mcseqOrg != mcseq || mccdOrg != mccd)
                            {
                                string tannm1 = (dgv["TANNM1"].ToString() == "") ? "null" : $"'{dgv["TANNM1"].ToString()}'";
                                string tannm2 = (dgv["TANNM2"].ToString() == "") ? "null" : $"'{dgv["TANNM2"].ToString()}'";
                                string thickness = (dgv["CUTTHICKNESS"].ToString() == "") ? "null" : dgv["CUTTHICKNESS"].ToString();
                                string scrap = (dgv["SCRAPLEN"].ToString() == "") ? "null" : dgv["SCRAPLEN"].ToString();
                                string setupnm1 = (dgv["SETUPNM1"].ToString() == "") ? "null" : $"'{dgv["SETUPNM1"].ToString()}'";
                                string setupnm2 = (dgv["SETUPNM2"].ToString() == "") ? "null" : $"'{dgv["SETUPNM2"].ToString()}'";
                                string setupnm3 = (dgv["SETUPNM3"].ToString() == "") ? "null" : $"'{dgv["SETUPNM3"].ToString()}'";
                                string sql = "UPDATE km8420 set " +
                                    $"MCGSEQ={mcgseq}" +
                                    $",MCGCD='{mcgcd}'" +
                                    $",MCGNM='{dgv["MCGNM"].ToString()}'" +
                                    $",MCSEQ={mcseq}" +
                                    $",MCCD='{mccd}'" +
                                    $",MCNM='{dgv["MCNM"].ToString()}'" +
                                    $",TANNM1={tannm1}" +
                                    $",TANNM2={tannm2}" +
                                    $",ONTIME={Convert.ToInt32(dgv["ONTIME"].ToString())}" +
                                    $",FLG1='{dgv["FLG1"].ToString()}'" +
                                    $",FLG2='{dgv["FLG2"].ToString()}'" +
                                    $",CUTTHICKNESS={thickness}" + 
                                    $",SCRAPLEN={scrap}" +
                                    $",SETUPNM1={setupnm1}" +
                                    $",SETUPTM1={Convert.ToInt32(dgv["SETUPTM1"].ToString())}" +
                                    $",SETUPNM2={setupnm2}" +
                                    $",SETUPTM2={Convert.ToInt32(dgv["SETUPTM2"].ToString())}" +
                                    $",SETUPNM3={setupnm3}" +
                                    $",SETUPTM3={Convert.ToInt32(dgv["SETUPTM3"].ToString())}" +
                                    $",UPDTID='{cmn.Ui.UserId}'" +
                                    $",UPDTDT=NOW()" + " " +
                                    $"WHERE MCGSEQ={mcgseqOrg} and MCGCD='{mcgcdOrg}' and MCSEQ={mcseqOrg} and MCCD='{mccdOrg}'";
                                ;
                                MySqlCommand myCmd = new MySqlCommand(sql, mpCnn);
                                try
                                {
                                    int res = myCmd.ExecuteNonQuery();
                                    countModify++;
                                }
                                catch (Exception ex)
                                {
                                    txn.Rollback();
                                    throw ex;
                                }
                                // 結果をデバッグ上にだけ表示
                                Debug.WriteLine("主キー更新：" + String.Format("{0:#,0}", countModify) + " 件");
                            }
                        }
                    }
                    txn.Commit();
                    dgvDt.AcceptChanges();
                    ret = true;
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
        /// KM8430: コード票マスタ取得
        /// </summary>
        /// <param name="codeSlipDt">コード票マスタ</param>
        /// <returns>注文情報データ</returns>
        public bool GetCodeSlipMst(ref DataTable codeSlipDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "SELECT * "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " "
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(codeSlipDt);
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
        /// コード票マスタ更新
        /// </summary>
        /// <param name="dgvDt">DataGridView</param>
        /// <returns>注文情報データ</returns>
        public bool UpdateCodeSlipMst(ref DataTable dgvDt)
        {

            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                var dtUpdate = new DataTable();
                var countInsert = 0;
                var countUpdate = 0;
                var countDelete = 0;
                using (var adapter = new MySqlDataAdapter())
                {
                    string sql = "SELECT * "
                        + "FROM "
                        + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " "
                    ;
                    adapter.SelectCommand = new MySqlCommand(sql, mpCnn);
                    using (var buider = new MySqlCommandBuilder(adapter))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        adapter.Fill(dtUpdate);

                        // 変更または削除があるかチェック
                        foreach (DataRow r in dtUpdate.Rows)
                        {
                            DataRow[] dgv = dgvDt.Select($"HMCD='{r["HMCD"].ToString()}'");
                            // 削除
                            if (dgv.Length == 0)
                            {
                                r.Delete();
                                countDelete++;
                            }
                            // 変更
                            else if (dgv.Length == 1)
                            {
                                for (int col = 0; col < dtUpdate.Columns.Count; col++)
                                {
                                    if (r[col].ToString() != dgv[0][col].ToString())
                                    {
                                        // 変更あり
                                        r[col] = dgv[0][col];
                                    }
                                }
                                //dtUpdate.Rows[0]["UPDTDT"] = DateTime.Now.ToString();
                                if (r.RowState == DataRowState.Modified)
                                    countUpdate++;
                            }
                            else
                            {
                                throw new Exception("品番に制約違反が発生");
                            }
                        }
                        // 追加更新削除があれば自動更新
                        if (countDelete + countUpdate > 0) adapter.Update(dtUpdate);


                        // 新規の行が存在するかチェック
                        foreach (DataRow r in dgvDt.Rows)
                        {
                            DataRow[] dr = dtUpdate.Select($"HMCD='{r["HMCD"].ToString()}'");
                            // 挿入
                            if (dr.Length == 0)
                            {
                                dtUpdate.ImportRow(r);
                                countInsert++;
                            }
                        }
                        if (countInsert > 0) adapter.Update(dtUpdate);

                        // 結果
                        if (countInsert + countUpdate + countDelete > 0)
                        {
                            Console.WriteLine("新規件数：" + String.Format("{0:#,0}", countInsert) + " 件");
                            Console.WriteLine("更新件数：" + String.Format("{0:#,0}", countUpdate) + " 件");
                            Console.WriteLine("削除件数：" + String.Format("{0:#,0}", countDelete) + " 件");
                        }
                        else
                        {
                            Console.WriteLine("更新はありませんでした．".PadLeft(18));
                        }

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
        /// 在庫ファイル取得
        /// </summary>
        /// <param name="invInfoMPDt">在庫ファイル取得</param>
        /// <returns>可否</returns>
        public bool GetInvInfoMPDt(ref DataTable invInfoMPDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "SELECT * "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8460 + " "
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(invInfoMPDt);
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
        /// 在庫ファイル更新
        /// </summary>
        /// <param name="dgvDt">DataGridViewBindingSource</param>
        /// <returns>成功可否</returns>
        public bool UpdateInventory(ref DataTable dgvDt)
        {

            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret = false;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                /*
                 * CommandBuilder でのコマンドの自動生成についての調査結果
                 * 「コマンドの自動生成規則」なるものがある
                 * 「更新および削除のオプティミスティック コンカレンシー」が怪しい
                 * つまり主キーが更新された場合はコマンドビルダーでの自動更新で例外異常となる↓
                 * 「同時実行違反 : UpdateCommand によって、処理予定の 1 レコードのうち 0 件が処理されました。」
                 * ※UpdateCommand に明示的に DataAdapter を設定し、コマンドの自動生成は行わないでください。
                */

                var dtUpdate = new DataTable();
                var countInsert = 0;
                var countUpdate = 0;
                var countDelete = 0;
                var countModify = 0;
                using (MySqlTransaction txn = mpCnn.BeginTransaction())
                {
                    using (var adapter = new MySqlDataAdapter())
                    {
                        string sql = "SELECT * FROM " + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8460;
                        adapter.SelectCommand = new MySqlCommand(sql, mpCnn);

                        using (var buider = new MySqlCommandBuilder(adapter))
                        {
                            Debug.WriteLine("Read from DataTable:");
                            adapter.Fill(dtUpdate);

                            // DataGridView上に新規変更削除があるかチェック
                            foreach (DataRow dgv in dgvDt.Rows)
                            {
                                if (dgv.RowState == DataRowState.Added)
                                {
                                    dgv["MCCD"] = (dgv["MCCD"] == DBNull.Value) ? "" : dgv["MCCD"];
                                    dgv["MPINSTID"] = cmn.Ui.UserId;
                                    dgv["MPINSTDT"] = DateTime.Now.ToString();
                                    dgv["MPUPDTID"] = cmn.Ui.UserId;
                                    dgv["MPUPDTDT"] = DateTime.Now.ToString();
                                    dtUpdate.ImportRow(dgv);
                                    countInsert++;
                                }
                                else if (dgv.RowState == DataRowState.Deleted)
                                {
                                    string hmcd = dgv["HMCD", DataRowVersion.Original].ToString();
                                    string mcgcd = dgv["MCGCD", DataRowVersion.Original].ToString();
                                    string mccd = dgv["MCCD", DataRowVersion.Original].ToString();
                                    DataRow[] r = dtUpdate.Select($"HMCD='{hmcd}' and MCGCD='{mcgcd}' and MCCD='{mccd}'");
                                    if (r.Length == 1)
                                    {
                                        r[0].Delete();
                                        countDelete++;
                                    }
                                }
                                else if (dgv.RowState == DataRowState.Modified)
                                {
                                    string hmcd1 = dgv["HMCD", DataRowVersion.Original].ToString();
                                    string mcgcd1 = dgv["MCGCD", DataRowVersion.Original].ToString();
                                    string mccd1 = dgv["MCCD", DataRowVersion.Original].ToString();
                                    string hmcd2 = dgv["HMCD", DataRowVersion.Current].ToString();
                                    string mcgcd2 = dgv["MCGCD", DataRowVersion.Current].ToString();
                                    string mccd2 = dgv["MCCD", DataRowVersion.Current].ToString();
                                    DataRow[] r = dtUpdate.Select($"HMCD='{hmcd2}' and MCGCD='{mcgcd2}' and MCCD='{mccd2}'");
                                    // 主キーに変更が無い更新の場合
                                    if (hmcd1==hmcd2 && mcgcd1==mcgcd2 && mccd1==mccd2 && r.Length==1)
                                    {
                                        int change = 0;
                                        for (int col = 0; col < dtUpdate.Columns.Count; col++)
                                        {
                                            // 変更あり
                                            if (r[0][col].ToString() != dgv[col].ToString())
                                            {
                                                r[0][col] = dgv[col];
                                                change++;
                                            }
                                        }
                                        if (change > 0)
                                        {
                                            r[0]["MCCD"] = (r[0]["MCCD"] == DBNull.Value) ? "" : r[0]["MCCD"];
                                            r[0]["MPUPDTID"] = cmn.Ui.UserId;
                                            r[0]["MPUPDTDT"] = DateTime.Now.ToString();
                                            countUpdate++;
                                        }
                                    }
                                }
                            }
                            // 追加更新削除があればコマンドビルダーにて一括更新
                            if (countInsert + countUpdate + countDelete > 0)
                            {
                                try
                                {
                                    adapter.Update(dtUpdate);
                                }
                                catch (Exception ex)
                                {
                                    txn.Rollback();
                                    throw ex;
                                }
                                // 結果をデバッグ上にだけ表示
                                Debug.WriteLine("新規件数：" + String.Format("{0:#,0}", countInsert) + " 件");
                                Debug.WriteLine("更新件数：" + String.Format("{0:#,0}", countUpdate) + " 件");
                                Debug.WriteLine("削除件数：" + String.Format("{0:#,0}", countDelete) + " 件");
                            }
                            else
                            {
                                Debug.WriteLine("更新はありませんでした．".PadLeft(18));
                            }
                        }
                    }

                    // オプティミスティック コンカレンシー
                    // 主キーの更新
                    foreach (DataRow dgv in dgvDt.Rows)
                    {
                        if (dgv.RowState == DataRowState.Modified)
                        {
                            string orghmcd = dgv["HMCD", DataRowVersion.Original].ToString();
                            string orgmcgcd = dgv["MCGCD", DataRowVersion.Original].ToString();
                            string orgmccd = dgv["MCCD", DataRowVersion.Original].ToString();
                            string hmcd = dgv["HMCD", DataRowVersion.Current].ToString();
                            string mcgcd = dgv["MCGCD", DataRowVersion.Current].ToString();
                            string mccd = dgv["MCCD", DataRowVersion.Current].ToString();
                            // 主キーが変更された場合
                            if (orghmcd != hmcd || orgmcgcd != mcgcd || orgmccd != mccd)
                            {
                                string indtparam = (dgv["INDT"].ToString() == "") ? ", INDT=null" : $", INDT='{dgv["INDT"].ToString()}'";
                                string outdtparam = (dgv["OUTDT"].ToString() == "") ? ", OUTDT=null" : $", OUTDT='{dgv["OUTDT"].ToString()}'";
                                string indtdtparam = (dgv["MPINSTDT"].ToString() == "") ? ", MPINSTDT=null" : $", MPINSTDT='{dgv["MPINSTDT"].ToString()}'";
                                string sql = "UPDATE kd8460 set " + 
                                    $"HMCD='{hmcd}'"+
                                    $", MCGCD='{mcgcd}'"+
                                    $", MCCD='{mccd}'" + 
                                    $", ZAIQTY={Convert.ToInt32(dgv["ZAIQTY"].ToString())}" +
                                    indtparam +
                                    outdtparam +
                                    $", MPINSTID='{dgv["MPINSTID"].ToString()}'" +
                                    indtdtparam + 
                                    $", MPUPDTID='{cmn.Ui.UserId}'" + 
                                    $", MPUPDTDT=NOW()" + " " +
                                    $"WHERE HMCD='{orghmcd}' and MCGCD='{orgmcgcd}' and MCCD='{orgmccd}'";
                                MySqlCommand myCmd = new MySqlCommand(sql, mpCnn);
                                try
                                {
                                    int res = myCmd.ExecuteNonQuery();
                                    countModify++;
                                }
                                catch (Exception ex)
                                {
                                    txn.Rollback();
                                    throw ex;
                                }
                                // 結果をデバッグ上にだけ表示
                                Debug.WriteLine("主キー更新：" + String.Format("{0:#,0}", countModify) + " 件");
                            }
                        }
                    }
                    txn.Commit();
                    dgvDt.AcceptChanges();
                    ret = true;
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
        /// 切削手配ファイル受注状態ミラーリング
        /// EMの手配状態をMPシステムにミラーリング（3,4,9になった状態をMPに反映)
        /// （9:取消は手動でやってもらうためにミラーリング対象外）＝＞（やっぱり自動で更新してしまう）
        /// </summary>
        /// <param name="dtEM">EMの手配ファイル</param>
        /// <returns>更新件数</returns>
        public int UpdateODRSTS(ref DataTable dtEM)
        {

            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            string from = DateTime.Now.AddDays(-14).ToString("yyyy/MM/dd");
            string to = DateTime.Now.AddDays(14).ToString("yyyy/MM/dd");
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                // 切削手配ファイルミラーリング:KD8430
                using (var adapter = new MySqlDataAdapter())
                {
                    var dtUpdate = new DataTable();
                    var countUpdate = 0;
                    string sql = "SELECT ODRNO, ODRSTS, JIQTY "
                        + "FROM "
                        + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                        + "WHERE "
                        + "ODCD like '6060%' "
                        + "and ODRSTS in ('2','3') "
                        + $"and EDDT between '{from}' and '{to}' "
                    ;
                    adapter.SelectCommand = new MySqlCommand(sql, mpCnn);
                    using (var buider = new MySqlCommandBuilder(adapter))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        adapter.Fill(dtUpdate);

                        // EMが3:着手、4:完了、9:取消になっているかチェック
                        foreach (DataRow r in dtUpdate.Rows)
                        {
                            DataRow[] drEM = dtEM.Select($"ODRNO='{r["ODRNO"].ToString()}'");
                            if (drEM.Length == 1)
                            {
                                if (r["ODRSTS"].ToString() != drEM[0]["ODRSTS"].ToString())
                                {
                                    r["ODRSTS"] = drEM[0]["ODRSTS"];
                                    r["JIQTY"] = drEM[0]["JIQTY"];
                                    countUpdate++;
                                }
                            }
                        }
                        // 更新があればデータベースへの一括更新
                        if (countUpdate > 0)
                        {
                            adapter.Update(dtUpdate);
                            ret = countUpdate;
                        }
                    }
                }

                // 切削オーダーファイルミラーリング:KD8450
                using (var adapter = new MySqlDataAdapter())
                {
                    var dtUpdate = new DataTable();
                    var countUpdate = 0;
                    string sql = "SELECT ODRNO, MPSEQ, LOTSEQ, ODRSTS, JIQTY "
                        + "FROM "
                        + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8450 + " "
                        + "WHERE "
                        + "ODRSTS in ('2','3') "
                        + $"and EDDT between '{from}' and '{to}' "
                    ;
                    adapter.SelectCommand = new MySqlCommand(sql, mpCnn);
                    using (var buider = new MySqlCommandBuilder(adapter))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        adapter.Fill(dtUpdate);

                        // EMが3:着手、4:完了、9:取消になっているかチェック
                        foreach (DataRow r in dtUpdate.Rows)
                        {
                            DataRow[] drEM = dtEM.Select($"ODRNO='{r["ODRNO"].ToString()}'");
                            if (drEM.Length == 1)
                            {
                                if (r["ODRSTS"].ToString() != drEM[0]["ODRSTS"].ToString())
                                {
                                    r["ODRSTS"] = drEM[0]["ODRSTS"];
                                    r["JIQTY"] = drEM[0]["JIQTY"];
                                    countUpdate++;
                                }
                            }
                        }
                        // 更新があればデータベースへの一括更新
                        if (countUpdate > 0)
                        {
                            adapter.Update(dtUpdate);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
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
        /// リスト印刷用の切削手配取得
        /// </summary>
        /// <param name="tehaiDt">EMの手配ファイル</param>
        /// <returns>取得件数</returns>
        public bool GetTehaiZan(ref DataTable tehaiDt, int offsetdays)
        {
            bool ret = false;
            DateTime today = DateTime.Today;
            if (today.DayOfWeek <= DayOfWeek.Monday) today = today.AddDays(-7);         // 基準日（月曜日までは先週扱い）
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek) % 7;   // 基準日を月曜日にする為の差分値
            DateTime thisMonday = today.AddDays(daysUntilMonday + offsetdays);          // 基準日からoffsetdays後を開始日
            string from = thisMonday.ToString("yyyy/MM/dd");
            string to = thisMonday.AddDays(5).ToString("yyyy/MM/dd");                   // 開始日から5日後を終了日

            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);
                string swSelect = "";
                string swGroupby = "";
                if (offsetdays == 0)
                {
                    swSelect = "CAST(CONCAT(YEAR(now()),'-1-1') as DATETIME) 完了予定日, ";
                }
                else
                {
                    swSelect = "aa.EDDT 完了予定日, ";
                    swGroupby = "aa.EDDT, ";
                }
                string sql = "SELECT " +
                    swSelect +
                    "aa.HMCD 品番, " +
                    "SUM(aa.ODRQTY) - SUM(aa.JIQTY) 手配残数, " +
                    "bb.KTKEY 工程 " +
                    "FROM " +
                    cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " aa, " +
                    cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " bb " +
                    "WHERE " +
                    "aa.HMCD=bb.HMCD and " +
                    "aa.ODRSTS<>'9' and " +
                    $"aa.EDDT between '{from}' and '{to}' " +
                    "GROUP BY " +
                    swGroupby + 
                    "aa.HMCD, " +
                    "bb.KTKEY " +
                    "HAVING SUM(aa.ODRQTY) - SUM(aa.JIQTY) > 0"
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(tehaiDt);
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
        /// リスト印刷用の切削内示情報取得
        /// </summary>
        /// <param name="naijiDt">EMの手配ファイル</param>
        /// <returns>取得件数</returns>
        public bool GetNaiji(ref DataTable naijiDt)
        {
            bool ret = false;
            DateTime today = DateTime.Today;
            if (today.DayOfWeek <= DayOfWeek.Monday) today = today.AddDays(-7);         // 基準日（月曜日までは先週扱い）
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek) % 7;   // 基準日を月曜日にする為の差分値
            DateTime thisMonday = today.AddDays(daysUntilMonday + 14);                  // 基準日から２週間後を開始日
            string from = thisMonday.ToString("yyyy/MM/dd");                            // ↓ゴールデンウィーク中は+19日(5+7+7)、通常時は+12日(5+7)
            string to = thisMonday.AddDays(19).ToString("yyyy/MM/dd");                  // 開始日から19日後(5+7+7)を終了日

            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "SELECT " +
                    "aa.EDDT 完了予定日, " +
                    "aa.HMCD 品番, " +
                    "SUM(aa.ODRQTY) - SUM(aa.JIQTY) 手配残数, " +
                    "bb.KTKEY 工程 " +
                    "FROM " +
                    cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " aa, " +
                    cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " bb " +
                    "WHERE " +
                    "aa.HMCD=bb.HMCD and " +
                    "aa.ODRSTS<>'9' and " +
                    $"aa.EDDT between '{from}' and '{to}' " +
                    "GROUP BY " +
                    "aa.EDDT, " +
                    "aa.HMCD, " +
                    "bb.KTKEY " +
                    "HAVING SUM(aa.ODRQTY) - SUM(aa.JIQTY) > 0"
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(naijiDt);
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
        /// リスト印刷用の在庫情報取得
        /// </summary>
        /// <param name="zaikoDt">EMの手配ファイル</param>
        /// <returns>取得件数</returns>
        public bool GetZaiko(ref DataTable zaikoDt)
        {
            bool ret = false;
            DateTime today = DateTime.Today;
            if (today.DayOfWeek <= DayOfWeek.Monday) today = today.AddDays(-7);         // 基準日（月曜日までは先週扱い）
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek) % 7;   // 基準日を月曜日にする為の差分値
            DateTime thisMonday = today.AddDays(daysUntilMonday);                       // 手配開始日
            string from1 = thisMonday.ToString("yyyy/MM/dd");
            string to1 = thisMonday.AddDays(12).ToString("yyyy/MM/dd");                 // 手配開始日から+12日間(5+7)を手配終了日
            string from2 = thisMonday.AddDays(14).ToString("yyyy/MM/dd");               // 手配開始日から+14日を内示開始日
            string to2 = thisMonday.AddDays(14+19).ToString("yyyy/MM/dd");              // 内示開始日から19日後(5+7+7)を終了日

            MySqlConnection mpCnn = null;
            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "SELECT " +
                    "CAST(CONCAT(YEAR(now()) + 1,'-12-31') as DATETIME) 完了予定日, " +
                    "aa.HMCD 品番, " +
                    "SUM(aa.ZAIQTY) 手配残数, " +
                    "bb.KTKEY 工程 " +
                    "FROM " +
                    cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8460 + " aa, " +
                    cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " bb " +
                    "WHERE " +
                    "aa.HMCD = bb.HMCD and " +
                    "aa.MCGCD = 'STORE' and " +
                    "(" +
                        "aa.HMCD in (" + 
                            $"SELECT yy.HMCD FROM kd8430 yy WHERE yy.EDDT between '{from1}' and '{to1}' and " +
                            "yy.ODRSTS<>'9' and (yy.ODRQTY - yy.JIQTY) > 0) " + 
                        "OR " + 
                        "aa.HMCD in (" +
                            $"SELECT zz.HMCD FROM kd8440 zz WHERE zz.EDDT between '{from2}' and '{to2}' and " +
                            "zz.ODRSTS<>'9' and (zz.ODRQTY - zz.JIQTY) > 0) " +
                    ") " +
                    "GROUP BY aa.HMCD"
                ;
                // 主キーだけど GroupBy で対策
                // 　<target>.手配残数 と <source>.手配残数 は DataType プロパティの不一致
                // zaikoDt.Columns.List[3].DataType = {Name = "Int32" FullName = "System.Int32"}
                // tehaiDt.Columns.List[3].DataType = {Name = "Decimal" FullName = "System.Decimal"}
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(zaikoDt);
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

        // 切削コード票の前工程を取得するSQL
        // ①対象品番の(KTSEQ >= 10 )の抽出し品番でグループ化する 
        // ②1つ前の(KTSEQ - 10 ) を抽出する
        public bool GetMPMaeKT(ref DataTable maektDt)
        {
            bool ret = false;

            MySqlConnection mpCnn = null;
            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "select m51.HMCD " +
                ", max(case when m51.KTSEQ = 10 and m51.KTCD in ('CMTM', 'WL04') then m41.KTNM when m51.KTSEQ = 10 then m30.ODRNM else null end) 前工程① " +
                ", max(case when m51.KTSEQ = 20 and m51.KTCD in ('CMTM', 'WL04') then m41.KTNM when m51.KTSEQ = 20 then m30.ODRNM else null end) 前工程② " +
                "from M0510 m51, M0410 m41, M0300 m30, " +
                "( " +
                    "select a.HMCD, min(a.KTSEQ) KTSEQ, max(a.VALDTF) VALDTF " +
                    "from m0510 a, km8430 b " +
                    "where a.HMCD = b.HMCD " +
                    "and a.ODCD like '6060%' " +
                    "and a.KTSEQ > 10 " +
                    "and MOD(a.KTSEQ,10) = 0 " +
                    "and a.VALDTF = " +
                    "(select MAX(tmp.VALDTF) from M0510 tmp where tmp.HMCD = a.HMCD and tmp.VALDTF < now()) " +
                    "group by a.HMCD" +
                ") base " +
                "where " +
                "base.HMCD = m51.HMCD " +
                "and base.VALDTF = m51.VALDTF " +
                "and m51.KTSEQ < base.ktseq " +
                "and MOD(m51.KTSEQ,10) = 0 " +
                "and m51.ODCD not like '6060%' " +
                "and m51.KTCD = m41.KTCD " +
                "and m51.ODCD = m30.ODCD " +
                "group by m51.HMCD"
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(maektDt);
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

    }
}
