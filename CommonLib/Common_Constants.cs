using NLog;
using System.Drawing;

namespace MPPPS
{
    /// <summary>
    /// 共通クラス
    /// </summary>
    public partial class Common
    {

        // ログ マネージャー
        public static readonly Logger s_Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 定数定義
        /// </summary>
        // ファイル パス
        public static readonly string KCM_PGM_ID = "KCM000ZZ";                  // 内製共通プログラム ID
        public static readonly string KEN_PGM_ID = "KMD000ZZ";                  // 製造部プログラム ID
        public static readonly string MY_PGM_ID = "KMD001SF";                   // プログラム ID
        public static readonly string MY_PGM_NAME = "切削生産計画システム";     // プログラム名称
        public static readonly string MY_PGM_VER = "230613.01a";                // プログラム バージョン
        public static readonly string PGM_STS_CD_ALPHA = "a";                   // プログラムの状態 (α版)
        public static readonly string PGM_STS_CD_BETA = "b";                    // プログラムの状態 (β版)
        public static readonly string PGM_STS_TXT_ALPHA = "アルファ版";         // プログラムの状態 (α版)
        public static readonly string PGM_STS_TXT_BETA = "ベータ版";            // プログラムの状態 (β版)
        public static readonly string PATH_DELIMITER = "\\";                    // パスのデリミタ
        public static readonly string ICON_FILE = @".\Koken.ico";               // フォーム アイコン定義
        public static readonly string CONFIG_DIR = "conf";                      // 設定ファイルのあるディレクトリ
        public static readonly string CONFIG_FILE_DB = "ConfigDB.xml";          // データベース設定ファイル
        public static readonly string CONFIG_FILE_FS = "ConfigFS.xml";          // ファイル システム設定ファイル
        public static readonly string OUTBOUND_INFO_FILE = "OutboundInfo.csv";  // スマート棚コン計画予定ファイル
        public static readonly string SOUND_DIR = "sounds";                     // 音声ファイルのあるディレクトリ
        public static readonly string SOUND_FILE_CORRECT = "correct.wav";       // 正答音ファイルのあるディレクトリ
        public static readonly string SOUND_FILE_WRONG = "wrong.wav";           // 誤答音ファイルのあるディレクトリ
        public static readonly string SOUND_FILE_OPENING = "opening.wav";       // iPhone 着信音ファイルのあるディレクトリ
        public static readonly string EXCEL_FILE_EXT = ".xlsx";                 // Excel ファイル拡張子
        public static readonly string CSV_FILE_EXT = @".csv";                   // CSV ファイル拡張子
        public static readonly string APP_EXPLORER = @"EXPLORER.EXE";           // 起動アプリ (エクスプローラー)
        public static readonly string QR_HMCD_IMG = @"\QR_HMCD.png";            // QRコード画像（スマート棚コン用品番）
        public static readonly string QR_ODRNO_IMG = @"\QR_ODRNO.png";          // QRコード画像（アイレポ用注文番号）

        public static readonly string DECIMAL_POINT = ".";                      // 小数点
        public static readonly bool NUMERIC_UNSIGNED = false;                   // 符号なし数値
        public static readonly bool NUMERIC_SIGNED = true;                      // 符号あり数値

        public static readonly int DB_CONFIG_EM = 0;                         // DB 接続定義 (EM)
        public static readonly int DB_CONFIG_KK = 1;                         // DB 接続定義 (内製プログラム)
        public static readonly int DB_CONFIG_MP = 2;                         // DB 接続定義 (切削生産計画システム)


        // デバッグ ログ
        // カテゴリー
        public static readonly string DBG_CAT_CLASS = "Class Name";
        public static readonly string DBG_CAT_METHOD = "Method Name";
        public static readonly string DBG_CAT_MSG = "Debug Message";


        // OpenFileDialog
        public static readonly string OFD_INIT_DIR = @"%USERPROFILE%\Desktop";  // 初期表示させるディレクトリ
        public static readonly string OFD_TITLE_OPEN = "ファイルを開く";        // タイトル (開く)
        public static readonly string OFD_FILE_TYPE_CSV = "CSV ファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*"; // ファイルの種類 (CSV)
        public static readonly string OFD_FILE_TYPE_XLS = "Excel ファイル (*.xlsx; *.xls)|*.xlsx;*.xls|CSV ファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*"; // ファイルの種類 (Excel)
        public static readonly string OFD_FILE_TYPE_MACRO = "Excel Macro ファイル (*.xlsm)|*.xlsm|Excel ファイル (*.xlsx; *.xls)|*.xlsx;*.xls|すべてのファイル (*.*)|*.*"; // ファイルの種類 (Macro)

        // SaveFileDialog
        public static readonly string SFD_INIT_DIR = @"%USERPROFILE%\Desktop";  // 初期表示させるディレクトリ
        public static readonly string SFD_TITLE_SAVE = "名前をつけて保存";      // タイトル (保存)
        public static readonly string SFD_FILE_TYPE_CSV = "CSV ファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*"; // ファイルの種類 (CSV)
        public static readonly string SFD_FILE_TYPE_XLS = "Excel ファイル (*.xlsx; *.xls)|*.xlsx;*.xls|CSV ファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*"; // ファイルの種類 (Excel)
        public const int SFD_RET_SAVE_SUCCEEDED = 1; // 結果コード (1: 保存成功)
        public const int SFD_RET_SAVE_CANCELED = 0;  // 結果コード (0: キャンセル)
        public const int SFD_RET_SAVE_FAILED = -1;   // 結果コード (-1: 保存失敗)
        public const int SFD_RET_AUTH_FAILED = -2;   // 結果コード (-2: 認証失敗)
        public const int SFD_RET_FILE_IN_USE = -3;   // 結果コード (-2: ファイル使用中)

        // フォーム
        public static readonly string FRM_ID_010 = "#010";  // ログイン
        public static readonly string FRM_ID_020 = "#020";  // メイン メニュー
        public static readonly string FRM_ID_030 = "#030";  // マスタ メンテナンス
        public static readonly string FRM_ID_031 = "#031";  // 切削生産計画システム利用者マスタ メンテ
        public static readonly string FRM_ID_032 = "#032";  // 切削刃具マスタ メンテ
        public static readonly string FRM_ID_033 = "#033";  // 切削設備マスタ メンテ
        public static readonly string FRM_ID_034 = "#034";  // 切削コード表マスタ メンテ
        public static readonly string FRM_ID_040 = "#040";  // オーダー管理
        public static readonly string FRM_ID_041 = "#041";  // 手配情報一覧
        public static readonly string FRM_ID_042 = "#042";  // 切削オーダーの平準化
        public static readonly string FRM_ID_043 = "#043";  // 追加オーダーの作成
        public static readonly string FRM_ID_044 = "#044";  // 内示情報一覧
        public static readonly string FRM_ID_045 = "#045";  // 加工進捗情報表示
        public static readonly string FRM_ID_050 = "#050";  // 製造管理
        public static readonly string FRM_ID_051 = "#051";  // 切削オーダー指示書
        public static readonly string FRM_ID_052 = "#052";  // 帳票出力
        public static readonly string FRM_ID_070 = "#070";  // 実績管理
        public static readonly string FRM_ID_071 = "#071";  // 切削ストア受入実績処理
        public static readonly string FRM_ID_072 = "#072";  // 切削ストア受入実績情報表示
        public static readonly string FRM_ID_073 = "#073";  // EM への実績入力
        public static readonly string FRM_ID_080 = "#080";  // 材料管理
        public static readonly string FRM_ID_081 = "#081";  // 材料在庫一覧
        public static readonly string FRM_ID_082 = "#082";  // 材料発注処理
        public static readonly string FRM_ID_083 = "#083";  // 材料検収
        public static readonly string FRM_ID_090 = "#090";  // 切削ストア
        public static readonly string FRM_ID_091 = "#091";  // 切削ストア出庫
        public static readonly string FRM_ID_092 = "#092";  // 切削ストア在庫情報
        public static readonly string FRM_ID_100 = "#100";  // バージョン情報

        public static readonly string FRM_NAME_010 = "ログイン";
        public static readonly string FRM_NAME_020 = "メイン メニュー";
        public static readonly string FRM_NAME_030 = "マスタ メンテナンス";
        public static readonly string FRM_NAME_031 = "切削生産計画システム利用者マスタ メンテ";
        public static readonly string FRM_NAME_032 = "切削刃具マスタ メンテ";
        public static readonly string FRM_NAME_033 = "切削設備マスタ メンテ";
        public static readonly string FRM_NAME_034 = "切削コード表マスタ メンテ";
        public static readonly string FRM_NAME_040 = "オーダー管理";
        public static readonly string FRM_NAME_041 = "手配情報一覧";
        public static readonly string FRM_NAME_042 = "切削オーダーの平準化";
        public static readonly string FRM_NAME_043 = "追加オーダーの作成";
        public static readonly string FRM_NAME_044 = "内示情報一覧";
        public static readonly string FRM_NAME_045 = "加工進捗情報表示";
        public static readonly string FRM_NAME_050 = "製造管理";
        public static readonly string FRM_NAME_051 = "切削オーダー指示書";
        public static readonly string FRM_NAME_052 = "帳票出力";
        public static readonly string FRM_NAME_070 = "実績管理";
        public static readonly string FRM_NAME_071 = "切削ストア受入実績処理";
        public static readonly string FRM_NAME_072 = "切削ストア受入実績情報表示";
        public static readonly string FRM_NAME_073 = "EM への実績入力";
        public static readonly string FRM_NAME_080 = "材料管理";
        public static readonly string FRM_NAME_081 = "材料在庫一覧";
        public static readonly string FRM_NAME_082 = "材料発注処理";
        public static readonly string FRM_NAME_083 = "材料検収";
        public static readonly string FRM_NAME_090 = "切削ストア";
        public static readonly string FRM_NAME_091 = "切削ストア出庫";
        public static readonly string FRM_NAME_092 = "切削ストア在庫情報";
        public static readonly string FRM_NAME_100 = "バージョン情報";

