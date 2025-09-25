using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm073_EntryShipRes : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DataTable tanaInfoDt = new DataTable(); // EM在庫ファイルを保持

        public Frm073_EntryShipRes(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_073 + ": " + Common.FRM_NAME_073 + ">";

            // 共通クラス
            this.cmn = cmn;

            // 初期設定
            SetInitialValues();
        }

        // コントロールの初期化
        private async void SetInitialValues()
        {
            // 全画面表示
            // this.WindowState = FormWindowState.Maximized;

            // 初期化
            toolStripStatusLabel1.Text = string.Empty;

            // データベースからマスタを取得するタスクを登録
            int countPG = 0;
            await Task.Run(() => countPG = cmn.Dba.GetTanaInfo(ref tanaInfoDt, txtBUCD.Text));
            if (countPG < 0) return;

            // DataGridViewの初期設定
            Dgv_TanaInfo.DataSource = tanaInfoDt;
            // DataGridViewのヘッダー背景色を設定
            Dgv_TanaInfo.EnableHeadersVisualStyles = false;
            Dgv_TanaInfo.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            Dgv_TanaInfo.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            // DataGridViewの明細を2行毎に背景色設定
            Dgv_TanaInfo.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;

            //DataGridViewの画面ちらつきをおさえるため、DoubleBufferedを有効にする
            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(Dgv_TanaInfo, true, null);

            // 列幅を自動で合わせる
            Dgv_TanaInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // 件数を表示
            toolStripStatusLabel1.Text = (countPG) + "件を読み込みました。";

        }

        // 行番号を付ける
        private void Dgv_TanaInfo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // 行ヘッダのセル領域を、行番号を描画する長方形とする
            // （ただし右端に4ドットのすき間を空ける）
            Rectangle rect = new Rectangle(
              e.RowBounds.Location.X,
              e.RowBounds.Location.Y,
              Dgv_TanaInfo.RowHeadersWidth - 4,
              e.RowBounds.Height);

            // 上記の長方形内に行番号を縦方向中央＆右詰めで描画する
            // フォントや前景色は行ヘッダの既定値を使用する
            TextRenderer.DrawText(
              e.Graphics,
              (e.RowIndex + 1).ToString(),
              Dgv_TanaInfo.RowHeadersDefaultCellStyle.Font,
              rect,
              Dgv_TanaInfo.RowHeadersDefaultCellStyle.ForeColor,
              TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        // キーボードショートカット
        private void Frm073_EntryShipRes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) btnRefreshDataGridView_Click(sender, e);
            if (e.KeyCode == Keys.F10) btn_ExportExcel_Click(sender, e);
            if (e.KeyCode == Keys.Escape) Close();
        }

        // 再読み込み(F5)
        private async void btnRefreshDataGridView_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "再読み込み中...";
            Dgv_TanaInfo.DataSource = null;
            tanaInfoDt.Rows.Clear();

            // マスタ類の読込
            int countPG = 0;
            await Task.Run(() => countPG = cmn.Dba.GetTanaInfo(ref tanaInfoDt, txtBUCD.Text));
            if (countPG < 0) return;

            // DataGridViewの初期設定
            Dgv_TanaInfo.DataSource = tanaInfoDt;

            // 件数を表示
            toolStripStatusLabel1.Text = tanaInfoDt.Rows.Count + "件を読み込みました。";
        }

        // 外部出力（F10）
        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            if (tanaInfoDt.Rows.Count == 0)
            {
                MessageBox.Show("出力対象が存在しません．");
                return;
            }

            // 保存ダイアログ
            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                FileName = $"棚卸_{DateTime.Now.ToString("M")}",
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
                    cmn.Fa.ExcelApplication(false);
                    cmn.Fa.AddNewBook();
                    cmn.Fa.ExportExcel(tanaInfoDt, filePath);
                    cmn.Fa.CloseExcel2();
                    // Interop.Excelではなく標準アプリケーションで開く
                    Process.Start(@filePath);
                }
                catch (Exception ex) // 例外処理
                {
                    MessageBox.Show("ファイルの保存中にエラーが発生しました: " + ex.Message);
                    cmn.Fa.CloseExcel2();
                }
            }
        }

        // 小文字を大文字に変換
        private void txtBUCD_TextChanged(object sender, EventArgs e)
        {
            var selpos = txtBUCD.SelectionStart;
            var sellen = txtBUCD.SelectionLength;
            txtBUCD.Text = txtBUCD.Text.ToUpper();
            txtBUCD.SelectionStart = selpos;
            txtBUCD.SelectionLength = sellen;
        }

        // Enterキーで検索実行
        private void txtBUCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnRefreshDataGridView_Click(sender, e);
        }
    }
}
