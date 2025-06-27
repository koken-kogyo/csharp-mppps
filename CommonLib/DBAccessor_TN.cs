using Oracle.ManagedDataAccess.Client;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace MPPPS
{
    /// <summary>
	/// データベース アクセス クラス (タナコンシステム)
    /// </summary>
    public partial class DBAccessor
    {
        /// <summary>
        /// タナコン在庫情報取得 (TLOC_STOCK)
        /// </summary>
        /// <param name="dataSet">データセット</param>
        /// <returns>結果 (0≦: 成功 (件数), 0≧: 失敗)</returns>
        public int GetTLOCSTOCK(ref System.Data.DataTable dataTable, string searchhmcd)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret;
            OracleConnection cnn = null;

            try
            {
                // タナコンサーバー データベースへ接続
                cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_TN, ref cnn);

                // マスタが存在するか先にチェック
                System.Data.DataTable chkDt = new System.Data.DataTable();
                string chk = $"select * from TITEM_MST where ITEM_CODE like '{searchhmcd}'";
                using (OracleCommand myCmd = new OracleCommand(chk, cnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        myDa.Fill(chkDt);
                    }
                }
                // タナコン在庫数のチェック
                if (chkDt.Rows.Count > 0 || searchhmcd.StartsWith("Initial Read"))
                {
                    // 条件式の判定
                    string condition = (searchhmcd.Contains("%")) ? "like" : "=";

                    // タナコンサーバーの品番検索
                    string sql = "select " +
                        "a.ITEM_CODE as 品番 " +
                        ", a.LOC_NO as ロケーション " +
                        ", a.STCK_QTY as 出庫可能数 " +
                        ", TO_DATE(a.IPGO_YMD, 'YYYYMMDD') as 在庫入庫日 " +
                        ", b.ITEM_NAME as 品名 " +
                        "from TLOC_STCK a, TITEM_MST b " +
                        $"where a.ITEM_CODE = b.ITEM_CODE and a.ITEM_CODE {condition} '{searchhmcd}' " +
                        "order by a.ITEM_CODE, a.IPGO_YMD"
                    ;
                    using (OracleCommand myCmd = new OracleCommand(sql, cnn))
                    {
                        using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                        {
                            // 結果取得
                            myDa.Fill(dataTable);
                        }
                    }
                    ret = dataTable.Rows.Count;
                }
                else
                {
                    ret = -9;
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
            finally
            {
                // 接続を閉じる
                cmn.Dbm.CloseOraSchema(cnn);
            }

            return ret;

        }

    }
}
