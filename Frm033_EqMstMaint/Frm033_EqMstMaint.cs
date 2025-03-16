using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm033_EqMstMaint : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DataTable equipMstDt = new DataTable(); // 設備マスタを保持

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
            toolStripLabel1.Text = "設備名称、稼働時間、段取り名称、段取り時間に変更がある場合は直接入力して反映ボタンを押して下さい";
        }

        // データグリッドビューの個別設定
        private void DataGridDetailSetting()
        {
            // 列ヘッダーの高さを指定
            Dgv_EquipMst.ColumnHeadersHeight = 100;

            // 列ヘッダーの文字列を文字位置を設定
            var offset = 0;
            string[] s1 = {
                "表示順",
                "ｸﾞﾙｰﾌﾟｺｰﾄﾞ",
                "ｸﾞﾙｰﾌﾟ名称",
                "表示順",
                "設備ｺｰﾄﾞ",
                "設備名称",
                "稼働時間",
                "",
                "",
                "切断刃厚",
                "端材長",
                "段取り１",
                "CT",
                "段取り２",
                "CT",
                "段取り３",
                "CT",
                "登録ID",
                "登録日時",
                "更新ID",
                "更新日時"
            };
            for (int i = 0; i < s1.Length; i++)
            {
                Dgv_EquipMst.Columns[i + offset].HeaderText = s1[i];
                if (i==0 || i==3) // 各SEQ
                {
                    Dgv_EquipMst.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Dgv_EquipMst.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Dgv_EquipMst.Columns[i + 0].ReadOnly = true;
                    Dgv_EquipMst.Columns[i + 1].ReadOnly = true;
                }
                if (i == 6 || i == 9 || i == 10 || i == 12 || i == 14 || i == 16)// 稼働時間、刃厚、端材長、各CT
                {
                    Dgv_EquipMst.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Dgv_EquipMst.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (i != 9)
                        Dgv_EquipMst.Columns[i].DefaultCellStyle.Format = "#,0"; // 刃厚以外
                }
            }
            offset += s1.Length;

            // デフォルトセルスタイル
            Dgv_EquipMst.Columns[18].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss"; // 登録日時
            Dgv_EquipMst.Columns[20].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss"; // 更新日時

            // DataGridViewの非表示設定
            Dgv_EquipMst.Columns[7].Visible = false;    // FLG1
            Dgv_EquipMst.Columns[8].Visible = false;    // FLG2
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
            // 一括更新
            cmn.Dba.UpdateEquipMst(ref equipMstDt);
            MessageBox.Show("更新が終了しました．");
        }

        // コピペ処理実装
        private void Dgv_EquipMst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
               // クリップボードの文字列を取得
                string[] s = Clipboard.GetText().Split('\t');

                // セル番号を取得
                var col = Dgv_EquipMst.CurrentCell.ColumnIndex;
                var row = Dgv_EquipMst.CurrentCell.RowIndex;

                // 選択セルに反映
                for (int i = 0; i < s.Length; i++)
                {
                    if (col + i == 7) col += 2; // 非表示の列があるので、その場合は2つ右にずらす
                    if (Dgv_EquipMst[col + i, row].ValueType.Name == "Int32")
                    {
                        s[i] = s[i].Replace(",", "").Replace("\"","");
                        Dgv_EquipMst[col + i, row].Value = Int32.Parse(s[i]);
                    }
                    else
                    {
                        Dgv_EquipMst[col + i, row].Value = s[i];
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

        // データベースからマスタを取得
        private void btnReloadDatabase_Click(object sender, EventArgs e)
        {
            equipMstDt.Clear();
            cmn.Dba.GetEquipMstDt(ref equipMstDt);
            Dgv_EquipMst.Refresh();
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
    }
}
