using MySql.Data.MySqlClient;
using NLog.Targets;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Interop;

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

            int ret;
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
        /// 注文情報データ取得
        /// </summary>
        /// <param name="mpDt">注文情報データ</param>
        /// <param name="firstDayOfMonth">検査対象月</param>
        /// <returns>注文情報データ</returns>
        public bool GetMpOrderSummaryInfo(ref DataTable mpDt, DateTime firstDayOfMonth)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string yyyyMMdd = firstDayOfMonth.ToString("yyyy/MM/dd");
                string sql = "SELECT "
                    + "EDDT "
                    + ", concat('',sum(case when ODRSTS in ('1','2') then 1 else 0 end)) \"MP2確定件数\" "
                    + ", concat('',sum(case when ODRSTS = '3' then 1 else 0 end)) \"MP3着手件数\" "
                    + ", concat('',sum(case when ODRSTS = '4' then 1 else 0 end)) \"MP4完了件数\" "
                    + ", concat('',sum(case when ODRSTS = '9' then 1 else 0 end)) \"MP9取消件数\" "
                    + ", concat('',sum(case when ODRSTS in ('1','2','3','4','9') then 1 else 0 end)) \"MP取込件数\" "
                    + ", concat('',sum(case when ODRSTS in ('1','2') then ODRQTY else 0 end)) \"MP2確定本数\" "
                    + ", concat('',sum(case when ODRSTS = '3' then ODRQTY-JIQTY else 0 end)) \"MP3着手本数\" "
                    + ", concat('',sum(case when ODRSTS = '4' then ODRQTY else 0 end)) \"MP4完了本数\" "
                    + ", concat('',sum(case when ODRSTS = '9' then ODRQTY else 0 end)) \"MP9取消本数\" "
                    + ", concat('',sum(case when ODRSTS in ('1','2','3','4','9') then ODRQTY else 0 end)) \"MP取込本数\" "
                    + ", concat('',sum(case when ODRSTS != '9' and ODCD != '60605' and (MPCARDDT<>'2999/12/31 23:59:00' or MPCARDDT is NULL) then 1 else 0 end)) \"MP印刷対象\" "
                    + ", concat('',sum(case when ODRSTS != '9' and ODCD != '60605' and MPCARDDT<>'2999/12/31 23:59:00' and MPCARDDT is not NULL then 1 else 0 end)) \"MP印刷件数\" "
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

            bool ret;
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

            bool ret;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "SELECT " + select + " "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + ", "
                    + "(SELECT HMCD as HMKEY, HMNM, MATERIALCD, KTKEY FROM "
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
        /// 注文情報データ取得
        /// </summary>
        /// <param name="mpOrderDt">注文情報データ</param>
        /// <param name="qrcd">検索条件WHERE文から指定</param>
        /// <returns>注文情報データ</returns>
        public bool GetMpQRCD(ref DataTable mpOrderDt, string qrcd)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "SELECT * FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                    + "WHERE QRCD = '" + qrcd + "'"
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
        /// 内示実績ありデータ取得
        /// </summary>
        /// <param name="mpNaijiDt">内示実績ありデータ</param>
        /// <returns>内示実績ありデータ</returns>
        public bool GetMpNaiji(ref DataTable mpNaijiDt)
        {
            bool ret;
            MySqlConnection mpCnn = null;
            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "select * from "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " "
                    + "where JIQTY > 0 order by HMCD asc, EDDT asc, EDTM asc, PLNNO asc";
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        myDa.Fill(mpNaijiDt);
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
        /// 手配日程テンポラリの作成
        /// </summary>
        /// <param name="mpNaijiTempDt">内示実績ありデータ</param>
        /// <returns>内示実績ありデータ</returns>
        public bool CreateMpNaijiTemp(ref DataTable mpNaijiTempDt, bool isNotRed)
        {
            bool ret;
            MySqlConnection mpCnn = null;
            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = mpCnn
                };

                if (isNotRed)
                {
                    // 手配日程テンポラリの削除
                    cmd.CommandText = "delete from "
                        + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KW8440;
                    int deleteCount = cmd.ExecuteNonQuery();

                    // 手配日程テンポラリの作成
                    string tancd = cmn.IkM0010.TanCd;
                    string insertSql = "insert into " +
                        cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KW8440 + " " +
                        "(HMCD, JIQTY, ODRALLOC, PLNALLOC, INSTID, UPDTID) " +
                        $"select HMCD, sum(JIQTY), 0, 0, '{tancd}','{tancd}' from " +
                        cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " " +
                        "group by HMCD having sum(JIQTY) > 0 order by HMCD";
                    cmd.CommandText = insertSql;
                    int insertCount = cmd.ExecuteNonQuery();
                }
                // 手配日程テンポラリ読み込み
                string sql = "select * from " +
                    cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KW8440;
                if (!isNotRed) sql += " " + "limit 0";
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        myDa.Fill(mpNaijiTempDt);
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
        /// <param name="exceptDt">EM側の登録すべきデータテーブル</param>
        /// <param name="codeDt">コード票マスタ</param>
        /// <param name="naijiDt">内示実績ありレコード</param>
        /// <param name="mpNaijiTempDt">手配日程テンポラリ</param>
        /// <param name="cardDt">内示カードファイル（手配登録時に印刷済み）</param>
        /// <returns>挿入件数、失敗時-1</returns>
        public int ImportMpOrder(ref DataTable exceptDt, ref DataTable codeDt, ref DataTable naijiDt, ref DataTable mpNaijiTempDt, ref DataTable cardDt, ref DataTable deleteODRNODt, Color styleBackColor)
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
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = mpCnn
                };

                // Bulk Insert用
                StringBuilder sb30 = new StringBuilder();
                StringBuilder sb50 = new StringBuilder();

                string sql = string.Empty;
                foreach (DataRow r in exceptDt.Rows)
                {
                    string odrno = r["ODRNO"].ToString();
                    string hmcd = r["HMCD"].ToString();

                    // KM8430:コード票マスタの品番抽出
                    DataRow[] mr = codeDt.Select($"HMCD='{hmcd}'");

                    // ODRNOが他の手配日付に存在したら削除しておく（手配日付がコロコロ変えられる対応）
                    // KD8450:切削オーダーの削除
                    cmd.CommandText = DeleteDuplicateOrderDetailSql(odrno);
                    cmd.ExecuteNonQuery();
                    // KD8430:切削手配ファイルの削除
                    cmd.CommandText = DeleteDuplicateOrderSql(odrno);
                    int delCountToday = cmd.ExecuteNonQuery();

                    // 受注状態「１：追加」を新設
                    if (styleBackColor == Common.FRM40_BG_COLOR_WARNING && r["ODRSTS"].ToString() == "2")
                    {
                        // 納期前倒し(delCountToday>0)または
                        // 納期後倒し(deleteODRNODt>0)の場合はステータスは更新しない
                        if (delCountToday == 0 && deleteODRNODt.Select($"ODRNO='{odrno}'").Count() == 0)
                        {
                            r["ODRSTS"] = "1";
                        }
                    }

                    // 内示カード発行済みの場合は手配カードを出させない
                    string MPCARDDT = string.Empty;
                    if (cardDt.Rows.Count > 0 && cardDt.Select($"HMCD='{hmcd}'").Count() != 0)
                    {
                        // 納期前倒し(delCountToday>0)または
                        DataRow[] cr = cardDt.Select($"HMCD='{hmcd}'");
                        int planqty = Int32.Parse(cr[0]["PLANQTY"].ToString());
                        int allocqty = Int32.Parse(cr[0]["ALLOCQTY"].ToString());
                        int odrqty = Int32.Parse(r["ODRQTY"].ToString());
                        if ((planqty - allocqty) >= odrqty)
                        {
                            MPCARDDT = "2999/12/31 23:59:00";
                        }
                        cr[0]["ALLOCQTY"] = allocqty + odrqty;
                        cr[0]["MPUPDTID"] = cmn.DrCommon.UpdtID;
                        cr[0]["MPUPDTDT"] = DateTime.Now;
                    }

                    // KD8430:切削手配ファイルの登録
                    // sql = ImportMpOrderSql(r, MPCARDDT);
                    sb30.Append(ImportMpOrderBulkData(r, MPCARDDT));

                    // KD8450:切削オーダーの登録（工程数分をループ）
                    int ktsu = Convert.ToInt32(mr[0]["KTSU"].ToString());
                    for (int kt = 1; kt <= ktsu; kt++)
                    {
                        string mcgcd = mr[0][$"KT{kt}MCGCD"].ToString();
                        string mccd = mr[0][$"KT{kt}MCCD"].ToString();

                        int appendqty = 0; // 実績に加算する数量
                        string odrsts = r["ODRSTS"].ToString();
                        // 追加処理の場合は実績数とステータスをいじらない
                        if (styleBackColor == Common.FRM40_BG_COLOR_WARNING)
                        {
                            odrsts = r["ODRSTS"].ToString();
                        }
                        // 初工程の場合のみ内示データの実績数を判定する（内示データが工程毎に分割されていない為）
                        // この方式が良いのかは不安が残る
                        else if (kt == 1)
                        {
                            int odrqty = Convert.ToInt32(r["ODRQTY"].ToString());
                            int odrjiq = Convert.ToInt32(r["JIQTY"].ToString());
                            int needqty = odrqty - odrjiq;
                            // KW8440:手配日程テンポラリ
                            DataRow[] wr = mpNaijiTempDt.Select($"HMCD='{hmcd}' and JIQTY>ODRALLOC");
                            if (wr.Length > 0)
                            {
                                int jiqty = Convert.ToInt32(wr[0]["JIQTY"].ToString());
                                int allocqty = Convert.ToInt32(wr[0]["ODRALLOC"].ToString());

                                if ((jiqty - allocqty) >= needqty)
                                {
                                    wr[0]["ODRALLOC"] = allocqty + needqty;     // テンポラリの引当数にまだまだ余裕がある
                                    odrsts = "4";
                                    appendqty = needqty;
                                }
                                else
                                {
                                    wr[0]["ODRALLOC"] = jiqty;                  // テンポラリの引当数を使い切った
                                    appendqty = jiqty - allocqty;
                                    odrsts = "3";
                                }
                            }
                        }

                        // KD8450:切削オーダーファイルの登録（各設備毎に分解）
                        // sql = DivideMpOrderSql(odrno, jiqty, kt, mcgcd, mccd, odrsts);
                        sb50.Append(DivideMpOrderBulkData(r, appendqty, kt, mcgcd, mccd, odrsts));

                    }   // kt ループ
                    insCount++;
                }

                // 最終的なデータベースへの一括登録
                int insert8430 = 0;
                int insert8450 = 0;
                if (sb30.Length > 0)
                {
                    sb30.Remove(sb30.Length - 1, 1); // 最後の1文字(,)を削除
                    sql = ImportMpOrderBulkSql() + sb30.ToString();
                    cmd.CommandText = sql;
                    insert8430 = cmd.ExecuteNonQuery();
                }
                if (sb50.Length > 0)
                {
                    sb50.Remove(sb50.Length - 1, 1); // 最後の1文字(,)を削除
                    sql = DivideMpOrderBulkSql() + sb50.ToString();
                    cmd.CommandText = sql;
                    insert8450 = cmd.ExecuteNonQuery();
                }
                Debug.WriteLine(exceptDt.Rows[0]["EDDT"].ToString() + $"手配: {insert8430} ({insert8450})件取込");

                // 内示ファイルと手配日程テンポラリの更新
                if (styleBackColor != Common.FRM40_BG_COLOR_WARNING)
                {
                    UpdateMpNaijiTemp(ref mpCnn, ref naijiDt, ref mpNaijiTempDt);
                }

                // トランザクション終了
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // ロールバック
                transaction?.Rollback();

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
        /// 内示ファイルと手配日程テンポラリの更新
        /// 手配日程テンポラリを参照し、内示実績をクリアした後、余りがあれば実績を付け替える
        /// </summary>
        /// <param name="plnno">計画No</param>
        /// <returns>SQL 構文</returns>
        private bool UpdateMpNaijiTemp(ref MySqlConnection mpCnn, ref DataTable naijiDt, ref DataTable mpNaijiTempDt)
        {
            MySqlCommand cmd = new MySqlCommand
            {
                Connection = mpCnn
            };
            int updateCount = 0;
            string sql;
            string tancd = cmn.IkM0010.TanCd;

            foreach (DataRow tmpRow in mpNaijiTempDt.Rows)
            {
                string hmcd = tmpRow["HMCD"].ToString();
                int jiqty = Convert.ToInt32(tmpRow["JIQTY"].ToString());
                int odralloc = Convert.ToInt32(tmpRow["ODRALLOC"].ToString());
                int plnalloc = 0;
                int countdownQty = jiqty - odralloc;
                if (tmpRow.RowState != DataRowState.Unchanged)
                {
                    DataRow[] targetRows =
                        naijiDt.Select($"HMCD='{hmcd}' and JIQTY>0",
                        "EDDT asc, EDTM asc, PLNNO asc");

                    // 対象品番の実績数とステータスを一括クリア
                    sql = $"update " +
                    cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " " +
                    $"set JIQTY=0, ODRSTS='2' where HMCD='{hmcd}' and JIQTY>0";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    foreach (DataRow row in targetRows)
                    {
                        row["JIQTY"] = 0;
                        row["ODRSTS"] = "2";
                    }
                    // 実績の残りを付け替え
                    foreach (DataRow row in targetRows)
                    {
                        if (countdownQty == 0) break; // 実績の余りが無い場合は即、手配日程テンポラリの更新へ
                        string plnno = row["PLNNO"].ToString();
                        int odrqty = Convert.ToInt32(row["ODRQTY"].ToString());
                        if (countdownQty >= odrqty)
                        {
                            // 内示の実績を付け替え
                            sql = $"update " +
                            cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " " +
                            $"set JIQTY={odrqty}, ODRSTS='4', UPDTID='{tancd}', UPDTDT=now() where PLNNO='{plnno}'";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            row["JIQTY"] = odrqty;
                            row["ODRSTS"] = "4";
                            // テンポラリの内示引落変数に加算
                            plnalloc += odrqty;
                            // ループ変数から引落
                            countdownQty -= odrqty;
                        }
                        else
                        {
                            // 内示の実績を付け替え
                            sql = $"update " +
                            cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " " +
                            $"set JIQTY={countdownQty}, ODRSTS='3', UPDTID='{tancd}', UPDTDT=now() where PLNNO='{plnno}'";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            row["JIQTY"] = countdownQty;
                            row["ODRSTS"] = "3";
                            // テンポラリの内示引落変数に加算
                            plnalloc += countdownQty;
                            // ループ変数をクリア
                            countdownQty = 0;
                        }
                    }
                    // テンポラリの内示引落数を更新
                    sql = $"update " +
                    cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KW8440 + " " +
                    $"set ODRALLOC={odralloc},PLNALLOC={plnalloc} where HMCD='{hmcd}'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    tmpRow["PLNALLOC"] = plnalloc;

                    updateCount++;
                }
            }
            if (updateCount > 0)
            {
                naijiDt.AcceptChanges();
                mpNaijiTempDt.AcceptChanges();
            }
            return true;
        }


        /// <summary>
        /// SQL 構文編集 (KD8430 切削手配ファイル) 
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string ImportMpOrderBulkSql()
        {
            string sql = "insert into "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                + "("
                + "ODRNO,"
                + "KTSEQ,"
                + "HMCD,"
                + "KTCD,"
                + "ODRQTY,"
                + "ODCD,"
                + "NEXTODCD,"
                + "LTTIME,"
                + "STDT,"
                + "STTIM,"
                + "EDDT,"
                + "EDTIM,"
                + "ODRSTS,"
                + "QRCD,"
                + "JIQTY,"
                + "DENPYOKBN,"
                + "DENPYODT,"
                + "NOTE,"
                + "WKNOTE,"
                + "WKCOMMENT,"
                + "DATAKBN,"
                + "INSTID,"
                + "INSTDT,"
                + "UPDTID,"
                + "UPDTDT,"
                + "UKCD,"
                + "NAIGAIKBN,"
                + "RETKTCD,"
                + "MPCARDDT,"
                + "MPINSTID,"
                + "MPUPDTID"
                + ") "
                + "values ";
            return sql;
        }
        private string ImportMpOrderBulkData(DataRow r, string mpCardDt)
        {
            // 登録形式により抽出対象が異なる
            // MySql の DATE 型列に値を代入するときは、その列が時刻を持っているか否かに関わらず、必ず to_datetime('<代入元>') メソッドで変換してから代入する必要がある
            // 代入元が定数か変数化に関わらずシングル クォーテーション括りは必須
            // 代入元に書式 'YYYY/MM/DD HH24:MI:SS' 等の記述は不要、MySql が適切に合わせ込んで登録してくれる
            // この変換を怠ると「ORA-01861: リテラルが書式文字列と一致しません」の例外が発生する
            string data = 
                "("
                + "'" + r["ODRNO"].ToString() + "',"
                + r["KTSEQ"] + ","
                + "'" + r["HMCD"].ToString() + "',"
                + "'" + r["KTCD"].ToString() + "',"
                + r["ODRQTY"] + ","
                + "'" + r["ODCD"].ToString() + "',"
                + "'" + r["NEXTODCD"].ToString() + "',"
                + r["LTTIME"] + ","
                + "'" + r["STDT"] + "',"
                + "'" + r["STTIM"].ToString() + "',"
                + "'" + r["EDDT"] + "',"
                + "'" + r["EDTIM"].ToString() + "',"
                + "'" + r["ODRSTS"].ToString() + "',"
                + "'" + r["QRCD"].ToString() + "',"
                + r["JIQTY"] + ","
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
                + (string.IsNullOrEmpty(mpCardDt) ? "null," : $"'{mpCardDt}',")
                + "'" + cmn.IkM0010.TanCd + "',"
                + "'" + cmn.IkM0010.TanCd + "'"
                + "),"
                ;
            return data;
        }

        /// <summary>
        /// SQL 構文編集 KD8440：切削オーダーファイル
        /// </summary>
        /// <returns>SQL 構文</returns>
        private string DivideMpOrderBulkSql()
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
                + ") values "
                ;
            return sql;
        }
        private string DivideMpOrderBulkData(DataRow r, int appendqty, int kt, string mcgcd, string mccd, string odrsts)
        {
            string data =
                "("
                + "'" + r["ODRNO"].ToString() + "',"
                + $"{kt},"
                + $"'{mcgcd}',"
                + $"'{mccd}',"
                + "'" + r["HMCD"].ToString() + "',"
                + "'" + r["EDDT"] + "',"
                + r["ODRQTY"] + ","
                + (Convert.ToInt32(r["JIQTY"].ToString()) + appendqty) + ","
                + $"'{odrsts}',"
                + $"'{cmn.IkM0010.TanCd}',"
                + $"'{cmn.IkM0010.TanCd}'"
                + "),"
                ;
            return data;
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
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = mpCnn
                };

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
                transaction?.Rollback();

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
        /// <param name="emPlanDt">取込対象のデータテーブル</param>
        /// <returns>可否</returns>
        public bool ImportMpPlan(ref DataTable emPlanDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret;
            MySqlConnection mpCnn = null;
            MySqlTransaction transaction = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                // トランザクション開始
                transaction = mpCnn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = mpCnn
                };

                string sql = string.Empty;

                // １．切削内示ファイルの実績数を集計
                DataTable naijiJissekiDt = new DataTable();
                sql = $"select HMCD, SUM(JIQTY) as JIQTY, 0 as PLNALLOC from " +
                    cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " " +
                    "group by HMCD having sum(JIQTY) > 0 order by HMCD";
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        myDa.Fill(naijiJissekiDt);
                    }
                }

                // ２．KD8440:切削手配日程ファイルの全削除
                sql = "delete from "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                // Bulk Insert 準備
                var insCount = 0;
                StringBuilder sb = new StringBuilder();
                string bulkinsert = ImportMpPlanBulkSql();
                foreach (DataRow r in emPlanDt.Rows)
                {
                    // ３．実績数チェック
                    string hmcd = r["HMCD"].ToString();
                    DataRow[] naijiJissekiDr = naijiJissekiDt.Select($"HMCD='{hmcd}'");
                    if (naijiJissekiDr.Length > 0)
                    {
                        int emodrqty = Convert.ToInt32(r["ODRQTY"].ToString());
                        int emjiqty = Convert.ToInt32(r["JIQTY"].ToString());
                        int mpalloc = Convert.ToInt32(naijiJissekiDr[0]["PLNALLOC"].ToString());
                        int mpjiqty = Convert.ToInt32(naijiJissekiDr[0]["JIQTY"].ToString());
                        if (mpjiqty != mpalloc)
                        {
                            if (emodrqty > emjiqty + (mpjiqty - mpalloc))
                            {
                                r["JIQTY"] = emjiqty + (mpjiqty - mpalloc);
                                r["ODRSTS"] = "3";
                                naijiJissekiDr[0]["PLNALLOC"] = mpjiqty;
                            }
                            else
                            {
                                r["JIQTY"] = emodrqty;
                                r["ODRSTS"] = "4";
                                naijiJissekiDr[0]["PLNALLOC"] = mpalloc + (emodrqty - emjiqty);
                            }
                        }
                    }

                    // ４．KD8440:切削手配日程ファイルの挿入
                    sb.Append(ImportMpPlanBulkData(r));
                    if (insCount > 0 && insCount % 2000 == 0)
                    {
                        if (sb.Length > 0) sb.Remove(sb.Length - 1, 1); // 最後の1文字(,)を削除
                        sql = bulkinsert + sb.ToString();
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        sb.Clear();
                    }

                    insCount++;
                }
                if (insCount > 0 && sb.Length > 0) // Bulk残の挿入
                {
                    sb.Remove(sb.Length - 1, 1); // 最後の1文字(,)を削除
                    sql = bulkinsert + sb.ToString();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }

                // ５．KW8440:切削手配日程テンポラリ削除
                var userid = cmn.Ui.UserId;
                sql = "delete from " +
                    cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KW8440 + " ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                // ６．KW8440:切削手配日程テンポラリへの一括登録
                sb.Clear();
                if (naijiJissekiDt.Rows.Count > 0)
                    sb.Append("insert into "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KW8440
                    + "(HMCD, JIQTY, PLNALLOC, INSTID, UPDTID) values ");
                for (int i = 0; i < naijiJissekiDt.Rows.Count; i++)
                {
                    var row = naijiJissekiDt.Rows[i];
                    sb.AppendFormat("('{0}',{1},{2},'{3}','{4}'),"
                        , row["HMCD"].ToString(), row["JIQTY"], row["PLNALLOC"], userid, userid);
                }
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1); // 最後の1文字(,)を削除
                    cmd.CommandText = sb.ToString();
                    cmd.ExecuteNonQuery();
                }

                // トランザクション終了
                transaction.Commit();

                ret = true;
            }
            catch (Exception ex)
            {
                // ロールバック
                transaction?.Rollback();

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
        private string ImportMpPlanBulkSql()
        {
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
                + ") values ";
            return sql;
        }
        private string ImportMpPlanBulkData(DataRow r)
        {
            string data = 
                "("
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
                + "),"
                ;
            return data;
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

            int ret;
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

            int ret;
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

            int ret;

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
                + "ODRSTS <> '9' and "
                + "ODCD <> '60605' and "  // 印刷対象外として「60605:タナコン管理外」を新設
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
                            // 接続を閉じる
                            cnn?.Close();
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

            int ret;
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

            int ret;
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
                ret = mpDt.Rows.Count;
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

            int ret;
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
                ret = cardDt.Rows.Count;
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

        // 通常の製造指示カード出力
        private string GetOrderCardPrintInfoSQL(ref DateTime eddtFrom, ref DateTime eddtTo)
        {
            string sql = GetOrderCardPrintInfoBaseSQL()
                + $"and a.EDDT between '{eddtFrom}' and '{eddtTo}' "
                + $"and a.ODRSTS <> '4' "
                + "and MPCARDDT is null " // 製造指示カード発行日
            ;
            return sql;
        }

        // 個別の製造指示カード再出力（手配No指定）
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
                + ",a.ODRQTY - a.JIQTY as ODRQTY "
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
                + "and a.ODCD <> '60605' " // 印刷対象外として「60605:タナコン管理外」を新設
            ;
            // 手配取込時に ODCD like '6060%'をしている
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

            // 切削生産計画システム データベースへ接続
            cmn.Dbm.IsConnectMySqlSchema(ref cnn);

            using (MySqlTransaction txn = cnn.BeginTransaction())
            {
                try
                {
                    string sql = string.Empty;

                    // 内示カードを加味する為の変数
                    DateTime monday = eddtFrom.AddDays(-(int)eddtFrom.DayOfWeek + 1);

                    // 製造指示カード発行済みに更新
                    sql = "UPDATE "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                    + "SET "
                    + "MPCARDDT = now(), "
                    + "UPDTID = '" + cmn.DrCommon.UpdtID + "' "
                    + "WHERE "
                    + "ODRSTS <> '9' and "
                    + "ODCD <> '60605' and "  // 印刷対象外として「60605:タナコン管理外」を新設
                    + "MPCARDDT is NULL and " // 製造指示カード発行日
                    + $"EDDT between '{eddtFrom}' and '{eddtTo}'"
                    ;
                    using (MySqlCommand myCmd = new MySqlCommand(sql, cnn))
                    {
                        int res = myCmd.ExecuteNonQuery();
                        if (res >= 1) ret += res;
                    }

                    // 製造指示カード発行済みに更新 SQLコミット
                    if (ret >= 1)
                    {
                        txn.Commit();
                    }
                }
                catch (Exception e)
                {
                    txn.Rollback();
                    MessageBox.Show("印刷済みステータス更新で異常が発生しました．\nシステム担当者に連絡してください．", "異常発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.WriteLine(Common.TABLE_ID_KD8430 + " table no data inserted/updated.");

                    Debug.WriteLine("Exception Source = " + e.Source);
                    Debug.WriteLine("Exception Message = " + e.Message);
                    ret = -1;
                }
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

            int ret;

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
                    + "ODRSTS <> '9' and "
                    + "ODCD <> '60605' and "  // 印刷対象外として「60605:タナコン管理外」を新設
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
                            // 接続を閉じる
                            cnn?.Close();
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

            bool ret;
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
                    + ", b.KTKEY "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " a, "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " b "
                    + "WHERE "
                    + "a.HMCD = b.HMCD "
                    + "and ODCD like '6060%' "
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

            bool ret;
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

            int ret;
            MySqlConnection mpCnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql =
                "SELECT "
                    + "a.EDDT "
                    + ",if(min(a.ODRNO) is null, concat('P', min(a.PLNNO)), concat('K', min(a.ODRNO))) PLNNO "
                    + ",sum(a.ODRQTY) ODRQTY "
                    + ",a.HMCD "
                    + ",b.HMNM "
                    + ",b.MATESIZE "
                    + ",b.LENGTH "
                    + ",b.BOXQTY "
                    + ",b.BOXCD "
                    + ",b.PARTNER "
                    + ",b.MATERIALLEN "
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
                    + ",min(c.CUTTHICKNESS) CUTTHICKNESS "
                    + ",min(c.SCRAPLEN) SCRAPLEN "
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
                    + "and NOT EXISTS ( "
                        + "SELECT * FROM "
                        + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8470 + " tmp "
                        + "WHERE tmp.HMCD=a.HMCD and tmp.WEEKEDDT=a.WEEKEDDT"
                        + ") "
                    + "GROUP BY "
                    + "c.MCSEQ "
                    + ", b.MATESIZE "
                    + ", a.HMCD "
                    + ", a.EDDT "
                    + "ORDER BY "
                    + "c.MCSEQ "
                    + ", b.MATESIZE desc "
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
        /// 内示カード発行済み登録（新規印刷）
        /// </summary>
        /// <param name="weekEddt">検査対象月</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int InsertPlanCard(DateTime weekEddt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret;

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
                + "( "
                + "SELECT "
                + "a.HMCD "
                + ", a.WEEKEDDT "
                + ", SUM(a.ODRQTY) "
                + ", now() "
                + ", '" + cmn.DrCommon.UpdtID + "' "
                + ", now() "
                + ", '" + cmn.DrCommon.UpdtID + "' "
                + ", now() "
                + "FROM "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " a, "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " b "
                + "WHERE "
                + "a.HMCD=b.HMCD "
                + "and b.KT1MCGCD = 'SW' "
                + $"and a.WEEKEDDT = '{weekEddt.ToString()}' "
                + "and NOT EXISTS ( "
                    + "SELECT * FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8470 + " tmp "
                    + "WHERE tmp.HMCD=a.HMCD and tmp.WEEKEDDT=a.WEEKEDDT"
                    + ") "
                + "GROUP BY "
                + "a.HMCD "
                + ") "
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
                            // 接続を閉じる
                            cnn?.Close();
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
        /// 内示カード発行済みを削除
        /// </summary>
        /// <param name="weekEddt">検査対象月</param>
        public bool DeletePlanCard(DateTime weekEddt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret;
            MySqlConnection mpCnn = null;
            MySqlTransaction transaction;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                // トランザクション開始
                transaction = mpCnn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = mpCnn
                };

                string sql = string.Empty;

                // KD8470:内示カードファイルの削除
                sql = "delete from "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8470 + " "
                    + $"where WEEKEDDT ='{weekEddt}'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                // トランザクション終了
                transaction.Commit();
                ret = true;
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





        // ********** 設備マスタ関連 **********
        /// <summary>
        /// 設備マスタ取得
        /// </summary>
        /// <param name="equipMstDt">設備マスタ</param>
        /// <returns>可否</returns>
        public bool GetEquipMstDt(ref DataTable equipMstDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret;
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

            bool ret;
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

            bool ret;
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

            bool ret;
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
                                    if (r[col].ToString() != dgv[0][col].ToString() && col != dtUpdate.Columns.IndexOf("UPDTID"))
                                    {
                                        // 変更あり
                                        r[col] = dgv[0][col];
                                        r["UPDTID"] = cmn.Ui.UserId;
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
                                r["INSTID"] = cmn.Ui.UserId;
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

            bool ret;
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

            bool ret;
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
            string from = DateTime.Now.AddDays(-31).ToString("yyyy/MM/dd");
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
                    var countDelete = 0;
                    string sql = "SELECT ODRNO, ODRSTS, JIQTY, DENPYODT, UPDTID, UPDTDT, MPUPDTID "
                        + "FROM "
                        + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8430 + " "
                        + "WHERE "
                        + "ODCD like '6060%' "
                        + "and ODRSTS in ('1','2','3','9') "
                        + $"and EDDT between '{from}' and '{to}' "
                    ;
                    adapter.SelectCommand = new MySqlCommand(sql, mpCnn);
                    using (var buider = new MySqlCommandBuilder(adapter))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        adapter.Fill(dtUpdate);

                        foreach (DataRow r in dtUpdate.Rows)
                        {
                            string MpSTS = r["ODRSTS"].ToString();
                            DataRow[] drEM = dtEM.Select($"ODRNO='{r["ODRNO"].ToString()}'");
                            if (drEM.Length == 1)
                            {
                                // EMのステータスと相違がないかチェック 2:確定、3:着手、4:完了、9:取消
                                string EmSTS = drEM[0]["ODRSTS"].ToString();
                                if (MpSTS == "1" && EmSTS == "2") continue;
                                if (MpSTS != EmSTS)
                                {
                                    r["ODRSTS"] = drEM[0]["ODRSTS"];
                                    r["JIQTY"] = drEM[0]["JIQTY"];
                                    r["DENPYODT"] = drEM[0]["DENPYODT"];
                                    r["UPDTID"] = drEM[0]["UPDTID"];
                                    r["UPDTDT"] = drEM[0]["UPDTDT"];
                                    r["MPUPDTID"] = cmn.Ui.UserId;
                                    countUpdate++;
                                }
                            }
                            else if (drEM.Length == 0 && MpSTS == "9")
                            {
                                r.Delete();
                                countDelete++;
                            }
                        }
                        // 更新があればデータベースへの一括更新
                        if (countUpdate + countDelete > 0)
                        {
                            adapter.Update(dtUpdate);
                            ret = countUpdate + countDelete;
                        }
                    }
                }

                // 切削オーダーファイルミラーリング:KD8450
                using (var adapter = new MySqlDataAdapter())
                {
                    var dtUpdate = new DataTable();
                    var countUpdate = 0;
                    var countDelete = 0;
                    string sql = "SELECT ODRNO, MPSEQ, LOTSEQ, ODRSTS, JIQTY, MPUPDTID "
                        + "FROM "
                        + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8450 + " "
                        + "WHERE "
                        + "ODRSTS in ('1','2','3','9') "
                        + $"and EDDT between '{from}' and '{to}' "
                    ;
                    adapter.SelectCommand = new MySqlCommand(sql, mpCnn);
                    using (var buider = new MySqlCommandBuilder(adapter))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        adapter.Fill(dtUpdate);

                        foreach (DataRow r in dtUpdate.Rows)
                        {
                            string MpSTS = r["ODRSTS"].ToString();
                            DataRow[] drEM = dtEM.Select($"ODRNO='{r["ODRNO"].ToString()}'");
                            if (drEM.Length == 1)
                            {
                                // EMのステータスと相違がないかチェック 2:確定、3:着手、4:完了、9:取消
                                string EmSTS = drEM[0]["ODRSTS"].ToString();
                                if (MpSTS == "1" && EmSTS == "2") continue;
                                if (MpSTS != EmSTS)
                                {
                                    r["ODRSTS"] = drEM[0]["ODRSTS"];
                                    r["JIQTY"] = drEM[0]["JIQTY"];
                                    r["MPUPDTID"] = cmn.Ui.UserId;
                                    countUpdate++;
                                }
                            }
                            else if (drEM.Length == 0 && MpSTS == "9")
                            {
                                r.Delete();
                                countDelete++;
                            }
                        }
                        // 更新があればデータベースへの一括更新
                        if (countUpdate + countDelete > 0)
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
            bool ret;
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
            bool ret;
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
            bool ret;
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
            bool ret;

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

        /// <summary>
        /// 内示情報データ取得
        /// </summary>
        /// <param name="mpPlanDt">注文情報データ</param>
        /// <param name="select">検索条件WHERE文から指定</param>
        /// <param name="where">検索条件WHERE文から指定</param>
        /// <returns>注文情報データ</returns>
        public bool FindMpPlan(ref DataTable mpPlanDt, string select, string where)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "SELECT " + select + " "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " a "
                    + "LEFT OUTER JOIN "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8470 + " b "
                    + " on b.HMCD=a.HMCD and b.WEEKEDDT=a.WEEKEDDT, "
                    + "(SELECT HMCD, HMNM, MATERIALCD, KTKEY FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + ") m "
                    + "WHERE "
                    + "a.HMCD=m.HMCD and "
                    + where
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(mpPlanDt);
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
        /// 内示カードに印刷するデータの取得（個別明細指定）
        /// </summary>
        /// <param name="targetDt">データグリッドビューで指定した印刷対象</param>
        /// <param name="cardDt">製造指示カードデータ</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int GetPlanCardPrintInfo(DataTable targetDt, ref DataTable cardDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            MySqlConnection mpCnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = GetPlanCardPrintInfoSQL(targetDt);

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

        // 個別の製造指示カード再出力（計画No指定）
        private string GetPlanCardPrintInfoSQL(DataTable targetDt)
        {
            // 計画Noを設定
            string numbers = string.Empty;
            foreach (DataRow dr in targetDt.Rows)
            {
                numbers += "'" + dr["計画No"].ToString().Substring(1) + "',"; // DataGridViewのタイトル名で来るので注意！
            }
            numbers += "'x'";
            string addWhere = "and (" +
                "a.ODRNO in (" + numbers + ") or " +
                "a.PLNNO in (" + numbers + ")" +
            ") ";

            string sql =
            "SELECT "
                + "a.EDDT "
                + ",if(a.ODRNO is null, concat('P', a.PLNNO), concat('K', a.ODRNO)) PLNNO "
                + ",a.ODRQTY "
                + ",a.HMCD "
                + ",a.WEEKEDDT "
                + ",b.HMNM "
                + ",b.MATESIZE "
                + ",b.LENGTH "
                + ",b.BOXQTY "
                + ",b.BOXCD "
                + ",b.PARTNER "
                + ",b.MATERIALLEN "
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
                + "and b.KT1MCGCD = c.MCGCD "
                + "and b.KT1MCCD = c.MCCD "
                + addWhere
                + "ORDER BY "
                + "c.MCSEQ "
                + ", b.MATESIZE desc "
                + ", a.HMCD "
                + ", a.EDDT"
            ;
            return sql;
        }

        /// <summary>
        /// 内示カード発行済みに更新（個別明細指定）
        /// </summary>
        /// <param name="targetDt">手配日</param>
        /// <returns>結果 (0≦: 成功 (件数), 0＞: 失敗)</returns>
        public int InsertUpdatePlanCard(ref DataTable targetDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret;

            MySqlConnection cnn = null;

            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref cnn);

                // 計画Noを設定
                string numbers = string.Empty;
                foreach (DataRow dr in targetDt.Rows)
                {
                    numbers += "'" + dr["PLNNO"].ToString().Substring(1) + "',";
                }
                numbers += "'x'";
                string where = "(" +
                    "a.ODRNO in (" + numbers + ") or " +
                    "a.PLNNO in (" + numbers + ")" +
                ") ";

                // 内示カード発行済みに更新（新規の場合はInsert、既存再印刷の場合はUpdate）
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
                + "( "
                + "SELECT "
                + "a.HMCD "
                + ", a.WEEKEDDT "
                + ", SUM(a.ODRQTY) "
                + ", now() "
                + ", '" + cmn.DrCommon.UpdtID + "' "
                + ", now() "
                + ", '" + cmn.DrCommon.UpdtID + "' "
                + ", now() "
                + "FROM "
                + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8440 + " a "
                + "WHERE "
                + where
                + "GROUP BY "
                + "a.WEEKEDDT "
                + ", a.HMCD "
                + ") " +
                "ON DUPLICATE KEY UPDATE " // 主キーが設定されている場合のみ可能
                + "PLANQTY=VALUES(PLANQTY) "
                + ", PLANCARDDT=VALUES(PLANCARDDT) "
                + ", MPUPDTID=VALUES(MPUPDTID) "
                + ", MPUPDTDT=VALUES(MPUPDTDT) "
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
                            // 接続を閉じる
                            cnn?.Close();
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
        /// 内示カードファイル一週間分を取得
        /// </summary>
        /// <param name="mpCardDt">空の内示カードデータファイル</param>
        /// <param name="eddtmon">月曜日を日付文字列で指定</param>
        /// <returns>内示カードファイルを取得（月曜日からの一週間分）</returns>
        public bool GetMpCard(ref DataTable mpCardDt, string eddtmon)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "SELECT * "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8470 + " "
                    + "WHERE "
                    + $"WEEKEDDT = '{eddtmon}'"
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(mpCardDt);
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
        /// 内示カードファイル３カ月分を取得
        /// </summary>
        /// <param name="mpCardReportDt">空の内示カードデータファイル</param>
        /// <param name="eddtmon">月曜日を日付文字列で指定</param>
        /// <returns>内示カードファイルを取得（月曜日からの一週間分）</returns>
        public bool GetMpCardReport(ref DataTable mpCardReportDt, DateTime targetMonth)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            bool ret;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                var from = targetMonth.AddMonths(-1);
                var to = targetMonth.AddMonths(2);
                string sql = "SELECT * "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8470 + " "
                    + "WHERE "
                    + $"WEEKEDDT between '{from}' and '{to}'"
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        Debug.WriteLine("Read from DataTable:");
                        // 結果取得
                        myDa.Fill(mpCardReportDt);
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
        /// KD8470: 内示カードファイル更新（コマンドビルダーにて一括更新）
        /// 手配インポートした際、手配数を内示カードの手配引当数として加算
        /// </summary>
        /// <param name="mpNaijiReportDt">DataGridView</param>
        /// <returns>注文情報データ</returns>
        public bool UpdateMpNaijiCard(ref DataTable mpNaijiReportDt)
        {
            bool ret;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                using (MySqlTransaction txn = mpCnn.BeginTransaction())
                {
                    using (var adapter = new MySqlDataAdapter())
                    {
                        DateTime eddtFrom = DateTime.Parse(mpNaijiReportDt.Compute("MIN(WEEKEDDT)", string.Empty).ToString());
                        DateTime eddtTo = DateTime.Parse(mpNaijiReportDt.Compute("MAX(WEEKEDDT)", string.Empty).ToString());
                        string sql = "SELECT * FROM "
                            + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8470 + " "
                            + $"WHERE WEEKEDDT between '{eddtFrom}' and '{eddtTo}'"
                        ;
                        adapter.SelectCommand = new MySqlCommand(sql, mpCnn);
                        using (var buider = new MySqlCommandBuilder(adapter))
                        {

                            // 更新があればコマンドビルダーにて一括更新
                            try
                            {
                                adapter.Update(mpNaijiReportDt);
                            }
                            catch (Exception ex)
                            {
                                txn.Rollback();
                                throw ex;
                            }
                        }
                    }
                    txn.Commit();
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
        /// 切削オーダーファイル集計（注文ダッシュボード用）
        /// リアルタイムで集計するのはサーバーの負荷が大きすぎるので
        /// 夜間バッチor手配取込時に計算してKD8510:集計テーブルを作成
        /// 先週・今週・来週の3週間分をリフレッシュ
        /// </summary>
        /// <param name="emDt">EMの手配ファイル</param>
        /// <returns>終了状態</returns>
        public bool HowManyOrders(ref DataTable calendarDt)
        {
            bool ret = false;
            MySqlConnection mpCnn = null;
            try
            {
                // 切削生産計画システム データベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                // 今週の月曜日を取得
                DateTime today = DateTime.Today;
                int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
                DateTime thisMonday = today.AddDays(-diff);

                // 稼働日ベースでの先週の月曜日をカレンダーテーブルから取得
                DateTime prevMonday = thisMonday;
                int workingDaysCount = 0;
                while (workingDaysCount == 0)
                {
                    prevMonday = prevMonday.AddDays(-7);
                    workingDaysCount = calendarDt.AsEnumerable()
                    .Where(row =>
                        row.Field<DateTime>("YMD") >= prevMonday &&
                        row.Field<DateTime>("YMD") <= prevMonday.AddDays(5))
                    .Count();
                }

                // 稼働日ベースでの来週の月曜日をカレンダーテーブルから取得
                DateTime nextMonday = thisMonday;
                workingDaysCount = 0;
                while (workingDaysCount == 0)
                {
                    nextMonday = nextMonday.AddDays(7);
                    workingDaysCount = calendarDt.AsEnumerable()
                    .Where(row =>
                        row.Field<DateTime>("YMD") >= nextMonday &&
                        row.Field<DateTime>("YMD") <= nextMonday.AddDays(5))
                    .Count();
                }

                DateTime[] from = new DateTime[3];
                DateTime[] to = new DateTime[3];
                from[0] = prevMonday;
                from[1] = thisMonday;
                from[2] = nextMonday;
                to[0] = prevMonday.AddDays(5);
                to[1] = thisMonday.AddDays(5);
                to[2] = nextMonday.AddDays(5);

                string sql = string.Empty;
                string mpSchema = cmn.DbCd[Common.DB_CONFIG_MP].Schema;
                int countInsert = 0;
                int countDelete = 0;

                // ①前回分削除
                sql = "delete from " + mpSchema + ".kd8510 where EDDT between " +
                    $"'{from[0]}' and '{to[2]}'";
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    countDelete = myCmd.ExecuteNonQuery();
                }
                for (int i = 0; i < 3; i++)
                {
                    // ②SW工程（週の段取り回数合計を手配日で割って取得するパターン）
                    sql = "insert into " + mpSchema + ".kd8510 " +
                        // サブクエリで週合計段取り回数を日当たりで割った回数を取得
                        "with w as " +
                        "(" +
                            "select MCGCD, MCCD, truncate(count(distinct MATESIZE) / count(distinct EDDT), 2) as SETUPNUM " +
                            "from " + mpSchema + ".kd8450 a " +
                            "inner join " + mpSchema + ".km8430 m30 on m30.HMCD=a.HMCD " +
                            "where a.ODRSTS <> '9' " +
                                $"and a.EDDT between '{from[i]}' and '{to[i]}' " +
                                "and concat(a.MCGCD,'-',a.MCCD) in ('SW-SW') " +
                            "group by a.MCGCD, a.MCCD" +
                        ")" +
                        // 本体クエリで日ごとの稼働明細を集計したレコード
                        "select z.EDDT, z.MCGCD, z.MCCD, m20.KTNKBN" +
                            ", count(z.HMCD) as アイテム数" +
                            ", sum(z.ODRQTY) as 注文本数" +
                            ", sum(z.OT) as 稼働時間" +
                            ", round(sum(z.OT) / 3600, 2) as 稼働時間h" +
                            ", w.SETUPNUM as 段取り回数" +
                            ", w.SETUPNUM * m20.SETUPTM2 as 段取り時間" +
                            ", round(w.SETUPNUM * m20.SETUPTM2 / 3600, 2) as 段取り時間h" +
                            ",'YAKAN' as 登録者" +
                            ", now() as 登録日時 " +
                        "from " +
                        "(" +
                            // サブクエリで稼働明細を取得
                            "select a.EDDT, a.MCGCD, a.MCCD, a.HMCD, m30.MATESIZE, sum(a.ODRQTY) as ODRQTY" +
                                ",case " +
                                    "when a.MCGCD=m30.KT1MCGCD and a.MCCD=m30.KT1MCCD then sum(a.ODRQTY) * ifnull(m30.KT1CT,0)" +
                                    "when a.MCGCD=m30.KT2MCGCD and a.MCCD=m30.KT2MCCD then sum(a.ODRQTY) * ifnull(m30.KT2CT,0)" +
                                    "when a.MCGCD=m30.KT3MCGCD and a.MCCD=m30.KT3MCCD then sum(a.ODRQTY) * ifnull(m30.KT3CT,0)" +
                                    "when a.MCGCD=m30.KT4MCGCD and a.MCCD=m30.KT4MCCD then sum(a.ODRQTY) * ifnull(m30.KT4CT,0)" +
                                    "when a.MCGCD=m30.KT5MCGCD and a.MCCD=m30.KT5MCCD then sum(a.ODRQTY) * ifnull(m30.KT5CT,0)" +
                                    "when a.MCGCD=m30.KT6MCGCD and a.MCCD=m30.KT6MCCD then sum(a.ODRQTY) * ifnull(m30.KT6CT,0)" +
                                    "else 0 " +
                                "end as OT " +
                            "from " + mpSchema + ".kd8450 a " +
                            "inner join " + mpSchema + ".km8430 m30 on m30.HMCD=a.HMCD " +
                            "where a.ODRSTS <> '9' " +
                                $"and a.EDDT between '{from[i]}' and '{to[i]}' " +
                                "and concat(a.MCGCD,'-',a.MCCD) in ('SW-SW') " +
                            "group by a.EDDT,a.MCGCD,a.MCCD,a.HMCD" +
                        ") z, " + mpSchema + ".km8420 m20, w " +
                        "where z.MCGCD=m20.MCGCD and z.MCCD=m20.MCCD and w.MCGCD=z.MCGCD and w.MCCD=z.MCCD " +
                        "group by z.EDDT, z.MCGCD, z.MCCD, w.SETUPNUM, m20.KTNKBN, m20.SETUPTM2 " +
                        "order by z.EDDT, z.MCGCD, z.MCCD"
                    ;
                    using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                    {
                        countInsert = myCmd.ExecuteNonQuery();
                    }
                    // ③SW工程以外
                    sql = "insert into " + mpSchema + ".kd8510 " +
                        // 本体クエリで日ごとの稼働明細を集計したレコード
                        "select z.EDDT, z.MCGCD, z.MCCD, m20.KTNKBN" +
                            ", count(z.HMCD) as アイテム数" +
                            ", sum(z.ODRQTY) as 注文本数" +
                            ", case when z.MCCD='S500' then sum(z.OT) / 2 else sum(z.OT) end as 稼働時間" +
                            ", case when z.MCCD='S500' then round(sum(z.OT) / 2 / 3600, 2) else round(sum(z.OT) / 3600, 2) end as 稼働時間h" +
                            ", case when z.MCCD='S500' then count(distinct z.MATESIZE) / 2 else count(distinct z.MATESIZE) end as 段取り回数" +
                            ", case when z.MCCD='S500' then count(distinct z.MATESIZE) / 2 * m20.SETUPTM1 else count(distinct z.MATESIZE) * m20.SETUPTM1 end as 段取り時間" +
                            ", case when z.MCCD='S500' then round(count(distinct z.MATESIZE) / 2 * m20.SETUPTM1 / 3600, 2) else round(count(distinct z.MATESIZE) * m20.SETUPTM1 / 3600, 2) end as 段取り時間h" +
                            ",'YAKAN' as 登録者" +
                            ", now() as 登録日時 " +
                        "from " +
                        "(" +
                            // サブクエリで稼働明細を取得
                            "select a.EDDT, a.MCGCD, a.MCCD, a.HMCD, m30.MATESIZE, sum(a.ODRQTY) as ODRQTY" +
                                ",case " +
                                    "when a.MCGCD=m30.KT1MCGCD and a.MCCD=m30.KT1MCCD then sum(a.ODRQTY) * ifnull(m30.KT1CT,0)" +
                                    "when a.MCGCD=m30.KT2MCGCD and a.MCCD=m30.KT2MCCD then sum(a.ODRQTY) * ifnull(m30.KT2CT,0)" +
                                    "when a.MCGCD=m30.KT3MCGCD and a.MCCD=m30.KT3MCCD then sum(a.ODRQTY) * ifnull(m30.KT3CT,0)" +
                                    "when a.MCGCD=m30.KT4MCGCD and a.MCCD=m30.KT4MCCD then sum(a.ODRQTY) * ifnull(m30.KT4CT,0)" +
                                    "when a.MCGCD=m30.KT5MCGCD and a.MCCD=m30.KT5MCCD then sum(a.ODRQTY) * ifnull(m30.KT5CT,0)" +
                                    "when a.MCGCD=m30.KT6MCGCD and a.MCCD=m30.KT6MCCD then sum(a.ODRQTY) * ifnull(m30.KT6CT,0)" +
                                    "else 0 " +
                                "end as OT " +
                            "from " + mpSchema + ".kd8450 a " +
                            "inner join " + mpSchema + ".km8430 m30 on m30.HMCD=a.HMCD " +
                            "where a.ODRSTS <> '9' " +
                                $"and a.EDDT between '{from[i]}' and '{to[i]}' " +
                                "and concat(a.MCGCD,'-',a.MCCD) in " +
                                "(" +
                                    "'NC-4','NC-5','NC-6','NC-7','NC-8'," +
                                    "'MC-3B','MC-3F','MC-CL','3BP-3BI','3BP-3BP','ON-S500'," +
                                    "'SS-SS','XT-XT','CN-CN1','CN-CN2','CN-CN3','CN-CN4'," +
                                    "'MS-1','MS-2','MS-3','MS-4','MS-5','MS-6','SK-SK2','TN-TN'" +
                                ") " +
                            "group by a.EDDT,a.MCGCD,a.MCCD,a.HMCD" +
                        ") z, " + mpSchema + ".km8420 m20 " +
                        "where z.MCGCD=m20.MCGCD and z.MCCD=m20.MCCD " +
                        "group by z.EDDT, z.MCGCD, z.MCCD, m20.KTNKBN, m20.SETUPTM1, m20.SETUPTM2 " +
                        "order by z.EDDT, z.MCGCD, z.MCCD"
                    ;
                    using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                    {
                        countInsert += myCmd.ExecuteNonQuery();
                    }
                    if (countInsert > 0)
                    {
                        Console.WriteLine($"[{from[i]}] {countInsert.ToString("#,0")}件 登録しました．");
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // 接続を閉じる
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return ret;
        }



    }
}
