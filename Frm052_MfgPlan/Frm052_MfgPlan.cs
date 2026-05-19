using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MPPPS
{
    public partial class Frm052_MfgPlan : Form
    {
        // 共通クラス
        private readonly Common cmn;

        // 定数
        private static readonly int STARTCOL = 7; // 0:品番、1:(品名)、2:頭、3:材料、4:(CT)、5:(製品長さ)、6:工程数、7:～
        private static readonly int COLCT = 4;
        private static readonly int COLHMSIZE = 5;

        // 切削母材マスタ（D0470:切削母材情報をグループ化）
        Dictionary<string, decimal> matDic = new Dictionary<string, decimal>(); // HMCD:品番、SOLEN:母材品番の全長

        // DB から取得したデータを格納するクラス
        private class DBRow
        {
            public string 受注番号 { get; set; }
            public string 品番 { get; set; }
            public string 品名 { get; set; }
            public DateTime 日付 { get; set; }
            public int 数量 { get; set; }
            public string 材料 { get; set; }
            public decimal CT { get; set; }
            public decimal 製品長さ { get; set; }
            public int 工程数 { get; set; }
            public string 備考 { get; set; }
        }

        // 元に戻すアクション種類
        private enum UndoType
        {
            RowMove,
            CellSwap,
            CellEdit,
            CellSplit4,
            RowDelete,
            RowInsert
        }

        // 元に戻すアクション
        private class UndoAction
        {
            public UndoType Type;

            // 行移動用 (RowMove)
            public int SourceRow;
            public int TargetRow;

            // セル入れ替え用（行跨ぎ禁止版） (CellSwap)
            public int SwapSourceRow;
            public int SwapSourceCol;
            public int SwapTargetCol;
            public Order SwapSourceOrder;
            public Order SwapTargetOrder;

            // セル分割用 (CellSplit4)
            public int SplitRow;
            public int[] SplitCols;
            public Order[] SplitOrders;

            // セル編集用 (CellEdit)
            public int EditRow;
            public int EditCol;
            public Order OldOrder;
            public Order NewOrder;

            // 行削除用 (RowDelete)
            public int DeletedRowIndex;
            public object[] DeletedRowValues;

            // 行挿入用
            //public int InsertRowIndex;
        }

        // UnDoアクション
        Stack<UndoAction> undoStack = new Stack<UndoAction>();  // 元に戻すスタック
        Order undoEditOldOrder = null;                          // Undoセル編集用の「編集中の値」
        private Point dragStartPoint;
        private ContextMenuStrip splitMenu;
        private ContextMenuStrip dropMenu;
        private DataGridViewCell dragSourceCell;
        private DataGridViewCell dropTargetCell;
        private DataGridViewCell splitSourceCell;
        private DataGridViewCell splitTargetCell;
        private bool isRowHeaderDrag = false;

        // ハイライトが偶数週の着色で上書きされてしまうので制御
        private int highlightedColumnIndex = -1;

        // 自動で閉じるメッセージボックスで使用
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        private const uint WM_CLOSE = 0x0010;

        // コンストラクタ
        public Frm052_MfgPlan(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_052 + ": " + Common.FRM_NAME_052 + ">";
            // 共通クラス
            this.cmn = cmn;

            // 切削母材情報マスタを非同期で取得
            Task.Run(() => matDic = cmn.Dba.GetD0470Dictionary());

            // 初期設定
            SetInitialValues();

            // イベント登録
            dataGridView1.KeyDown += DataGridView1_KeyDown;
            dataGridView1.CellBeginEdit += DataGridView1_CellBeginEdit;         // セル編集開始
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;             // 小文字大文字変換
            dataGridView1.CellMouseDown += DataGridView1_CellMouseDown;         // 右クリックメニュー、クリップボード処理DataGridView1_CellFormatting隔週で背景色を変更
            dataGridView1.MouseDown += DataGridView1_MouseDown;                 // ドラッグ＆ドロップ
            dataGridView1.MouseMove += DataGridView1_MouseMove;                 // ドラッグ＆ドロップ
            dataGridView1.DragOver += DataGridView1_DragOver;                   // ドラッグ＆ドロップ
            dataGridView1.DragDrop += DataGridView1_DragDrop;                   // ドラッグ＆ドロップ
            dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;           // 行番号と矢印
            dataGridView1.RowHeaderMouseClick += DataGridView1_RowHeaderMouseClick;//行移動、行削除
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;   // 詳細情報表示
            dataGridView1.AllowDrop = true;                                     // ドラッグ＆ドロップ
            // チャートイベント登録
            chart1.MouseClick += chart1_MouseClick;
        }

        // 初期設定
        private void SetInitialValues()
        {
            // データ読み込み
            DataTable xt2OrderDt = new DataTable();
            bool ret = cmn.Dba.ReadXT2(ref xt2OrderDt);

            List<DBRow> dbList = new List<DBRow>();
            foreach (DataRow row in xt2OrderDt.Rows)
            {
                try
                {
                    DBRow r = new DBRow
                    {
                        受注番号 = row["ODRNO"].ToString(),
                        品番 = row["HMCD"].ToString(),
                        品名 = row["HMNM"].ToString(),
                        日付 = (DateTime)row["EDDT"],
                        数量 = (int)row["ODRQTY"],
                        材料 = row["MATESIZE"].ToString(),
                        CT = decimal.Parse(row["CT"].ToString()),
                        製品長さ = (row["LENGTH"] == DBNull.Value) ? 0 : decimal.Parse(row["LENGTH"].ToString()),
                        工程数 = (int)row["KTSU"],
                        備考 = row["NOTE"].ToString() // DBNull.Value は "" に変換される
                    };
                    dbList.Add(r);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(row["HMCD"].ToString() + "\n" + ex.ToString());
                }
            }
            InitializeDataGridView(dataGridView1);      // データーグリッドコントロールの初期設定
            LoadToDataGrid(dataGridView1, dbList);      // DBListデータをセット
            InitializeChart();
            UpdateChartFromGrid(dataGridView1);
        }

        // データーグリッドの初期枠設定
        private void InitializeDataGridView(DataGridView dgv)
        {
            dgv.Columns.Clear();

            // 固定列
            dgv.Columns.Add("品番", "品番");
            dgv.Columns.Add("品名", "品名");
            dgv.Columns.Add("頭", "頭");
            dgv.Columns.Add("材料", "材料");
            dgv.Columns.Add("CT", "CT");
            dgv.Columns.Add("製品長さ", "製品長さ");
            dgv.Columns.Add("工程数", "工程数");
            dgv.Columns["品番"].Width = 150;
            dgv.Columns["品名"].Visible = false;        // 非表示にする
            dgv.Columns["頭"].Width = 30;
            dgv.Columns["材料"].Width = 65;
            dgv.Columns["CT"].Visible = false;          // 非表示にする
            dgv.Columns["製品長さ"].Visible = false;    // 非表示にする            
            var ktsu = dgv.Columns["工程数"];
                ktsu.Width = 30;
                ktsu.HeaderCell.Style.Font = new Font(dataGridView1.Font.FontFamily, 4f, FontStyle.Regular);// フォントを極端に小さくする
                ktsu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;                // セル右寄せ
            // 列0～3を編集不可にする
            for (int i = 0; i < STARTCOL; i++)
            {
                dgv.Columns[i].ReadOnly = true;
                dgv.Columns[i].DefaultCellStyle.BackColor = Color.Azure; // 薄ブルー
            }

            // 6/1〜6/30 の日付列を自動生成
            DateTime start = new DateTime(2026, 6, 1);
            DateTime end = new DateTime(2026, 6, 30);

            for (DateTime d = start; d <= end; d = d.AddDays(1))
            {
                if (d.DayOfWeek == DayOfWeek.Sunday || d.DayOfWeek == DayOfWeek.Saturday)
                {
                    //dgv.Columns[colName].Width = 15;      // １っか月分だと横幅が足りないので土日列なし
                }
                else
                {
                    string colName = d.ToString("MMdd");    // 例: "0601"
                    string header = d.ToString("M/d");      // 例: "6/1"
                    dgv.Columns.Add(colName, header);
                    var col = dataGridView1.Columns[colName];
                    col.Width = 45;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;     // ヘッダー中央寄せ
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;      // セル右寄せ
                    col.HeaderCell.Style.Font = 
                        new Font(dataGridView1.Font.FontFamily, 9f, FontStyle.Regular);             // フォントを極端に小さくする

                    // 週番号を取得（ISO週番号）し偶数週だけ薄グレー
                    var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
                    int week = cal.GetWeekOfYear(d, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    if (week % 2 == 0) col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                }
            }
            // コンテキストメニューの作成
            var f = new Font("Meiryo", 24); // メニューのフォントを大きく
            dropMenu = new ContextMenuStrip();
            dropMenu.Items.Add(new ToolStripMenuItem("合算", null, SumMove_Click) { Font = f });
            dropMenu.Items.Add(new ToolStripMenuItem("上書き", null, OverwriteMove_Click) { Font = f });
            dropMenu.Items.Add(new ToolStripMenuItem("キャンセル", null, (s, ev) => { }) { Font = f });
            //
            splitMenu = new ContextMenuStrip();
            splitMenu.Items.Add(new ToolStripMenuItem("隔週分割（2分割）", null, Split2_Click) { Font = f });
            splitMenu.Items.Add(new ToolStripMenuItem("4分割", null, Split4_Click) { Font = f });
        }

        // 初期データをデータグリッドに貼り付け
        private void LoadToDataGrid(DataGridView dgv, List<DBRow> dbList)
        {
            dgv.Rows.Clear();

            // 1. 品番一覧を取得（材料の記号での並び替え＋材料を数値に変換して並び替え）
            var 品番一覧 = dbList
                .Select(x => {
                    string 頭 = (x.材料.Length > 0) ? x.材料.Substring(0, 1) : "";
                    string サイズ文字列 = (x.材料.Length >= 2) ? x.材料.Substring(1) : "";
                    decimal.TryParse(サイズ文字列, out decimal サイズ数値);
                    return new
                    {
                        x.品番,
                        x.品名,
                        頭,
                        x.材料,
                        サイズ = サイズ数値,
                        x.CT,
                        x.製品長さ,
                        x.工程数
                    };
                })
                .Distinct()
                .OrderBy(x => x.頭)                  // 材料サイズの先頭1文字 昇順
                .ThenByDescending(x => x.サイズ)     // 材料サイズ 全体 降順
                .ThenBy(x => x.品番)                 // 品番 昇順
                .ToList();

            // 2. (品番, 日付) → OrderCell の辞書を作る（高速アクセス）
            //（重複を許容して、最初の1件だけ使う）
            var map = dbList
                .GroupBy(x => (x.品番, x.日付.Date))
                .ToDictionary(
                    g => g.Key,
                    g => {
                        var x = g.First();
                        return new Order(x.数量, x.受注番号, x.備考, "");
                    }
                );

            // 3. DataGridView に行を追加
            foreach (var hinban in 品番一覧)
            {
                int rowIndex = dgv.Rows.Add();
                var row = dgv.Rows[rowIndex];

                row.Cells["品番"].Value = hinban.品番;
                row.Cells["品名"].Value = hinban.品名;
                row.Cells["頭"].Value = hinban.頭;
                row.Cells["材料"].Value = hinban.材料;
                row.Cells["CT"].Value = hinban.CT;
                row.Cells["製品長さ"].Value = hinban.製品長さ;
                row.Cells["工程数"].Value = hinban.工程数;

                // 4. 6/1〜6/30 の日付列にデータをセット
                DateTime start = new DateTime(2026, 6, 1);
                DateTime end = new DateTime(2026, 6, 30);

                for (DateTime d = start; d <= end; d = d.AddDays(1))
                {
                    string colName = d.ToString("MMdd"); // 0601 など

                    if (map.TryGetValue((hinban.品番, d.Date), out Order order))
                    {
                        row.Cells[colName].Value = order;
                    }
                }
            }
        }
        // 行番号を付ける（1 から始める）
        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (e.RowIndex == dgv.NewRowIndex) return; // ★ 新規行は行番号を描画しない、「※」は描画

            // ★ 通常行は矢印を上書きして消す
            Rectangle rect = new Rectangle(
                e.RowBounds.Left + 1,
                e.RowBounds.Top + 1,
                dgv.RowHeadersWidth - 2,
                e.RowBounds.Height - 2);
            e.Graphics.FillRectangle(SystemBrushes.Control, rect);

            // 行番号を描画
            string rowNumber = (e.RowIndex + 1).ToString();
            var headerBounds = new Rectangle(
                e.RowBounds.Left,
                e.RowBounds.Top,
                dataGridView1.RowHeadersWidth,
                e.RowBounds.Height);
            TextRenderer.DrawText(
                e.Graphics,
                rowNumber,
                dataGridView1.Font,
                headerBounds,
                Color.Black,
                TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }

        // ショートカットキー
        private void Frm052_FormsPrinting_KeyDown(object sender, KeyEventArgs e)
        {
            // 「元に戻す」を呼び出す
            if (e.Control && e.KeyCode == Keys.Z)
            {
                ButtonUndo_Click(sender, e);    
                e.Handled = true;
            }
            // 「閉じる」
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        /*
         *  テキストボックス関連
        */

        // 「備考」入力
        private void TextBoxNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Order o = dataGridView1.CurrentCell.Value as Order;
                o.Note = textBoxNote.Text;
                dataGridView1.Focus();
            }
        }

        // 「閉じる」ボタン
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        // 「行削除」ボタン
        private void ButtonRowDelete_Click(object sender, EventArgs e)
        {
            var dgv = dataGridView1;
            int rowIndex = dgv.CurrentRow.Index;
            if (rowIndex < 0 || rowIndex >= dgv.Rows.Count) return;
            var row = dgv.Rows[rowIndex];

            // 行の値を配列に保存
            object[] values = new object[row.Cells.Count];
            for (int i = 0; i < row.Cells.Count; i++)
                values[i] = row.Cells[i].Value;

            // Undo 情報を積む
            var action = new UndoAction()
            {
                Type = UndoType.RowDelete,
                DeletedRowIndex = rowIndex,
                DeletedRowValues = values
            };
            undoStack.Push(action);
            ButtonUndo.Enabled = true;

            // 実際に削除
            dgv.Rows.RemoveAt(rowIndex);
        }

        // Excelにエクスポート
        private void ButtonExcelExport_Click(object sender, EventArgs e)
        {
            bool ret = cmn.Fa.製造部計画表出力ファイルチェック(out string templateFullPath, out string outputFullPath);
            if (ret == false) return;
            cmn.Fa.ExportFromDgvToExcel(templateFullPath, outputFullPath, dataGridView1, chart1);
            if (File.Exists(outputFullPath))
            {
                // Interop.Excelではなく標準アプリケーションでExcelを開く
                Process.Start(outputFullPath);
            }
        }

        // 「元に戻す」ボタン
        private void ButtonUndo_Click(object sender, EventArgs e)
        {
            if (undoStack == null) return;
            if (undoStack.Count == 0)
            {
                ButtonUndo.Enabled = false;
                return;
            }
            var action = undoStack.Pop();
            var dgv = dataGridView1;
            try
            {
                switch (action.Type)
                {
                    case UndoType.RowMove:
                        // 「行移動」を元に戻す
                        var row = dgv.Rows[action.TargetRow];
                        dgv.Rows.RemoveAt(action.TargetRow);
                        dgv.Rows.Insert(action.SourceRow, row);
                        dgv.CurrentCell = dgv[0, action.SourceRow];
                        break;

                    case UndoType.CellEdit:
                        // 「セル編集」を元に戻す
                        dgv[action.EditCol, action.EditRow].Value = 
                            (action.OldOrder.OrderNo == Common.NEWORDER && action.OldOrder.Qty == 0) ?
                                null : action.OldOrder;
                        dgv.CurrentCell = dgv[action.EditCol, action.EditRow];
                        break;

                    case UndoType.CellSplit4:
                        // 「セル分割」を元に戻す
                        int splitRow = action.SplitRow;
                        for (int i = 0; i < 4; i++)
                        {
                            int splitCol = action.SplitCols[i];
                            dgv[splitCol, splitRow].Value = 
                                (action.SplitOrders[i].Qty == 0) ?
                                    null : action.SplitOrders[i];
                            if (i == 0) dgv.CurrentCell = dgv[splitCol, splitRow];
                        }
                        break;

                    case UndoType.CellSwap:
                        // 「セル入れ替え」を元に戻す
                        dgv[action.SwapSourceCol, action.SwapSourceRow].Value = action.SwapSourceOrder;
                        dgv[action.SwapTargetCol, action.SwapSourceRow].Value = action.SwapTargetOrder;
                        dgv.CurrentCell = dgv[action.SwapSourceCol, action.SwapSourceRow];
                        break;

                    case UndoType.RowDelete:
                        // 「行削除」を元に戻す
                        dgv.Rows.Insert(action.DeletedRowIndex, action.DeletedRowValues);
                        dgv.CurrentCell = dgv[0, action.DeletedRowIndex];
                        break;

                    //case UndoType.RowInsert:
                    //    // 挿入された行を削除する
                    //    action.dgv.Rows.RemoveAt(action.InsertRowIndex);
                    //    break;
                }
                if (undoStack.Count == 0) ButtonUndo.Enabled = false;
                RefreshPlanInformation();
                dgv.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("想定外の動作ですm(__)m\nお困りであればシステム担当者に連絡してください"
                    , action.Type.ToString() + "を元に戻す処理"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /*
         * DataGridView関連
         */

        // 「セル編集」開始
        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var dgv = (DataGridView)sender;
            undoEditOldOrder = new Order((Order)dgv[e.ColumnIndex, e.RowIndex].Value ?? new Order());
        }
        // 「セル編集」終了
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Undo 情報を積む
            var dgv = (DataGridView)sender;
            var undoEditNewOrder = new Order(undoEditOldOrder);
            undoEditNewOrder.Qty = int.Parse(dgv[e.ColumnIndex, e.RowIndex].Value?.ToString() ?? "0");
            if (undoEditOldOrder.Qty == undoEditNewOrder.Qty) return;
            if (undoEditNewOrder.OrderNo == Common.NEWORDER && undoEditNewOrder.Qty == 0)
            {
                undoEditNewOrder = null;
            }
            else
            {
                undoEditNewOrder.EditStatus =
                    (undoEditNewOrder.OrderNo == Common.NEWORDER) ? "1:NEWORDER" :
                    (undoEditNewOrder.Qty == 0) ? "9:CANCEL" : "2:MODIFY";
            }
            var action = new UndoAction()
            {
                Type = UndoType.CellEdit,
                EditRow = e.RowIndex,
                EditCol = e.ColumnIndex,
                OldOrder = undoEditOldOrder,
                NewOrder = undoEditNewOrder
            };
            undoStack.Push(action);
            ButtonUndo.Enabled = true;

            dgv[e.ColumnIndex, e.RowIndex].Value = undoEditNewOrder;
            RefreshPlanInformation();
        }
        // 「行ヘッダー」クリック
        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentCell = dataGridView1[0, e.RowIndex];
                dataGridView1.Rows[e.RowIndex].Selected = true;   // 行全体を選択したい場合
            }
        }
        // 「行移動」「行削除」「セル入れ替え」ドラッグ開始
        private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            var dgv = (DataGridView)sender;
            dragStartPoint = e.Location;

            var hit = dgv.HitTest(e.X, e.Y);

            // CurrentCell の更新（MouseDown → CellMouseDown → CurrentCell 更新 → MouseUp → Click）
            if (hit.RowIndex >= 0 && hit.ColumnIndex >= 0)
            {
                dgv.CurrentCell = dgv[hit.ColumnIndex, hit.RowIndex];
            }

            dragSourceCell = null;
            isRowHeaderDrag = false;
            if (hit.RowIndex >= 0 && hit.RowIndex != dgv.NewRowIndex)
            {
                if (hit.Type == DataGridViewHitTestType.RowHeader)
                {
                    // 行ヘッダー → 「行移動」モード
                    isRowHeaderDrag = true;
                    dragSourceCell = dgv[0, hit.RowIndex]; // 行番号だけ覚えておけばOK

                    // マウスダウンした行を選択
                    dgv.ClearSelection();
                    dgv.Rows[hit.RowIndex].Selected = true;
                }
                else if (hit.ColumnIndex >= STARTCOL)
                {
                    // 通常セル → 「セル入れ替え」モード
                    dragSourceCell = dgv[hit.ColumnIndex, hit.RowIndex];
                }
            }
        }
        // 「行移動」「行削除」「セル入れ替え」マウス移動
        private void DataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dragSourceCell == null)
                return;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (Math.Abs(e.X - dragStartPoint.X) > SystemInformation.DragSize.Width ||
                    Math.Abs(e.Y - dragStartPoint.Y) > SystemInformation.DragSize.Height)
                {
                    dgv.DoDragDrop(dragSourceCell, DragDropEffects.Move);
                }
            }
        }
        // ドラッグ中のカーソルの形
        private void DataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        // 「行移動」「行削除」「セル入れ替え」ドロップ処理
        private void DataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            if (dragSourceCell == null) return;
            //{
            //    isRowHeaderDrag = false;
            //    return;
            //}

            var dgv = (DataGridView)sender;
            Point clientPoint = dgv.PointToClient(new Point(e.X, e.Y));
            var hit = dgv.HitTest(clientPoint.X, clientPoint.Y);

            if (hit.ColumnIndex >= 0 && hit.ColumnIndex < STARTCOL) return; // 品番～日付までは無視
            if (!isRowHeaderDrag)
            {
                dropTargetCell = dgv[hit.ColumnIndex, hit.RowIndex];        // 行ヘッダーでは取得できない
                if (dragSourceCell.RowIndex != hit.RowIndex) return;        // 行跨ぎ禁止
                if (dropTargetCell == dragSourceCell) return;               // 同一セル内は無視
            }

            if (isRowHeaderDrag && (hit.RowIndex < 0 || hit.RowIndex == dgv.NewRowIndex))
            {
                // ----------------------------------------
                // 行ヘッダー（データ範囲外） → 「行削除」
                // ----------------------------------------
                ButtonRowDelete_Click(sender, e);
            }
            else if (isRowHeaderDrag)
            {
                // ----------------------------------------
                // 行ヘッダー（データ範囲内） → 「行移動」
                // ----------------------------------------
                int sourceRow = dragSourceCell.RowIndex;
                int targetRow = hit.RowIndex;

                if (sourceRow == targetRow)
                    return;

                // Undo情報スタック
                var action = new UndoAction()
                {
                    Type = UndoType.RowMove,
                    SourceRow = sourceRow,
                    TargetRow = targetRow
                };
                undoStack.Push(action);
                ButtonUndo.Enabled = true;

                // 「行移動」
                var row = dgv.Rows[sourceRow];
                dgv.Rows.RemoveAt(sourceRow);
                dgv.Rows.Insert(targetRow, row);

                dgv.ClearSelection();
                dgv.Rows[targetRow].Selected = true;
            }
            else if (dropTargetCell.Value != null)
            {
                // ----------------------------------------
                // 移動先にデータが存在 → 「コンテキストメニュー」
                // ----------------------------------------
                dropMenu.Show(System.Windows.Forms.Cursor.Position);
                return;
            }
            else
            {
                // ----------------------------------------
                // 移動先は空セル → 「受注移動」
                // ----------------------------------------
                if (hit.ColumnIndex < 0)
                    return;

                // Undo情報スタック
                var undoSource = new Order((Order)dragSourceCell.Value);
                var action = new UndoAction()
                {
                    Type = UndoType.CellSwap,
                    SwapSourceRow = dragSourceCell.RowIndex,
                    SwapSourceCol = dragSourceCell.ColumnIndex,
                    SwapSourceOrder = undoSource,
                    SwapTargetCol = dropTargetCell.ColumnIndex,
                    SwapTargetOrder = null
                };
                undoStack.Push(action);
                ButtonUndo.Enabled = true;

                // 移動
                var newTarget = new Order(undoSource);
                newTarget.EditStatus = (undoSource.OrderNo == Common.NEWORDER) ? "1:NEWORDER" : "2:MODIFY";
                dragSourceCell.Value = null;
                dropTargetCell.Value = newTarget;
                dgv.CurrentCell = dropTargetCell;
            }
            dragSourceCell = null;
            isRowHeaderDrag = false;
            RefreshPlanInformation();
        }
        // 「上書き移動」
        private void OverwriteMove_Click(object sender, EventArgs e)
        {
            // Undo情報スタック
            var undoSource = new Order((Order)dragSourceCell.Value);
            var undoTarget = new Order((Order)dropTargetCell.Value);
            var action = new UndoAction()
            {
                Type = UndoType.CellSwap,
                SwapSourceRow = dragSourceCell.RowIndex,
                SwapSourceCol = dragSourceCell.ColumnIndex,
                SwapSourceOrder = undoSource,
                SwapTargetCol = dropTargetCell.ColumnIndex,
                SwapTargetOrder = undoTarget
            };
            undoStack.Push(action);
            ButtonUndo.Enabled = true;

            // 移動
            var newSource = new Order((Order)dragSourceCell.Value);
            var newTarget = new Order((Order)dropTargetCell.Value);
            if (newSource.OrderNo == Common.NEWORDER)
            {
                newSource = null;
            }
            else
            {
                newSource.Qty = 0;
                newSource.EditStatus = "9:CANCEL";
                newTarget.Qty += undoSource.Qty;
                newTarget.EditStatus = "2:MODIFY";
            }
            dragSourceCell.Value = newSource;
            dropTargetCell.Value = newTarget;
            dataGridView1.CurrentCell = dropTargetCell;
        }
        // 「合算移動」
        private void SumMove_Click(object sender, EventArgs e)
        {
            // Undo情報スタック
            var undoSource = new Order((Order)dragSourceCell.Value);
            var undoTarget = new Order((Order)dropTargetCell.Value);
            var action = new UndoAction()
            {
                Type = UndoType.CellSwap,
                SwapSourceRow = dragSourceCell.RowIndex,
                SwapSourceCol = dragSourceCell.ColumnIndex,
                SwapSourceOrder = undoSource,
                SwapTargetCol = dropTargetCell.ColumnIndex,
                SwapTargetOrder = undoTarget
            };
            undoStack.Push(action);
            ButtonUndo.Enabled = true;

            // 合算とクリア
            var sourceOrder = new Order(undoSource);
            if (undoSource.OrderNo == Common.NEWORDER)
            {
                dragSourceCell.Value = null;
            }
            else
            {
                sourceOrder.Qty = 0;
                sourceOrder.EditStatus = "9:CANCEL";
                dragSourceCell.Value = sourceOrder;
            }
            var targetOrder = new Order(undoSource.Qty + undoTarget.Qty, undoTarget.OrderNo, undoTarget.Note, "2:MODIFY");
            dropTargetCell.Value = targetOrder;
            dataGridView1.CurrentCell = dropTargetCell;
        }

        // 「分割」コンテキストメニュー表示
        private void DataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // クリップボード機能（おまけ）
            if (e.Button == MouseButtons.Right && e.ColumnIndex < STARTCOL)
            {
                var dgv = dataGridView1;
                var value = dgv.CurrentCell.Value?.ToString() ?? "";
                if (value == "") return;
                Clipboard.SetText(value);
            }

            if (e.RowIndex < 0 || e.ColumnIndex < STARTCOL) return;

            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                splitSourceCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                dataGridView1.CurrentCell = splitSourceCell;

                splitMenu.Show(System.Windows.Forms.Cursor.Position);
            }
        }
        // 「隔週分割」
        private void Split2_Click(object sender, EventArgs e)
        {
            if (splitSourceCell == null) return;

            int col = splitSourceCell.ColumnIndex;
            int row = splitSourceCell.RowIndex;

            // 分割先セルの計算
            int dstCol = 0;
            if (col + 10 < dataGridView1.ColumnCount)       dstCol = col + 10;
            else if (col + 5 < dataGridView1.ColumnCount)   dstCol = col + 5;
            else if (col + 1 < dataGridView1.ColumnCount)   dstCol = col + 1;
            else                                            dstCol = col - 1;
            splitTargetCell = dataGridView1[dstCol, row];

            // Undo情報スタック
            var undoSource = new Order((Order)splitSourceCell.Value);
            var undoTarget = new Order((Order)splitTargetCell.Value) ?? new Order();
            var action = new UndoAction()
            {
                Type = UndoType.CellSwap,
                SwapSourceRow = splitSourceCell.RowIndex,
                SwapSourceCol = splitSourceCell.ColumnIndex,
                SwapSourceOrder = undoSource,
                SwapTargetCol = dstCol,
                SwapTargetOrder = undoTarget
            };
            undoStack.Push(action);
            ButtonUndo.Enabled = true;

            // 分割本数算出
            int half1 = undoSource.Qty / 2;
            int half2 = undoSource.Qty - half1;

            // 分割
            var newSourceOrder = new Order((Order)splitSourceCell.Value) ?? new Order();
            var newTargetOrder = new Order((Order)splitTargetCell.Value) ?? new Order();
            newSourceOrder.Qty = half1;
            newSourceOrder.EditStatus = "2:MODIFY";
            newTargetOrder.Qty += half2;
            newTargetOrder.EditStatus = 
                (newTargetOrder.OrderNo == Common.NEWORDER) ? "1:NEWORDER" : "2:MODIFY";
            splitSourceCell.Value = newSourceOrder;
            splitTargetCell.Value = newTargetOrder;
            RefreshPlanInformation();
        }
        // 「4分割」
        private void Split4_Click(object sender, EventArgs e)
        {
            if (splitSourceCell == null) return;

            int col = splitSourceCell.ColumnIndex;
            int row = splitSourceCell.RowIndex;
            var dgv = dataGridView1;

            // 分割先セルの計算
            int[] splitCols = new int[4];
            splitCols[0] = col;
            if (col <= STARTCOL + 4)        // 1週目
            {
                splitCols[1] = col + 5;
                splitCols[2] = col + 10;
                splitCols[3] = col + 15;
            }
            else if (col < STARTCOL + 9)    // 2週目
            {
                splitCols[1] = col + 1;
                splitCols[2] = col + 5;
                splitCols[3] = col + 10;
            }
            else                            //3週目以降
            {
                splitCols[1] = col + 1;
                splitCols[2] = col + 2;
                splitCols[3] = col + 3;
            }

            // Undo 情報を積む
            Order[] undoOrders = new Order[4];
            for (int i = 0; i < 4; i++)
            {
                undoOrders[i] = new Order((Order)dgv[splitCols[i], row].Value ?? new Order());
            }
            var action = new UndoAction()
            {
                Type = UndoType.CellSplit4,
                SplitRow = row,
                SplitCols = splitCols,
                SplitOrders = undoOrders
            };
            undoStack.Push(action);

            // 分割値のセット
            int baseQty = undoOrders[0].Qty / 4;
            int first = undoOrders[0].Qty - baseQty * 3; // 端数は最初に寄せる
            dgv[splitCols[0], row].Value = new Order(first, undoOrders[0].OrderNo, undoOrders[0].Note, "2:MODIFY");
            for (int i = 1; i < 4; i++)
            {
                dgv[splitCols[i], row].Value = new Order(baseQty, Common.NEWORDER, "", "1:NEWORDER");
            }
            ButtonUndo.Enabled = true;
            RefreshPlanInformation();
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.CurrentCell == null) return;

            // Ctrl + 矢印キー（Excel 互換）
            if (e.Control &&
                    (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||
                     e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                )
            {
                int row = dgv.CurrentCell.RowIndex;
                int col = dgv.CurrentCell.ColumnIndex;

                // Ctrl + ↓
                if (e.KeyCode == Keys.Down && col >= STARTCOL)
                {
                    int r = row + 1;
                    while (r < dgv.RowCount - 1 && dgv[col, r].Value == null) r++;
                    dgv.CurrentCell = dgv[col, r];
                    e.Handled = true;
                }

                // Ctrl + ↑
                else if (e.KeyCode == Keys.Up && col >= STARTCOL)
                {
                    int r = row - 1;
                    while (r > 0 && dgv[col, r].Value == null) r--;
                    dgv.CurrentCell = dgv[col, r];
                    e.Handled = true;
                }
                // Ctrl + →
                if (e.KeyCode == Keys.Right)
                {
                    int c = (col < STARTCOL) ? STARTCOL : col + 1;
                    while (c < dgv.ColumnCount - 1 && dgv[c, row].Value == null) c++;
                    dgv.CurrentCell = dgv[c, row];
                    e.Handled = true;
                }
                // Ctrl + ←
                else if (e.KeyCode == Keys.Left)
                {
                    int c = (col < STARTCOL) ? 0 : col - 1;
                    while (c > 0 && dgv[c, row].Value == null) c--;
                    dgv.CurrentCell = dgv[c, row];
                    e.Handled = true;
                }
                RefreshPlanInformation();
            }

            // Deleteキーで注文取消し（数量0）
            if (e.KeyCode == Keys.Delete)
            {
                // Undo 情報を積む
                var undoOldOrder = new Order((Order)dgv.CurrentCell.Value);
                Order undoNewOrder = null;
                if (undoOldOrder.OrderNo != Common.NEWORDER) {
                    undoNewOrder = new Order((Order)dgv.CurrentCell.Value);
                    undoNewOrder.Qty = 0;
                    undoNewOrder.EditStatus = "9:CANCEL";
                }
                var action = new UndoAction()
                {
                    Type = UndoType.CellEdit,
                    EditRow = dgv.CurrentCell.RowIndex,
                    EditCol = dgv.CurrentCell.ColumnIndex,
                    OldOrder = undoOldOrder,
                    NewOrder = undoNewOrder
                };
                undoStack.Push(action);
                ButtonUndo.Enabled = true;

                // 値のセット
                dgv.CurrentCell.Value = undoNewOrder;
                RefreshPlanInformation();
            }
        }
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            RefreshPlanInformation();
        }

        // 注文詳細情報、品番詳細情報
        private void RefreshPlanInformation()
        {
            var dgv = dataGridView1;
            if (dgv.CurrentCell == null) return;

            int row = dgv.CurrentCell.RowIndex;
            int col = dgv.CurrentCell.ColumnIndex;

            // 注文情報クリア
            labelOrderNo.Text = "";
            labelQty.Text = "";
            labelTime.Text = "";
            textBoxNote.Text = "";
            textBoxNote.Visible = false;
            labelStatus.Text = "";
            // 品目情報クリア
            labelHMCD.Text = "";
            labelHMNM.Text = "";
            labelCT.Text = "";
            labelHour.Text = "";
            labelHmSize.Text = "";
            labelMatSize.Text = "";
            labelMatQty.Text = "";
            if (dgv.CurrentCell.Value == null) return;

            try
            {
                string hmcd = dgv[0, row].Value.ToString();
                string hmnm = dgv[1, row].Value.ToString();
                decimal ct = (decimal)dgv[COLCT, row].Value;
                decimal hmsize = (decimal)dgv[COLHMSIZE, row].Value;
                decimal matsize = (matDic.ContainsKey(hmcd)) ? matDic[hmcd] : 0m;
                labelHMCD.Text = hmcd;
                labelHMNM.Text = hmnm;
                labelCT.Text = $"{ct}秒";
                labelHour.Text = (ct == 0) ? "0除算エラー" : $"{Math.Floor(3600 / ct / 10) * 10}本";
                labelHmSize.Text = $"{hmsize}mm";
                labelMatSize.Text = (matsize == 0) ? "切削母材情報なし" : $"{matDic[hmcd]:#,0}mm";
                labelMatQty.Text = (matsize == 0) ? "切削母材情報なし" : (hmsize == 0) ? "0除算エラー" : $"{Math.Floor(matsize / hmsize / 10) * 10}本";

                // 注文詳細は OrderCell クラスから取得
                if (col >= STARTCOL)
                {
                    Order o = (Order)dgv.CurrentCell.Value ?? new Order();

                    decimal x = o.Qty * ct;
                    labelOrderNo.Text = o.OrderNo;
                    labelQty.Text = o.Qty.ToString();
                    labelTime.Text = (ct == 0) ? "CT未設定" : (x > 3600) ? $"{x / 3600:0.0}時間" : $"{Math.Ceiling(x / 60)}分";
                    textBoxNote.Text = o.Note;
                    textBoxNote.Visible = true;
                    labelStatus.Text = o.EditStatus;
                }

                // Chartデータの更新
                UpdateChartFromGrid(dataGridView1);
            }
            catch (Exception ex)
            {
                labelOrderNo.Text = ex.Message;
            }
        }

        /*
         *  チャート関連
         */

        private void InitializeChart()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            var area = new ChartArea("MainArea");
            chart1.ChartAreas.Add(area);

            chart1.Legends[0].Docking = Docking.Left;               // 凡例を左側に
            chart1.Legends[0].Alignment = StringAlignment.Near;     // 左寄せ

            var axisX = chart1.ChartAreas["MainArea"].AxisX;
            axisX.Interval = 1;                         // 毎日表示
            axisX.LabelStyle.Angle = -45;               // 斜め表示で見やすく
            axisX.IsLabelAutoFit = false;               // 自動間引きを無効化
            axisX.MajorGrid.Enabled = false;            // グリッド線なし
            axisX.LabelStyle.Font = new Font("Meiryo", 9);
            chart1.ChartAreas["MainArea"].AxisX.IsMarginVisible = true;
            chart1.ChartAreas["MainArea"].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas["MainArea"].AxisX.LabelStyle.IsEndLabelVisible = true;

            var axisY = chart1.ChartAreas["MainArea"].AxisY;
            axisY.Title = "生産時間 [時間]";
            axisY.LabelStyle.Format = "0.0";            // 小数点1桁

            area.AxisY2.Title = "段取り回数";
            area.AxisY2.Enabled = AxisEnabled.True;
            area.AxisY2.MajorGrid.Enabled = false;      // グリッド線なし

            // ▼ 棒グラフ（生産時間）
            var bar = new Series("生産時間")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Double,
                ChartArea = "MainArea",
                IsValueShownAsLabel = true,
                Font = new Font("Meiryo", 7)
            };
            bar.LabelFormat = "0.0";

            // ▼ 折れ線（段取り回数）
            var line = new Series("段取り回数")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Int32,
                YAxisType = AxisType.Secondary
            };

            chart1.Series.Add(bar);
            chart1.Series.Add(line);
        }
        // チャートデータ更新
        private void UpdateChartFromGrid(DataGridView dgv)
        {
            var bar = chart1.Series["生産時間"];
            var line = chart1.Series["段取り回数"];
            bar.Font = new Font("Meiryo", 9);

            bar.Points.Clear();
            line.Points.Clear();

            int dateStartCol = STARTCOL;

            for (int col = dateStartCol; col < dgv.Columns.Count; col++)
            {
                string dateLabel = dgv.Columns[col].HeaderText;

                double totalSeconds = 0;
                HashSet<string> materialSet = new HashSet<string>();

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (row.Cells[col].Value == null) continue;
                    if (!int.TryParse(row.Cells[col].Value.ToString(), out int qty)) continue;
                    if (qty == 0) continue;

                    decimal ct = Convert.ToDecimal(row.Cells["CT"].Value);

                    totalSeconds += (double)(ct * qty);

                    string material = row.Cells["材料"].Value?.ToString();
                    if (!string.IsNullOrEmpty(material))
                        materialSet.Add(material);
                }

                double totalHours = totalSeconds / 3600.0;
                totalHours += materialSet.Count * 0.5d;         // 段取り回数*30分(0.5h)を加算

                // ▼ チャートに追加
                int pointIndex = bar.Points.AddXY(dateLabel, totalHours);
                line.Points.AddXY(dateLabel, materialSet.Count);

                // ▼ 週番号を取得して色を決める
                DateTime dt = DateTime.Parse(dateLabel);
                var ci = System.Globalization.CultureInfo.CurrentCulture;
                int week = ci.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

                // ▼ 偶数週・奇数週で色を変える
                if (week % 2 != 0)
                    bar.Points[pointIndex].Color = Color.SteelBlue;     // 偶数週
                else
                    bar.Points[pointIndex].Color = Color.LightGray;     // 奇数週
            }
        }
        // チャート縦棒クリックイベント
        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult result = chart1.HitTest(e.X, e.Y);

            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                Series series = result.Series;
                int pointIndex = result.PointIndex;
                DataPoint point = series.Points[pointIndex];

                string dateLabel = point.AxisLabel;   // 例: "6/12"

                HighlightColumnByDate(dateLabel);
            }
        }
        // データグリッド日付列の背景をハイライトする
        private void HighlightColumnByDate(string dateLabel)
        {
            int dateColIndex = -1;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.HeaderText == dateLabel)
                {
                    dateColIndex = col.Index;
                    break;
                }
            }
            if (dateColIndex == -1) return;

            highlightedColumnIndex = dateColIndex;  // イベントコントロール変数
            ApplyWeeklyColumnColors();              // 週ごとの色を再適用
            dataGridView1.Columns[highlightedColumnIndex].DefaultCellStyle.BackColor = Color.LightYellow;
        }
        // データグリッド日付列の背景をクリア
        private void ApplyWeeklyColumnColors()
        {
            int dateStartCol = STARTCOL;

            for (int col = dateStartCol; col < dataGridView1.Columns.Count; col++)
            {
                string header = dataGridView1.Columns[col].HeaderText;

                if (!DateTime.TryParse(header, out DateTime dt))
                    continue;

                var ci = System.Globalization.CultureInfo.CurrentCulture;
                int week = ci.Calendar.GetWeekOfYear(dt,
                    System.Globalization.CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Monday);

                // 列単位で背景色を設定
                if (week % 2 == 0)
                    dataGridView1.Columns[col].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else
                    dataGridView1.Columns[col].DefaultCellStyle.BackColor = Color.White;
            }
        }

        // 受注登録（保存）
        private void ButtonEntry_Click(object sender, EventArgs e)
        {
            var dgv = dataGridView1;
            if (cmn.Dba.SavePlanOrder(ref dgv, STARTCOL))
            {
                // EditStatusを初期値に戻す
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    for (int col = STARTCOL; col < dgv.Columns.Count; ++col)
                    {
                        if (dgv[col, row].Value != null)
                        {
                            Order o = (Order)dgv[col, row].Value;
                            o.EditStatus = "";
                        }
                    }
                }
                // 初期データ読み込み直し
                SetInitialValues();

                // 新しいスレッドを作成
                Thread thread = new Thread(ShowMessageBox);
                thread.Start();
                Thread.Sleep(1500);
                IntPtr hWnd = FindWindow(null, "マスタ更新");
                SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }

        // 自動で閉じるメッセージボックス
        private static void ShowMessageBox()
        {
            MessageBox.Show("保存しました．", "マスタ更新");
        }



    }
}
