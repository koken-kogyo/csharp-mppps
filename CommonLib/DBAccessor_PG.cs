using Npgsql;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace MPPPS
{
    /// <summary>
	/// データベース アクセス クラス (i-Reporter)
    /// </summary>
    public partial class DBAccessor
    {
        /// <summary>
        /// 棚卸情報取得 (帳票定義元ID:470)
        /// </summary>
        /// <param name="dataTable">データセット</param>
        /// <param name="BUCD">棚番号</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int GetTanaInfo(ref DataTable dataTable, string BUCD)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret;
            NpgsqlConnection cnn = null;

            try
            {
                // 当年取得
                int yyyy = DateTime.Now.Year;

                // i-Reporterサーバー データベースへ接続
                cmn.Dbm.IsConnectPostgreSQL(Common.DB_CONFIG_PG, ref cnn);

                // 棚卸(view_report_470)ファイルの検索
                string sql = "select rep_top_id as 帳票ID " +
                    // ", rep_top_name as 帳票名称" + 
                    ", cluster_1_1_t 棚番" +
                    ", cluster_1_4_t 品番" +
                    ", CAST(cluster_1_5_n as INTEGER) 数量" +
                    ", CASE edit_refer_status " + 
                        " WHEN '1' THEN '編集中'" + 
                        " WHEN '4' THEN '入力完了'" +
                        " ELSE edit_refer_status::text " + 
                        "END as 状態" +
                    ", CAST(cluster_1_0_n as INTEGER) 社員番号" +
                    ", DATE_TRUNC('second', sys_regist_time) as 登録日時" +
                    ", DATE_TRUNC('second', sys_update_time) as  更新日時" + 
                    ", CASE " + 
                        " WHEN cluster_1_2_t is not null THEN '1'" + 
                        " WHEN cluster_1_3_t is not null THEN '2'" + 
                        " ELSE '3' " + 
                    "END as 入力区分 " + 
                    "from view_report_470 " + 
                    "where sys_regist_time between " + 
                        $"cast('${yyyy}-01-01' as date) and " + 
                        $"cast('${yyyy}-12-30' as date) + cast('1 days' as INTERVAL) " +
                    $"and cluster_1_1_t like '{BUCD}%' " + 
                    "order by sys_regist_time asc"
                ;
                using (NpgsqlCommand myCmd = new NpgsqlCommand(sql, cnn))
                {
                    using (NpgsqlDataAdapter myDa = new NpgsqlDataAdapter(myCmd))
                    {
                        // 結果取得
                        myDa.Fill(dataTable);
                    }
                }
                ret = dataTable.Rows.Count;

            }
            catch (Exception ex)
            {
                // エラー
                string msg = "Exception Source = " + ex.Source + ", Message = " + ex.Message;
                if (AssemblyState.IsDebug) Debug.WriteLine(msg);
                MessageBox.Show("DBAccessor:\n" + ex.Message);
                Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
                cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_802, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);
                ret = -1;
            }
            finally
            {
                // 接続を閉じる
                cmn.Dbm.ClosePgSchema(cnn);
            }

            return ret;

        }

    }
}
