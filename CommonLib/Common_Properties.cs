using Oracle.ManagedDataAccess.Client;
using MySql.Data.MySqlClient;

namespace MPPPS
{
    /// <summary>
    /// 共通クラス
    /// </summary>
    public partial class Common
    {
        /// <summary>
        /// プロパティ
        /// </summary>
        //        public OracleConnection EmCnn { get; set; } // Oracle データ接続 (Managed)
        //        public Oracle.DataAccess.Client.OracleConnection ODACEmCnn { get; set; } // Oracle データ接続 (ODAC)
        public OracleConnection[]OraCnn { get; set; }     // Oracle データベース接続 (ODAC)
        public MySqlConnection MySqlCnn { get; set; }     // MySQL データベース接続 (ODAC)
        public DBManager Dbm { get; set; }             // DB マネージャ
        public DBAccessor Dba { get; set; }            // DB アクセサ
        public DBConfigData[] DbCd { get; set; }    // データベース設定データ
        public FSConfigData[] FsCd { get; set; }    // ファイル システム設定データ

        // 内製システム共有テーブル
        public PkKS0010 PkKS0010 { get; set; }      // PkKS0010 テーブル 主キー

        // 切削生産計画システム テーブル
        // 主キー
        public PkKM8400 PkKM8400 { get; set; }      // KM8400 切削生産計画システム利用者マスター 主キー


        // EM テーブル
        public PkM0010 PkM0010 { get; set; }        // M0010 担当者マスター 主キー
        public IkM0010 IkM0010 { get; set; }        // M0010 担当者マスター 検索キー
        public DrCommon DrCommon { get; set; }      // 共通データ レコード
        public FileAccessor Fa { get; set; }        // ファイル アクセサ
        public UserInfo Ui { get; set; }            // ユーザー情報
        public string BaseDir { get; set; }         // 実行ファイルのあるディレクトリ
        public string SoundDir { get; set; }        // 音声ファイルのあるディレクトリ
        public string ConfDir { get; set; }         // 設定ファイルのあるディレクトリ
        public string ExeFileName { get; set; }     // 実行ファイル名
        public string CnfFilePathDb { get; set; }   // データベース設定ファイルへのパス
        public string CnfFilePathFs { get; set; }   // ファイル システム設定ファイルへのパス
        public string SoundFilePathCorrect { get; set; }  // 正答音ファイルへのパス
        public string SoundFilePathWrong { get; set; }    // 誤答音ファイルへのパス
        public string SoundFilePathOpening { get; set; }  // iPhone 着信音ファイルへのパス
        public int Progress { get; set; }           // 進捗状況
        public string Step { get; set; }            // 進捗注釈
        public char RegForm { get; set; }           // 登録形式 (S: 単票形式、L: リスト形式)

        public int DgvErrIndex { get; set; }        // DataGridView エラー インデックス
        public string ButtonText { get; set; }      // クリックされたボタンのテキスト

        public double ScreenMagnification { get; set; }     // 実DPIと仮想DPIIの倍率 (表示倍率 例1.25(125%))
    }
}