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
        public static readonly string MY_PGM_VER = "260519.01";                 // プログラム バージョン
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

        public static readonly int DB_CONFIG_EM = 0;                            // DB 接続定義 (EM)
        public static readonly int DB_CONFIG_KK = 1;                            // DB 接続定義 (内製プログラム)
        public static readonly int DB_CONFIG_MP = 2;                            // DB 接続定義 (切削生産計画システム)
        public static readonly int DB_CONFIG_TN = 3;                            // DB 接続定義 (タナコンサーバー)
        public static readonly int DB_CONFIG_PG = 4;                            // DB 接続定義 (i-Reporterサーバー)


        // デバッグ ログ
        // カテゴリー
        public static readonly string DBG_CAT_CLASS = "Class Name";
        public static readonly string DBG_CAT_METHOD = "Method Name";
        public static readonly string DBG_CAT_MSG = "Debug Message";


        // OpenFileDialog
        public static readonly string OFD_INIT_DIR = @"%USERPROFILE%\Desktop";  // 初期表示させるディレクトリ
        public static readonly string OFD_TITLE_OPEN = "ファイルを開く";        // タイトル (開く)
        public static readonly string OFD_FILE_TYPE_CSV = "CSV ファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*"; // ファイルの種類 (CSV)
        public static readonly string OFD_FILE_TYPE_XLS = "Excel ファイル (*.xlsx; *.xlsm; *.xls)|*.xlsx;*.xlsm;*.xls|CSV ファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*"; // ファイルの種類 (Excel)

        // SaveFileDialog
        public static readonly string SFD_INIT_DIR = @"%USERPROFILE%\Desktop";  // 初期表示させるディレクトリ
        public static readonly string SFD_TITLE_SAVE = "名前をつけて保存";      // タイトル (保存)
        public static readonly string SFD_FILE_TYPE_CSV = "CSV ファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*"; // ファイルの種類 (CSV)
        public static readonly string SFD_FILE_TYPE_XLS = "Excel ファイル (*.xlsx; *.xls)|*.xlsx;*.xls|CSV ファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*"; // ファイルの種類 (Excel)
        public const int SFD_RET_SAVE_SUCCEEDED = 1;        // 結果コード (1: 保存成功)
        public const int SFD_RET_SAVE_CANCELED = 0;         // 結果コード (0: キャンセル)
        public const int SFD_RET_SAVE_FAILED = -1;          // 結果コード (-1: 保存失敗)
        public const int SFD_RET_AUTH_FAILED = -2;          // 結果コード (-2: 認証失敗)
        public const int SFD_RET_FILE_IN_USE = -3;          // 結果コード (-2: ファイル使用中)

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
        public static readonly string FRM_ID_042 = "#042";  // 手配検索
        public static readonly string FRM_ID_043 = "#043";  // 追加オーダーの作成
        public static readonly string FRM_ID_044 = "#044";  // 内示情報一覧
        public static readonly string FRM_ID_045 = "#045";  // 内示検索
        public static readonly string FRM_ID_050 = "#050";  // 製造管理
        public static readonly string FRM_ID_051 = "#051";  // 加工進捗状況（進度盤）
        public static readonly string FRM_ID_052 = "#052";  // 製造部計画表
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
        public static readonly string FRM_ID_093 = "#093";  // タナコン在庫情報
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
        public static readonly string FRM_NAME_042 = "手配検索";
        public static readonly string FRM_NAME_043 = "追加オーダーの作成";
        public static readonly string FRM_NAME_044 = "内示情報一覧";
        public static readonly string FRM_NAME_045 = "内示検索";
        public static readonly string FRM_NAME_050 = "製造管理";
        public static readonly string FRM_NAME_051 = "加工進捗状況（進度盤）";
        public static readonly string FRM_NAME_052 = "製造部計画表";
        public static readonly string FRM_NAME_070 = "実績管理";
        public static readonly string FRM_NAME_071 = "切削ストア受入実績処理";
        public static readonly string FRM_NAME_072 = "切削ストア受入実績情報表示";
        public static readonly string FRM_NAME_073 = "棚卸情報";
        public static readonly string FRM_NAME_080 = "材料管理";
        public static readonly string FRM_NAME_081 = "材料在庫一覧";
        public static readonly string FRM_NAME_082 = "材料発注処理";
        public static readonly string FRM_NAME_083 = "材料検収";
        public static readonly string FRM_NAME_090 = "切削ストア";
        public static readonly string FRM_NAME_091 = "切削ストア出庫";
        public static readonly string FRM_NAME_092 = "切削ストア在庫情報";
        public static readonly string FRM_NAME_093 = "タナコン在庫情報";
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


        // Color定数
        // #041 手配取込
        // #042 手配情報
        // #044 内示取込
        public static readonly Color FRM40_BG_COLOR_CONTROL = SystemColors.Control;
        public static readonly Color FRM40_BG_COLOR_WORKING = Color.White;
        public static readonly Color FRM40_BG_COLOR_HOLIDAY = Color.LightPink;
        public static readonly Color FRM40_BG_COLOR_SATURDAY = Color.LightBlue;
        public static readonly Color FRM40_BG_COLOR_ORDERED = Color.MistyRose;      // EMあり、未取込
        public static readonly Color FRM40_BG_COLOR_IMPORTED = Color.LightCyan;     // MP取込済
        public static readonly Color FRM40_BG_COLOR_WARNING = Color.LightCoral;     // MPとEMで差異あり
        public static readonly Color FRM40_BG_COLOR_PRINTED = Color.LightGreen;     // MP印刷済
        public static readonly Color FRM40_BG_COLOR_PLANED = Color.LightGreen;      // MP内示取込済
        public static readonly Color FRM40_BG_COLOR_OTHERMONTH = Color.WhiteSmoke;
        public static readonly Color FRM40_COLOR_BLACK = Color.Black;
        public static readonly Color FRM40_COLOR_DIMGRAY = Color.DimGray;

        // #052 製造部計画表
        public static readonly string NEWORDER = "NewOrder";


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
        public static readonly string MSG_CD_106 = "106";
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
        public static readonly string MSG_SCHEDULE_NOT_FIXED = "手配日程データがまだ確定されていません。";
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

        // テーブル名称
        // EM (照会のみ)
        public const string TABLE_ID_M0010 = "M0010";    // 担当者マスター
        public const string TABLE_ID_M0200 = "M0200";    // 得意先名称マスター
        public const string TABLE_ID_M0300 = "M0300";    // 手配先名称マスター
        public const string TABLE_ID_M0340 = "M0340";    // 手配先管理期間マスター
        public const string TABLE_ID_M0410 = "M0410";    // 工程マスター
        public const string TABLE_ID_M0500 = "M0500";    // 品目マスター
        public const string TABLE_ID_M0510 = "M0510";    // 品目手順詳細マスター
        public const string TABLE_ID_M0520 = "M0520";    // 品目構成マスター
        public const string TABLE_ID_S0820 = "S0820";    // カレンダーマスタ
        public const string TABLE_ID_D0410 = "D0410";    // 手配ファイル
        public const string TABLE_ID_D0440 = "D0440";    // 手配日程ファイル
        public const string TABLE_ID_D0520 = "D0520";    // 在庫ファイル

        // 切削生産計画システム (照会 / 更新 / マスタメンテ)
        public const string TABLE_ID_KD8430 = "kd8430";   // 切削手配ファイル (確定)
        public const string TABLE_ID_KD8440 = "kd8440";   // 切削手配日程ファイル (内示)
        public const string TABLE_ID_KD8450 = "kd8450";   // 切削オーダーファイル (確定)
        public const string TABLE_ID_KD8460 = "kd8460";   // 切削在庫ファイル
        public const string TABLE_ID_KD8470 = "kd8470";   // 切削内示カードファイル
        public const string TABLE_ID_KD8480 = "kd8480";   // 切削日報兼実績ファイル
        public const string TABLE_ID_KM8400 = "km8400";   // 切削生産計画システム利用者マスター
        public const string TABLE_ID_KM8410 = "km8410";   // 切削刃具マスタ
        public const string TABLE_ID_KM8420 = "km8420";   // 切削設備マスタ
        public const string TABLE_ID_KM8430 = "km8430";   // 切削コード票マスタ
        public const string TABLE_ID_KM8435 = "km8435";   // 切削共通部品マスタ
        public const string TABLE_ID_KW8440 = "kw8440";   // 切削手配日程テンポラリ

        public const string TABLE_NAME_KD8430 = "切削手配ファイル";
        public const string TABLE_NAME_KD8440 = "切削手配日程ファイル";
        public const string TABLE_NAME_KD8450 = "切削オーダーファイル";
        public const string TABLE_NAME_KM8400 = "切削生産計画システム利用者マスター";
        public const string TABLE_NAME_KM8410 = "切削刃具マスタ";
        public const string TABLE_NAME_KM8420 = "切削設備マスタ";
        public const string TABLE_NAME_KM8430 = "切削コード票マスタ";
        public const string TABLE_NAME_KM8435 = "切削共通部品マスタ";

        // 内製システム共有テーブル (照会のみ)
        public const string TABLE_ID_KS0010 = "KS0010";   // ホスト マスター
        public const string TABLE_ID_KS0030 = "KS0030";   // 権限マスター
        public const string TABLE_ID_KS0040 = "KS0040";   // メッセージ マスター

        // データ ディクショナリ ビュー
        public static readonly string USER_TAB_COLUMNS_COLUMN_NAME = "COLUMN_NAME";        // 物理名
        public static readonly string USER_TAB_COLUMNS_DATA_LENGTH = "DATA_LENGTH";        // データ桁数
        public static readonly string USER_TAB_COLUMNS_DATA_PRECISION = "DATA_PRECISION";  // データ精度
        public static readonly string USER_TAB_COLUMNS_DATA_SCALE = "DATA_SCALE";          // 小数点以下有効桁数
        public static readonly string USER_TAB_COLUMNS_NULLABLE = "NULLABLE";           // NULL 許容
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