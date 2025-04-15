using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm042_InformationOrder : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DataTable km8420 = new DataTable();     // 設備マスタ
        private DataTable km8430 = new DataTable();     // コード票マスタ
        private DataTable kd8430 = new DataTable();     // 手配ファイル
        private DataTable kd8450 = new DataTable();     // 切削オーダーファイル

        // データグリッドビューヘッダータイトル
        private class MultipleValues
        {
            public string JPNAME { get; set; }
            public int Width { get; set; }
            public DataGridViewContentAlignment StyleAlignment { get; set; }
            public string Format { get; set; }      // 例："#,0"; "yyyy/MM/dd HH:mm:ss";
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="common">共通クラス</param>
        public Frm042_InformationOrder(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_042 + ": " + Common.FRM_NAME_041 + ">";

            // 共通クラス
            this.cmn = cmn;

            // 初期設定
            SetInitialValues();
        }

        // ********************** コントロールの初期化関連ここから ************************
        private async void SetInitialValues()
        {
            // マスタ取得
            bool ret8420 = false;
            bool ret8430 = false;
            var task8420 = Task.Run(() => ret8420 = cmn.Dba.GetEquipMstDt(ref km8420));
            var task8430 = Task.Run(() => ret8430 = cmn.Dba.GetCodeSlipMst(ref km8430));
            await Task.WhenAll(task8420, task8430);
            if (ret8420 == false || ret8430 == false) return;

            // 設備グループドロップダウン設定
            List<string> mcgcds = km8420.AsEnumerable()
                .OrderBy(x => x["MCGSEQ"])
                .GroupBy(grp => new {
                    MCGCD = grp["MCGCD"].ToString()
                })
                .Select(row => row.Key.MCGCD)
                .ToList();
            cmb_MCGCD.Items.AddRange(mcgcds.ToArray());
            cmb_MCGCD.Items.Insert(0, "全て");

            // 設備グループドロップダウン設定
            List<string> mccds = km8420.AsEnumerable()
                .OrderBy(x => x["MCGSEQ"])
                .ThenBy(x => x["MCSEQ"])
                .Select(c => c["MCGCD"].ToString() + "-" + c["MCCD"].ToString())
                .ToList();
            cmb_MCCD.Items.AddRange(mccds.ToArray());

            // DataGridViewの初期設定
            dgv_Order.ReadOnly = true;
            // DataGridViewのヘッダー背景色を設定
            dgv_Order.EnableHeadersVisualStyles = false;
            dgv_Order.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            dgv_Order.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            // DataGridViewの明細を2行毎に背景色設定
            dgv_Order.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            // DataGridViewの画面ちらつきをおさえるため、DoubleBufferedを有効にする
            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(dgv_Order, true, null);

            cmb_Condition.SelectedIndex = 0; // 0:[＝]

            // データグリッドの初期化
            kd8430.Rows.Clear();
            cmn.Dba.FindMpOrder(ref kd8430, selectSQL(), "1=1 limit 0");
            dgv_Order.DataSource = kd8430;
            // ヘッダータイトルを日本語に変換
            columnHeaderText();

            // ボタンを非活性化
            setCommandButtons(false);

            // 初期フォーカス
            dtp_EDDT_From.Focus();

            toolStripStatusLabel1.Text = string.Empty;
            toolStripStatusLabel2.Text = "セル選択後、右クリックでコピー";
        }

        // データグリッドビューに行番号をつける
        private void Dgv_CodeSlipMst_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // 行ヘッダのセル領域を、行番号を描画する長方形とする
            // （ただし右端に4ドットのすき間を空ける）
            Rectangle rect = new Rectangle(
              e.RowBounds.Location.X,
              e.RowBounds.Location.Y,
              dgv_Order.RowHeadersWidth - 4,
              e.RowBounds.Height);

            // 上記の長方形内に行番号を縦方向中央＆右詰めで描画する
            // フォントや前景色は行ヘッダの既定値を使用する
            TextRenderer.DrawText(
              e.Graphics,
              (e.RowIndex + 1).ToString(),
              dgv_Order.RowHeadersDefaultCellStyle.Font,
              rect,
              dgv_Order.RowHeadersDefaultCellStyle.ForeColor,
              TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        // データグリッドビューに表示するSQL文の項目名（順番もここで決定）
        private string selectSQL()
        {
            return 
                "ODRNO," +
                "HMCD," +
                "KTKEY," +
                "MATERIALCD," +
                "ODRQTY," +
                "JIQTY," +
                "EDDT," +
                "EDTIM," +
                "ODRSTS," +
                "DENPYOKBN," +
                "DENPYODT," +
                "MPCARDDT," +
                "MPTANADT," +
                "NOTE," +
                "WKNOTE," +
                "WKCOMMENT," +
                "KTCD," +
                "INSTID," +
                "INSTDT," +
                "UPDTID," +
                "UPDTDT";
        }
        // データグリッドの日本語列名、文字寄せ、幅px、文字フォーマットをここで設定
        private void columnHeaderText()
        {
            Dictionary<string, MultipleValues> dic = new Dictionary<string, MultipleValues>();
            dic.Add("ODRNO", new MultipleValues { JPNAME = "手配No", Width = 110 });
            dic.Add("KTSEQ", new MultipleValues { JPNAME = "工程順序", Width = 40 });
            dic.Add("HMCD", new MultipleValues { JPNAME = "品番", Width = 170 });
            dic.Add("KTCD", new MultipleValues { JPNAME = "工程ｺｰﾄﾞ", Width = 60 });
            dic.Add("ODRQTY", new MultipleValues { JPNAME = "手配数", Width = 60, StyleAlignment = DataGridViewContentAlignment.MiddleRight });
            dic.Add("ODCD", new MultipleValues { JPNAME = "手配先ｺｰﾄﾞ", Width = 60 });
            dic.Add("NEXTODCD", new MultipleValues { JPNAME = "次手配先コード", Width = 60 });
            dic.Add("LTTIME", new MultipleValues { JPNAME = "LT", Width = 40 });
            dic.Add("STDT", new MultipleValues { JPNAME = "着手予定日", Width = 100 });
            dic.Add("STTIM", new MultipleValues { JPNAME = "着手予定時刻", Width = 60 });
            dic.Add("EDDT", new MultipleValues { JPNAME = "完了予定日", Width = 100 });
            dic.Add("EDTIM", new MultipleValues { JPNAME = "完了予定時刻", Width = 60 });
            dic.Add("ODRSTS", new MultipleValues { JPNAME = "手配状況", Width = 40, StyleAlignment = DataGridViewContentAlignment.MiddleCenter });
            dic.Add("QRCD", new MultipleValues { JPNAME = "QRコード", Width = 100 });
            dic.Add("JIQTY", new MultipleValues { JPNAME = "実績数", Width = 60, StyleAlignment = DataGridViewContentAlignment.MiddleRight });
            dic.Add("DENPYOKBN", new MultipleValues { JPNAME = "帳票発行区分", Width = 40, StyleAlignment = DataGridViewContentAlignment.MiddleCenter });
            dic.Add("DENPYODT", new MultipleValues { JPNAME = "帳票発行日", Width = 100, Format = "MM/dd HH:mm" });
            dic.Add("NOTE", new MultipleValues { JPNAME = "摘要", Width = 100 });
            dic.Add("WKNOTE", new MultipleValues { JPNAME = "作業内容", Width = 100 });
            dic.Add("WKCOMMENT", new MultipleValues { JPNAME = "作業注釈", Width = 100 });
            dic.Add("DATAKBN", new MultipleValues { JPNAME = "データ区分", Width = 40 });
            dic.Add("INSTID", new MultipleValues { JPNAME = "登録者", Width = 60 });
            dic.Add("INSTDT", new MultipleValues { JPNAME = "登録日時", Width = 100, Format = "MM/dd HH:mm" });
            dic.Add("UPDTID", new MultipleValues { JPNAME = "更新者", Width = 60 });
            dic.Add("UPDTDT", new MultipleValues { JPNAME = "更新日時", Width = 100, Format = "MM/dd HH:mm" });
            dic.Add("NAIGAIKBN", new MultipleValues { JPNAME = "社内外区分", Width = 40 });
            dic.Add("RETKTCD", new MultipleValues { JPNAME = "検索用工程コード", Width = 40 });

            dic.Add("KTKEY", new MultipleValues { JPNAME = "工程キー", Width = 100 });
            dic.Add("MATERIALCD", new MultipleValues { JPNAME = "切削母材品番", Width = 110 });
            dic.Add("MPCARDDT", new MultipleValues { JPNAME = "製造指示カード発行日", Width = 100, Format = "MM/dd HH:mm" });
            dic.Add("MPTANADT", new MultipleValues { JPNAME = "棚コンデータ作成日", Width = 100, Format = "MM/dd HH:mm" });
            dic.Add("MPINSTID", new MultipleValues { JPNAME = "MP登録者", Width = 100 });
            dic.Add("MPINSTDT", new MultipleValues { JPNAME = "MP登録日時", Width = 100, Format = "MM/dd HH:mm" });
            dic.Add("MPUPDTID", new MultipleValues { JPNAME = "MP更新者", Width = 100 });
            dic.Add("MPUPDTDT", new MultipleValues { JPNAME = "MP更新日時", Width = 100, Format = "MM/dd HH:mm" });

            for (int c = 0; c < dgv_Order.Columns.Count; c++)
            {
                try
                {
                    var headerName = dgv_Order.Columns[c].HeaderText;
                    dgv_Order.Columns[c].HeaderText = dic[headerName].JPNAME;
                    dgv_Order.Columns[c].Width = dic[headerName].Width;
                    dgv_Order.Columns[c].HeaderCell.Style.Alignment = dic[headerName].StyleAlignment;
                    dgv_Order.Columns[c].DefaultCellStyle.Alignment = dic[headerName].StyleAlignment;
                    dgv_Order.Columns[c].DefaultCellStyle.Format = dic[headerName].Format;
                }
                catch
                {
                    dgv_Order.Columns[c].HeaderText = dgv_Order.Columns[c].HeaderText;
                }
            }
        }
        // 各ボタンの活性化、非活性化
        private void setCommandButtons(bool flg)
        {
            if (flg == false) // 未実装
            {
                btn_DetailOrder.BackColor = (flg) ? Common.FRM40_BG_COLOR_ORDERED : Common.FRM40_BG_COLOR_CONTROL;
                btn_DetailOrder.Enabled = flg;
                btn_Progress.BackColor = (flg) ? Common.FRM40_BG_COLOR_ORDERED : Common.FRM40_BG_COLOR_CONTROL;
                btn_Progress.Enabled = flg;
            }
            btn_PrintOrder.BackColor = (flg) ? Common.FRM40_BG_COLOR_PRINTED : Common.FRM40_BG_COLOR_CONTROL;
            btn_PrintOrder.Enabled = flg;
            btn_ExportOrder.BackColor = (flg) ? Common.FRM40_BG_COLOR_PRINTED : Common.FRM40_BG_COLOR_CONTROL;
            btn_ExportOrder.Enabled = flg;
        }

        // ********************** コントロールの初期化関連ここまで ************************


        // ******************************** 条件関連ここから *********************************
        private void chk_ODRSTS_CheckedChanged(object sender, EventArgs e)
        {
            checkedFillter();
            // コントロールの活性、非活性化
            chk_2.Enabled = chk_ODRSTS.Checked;
            chk_3.Enabled = chk_ODRSTS.Checked;
            chk_4.Enabled = chk_ODRSTS.Checked;
            chk_9.Enabled = chk_ODRSTS.Checked;
        }

        private void chk_2_CheckedChanged(object sender, EventArgs e) { checkedFillter(); }

        private void chk_3_CheckedChanged(object sender, EventArgs e) { checkedFillter(); }

        private void chk_4_CheckedChanged(object sender, EventArgs e) { checkedFillter(); }

        private void chk_9_CheckedChanged(object sender, EventArgs e) { checkedFillter(); }

        private void chk_ODRNO_CheckedChanged(object sender, EventArgs e)
        {
            checkedFillter();
            // 入力ボックスの活性、非活性化
            txt_HMCD.Enabled = chk_ODRNO.Checked;
            btn_HMCDPaste.Enabled = chk_ODRNO.Checked;
            if (chk_HMCD.Checked) txt_HMCD.Focus();
        }

        private void chk_HMCD_CheckedChanged(object sender, EventArgs e)
        {
            checkedFillter();
            // 入力ボックスの活性、非活性化
            txt_HMCD.Enabled = chk_HMCD.Checked;
            btn_HMCDPaste.Enabled = chk_HMCD.Checked;
            if (chk_HMCD.Checked) txt_HMCD.Focus();
        }

        private void txt_HMCD_TextChanged(object sender, EventArgs e)
        {
            var selpos = txt_HMCD.SelectionStart;
            var sellen = txt_HMCD.SelectionLength;
            txt_HMCD.Text = txt_HMCD.Text.ToUpper();
            txt_HMCD.SelectionStart = selpos;
            txt_HMCD.SelectionLength = sellen;
            if (txt_HMCD.TextLength > 2) checkedFillter();
        }

        // 手配完了予定日のFromToを表示するかの判定
        private void cmb_Condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtp_EDDT_To.Visible = (cmb_Condition.SelectedIndex == 0) ? false : true;
        }

        private void chk_MCCD_CheckedChanged(object sender, EventArgs e)
        {
            checkedFillter();
            // コントロールの活性、非活性化
            cmb_MCGCD.Enabled = chk_MCCD.Checked;
            cmb_MCCD.Enabled = chk_MCCD.Checked;
        }
        private void cmb_MCGCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_MCCD.Items.Clear();
            // 設備グループドロップダウン設定
            var findstring = (cmb_MCGCD.Text == "全て") ? "" : cmb_MCGCD.Text;
            List<string> mccds = km8420.AsEnumerable()
                .Where(x => x["MCGCD"].ToString().StartsWith(findstring))
                .OrderBy(x => x["MCSEQ"])
                .Select(c => c["MCGCD"].ToString() + "-" + c["MCCD"].ToString())
                .ToList();
            cmb_MCCD.Items.AddRange(mccds.ToArray());
            checkedFillter();
        }

        private void cmb_MCCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedFillter();
        }
        // 現在表示されている各条件を元にフィルター
        private void checkedFillter()
        {
            if (kd8430.Rows.Count == 0) return;
            var f = addFillter();
            if (f == string.Empty)
            {
                kd8430.DefaultView.RowFilter = string.Empty;
                toolStripStatusLabel1.Text = kd8430.Rows.Count.ToString("#,0") + "件抽出";
            }
            else
            {
                kd8430.DefaultView.RowFilter = f;
                toolStripStatusLabel1.Text = dgv_Order.RowCount.ToString("#,0") +
                        "件 / " + kd8430.Rows.Count.ToString("#,0") + "件中";
            }
        }


        // 現在表示されている検索条件フィルター文の作成
        private string addFillter()
        {
            var f = "";
            if (chk_ODRSTS.Checked)
            {
                var c = 0;
                c += (chk_2.Checked) ? 1 : 0;
                c += (chk_3.Checked) ? 1 : 0;
                c += (chk_4.Checked) ? 1 : 0;
                c += (chk_9.Checked) ? 1 : 0;
                if (c == 1) // 単数検索条件を設定
                {

                    if (chk_2.Checked) f += "ODRSTS='2'";
                    if (chk_3.Checked) f += "ODRSTS='3'";
                    if (chk_4.Checked) f += "ODRSTS='4'";
                    if (chk_9.Checked) f += "ODRSTS='9'";
                }
                if (c > 1)
                {
                    // 複数検索条件を設定
                    f += "ODRSTS IN(";
                    if (chk_2.Checked) f += "'2',";
                    if (chk_3.Checked) f += "'3',";
                    if (chk_4.Checked) f += "'4',";
                    if (chk_9.Checked) f += "'9',";
                    f += "'x')";
                }
            }
            if (chk_ODRNO.Checked && txt_HMCD.Text != string.Empty)
            {
                if (f != string.Empty) f += " and ";
                f += "ODRNO = '" + txt_HMCD.Text + "' ";
            }
            if (chk_HMCD.Checked && txt_HMCD.Text != string.Empty)
            {
                if (f != string.Empty) f += " and ";
                f += "HMCD like '%" + txt_HMCD.Text + "%' ";
            }
            if (chk_MCCD.Checked)
            {
                if (cmb_MCGCD.SelectedIndex > 0)
                {
                    if (f != string.Empty) f += " and ";
                    f += "KTKEY like '%" + cmb_MCGCD.Text + "-";
                    if (cmb_MCCD.SelectedIndex >= 0)
                        f += cmb_MCCD.Text.Replace(cmb_MCGCD.Text + "-", "") + ":";
                    f += "%'";
                }
            }
            return f;
        }
        // ******************************** 条件関連ここまで *********************************



        // ******************************** おまけ関連ここから *********************************
        // Form.KeyPreview.KeyDownハンドラー
        private void Frm042_InformationOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) btn_Search_Click(sender, e);
            if (e.KeyCode == Keys.F10) btn_ExportOrder_Click(sender, e);
            if (e.KeyCode == Keys.F12) btn_PrintOrder_Click(sender, e);
            if (e.KeyCode == Keys.Escape) Close();
        }

        private void dtp_EDDT_From_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btn_Search_Click(sender, e);
        }

        // データグリッド上のセルダブルクリックイベント
        private void dgv_Order_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string val = dgv_Order[e.ColumnIndex, e.RowIndex].Value.ToString();
            int cnt = 0;
            for (int i = 0; i < dgv_Order.Rows.Count; i++)
            {
                if (dgv_Order[e.ColumnIndex, i].Value.ToString() == val)
                {
                    dgv_Order.Rows[i].Selected = true;
                    cnt++;
                }
                else
                {
                    dgv_Order.Rows[i].Selected = false;
                }
                toolStripStatusLabel1.Text = cnt.ToString("#,0") + "件が選択されました．";
            }
        }

        // クリップボードからペースト
        private void btn_HMCDPaste_Click(object sender, EventArgs e)
        {
            txt_HMCD.Text = Clipboard.GetText().Replace("\r\n", "");
        }

        // クリップボードへ品番コピー
        private void dgv_Order_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int col = dgv_Order.CurrentCell.ColumnIndex;
                string ht = dgv_Order.Columns[col].HeaderText;
                string val = dgv_Order[col, e.RowIndex].Value.ToString();
                toolStripStatusLabel2.Text = $"{ht}[{val}] をクリップしました．";
                Clipboard.SetText(val);
            }
        }
        // ******************************** おまけ関連ここまで *********************************



        // ******************************** メイン処理ここから *********************************
        // 検索処理
        private void btn_Search_Click(object sender, EventArgs e)
        {
            // 検索済データクリア
            kd8430.Rows.Clear();
            // ここから条件設定
            var where = "";
            if (!chk_ODRNO.Checked)
            {
                if (dtp_EDDT_To.Visible == false)
                {
                    where += "EDDT='" + dtp_EDDT_From.Value.Date.ToString("yyyy-MM-dd") + "'";
                }
                else
                {
                    where += "EDDT between '" + dtp_EDDT_From.Value.Date.ToString("yyyy-MM-dd") +
                        "' and '" + dtp_EDDT_To.Value.Date.ToString("yyyy-MM-dd") + "'";
                }
            }
            var fillter = addFillter();
            if (fillter != string.Empty) where += (where == string.Empty) ? fillter : " and " + fillter;

            // データベースアクセス
            var ret8420 = cmn.Dba.FindMpOrder(ref kd8430, selectSQL(), where);
            dgv_Order.DataSource = kd8430;

            // ステータス表示
            if (kd8430.Rows.Count == 0)
            {
                setCommandButtons(false);
                toolStripStatusLabel1.Text = "データが存在しませんでした．";
            }
            else
            {
                setCommandButtons(true);
                toolStripStatusLabel1.Text = kd8430.Rows.Count.ToString("#,0") + "件を抽出しました．";
            }
        }

        // フィルターされた行を取得しデータテーブルとして返却
        private void GetFilteredRows(DataGridView dataGridView, ref DataTable exportDt)
        {
            //List<DataGridViewRow> filteredRows = new List<DataGridViewRow>();
            // カラムの追加
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                exportDt.Columns.Add(column.HeaderText, column.ValueType);
            }
            // 行の追加
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Visible) // フィルターされた行は Visible プロパティが true になります
                {
                    // 追加機能：複数行選択された場合は選択行のみデータを取得
                    if (dgv_Order.SelectedRows.Count == 0 || 
                        dgv_Order.SelectedRows.Count == 1 ||
                        dgv_Order.SelectedRows.Count > 1 && row.Selected)
                    {
                        DataRow dr = exportDt.NewRow();
                        for (int i = 0; i < dataGridView.Columns.Count; i++)
                        {
                            dr[i] = row.Cells[i].Value;
                        }
                        exportDt.Rows.Add(dr);

                        //filteredRows.Add(row);
                    }
                }
            }
            //return filteredRows;
        }

        // 外部出力（F10）
        private void btn_ExportOrder_Click(object sender, EventArgs e)
        {
            DataTable exportDt = new DataTable();
            GetFilteredRows(dgv_Order, ref exportDt);

            // 保存ダイアログ
            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                FileName = $"手配情報_{DateTime.Now.ToString("M")}",
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

        // 製造指示カード発行（F12）
        private async void btn_PrintOrder_Click(object sender, EventArgs e)
        {
            // モニターの倍率をチェックし雛形ファイルのインデックス番号を取得
            int idx = (cmn.ScreenMagnification == 1d) ? 1 : (cmn.ScreenMagnification == 1.25d) ? 2 : 9;
            if (idx == 9)
            {
                Debug.WriteLine(Common.MSG_NO_PATTERN_FILE);
                cmn.ShowMessageBox(Common.MY_PGM_ID, Common.MSG_CD_105, Common.MSG_TYPE_W, MessageBoxButtons.OK, Common.MSG_NO_PATTERN_FILE, MessageBoxIcon.Warning);
                return;
            }
            // 雛形ファイルの存在チェック
            var templateFi = new FileInfo($@"{cmn.FsCd[idx].RootPath}\{cmn.FsCd[idx].FileName}");
            if (!templateFi.Exists)
            {
                Debug.WriteLine(Common.MSG_NO_PATTERN_FILE);
                cmn.ShowMessageBox(Common.MY_PGM_ID, Common.MSG_CD_103, Common.MSG_TYPE_E, MessageBoxButtons.OK, Common.MSG_NO_PATTERN_FILE, MessageBoxIcon.Error);
                return;
            }
            // 印刷対象データの取得と最終確認
            DataTable targetDt = new DataTable();
            GetFilteredRows(dgv_Order, ref targetDt);
            var msg = targetDt.Rows.Count.ToString() + "件を印刷します．\nよろしいですか？";
            if (MessageBox.Show(msg, "最終確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1) == DialogResult.Cancel) return;

            // ステータス表示
            toolStripStatusLabel1.Text = "製造指示カード印刷中...";

            // Excelアプリケーションを起動
            cmn.Fa.OpenExcel2(idx);

            // 製造指示カード雛形を開く（拡縮倍率にあった帳票を選択）
            cmn.Fa.OpenExcelFile2($@"{cmn.FsCd[idx].RootPath}\{cmn.FsCd[idx].FileName}");

            // 対象の製造指示データをDataTableに読み込む
            int ret = 0;
            DataTable cardDt = new DataTable();
            await Task.Run(() => ret = cmn.Dba.GetOrderCardPrintInfo(targetDt, ref cardDt));
            if (ret < 0)
            {
                cmn.Fa.CloseExcel2(); // Excelアプリケーションを閉じる
                return;
            }

            // 製造指示カード雛形に製造指示データをセット
            await Task.Run(() => ret = PrintOrderCard(ref cardDt));
            if (ret != 0)
            {
                cmn.Fa.CloseExcel2(); // Excelアプリケーションを閉じる
                return;
            }

            // ExcelブックからPDFを設定ファイルの場所に保存して通常アプリケーションで起動
            var pdfName = cmn.FsCd[idx].FileName
                .Replace("雛形", "_個別指定_" + DateTime.Now.ToString("yyyyMMddhhmm"))
                .Replace(".xlsx", ".pdf");
            cmn.Fa.ExportExcelToPDF($@"{cmn.FsCd[0].RootPath}\{pdfName}"); // 0:生産計画システム出力先フォルダ

            // 製造指示カード雛形を閉じる
            cmn.Fa.CloseExcelFile2(false);

            // 出力済ステータスに更新
            cmn.Dba.UpdatePrintOrderCardDay(ref cardDt);

            // Excelアプリケーションを閉じる
            cmn.Fa.CloseExcel2();

        }

        /// <summary>
        /// 出荷指示カード作成
        /// </summary>
        /// <param name="cardDay">完了予定日</param>
        /// 
        /// <returns>結果 (0: 保存成功 (保存件数), -1: 保存失敗, -2: 認証失敗)</returns>
        public int PrintOrderCard(ref System.Data.DataTable cardDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;

            try
            {
                // Excelシートの作成
                int baseRow = 1;
                int cardCnt = 1;    // ※手配件数とカード枚数は異なる（ロットで分割するため）
                int cardRows = 21;  // 1カードの行数（余白含む）
                int row = 0;
                int col = 0;
                cmn.Fa.CreateTemplateOrderCard(); // テンプレートオブジェクトの作成（製造指示カードの雛形を作成）
                for (int i = 0; i < cardDt.Rows.Count; i++)
                {
                    DataRow r = cardDt.Rows[i];

                    // 収容数で分割
                    decimal odrqty = Decimal.Parse(r["ODRQTY"].ToString());
                    decimal boxqty = 0;
                    int loopCnt = 1;
                    if (Decimal.TryParse(r["BOXQTY"].ToString(), out boxqty))
                    {
                        loopCnt = Decimal.ToInt32(Math.Ceiling((odrqty / boxqty)));
                    }

                    // 収容数でループ
                    for (int j = 1; j <= loopCnt; j++)
                    {
                        // 書き込みを行う先頭行番号を計算
                        row = cardRows * (Convert.ToInt32(Math.Ceiling(cardCnt / 2d)) - 1) + baseRow;

                        // 左右の列番号を切り替え
                        col = (cardCnt % 2 != 0) ? 1 : 10;

                        // 処理速度の計測開始
                        // DateTime SW3 = DateTime.Now;
                        // Debug.WriteLine("[StopWatch] Read開始 ");

                        // カードに値をセット
                        cmn.Fa.SetOrderCard(ref r, ref row, ref col, j, loopCnt);

                        cardCnt++;
                    }
                }
                // ループ終了時に最後のページの印刷枚数が４の倍数でなかった場合、
                // 残りの余分なデータをクリア（COMアクセスを減らす為にクリア処理は最後の一回だけ行う）
                if ((cardCnt - 1) % 4 != 0)
                    cmn.Fa.ClearZanOrderCard(cardCnt - 1);
                toolStripStatusLabel1.Text = $" {cardDt.Rows.Count}件のカードが作成されました.";
            }
            // ファイルの保存に失敗すると Exception が発生する
            catch (Exception e)
            {
                Debug.WriteLine("Exception Source = " + e.Source);
                Debug.WriteLine("Exception Message = " + e.Message);

                cmn.Fa.CloseExcel2();

                // 戻り値でエラー種別を判定
                if (cmn.ConvertDecToHex(e.HResult) == Common.HRESULT_FILE_IN_USE)
                {
                    // ファイル使用中
                    ret = Common.SFD_RET_FILE_IN_USE;
                }
                else
                {
                    // それ以外
                    ret = Common.SFD_RET_SAVE_FAILED;
                }
            }
            return ret;
        }

        private void btn_DetailOrder_Click(object sender, EventArgs e)
        {
            MessageBox.Show("未実装");
        }

        private void btn_Progress_Click(object sender, EventArgs e)
        {
            MessageBox.Show("未実装");
        }

        // ******************************** メイン処理ここまで *********************************
    }
}