        public static readonly string FRM_BUTTON_TEXT_CLEAR = "クリア";
        public static readonly string FRM_BUTTON_TEXT_CLOSE = "閉じる";
        public static readonly string FRM_BUTTON_TEXT_INSUPD = "登録/更新";
        public static readonly string FRM_BUTTON_TEXT_CONF = "確認";
        public static readonly string FRM_BUTTON_TEXT_DEL = "削除";


        // #020: メイン メニュー
        public static readonly string FRM020_KM8400TMI_TEXT = "KM8400 切削生産計画システム利用者マスタ(&0)";
        public static readonly string FRM020_KM8410TMI_TEXT = "KM8410 切削刃具マスタ(&1)";
        public static readonly string FRM020_KM8420TMI_TEXT = "KM8420 切削設備マスタ(&2)";
        public static readonly string FRM020_KM8430TMI_TEXT = "KM8430 切削コード票マスタ(&3)";


        // #21 切削設備登録画面
        // DataGridView
        //public const int FRM23_DGV_IDX_ODCD      = 0;  // 列インデックス: 手配先コード
        //public const int FRM23_DGV_IDX_WKGRCD    = 1;  // 列インデックス: 切削刃具 コード
        //public const int FRM23_DGV_IDX_HMCD      = 2;  // 列インデックス: 品番
        //public const int FRM23_DGV_IDX_VALDTF    = 3;  // 列インデックス: 適用年月日
        //public const int FRM23_DGV_IDX_WKSEQ     = 4;  // 列インデックス: 作業順序
        //public const int FRM23_DGV_IDX_WORK      = 5;  // 列インデックス: 作業内容
        //public const int FRM23_DGV_IDX_SETUPTMMP = 6;  // 列インデックス: 切削設備時間 (量産)
        //public const int FRM23_DGV_IDX_SETUPTMSP = 7;  // 列インデックス: 切削設備時間 (補給部品等)
        //public const int FRM23_DGV_IDX_NOTE      = 8;  // 列インデックス: 備考
        //public const int FRM23_DGV_IDX_INSTID    = 9;  // 列インデックス: 登録者
        //public const int FRM23_DGV_IDX_INSTDT    = 10; // 列インデックス: 登録日時
        //public const int FRM23_DGV_IDX_UPDTID    = 11; // 列インデックス: 更新者
        //public const int FRM23_DGV_IDX_UPDTDT    = 12; // 列インデックス: 更新日時

        // Color定数
        // #041 手配情報一覧
        // #044 内示情報一覧
        public static readonly Color FRM40_BG_COLOR_CONTROL = SystemColors.Control;
        public static readonly Color FRM40_BG_COLOR_WORKING = Color.White;
        public static readonly Color FRM40_BG_COLOR_HOLIDAY = Color.LightPink;
        public static readonly Color FRM40_BG_COLOR_SATURDAY = Color.LightBlue;
        public static readonly Color FRM40_BG_COLOR_ORDERED = Color.LightCyan;
        public static readonly Color FRM40_BG_COLOR_IMPORTED = Color.MistyRose;
        public static readonly Color FRM40_BG_COLOR_WARNING = Color.LightCoral;
        public static readonly Color FRM40_BG_COLOR_PRINTED = Color.LightGreen;
        public static readonly Color FRM40_BG_COLOR_PLANED = Color.LightGreen;
        public static readonly Color FRM40_BG_COLOR_OTHERMONTH = Color.WhiteSmoke;
        public static readonly Color FRM40_COLOR_BLACK = Color.Black;
        public static readonly Color FRM40_COLOR_DIMGRAY = Color.DimGray;

        // DataGridView 列インデックス
        // #042 切削オーダー平準化画面
        // 検索キー
        public enum Frm042InqKeyIdx
        {
            EdDt,       // 完了予定日
            McGCd,      // グループ コード
            McCd,       // 設備コード
            McOnTime,   // 設備稼働時間
            McSetupTm,  // 設備段取り時間
            EdTim,      // 完了予定時刻
            OdrQty,     // 手配数
            SplitSeq,   // 手配分割 SEQ
        }

        // DataGridView (テーブル列の抽出順と合わせること)

        public enum Frm042DgvIdx
        {
            EdDt,       // 完了予定日
            EdTim,      // 完了予定時刻
            McCd,       // 設備コード
            OdrQty,     // 手配数
            SplitSeq,   // 手配分割 SEQ
            OdCd,       // 手配先コード
            PlnNo,      // 計画No
            OdrNo,      // 手配 No
            KtSeq,      // 工程順序
            McGCd,      // グループ コード
            McOnTime,   // 設備稼働時間
            McSetupTm,  // 設備段取り時間
            HmCd,       // 品番
            Seq,        // SEQ
            McSeq,      // 切削工程順序
            KtCycleTm,  // 工程サイクル タイム
            KtSetupTm,  // 工程段取り時間
            KtOtherTm,  // 工程その他時間
            TableName,  // テーブル名
        }
    

        // DataGridView 列エラー
        public static readonly string FRM042_DGV_ERR_MCGCD = "グループ コードが登録されていません。";
        public static readonly string FRM042_DGV_ERR_MCCD = "設備コードが登録されていません。";
        public static readonly string FRM042_DGV_ERR_HMCD = "品番が登録されていません。";
        public static readonly string FRM042_DGV_ERR_EDDT = "完了予定日が不正です。";
        public static readonly string FRM042_DGV_ERR_ODRQTY = "手配数が不正です。";


        // #23 切削設備登録画面
        public enum Frm23DgvIdx
        {
            OdCd,       // 手配先コード
            WkGrCd,     // 切削刃具 コード
            HmCd,       // 品番
            ValDtF,     // 適用年月日
            WkSeq,      // 作業順序
            Work,       // 作業内容
            SetupTmMP,  // 切削設備時間 (量産)
            SetupTmSP,  // 切削設備時間 (補給部品等)
            Note,       // 備考
            InstID,     // 登録者
            InstDt,     // 登録日時
            UpdtID,     // 更新者
            UpdtDt,     // 更新日時
        }

        // DataGridView 列エラー
        public static readonly string FRM23_DGV_ERR_ODCD = "手配先名称マスタに登録されていません。";                               // 手配先コード
        public static readonly string FRM23_DGV_ERR_WKGRCD = "切削刃具 マスタに登録されていません。";                            // 切削刃具 コード
        public static readonly string FRM23_DGV_ERR_HMCD = "品目マスタに登録されていません。";                                     // 品番
        public static readonly string FRM23_DGV_ERR_VALDTF = "日付が不正です。";                                                     // 適用年月日
        public static readonly string FRM23_DGV_ERR_WKSEQ = "数値が不正です。正数 3 桁以内で入力してください。";                    // 作業順序
        public static readonly string FRM23_DGV_ERR_WORK = "桁数が上限 (半角 100 文字相当) を超えました。";                        // 作業内容
        public static readonly string FRM23_DGV_ERR_SETUPTMMP = "数値が不正です。正数部 4 桁以内＋小数部 2 桁以内で入力してください。"; // 切削設備時間 (量産)
        public static readonly string FRM23_DGV_ERR_SETUPTMSP = "数値が不正です。正数部 4 桁以内＋小数部 2 桁以内で入力してください。"; // 切削設備時間 (補給部品等)
        public static readonly string FRM23_DGV_ERR_NOTE = "桁数が上限 (半角 100 文字相当) を超えました。";                        // 備考
        public static readonly string FRM23_DGV_ERR_ID = "切削生産計画システムまたは EM に登録されていません。";                     // 登録者、更新者
        public static readonly string FRM23_DGV_ERR_DT = "日時が不正です。";                                                     // 登録日時、更新日時

        // #24 切削コード票登録画面
        // DataGridView 列インデックス
        public enum Frm24DgvIdx
        {
            OdCd,     // 手配先コード
            WkGrCd,   // 切削刃具 コード
            HmCd,     // 品番
            ValDtF,   // 適用年月日
            WkSeq,    // 作業順序
            CT,       // サイクルタイム
            Note,     // 備考
            InstID,   // 登録者
            InstDt,   // 登録日時
            UpdtID,   // 更新者
            UpdtDt,   // 更新日時
        }

        // DataGridView 列エラー
        public static readonly string FRM24_DGV_ERR_ODCD = "手配先名称マスタに登録されていません。";                               // 手配先コード
        public static readonly string FRM24_DGV_ERR_WKGRCD = "切削刃具 マスタに登録されていません。";                            // 切削刃具 コード
        public static readonly string FRM24_DGV_ERR_HMCD = "品目マスタに登録されていません。";                                     // 品番
        public static readonly string FRM24_DGV_ERR_VALDTF = "日付が不正です。";                                                     // 適用年月日
        public static readonly string FRM24_DGV_ERR_WKSEQ = "数値が不正です。正数 3 桁以内で入力してください。";                    // 作業順序
        public static readonly string FRM24_DGV_ERR_CT = "数値が不正です。正数部 4 桁以内＋小数部 2 桁以内で入力してください。"; // サイクルタイム
        public static readonly string FRM24_DGV_ERR_NOTE = "桁数が上限 (半角 100 文字相当) を超えました。";                        // 備考
        public static readonly string FRM24_DGV_ERR_ID = "切削生産計画システムまたは EM に登録されていません。";                     // 登録者、更新者
        public static readonly string FRM24_DGV_ERR_DT = "日時が不正です。";                                                     // 登録日時、更新日時

