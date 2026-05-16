using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
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

        private readonly string vmst = @"
            with vmst as 
            (
            select 1 as SEQ, KT1MCGCD as MCGCD, KT1MCCD as MCCD, ifnull(KT1CT,0) as CT, HMCD, MATESIZE, LENGTH, KTSU from km8430 where KT1MCGCD is not null and KT1MCGCD <> '' and KT1MCGCD <> 'EX'
            union
            select 2 as SEQ, KT2MCGCD as MCGCD, KT2MCCD as MCCD, ifnull(KT2CT,0) as CT, HMCD, MATESIZE, LENGTH, KTSU from km8430 where KT2MCGCD is not null and KT2MCGCD <> '' and KT2MCGCD <> 'EX'
            union
            select 3 as SEQ, KT3MCGCD as MCGCD, KT3MCCD as MCCD, ifnull(KT3CT,0) as CT, HMCD, MATESIZE, LENGTH, KTSU from km8430 where KT3MCGCD is not null and KT3MCGCD <> '' and KT3MCGCD <> 'EX'
            union
            select 4 as SEQ, KT4MCGCD as MCGCD, KT4MCCD as MCCD, ifnull(KT4CT,0) as CT, HMCD, MATESIZE, LENGTH, KTSU from km8430 where KT4MCGCD is not null and KT4MCGCD <> '' and KT4MCGCD <> 'EX'
            union
            select 5 as SEQ, KT5MCGCD as MCGCD, KT5MCCD as MCCD, ifnull(KT5CT,0) as CT, HMCD, MATESIZE, LENGTH, KTSU from km8430 where KT5MCGCD is not null and KT5MCGCD <> '' and KT5MCGCD <> 'EX'
            union
            select 6 as SEQ, KT6MCGCD as MCGCD, KT6MCCD as MCCD, ifnull(KT6CT,0) as CT, HMCD, MATESIZE, LENGTH, KTSU from km8430 where KT6MCGCD is not null and KT6MCGCD <> '' and KT6MCGCD <> 'EX'
            )
        ";

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

                string sql = vmst +
                    "SELECT a.ODRNO, a.HMCD, a.EDDT, a.ODRQTY, m.MATESIZE, m.CT, m.LENGTH, m.KTSU "
                    + "FROM "
                    + cmn.DbCd[Common.DB_CONFIG_MP].Schema + "." + Common.TABLE_ID_KD8450 + " a "
                    + "INNER JOIN vmst m on "
                        + "m.HMCD = a.HMCD and m.MCGCD = a.MCGCD "
                    + "WHERE "
                    + "a.MCGCD = 'XT' and a.MCCD = 'XT2' "
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


        /// <summary>
        /// D0470切削母材情報をサマリーしてマスターとして使用
        /// </summary>
        public Dictionary<string, decimal> GetD0470Dictionary()
        {
            var dict = new Dictionary<string, decimal>();
            OracleConnection cnn = null;
            cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref cnn);
            string sql = "select HMCD,SOLEN from D0470 group by HMCD,SOLEN order by HMCD";
            using (var cmd = cnn.CreateCommand())
            {
                cmd.CommandText = sql;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string hmcd = reader.GetString(0);
                        decimal solen = reader.IsDBNull(1) ? 0m : reader.GetDecimal(1);
                        if (!dict.ContainsKey(hmcd)) dict.Add(hmcd, solen);
                    }
                }
            }
            cmn.Dbm.CloseOraSchema(cnn);
            return dict;
        }





    }
}
