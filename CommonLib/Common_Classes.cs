using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MPPPS
{
    /// <summary>
    /// アセンブリ状態クラス
    /// </summary>
    public static class AssemblyState
    {
        public const bool IsDebug =
    #if DEBUG
        true;
    #else
       false;
    #endif
    }

    /// <summary>
    /// データベース設定データ クラス
    /// </summary>
    public class DBConfigData
    {
        // プロパティ
        public string User { get; set; }        // ユーザー ID
        public string EncPasswd { get; set; }   // 暗号化パスワード ([KCM002SF] パスワード暗号化アプリ で暗号化した文字列)
        public string Protocol { get; set; }    // 通信プロトコル
        public string Host { get; set; }        // ホスト名または IPv4 アドレス
        public int Port { get; set; }           // ポート番号
        public string ServiceName { get; set; } // サービス名
        public string Schema { get; set; }      // スキーマ名
        public string CharSet { get; set; }     // 文字セット
    }

    /// <summary>
    /// ファイル システム設定データ クラス
    /// </summary>
    public class FSConfigData
    {
        // プロパティ
        public string HostName { get; set; }    // ホスト名
        public string IpAddr { get; set; }      // IPv4 アドレス
        public string UserId { get; set; }      // ＜ホスト名＞\＜ローカル アカウント名＞
        public string EncPasswd { get; set; }   // [KCM002SF] パスワード暗号化アプリで暗号化したパスワード
        public string ShareName { get; set; }   // ファイル保存先の直近共有フォルダー
        public string RootPath { get; set; }    // ファイル保存先へのフルパス
        public string FileName { get; set; }    // ファイル名
        public bool VisibleExcel { get; set; }  // Excel 可視 (出力専用) ("": 未設定, false: いいえ, true: はい (動作遅い))
        public bool CreateSubDir { get; set; }  // サブ ディレクトリ生成 (出力専用) ("": 未設定, false: いいえ, true: はい)
    }

    /// <summary>
    /// COMMON 共通データ レコード クラス
    /// </summary>
    public class DrCommon
    {
        public string InstID { get; set; }      // 登録者
        public string InstDT { get; set; }      // 登録日時
        public string UpdtID { get; set; }      // 更新者
        public string UpdtDT { get; set; }      // 更新日時
    }


    /// <summary>
    // データグリッドビューヘッダー クラス
    /// </summary>
    public class MultipleValues
    {
        public string JPNAME { get; set; }
        public int Width { get; set; }
        public DataGridViewContentAlignment StyleAlignment { get; set; }
        public string Format { get; set; }      // 例："#,0"; "yyyy/MM/dd HH:mm:ss";
    }


    /// <summary>
    /// 受注 クラス
    /// DataGridViewのCellに登録する受注(#052で使用)
    /// </summary>
    public class Order
    {
        public int Qty { get; set; }
        public string OrderNo { get; set; }
        public string Note { get; set; }
        public string EditStatus { get; set; }

        public Order()
        {
            Qty = 0;
            OrderNo = Common.NEWORDER;
            Note = "";
            EditStatus = "";
        }
        public Order(Order order)
        {
            Qty = (order != null) ? order.Qty : 0;
            OrderNo = (order != null) ? order.OrderNo : Common.NEWORDER;
            Note = (order != null) ? order.Note : "";
            EditStatus = (order != null) ? order.EditStatus : "";
        }
        public Order(int qty, string orderNo, string note, string editstatus)
        {
            Qty = qty;
            OrderNo = orderNo;
            Note = note;
            EditStatus = editstatus;
        }
        // データグリッド上の表示は数量だけ
        public override string ToString()
        {
            return Qty.ToString();
        }
    }

    /// <summary>
    /// KS0010 ホストマスタ 主キー クラス
    /// </summary>
    public class PkKS0010
    {
        public string HostNm { get; set; }       // ホスト名
    }

    /// <summary>
    /// KM8400 切削生産計画システム利用者マスター 主キー クラス
    /// </summary>
    public class PkKM8400
    {
        public string UserId { get; set; }       // 利用者コード
    }

    /// <summary>
    /// KM8400 切削生産計画システム利用者マスター データレコード クラス
    /// </summary>
    public class DrKM8400
    {
        public string Active { get; set; }      // 有効フラグ
        public string AuthLv { get; set; }      // 権限レベル
    }

    /// <summary>
    /// M0010 担当者マスター 主キー クラス
    /// </summary>
    public class PkM0010
    {
        public string TanCd { get; set; }       // 担当者コード
    }
    
    /// <summary>
    /// M0010 担当者マスター 検索キー クラス
    /// </summary>
    public class IkM0010
    {
        public string TanCd { get; set; }       // 担当者コード
        public string Passwd { get; set; }      // パスワード
    }


    /// <summary>
    /// 利用者情報
    /// </summary>
    public class UserInfo
    {
        // 変数
        public string UserId;       // ユーザー ID
        public string Passwd;       // パスワード
        public string UserName;     // ユーザー名称
        public string AtgCd;        // EM 権限グループ コード
        public string Active;       // 切削生産計画システム有効フラグ
        public string AuthLv;       // 切削生産計画システム権限レベル
        public bool MemAuthInfo;    // 認証情報記憶

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UserInfo()
        {
            UserId = "";
            Passwd = "";
            UserName = "";
            AtgCd = "";
            Active = "";
            AuthLv = "";
            MemAuthInfo = false;
        }
    }

    /// <summary>
    /// 【C#】DataGridView で行コピーペーストを実装する
    /// https://ktts.hatenablog.com/entry/2020/03/31/235559
    /// </summary>
    public class ClipboardUtils
    {
        public static void OnDataGridViewPaste(object grid, KeyEventArgs e)
        {
            if ((e.Shift && e.KeyCode == Keys.Insert) || (e.Control && e.KeyCode == Keys.V))
            {
                PasteTSV((DataGridView)grid);
            }
        }

        public static void PasteTSV(DataGridView grid)
        {
            char[] rowSplitter = { '\r', '\n' };
            char[] columnSplitter = { '\t' };

            // クリップボードからテキストを取得する
            IDataObject dataInClipboard = Clipboard.GetDataObject();
            string stringInClipboard = (string)dataInClipboard.GetData(DataFormats.Text);

            // グリッドで分割する
            string[] rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);

            // ペースト先のセル番地を取得する
            int r = grid.SelectedCells[0].RowIndex;
            int c = grid.SelectedCells[0].ColumnIndex;

            //// コピペで行数が不足している場合は、行追加する
            //if (grid.Rows.Count - (grid.AllowUserToAddRows ? 1 : 0) < (r + rowsInClipboard.Length))
            //{
            //    grid.Rows.Add(r + rowsInClipboard.Length - grid.Rows.Count + (grid.AllowUserToAddRows ? 1 : 0));
            //}

            // コピー行を列挙する
            for (int iRow = 0; iRow < rowsInClipboard.Length; iRow++)
            {
                // Split row into cell values
                List<string> valuesInRow = rowsInClipboard[iRow].Split(columnSplitter).ToList();

                // コピーしている列数がグリッドの列数を超える場合は、先頭から削除する（RowHeadersVisible表示時）
                while (valuesInRow.Count > grid.ColumnCount)
                {
                    valuesInRow.RemoveAt(0);
                }

                // コピーセルを列挙する
                for (int iCol = 0; iCol < valuesInRow.Count; iCol++)
                {
                    // グリッドの列数を超えない場合に、対応するセルに値を入力する
                    if (grid.ColumnCount - 1 >= c + iCol)
                    {
                        DataGridViewCell cell = grid.Rows[r + iRow].Cells[c + iCol];

                        if (!cell.ReadOnly)
                        {
                            cell.Value = valuesInRow[iCol];
                        }
                    }
                }
            }
        }
    }

}