        // #25 賃率登録画面
        // DataGridView 列インデックス
        public enum Frm25DgvIdx
        {
            OdCd,         // 手配先コード
            KtCd,         // 工程コード
            ValDtF,       // 適用年月日
            KtSeq,        // 工程順序
            KtNm,         // 工程名称
            EqClass,      // 設備分類
            Model,        // 機種
            Manufacturer, // 製造元
            OpeCost,      // 操業費
            LaborCost,    // 労務費
            EqCost,       // 設備費
            LaborRate,    // 賃率
            Note,         // 備考
            InstID,       // 登録者
            InstDt,       // 登録日時
            UpdtID,       // 更新者
            UpdtDt,       // 更新日時
        }

        // DataGridView 列エラー
        public static readonly string FRM25_DGV_ERR_ODCD = "手配先名称マスタに登録されていません。";                               // 手配先コード
        public static readonly string FRM25_DGV_ERR_KTCD = "工程マスタに登録されていません。";                                     // 工程コード
        public static readonly string FRM25_DGV_ERR_VALDTF = "日付が不正です。";                                                     // 適用年月日
        public static readonly string FRM25_DGV_ERR_KTSEQ = "数値が不正です。正数 3 桁以内で入力してください。";                    // 工程順序
        public static readonly string FRM25_DGV_ERR_OUTLINE = "桁数が上限 (半角 1000 文字相当) を超えました。";                       // 工程名称、設備分類、機種、製造元
        public static readonly string FRM25_DGV_ERR_COST = "数値が不正です。正数部 2 桁以内＋小数部 3 桁以内で入力してください。"; // 操業費、労務費、設備費
        public static readonly string FRM25_DGV_ERR_RATE = "数値が不正です。正数部 2 桁以内＋小数部 3 桁以内で入力してください。"; // 賃率
        public static readonly string FRM25_DGV_ERR_NOTE = "桁数が上限 (半角 100 文字相当) を超えました。";                        // 備考
        public static readonly string FRM25_DGV_ERR_ID = "切削生産計画システムまたは EM に登録されていません。";                     // 登録者、更新者
        public static readonly string FRM25_DGV_ERR_DT = "日時が不正です。";                                                     // 登録日時、更新日時

        // #26 製造原価登録画面
        // DataGridView 列インデックス
        public enum Frm26DgvIdx
        {
            HmCd,             // 品番
            ValDtF,           // 適用年月日
            KtCd,             // 工程コード
            PrepWt,           // 仕込み重量
            ScrapWt,          // スクラップ重量
            ScrapCost,        // スクラップ単価
            OSPtsCost,        // 外注部品費
            OSWages,          // 外注工賃
            BuySellPtsCost,   // 支給部品費 (有償)
            PurPtsCost,       // 購買部品費
            Note,             // 備考
            InstID,           // 登録者
            InstDt,           // 登録日時
            UpdtID,           // 更新者
            UpdtDt,           // 更新日時
        }

        // DataGridView 列エラー
        public static readonly string FRM26_DGV_ERR_HMCD = "品目マスタに登録されていません。";                                     // 品番
        public static readonly string FRM26_DGV_ERR_VALDTF = "日付が不正です。";                                                     // 適用年月日
        public static readonly string FRM26_DGV_ERR_KTCD = "工程マスタに登録されていません。";                                     // 工程コード
        public static readonly string FRM26_DGV_ERR_WT = "数値が不正です。正数部 3 桁以内＋小数部 2 桁以内で入力してください。"; // 仕込み重量、スクラップ重量
        public static readonly string FRM26_DGV_ERR_COST = "数値が不正です。正数部 7 桁以内＋小数部 2 桁以内で入力してください。"; // スクラップ単価、外注部品費、外注工賃、支給部品費 (有償)、購買部品費
        public static readonly string FRM26_DGV_ERR_NOTE = "桁数が上限 (半角 100 文字相当) を超えました。";                        // 備考
        public static readonly string FRM26_DGV_ERR_ID = "切削生産計画システムまたは EM に登録されていません。";                     // 登録者、更新者
        public static readonly string FRM26_DGV_ERR_DT = "日時が不正です。";                                                     // 登録日時、更新日時




        public static readonly bool DGV_ERR_SET_TEXT = true;    // 行エラー: テキスト設定
        public static readonly bool DGV_ERR_CLEAR_TEXT = false; // 行エラー: テキスト クリア


        // 処理件数配列インデックス
        public static readonly int IDX_NORMAL_END = 0;                   // 正常終了
        public static readonly int IDX_NO_DATA = 1;                   // 該当データなし
        public static readonly int IDX_ABNORMAL_END = 2;                   // 異常終了
        public static readonly string MSG_TXT_INS_NORMAL_END = "登録成功: ";        // 正常終了
        public static readonly string MSG_TXT_INS_ABNORMAL_END = "登録失敗: ";        // 異常終了
        public static readonly string MSG_TXT_UPD_NORMAL_END = "更新成功: ";        // 正常終了
        public static readonly string MSG_TXT_UPD_NO_DATA = "該当データなし: ";  // 該当データなし
        public static readonly string MSG_TXT_UPD_ABNORMAL_END = "更新失敗: ";        // 異常終了
        public static readonly string MSG_TXT_DEL_NORMAL_END = "削除成功: ";        // 正常終了
        public static readonly string MSG_TXT_DEL_NO_DATA = "該当データなし: ";  // 該当データなし
        public static readonly string MSG_TXT_DEL_ABNORMAL_END = "削除失敗: ";        // 異常終了
        public static readonly string MSG_TXT_ROWS = " 件";               // 件数
        public static readonly string MSG_TXT_LF = "\n";                // 改行 (LF)

        // 結果コード
        public const int RET_CD_OK_NUMERICAL_NUMBER = 0;  // 正常 (数値)
        public const int RET_CD_OK_NULL_OR_WHITESPC = 1;  // 正常 (null または空白)
        public const int RET_CD_NG_NOT_NUM = -1;  // 異常 (数値ではない)
        public const int RET_CD_NG_MINUS = -2;  // 異常 (負数)
        public const int RET_CD_NG_FORMAT = -3;  // 異常 (不正な書式)
        public const int RET_CD_NG_OVERFLOW = -4;  // 異常 (桁溢れ)

        public static readonly string RET_TXT_OK_NUMERICAL_NUMBER = "正常 (数値)";
        public static readonly string RET_TXT_OK_NULL_OR_WHITESPC = "正常 (null または空白)";
        public static readonly string RET_TXT_NG_NOT_NUM = "異常 (数値ではない)";
        public static readonly string RET_TXT_NG_MINUS = "異常 (負数)";
        public static readonly string RET_TXT_NG_FORMAT = "異常 (不正な書式)";
        public static readonly string RET_TXT_NG_OVERFLOW = "異常 (桁溢れ)";
        public static readonly string RET_TXT_NG_OTHER = "異常 (その他)";
        // メッセージ有効
        public static readonly bool MSG_ALERT_YES = true;  // メッセージ表示: する
        public static readonly bool MSG_ALERT_NO = false; // メッセージ表示: しない
        // コントロール可視
        public static readonly bool CTL_VISIBLE = true;  // コントロール可視
        public static readonly bool CTL_NON_VISIBLE = false; // コントロール非可視
        // コントロール有効
        public static readonly bool CTL_ENABLED_ENABLE = true;  // コントロール有効
        public static readonly bool CTL_ENABLED_DISABLE = false; // コントロール無効

