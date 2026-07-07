using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace MPPPS
{
    /// <summary>
	/// データベース アクセス クラス (切削MS工程が主に使用する)
    /// </summary>
    public partial class DBAccessor
    {
        /// <summary>
        /// 対象設備の品目リストを取得する
        /// </summary>
        public string GetHMCDListFromMySQL(string mcgcd, string mccd)
        {
            string ret = string.Empty;
            MySqlConnection mpCnn = null;
            // MPデータベースへ接続
            cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);
            /*
             * 本当はこの方法で取得したいが途中でデータが切れてしまった！
            string sql = $@"
                SELECT
                    CONCAT('(', GROUP_CONCAT(CONCAT('''', hmcd, '''') SEPARATOR ','), ')') AS HMCDLIST,
                    COUNT(*) AS 件数
                FROM km8430
                WHERE ktkey LIKE '%{mcgcd}-{mccd}%';
            ";
            */
            string sql = $"SELECT hmcd FROM km8430 WHERE ktkey LIKE '%{mcgcd}-{mccd}%'";
            using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
            {
                List<string> hmcdList = new List<string>();

                using (MySqlDataReader reader = myCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        hmcdList.Add(reader.GetString(0));
                    }
                }
                string joined = "(" + string.Join(",", hmcdList.Select(x => $"'{x}'")) + ")";
                ret = joined;
            }
            // 接続を閉じる
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return ret;
        }

        /// <summary>
        /// V_BOM_LEAF_PARENT情報から内示と確定情報を取得する
        /// （ピボット前の羅列データ）
        /// </summary>
        public bool おかむーリストデータ取得(DataTable dt, string hmcdlist)
        {
            bool ret = false;
            string sql = string.Empty;
            OracleConnection emCnn = null;
            cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref emCnn);
            try
            {
                sql = $@"
                with vbom as
                (
                    select * from V_BOM_LEAF_PARENT where KOHMCD in {hmcdlist}
                )
                select v.KOHMCD 切削品番
                    , '内示' 区分
                    , case when min(HMCD) is null then 'なし' else LISTAGG(distinct v.OYAHMCD, ',') WITHIN GROUP (ORDER BY v.OYAHMCD) end as 注文品番
                    , trunc(JUDT, 'MM') 集計月
                    , sum(JUQTY*原単位) 注文数
                from vbom v
                    left outer join D0030 on HMCD=v.OYAHMCD and JUDT between ADD_MONTHS(TRUNC(SYSDATE, 'YYYY'), -12) and  ADD_MONTHS(TRUNC(SYSDATE, 'YYYY'), 24) - 1
                group by v.KOHMCD, trunc(JUDT, 'MM')
                union all
                select v.KOHMCD 切削品番
                    , '手配' 区分
                    , case when min(HMCD) is null then 'なし' else LISTAGG(distinct v.OYAHMCD, ',') WITHIN GROUP (ORDER BY v.OYAHMCD) end as 注文品番
                    , trunc(JUDT, 'MM') 集計月
                    , sum(JUQTY*原単位) 注文数
                from vbom v
                    left outer join D0010 on HMCD=v.OYAHMCD and JUDT between ADD_MONTHS(TRUNC(SYSDATE, 'YYYY'), -12) and  ADD_MONTHS(TRUNC(SYSDATE, 'YYYY'), 24) - 1 and ODRSTS<>'4' -- 4:中止
                group by v.KOHMCD, trunc(JUDT, 'MM')
                order by 切削品番
                ";
                using (OracleCommand myCmd = new OracleCommand(sql, emCnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        myDa.Fill(dt);
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"おかむーリストデータ取得でエラーが発生しました。\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ret = false;
            }
            finally
            {
                cmn.Dbm.CloseOraSchema(emCnn);
            }
            return ret;
        }





    }



    /// <summary>
	/// ファイル アクセス クラス (切削MS工程が主に使用する)
    /// </summary>
    public partial class FileAccessor
    {



    }

}
