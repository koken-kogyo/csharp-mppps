using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm050_MfgCtrl : Form
    {
        // 共通クラス
        private readonly Common cmn;

        public Frm050_MfgCtrl(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            // [切削設備登録] ボタンからの起動のとき
            Text = " <" + Common.FRM_ID_050 + ": " + Common.FRM_NAME_050 + ">";

            // 初期化
            toolStripStatusLabel1.Text = "";

            // 共通クラス
            this.cmn = cmn;
        }

        private void Frm050_MfgCtrl_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        // 加工進捗情報表示
        private void Btn_MfgProgress_Click(object sender, EventArgs e)
        {
            Frm051_OrderDirection frm051 = new Frm051_OrderDirection();
            frm051.Show();
        }

        private async void btn_All_Print_Click(object sender, EventArgs e)
        {
            Frm052_PrintSettings frm052 = new Frm052_PrintSettings(cmn);
            frm052.Show();
        }

        // 閉じる
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void Frm050_MfgCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Hide();
        }
        private void Frm050_MfgCtrl_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        // フォームアクティブ時にマウスポインタの位置をトップボタンの中央にする
        private void Frm050_MfgCtrl_Activated(object sender, EventArgs e)
        {
            // フォーム上のクライアント座標を、画面座標に変換する
            Point sp = this.PointToScreen(new Point(Btn_MfgProgress.Left, Btn_MfgProgress.Top));

            // マウスポインタの位置をトップボタンに設定
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(sp.X + 10,
                sp.Y + (Btn_MfgProgress.Height / 2));
        }


        // 工程別促進データ作成
        private async void btn_PickupTehai_Click(object sender, EventArgs e)
        {
            // 出力ファイルを設定
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath1 = Path.Combine(userProfile, "促進.xlsx");
            string filePath2 = Path.Combine(userProfile, "①.xlsx");

            // Excelブックが開いているかどうか確認
            if (cmn.Fa.IsWorkbookOpen("①.xlsx"))
            {
                MessageBox.Show("Excelブックが開かれています。書き込み出来ません．", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // 読み取り専用確認
            FileInfo fileInfo1 = new FileInfo(filePath1);
            FileInfo fileInfo2 = new FileInfo(filePath2);
            if ((fileInfo1.Exists && fileInfo1.IsReadOnly) || (fileInfo2.Exists && fileInfo2.IsReadOnly))
            {
                MessageBox.Show("読み取り専用で書き込み出来ません．", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // 上書き保存確認
            if (fileInfo1.Exists || fileInfo2.Exists)
            {
                if (MessageBox.Show("上書き保存してもよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    == DialogResult.No) return;
            }
            else if (MessageBox.Show("促進表データの作成を行います．\n\nよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    == DialogResult.No)
            {
                return;
            }



            DataTable d0410 = new DataTable();
            DataTable km8430 = new DataTable();
            int ret = 0;

            try
            {
                toolStripStatusLabel1.Text = "促進データ抽出中...";
                // 最新のコード票マスタ抽出
                var taskA = Task.Run(() => cmn.Dba.GetCodeSlipMst(ref km8430));
                // 促進データ抽出
                var taskB = Task.Run(() => ret = cmn.Dba.促進データ抽出(ref d0410));
                // 両者が完了するまで待機する
                await Task.WhenAll(taskA, taskB);
                if (ret < 0) return;
                if (ret == 0)
                {
                    MessageBox.Show("促進データが抽出出来ませんでした．", "データなし", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // Interop.Excel起動
                cmn.Fa.ExcelApplication(false);

                // Excelにぶち込み一旦保存
                toolStripStatusLabel1.Text = "[促進.xlsx] を作成中...";
                cmn.Fa.AddNewBook();
                cmn.Fa.ExportExcel(d0410, filePath1);

                // ExcelでPivotテーブルを作成し集計シートを作成し保存
                cmn.Fa.促進データPivot集計保存();

                // 集計した工程シート用データを次の処理に使用するため保持
                object[,] data = cmn.Fa.getWorkSheetUsedRange("集計");

                // Bookを閉じる
                cmn.Fa.CloseExcelFile2(false);

                toolStripStatusLabel1.Text = "[促進.xlsx] の作成が終了しました.";

                // Interop.Excelではなく標準アプリケーションで開く
                //Process.Start(@filePath1);

                await Task.Delay(1000); // COMオブジェクト開放完了まで少し待つ（念のため）


                // ①_雛形.xlsxを開く
                toolStripStatusLabel1.Text = "[①.xlsx] に 最新のコード票 を 転送中...";
                cmn.Fa.OpenExcelFile2($@"{cmn.FsCd[5].RootPath}\{cmn.FsCd[5].FileName}");

                // ①_雛形シート[1]に最新のコード票マスタを書き込む
                cmn.Fa.WriteDataTableToExcel(km8430, 38);

                // ①_雛形に工程データを渡して編集して保存
                cmn.Fa.印刷用促進データ編集保存(data, filePath2, ref toolStripStatusLabel1);

                // Bookを閉じる
                cmn.Fa.CloseExcelFile2(false);

                if (File.Exists(filePath2))
                {
                    // Interop.Excelではなく標準アプリケーションで開く
                    Process.Start(@filePath2);

                    toolStripStatusLabel1.Text = "促進データの作成が終了しました.";
                }
                else
                {
                    toolStripStatusLabel1.Text = "促進データの作成終了.";
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "異常発生.";
                MessageBox.Show("工程別促進データ作成中にエラーが発生しました: " + ex.Message, "異常発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                // Interop.Excel終了
                cmn.Fa.CloseExcel2();
            }



        }

        private async void btn_PickupNaiji_Click(object sender, EventArgs e)
        {
        }
    }
}
