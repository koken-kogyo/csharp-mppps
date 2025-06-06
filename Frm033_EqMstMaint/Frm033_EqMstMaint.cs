﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm033_EqMstMaint : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DataTable equipMstDt = new DataTable(); // 設備マスタを保持

        // null OK の列番号定数
        private static int cColThick = 9;
        private static int cColScrap = 10;

        // 自動で閉じるメッセージボックスで使用
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        private const uint WM_CLOSE = 0x0010;


        public Frm033_EqMstMaint(Common cmn)
        {
            InitializeComponent();
            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_033 + ": " + Common.FRM_NAME_033 + ">";

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

            // データベースからマスタを取得するタスクを登録
            bool ret8420 = await Task.Run(() => ret8420 = cmn.Dba.GetEquipMstDt(ref equipMstDt));
            if (ret8420 == false) return;

            // 設備グループドロップダウン設定
            List<string> mcgcds = equipMstDt.AsEnumerable()
                .OrderBy(x => x["MCGSEQ"])
                .GroupBy(grp => new { 
                    MCGCD = grp["MCGCD"].ToString(),
                    MCGNM = grp["MCGNM"].ToString()
                })
                .Select(row => row.Key.MCGCD + ": " + row.Key.MCGNM)
                .ToList();
            cmbMCGCD.Items.AddRange(mcgcds.ToArray());
            cmbMCGCD.Items.Insert(0, "全て");

            // DataGridViewの初期設定
            Dgv_EquipMst.DataSource = equipMstDt;
            // DataGridViewのヘッダー背景色を設定
            Dgv_EquipMst.EnableHeadersVisualStyles = false;
            Dgv_EquipMst.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            Dgv_EquipMst.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            // DataGridViewの明細を2行毎に背景色設定
            Dgv_EquipMst.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            // DataGridViewの画面ちらつきをおさえるため、DoubleBufferedを有効にする
            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(Dgv_EquipMst, true, null);
            // データグリッドビューの個別設定
            DataGridDetailSetting();
            // DataGridViewのセルの幅を自動調整（ヘッダー以外）
            FixedColumnAfterAutoAdjustment();

            // ステータス表示を初期化
            toolStripStatusLabel1.Text = "設備名称、稼働時間、段取り名称、段取り時間に変更がある場合は直接入力して反映ボタンを押して下さい";
        }

        // コンストラクタ後のロードで画面をアクティブ化し初期フォーカス
        private void Frm033_EqMstMaint_Load(object sender, EventArgs e)
        {
            this.Activate();
            this.Dgv_EquipMst.Focus();
        }

        // キープレビュー
        private void Frm033_EqMstMaint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) btnReloadDatabase_Click(sender, e);
            if (e.KeyCode == Keys.F9) btnUpdateDatabase_Click(sender, e);
            if (e.KeyCode == Keys.Escape) Close();
        }

        // データグリッドビューの個別設定
        private void DataGridDetailSetting()
        {
            // 列ヘッダーの高さを指定
            Dgv_EquipMst.ColumnHeadersHeight = 100;

            // 列ヘッダーの文字列を文字位置を設定
            Dictionary<string, MultipleValues> dic = new Dictionary<string, MultipleValues>();
            dic.Add("MCGSEQ", new MultipleValues { JPNAME = "表示順", Width = 40, StyleAlignment = DataGridViewContentAlignment.MiddleRight });
            dic.Add("MCGCD", new MultipleValues { JPNAME = "ｸﾞﾙｰﾌﾟｺｰﾄﾞ", Width = 60 });
            dic.Add("MCGNM", new MultipleValues { JPNAME = "ｸﾞﾙｰﾌﾟ名称", Width = 100 });
            dic.Add("MCSEQ", new MultipleValues { JPNAME = "表示順", Width = 40, StyleAlignment = DataGridViewContentAlignment.MiddleRight });
            dic.Add("MCCD", new MultipleValues { JPNAME = "設備ｺｰﾄﾞ", Width = 60});
            dic.Add("MCNM", new MultipleValues { JPNAME = "設備名称", Width = 100 });
            dic.Add("TANNM1", new MultipleValues { JPNAME = "主担当", Width = 100 });
            dic.Add("TANNM2", new MultipleValues { JPNAME = "副担当", Width = 100 });
            dic.Add("ONTIME", new MultipleValues { JPNAME = "稼働時間", Width = 60, Format = "#,0", StyleAlignment = DataGridViewContentAlignment.MiddleRight });
            dic.Add("FLG1", new MultipleValues { JPNAME = "", Width = 20 });
            dic.Add("FLG2", new MultipleValues { JPNAME = "", Width = 20 });
            dic.Add("CUTTHICKNESS", new MultipleValues { JPNAME = "切断刃厚", Width = 60, StyleAlignment = DataGridViewContentAlignment.MiddleRight });
            dic.Add("SCRAPLEN", new MultipleValues { JPNAME = "端材長", Width = 60, Format = "#,0", StyleAlignment = DataGridViewContentAlignment.MiddleRight });
            dic.Add("SETUPNM1", new MultipleValues { JPNAME = "段取り１", Width = 140 });
            dic.Add("SETUPTM1", new MultipleValues { JPNAME = "CT1", Width = 60, Format = "#,0", StyleAlignment = DataGridViewContentAlignment.MiddleRight });
            dic.Add("SETUPNM2", new MultipleValues { JPNAME = "段取り２", Width = 140 });
            dic.Add("SETUPTM2", new MultipleValues { JPNAME = "CT2", Width = 60, Format = "#,0", StyleAlignment = DataGridViewContentAlignment.MiddleRight });
            dic.Add("SETUPNM3", new MultipleValues { JPNAME = "段取り３", Width = 140 });
            dic.Add("SETUPTM3", new MultipleValues { JPNAME = "CT3", Width = 60, Format = "#,0", StyleAlignment = DataGridViewContentAlignment.MiddleRight });
            dic.Add("INSTID", new MultipleValues { JPNAME = "登録者", Width = 60 });
            dic.Add("INSTDT", new MultipleValues { JPNAME = "登録日時", Width = 100, Format = "yyyy/MM/dd HH:mm:ss" });
            dic.Add("UPDTID", new MultipleValues { JPNAME = "更新者", Width = 60 });
            dic.Add("UPDTDT", new MultipleValues { JPNAME = "更新日時", Width = 100, Format = "yyyy/MM/dd HH:mm:ss" });

            for (int i = 0; i < Dgv_EquipMst.Columns.Count; i++)
            {
                try
                {
                    var headerName = Dgv_EquipMst.Columns[i].HeaderText;
                    Dgv_EquipMst.Columns[i].HeaderText = dic[headerName].JPNAME;
                    Dgv_EquipMst.Columns[i].Width = dic[headerName].Width;
                    Dgv_EquipMst.Columns[i].HeaderCell.Style.Alignment = dic[headerName].StyleAlignment;
                    Dgv_EquipMst.Columns[i].DefaultCellStyle.Alignment = dic[headerName].StyleAlignment;
                    Dgv_EquipMst.Columns[i].DefaultCellStyle.Format = dic[headerName].Format;
                }
                catch
                {
                    Dgv_EquipMst.Columns[i].HeaderText = Dgv_EquipMst.Columns[i].HeaderText;
                }
            }
        }

        // 行番号をつける
        private void Dgv_EquipMst_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // 行ヘッダのセル領域を、行番号を描画する長方形とする
            // （ただし右端に4ドットのすき間を空ける）
            Rectangle rect = new Rectangle(
              e.RowBounds.Location.X,
              e.RowBounds.Location.Y,
              Dgv_EquipMst.RowHeadersWidth - 4,
              e.RowBounds.Height);

            // 上記の長方形内に行番号を縦方向中央＆右詰めで描画する
            // フォントや前景色は行ヘッダの既定値を使用する
            TextRenderer.DrawText(
              e.Graphics,
              (e.RowIndex + 1).ToString(),
              Dgv_EquipMst.RowHeadersDefaultCellStyle.Font,
              rect,
              Dgv_EquipMst.RowHeadersDefaultCellStyle.ForeColor,
              TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        // 設備選択条件の変更
        private void cmbMCGCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 複数検索条件を設定
            if (cmbMCGCD.SelectedIndex == 0)
            {
                equipMstDt.DefaultView.RowFilter = string.Empty;
            }
            else
            {
                var filter = "MCGCD='" + cmbMCGCD.Text.Split(':')[0] + "'";
                equipMstDt.DefaultView.RowFilter = filter;
            }
            setRowHeight();
        }

        // データベース反映ボタン
        private void btnUpdateDatabase_Click(object sender, EventArgs e)
        {
            int insertCount = 0;
            int modifyCount = 0;
            int deleteCount = 0;
            foreach (DataRow r in equipMstDt.Rows)
            {
                if (r.RowState == DataRowState.Added) insertCount++;
                if (r.RowState == DataRowState.Modified) modifyCount++;
                if (r.RowState == DataRowState.Deleted) deleteCount++;
            }
            if (insertCount + modifyCount + deleteCount > 0)
            {
                // 一括更新
                if (!cmn.Dba.UpdateEquipMst(ref equipMstDt)) return;
                equipMstDt.AcceptChanges(); // これを実行しないと何回も更新されてしまう
                toolStripStatusLabel1.Text = (insertCount > 0) ? $"{insertCount}件 を追加 " : "";
                toolStripStatusLabel1.Text += (modifyCount > 0) ? $"{modifyCount}件 を更新 " : "";
                toolStripStatusLabel1.Text += (deleteCount > 0) ? $"{deleteCount}件 を削除 " : "";
                toolStripStatusLabel1.Text += "しました．";
                toolStrip1.Refresh();

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

        // データベースからマスタを取得
        private void btnReloadDatabase_Click(object sender, EventArgs e)
        {
            equipMstDt.Clear();
            cmn.Dba.GetEquipMstDt(ref equipMstDt);
            Dgv_EquipMst.Refresh();
            Dgv_EquipMst.Focus();
            // 件数を表示
            toolStripStatusLabel1.Text = (Dgv_EquipMst.Rows.Count - 1) + "件を読み込みました。";
        }

        // コピペ処理実装
        private async void Dgv_EquipMst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
               // クリップボードの文字列を取得
                string[] s = Clipboard.GetText().Split('\t');
                if (s[0] == "") s = s.Skip(1).ToArray();        // 先頭が空白だったらカット（データグリッド上のコピペ対応）

                // セル番号を取得
                var col = Dgv_EquipMst.CurrentCell.ColumnIndex;
                var row = Dgv_EquipMst.CurrentCell.RowIndex;

                if (row == Dgv_EquipMst.RowCount - 1)
                {
                    Dgv_EquipMst.CurrentCell = Dgv_EquipMst[1, row];
                    await Task.Delay(200);
                    SendKeys.Send("{Z}+{TAB}"); // +はShiftキー
                    await Task.Delay(800);
                }

                // 選択セルに反映
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] = s[i].Replace(",", "").Replace("\"", "").Replace("\r\n","");
                    if (col + i == cColScrap) // null ok 列（int型だけど 0 にはしたくない）
                    {
                        if (s[i] != "") Dgv_EquipMst[col + i, row].Value = Convert.ToInt32(s[i]);
                    }
                    else if (Dgv_EquipMst[col + i, row].ValueType.Name == "Decimal")
                    {
                        if (s[i] != "") Dgv_EquipMst[col + i, row].Value = Convert.ToDecimal(s[i]);
                    }
                    else if (Dgv_EquipMst[col + i, row].ValueType.Name == "Int32")
                    {
                        Dgv_EquipMst[col + i, row].Value = (s[i] == "") ? 0 : Convert.ToInt32(s[i]);
                    }
                    else if (Dgv_EquipMst[col + i, row].ValueType.Name == "DateTime")
                    {
                        if (s[i] != "") Dgv_EquipMst[col + i, row].Value = DateTime.Parse(s[i]);
                    }
                    else
                    {
                        Dgv_EquipMst[col + i, row].Value = s[i];
                    }
                    if (col + i >= Dgv_EquipMst.ColumnCount - 1) break;
                }
            }
            // Deleteキー
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewCell c in Dgv_EquipMst.SelectedCells)
                {
                    switch (c.ColumnIndex)
                    {
                        case 0:
                        case 1:
                        case 3:
                        case 4:
                            break;
                        case 6:
                        case 12:
                        case 14:
                        case 16:
                            c.Value = 0;
                            break;
                        default:
                            c.Value = DBNull.Value;
                            break;
                    }
                }
            }
        }

        // 表示拡大ボタン
        private void btnDisplayExpantion_Click(object sender, EventArgs e)
        {
            var fntName = Dgv_EquipMst.DefaultCellStyle.Font.Name;
            var fntStyle = Dgv_EquipMst.DefaultCellStyle.Font.Style;
            var fntSize = Dgv_EquipMst.DefaultCellStyle.Font.Size + 3;
            Dgv_EquipMst.DefaultCellStyle.Font = new Font(fntName, fntSize, fntStyle);
            Dgv_EquipMst.ColumnHeadersDefaultCellStyle.Font = new Font(fntName, fntSize, fntStyle);
            setRowHeight();
        }
        // 表示縮小ボタン
        private void btnDisplayReduction_Click(object sender, EventArgs e)
        {
            var fntName = Dgv_EquipMst.DefaultCellStyle.Font.Name;
            var fntStyle = Dgv_EquipMst.DefaultCellStyle.Font.Style;
            var fntSize = Dgv_EquipMst.DefaultCellStyle.Font.Size - 3;
            Dgv_EquipMst.DefaultCellStyle.Font = new Font(fntName, fntSize, fntStyle);
            Dgv_EquipMst.ColumnHeadersDefaultCellStyle.Font = new Font(fntName, fntSize, fntStyle);
            // 現在のフォントに合わせた高さに調整
            setRowHeight();
        }
        // 行高さ調整
        private void setRowHeight()
        {
            int intRowHeight = Int32.Parse(Math.Ceiling(Dgv_EquipMst.DefaultCellStyle.Font.Size).ToString()) + 10;
            for (int i = 0; i < Dgv_EquipMst.Rows.Count; i++)
            {
                Dgv_EquipMst.Rows[i].Height = intRowHeight;
            }
            int intHeaderHeight = Int32.Parse(Math.Ceiling(Dgv_EquipMst.DefaultCellStyle.Font.Size).ToString()) + 15;
            Dgv_EquipMst.ColumnHeadersHeight = intHeaderHeight * 4; // 縦４文字分
        }

        // 列幅自動調整ボタン
        private void btnColumnsAutoFit_Click(object sender, EventArgs e)
        {
            FixedColumnAfterAutoAdjustment();
        }
        // 列幅を自動調整して列幅を取得した後固定化
        private void FixedColumnAfterAutoAdjustment()
        {
            // 列幅を自動調整し列幅を控えておく
            Dgv_EquipMst.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            List<int> w = new List<int>();
            for (int i = 0; i < Dgv_EquipMst.Columns.Count; i++)
            {
                w.Add(Dgv_EquipMst.Columns[i].Width);
            }
            // 列幅を固定にした後、自動調整した幅を復元
            Dgv_EquipMst.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            for (int i = 0; i < Dgv_EquipMst.Columns.Count; i++)
            {
                Dgv_EquipMst.Columns[i].Width = w[i];
            }
        }

        // 小文字を大文字に変換
        private void Dgv_EquipMst_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textBox)
            {
                textBox.CharacterCasing = CharacterCasing.Upper;
            }
        }
    }
}
