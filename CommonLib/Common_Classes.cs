using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

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
        public string InstID { get; set; }  // 登録者
        public string InstDT { get; set; }  // 登録日時
        public string UpdtID { get; set; }  // 更新者
        public string UpdtDT { get; set; }  // 更新日時
    }

    /// <summary>
    /// KD8430 切削生産計画ファイル 主キー クラス
    /// </summary>
    public class PkKD8430
    {
        public string OdrNo { get; set; }       // 手配 No
        public int McSeq { get; set; }          // 切削工程順序
        public int SplitSeq { get; set; }       // 手配分割 SEQ
    }

    /// <summary>
    /// KD8430 切削生産計画ファイル 検索キー クラス
    /// </summary>
    public class IkKD8430
    {
        public DateTime EdDt { get; set; }      // 完了予定日
        public string McGCd { get; set; }       // グループ コード
        public string McCd { get; set; }        // 設備コード
        public DateTime KEdDt { get; set; }     // 確定完了予定日
        public string KMcCd { get; set; }       // 確定設備コード
    }

    /// <summary>
    /// KD8430 切削生産計画ファイル データレコード クラス
    /// </summary>
    public class DrKD8430
    {
        public string OdrNo { get; set; }       // 手配No
        public int KtSeq { get; set; }       // 工程順序
        public string HmCd { get; set; }        // 品番
        public string KtCd { get; set; }        // 工程ｺｰﾄﾞ
        public int OdrQty { get; set; }      // 手配数
        public string OdCd { get; set; }        // 手配先ｺｰﾄﾞ
        public string NextOdCd { get; set; }    // 次手配先コード
        public int LtTime { get; set; }      // LT
        public DateTime StDt { get; set; }        // 着手予定日
        public string StTim { get; set; }       // 着手予定時刻
        public DateTime EdDt { get; set; }        // 完了予定日
        public string EdTim { get; set; }       // 完了予定時刻
        public string OdrSts { get; set; }      // 手配状況
        public string QrCd { get; set; }        // QRコード
        public int JiQty { get; set; }       // 実績数
        public string DenpyoKbn { get; set; }   // 帳票発行区分
        public DateTime DenpyoDt { get; set; }    // 帳票発行日
        public string Note { get; set; }        // 摘要
        public string WkNote { get; set; }      // 作業内容
        public string WkComment { get; set; }   // 作業注釈
        public string DataKbn { get; set; }     // データ区分
        public string Instid { get; set; }      //  
        public DateTime InstDt { get; set; }      //  
        public string UpdtId { get; set; }      //  
        public DateTime UpdtDt { get; set; }      //  
        public string UkCd { get; set; }        //  
        public string NaigaiKbn { get; set; }   // 社内外区分
        public string RetKtCd { get; set; }     // 検索用工程コード
        public int McSeq { get; set; }       // 切削工程順序
        public int SplitSeq { get; set; }    // 手配分割SEQ
        public string McGCd { get; set; }       // グループコード
        public string McCd { get; set; }        // 設備コード
        public DateTime KEdDt { get; set; }       // 確定完了予定日
        public string KEdTim { get; set; }      // 確定完了予定時刻
        public int KOdrQty { get; set; }     // 確定手配数
        public string KMcCd { get; set; }       // 確定設備コード
        public DateTime WkDtDt { get; set; }      // 段取開始日時
        public DateTime WkStDt { get; set; }      // 作業開始日時
        public DateTime WkEdDt { get; set; }		// 作業完了日時


    }

    /// <summary>
    /// KD8440 切削生産計画日程ファイル ユニーク キー クラス
    /// </summary>
    public class UqKD8440
    {
        public string OdCd { get; set; }        // 手配 先コード
        public string PlnNo { get; set; }       // 計画 No
        public string OdrNo { get; set; }       // 手配 No
        public int KtSeq { get; set; }          // 工程順序
        public int Seq { get; set; }            // SEQ
        public int McSeq { get; set; }          // 切削工程順序
        public int SplitSeq { get; set; }       // 手配分割 SEQ
    }

    /// <summary>
    /// KD8440 切削生産計画日程ファイル 検索キー クラス
    /// </summary>
    public class IkKD8440
    {
        public DateTime EdDt { get; set; }      // 完了予定日
        public string McGCd { get; set; }       // グループ コード
        public string McCd { get; set; }        // 設備コード
        public DateTime KEdDt { get; set; }     // 確定完了予定日
        public string KMcCd { get; set; }       // 確定設備コード
    }

    /// <summary>
    /// KD8440 切削生産計画日程ファイル データレコード クラス
    /// </summary>
    public class DrKD8440
    {
        public string OdCd { get; set; }        // 手配先コード
        public string PlnNo { get; set; }       // 計画No
        public string OdrNo { get; set; }       // 手配NO
        public int KtSeq { get; set; }          // 工程順序
        public string HmCd { get; set; }        // 品番
        public string KtCd { get; set; }        // 工程コード
        public string UkCd { get; set; }        // 受入場所コード
        public string DataKbn { get; set; }       // データ区分
        public string HmStKbn { get; set; }       // 品目指定区分
        public int OdrQty { get; set; }         // 手配数
        public int TrialQty { get; set; }       // 試料数
        public DateTime StDt { get; set; }      // 着手予定日
        public string AtTm { get; set; }        // 着手予定時刻
        public DateTime EdDt { get; set; }      // 完了予定日
        public string EdTm { get; set; }        // 完了予定時刻
        public string KjuNo { get; set; }       // 受注NO
        public string ReasonKbn { get; set; }     // 事由区分
        public string Note { get; set; }        // 摘要
        public string TkCd { get; set; }        // 得意先コード
        public string TkCtlNo { get; set; }     // 得意先管理番号
        public string TkHmCd { get; set; }      // 得意先品番
        public string RqMnNo { get; set; }      // RQ管理NO
        public int RqSeqNo { get; set; }        // RQ順序NO
        public int RqbNo { get; set; }          // RQ分割回数
        public string NextKtCd { get; set; }    // 次工程コード
        public string NextOdCd { get; set; }    // 次手配先コード
        public string DvRqNo { get; set; }      // 分割元RQオーダーNO
        public string OdrSts { get; set; }        // 手配状況
        public int SepDay { get; set; }         // 分割日数
        public string WkNote { get; set; }      // 作業内容
        public string WkComment { get; set; }   // 作業注釈
        public string KtKbn { get; set; }         // 工程区分
        public string KkTkKbn { get; set; }       // 実績有無
        public string SodKbn { get; set; }        // 先行手配区分
        public string NaigaiKbn { get; set; }     // 内外作区分
        public string InstId { get; set; }      //  
        public DateTime InstDt { get; set; }    //  
        public string UpdtId { get; set; }      //  
        public DateTime UpdtDt { get; set; }    //  
        public int JiQty { get; set; }          // 実績数
        public int Seq { get; set; }            // SEQ
        public int McSeq { get; set; }          // 切削工程順序
        public int SplitSeq { get; set; }       // 手配分割SEQ
        public string McGCd { get; set; }       // グループコード
        public string McCd { get; set; }        // 設備コード
        public DateTime KEdDt { get; set; }     // 確定完了予定日
        public string KEdTm { get; set; }       // 確定完了予定時刻
        public int KOdrQty { get; set; }        // 確定手配数
        public string KMcCd { get; set; }       // 確定設備コード
        public DateTime WkDtDt { get; set; }    // 段取開始日時
        public DateTime WkStDt { get; set; }    // 作業開始日時
        public DateTime WkEdDt { get; set; }    // 作業完了日時
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
    /// KM8410 切削刃具 マスター 主キー クラス
    /// </summary>
    public class PkKM8410
    {
        public string TkCd { get; set; }        // 得意先コード
        public string HmCd { get; set; }        // 品番
        public string KtCd { get; set; }        // 工程コード
        public string ToolNo { get; set; }      // 工具番号
        public DateTime ValDtF { get; set; }    // 発行日
        public DateTime ValDtT { get; set; }    // 失効日
    }

    /// <summary>
    /// KM8410 作業グループマスタ データレコード クラス
    /// </summary>
    public class DrKM8410
    {
        public string WkGrNm { get; set; }      // 切削刃具名称
        public string Note { get; set; }        // 備考
    }

    /// <summary>
    /// KM8420 切削設備マスター 主キー クラス
    /// </summary>
    public class PkKM8420
    {
        public int McGSeq { get; set; }        // グループ順序
        public string McGCd { get; set; }      // グループ コード
        public int McSeq { get; set; }         // 設備順序
        public string McCd { get; set; }       // 設備コード
    }

    /// <summary>
    /// KM8420 切削設備マスター データレコード クラス
    /// </summary>
    public class DrKM8420
    {
        public string Work { get; set; }        // 作業内容
        public double SetupTmMP { get; set; }    // 切削設備時間 (量産)
        public double SetupTmSP { get; set; }    // 切削設備時間 (補給部品等)
        public string Note { get; set; }         // 備考
    }

    /// <summary>
    /// KM8430 切削コード票マスター 主キー クラス
    /// </summary>
    public class PkKM8430
    {
        public string OdCd { get; set; }        // 手配先コード
        public string WkGrCd { get; set; }      // 切削刃具 コード
        public string HmCd { get; set; }        // 品番
        public DateTime ValDtFF { get; set; }   // 適用年月日 (From)
        public DateTime ValDtFT { get; set; }   // 適用年月日 (To)
        public int WkSeq { get; set; }          // 作業順序
    }

    /// <summary>
    /// KM8430 切削コード票マスター データレコード クラス
    /// </summary>
    public class DrKM8430
    {
        public double CT { get; set; }           // サイクルタイム
        public string Note { get; set; }         // 備考
    }

    /// <summary>
    /// KM8440 賃率マスター 主キー クラス
    /// </summary>
    public class PkKM8440
    {
        public string OdCd { get; set; }        // 手配先コード
        public string KtCd { get; set; }        // 工程コード
        public DateTime ValDtFF { get; set; }   // 適用年月日 (From)
        public DateTime ValDtFT { get; set; }   // 適用年月日 (To)
        public int KtSeq { get; set; }          // 工程順序
    }

    /// <summary>
    /// KM8440 賃率マスター データレコード クラス
    /// </summary>
    public class DrKM8440
    {
        public string KtNm { get; set; }         // 工程名称
        public string EqClass { get; set; }      // 設備分類
        public string Model { get; set; }        // 機種
        public string Manufacturer { get; set; } // 製造元
        public double OpeCost { get; set; }      // 操業費
        public double LaborCost { get; set; }    // 労務費
        public double EqCost { get; set; }       // 設備費
        public double LaborRate { get; set; }    // 賃率
        public string Note { get; set; }         // 備考
    }

    /// <summary>
    /// KM8450 製造原価マスター 主キー クラス
    /// </summary>
    public class PkKM8450
    {
        public string HmCd { get; set; }        // 品番
        public DateTime ValDtFF { get; set; }   // 適用年月日 (From)
        public DateTime ValDtFT { get; set; }   // 適用年月日 (To)
        public string KtCd { get; set; }        // 工程コード
    }

    /// <summary>
    /// KM8450 製造原価マスター データレコード クラス
    /// </summary>
    public class DrKM8450
    {
        public double PrepWt { get; set; }           // 仕込み重量
        public double ScrapWt { get; set; }          // スクラップ重量
        public double ScrapCost { get; set; }        // スクラップ単価
        public double OSPtsCost { get; set; }        // 外注部品費
        public double OSWages { get; set; }          // 外注工賃
        public double BuySellPtsCost { get; set; }   // 支給部品費(有償)
        public double PurPtsCost { get; set; }       // 購買部品費
        public string Note { get; set; }             // 備考
    }

    /// <summary>
    /// KM8460 原価管理雛形ファイル定義マスター 主キー クラス
    /// </summary>
    public class PkKM8460
    {
        public string FileID { get; set; }      // ファイル ID
        public DateTime RevDtF { get; set; }    // 改定年月日 (From)
        public DateTime RevDtT { get; set; }    // 改定年月日 (To)
        public int BranchNo { get; set; }       // 枝番
    }

    /// <summary>
    /// KM8460 原価管理雛形ファイル定義マスター データレコード クラス
    /// </summary>
    public class DrKM8460
    {
        public string ItemType { get; set; }      // 項目種別
        public string PhysicNm { get; set; }      // 物理名
        public string LogicNm { get; set; }       // 論理名
        public string HlLeft { get; set; }        // 見出し左端
        public string HlRight { get; set; }       // 見出し右端
        public int HlTop { get; set; }            // 見出し上端
        public int HlBottom { get; set; }         // 見出し下端
        public string DtStrConvArg { get; set; }  // データ セル文字列変換引数
        public string DtDispStyle { get; set; }   // データ セル表示形式
        public int DtNumOfDec { get; set; }       // データ セル小数点以下桁数
        public string DtExpDir { get; set; }      // データ セル展開方向
        public string DtOccur { get; set; }       // データ セル反復数
        public int DtExtConNum { get; set; }      // データ セル展開方向結合数
        public string UprHlLeft { get; set; }     // 上位見出し左端
        public int UprHlTop { get; set; }         // 上位見出し上端
        public string Note { get; set; }          // 備考
    }

    /// <summary>
    /// KD5000 原価計算明細ファイル 主キー クラス
    /// </summary>
    public class PkKD5000
    {
        public int DispSeq { get; set; }            // 表示順序
    }

    /// <summary>
    /// KD5000 原価計算明細ファイル データレコード クラス
    /// </summary>
    public class DrKD5000
    {
        public int Num { get; set; }                // NO.
        public string HmCd { get; set; }            // 品番
        public string HmNm { get; set; }            // 品目名称
        public int KoQty { get; set; }              // 標数
        public string ZaiNm { get; set; }           // 材料・材質
        public double Setulen { get; set; }         // 材料・材料寸法
        public string MateSrc { get; set; }         // 材料・取引先
        public string MateNote { get; set; }        // 材料・備考
        public double PrepWt { get; set; }          // 材料・仕込み重量
        public double Weight { get; set; }          // 材料・製品重量
        public double ScrapWt { get; set; }         // 材料・スクラップ重量
        public double HPrice { get; set; }          // 材料・材料単価
        public double ScrapPrice { get; set; }      // 材料・スクラップ単価
        public double MateCost { get; set; }        // 材料・材料費
        public double ScrapCost { get; set; }       // 材料・スクラップ費
        public int KtSeq { get; set; }              // 工程別原価・工順
        public string KtNm { get; set; }            // 工程別原価・工程名
        public string OdrNm { get; set; }           // 工程別原価・取引先/加工区
        public DateTime RevDate { get; set; }       // 工程別原価・変更月
        public double OSPtsCost { get; set; }       // 工程別原価・外注部品費
        public double OSWages { get; set; }         // 工程別原価・外注工賃
        public double BuySellPtsCost { get; set; }  // 工程別原価・支給部品費(有償)
        public double PurPtsCost { get; set; }      // 工程別原価・購買部品費
        public double OSPtsCostAccum { get; set; }  // 工程別原価・積算外注部品費
        public double IhProcCost { get; set; }      // 工程別原価・社内加工・加工費
        public double ManHour { get; set; }         // 工程別原価・社内加工・工数
        public double LaborRate { get; set; }       // 工程別原価・社内加工・賃率
        public double IhProcCostAccum { get; set; } // 工程別原価・積算社内加工費
        public double EqCost { get; set; }          // 社内加工賃率算出明細・賃率・設備費
        public double LaborCost { get; set; }       // 社内加工賃率算出明細・賃率・労務費
        public double OpeCost { get; set; }         // 社内加工賃率算出明細・賃率・操業費
        public double LaborCostTotal { get; set; }  // 社内加工賃率算出明細・賃率・計
        public string Note { get; set; }            // 社内加工賃率算出明細・内容
        public int Layer { get; set; }              // 階層 
        public string KoHmType { get; set; }        // 子品目分類 
        public string KtCd { get; set; }            // 工程コード 
        public string OdCd { get; set; }            // 手配先コード 
        public int RegSeq { get; set; }             // 登録順序 
        public int AccumSeq { get; set; }           // 集計順序
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
    /// M0300 手配先名称マスター 検索キー クラス
    /// </summary>
    public class IkM0300
    {
        public string OdCd { get; set; }            // 手配先コード
        public Char IOKbn { get; set; }             // 社内外区分
        public bool IsInclusionSubCo { get; set; }  // 子会社を含む (0: いいえ, 1: はい)
        public bool IsTiedToKt { get; set; }        // 工程への紐付けあり (0: いいえ, 1: はい)
    }

    /// <summary>
    /// M0410 工程マスター 検索キー クラス
    /// </summary>
    public class IkM0410
    {
        public string KtCd { get; set; }            // 工程コード
        public string OdCd { get; set; }            // 手配先コード
        public Char OdrKbn { get; set; }            // 手配区分
        public bool IsInclusionSubCo { get; set; }  // 子会社を含む
        public bool IsTiedToOd { get; set; }        // 手配先への紐付けあり (0: いいえ, 1: はい)
    }

    /// <summary>
    /// M0500 品目マスター 主キー クラス
    /// </summary>
    public class PkM0500
    {
        public string HmCd { get; set; }       // 品番
    }


    /// <summary>
    /// 生産情報クラス
    /// </summary>
    public class ProductionInfo
    {
        private string[] Values = new string[2]; // 手配先コード接頭辞

        // プロパティ
        public string KtCdPrefix { get; set; } // 工程コード接頭辞
        public string[] OdCdPrefix { get { return Values; } set { Values = value; } } // 手配先コード接頭辞
        public string PlanningDate { get; set; } // 計画日
        public string PlanningDateFrom { get; set; } // 計画日 (From)
        public string PlanningDateTo { get; set; } // 計画日 (To)
    }

    /// <summary>
    /// 計画条件クラス
    /// </summary>
    public class PlanningCondition
    {
        // 変数
        public int ArrIdxBreakKey; // ブレーク キー
        public string SortRegion;  // ソート範囲
        public string KtNm;        // 工程名称

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlanningCondition()
        {
            ArrIdxBreakKey = 0;
            SortRegion = "";
            KtNm = "";
        }
    }

    /// <summary>
    /// ソート キー クラス
    /// </summary>
    public class SortKey
    {
        // 変数
        public string Key;
        public Excel.XlSortOrder Direction;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SortKey()
        {
            Key = "";
            Direction = Excel.XlSortOrder.xlAscending;
        }
    }

    /// <summary>
    /// 採番条件クラス
    /// </summary>
    public class NumberingCondition
    {
        // 変数
        public int ColNumType;       // [計画種別] の列番号
        public int ColNumCond;       // 条件の列番号
        public int ColNumIRepoOdrNo; // [i-Reporter 指示番号] の列番号
        public string KtCdPrefix;    // 工程コード接頭辞
        public string MinCellVacant;    // 空き行列の先頭セル

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NumberingCondition()
        {
            ColNumType = 0;
            ColNumCond = 0;
            ColNumIRepoOdrNo = 0;
            KtCdPrefix = "";
            MinCellVacant = "";
        }
    }

    /// <summary>
    /// 部品表クラス
    /// </summary>
    public class PartsList
    {
        // 変数
        public int SeqNo;     // 順番
        public string PLKtNm; // 部品表工程名称
        public string PLMcNo; // 機械番号
        public int LeadTime;  // 生産リードタイム

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PartsList()
        {
            SeqNo = 0;
            PLKtNm = "";
            PLMcNo = "";
            LeadTime = 0;
        }
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
    /// DataGridView エラー リスト
    /// </summary>
    public class DgvError
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public string ErrorText { get; set; }
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
