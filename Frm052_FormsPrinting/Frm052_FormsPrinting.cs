using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm052_FormsPrinting : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DataTable km8420 = new DataTable();     // 設備マスタ
        private DataTable km8430 = new DataTable();     // コード票マスタ
        private DataTable kd8430 = new DataTable();     // 手配ファイル

        public Frm052_FormsPrinting(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_042 + ": " + Common.FRM_NAME_041 + ">";

            // 共通クラス
            this.cmn = cmn;
        }

        private async void btn_All_Print_Click(object sender, EventArgs e)
        {
            // 手配情報:kd8430、内示情報:kd8440、在庫情報:kd8460を読込
            DataTable exportDt = new DataTable();
            DataTable okureDt = new DataTable();
            DataTable tehaiDt = new DataTable();
            DataTable naijiDt = new DataTable();
            DataTable zaikoDt = new DataTable();
            DataTable maektDt = new DataTable();
            bool retOkure = false;
            bool retTehai = false;
            bool retNaiji = false;
            bool retZaiko = false;
            bool retMaekt = false;
            var taskOkure = Task.Run(() => retOkure = cmn.Dba.GetTehaiZan(ref okureDt, 0)); // W
            var taskTehai = Task.Run(() => retTehai = cmn.Dba.GetTehaiZan(ref tehaiDt, 7)); // 1W
            var taskNaiji = Task.Run(() => retNaiji = cmn.Dba.GetNaiji(ref naijiDt));       // 2W～3W
            var taskZaiko = Task.Run(() => retZaiko = cmn.Dba.GetZaiko(ref zaikoDt));
            var taskMaekt = Task.Run(() => retMaekt = cmn.Dba.GetMPMaeKT(ref maektDt));
            await Task.WhenAll(taskOkure, taskTehai, taskNaiji, taskZaiko, taskMaekt);
            if (!retOkure || !retTehai || !retNaiji || !retZaiko || !retMaekt) return;

            // 各データテーブルをマージ
            exportDt.Merge(okureDt);
            exportDt.Merge(tehaiDt);
            exportDt.Merge(naijiDt);
            exportDt.Merge(zaikoDt);

            // 前工程の情報を付与する
            MargeOrderDataTable(ref exportDt, ref maektDt);

            // 保存ダイアログ
            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                FileName = $"手配内示在庫状況_{DateTime.Now.ToString("M")}時点",
                InitialDirectory = Common.SFD_INIT_DIR, // 既定のディレクトリ名
                Filter = Common.SFD_FILE_TYPE_XLS,      // [ファイルの種類] の選択肢
                FilterIndex = 1,                        // [ファイルの種類] の既定値
                Title = Common.SFD_TITLE_SAVE,          // ダイアログのタイトル
                RestoreDirectory = true,                // ダイアログを閉じる前に現在のディレクトリを復元
                CheckFileExists = false,                // 存在しないファイル名前が指定されたとき警告を表示 (既定値: true)
                CheckPathExists = true                  // 存在しないパスが指定されたとき警告を表示 (既定値: true)
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;
                string filePath = sfd.FileName; // ファイルパスの取得
                try
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    if (fileInfo.Exists && fileInfo.IsReadOnly)
                    {
                        MessageBox.Show("指定されたファイルは読み取り専用です。");
                        return;
                    }

                    // Interop.Excel起動
                    cmn.Fa.ExcelApplication(false);
                    cmn.Fa.AddNewBook();

                    // Excelにぶち込み一旦保存
                    cmn.Fa.ExportExcel(exportDt, filePath);

                    // ExcelでPivotテーブルを作成

                    // Interop.Excel終了
                    cmn.Fa.CloseExcel2();

                    // Interop.Excelではなく標準アプリケーションで開く
                    Process.Start(@filePath);
                }
                catch (Exception ex) // 例外処理
                {
                    MessageBox.Show("ファイルの保存中にエラーが発生しました: " + ex.Message);
                    //toolStripStatusLabel2.Text = ex.Message;
                    cmn.Fa.CloseExcel2();
                }
            }
        }

        // EMデータテーブルとMPデータテーブルをマージ
        private void MargeOrderDataTable(ref DataTable exportDt, ref DataTable maektDt)
        {
            // EM注文データテーブルに列を追加
            exportDt.Columns.Add("前工程①", typeof(string));
            exportDt.Columns.Add("前工程②", typeof(string));

            // MPデータの内容をEM注文データにマージ
            foreach (DataRow r in exportDt.Rows)
            {
                string input = r["工程"].ToString();
                string pattern = @"-.*?:";
                string result = Regex.Replace(input, pattern, "-"); // MCCDをカット
                result = result.Replace("EX-", "");                 // EX工程をカット
                result = result.Substring(0, result.Length - 1);    // 最後のハイフンをカット
                r["工程"] = result;
                if (maektDt.Select($"HMCD='{r["品番"]}'").Length > 0)
                {
                    r["前工程①"] = maektDt.Select($"HMCD='{r["品番"]}'")[0]["前工程①"];
                    r["前工程②"] = maektDt.Select($"HMCD='{r["品番"]}'")[0]["前工程②"];
                }
                else
                {
                    r["前工程①"] = "";
                    r["前工程②"] = "";
                }
            }
        }

        private void Frm052_FormsPrinting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }
    }
}