        // メッセージ種別
        public static readonly string MSG_TYPE_Q = "Q";   // メッセージ種別: 質問
        public static readonly string MSG_TYPE_C = "C";   // メッセージ種別: 確認
        public static readonly string MSG_TYPE_I = "I";   // メッセージ種別: 情報
        public static readonly string MSG_TYPE_W = "W";   // メッセージ種別: 警告
        public static readonly string MSG_TYPE_E = "E";   // メッセージ種別: エラー
        public static readonly string MSG_TYPE_F = "F";   // メッセージ種別: 重大
        // メッセージ コード
        public static readonly string MSG_CD_000 = "000";
        public static readonly string MSG_CD_001 = "001";
        public static readonly string MSG_CD_002 = "002";
        public static readonly string MSG_CD_003 = "003";
        public static readonly string MSG_CD_100 = "100";
        public static readonly string MSG_CD_101 = "101";
        public static readonly string MSG_CD_102 = "102";
        public static readonly string MSG_CD_103 = "103";
        public static readonly string MSG_CD_104 = "104";
        public static readonly string MSG_CD_105 = "105";
        public static readonly string MSG_CD_200 = "200";
        public static readonly string MSG_CD_201 = "201";
        public static readonly string MSG_CD_202 = "202";
        public static readonly string MSG_CD_203 = "203";
        public static readonly string MSG_CD_204 = "204";
        public static readonly string MSG_CD_205 = "205";
        public static readonly string MSG_CD_206 = "206";
        public static readonly string MSG_CD_207 = "207";
        public static readonly string MSG_CD_208 = "208";
        public static readonly string MSG_CD_209 = "209";
        public static readonly string MSG_CD_210 = "210";
        public static readonly string MSG_CD_211 = "211";
        public static readonly string MSG_CD_212 = "212";
        public static readonly string MSG_CD_213 = "213";
        public static readonly string MSG_CD_305 = "305";
        public static readonly string MSG_CD_306 = "306";
        public static readonly string MSG_CD_307 = "307";
        public static readonly string MSG_CD_308 = "308";
        public static readonly string MSG_CD_309 = "309";
        public static readonly string MSG_CD_310 = "310";
        public static readonly string MSG_CD_311 = "311";
        public static readonly string MSG_CD_312 = "312";
        public static readonly string MSG_CD_313 = "313";
        public static readonly string MSG_CD_314 = "314";
        public static readonly string MSG_CD_315 = "315";
        public static readonly string MSG_CD_316 = "316";
        public static readonly string MSG_CD_317 = "317";
        public static readonly string MSG_CD_318 = "318";
        public static readonly string MSG_CD_319 = "319";
        public static readonly string MSG_CD_320 = "320";
        public static readonly string MSG_CD_321 = "321";
        public static readonly string MSG_CD_322 = "322";
        public static readonly string MSG_CD_323 = "323";
        public static readonly string MSG_CD_324 = "324";
        public static readonly string MSG_CD_325 = "325";
        public static readonly string MSG_CD_326 = "326";
        public static readonly string MSG_CD_327 = "327";
        public static readonly string MSG_CD_400 = "400";
        public static readonly string MSG_CD_401 = "401";
        public static readonly string MSG_CD_402 = "402";
        public static readonly string MSG_CD_403 = "403";
        public static readonly string MSG_CD_404 = "404";
        public static readonly string MSG_CD_405 = "405";
        public static readonly string MSG_CD_406 = "406";
        public static readonly string MSG_CD_700 = "700";
        public static readonly string MSG_CD_701 = "701";
        public static readonly string MSG_CD_800 = "800";
        public static readonly string MSG_CD_801 = "801";
        public static readonly string MSG_CD_802 = "802";
        public static readonly string MSG_CD_803 = "803";
        public static readonly string MSG_CD_902 = "902";
        // HResult
        public static readonly string HRESULT_FILE_IN_USE = "80070020"; // ファイル使用中


        // メッセージ タイトル
        public static readonly string MSGBOX_TXT_FATAL = "重大エラー";
        public static readonly string MSGBOX_TXT_ERR = "エラー";
        public static readonly string MSGBOX_TXT_WARN = "警告";
        public static readonly string MSGBOX_TXT_CONF = "確認";
        public static readonly string MSGBOX_TXT_INFO = "情報";
        public static readonly string MEGBOX_TXT_QUESTION = "質問";

        /// <summary>                                                                 
        /// メッセージ                                                                
        /// </summary>                                                                

        // メッセージ コード (メッセージ マスター (KS0040) 検索用)                      
        public static readonly string MSGCD_000 = "000";                                // 変換成功
        public static readonly string MSGCD_001 = "001";                                // 最終確認
        public static readonly string MSGCD_200 = "200";                                // 得意先未選択
        public static readonly string MSGCD_300 = "300";                                // 稼働日未登録
        public static readonly string MSGCD_301 = "301";                                // 得意先コード未登録
        public static readonly string MSGCD_302 = "302";                                // 納入場所コード未登録
        public static readonly string MSGCD_303 = "303";                                // 品番未登録
        public static readonly string MSGCD_304 = "304";                                // EM得意先品番未登録
        public static readonly string MSGCD_305 = "305";                                // 納入場所変換失敗
        public static readonly string MSGCD_306 = "306";                                // 期間未登録
        public static readonly string MSGCD_400 = "400";                                // 注文数不正
        public static readonly string MSGCD_401 = "401";                                // 注番不正
        public static readonly string MSGCD_402 = "402";                                // EDI ファイルなし
        public static readonly string MSGCD_403 = "403";                                // EDI ファイル相違
        public static readonly string MSGCD_404 = "404";                                // EDI ファイル項目数相違
        public static readonly string MSGCD_405 = "405";                                // EDI ファイル未指定
        public static readonly string MSGCD_406 = "406";                                // 標準 CSV ファイルあり
        public static readonly string MSGCD_407 = "407";                                // 設定ファイルなし
        public static readonly string MSGCD_408 = "408";                                // ログ設定ファイルなし
        public static readonly string MSGCD_409 = "409";                                // 設定ファイル相違
        public static readonly string MSGCD_411 = "411";                                // 注文日不正
        public static readonly string MSGCD_412 = "412";                                // 納期不正
        public static readonly string MSGCD_413 = "413";                                // 内確区分不正
        public static readonly string MSGCD_414 = "414";                                // 標準CSVヘッダーファイルなし
        public static readonly string MSGCD_500 = "500";                                // 変換失敗 (Excel→CSV)
        public static readonly string MSGCD_501 = "501";                                // 変換失敗 (CSV→標準 CSV)
        public static readonly string MSGCD_701 = "701";                                // ログ ファイル使用中
        public static readonly string MSGCD_800 = "800";                                // データベース応答タイムアウト
        public static readonly string MSGCD_801 = "801";                                // データベース接続不可
        public static readonly string MSGCD_802 = "802";                                // データベースエラー
        public static readonly string MSGCD_900 = "900";                                // 不明なエラー


        // メッセージ ボックス                                                        
        public static readonly string MSGBOX_BRACKET_L = "【";                          // ブラケット (左)
        public static readonly string MSGBOX_BRACKET_R = "】";                          // ブラケット (右)
        public static readonly string MSGBOX_CAPTION_CONFIRM = "確認";
        public static readonly string MSGBOX_CAPTION_FAILED = "変換失敗";
        public static readonly string MSGBOX_CAPTION_SUCCEEDED = "変換成功";
        public static readonly string MSGBOX_MSGTITLE_USING_LOG_FILE = "ログ ファイル使用中";
        public static readonly string MSGBOX_MSGBODY_USING_LOG_FILE = "ログ ファイル (yyyy-mm-dd.log) が開いています。\n閉じてからやりなおしてください。";
        public const string MSGBOX_TYPE_INFO = "I";                                     // 情報
        public const string MSGBOX_TYPE_QUESTION = "Q";                                 // 質問
        public const string MSGBOX_TYPE_WARNING = "W";                                  // 警告
        public const string MSGBOX_TYPE_ERROR = "E";                                    // エラー
        public const string MSGBOX_TYPE_FATAL = "F";                                    // 重大エラー


        // メッセージ ボックス

        public static readonly string MSG_CD_PREFIX = "【 ";        // エラー表示(前)
        public static readonly string MSG_CD_SUFFIX = " 】";        // エラー表示(前)
        public static readonly string MSG_TITLE_DB_CONN_ERR = "データベース接続不可";
        public static readonly string MSG_BODY_EM_DB_CONN_ERR = "EM データベースに接続できません。\n設定ファイル \"ConfigDB.xml\" の接続文字列を確認してください。";
        public static readonly string MSG_BODY_KK_DB_CONN_ERR = "内製プログラム データベースに接続できません。\n設定ファイル \"ConfigDB.xml\" の接続文字列を確認してください。";
        public static readonly string MSG_BODY_MP_DB_CONN_ERR = "切削生産計画システム データベースに接続できません。\n設定ファイル \"ConfigDB.xml\" の接続文字列を確認してください。";
        public static readonly string MSG_PASTDATE = "過去の日付が選択されています。よろしいですか ?";
        public static readonly string MSG_EXCEL_RUNNING = "Excel ファイルが開いています。閉じてからやりなおしてください。タスク マネージャーの Microsoft Excel プロセスも強制終了させてください。";
        public static readonly string MSG_SELECT_FOLDER = "フォルダを指定してください。\n指定されたフォルダの下に画面で選択された日付のフォルダを作成します。";
        public static readonly string MSG_CONFIRM = "i-Reporter 向けの生産計画を行います。よろしいですか ?";
        public static readonly string MSG_COMPLETED = "i-Reporter 取込用の生産計画ファイルを作成しました。";
        public static readonly string MSG_FAILED = "i-Reporter 取込用生産計画ファイルの作成に失敗しました。";
        public static readonly string MSG_NO_DATA = "指定された条件に合致する手配データがありません。\n生産計画 CSV ファイルは作成されません。";
        public static readonly string MSG_NO_CONFIG_FILE = "設定ファイルが存在しません。";
        public static readonly string MSG_NO_PATTERN_FILE = "帳票定義雛形ファイルが存在しません。";
        public static readonly string MSG_PP_FILE_EXISTS = "生産計画ファイルが既に存在しています。";
        public static readonly string MSG_ORDER_NUM = "設備別手配件数";
        public static readonly string PROGRESS_COMPLETED = "生産計画 CSV ファイルから i-Reporter 帳票を作成してください。";
        public static readonly string MSG_BODY_EXT_STR_CSV_ERROR = "\nCSV: {0} 行目\n理由: 「{1}」が不正です。";
        public static readonly string MSG_BODY_EXT_STR_TABLE_ID = "テーブル名: {0}";

