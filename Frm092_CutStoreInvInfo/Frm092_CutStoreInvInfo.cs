using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm092_CutStoreInvInfo : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DataTable invInfoEMDt = new DataTable(); // EM在庫ファイルを保持
        private DataTable invInfoMPDt = new DataTable(); // MP在庫ファイルを保持

        // 自動で閉じるメッセージボックスで使用
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        private const uint WM_CLOSE = 0x0010;

        public Frm092_CutStoreInvInfo(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_092 + ": " + Common.FRM_NAME_092 + ">";

            // 共通クラス
            this.cmn = cmn;

            // 初期設定
            SetInitialValues();
        }

        // コントロールの初期化
        private async void SetInitialValues()
        {
            // 全画面表示
            this.WindowState = FormWindowState.Maximized;

            // 初期化
            toolStripStatusLabel1.Text = string.Empty;
            toolStripStatusLabel2.Text = string.Empty;

            // データベースからマスタを取得するタスクを登録
            bool retEM = false;
            bool retMP = false;
            var taskEM = Task.Run(() => retEM = cmn.Dba.GetInvInfoEMDt(ref invInfoEMDt));
            var taskMP = Task.Run(() => retMP = cmn.Dba.GetInvInfoMPDt(ref invInfoMPDt));
            await Task.WhenAll(taskEM, taskMP);
            if (retEM == false || retMP == false) return;
            // DataGridViewの初期設定
            Dgv_InvInfo.DataSource = invInfoMPDt;
            // DataGridViewのヘッダー背景色を設定
            Dgv_InvInfo.EnableHeadersVisualStyles = false;
            Dgv_InvInfo.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            Dgv_InvInfo.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            // DataGridViewの明細を2行毎に背景色設定
            Dgv_InvInfo.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;

            //DataGridViewの画面ちらつきをおさえるため、DoubleBufferedを有効にする
            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(Dgv_InvInfo, true, null);

            // データグリッドビューの個別設定
            DataGridDetailSetting();

            // EMの在庫数を追加で表示
            addEMQTY(ref invInfoEMDt);

            // 件数を表示
            toolStripStatusLabel1.Text = (Dgv_InvInfo.Rows.Count - 1) + "件を読み込みました。";

        }

        // データグリッドビューの個別設定
        private void DataGridDetailSetting()
        {
            // 列ヘッダーの文字列を文字位置を設定
            string[] s1 = {
                "品番",
                "在庫場所",
                "設備ｺｰﾄﾞ",
                "在庫数",
                "最終入庫日時",
                "最終出庫日時",
                "登録ID",
                "登録日時",
                "更新ID",
                "更新日時",
            };
            for (int i = 0; i < s1.Length; i++)
            {
                Dgv_InvInfo.Columns[i].HeaderText = s1[i];
            }

            // 在庫数 AlignRight設定
            Dgv_InvInfo.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            Dgv_InvInfo.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            // 在庫数のカンマ区切りフォーマット
            Dgv_InvInfo.Columns[3].DefaultCellStyle.Format = "#,0";

            // DataGridViewの幅を個別設定
            Dgv_InvInfo.Columns[0].Width = 230;         // 品番
            int[] datewidth = { 4, 5, 7, 9 };
            foreach (int i in datewidth) Dgv_InvInfo.Columns[i].Width = 130; // 日付
        }

        // EMの在庫数を追記
        private void addEMQTY(ref DataTable invInfoEMDt)
        {
            //DataGridViewTextBoxColumn列を作成する
            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn
            {
                // データソースの"Column1"をバインドする
                DataPropertyName = "EMQTY",
                // 名前とヘッダーを設定する
                Name = "EMQTY",
                HeaderText = "EMの\n在庫情報"
            };
            // 初回起動時のみ列を追加する
            if (Dgv_InvInfo.Columns.Count == invInfoMPDt.Columns.Count)
            {
                Dgv_InvInfo.Columns.Add(textColumn);
                // AlignRight設定
                Dgv_InvInfo.Columns[Dgv_InvInfo.Columns.Count - 1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                Dgv_InvInfo.Columns[Dgv_InvInfo.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                // EM在庫数のカンマ区切りフォーマット
                Dgv_InvInfo.Columns[Dgv_InvInfo.Columns.Count - 1].DefaultCellStyle.Format = "#,0";
            }
            foreach (DataGridViewRow r in Dgv_InvInfo.Rows)
            {
                DataRow[] em = invInfoEMDt.Select($"HMCD='{r.Cells["HMCD"].Value}'");
                if (em.Length == 1)
                {
                    r.Cells["EMQTY"].Value = em[0]["EMQTY"];
                }
                else
                {
                    var t = invInfoEMDt.Select($"HMCD='{r.Cells["HMCD"].Value}'")
                        .Where(w => w["KTCD"].ToString().StartsWith("MP"))
                        .Sum(s => int.Parse(s["EMQTY"].ToString()));
                    r.Cells["EMQTY"].Value = t + "(+)";
                }
            }
        }

        private void Dgv_InvInfo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // 行ヘッダのセル領域を、行番号を描画する長方形とする
            // （ただし右端に4ドットのすき間を空ける）
            Rectangle rect = new Rectangle(
              e.RowBounds.Location.X,
              e.RowBounds.Location.Y,
              Dgv_InvInfo.RowHeadersWidth - 4,
              e.RowBounds.Height);

            // 上記の長方形内に行番号を縦方向中央＆右詰めで描画する
            // フォントや前景色は行ヘッダの既定値を使用する
            TextRenderer.DrawText(
              e.Graphics,
              (e.RowIndex + 1).ToString(),
              Dgv_InvInfo.RowHeadersDefaultCellStyle.Font,
              rect,
              Dgv_InvInfo.RowHeadersDefaultCellStyle.ForeColor,
              TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void Dgv_InvInfo_Sorted(object sender, EventArgs e)
        {
            addEMQTY(ref invInfoEMDt);
        }

        private void txtHMCD_TextChanged(object sender, EventArgs e)
        {
            var selpos = txtHMCD.SelectionStart;
            var sellen = txtHMCD.SelectionLength;
            txtHMCD.Text = txtHMCD.Text.ToUpper();
            txtHMCD.SelectionStart = selpos;
            txtHMCD.SelectionLength = sellen;
            myFilter();
        }

        // 検索条件を設定
        private void myFilter()
        {
            string filter = (txtHMCD.Text.Length == 0) ? string.Empty :
                $"HMCD LIKE '{txtHMCD.Text}*'";
            //if (filter != string.Empty && cmbMaterial.SelectedIndex > 0)
            //    filter += " and ";
            //filter += (cmbMaterial.SelectedIndex <= 0) ? string.Empty :
            //    $"MATESIZE LIKE '{cmbMaterial.Text}*'";
            // 複数検索条件を設定
            invInfoMPDt.DefaultView.RowFilter = filter;
            addEMQTY(ref invInfoEMDt);
        }

        private void Frm092_CutStoreInvInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) btnReloadDatabase_Click(sender, e);
            if (e.KeyCode == Keys.F9) btnUpdateDatabase_Click(sender, e);
            if (e.KeyCode == Keys.Escape) Close();
        }

        private void Dgv_InvInfo_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textBox)
            {
                textBox.CharacterCasing = CharacterCasing.Upper;
            }
        }

        // コピペ処理実装
        private async void Dgv_InvInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                int row = Dgv_InvInfo.CurrentCell.RowIndex;                                         // 0 Start
                int basecol = Dgv_InvInfo.CurrentCell.ColumnIndex;                                  // 0 Start
                string hmcd = Dgv_InvInfo[0, row].Value.ToString();                                 // 品番を取得
                string[] records = Clipboard.GetText().Replace("\r", "").TrimEnd('\n').Split('\n'); // 複数行の貼り付け対応
                if (basecol == 0 && !Dgv_InvInfo.AllowUserToAddRows)
                {
                    MessageBox.Show("品番への貼り付けは出来ません．");
                    return;
                }
                if (row < Dgv_InvInfo.RowCount - 1 && records.Length > 1)
                {
                    MessageBox.Show("新規行以外で複数行の貼り付けは出来ません．");
                    return;
                }
                foreach (string r in records)
                {
                    int col = basecol;
                    string[] cells = r.Split('\t');                                         // タブ区切りを配列に変換
                    if (cells[0] == "") cells = cells.Skip(1).ToArray();                    // 先頭が空白だったらカット（データグリッド上のコピペ対応）

                    // 最終行への貼り付け処理
                    // ソースからどうやっても想定通り行追加がされないのでSendKeysに頼る！
                    // PCの処理能力によってwait時間を調整しないといけない問題あり！
                    // あと、ソースでのデバッグが出来なくなる（このエディタ上にSendKeysされる）！
                    if (row == Dgv_InvInfo.RowCount - 1 && Dgv_InvInfo.AllowUserToAddRows)
                    {
                        Dgv_InvInfo.CurrentCell = Dgv_InvInfo[1, row];
                        await Task.Delay(200);
                        SendKeys.Send("{Z}+{TAB}"); // +はShiftキー
                        await Task.Delay(800);
                    }

                    foreach (string s in cells)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            if (col == 0 && invInfoMPDt.Select($"HMCD='{s}'").Count() > 0)   // 同一品番のコピペ対応
                            {
                                Dgv_InvInfo[col, row].Value = s + " (2)";
                            }
                            else if (Dgv_InvInfo.Columns[col].DefaultCellStyle.Format == "#,0") // フォーマットがかかったInt型
                            {
                                Dgv_InvInfo[col, row].Value = s.Replace(",", "");       // 区切り文字をカット
                            }
                            else
                            {
                                Dgv_InvInfo[col, row].Value = s;
                            }
                        }
                        // 次のセルへ
                        col++;
                        if (col >= Dgv_InvInfo.ColumnCount - 1) break;
                    }
                    // RowStateの変更
                    DataRow[] dr = invInfoMPDt.Select($"HMCD='{hmcd}'");
                    if (dr.Length == 1) dr[0].EndEdit(); // ここでようやく RowState が Unchanged から Modified に変更される（コミットみたいなもの）

                    // クリップボード内で改行されていたら次の行へ
                    row++;
                }
            }
            // Deleteキー
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewCell c in Dgv_InvInfo.SelectedCells)
                {
                    switch (c.ColumnIndex)
                    {
                        case 0:
                            break;
                        case 1:
                        case 2:
                            c.Value = "";
                            break;
                        case 3:
                            c.Value = 0;
                            break;
                        default:
                            c.Value = DBNull.Value;
                            break;
                    }
                }
            }
        }

        // データベース反映 (F9)
        private async void btnUpdateDatabase_Click(object sender, EventArgs e)
        {
            int insertCount = 0;
            int modifyCount = 0;
            int deleteCount = 0;
            foreach (DataRow r in invInfoMPDt.Rows)
            {
                if (r.RowState == DataRowState.Added) insertCount++;
                if (r.RowState == DataRowState.Modified) modifyCount++;
                if (r.RowState == DataRowState.Deleted) deleteCount++;
            }
            // 一括更新
            if (insertCount + modifyCount + deleteCount > 0)
            {
                if (!cmn.Dba.UpdateInventory(ref invInfoMPDt)) return;
                invInfoMPDt.AcceptChanges(); // これを実行しないと何回も更新されてしまう
                await Task.Run(() =>
                {
                    toolStripStatusLabel1.Text = (insertCount > 0) ? $"{insertCount}件 を追加 " : "";
                    toolStripStatusLabel1.Text += (modifyCount > 0) ? $"{modifyCount}件 を更新 " : "";
                    toolStripStatusLabel1.Text += (deleteCount > 0) ? $"{deleteCount}件 を削除 " : "";
                    toolStripStatusLabel1.Text += "しました．";
                });

                // 新しいスレッドを作成
                Thread thread = new Thread(ShowMessageBox);
                thread.Start();
                Thread.Sleep(1500);
                IntPtr hWnd = FindWindow(null, "マスタ更新");
                SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
            else
            {
                MessageBox.Show("更新はありませんでした．");
            }
        }

        // 自動で閉じるメッセージボックス
        private static void ShowMessageBox()
        {
            MessageBox.Show("更新が終了しました．", "マスタ更新");
        }

        // 再読み込み (F5)
        private void btnReloadDatabase_Click(object sender, EventArgs e)
        {
            txtHMCD.Text = string.Empty;
            invInfoMPDt.Clear();
            if (!cmn.Dba.GetInvInfoMPDt(ref invInfoMPDt)) return;
            // EMの在庫数を追加で表示
            addEMQTY(ref invInfoEMDt);
            Dgv_InvInfo.Refresh();
            Dgv_InvInfo.Focus();
            // 件数を表示
            toolStripStatusLabel1.Text = (Dgv_InvInfo.Rows.Count - 1) + "件を読み込みました。";
        }

        // 右クリックでクリップボードにコピー
        private void Dgv_InvInfo_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Dgv_InvInfo.SelectedRows.Count > 0)
                {
                    List<string> selectedData = new List<string>();
                    foreach (DataGridViewRow selectedRow in Dgv_InvInfo.SelectedRows)
                    {
                        List<string> rowData = new List<string>();
                        foreach (DataGridViewCell cell in selectedRow.Cells)
                        {
                            if (cell != null)
                                rowData.Add((string.IsNullOrEmpty(cell.Value.ToString())) ? "" : cell.Value.ToString());
                        }
                        selectedData.Add(string.Join("\t", rowData));
                    }
                    Clipboard.SetText(string.Join("\n", selectedData));
                    toolStripStatusLabel2.Text = $"選択行をクリップしました．";
                }
                else
                {
                    int col = Dgv_InvInfo.CurrentCell.ColumnIndex;
                    string ht = Dgv_InvInfo.Columns[col].HeaderText;
                    string val = Dgv_InvInfo[col, e.RowIndex].Value.ToString();
                    if (val != "")
                    {
                        Clipboard.SetText(val);
                        toolStripStatusLabel2.Text = $"{ht} [ {val} ] をクリップしました．";
                    }
                }
            }
        }

        // 検索条件クリア
        private void btnFilterClear_Click(object sender, EventArgs e)
        {
            txtHMCD.Text = string.Empty;
        }

        // 品番入力欄にペースト
        private void btnHMCDPaste_Click(object sender, EventArgs e)
        {
            txtHMCD.Text = Clipboard.GetText().Replace("\r\n", "");
        }

    }
}
