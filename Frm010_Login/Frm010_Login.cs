using Oracle.ManagedDataAccess.Client;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace MPPPS
{
    using static Common;

    /// <summary>
    /// [ログイン] フォーム クラス
    /// </summary>
    public partial class Frm010_Login : Form, Interface

    {
        // 共通クラス
        private Common cmn;
        delegate void Delegate1(Common cmn);

        /// <summary>
        /// デフォルト コンストラクタ
        /// </summary>
        public Frm010_Login()
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            this.Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            this.Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_010 + ": " + Common.FRM_NAME_010 + ">";

            // [ログイン] フォームをモーダルで表示
            this.ShowDialog();
        }


        /// <summary>
        /// [ログイン] フォーム 読み込み
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Frm010_Login_Load(object sender, EventArgs e)
        {
            try
            {
                s_Logger.Info("*** Program started ***");
            }
            catch
            {
                // ログ ファイル使用中
                if (MessageBox.Show(MSGBOX_BRACKET_L + MSGBOX_TYPE_ERROR + MSGCD_701 + ": " + MSGBOX_MSGTITLE_USING_LOG_FILE + Common.MSGBOX_BRACKET_R + "\n" + MSGBOX_MSGBODY_USING_LOG_FILE, MSGBOX_CAPTION_FAILED, MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    // フォームを閉じる
                    Close();
                }
                return;
            };

            // 共通クラス
            cmn = new Common
            {
                // データベース設定データ クラス
                DbCd = new DBConfigData[3],
                // ファイル システム設定データ クラス配列
                FsCd = new FSConfigData[2],
                // 利用者情報クラス
                Ui = new UserInfo(),
                // Oracle 接続クラス
                OraCnn = new OracleConnection[2],
                // MySQL 接続クラス
                MySqlCnn = new MySqlConnection(),
            };

            // データベース管理者クラス
            cmn.Dbm = new DBManager(cmn);
            // データベース アクセサ クラス
            cmn.Dba = new DBAccessor(cmn);
            // ファイル管理クラス
            cmn.Fa = new FileAccessor(cmn);
            // 実行ファイル名
            cmn.ExeFileName = AppDomain.CurrentDomain.FriendlyName;
            // 実行ファイルのあるディレクトリ
            cmn.BaseDir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            // 設定ファイルのあるディレクトリ
            cmn.ConfDir = Path.Combine(cmn.Fa.GetParentDirectory(cmn.BaseDir), Common.CONFIG_DIR);
            // データベース設定ファイル取得
            cmn.CnfFilePathDb = Path.Combine(cmn.ConfDir, Common.CONFIG_FILE_DB);
            // ファイル システム設定ファイル取得
            cmn.CnfFilePathFs = Path.Combine(cmn.ConfDir, Common.CONFIG_FILE_FS);
            // 音声ファイルのあるディレクトリ
            cmn.SoundDir = Path.Combine(cmn.Fa.GetParentDirectory(cmn.BaseDir), Common.SOUND_DIR);
            // 正答音ファイル取得
            cmn.SoundFilePathCorrect = Path.Combine(cmn.SoundDir, Common.SOUND_FILE_CORRECT);
            // 誤答音ファイル取得
            cmn.SoundFilePathWrong = Path.Combine(cmn.SoundDir, Common.SOUND_FILE_WRONG);
            // 誤答音ファイル取得
            cmn.SoundFilePathOpening = Path.Combine(cmn.SoundDir, Common.SOUND_FILE_OPENING);

            // データベース設定ファイル逆シリアライズ
            cmn.DbCd = cmn.Fa.ReserializeDBConfigFile();
            // ファイル システム設定ファイル逆シリアライズ
            cmn.FsCd = cmn.Fa.ReserializeFSConfigFile();

            // スクリーン表示倍率
            double dpi = Screen.PrimaryScreen
                .Bounds.Height;         // ディスプレイの解像度 (例:Width=1920, Height=1080)
            double dpiMag = System.Windows.SystemParameters
                .PrimaryScreenHeight;   // 拡大時の画面高さ     (例:Width=1536, Height=864)
            cmn.ScreenMagnification = dpi / dpiMag; // スクリーン表示倍率   (例:1.25)

            // ホストマスタ オブジェクトを生成
            cmn.PkKS0010 = new PkKS0010();

            // 前回の認証情報を初期表示
            LoadAuthInfo();
        }

        /// <summary>
        /// 進捗率設定
        /// </summary>
        /// <param name="cmn">共通クラス</param>
        private void SetStateOfProgress(Common cmn)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            if (cmn.Progress >= 100)
            {
                cmn.Progress = 100;
            }
            //this.Tsl2.Text = Convert.ToString(cmn.Progress) + "%";
            //this.toolStripProgressBar1.Value = cmn.Progress;
            //this.Tsl3.Text = cmn.Step;
        }

        ///
        /// インタフェース クラスのメソッド定義
        ///

        /// <summary>
        /// 進捗状況設定スレッド
        /// </summary>
        /// <param name="progress">進捗率</param>
        void Interface.SetStateOfProgressThread(object cmn)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 進捗率設定
            Invoke(new Delegate1(SetStateOfProgress), (Common)cmn);
        }

        // [メイン メニュー] フォーム宣言
        Frm020_MainMenu frm020 = null;

        /// <summary>
        /// [OK] ボタン クリック★
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_OK_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // EM データベース接続テスト
            OracleConnection emCnn = null;
            if (!cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_EM, ref emCnn))
            {
                Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);

                // 復改文字を文字列 (\\n) から制御コード (\n) に変換
                string msgBody = cmn.CorrectLineFeed(Common.MSG_BODY_EM_DB_CONN_ERR);

                // メッセージ ボックス表示 (データベース接続前なので直接作成する)
                MessageBox.Show(Common.MSG_CD_PREFIX + Common.MSG_TYPE_F + Common.MSG_CD_801 + Common.MSG_CD_SUFFIX + Common.MSG_TITLE_DB_CONN_ERR + '\n' + '\n' +
                       msgBody, Common.MSGBOX_TXT_FATAL, MessageBoxButtons.OK, MessageBoxIcon.Error);

                // [ログイン] フォームを閉じる
                this.Close();
                return;
            }

            // 内製プログラム データベース接続テスト
            OracleConnection kkCnn = null;
            if (!cmn.Dbm.IsConnectOraSchema(Common.DB_CONFIG_KK, ref kkCnn))
            {
                Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);

                // 復改文字を文字列 (\\n) から制御コード (\n) に変換
                string msgBody = cmn.CorrectLineFeed(Common.MSG_BODY_KK_DB_CONN_ERR);

                // メッセージ ボックス表示 (データベース接続前なので直接作成する)
                MessageBox.Show(Common.MSG_CD_PREFIX + Common.MSG_TYPE_F + Common.MSG_CD_801 + Common.MSG_CD_SUFFIX + Common.MSG_TITLE_DB_CONN_ERR + '\n' + '\n' +
                       msgBody, Common.MSGBOX_TXT_FATAL, MessageBoxButtons.OK, MessageBoxIcon.Error);

                // [ログイン] フォームを閉じる
                this.Close();
                return;
            }

            // 切削生産計画システム データベースに接続
            MySqlConnection mpCnn = null;
            if (!cmn.Dbm.IsConnectMySqlSchema(ref mpCnn))
            {
                Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);

                // 復改文字を文字列 (\\n) から制御コード (\n) に変換
                string msgBody = cmn.CorrectLineFeed(Common.MSG_BODY_MP_DB_CONN_ERR);

                // メッセージ ボックス表示 (データベース接続前なので直接作成する)
                MessageBox.Show(Common.MSG_CD_PREFIX + Common.MSG_TYPE_F + Common.MSG_CD_801 + Common.MSG_CD_SUFFIX + Common.MSG_BODY_MP_DB_CONN_ERR + '\n' + '\n' +
                       msgBody, Common.MSGBOX_TXT_FATAL, MessageBoxButtons.OK, MessageBoxIcon.Error);

                // [ログイン] フォームを閉じる
                this.Close();
                return;
            }

            // 認証処理
            // 未入力チェック
            if ((Tbx_UserId.Text.Length == 0) || (Tbx_Passwd.Text.Length == 0))
            {
                // 復改文字を文字列 (\\n) から制御コード (\n) に変換
                string msgBody = cmn.CorrectLineFeed(Common.MSG_BODY_EM_DB_CONN_ERR);

                // 未入力
                Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
                cmn.ShowMessageBox(Common.KCM_PGM_ID,Common.MSG_CD_100, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error); // 設定ファイルなし
                return;
            }

            // EM 利用権限チェック
            cmn.IkM0010 = new IkM0010
            {
                TanCd = Tbx_UserId.Text,
                Passwd = Tbx_Passwd.Text
            };
            bool isPasswdFree = false;
            string userName = "";
            string atgCd = "";
            if (!cmn.Dba.IsAuthrizedEMUser(isPasswdFree, ref userName, ref atgCd))
            {
                // 利用者登録なし
                Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
                cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_101, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);

                // [ログイン] フォームを閉じる
                this.Close();
                return;
            }
            else
            {
                // 利用者登録あり
                cmn.Ui.UserId = Tbx_UserId.Text;
                cmn.Ui.Passwd = Tbx_Passwd.Text;
                cmn.Ui.UserName = userName;
                cmn.Ui.AtgCd = atgCd;

                // EM 担当者コードをテーブルの登録者/更新者に設定
                cmn.DrCommon = new DrCommon
                {
                    InstID = cmn.Ui.UserId,
                    UpdtID = cmn.Ui.UserId
                };
            }

            // 切削生産計画システム利用権限チェック
            cmn.PkKM8400 = new PkKM8400
            {
                UserId = Tbx_UserId.Text
            };
            string active = "";
            string authLv = "";
            if (!cmn.Dba.IsAuthrizedMPPPUser(ref active, ref authLv))
            {
                // 利用者登録なし
                Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
                cmn.ShowMessageBox(Common.MY_PGM_ID, Common.MSG_CD_100, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error); 

                // [ログイン] フォームを閉じる
                this.Close();
                return;
            }
            else
            {
                switch (active)
                {
                    case Common.KM8400_ACTIVE_VALID:
                        {
                            // 利用権限有効
                            cmn.Ui.Active = active;
                            cmn.Ui.AuthLv = authLv;
                            break;
                        }
                    case Common.KM8400_ACTIVE_INVALID:
                        {
                            // 利用権限無効
                            Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
                            cmn.ShowMessageBox(Common.MY_PGM_ID, Common.MSG_CD_101, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);

                            // [ログイン] フォームを閉じる
                            this.Close();
                            return;
                        }
                    case Common.KM8400_ACTIVE_EXPIRED:
                        {
                            // 利用権限失効
                            Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
                            cmn.ShowMessageBox(Common.MY_PGM_ID, Common.MSG_CD_102, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);

                            // [ログイン] フォームを閉じる
                            this.Close();
                            return;
                        }
                    default:
                        {
                            // 有効フラグ不正
                            Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
                            cmn.ShowMessageBox(Common.MY_PGM_ID, Common.MSG_CD_103, Common.MSG_TYPE_F, MessageBoxButtons.OK, Common.MSGBOX_TXT_FATAL, MessageBoxIcon.Error);

                            // [ログイン] フォームを閉じる
                            this.Close();
                            return;
                        }
                }
            }

            // 認証 OK
            if ((frm020 == null) || frm020.IsDisposed)
            {
                // 認証情報保存
                SaveAuthInfo();

                // [ログイン] フォームを隠す
                this.Hide();

                // [メイン メニュー] フォームをモーダルで表示
                frm020 = new Frm020_MainMenu(this);
                frm020.ShowDialog(this);
            }
        }

        /// <summary>
        /// [キャンセル] ボタン クリック
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // [ログイン] フォームを閉じる
            this.Close();
        }

        /// <summary>
        /// フォームが閉じられるときのイベント処理
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Frm010_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 認証情報保存
            SaveAuthInfo();

        }
        
        /// <summary>
        /// 認証情報保存
        /// </summary>
        private void SaveAuthInfo()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 各コントロールの選択値 (表示値) を App.config に書き込む
            // ・接続情報 (Oracle バージョン)
            // ・接続情報 (スキーマ名)
            // ・ユーザー ID
            // ・ユーザー ID を記憶する
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["oracleVer"].Value = Cbx_OracleVer.Text;
            config.AppSettings.Settings["schema"].Value = Cbx_Schema.Text;
            config.AppSettings.Settings["userID"].Value = Tbx_UserId.Text;
            config.AppSettings.Settings["memUserId"].Value = Chk_MemUserId.Checked.ToString();
            config.Save();
        }

        /// <summary>
        /// 認証情報読み出し
        /// </summary>
        private void LoadAuthInfo()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // App.config から認証情報を読み込み各コントロールに設定 (表示) させる
            // 読み込む情報が (前回画面終了時に選択されていた値)
            // ・接続情報 (Oracle バージョン)
            // ・接続情報 (スキーマ名)
            // ・ユーザー ID
            // ・ユーザー ID を記憶する
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // 前回状態復元要否
            if (config.AppSettings.Settings["memUserId"].Value == "True") // 前回 [ユーザー ID を記憶する] がチェック「オン」だったとき
            {
                Cbx_OracleVer.Text = config.AppSettings.Settings["oracleVer"].Value;
                Cbx_Schema.Text = config.AppSettings.Settings["schema"].Value; // 【要検討】スキーマ名は設定情報からセットしたほうがよいかも
                //▼[デバッグ][MOD] 評価用初期表示
                // リリース時には元に戻すこと
                //Tbx_UserId.Text = config.AppSettings.Settings["userID"].Value;
                //Tbx_Passwd.Text = "";
                Tbx_UserId.Text = "11014";
                Tbx_Passwd.Text = "0215";
                //▲[デバッグ][MOD] 評価用初期表示
                Chk_MemUserId.Checked = true;
            }
            else // チェック「オフ」だったとき
            {
                Cbx_OracleVer.Text = Cbx_OracleVer.Items[1].ToString();
                Cbx_Schema.Text = cmn.DbCd[Common.DB_CONFIG_EM].Schema;
                //▼[デバッグ][MOD] 評価用初期表示
                // リリース時には元に戻すこと
                //Tbx_UserId.Text = "";
                //Tbx_Passwd.Text = "";
                Tbx_UserId.Text = "11014";
                Tbx_Passwd.Text = "0215";
                //▲[デバッグ][MOD] 評価用初期表示
                Chk_MemUserId.Checked = false;
            }
        }

        /// <summary>
        /// 共通クラス取得
        /// </summary>
        /// <param name="cmnIntfc"></param>
        void Interface.GetCommonClass(ref Common cmn)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            cmn = this.cmn;
        }

        /// <summary>
        /// ウィンドウを閉じる
        /// </summary>
        void Interface.CloseWindow()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            Close();
        }

        /// <summary>
        /// ユーザー ID を記憶するチェック ボックス変更
        /// </summary>
        /// <param name="sender">送信オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Chk_MemUserId_CheckedChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 「オン」のとき、App.config に認証情報を書き込む
            if (Chk_MemUserId.Checked)
            {
                cmn.Ui.MemAuthInfo = true;
            }
            else
            {
                cmn.Ui.MemAuthInfo = false;
            }
        }

        private void Tbx_UserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Tbx_Passwd.Focus();
        }

        private void Tbx_Passwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Btn_OK.Focus();
        }

        private void Frm010_Login_Activated(object sender, EventArgs e)
        {
            if (Chk_MemUserId.Checked)
            {
                Tbx_Passwd.Focus();
            }
            else
            {
                Tbx_Passwd.Focus();
            }
        }
    }
}