        // ツール ストリップ ステータス ラベル
        public static readonly string TSL_TEXT_CSV_READING = "CSV ファイルを読み込んでいます。完了までお待ちください。";
        public static readonly string TSL_TEXT_TABLE_SELECTING = "テーブル データの検索をしています。完了までお待ちください。";
        public static readonly string TSL_TEXT_TABLE_MERGING = "テーブル データの登録/更新をしています。完了までお待ちください。";
        public static readonly string TSL_TEXT_TABLE_DELETING = "テーブル データの削除をしています。完了までお待ちください。";
        public static readonly string TSL_TEXT_SELECT_TABLE_COUNT = " 件のデータを検索しました。";
        public static readonly string TSL_TEXT_READ_FILE_COUNT = " 件のデータを読み込みました。";
        public static readonly string TSL_TEXT_SAVE_FILE_COUNT = " 件のデータを保存しました。";


        // 処理ステップ
        public static readonly string STEP_01 = "01: 変数設定";
        public static readonly string STEP_02 = "02: ピボットテーブル作成";
        public static readonly string STEP_03 = "03: [完成] シート追加";
        public static readonly string STEP_04 = "04: [完成] シートのデータを加工";
        public static readonly string STEP_05 = "05: 並べ替え";
        public static readonly string STEP_06 = "06: 連番振り";
        public static readonly string STEP_07 = "07: 不要なシートおよび列の削除";
        public static readonly string STEP_08 = "08: データテーブルにコピー";
        public static readonly string STEP_09 = "09: 生産計画一覧 CSV ファイルの基礎DT作成";
        public static readonly string STEP_10 = "10: データ件数チェック";
        public static readonly string STEP_11 = "11: CSV 定義を二次元配列に格納";
        public static readonly string STEP_12 = "12: シートを作って CSV 定義をデータ件数分貼り付け";
        public static readonly string STEP_13 = "13: 定義基礎 DT を二次元配列に格納";
        public static readonly string STEP_14 = "14: シートを作って定義基礎データを全件貼り付け";
        public static readonly string STEP_15 = "15: CSV ファイルに保存";
        public static readonly string STEP_16 = "16: 終了";
        // 登録形式
        public static readonly char REG_FORM_SINGLE_SHEET = 'S';    // 単票形式
        public static readonly char REG_FORM_LIST = 'L';            // リスト形式
        public static readonly string TABP_TEXT_SINGLE_SHEET = "単票形式";
        public static readonly string TABP_TEXT_LIST = "リスト形式";
        // 手配先コード設定状態
        public static readonly int ODCD_SETTING_STATUS_ALL = 0;                 // 全件
        public static readonly int ODCD_SETTING_STATUS_IS_SETUP_KTCD = 1;       // 設定あり
        public static readonly int ODCD_SETTING_STATUS_IS_NOT_SETUP_KTCD = -1;  // 設定なし

        public static readonly string SIGN_EQ = " = ";   // 等しい
        public static readonly string SIGN_NE = " != ";  // 等しくない

        // 処理コード
        public const char OPE_CD_SEL = 'S'; // 検索
        public const char OPE_CD_INS = 'I'; // 登録
        public const char OPE_CD_UPD = 'U'; // 更新
        public const char OPE_CD_DEL = 'D'; // 削除
        // ボタン
        public static readonly string BTN_TXT_READ_CSV_FILE = "CSV 読込";
        public static readonly string BTN_TXT_SAVE_CSV_FILE = "CSV 保存";
        public static readonly bool BTN_ENABLED_DISABLE = false;    // ボタン有効状態: 無効
        public static readonly bool BTN_ENABLED_ENABLE = true;     // ボタン有効状態: 有効
        // タブ コントロール
        public static readonly int TBC1_INDEX_SINGLE_SHEET = 0; // 単票形式
        public static readonly int TBC1_INDEX_LIST = 1; // リスト形式
        public static readonly string Tbp_TXT_SINGLE_SHEET = "単票形式";
        public static readonly string Tbp_TXT_LIST = "リスト形式";
        // ラジオ ボタン
        public static readonly string Rbt_TXT_REGDT = "登録済日付";
        public static readonly string Rbt_TXT_DSNDT = "指定日付";
        // コンボボックス
        public static readonly string STR_IREPOKTCD_BDR = "BDR";                 // 仕切り線
        public static readonly string STR_IREPOKTNM_BDR = "-------------------"; // 仕切り線
        // 判定値
        public static readonly bool FILE_INFO_OK = true;          // ファイル情報: あり
        public static readonly bool FILE_INFO_NG = false;         // ファイル情報: なし
        public static readonly string SORT_ORDER_ASCENDING = "A"; // ソート順 (昇順)
        public static readonly string SORT_ORDER_DECENDING = "D"; // ソート順 (降順)
        // 工程コード接頭辞
        public const string KTCD_PREFIX_CT = "CT";     // 切断工程
        public const string KTCD_PREFIX_BE = "BE";     // ベンダー工程
        public const string KTCD_PREFIX_BETP = "BETP"; // ベンダー工程 (先端末加工)
        public const string KTCD_PREFIX_BE00 = "BE00"; // ベンダー工程 (曲げ)
        public const string KTCD_PREFIX_BEAF = "BEAF"; // ベンダー工程 (後加工)
        public const string KTCD_PREFIX_MP = "MP";     // 切削工程
        // 切断工程
        public static readonly string CT_KTNM = "切断";                // 工程名称
        public static readonly string CT_KTGRP = "CT";                 // 工程グループ
        public static readonly string CT8_KTCD = "CTTC8";              // 工程コード (8 号機)  // [ADD] 2020/01/09 切断 8 号機にてパイロット導入 
        public static readonly string CT_ODCD_PREFIX = "70";           // 手配先コード プレフィックス
        public static readonly string CT8_ODCD = "70008";              // 手配先コード (8 号機)  // [ADD] 2020/01/09 切断 8 号機にてパイロット導入
        public static readonly string CT_CELL_VACANT_MIN = "Y1";       // [完成] シート上の左上の空きセル
        public static readonly string CT_CELL_REGION_ODCD = "E2:E";    // [完成] シート上の [手配先コード] 列範囲
        public static readonly string CT_CELL_REGION_SOOD = "H2:H";    // [完成] シート上の [外径] 列範囲
        public static readonly string CT_CELL_REGION_SOTC = "I2:I";    // [完成] シート上の [厚さ (t)] 列範囲
        public static readonly string CT_CELL_REGION_ZAINM = "G2:G";   // [完成] シート上の [材料名称] の列範囲
        public static readonly string CT_CELL_REGION_SETULEN = "J2:J"; // [完成] シート上の [切断長] の範囲
        public static readonly string CT_ROW_REGION_HEADER = "1:3";    // 見出し行範囲
        public static readonly string CT_SORT_REGION = "A1:W";         // [完成] シート上の並べ替えの範囲
        public static readonly string CT_TABLE_ID_PT1 = "ﾋﾟﾎﾞｯﾄﾃｰﾌﾞﾙ1"; // ピボット テーブル 1 
        public static readonly int CT_COLUMN_NUM_TYPE = 2;             // [完成] シート上の [計画種別] の列番号
        public static readonly int CT_COLUMN_NUM_ODCD = 6;             // [完成] シート上の [手配先コード] の列番号
        public static readonly int CT_COLUMN_NUM_IREPOODRNO = 21;      // [完成] シート上の [i-Reporter手配NO] の列番号
        public static readonly int CT_COLUMN_NUM_OVERTOLERANCE = 18;   // [完成] シート上の [プラス公差] の列番号
        public static readonly int CT_COLUMN_NUM_MINUSTOLERANCE = 19;  // [完成] シート上の [マイナス公差] の列番号
        public static readonly int CT_ITEM_ARRAY_INDEX_MCNO = 4;       // データ テーブル上の [設備名称] の配列インデックス (0 始まり)
        public static readonly int CT_SORT_KEY_NUM = 5;                // ソート キーの数
        public static readonly double CT_DEFAULT_TOLERANCE = 0.5;      // 公差既定値
        // ベンダー工程 (先端末加工)
        public static readonly string BETP_KTNM = "先端末";            // 工程名称
        public static readonly string BETP_ODCD_PREFIX = "6030";       // 手配先コード プレフィックス
        public static readonly string BETP_ODCD_PREFIX2 = "6031";      // 手配先コード プレフィックス2
        public static readonly string BETP_CELL_VACANT_MIN = "W1";     // [完成] シート上の左上の空きセル
        public static readonly string BETP_CELL_REGION_MCNO = "G2:G";  // [完成] シート上の [設備名称] の範囲
        public static readonly string BETP_CELL_REGION_HMCD = "B2:B";  // [完成] シート上の [品番] の範囲
        public static readonly string BETP_CELL_REGION_EDDT = "C2:C";  // [完成] シート上の [完了予定日] の範囲
        public static readonly string BETP_SORT_REGION = "A1:V";       // [完成] シート上の並べ替えの範囲
        public static readonly int BETP_COLUMN_NUM_TYPE = 18;          // [完成] シート上の [計画種別] の列番号
        public static readonly int BETP_COLUMN_NUM_MCNO = 8;           // [完成] シート上の [設備名称] の列番号
        public static readonly int BETP_COLUMN_NUM_IREPOODRNO = 5;     // [完成] シート上の [i-Reporter手配NO] の列番号
        public static readonly int BETP_ITEM_ARRAY_INDEX_MCNO = 6;     // データ テーブル上の [設備名称] の配列インデックス (0 始まり)
        public static readonly int BETP_SORT_KEY_NUM = 3;              // ソート キーの数
        // ベンダー工程 (曲げ)
        public static readonly string BE00_KTNM = "曲げ";              // 工程名称
        public static readonly string BE00_ODCD_PREFIX = "6032";       // 手配先コード プレフィックス
        public static readonly string BE00_COLUMN_JIQTY_4 = "AC";      // [完成] シート上の [W] の列記号
        public static readonly string BE00_CELL_VACANT_MIN = "AB1";    // [完成] シート上の左上の空きセル
        public static readonly string BE00_CELL_REGION_MCNO = "D2:D";  // [完成] シート上の [設備] の範囲
        public static readonly string BE00_CELL_REGION_SOOD = "R2:R";  // [完成] シート上の [外径(Φ)] の範囲
        public static readonly string BE00_CELL_REGION_HQTY = "J2:J";  // [完成] シート上の [加変量(R)] の範囲
        public static readonly string BE00_CELL_REGION_ZAINM = "O2:O"; // [完成] シート上の [材質] の範囲
        public static readonly string BE00_CELL_REGION_SOTC = "T2:T";  // [完成] シート上の [厚さ(t)] の範囲
        public static readonly string BE00_CELL_REGION_KQTY = "M2:M";  // [完成] シート上の [加変数(曲数)] の範囲
        public static readonly string BE00_CELL_REGION_HMCD = "B2:B";  // [完成] シート上の [品番] の範囲
        public static readonly string BE00_SORT_REGION = "A1:Z";       // [完成] シート上の並べ替えの範囲
        public static readonly int BE00_COLUMN_NUM_TYPE = 20;          // [完成] シート上の [計画種別] の列番号
        public static readonly int BE00_COLUMN_NUM_MCNO = 5;           // [完成] シート上の [設備名称] の列番号
        public static readonly int BE00_COLUMN_NUM_IREPOODRNO = 6;     // [完成] シート上の [i-Reporter手配NO] の列番号
        public static readonly int BE00_ITEM_ARRAY_INDEX_MCNO = 3;     // データ テーブル上の [設備名称] の配列インデックス (0 始まり)
        public static readonly int BE00_SORT_KEY_NUM = 7;              // ソート キーの数
        // ベンダー工程 (後加工)
        public static readonly string BEAF_KTNM = "後加工";            // 工程名称
        public static readonly string BEAF_ODCD_PREFIX = "6033";       // 手配先コード プレフィックス
        public static readonly string BEAF_CELL_VACANT_MIN = "W1";     // [完成] シート上の左上の空きセル
        public static readonly string BEAF_CELL_REGION_MCNO = "G2:G";  // [完成] シート上の [設備名称] の範囲
        public static readonly string BEAF_CELL_REGION_HMCD = "B2:B";  // [完成] シート上の [品番] の範囲
        public static readonly string BEAF_CELL_REGION_EDDT = "C2:C";  // [完成] シート上の [完了予定日] の範囲
        public static readonly string BEAF_SORT_REGION = "A1:V";       // [完成] シート上の並べ替えの範囲
        public static readonly int BEAF_COLUMN_NUM_TYPE = 18;          // [完成] シート上の [計画種別] の列番号
        public static readonly int BEAF_COLUMN_NUM_MCNO = 8;           // [完成] シート上の [設備名称] の列番号
        public static readonly int BEAF_COLUMN_NUM_IREPOODRNO = 5;     // [完成] シート上の [i-Reporter手配NO] の列番号
        public static readonly int BEAF_ITEM_ARRAY_INDEX_MCNO = 6;     // データ テーブル上の [設備名称] の配列インデックス (0 始まり)
        public static readonly int BEAF_SORT_KEY_NUM = 3;              // ソート キーの数
        // 切削工程
        public static readonly string MP_KTNM = "切削";                // 工程名称
        public static readonly string MP_CELL_EQUIPCD = "E";           // [完成A] シート上の [設備コード] の列記号
        public static readonly string MP_CELL_MCNO = "G";              // [完成A] シート上の [機械番号] の列記号
        public static readonly string MP_CELL_OPERATOR = "Y";          // [完成A] シート上の [作業者] の列記号
        public static readonly string MP_CELL_VACANT_MIN = "BU1";      // [完成] シート上の左上の空きセル
        public static readonly string MP_CELL_REGION_EQUIPCD = "E2:E"; // [完成] シート上の [設備コード] の範囲
        public static readonly string MP_CELL_REGION_MCNO = "G2:G";    // [完成] シート上の [機械番号] の範囲
        public static readonly string MP_SORT_REGION = "A1:BS";        // [完成] シート上の並べ替えの範囲
        public static readonly string MP_SORT_REGION_FROM_COLUMN = "A";// [完成] シート上の並べ替えの範囲 (開始列)
        public static readonly string MP_SORT_REGION_TO_COLUMN = "BS"; // [完成] シート上の並べ替えの範囲 (終了列)
        public static readonly string MP_EQUIPCD_COLUMN_FROM = "BE";   // [完成] シート上の設備コード列の範囲 (開始列)
        public static readonly string MP_EQUIPCD_COLUMN_TO = "BS";     // [完成] シート上の設備コード列の範囲 (終了列)
        public static readonly string MP_ODCD_PREFIX = "606";       // 手配先コード プレフィックス
        public static readonly string MP_COLUMN_NEXTODCD = "R";        // [完成] シート上の [次手配先コード] の列記号
        public static readonly string MP_EQUIPCD_ON1 = "ON1";          // 設備コード「ON1」
        public static readonly string MP_EQUIPCD_ON3 = "ON3";          // 設備コード「ON3」
        public static readonly int MP_COLUMN_NUM_TYPE = 17;            // [完成] シート上の [計画種別] の列番号
        public static readonly int MP_COLUMN_NUM_EQUIPCD = 6;          // [完成] シート上の [設備コード] の列番号
        public static readonly int MP_COLUMN_NUM_IREPOODRNO = 5;       // [完成] シート上の [i-Reporter手配NO] の列数
        public static readonly int MP_ITEM_ARRAY_INDEX_EQUIPCD = 4;    // データ テーブル上の [設備コード] の配列インデックス (0 始まり)
        public static readonly int MP_ITEM_ARRAY_INDEX_ARRIVALDAY = 23;// データ テーブル上の [材料仕入日] の配列インデックス (0 始まり)
        public static readonly int MP_SORT_KEY_NUM = 2;                // ソート キーの数
        public static readonly int MP_PARTS_LIST_ROW_MAX = 7;          // 部品表の最大行数
        public static readonly int MP_PARTS_LIST_OFFSET_COLUMN = 27;   // 部品表までのオフセット列数
        public static readonly int MP_PARTS_LIST_ITEM_COUNT = 4;       // 部品表の項目数
        public static readonly int MP_EQUIP_NUM = 15;                  // 設備数
        public static readonly int MP_EQUIP_OFFSET_COLUMN = 56;        // 設備までのオフセット列数
        public static readonly int MP_SHEET1_ITEM_NUM = 22;            // シート 1 の項目数
        public static readonly int MP_SHEET2_ITEM_NUM = 31;            // シート 2 の項目数

