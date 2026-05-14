using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace MPPPS
{
    /// <summary>
	/// データベース アクセス クラス (切削工程所管テーブル)
    /// </summary>
    public partial class DBAccessor
    {

        /// <summary>
        /// 内示データ(xt2)取得
        /// </summary>
        /// <param name="xt2OrderDt">内示データ</param>
        /// <returns>注文情報データ</returns>
        public bool ReadXT2(ref DataTable xt2OrderDt)
        {
            bool ret;
            MySqlConnection mpCnn = null;

            try
            {
                // MPデータベースへ接続
                cmn.Dbm.IsConnectMySqlSchema(ref mpCnn);

                string sql = "SELECT a.ODRNO, a.HMCD, a.EDDT, a.ODRQTY, m.MATESIZE "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8450 + " a "
                    + "INNER JOIN "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KM8430 + " m "
                    + "ON m.HMCD = a.HMCD "
                    + "WHERE "
                    + "MCGCD = 'XT' and MCCD = 'XT2' "
                    + $"and EDDT between '2026/6/1' and '2026/6/30'"
                ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, mpCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        myDa.Fill(dataTable: xt2OrderDt);
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("内示データの取得に失敗しました。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ret = false;
            }
            cmn.Dbm.CloseMySqlSchema(mpCnn);
            return ret;
        }






    }
}
