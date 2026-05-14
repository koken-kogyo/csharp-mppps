using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm052_MfgPlan : Form
    {
        // 共通クラス
        private readonly Common cmn;

        // DB から取得したデータを格納するクラス
        private class DBRow
        {
            public string 受注番号 { get; set; }
            public string 品番 { get; set; }
            public string 材料 { get; set; }
            public DateTime 日付 { get; set; }
            public int 数量 { get; set; }
        }

        // セルに表示するクラス（数量と受注番号を格納）
        private class OrderCell
        {
            public int Qty { get; set; }
            public string OrderNo { get; set; }

            public OrderCell(int qty, string orderNo)
            {
                Qty = qty;
                OrderNo = orderNo;
            }

            // 表示は数量だけ
            public override string ToString()
            {
                return Qty.ToString();
            }
        }

        // 元に戻すアクション種類
        private enum UndoType
        {
            RowMove,
            CellSwap,
            CellEdit,
            RowDelete,
            RowInsert
        }

        // 元に戻すアクション
        private class UndoAction
        {
            public DataGridView dgv;
            public UndoType Type;

            // 行移動用
            public int SourceRow;
            public int TargetRow;

            // セル入れ替え用
            public int Row1, Col1;
            public object Value1;
            public int Row2, Col2;
            public object Value2;

            // セル編集用
            public int EditRow;
            public int EditCol;
            public object OldValue;
            public object NewValue;

            // 行削除用
            public int DeletedRowIndex;
            public object[] DeletedRowValues;

            // 行挿入用
            //public int InsertRowIndex;
        }

        // UnDoアクション
        Stack<UndoAction> undoStack = new Stack<UndoAction>();  // 元に戻すスタック
        object beforeEditValue = null;                          // Undoセル編集用の「編集中の値」
        private Point dragStartPoint;
        private DataGridViewCell dragSourceCell;
        private bool isRowHeaderDrag = false;

        // コンストラクタ
        public Frm052_MfgPlan(Common cmn)
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

            // イベント登録
            dataGridView1.KeyDown += DataGridView1_KeyDown;
            dataGridView1.CellBeginEdit += DataGridView1_CellBeginEdit;          // セル編集開始
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;              // 小文字大文字変換
            dataGridView1.MouseDown += DataGridView1_MouseDown;                  // ドラッグ＆ドロップ
            dataGridView1.MouseMove += DataGridView1_MouseMove;                  // ドラッグ＆ドロップ
            dataGridView1.DragOver += DataGridView1_DragOver;                    // ドラッグ＆ドロップ
            dataGridView1.DragDrop += DataGridView1_DragDrop;                    // ドラッグ＆ドロップ
            dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;            // 行番号と矢印
            dataGridView1.RowHeaderMouseClick += DataGridView1_RowHeaderMouseClick;
            dataGridView1.AllowDrop = true;                                      // ドラッグ＆ドロップ
        }
        // 初期設定
        private void SetInitialValues()
        {
            DataTable xt2OrderDt = new DataTable();
            bool ret = cmn.Dba.ReadXT2(ref xt2OrderDt);

            List<DBRow> dbList = new List<DBRow>();
            foreach (DataRow row in xt2OrderDt.Rows)
            {
                DBRow r = new DBRow
                {
                    品番 = row["HMCD"].ToString(),
                    材料 = row["MATESIZE"].ToString(),
                    受注番号 = row["ODRNO"].ToString(),
                    日付 = (DateTime)row["EDDT"],
                    数量 = (int)row["ODRQTY"]
                };
                dbList.Add(r);
            }
            SetupGrid(dataGridView1);               // データーグリッドの初期設定
            LoadToGrid(dataGridView1, dbList);      // DBListをセット
        }

        // データーグリッドの初期枠設定
        private void SetupGrid(DataGridView dgv)
        {
            dgv.Columns.Clear();

            // 固定列
            dgv.Columns.Add("品番", "品番");
            dgv.Columns.Add("頭", "頭");
            dgv.Columns.Add("材料", "材料");
            dgv.Columns["品番"].Width = 150;
            dgv.Columns["頭"].Width = 30;
            dgv.Columns["材料"].Width = 65;

            // 6/1〜6/30 の日付列を自動生成
            DateTime start = new DateTime(2026, 6, 1);
            DateTime end = new DateTime(2026, 6, 30);

            for (DateTime d = start; d <= end; d = d.AddDays(1))
            {
                if (d.DayOfWeek == DayOfWeek.Sunday || d.DayOfWeek == DayOfWeek.Saturday)
                {
                    //dgv.Columns[colName].Width = 15;
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
                }
            }
        }
        // 初期データをデータグリッドに貼り付け
        private void LoadToGrid(DataGridView dgv, List<DBRow> dbList)
        {
            dgv.Rows.Clear();

            // 1. 品番一覧を取得
            var 品番一覧 = dbList
                .Select(x => {
                    string 頭 = (x.材料.Length > 0) ? x.材料.Substring(0, 1) : "";
                    string サイズ文字列 = (x.材料.Length >= 2) ? x.材料.Substring(1) : "";
                    decimal.TryParse(サイズ文字列, out decimal サイズ数値);
                    return new
                    {
                        x.品番,
                        頭,
                        x.材料,
                        サイズ = サイズ数値
                    };
                })
                .Distinct()
                .OrderBy(x => x.頭)                  // 材料サイズの先頭1文字 昇順
                .ThenByDescending(x => Convert.ToInt32(x.サイズ))    // 材料サイズ 全体 降順
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
                        return new OrderCell(x.数量, x.受注番号);
                    }
                );

            // 3. DataGridView に行を追加
            foreach (var hinban in 品番一覧)
            {
                int rowIndex = dgv.Rows.Add();
                var row = dgv.Rows[rowIndex];

                row.Cells["品番"].Value = hinban.品番;
                row.Cells["頭"].Value = hinban.頭;
                row.Cells["材料"].Value = hinban.材料;

                // 4. 6/1〜6/30 の日付列にデータをセット
                DateTime start = new DateTime(2026, 6, 1);
                DateTime end = new DateTime(2026, 6, 30);

                for (DateTime d = start; d <= end; d = d.AddDays(1))
                {
                    string colName = d.ToString("MMdd"); // 0601 など

                    if (map.TryGetValue((hinban.品番, d.Date), out OrderCell cell))
                    {
                        row.Cells[colName].Value = cell;
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
        // 隔週で背景色を変更
        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 日付列だけ対象
            if (e.ColumnIndex >= 3)
            {
                string colName = dataGridView1.Columns[e.ColumnIndex].Name; // "0601" など
                if (DateTime.TryParseExact(colName, "MMdd", null, System.Globalization.DateTimeStyles.None, out DateTime dt))
                {
                    // 週番号を取得（ISO週番号）
                    var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
                    int week = cal.GetWeekOfYear(dt, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

                    // 偶数週だけ薄グレー
                    if (week % 2 == 0)
                    {
                        e.CellStyle.BackColor = Color.LightGray;
                    }
                }
            }
        }

        // ショートカットキー
        private void Frm052_FormsPrinting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                ButtonUndo_Click(sender, e);    // ボタンの「元に戻す」を呼び出す
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
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
                dgv = dgv,
                Type = UndoType.RowDelete,
                DeletedRowIndex = rowIndex,
                DeletedRowValues = values
            };
            undoStack.Push(action);
            ButtonUndo.Enabled = true;

            // 実際に削除
            dgv.Rows.RemoveAt(rowIndex);
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

            try
            {
                switch (action.Type)
                {
                    case UndoType.RowMove:
                        // 「行移動」を元に戻す
                        var row = action.dgv.Rows[action.TargetRow];
                        action.dgv.Rows.RemoveAt(action.TargetRow);
                        action.dgv.Rows.Insert(action.SourceRow, row);
                        action.dgv.CurrentCell = action.dgv[0, action.SourceRow];
                        break;

                    case UndoType.CellSwap:
                        // 「セル入れ替え」を元に戻す
                        action.dgv[action.Col1, action.Row1].Value = action.Value1;
                        action.dgv[action.Col2, action.Row2].Value = action.Value2;
                        action.dgv.CurrentCell = action.dgv[action.Col1, action.Row1];
                        break;

                    case UndoType.CellEdit:
                        // 「セル編集」を元に戻す
                        action.dgv[action.EditCol, action.EditRow].Value = action.OldValue;
                        action.dgv.CurrentCell = action.dgv[action.EditCol, action.EditRow];
                        break;

                    case UndoType.RowDelete:
                        // 「行削除」を元に戻す
                        action.dgv.Rows.Insert(action.DeletedRowIndex, action.DeletedRowValues);
                        action.dgv.CurrentCell = action.dgv[0, action.DeletedRowIndex];
                        break;

                    //case UndoType.RowInsert:
                    //    // 挿入された行を削除する
                    //    action.dgv.Rows.RemoveAt(action.InsertRowIndex);
                    //    break;
                }
                if (undoStack.Count == 0) ButtonUndo.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("想定外の動作ですm(__)m\nお困りであればシステム担当者に連絡してください"
                    , action.Type.ToString() + "を元に戻す処理"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 「セル編集」開始
        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var dgv = (DataGridView)sender;
            beforeEditValue = dgv[e.ColumnIndex, e.RowIndex].Value;
        }
        // 「セル編集」終了
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            var afterValue = dgv[e.ColumnIndex, e.RowIndex].Value;

            // 値が変わっていなければ何もしない
            if ((beforeEditValue == null && afterValue == null) ||
                (beforeEditValue != null && beforeEditValue.Equals(afterValue)))
            {
                return;
            }

            // Undo 情報を積む
            var action = new UndoAction()
            {
                dgv = dgv,
                Type = UndoType.CellEdit,
                EditRow = e.RowIndex,
                EditCol = e.ColumnIndex,
                OldValue = beforeEditValue,
                NewValue = afterValue
            };
            undoStack.Push(action);
            ButtonUndo.Enabled = true;
        }
        // 行ヘッダークリック
        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentCell = dataGridView1[0, e.RowIndex];
                dataGridView1.Rows[e.RowIndex].Selected = true;   // 行全体を選択したい場合
            }
        }
        // 「行移動」「セル入れ替え」ドラッグ開始判定
        private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            var dgv = (DataGridView)sender;
            dragStartPoint = e.Location;

            var hit = dgv.HitTest(e.X, e.Y);

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
                else if (hit.ColumnIndex >= 0)
                {
                    // 通常セル → 「セル入れ替え」モード
                    isRowHeaderDrag = false;
                    dragSourceCell = dgv[hit.ColumnIndex, hit.RowIndex];
                }
            }
        }
        // 「行移動」「セル入れ替え」マウス移動
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
        // 「行移動」「セル入れ替え」ドロップ処理
        private void DataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            if (dragSourceCell == null)
            {
                isRowHeaderDrag = false;
                return;
            }

            var dgv = (DataGridView)sender;
            Point clientPoint = dgv.PointToClient(new Point(e.X, e.Y));
            var hit = dgv.HitTest(clientPoint.X, clientPoint.Y);

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
                    dgv = dgv,
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
            else
            {
                // ----------------------------------------
                // 通常セル → 「セル入れ替え」
                // ----------------------------------------
                if (hit.ColumnIndex < 0)
                    return;

                DataGridViewCell targetCell = dgv[hit.ColumnIndex, hit.RowIndex];

                if (targetCell == dragSourceCell)
                    return;

                // Undo情報スタック
                var action = new UndoAction()
                {
                    dgv = dgv,
                    Type = UndoType.CellSwap,
                    Row1 = dragSourceCell.RowIndex,
                    Col1 = dragSourceCell.ColumnIndex,
                    Value1 = dragSourceCell.Value,
                    Row2 = targetCell.RowIndex,
                    Col2 = targetCell.ColumnIndex,
                    Value2 = targetCell.Value
                };
                undoStack.Push(action);
                ButtonUndo.Enabled = true;

                // 実際に値の入れ替え
                object temp = dragSourceCell.Value;
                dragSourceCell.Value = targetCell.Value;
                targetCell.Value = temp;

                dgv.CurrentCell = targetCell;
            }
            dragSourceCell = null;
            isRowHeaderDrag = false;
        }
        // Deleteキーでセルの値を消す
        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var dgv = (DataGridView)sender;
                var beforeEditValue = dgv.CurrentCell.Value;
                var afterValue = string.Empty;
                dgv.CurrentCell.Value = string.Empty;
                // Undo 情報を積む
                var action = new UndoAction()
                {
                    dgv = dgv,
                    Type = UndoType.CellEdit,
                    EditRow = dgv.CurrentCell.RowIndex,
                    EditCol = dgv.CurrentCell.ColumnIndex,
                    OldValue = beforeEditValue,
                    NewValue = afterValue
                };
                undoStack.Push(action);
                ButtonUndo.Enabled = true;
            }
        }








    }
}