        // テーブル名称
        // EM (照会のみ)
        public const string TABLE_ID_M0010 = "M0010";    // 担当者マスター
        public const string TABLE_ID_M0200 = "M0200";    // 得意先名称マスター
        public const string TABLE_ID_M0300 = "M0300";    // 手配先名称マスター
        public const string TABLE_ID_M0410 = "M0410";    // 工程マスター
        public const string TABLE_ID_M0500 = "M0500";    // 品目マスター
        public const string TABLE_ID_M0510 = "M0510";    // 品目手順詳細マスター
        public const string TABLE_ID_M0520 = "M0520";    // 品目構成マスター
        public const string TABLE_ID_M0600 = "M0600";    // 受注品マスター
        public const string TABLE_ID_M0700 = "M0700";    // 売上単価マスター
        public const string TABLE_ID_M0720 = "M0720";    // 購入単価マスター
        public const string TABLE_ID_S0820 = "S0820";    // カレンダーマスタ
        public const string TABLE_ID_D0520 = "D0520";    // 在庫ファイル

        // 切削生産計画システム (照会 / 更新 / マスタメンテ)
        public const string TABLE_ID_D0410  = "d0410";    // EM手配ファイル (確定)(手配情報)
        public const string TABLE_ID_D0440  = "d0440";    // EM手配日程ファイル (内示)(所要量情報)
        public const string TABLE_ID_KD8430 = "kd8430";   // 切削手配ファイル (確定)
        public const string TABLE_ID_KD8440 = "kd8440";   // 切削手配日程ファイル (内示)
        public const string TABLE_ID_KD8450 = "kd8450";   // 切削オーダーファイル (確定)
        public const string TABLE_ID_KD8460 = "kd8460";   // 切削在庫ファイル
        public const string TABLE_ID_KM8400 = "km8400";   // 切削生産計画システム利用者マスター
        public const string TABLE_ID_KM8410 = "km8410";   // 切削刃具マスター
        public const string TABLE_ID_KM8420 = "km8420";   // 切削設備マスター
        public const string TABLE_ID_KM8430 = "km8430";   // 切削コード票マスター

        public const string TABLE_NAME_KD8430  = "切削生産計画ファイル (確定)";
        public const string TABLE_NAME_KD8440  = "切削生産計画ファイル (内示)";
        public const string TABLE_NAME_KM8400 = "切削生産計画システム利用者マスター";
        public const string TABLE_NAME_KM8410 = "切削刃具 マスター";
        public const string TABLE_NAME_KM8420 = "切削設備マスター";
        public const string TABLE_NAME_KM8430 = "切削コード票マスター";
        public const string TABLE_NAME_KM8440 = "賃率マスター";
        public const string TABLE_NAME_KM8450 = "製造原価マスター";
        public const string TABLE_NAME_KM8460 = "原価管理雛形ファイル定義マスター";

