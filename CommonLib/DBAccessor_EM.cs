using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

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

                // 再来週の月曜日を取得
                string from = 切削の再来週月曜日().ToString("yyyy/MM/dd");

                string sql; // EM内示データに、独自列（MP週初完了予定日）を追加して取得
                sql = "SELECT "
                    + Common.TABLE_ID_D0440 + ".*, NEXT_DAY(EDDT - 7, 2) WEEKEDDT "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_D0440 + " "
                    + "WHERE "
                    + "ODCD like '6060%' and "
                    + $"EDDT >= '{from}' "
                    + "ORDER BY EDDT, HMCD, PLNNO "
                ;
                using (OracleCommand myCmd = new OracleCommand(sql, emCnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        myDa.Fill(emPlanDt);
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

        // PCの日付を起算日として、再来週の月曜日を返却
        // ※切削の今週の月曜日の扱いに注意（月曜日の場合は先週の月曜日が今週の月曜日となる）
        private DateTime 切削の再来週月曜日()
        {
            DateTime today = DateTime.Today.AddDays(0);

            // 今日の曜日を取得（0=Sunday, 1=Monday, ... 6=Saturday）
            int todayDayOfWeek = (int)today.DayOfWeek;

            // 今週の月曜日までの日数差
            int daysUntilMonday = ((int)DayOfWeek.Monday - todayDayOfWeek) % 7;

            // 今週の月曜日（日・月の場合は-7日）
            DateTime thisWeekMonday = today.AddDays((todayDayOfWeek <= 1) ? daysUntilMonday - 7 : daysUntilMonday);

            // 再来週の月曜日 = 今週の月曜日 + 14日
            DateTime mondayAfterNextWeek = thisWeekMonday.AddDays(14);

            return mondayAfterNextWeek;
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
            string from = DateTime.Now.AddDays(-31).ToString("yyyy/MM/dd");
            string to = DateTime.Now.AddDays(14).ToString("yyyy/MM/dd");
            OracleConnection emCnn = null;

            try
            {
                // EMデータベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref emCnn);

                string sql;
                sql = "SELECT ODRNO, ODRSTS, JIQTY, DENPYODT, UPDTID, UPDTDT "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_D0410 + " "
                    + "WHERE "
                    + $"ODRNO > {yyMM}000000 " // EDDTにインデックスが貼ってないので検索対象をまず絞ってから抽出する
                    + "and ODCD like '6060%' "
                    + "and ODRSTS in ('2','3','4','9') "
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

                // ダミーデータの作成
                if (dataTable.Rows.Count > 0)
                {
                    from = thisTuesday.AddDays(-1).ToString("yyyy/MM/dd");
                    to = nextFriday.ToString("yyyy/MM/dd");
                    sql = "select '!DUMMY' \"品番\", '!DUMMY' \"品目略称\", to_char(YMD,'YYYY/MM/DD') \"完了予定日\""
                        + ", 0 \"手配数\", 0 \"実績数\", 1 \"件数\", 0 \"S\" "
                        + "from "
                        + cmn.DbCd[Common.DB_CONFIG_EM].Schema + "." + Common.TABLE_ID_S0820 + " "
                        + "where "
                        + "CALTYP = '00001' "
                        + "and WKKBN = '1' "
                        + $"and YMD between '{from}' and '{to}' "
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