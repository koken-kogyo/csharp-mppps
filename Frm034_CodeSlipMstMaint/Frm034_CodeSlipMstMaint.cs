using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.Common;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace MPPPS
{
    public partial class Frm034_CodeSlipMstMaint : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DataTable equipMstDt = new DataTable(); // 設備マスタを保持
        private DataTable codeSlipDt = new DataTable(); // コード票マスタを保持
        private bool eventFlg = false;
        private int errorRow = 0;
        private static System.Timers.Timer timer;

        // 列番号定数
        private static int cColKTSU = 24;
        private static int cColKTKEY = 38;

        // 自動で閉じるメッセージボックスで使用
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        private const uint WM_CLOSE = 0x0010;

        public Frm034_CodeSlipMstMaint(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_034 + ": " + Common.FRM_NAME_034 + ">";

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

            // ステータスクリア
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel2.Text = "";

            // トグルボタンを標準表示側に設定
            tglViewNormal.Select();

            // ラジオボタンを全てに設定
            radioButton1.Checked = true;

            // 次の相違点ボタンを非活性化
            errorRow = 0;
            btnNextDiffer.BackColor = SystemColors.Control;
            btnNextDiffer.Enabled = false;

            // マスタ類の読込
            await ReadCodeSlipToDataGridView();

        }

        // コンストラクタ後のロードで画面をアクティブ化し初期フォーカス
        private void Frm034_CodeSlipMstMaint_Load(object sender, EventArgs e)
        {
            this.Activate();
            this.Dgv_CodeSlipMst.Focus();
        }

        // DataGridViewへのデータの読込（初期時と再読み込み時）
        private async Task ReadCodeSlipToDataGridView()
        {
            // データベースからマスタを取得するタスクを登録
            bool ret8420 = false;
            bool ret8430 = false;
            var task8420 = Task.Run(() => ret8420 = cmn.Dba.GetEquipMstDt(ref equipMstDt));
            var task8430 = Task.Run(() => ret8430 = cmn.Dba.GetCodeSlipMst(ref codeSlipDt));
            await Task.WhenAll(task8420, task8430);
            if (ret8420 == false || ret8430 == false) return;

            // DataGridViewの初期設定
            Dgv_CodeSlipMst.DataSource = codeSlipDt;
            // DataGridViewのヘッダー背景色を設定
            Dgv_CodeSlipMst.EnableHeadersVisualStyles = false;
            Dgv_CodeSlipMst.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            Dgv_CodeSlipMst.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            // DataGridViewの明細を2行毎に背景色設定
            Dgv_CodeSlipMst.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            // DataGridViewの必要な列幅を初期設定
            Dgv_CodeSlipMst.Columns[0].Width = 200;
            Dgv_CodeSlipMst.Columns[1].Width = 200; //HMNM
            //DataGridViewの画面ちらつきをおさえるため、DoubleBufferedを有効にする
            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(Dgv_CodeSlipMst, true, null);
            // ヘッダータイトルを日本語に変更
            columnHeaderText();
            // 件数を表示
            toolStripStatusLabel1.Text = (Dgv_CodeSlipMst.Rows.Count - 1) + "件を読み込みました。";
        }

        // 再読み込み(F5)
        private async void btnRefreshDataGridView_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "再読み込み中...";
            Dgv_CodeSlipMst.DataSource = null;
            equipMstDt.Rows.Clear();
            codeSlipDt.Rows.Clear();
            txtHMCD.Text = string.Empty;
            tglViewNormal.Select();
            // マスタ類の読込
            await ReadCodeSlipToDataGridView();
            errorRow = 0;
            btnNextDiffer.BackColor = SystemColors.Control;
            btnNextDiffer.Enabled = false;
        }

        // 行番号をつける
        private void Dgv_CodeSlipMst_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // 行ヘッダのセル領域を、行番号を描画する長方形とする
            // （ただし右端に4ドットのすき間を空ける）
            Rectangle rect = new Rectangle(
              e.RowBounds.Location.X,
              e.RowBounds.Location.Y,
              Dgv_CodeSlipMst.RowHeadersWidth - 4,
              e.RowBounds.Height);

            // 上記の長方形内に行番号を縦方向中央＆右詰めで描画する
            // フォントや前景色は行ヘッダの既定値を使用する
            TextRenderer.DrawText(
              e.Graphics,
              (e.RowIndex + 1).ToString(),
              Dgv_CodeSlipMst.RowHeadersDefaultCellStyle.Font,
              rect,
              Dgv_CodeSlipMst.RowHeadersDefaultCellStyle.ForeColor,
              TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        // 検索キーの品番入力
        private void txtHMCD_TextChanged(object sender, EventArgs e)
        {
            if (txtHMCD.TextLength > 24)
                txtHMCD.Text = txtHMCD.Text.TrimStart('\t').Split('\t')[0];
            var selpos = txtHMCD.SelectionStart;
            var sellen = txtHMCD.SelectionLength;
            txtHMCD.Text = txtHMCD.Text.ToUpper();
            txtHMCD.SelectionStart = selpos;
            txtHMCD.SelectionLength = sellen;
            myFilter();
        }

        // 検索キーの設備コード入力
        private void txtKTKEY_TextChanged(object sender, EventArgs e)
        {
            var selpos = txtKTKEY.SelectionStart;
            var sellen = txtKTKEY.SelectionLength;
            txtKTKEY.Text = txtKTKEY.Text.ToUpper();
            txtKTKEY.SelectionStart = selpos;
            txtKTKEY.SelectionLength = sellen;
            myFilter();
        }

        // 検索キーの材料選択
        private void cmbMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMaterial.SelectedIndex == -1) return;
            myFilter();
        }

        // 検索条件を設定
        private void myFilter()
        {
            string filter = (txtHMCD.TextLength == 0) ? string.Empty :
                $"HMCD LIKE '{txtHMCD.Text}*'";
            if (txtKTKEY.TextLength > 0)
            {
                if (filter != string.Empty) filter += " and ";
                filter += $"KTKEY LIKE '*{txtKTKEY.Text}*'";
            }
            if (cmbMaterial.SelectedIndex > 0)
            {
                if (filter != string.Empty) filter += " and ";
                filter += $"MATESIZE LIKE '{cmbMaterial.Text}*'";
            }
            // 工程を指定された場合、複数のLIKE条件を指定
            if (radioButton2.Checked) // 手動機
            {
                if (filter != string.Empty) filter += " and ";
                filter += "(KTKEY LIKE '%SW%' OR KTKEY LIKE '%NC%' OR KTKEY LIKE '%MC%' OR KTKEY LIKE '%LF%' " + 
                    "OR KTKEY LIKE '%G-%' OR KTKEY LIKE '%MD%' OR KTKEY LIKE '%TP%')";
            }
            if (radioButton3.Checked) // 自動機
            {
                if (filter != string.Empty) filter += " and ";
                filter += "(KTKEY LIKE '%SS%' OR KTKEY LIKE '%XT%' OR KTKEY LIKE '%CN%' OR KTKEY LIKE '%MS%' " +
                    "OR KTKEY LIKE '%SK%' OR KTKEY LIKE '%TN%')";
            }
            // 複数検索条件を設定
            codeSlipDt.DefaultView.RowFilter = filter;
            Dgv_CodeSlipMst.AllowUserToAddRows = (filter == "") ? true : false; // 新規追加が出来るのはフィルターなしの時だけ
            // 抽出結果をステータスに表示
            toolStripStatusLabel1.Text = (filter == string.Empty) 
                ? $"{codeSlipDt.Rows.Count}件 全てを表示しています．"
                : $"{Dgv_CodeSlipMst.RowCount}件 抽出しました．";
        }

        // 標準ビュートグルボタン
        private void tglViewNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (tglViewNormal.Checked)
            {
                tglViewNormal.BackColor = Color.LightSkyBlue;
                tglViewSimple.BackColor = SystemColors.Control;
                viewChange();
            }
        }

        // 簡易ビュートグルボタン
        private void tglViewSimple_CheckedChanged(object sender, EventArgs e)
        {
            if (tglViewSimple.Checked)
            {
                tglViewSimple.BackColor = Color.LightSkyBlue;
                tglViewNormal.BackColor = SystemColors.Control;
                // ビューの切り替え
                viewChange();
            }
        }


        // 列ヘッダーの文字列変更
        private void columnHeaderText()
        {
            // 列ヘッダーの文字列を文字位置を設定
            var offset = 0;
            string[] s1 = { 
                "品番", 
                "品名", 
                "材料", 
                "全長", 
                "収容数", 
                "ﾁｪｯｸ", 
                "協力" };
            for (int i = 0; i < s1.Length; i++)
            {
                Dgv_CodeSlipMst.Columns[i + offset].HeaderText = s1[i];
                if (i == 2 || i == 3 || i == 4)
                {
                    Dgv_CodeSlipMst.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Dgv_CodeSlipMst.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Dgv_CodeSlipMst.Columns[i].Width = 60;
                }
            }
            offset += s1.Length;

            // 設備マスタの設備グループ名を取得
            var s2 = equipMstDt.AsEnumerable()
                .OrderBy(x => x["MCGSEQ"])
                .GroupBy(grp => new { MCGCD = grp["MCGCD"].ToString() })
                .Select(x => x.Key.MCGCD)
                .ToArray();
            if (s2.Length != 17 && s2.Length != 18) // 工程G[EX]を新規追加 2025.04.03
            {
                MessageBox.Show("設備マスタに異常があります（列が17個ではない)");
                return;
            }
            for (int i = 0; i < 17; i++)
            {
                Dgv_CodeSlipMst.Columns[i + offset].HeaderText = s2[i];
                Dgv_CodeSlipMst.Columns[i + offset].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Dgv_CodeSlipMst.Columns[i + offset].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Dgv_CodeSlipMst.Columns[i + offset].Width = 40;
            }
            offset += 17; // 廃止⇒s2.Length; 2025.04.03

            // サイクルタイムとその他情報
            string[] s3 = {
                "工程数",
                "CTSWCN", 
                "CTONSKMS", 
                "CTMC", 
                "CTNC", 
                "CTSSTN", 
                "CTXT",
                "ｽﾄｱ",
                "備考",
                "HT",
                "母材品番",
                "母材略称",
                "母材長さ",
                "容器",
                "検索ｷｰ" 
            };
            for (int i = 0; i < s3.Length; i++)
            {
                Dgv_CodeSlipMst.Columns[i + offset].HeaderText = s3[i];
                if (1 <= i && i <= 6)
                {
                    Dgv_CodeSlipMst.Columns[i + offset].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Dgv_CodeSlipMst.Columns[i + offset].Width = 40;
                    
                }
                if (i == 12) // 母材長さ
                {
                    Dgv_CodeSlipMst.Columns[i + offset].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Dgv_CodeSlipMst.Columns[i + offset].Width = 60;
                    Dgv_CodeSlipMst.Columns[i + offset].DefaultCellStyle.Format = "#,0";
                }
                if (i == 0 || i == 7 || i == 13) // 工程数, ストア, 容器
                {
                    Dgv_CodeSlipMst.Columns[i + offset].Width = 40;
                }
                if (i == 14) // 検索ｷｰ
                {
                    Dgv_CodeSlipMst.Columns[i + offset].Width = 180;
                }
            }
            offset += s3.Length;

            // 新システム用工程経路情報
            string[] ktseq = { "０", "１", "２", "３", "４", "５", "６" };
            for (int j = 1; j <= 6; j++)
            {
                string[] s4 = {
                    $"工程{ktseq[j]}",
                    $"設備{ktseq[j]}", 
                    $"CT{j}", 
                    $"LOT{j}", 
                    $"ID{j}" 
                };
                for (int i = 0; i < s4.Length; i++)
                {
                    Dgv_CodeSlipMst.Columns[i + offset].HeaderText = s4[i];
                    Dgv_CodeSlipMst.Columns[i + offset].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Dgv_CodeSlipMst.Columns[i + offset].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    if (i <= 2) Dgv_CodeSlipMst.Columns[i + offset].Width = 40;
                    if (i >= 3) Dgv_CodeSlipMst.Columns[i + offset].Width = 50;
                    if (j % 2 != 0)
                        Dgv_CodeSlipMst.Columns[i + offset].DefaultCellStyle.BackColor = Color.LightCyan;
                }
                offset += s4.Length;
            }

        }

        // トグルボタンで標準ビューと簡易ビューの切り替え
        private void viewChange()
        {
            if (Dgv_CodeSlipMst.Columns.Count == 0) return;
            bool v = (tglViewSimple.Checked) ? false : true;
            Dgv_CodeSlipMst.Columns[5].Visible = v;        // ﾁｪｯｸ
            Dgv_CodeSlipMst.Columns[6].Visible = v;        // 協力
            // Excelで使用している列
            for (int i = 7; i < 31; i++)
                Dgv_CodeSlipMst.Columns[i].Visible = v;
            // 工程１～工程６ A5M2の行番号43,44,48,49,53,54,58,59,63,64,68,69,
            for (int i = 42; i < 68; i = i + 5)
            {
                Dgv_CodeSlipMst.Columns[i + 0].Visible = v; // LOT
                //Dgv_CodeSlipMst.Columns[i + 1].Visible = v; // 帳票定義ID
            }
            Dgv_CodeSlipMst.Columns[cColKTSU].Visible = true;     // 工程数
            Dgv_CodeSlipMst.Columns[31].Visible = v;        // ｽﾄｱ
            //Dgv_CodeSlipMst.Columns[32].Visible = v;        // 備考
            Dgv_CodeSlipMst.Columns[33].Visible = v;        // HT
            Dgv_CodeSlipMst.Columns[34].Visible = v;        // 母材品番
            Dgv_CodeSlipMst.Columns[35].Visible = v;        // 母材略称
            Dgv_CodeSlipMst.Columns[36].Visible = v;        // 母材長さ
            Dgv_CodeSlipMst.Columns[37].Visible = v;        // 容器
            //Dgv_CodeSlipMst.Columns[38].Visible = v;        // 工程検索キー
            Dgv_CodeSlipMst.Columns[69].Visible = false;    // INSTID
            Dgv_CodeSlipMst.Columns[70].Visible = false;    // INSTDT
            Dgv_CodeSlipMst.Columns[71].Visible = false;    // UPDTID
            Dgv_CodeSlipMst.Columns[72].Visible = false;    // UPDTDT
        }

        // 検索条件クリア
        private void btnFilterClear_Click(object sender, EventArgs e)
        {
            cmbMaterial.SelectedIndex = -1;
            txtKTKEY.Text = string.Empty;
            txtHMCD.Text = string.Empty;
        }

        // データベース反映
        private async void btnUpdateDatabase_Click(object sender, EventArgs e)
        {
            int insertCount = 0;
            int modifyCount = 0;
            foreach (DataRow r in codeSlipDt.Rows)
            {
                if (r.RowState == DataRowState.Added) insertCount++;
                if (r.RowState == DataRowState.Modified) modifyCount++;
            }
            // 一括更新
            if (insertCount + modifyCount > 0)
            {
                cmn.Dba.UpdateCodeSlipMst(ref codeSlipDt);
                codeSlipDt.AcceptChanges(); // これを実行しないと何回も更新されてしまう
                await Task.Run(() =>
                {
                    toolStripStatusLabel1.Text = (insertCount > 0) ? $"{insertCount}件 を追加 " : "";
                    toolStripStatusLabel1.Text += (modifyCount > 0) ? $"{modifyCount}件 を更新 " : "";
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

        // 削除
        private void btn_HMCDDelete_Click(object sender, EventArgs e)
        {
            if (Dgv_CodeSlipMst.SelectedRows.Count == 0)
            {
                MessageBox.Show("削除する行を選択してください．");
                return;
            }
            if (MessageBox.Show("元に戻せません。本当に削除してよろしいですか？", "最終質問",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;
            int deleteCount = 0;
            // 選択行の削除
            foreach (DataGridViewRow r in Dgv_CodeSlipMst.SelectedRows)
            {
                string hmcd = r.Cells[0].Value.ToString();
                codeSlipDt.Select($"HMCD='{hmcd}'")[0].Delete();
                deleteCount++;
            }
            // 一括更新
            if (deleteCount > 0)
            {
                codeSlipDt.AcceptChanges();
                cmn.Dba.UpdateCodeSlipMst(ref codeSlipDt);
                toolStripStatusLabel1.Text = $"{deleteCount}件 を削除しました.";
            }
        }

        // 新システム用に変換（個別明細選択）
        private void btnConvertMP_Click(object sender, EventArgs e)
        {
            if (Dgv_CodeSlipMst.SelectedRows.Count == 0)
            {
                MessageBox.Show("変換する行を選択してください．");
                return;
            }
            foreach (DataGridViewRow r in Dgv_CodeSlipMst.SelectedRows)
            {
                setKTKEY(r.Index);
            }
            MessageBox.Show("Excelの旧コード票を単純に連結しただけです。\n\n" +
                "①実際の工程順序\n" + 
                "②バリ取り工程、面取り工程の有無等は\n" + 
                "担当者に確認してください。", "",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void setKTKEY(int row)
        {
            string searchKeys = ""; // [MCGCD-MCCD:]
            int ktsu = 0;
            int col = cColKTKEY + 1; // [工程１設備グループ]の列番号 A5M2の40行目
            for (int j = 7; j <= 23; j++) //7:SW ～ 23:TN
            {
                if (Dgv_CodeSlipMst[j, row].Value != null && 
                    Dgv_CodeSlipMst[j, row].Value != DBNull.Value)
                {
                    if (string.IsNullOrEmpty(Dgv_CodeSlipMst[j, row].Value.ToString()) == false)
                    {
                        var mcgcd = Dgv_CodeSlipMst.Columns[j].HeaderText;
                        var val = Dgv_CodeSlipMst[j, row].Value;
                        DataRow[] equip = equipMstDt.Select($"MCGCD='{mcgcd}' and MCCD='{val}'"); // DataGrid上の設備コードから設備名称取得
                        if (equip.Length == 1)
                        {
                            Dgv_CodeSlipMst[col + 0, row].Value = mcgcd;
                            Dgv_CodeSlipMst[col + 1, row].Value = equip[0]["MCCD"];
                            searchKeys += mcgcd + "-" + val + ":";
                            ktsu++;
                            col += 5;
                        }
                        else
                        {
                            Dgv_CodeSlipMst[j, row].Style.BackColor = Color.Red;
                        }
                    }
                }
            }
            // key
            if (searchKeys.Length > 0)
            {
                Dgv_CodeSlipMst[cColKTSU, row].Value = ktsu;                // ここでDataGridとDataTableの両者が変更されるが
                Dgv_CodeSlipMst[cColKTKEY, row].Value = searchKeys;         // RowStateは変更されない（トランザクションみたいなもの）
                string hmcd = Dgv_CodeSlipMst[0, row].Value.ToString();
                DataRow[] dr = codeSlipDt.Select($"HMCD='{hmcd}'");
                if (dr.Length == 1) dr[0].EndEdit();                        // ここでようやく RowState が Unchanged から Modified に変更される（コミットみたいなもの）
            }
        }

        // 新たに工程設計した工程１～６までの工程数と検索キーを自動作成してデータベース反映
        private async void btnUpdateDatabase2_Click(object sender, EventArgs e)
        {
            // 表示されている件数が100件を超える場合は必ず選択させる
            if (Dgv_CodeSlipMst.SelectedRows.Count == 0 && Dgv_CodeSlipMst.RowCount > 100)
            {
                MessageBox.Show("変換する行を選択してください．");
                return;
            }
            foreach (DataGridViewRow r in Dgv_CodeSlipMst.Rows)
            {
                if (r.Visible || r.Selected)
                {
                    var hmcd = r.Cells[0].Value;
                    DataRow[] dr = codeSlipDt.Select($"HMCD='{hmcd}'");
                    if (dr.Length == 1)
                    {
                        string ktkey = "";
                        int ktsu = 1;
                        int col = 39; // [工程１設備グループ]の列番号 A5M2のカラムタブ№40-1
                        while (ktsu <= 6)
                        {
                            ktkey += r.Cells[col].Value.ToString();
                            ktkey += "-";
                            ktkey += r.Cells[col + 1].Value.ToString();
                            ktkey += ":";
                            if (r.Cells[col + 5].Value.ToString() == "") break;
                            col += 5;
                            ktsu++;
                        }
                        if (dr[0][cColKTSU].ToString() != ktsu.ToString()) dr[0][cColKTSU] = ktsu;
                        if (dr[0][cColKTKEY].ToString() != ktkey) dr[0][cColKTKEY] = ktkey;
                    }
                }
            }
            int modifyCount = 0;
            foreach (DataRow r in codeSlipDt.Rows)
            {
                if (r.RowState == DataRowState.Modified) modifyCount++;
            }
            // 一括更新
            if (modifyCount > 0)
            {
                cmn.Dba.UpdateCodeSlipMst(ref codeSlipDt);
                codeSlipDt.AcceptChanges(); // これを実行しないと何回も更新されてしまう
                await Task.Run(() =>
                {
                    toolStripStatusLabel1.Text = $"{modifyCount}件 を更新しました．";
                });

                // 新しいスレッドを作成
                Thread thread = new Thread(ShowMessageBox);
                thread.Start();
                Thread.Sleep(800);
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

        // 最新のコード票マスタを読み込み変更点をチェック
        private async void btnReadExcelMaster_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine($"計測開始[DialogBox]");

            // OpenFileDialog クラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog()
            {
                FileName = "",                           // 既定のファイル名
                InitialDirectory = Common.OFD_INIT_DIR,  // 既定のディレクトリ名
                Filter = Common.OFD_FILE_TYPE_XLS,       // [ファイルの種類] の選択肢
                FilterIndex = 1,                         // [ファイルの種類] の既定値
                Title = Common.OFD_TITLE_OPEN,           // ダイアログのタイトル
                RestoreDirectory = true,                 // ダイアログを閉じる前に現在のディレクトリを復元
                CheckFileExists = true,                  // 存在しないファイル名前が指定されたとき警告を表示 (既定値: true)
                CheckPathExists = true                   // 存在しないパスが指定されたとき警告を表示 (既定値: true)
            };

            //計測結果表示
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            Console.WriteLine($"{ts.TotalSeconds:0.00}");

            // ダイアログを表示
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // [開く] ボタンがクリックされたとき、選択されたファイル名を表示
                toolStripStatusLabel1.Text = $"[{ofd.FileName}]を解析中... しばらくお待ちください。";

                // 次の相違点ボタンを非活性化
                btnNextDiffer.BackColor = SystemColors.Control;
                btnNextDiffer.Enabled = false;
                Dgv_CodeSlipMst.FirstDisplayedScrollingRowIndex = 0;
                errorRow = 0;

                //cmn.Fa.OpenExcelFile(ofd.FileName);

                // Excelコード票を読み込みデータテーブルに変換
                DataTable excelCodeSlipDt = new DataTable();
                await Task.Run(() => excelCodeSlipDt = cmn.Fa.ReadExcelToDatatble2(ofd.FileName));

                if (excelCodeSlipDt == null) return;
                if (excelCodeSlipDt.Rows.Count <= 0) return;

                // コード票マスタとExcelコード票とを比較
                int differCellCount = 0;
                for (int row = 0; row < Dgv_CodeSlipMst.Rows.Count - 1; row++) // 新規行は除く
                {
                    try
                    {
                        string s = Dgv_CodeSlipMst[0, row].Value.ToString();
                        DataRow[] excelDr = excelCodeSlipDt.Select($"品番='{s}'");
                        if (excelDr.Length != 1) continue;
                        for (int col = 0; col < excelCodeSlipDt.Columns.Count; col++)
                        {
                            //if (codeSlipDt.Columns[col].DataType.ToString() == "System.Decimal") // decimal型に変換（変換に神経を使って嫌だDBNullとか分けわからん）
                            if (col == 3) // 長さ: Length: Decimal
                            {
                                decimal xslsnum;
                                if (!decimal.TryParse(excelDr[0][col].ToString(), out xslsnum))
                                    xslsnum = 0;
                                decimal dbnum;
                                if (!decimal.TryParse(Dgv_CodeSlipMst[col, row].Value.ToString(), out dbnum))
                                    dbnum = 0;
                                // decimal型で比較
                                if (xslsnum != dbnum)
                                {
                                    Dgv_CodeSlipMst[col, row].Style.BackColor = Color.LightCoral;
                                    differCellCount++;
                                }
                            }
                            else if (col == cColKTSU) // Excelコード票の工程数と新システムでの工程数で意味合いが違うためチェック対象外
                            {
                                continue;
                            }
                            else if (Dgv_CodeSlipMst.Columns[col].DefaultCellStyle.Format == "#,0")
                            {
                                if (excelDr[0][col].ToString().Replace(",", "") !=
                                    Dgv_CodeSlipMst[col, row].Value.ToString().Replace(",", ""))
                                {
                                    Dgv_CodeSlipMst[col, row].Style.BackColor = Color.LightCoral;
                                    differCellCount++;
                                }

                            }
                            else if (excelDr[0][col].ToString() != Dgv_CodeSlipMst[col, row].Value.ToString())
                            {
                                Dgv_CodeSlipMst[col, row].Style.BackColor = Color.LightCoral;
                                differCellCount++;
                            }
                        }
                    }
                    catch (Exception ex) {
                        toolStripStatusLabel2.Text = row + "行 [" + Dgv_CodeSlipMst[0,row].Value.ToString() + "] で異常が発生しました．";
                    }

                }

                // 追加削除件数の調査
                int offset = (Dgv_CodeSlipMst.AllowUserToAddRows) ? 1 : 0;
                int differRowCount = (Dgv_CodeSlipMst.RowCount - offset) - excelCodeSlipDt.Rows.Count;
                if (differRowCount != 0)
                {
                    List<string> list1 = GetColumnData(0); // Dgv_CodeSlipMstの1番目の列（品番）を取得
                    List<string> list2 = excelCodeSlipDt.AsEnumerable()
                        .Select(c => c["品番"].ToString())
                        .ToList();
                    // 対象差集合を求める（「Aに属しBに属さないもの」と「Bに属しAに属さないもの」の両方を合わせた集合）
                    // https://qiita.com/nkojima/items/575a1e5879d62441662d
                    var set = list1.Except(list2).Union(list2.Except(list1));
                    Debug.WriteLine("対象差：" + string.Join(",", set));
                    Clipboard.SetText(string.Join("\n", set));
                    toolStripStatusLabel2.Text = "対象品番をクリップしました(" +
                        $"Excel {list2.Count.ToString("#,0")}件 - " + 
                        $"新システム {list1.Count.ToString("#,0")}件)";
                }

                // 結果表示
                if (differCellCount > 0)
                {
                    // 次の相違点ボタンを活性化
                    btnNextDiffer.BackColor = Color.LightCoral;
                    btnNextDiffer.Enabled = true;
                    toolStripStatusLabel1.Text = $"{differCellCount.ToString("#,0")}件の相違を検出しました。";
                }
                else if (differRowCount > 0)
                {
                    toolStripStatusLabel1.Text = "追加または削除を検出しました。";
                }
                else
                {
                    toolStripStatusLabel1.Text = "相違はありませんでした。";
                }
                ofd.Dispose();
                MessageBox.Show("変更点のチェックが完了しました。");
            }
        }
        // 次の相違点ボタン
        private void btnNextDiffer_Click(object sender, EventArgs e)
        {
            if (NextError() == false)
            {
                var msg = "検索が終了しました。\n\n先頭から再度実行しますか？";
                if (MessageBox.Show(msg, "質問", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    == DialogResult.No) return;
                //Dgv_CodeSlipMst.FirstDisplayedScrollingRowIndex = 0;
                errorRow = 0;
                NextError();
            }
        }
        private bool NextError()
        {
            var hit = false;
            var startRow = errorRow + 1;
            for (int row = startRow; row < Dgv_CodeSlipMst.Rows.Count - 1; row++)
            {
                for (int col = 0; col < Dgv_CodeSlipMst.Columns.Count; col++)
                {
                    if (Dgv_CodeSlipMst[col, row].Style.BackColor == Color.LightCoral)
                    {
                        Dgv_CodeSlipMst.FirstDisplayedScrollingRowIndex = row - 1;
                        errorRow = row;
                        hit = true;
                        break;
                    }
                }
                if (hit) break;
            }
            return hit;
        }
        // DataGridViewの特定の列をListに変換
        private List<string> GetColumnData(int columnIndex)
        {
            List<string> columnData = new List<string>();

            foreach (DataGridViewRow row in Dgv_CodeSlipMst.Rows)
            {
                if (row.Cells[columnIndex].Value != null)
                {
                    columnData.Add(row.Cells[columnIndex].Value.ToString());
                }
            }

            return columnData;
        }

        // 右クリックでクリップボードにコピー
        private void Dgv_CodeSlipMst_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Dgv_CodeSlipMst.SelectedRows.Count > 0)
                {
                    List<string> selectedData = new List<string>();
                    foreach (DataGridViewRow selectedRow in Dgv_CodeSlipMst.SelectedRows)
                    {
                        List<string> rowData = new List<string>();
                        foreach (DataGridViewCell cell in selectedRow.Cells)
                        {
                            rowData.Add((string.IsNullOrEmpty(cell.Value.ToString())) ? "" : cell.Value.ToString());
                        }
                        selectedData.Add(string.Join("\t", rowData));
                    }
                    Clipboard.SetText(string.Join("\n", selectedData));
                    toolStripStatusLabel2.Text = $"選択行をクリップしました．";
                }
                else
                {
                    int col = Dgv_CodeSlipMst.CurrentCell.ColumnIndex;
                    string ht = Dgv_CodeSlipMst.Columns[col].HeaderText;
                    string val = Dgv_CodeSlipMst[col, e.RowIndex].Value.ToString();
                    if (val != "")
                    {
                        Clipboard.SetText(val);
                        toolStripStatusLabel2.Text = $"{ht} [ {val} ] をクリップしました．";
                    }
                }
            }
        }

        // 品番入力欄にペースト
        private void btnHMCDPaste_Click(object sender, EventArgs e)
        {
            txtHMCD.Text = Clipboard.GetText().Replace("\r\n", "");
        }

        // データグリッド上のキーダウンイベント（ペースト、削除）
        private async void Dgv_CodeSlipMst_KeyDown(object sender, KeyEventArgs e)
        {
            if (eventFlg) return;

            // 貼り付け Ctrl + V
            if (e.Control && e.KeyCode == Keys.V)   
            {
                eventFlg = true;
                int row = Dgv_CodeSlipMst.CurrentCell.RowIndex;                             // 0 Start
                int basecol = Dgv_CodeSlipMst.CurrentCell.ColumnIndex;                      // 0 Start
                string hmcd = Dgv_CodeSlipMst[0, row].Value.ToString();                     // 品番を取得
                string[] records = Clipboard.GetText().Replace("\r", "").TrimEnd('\n').Split('\n');       // 複数行の貼り付け対応
                if (basecol == 0 && !Dgv_CodeSlipMst.AllowUserToAddRows)
                {
                    MessageBox.Show("品番への貼り付けは出来ません．");
                    return;
                }
                if (row < Dgv_CodeSlipMst.RowCount - 1 && records.Length > 1)
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
                    if (row == Dgv_CodeSlipMst.RowCount - 1 && Dgv_CodeSlipMst.AllowUserToAddRows)
                    {
                        Dgv_CodeSlipMst.CurrentCell = Dgv_CodeSlipMst[1, row];
                        await Task.Delay(200);
                        SendKeys.Send("{Z}+{TAB}"); // +はShiftキー
                        await Task.Delay(800); 
                    }

                    foreach (string s in cells)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            if (col == 0 && codeSlipDt.Select($"HMCD='{s}'").Count() > 0)   // 同一品番のコピペ対応
                            {
                                Dgv_CodeSlipMst[col, row].Value = s + " (2)";
                            }
                            else if (Dgv_CodeSlipMst.Columns[col].DefaultCellStyle.Format == "#,0") // フォーマットがかかったInt型
                            {
                                Dgv_CodeSlipMst[col, row].Value = s.Replace(",", "");       // 区切り文字をカット
                            }
                            else
                            {
                                Dgv_CodeSlipMst[col, row].Value = s;
                            }
                        }
                        // 次のセルへ
                        col++;      
                        if (col >= Dgv_CodeSlipMst.ColumnCount - 1) break;
                    }
                    // 工程キーが入っていなかったら自動で変換
                    if (Dgv_CodeSlipMst[cColKTKEY, row].Value == null || Dgv_CodeSlipMst[cColKTKEY, row].Value == DBNull.Value)
                    {
                        setKTKEY(row);
                    }
                    // RowStateの変更
                    DataRow[] dr = codeSlipDt.Select($"HMCD='{hmcd}'");
                    if (dr.Length == 1) dr[0].EndEdit(); // ここでようやく RowState が Unchanged から Modified に変更される（コミットみたいなもの）

                    // クリップボード内で改行されていたら次の行へ
                    row++;
                }
                eventFlg = false;
            }
            // Deleteキー
            if (e.KeyCode == Keys.Delete)           
            {
                Debug.WriteLine(Dgv_CodeSlipMst.CurrentCell.ValueType);
                foreach (DataGridViewCell c in Dgv_CodeSlipMst.SelectedCells)
                {
                    if (c.ColumnIndex == 0) continue;                               // 主キーは消さない
                    if (c.ColumnIndex == cColKTSU) c.Value = 0;                     // 工程数はDB必須列
                    if (c.ColumnIndex != cColKTSU) c.Value = DBNull.Value;          // ただのnullは入らない
                    string hmcd = Dgv_CodeSlipMst[0, c.RowIndex].Value.ToString();
                    DataRow[] dr = codeSlipDt.Select($"HMCD='{hmcd}'");
                    if (dr.Length == 1) dr[0].EndEdit();                            // RowState の変更
                }
            }
        }

        // キーボードショートカット
        private void Frm034_CodeSlipMstMaint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) btnRefreshDataGridView_Click(sender, e);
            if (e.KeyCode == Keys.F9) btnUpdateDatabase_Click(sender, e);
            if (e.KeyCode == Keys.F10) btn_ExportExcel_Click(sender, e);
            if (e.KeyCode == Keys.Escape) Close();
        }

        // フィルターされた行を取得しデータテーブルとして返却（外部出力（F10）にて使用）
        private void GetFilteredRows(DataGridView dataGridView, ref DataTable exportDt)
        {
            // カラムの追加
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.Visible) // フィルターされた行は Visible プロパティが true になります
                    exportDt.Columns.Add(column.HeaderText, column.ValueType);
            }
            // 行の追加
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Visible && row.IsNewRow == false) // フィルターされた行は Visible プロパティが true になります
                {
                    // 選択行が0件または1件の場合：フィルターされた全件データを取得
                    // 選択行が複数行選ばれていた場合：フィルターされた中の選択行を取得
                    if (dataGridView.SelectedRows.Count == 0 ||
                        dataGridView.SelectedRows.Count == 1 ||
                        dataGridView.SelectedRows.Count > 1 && row.Selected)
                    {
                        DataRow dr = exportDt.NewRow();
                        int col = 0;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Visible) // フィルターされた行は Visible プロパティが true になります
                            {
                                if (exportDt.Columns[col].DataType == typeof(decimal))
                                {
                                    decimal decimaldata;
                                    if (Decimal.TryParse(cell.Value.ToString(), out decimaldata))
                                        dr[col] = decimaldata;
                                }
                                else
                                {
                                    dr[col] = cell.Value;
                                }
                                col++;
                            }
                        }
                        exportDt.Rows.Add(dr);
                    }
                }
            }
        }

        // 外部出力（F10）
        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            DataTable exportDt = new DataTable();
            GetFilteredRows(Dgv_CodeSlipMst, ref exportDt);

            // 保存ダイアログ
            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                FileName = $"コード票_{DateTime.Now.ToString("M")}",
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
                    cmn.Fa.ExportExcel(exportDt, filePath);
                    cmn.Fa.CloseExcel2();
                    // Interop.Excelではなく標準アプリケーションで開く
                    Process.Start(@filePath);
                }
                catch (Exception ex) // 例外処理
                {
                    MessageBox.Show("ファイルの保存中にエラーが発生しました: " + ex.Message);
                    toolStripStatusLabel2.Text = ex.Message;
                    cmn.Fa.CloseExcel2();
                }
            }
        }

        // 小文字を大文字に変換
        private void Dgv_CodeSlipMst_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textBox)
            {
                textBox.CharacterCasing = CharacterCasing.Upper;
            }
        }

        // 工程ラジオボタンイベント
        private void RadioButton_Paint(object sender, PaintEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                Font font = new Font(rb.Font, FontStyle.Underline);
                TextRenderer.DrawText(e.Graphics, rb.Text, font, rb.ClientRectangle, Color.FromArgb(200, 60, 60, 60));
            }
            else
            {
                TextRenderer.DrawText(e.Graphics, rb.Text, rb.Font, rb.ClientRectangle, Color.FromArgb(80, 160, 160, 160));
            }
        }

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            myFilter();
        }

    }
}