        // 内製システム共有テーブル (照会のみ)
        public const string TABLE_ID_KS0010 = "KS0010";   // ホスト マスター
        public const string TABLE_ID_KS0030 = "KS0030";   // 権限マスター
        public const string TABLE_ID_KS0040 = "KS0040";   // メッセージ マスター

        // データ ディクショナリ ビュー
        public static readonly string USER_TAB_COLUMNS_COLUMN_NAME = "COLUMN_NAME";        // 物理名
        public static readonly string USER_TAB_COLUMNS_DATA_LENGTH = "DATA_LENGTH";        // データ桁数
        public static readonly string USER_TAB_COLUMNS_DATA_PRECISION = "DATA_PRECISION";  // データ精度
        public static readonly string USER_TAB_COLUMNS_DATA_SCALE = "DATA_SCALE";          // 小数点以下有効桁数
        public static readonly string USER_TAB_COLUMNS_NULLABLE = "NULLABLE";       // NULL 許容
        public static readonly string USER_TAB_COLUMNS_COLUMN_NAME_WKSEQ = "WKSEQ";    // 物理名 (WKSEQ)
        public static readonly string USER_TAB_COLUMNS_COLUMN_NAME_WORK = "WORK";      // 物理名 (WORK)
        public static readonly string USER_TAB_COLUMNS_COLUMN_NAME_SETUPTMMP = "SETUPTMMP";      // 物理名 (SETUPTMMP)
        public static readonly string USER_TAB_COLUMNS_COLUMN_NAME_SETUPTMSP = "SETUPTMSP";      // 物理名 (SETUPTMSP)
        public static readonly string USER_TAB_COLUMNS_COLUMN_NAME_NOTE = "NOTE";      // 物理名 (NOTE)
        public static readonly string USER_TAB_COLUMNS_DATA_TYPE_NUMBER = "NUMBER";    // Oracle データ型: 数値
        public static readonly string USER_TAB_COLUMNS_DATA_TYPE_INT = "INT";          // MySQL データ型: 数値
        public static readonly string USER_TAB_COLUMNS_NULLABLE_YES = "Y";             // NULL 許容 (はい)
        public static readonly string USER_TAB_COLUMNS_NULLABLE_NO = "N";              // NULL 許容 (いいえ)
        public static readonly int USER_TAB_COLUMNS_INDEX_DATA_TYPE = 2;               // データ型のインデックス
        public static readonly int USER_COL_COMMENTS_INDEX_COLUNM_NAME = 1;            // 列名のインデックス
        public static readonly int USER_COL_COMMENTS_INDEX_COMMENTS = 8;               // 備考のインデックス

        // 検索条件
        public static readonly string MIN_DATE = "1971/12/01";                         // 日付最小値 (創業日)
        public static readonly string MAX_DATE = "9999/12/31";                         // 日付最大値
        public static readonly string MIN_DATETIME = "1971/12/01 00:00:00";            // 日時最小値 (創業日)
        public static readonly string MAX_DATETIME = "9999/12/31 23:59:59";            // 日時最大値

        // KM8400 切削生産計画システム利用者マスター
        public const string KM8400_ACTIVE_INVALID = "0";    // 有効フラグ: 無効
        public const string KM8400_ACTIVE_VALID = "1";    // 有効フラグ: 有効
        public const string KM8400_ACTIVE_EXPIRED = "9";    // 有効フラグ: 失効

        // KM8410 切削刃具 マスター

        // KM8420 切削設備マスター
        public static readonly string KM8420_ONTIME = "設備稼働時間"; 
        public static readonly string KM8420_SETUPTM = "段取り時間";   // 段取り時間・1 ～ 段取り時間・3の合計


        //public static readonly int KM8420_PKEY_ODCD   = 1;  // 主キー番号 
        //public static readonly int KM8420_PKEY_WKGRCD = 2;  // 主キー番号 
        //public static readonly int KM8420_PKEY_HMCD   = 3;  // 主キー番号 
        //public static readonly int KM8420_PKEY_VALDTF = 4;  // 主キー番号 
        //public static readonly int KM8420_PKEY_WKSEQ  = 5;  // 主キー番号 

        //public const int KM8420_COLUMN_IDX_ODCD      = 0;  // 列インデックス: 手配先コード
        //public const int KM8420_COLUMN_IDX_WKGRCD    = 1;  // 列インデックス: 切削刃具 コード
        //public const int KM8420_COLUMN_IDX_HMCD      = 2;  // 列インデックス: 品番
        //public const int KM8420_COLUMN_IDX_VALDTF    = 3;  // 列インデックス: 適用年月日
        //public const int KM8420_COLUMN_IDX_WKSEQ     = 4;  // 列インデックス: 作業順序
        //public const int KM8420_COLUMN_IDX_WORK      = 5;  // 列インデックス: 作業内容
        //public const int KM8420_COLUMN_IDX_SETUPTMMP = 6;  // 列インデックス: 切削設備時間 (量産)
        //public const int KM8420_COLUMN_IDX_SETUPTMSP = 7;  // 列インデックス: 切削設備時間 (補給部品等)
        //public const int KM8420_COLUMN_IDX_NOTE      = 8;  // 列インデックス: 備考
        //public const int KM8420_COLUMN_IDX_INSTID    = 9;  // 列インデックス: 登録者
        //public const int KM8420_COLUMN_IDX_INSTDT    = 10;  // 列インデックス: 登録日時
        //public const int KM8420_COLUMN_IDX_UPDTID    = 11; // 列インデックス: 更新者
        //public const int KM8420_COLUMN_IDX_UPDTDT    = 12; // 列インデックス: 更新日時


        // テーブル列インデックス
        // KD8430 切削生産計画ファイル
        // #042 切削オーダー平準化画面
        // DataGridView 列インデックス
        public enum KD8430ClmIdx
        {
            OdrNo,    // 手配 No
            McGcd,    // グループ コード
            McCd,     // 設備コード
            HmCd,     // 品番
            EdDt,     // 完了予定日
            OdrQty,   // 手配数
        }

        public static readonly int KD8430_MCGCD_LEN   = 6;     // グループ コード 文字列長
        public static readonly int KD8430_ODQTY_SCALE = 0;     // 手配数 小数点以下桁数
        public static readonly string KD8430_PLNNO = "計画No"; // 工程1CT ～ 工程6CT の合計

        public const int KD8430_TARGET_CUR_ALL  = 0;           // 現状全件
        public const int KD8430_TARGET_SIM_ALL  = 1;           // 変更後全件
        public const int KD8430_TARGET_SPECIFIC = 2;           // 特定データ
        public const int KD8430_TARGET_MCGCD    = 3;           // グループのみ
        public const int KD8430_TARGET_MCCD     = 4;           // 設備のみ
        public const int KD8430_TARGET_MCTM     = 5;           // 設備時間


        // KD8440 切削生産計画日程ファイル
        // #042 切削オーダー平準化画面
        // DataGridView 列インデックス
        public enum KD8440ClmIdx
        {
            OdrNo,    // 手配 No
            McGcd,    // グループ コード
            McCd,     // 設備コード
            HmCd,     // 品番
            EdDt,     // 完了予定日
            OdrQty,   // 手配数
        }

        public static readonly int KD8440_ODSEQ_SCALE = 0;      // 手配数 小数点以下桁数

        public const int KD8440_TARGET_CUR_ALL  = 0;            // 現状全件
        public const int KD8440_TARGET_SIM_ALL  = 1;            // 変更後全件
        public const int KD8440_TARGET_SPECIFIC = 2;            // 特定データ
        public const int KD8440_TARGET_MCGCD    = 3;            // グループのみ
        public const int KD8440_TARGET_MCCD     = 4;            // 設備のみ
        public const int KD8440_TARGET_MCTM     = 5;            // 設備時間


        // KM8430 切削コード票マスター
        // #22 切削コード票登録画面
        // DataGridView 列インデックス
        public enum KM8430ClmIdx
        {
            OdCd,     // 手配先コード
            WkGrCd,   // 切削刃具 コード
            HmCd,     // 品番
            ValDtF,   // 適用年月日
            WkSeq,    // 作業順序
            CT,       // サイクルタイム
            Note,     // 備考
            InstID,   // 登録者
            InstDt,   // 登録日時
            UpdtID,   // 更新者
            UpdtDt,   // 更新日時
        }

        public static readonly int KM8430_WKSEQ_PRECISION = 3;  // 作業順序 データ精度
        public static readonly int KM8430_WKSEQ_SCALE = 0;      // 作業順序 小数点以下桁数
        public static readonly int KM8430_CT_PRECISION = 6;     // サイクルタイム データ精度
        public static readonly int KM8430_CT_SCALE = 2;         // サイクルタイム 小数点以下桁数
        public static readonly int KM8430_NOTE_LENGTH = 100;    // 備考 データ長

        public static readonly string KM8430_KT_CT_SUM = "工程CT合計";           // 工程1CT ～ 工程6CT の合計
        public static readonly string KM8430_KT_DT_SUM = "工程段取り時間合計";   // 工程1段取り時間 ～ 工程6段取り時間 の合計
        public static readonly string KM8430_KT_OT_SUM = "工程その他時間合計";   // 工程1段取り時間 ～ 工程6段取り時間 の合計

        public const int KM8430_TARGET_ALL = 0; // 全件
        public const int KM8430_TARGET_SPECIFIC = 1; // 特定データ
        public const int KM8430_TARGET_HMCD = 2; // 品番のみ


        // KM8440 賃率マスター
        // #23 賃率登録画面
        // DataGridView 列インデックス
        public enum KM8440ClmIdx
        {
            OdCd,         // 手配先コード
            KtCd,         // 工程コード
            ValDtF,       // 適用年月日
            KtSeq,        // 工程順序
            KtNm,         // 工程名称
            EqClass,      // 設備分類
            Model,        // 機種
            Manufacturer, // 製造元
            OpeCost,      // 操業費
            LaborCost,    // 労務費
            EqCost,       // 設備費
            LaborRate,    // 賃率
            Note,         // 備考
            InstID,       // 登録者
            InstDt,       // 登録日時
            UpdtID,       // 更新者
            UpdtDt,       // 更新日時
        }

        public static readonly int KM8440_KTSEQ_PRECISION = 3;   // 工程順序 データ精度
        public static readonly int KM8440_KTSEQ_SCALE = 0;       // 工程順序 小数点以下桁数
        public static readonly int KM8440_OUTLINE_LENGTH = 1000; // 工程名称、設備分類、機種、製造元 データ長
        public static readonly int KM8440_COST_PRECISION = 5;    // 操業費、労務費、設備費 データ精度 
        public static readonly int KM8440_COST_SCALE = 3;        // 操業費、労務費、設備費 小数点以下桁数
        public static readonly int KM8440_RATE_PRECISION = 5;    // 賃率 データ精度
        public static readonly int KM8440_RATE_SCALE = 3;        // 賃率 小数点以下桁数
        public static readonly int KM8440_NOTE_LENGTH = 100;     // 備考 データ長

        public const int KM8440_TARGET_ALL = 0; // 全件
        public const int KM8440_TARGET_SPECIFIC = 1; // 特定データ

        // KM8450 製造原価マスター
        // #24 製造原価登録画面
        // DataGridView 列インデックス
        public enum KM8450ClmIdx
        {
            HmCd,             // 品番
            ValDtF,           // 適用年月日
            KtCd,             // 工程コード
            PrepWt,           // 仕込み重量
            ScrapWt,          // スクラップ重量
            ScrapCost,        // スクラップ単価
            OSPtsCost,        // 外注部品費
            OSWages,          // 外注工賃
            BuySellPtsCost,   // 支給部品費 (有償)
            PurPtsCost,       // 購買部品費
            Note,             // 備考
            InstID,           // 登録者
            InstDt,           // 登録日時
            UpdtID,           // 更新者
            UpdtDt,           // 更新日時
        }

        public static readonly int KM8450_WT_PRECISION = 5;    // 仕込み重量、スクラップ重量 データ精度
        public static readonly int KM8450_WT_SCALE = 2;        // 仕込み重量、スクラップ重量 小数点以下桁数
        public static readonly int KM8450_COST_PRECISION = 9;  // スクラップ単価、外注部品費、外注工賃、支給部品費 (有償)、購買部品費 データ精度
        public static readonly int KM8450_COST_SCALE = 2;      // スクラップ単価、外注部品費、外注工賃、支給部品費 (有償)、購買部品費 小数点以下桁数
        public static readonly int KM8450_NOTE_LENGTH = 100;   // 備考 データ長

        public const int KM8450_TARGET_ALL = 0; // 全件
        public const int KM8450_TARGET_SPECIFIC = 1; // 特定データ
        public const int KM8450_TARGET_HMCD = 2; // 品番のみ



        public const string TABLE_ID_A01 = "A01";    // ベンダー工程 (母材用)
        public const string TABLE_ID_A02 = "A02";    // ベンダー工程 (手配用)
        public const string TABLE_ID_A03 = "A03";    // ベンダー工程 (所要用)
        public const string TABLE_ID_A13 = "A13";    // 切削工程コード表
        public const string TABLE_ID_A14 = "A14";    // 設備マスター
        public const string TABLE_ID_A15 = "A15";    // 工程内加工リストソート順マスター
        public const string TABLE_ID_A16 = "A16";    // 部品表工程マスター
        public const string TABLE_ID_HS02 = "H_S02"; // 切断工程
        public const string TABLE_ID_HS03 = "H_S03"; // ベンダー工程 (先端末加工)
        public const string TABLE_ID_HS04 = "H_S04"; // ベンダー工程 (曲げ)
        public const string TABLE_ID_HS05 = "H_S05"; // ベンダー工程 (後加工)
        public const string TABLE_ID_HS06 = "H_S06"; // 切削工程
        public const string TABLE_ID_HS99 = "H_S99"; // その他工程

        // 手配先名称マスター (M0300)
        public static readonly string M0300_ODCD_ISHIGURO_MFG = "42101";     // 手配先コード: ㈱石黒製作
        public static readonly string M0300_ODCD_F2_BENDING_FROM = "60300";  // 手配先コード(開始): F2ﾍﾞﾝﾀﾞｰ
        public static readonly string M0300_ODCD_F2_BENDING_TO = "60330W";   // 手配先コード(終了): F2ﾍﾞﾝﾀﾞｰ
        public static readonly string M0300_ODCD_HEAD_PROCESSING = "60310";  // 手配先コード: 先端加工
        public static readonly string M0300_ODCD_BENDING_1 = "60320";        // 手配先コード: 曲げ
        public static readonly string M0300_ODCD_BENDING_2 = "60302";        // 手配先コード: 曲げ
        public static readonly string M0300_ODCD_VARTICAL_PRESS_1 = "6033H"; // 手配先コード: 縦ﾌﾟﾚｽ１
        public static readonly string M0300_ODCD_MP_FROM = "60600";          // 手配先コード(開始): 切削
        public static readonly string M0300_ODCD_MP_TO = "60610";            // 手配先コード(終了): 切削
        public static readonly char M0300_IOKBN_INHOUSE = '1';               // 社内外区分: 1: 社内
        public static readonly char M0300_IOKBN_EXTERNAL = '2';              // 社内外区分: 2: 社外
        public static readonly char M0300_IOKBN_ALL = '%';                   // 社内外区分: %: 全件

        // 工程マスター (M0410)
        public static readonly string M0410_ODCD_ISHIGURO_MFG = "42101";     // 手配先コード: ㈱石黒製作
        public static readonly string M0410_ODCD_F2_BENDING_FROM = "60300";  // 手配先コード(開始): F2ﾍﾞﾝﾀﾞｰ
        public static readonly string M0410_ODCD_F2_BENDING_TO = "60330W";   // 手配先コード(終了): F2ﾍﾞﾝﾀﾞｰ
        public static readonly string M0410_ODCD_HEAD_PROCESSING = "60310";  // 手配先コード: 先端加工
        public static readonly string M0410_ODCD_BENDING_1 = "60320";        // 手配先コード: 曲げ
        public static readonly string M0410_ODCD_BENDING_2 = "60302";        // 手配先コード: 曲げ
        public static readonly string M0410_ODCD_VARTICAL_PRESS_1 = "6033H"; // 手配先コード: 縦ﾌﾟﾚｽ１
        public static readonly string M0410_ODCD_MP_FROM = "60600";          // 手配先コード(開始): 切削
        public static readonly string M0410_ODCD_MP_TO = "60610";            // 手配先コード(終了): 切削
        public static readonly char M0410_ODRKBN_INHOUSE = '1';              // 手配区分: 1: 内作
        public static readonly char M0410_ODRKBN_EXTERNAL = '2';             // 手配区分: 2: 外作
        public static readonly char M0410_ODRKBN_NYD = '3';                  // 手配区分: 3: 未定
        public static readonly char M0410_ODRKBN_ALL = '%';                  // 手配区分: %: 全件




        // 品目手順詳細マスター (M0510)
        public static readonly string M0510_STKTKBN_YES = "1";               // 初工程区分: Yes
        public static readonly string M0510_WKNOTE_CYCLETIME = "/CT";        // 作業内容: サイクルタイム ("/CT" 直後の小数値)
        // 手配ファイル (D0410)
        public static readonly string D0410_ODRSTS_ARRANGED = "2";           // 手配状態: 手配済み
        public static readonly string D0410_ODRSTS_RECEIVING = "3";          // 手配状態: 受入中
        // 切削工程生産計画元DT (H_S06)
        public static readonly int HS06_COLUMNNUM_EQUIPCD = 6;               // 「設備」の列番号 (1 始まり)
        public static readonly int HS06_COLUMNNUM_MCNO = 7;                  // 「機械番号」の列番号
        public static readonly int HS06_COLUMNNUM_INPROCESSNUM = 20;         // 「工程内加工数」の列番号
        public static readonly int HS06_COLUMNNUM_EQUIPCD_MIN = 30;          // 「設備コード」の最小列番号
        public static readonly int HS06_COLUMNNUM = 44;                      // 列数
        // 設備マスター (A13)
        public static readonly int A13_COLUMNNUM_EQUIPCD = 3;                // 「設備コード」の列番号
        // 部品表工程マスター (A16)
        // Excel
        public static readonly string EXL_SHEETNAME_ORGDATA = "元DT";        // [元DT] シート
        public static readonly string EXL_SHEETNAME_PT = "PT";               // [PT] シート
        public static readonly string EXL_SHEETNAME_COMPLETE = "完成";       // [完成] シート
        public static readonly string EXL_COLUMN_A = "A:A";                  // 列 A
        public static readonly string EXL_COLUMNNAME_A = "NO";               // 列名 A
        public static readonly string EXL_CELL_A1 = "A1";                    // セル A1

        // 共通
        public static readonly string TBL_NAME = "テーブル名";               // テーブル名
    }
}