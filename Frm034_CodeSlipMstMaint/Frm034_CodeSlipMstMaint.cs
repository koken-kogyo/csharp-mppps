﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace MPPPS
{
    public partial class Frm034_CodeSlipMstMaint : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DataTable equipMstDt = new DataTable(); // 設備マスタを保持
        private DataTable codeSlipDt = new DataTable(); // コード票マスタを保持
        private bool loadedFlg = false;

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

            // 可変部分を一旦非表示
            groupBox1.Visible = false;
            groupBox2.Visible = false;

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
            Dgv_CodeSlipMst.ColumnHeadersDefaultCellStyle.BackColor= SystemColors.Control;
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

            // 可変部分をDataGridView作成後に表示
            groupBox1.Visible = true;
            groupBox2.Visible = true;

            // トグルボタンを標準表示側に設定
            tglViewNormal.Select();

            loadedFlg = true;

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
            var selpos = txtHMCD.SelectionStart;
            var sellen = txtHMCD.SelectionLength;
            txtHMCD.Text = txtHMCD.Text.ToUpper();
            txtHMCD.SelectionStart = selpos;
            txtHMCD.SelectionLength = sellen;
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
            var filter = "";
            filter = (txtHMCD.Text.Length == 0) ? string.Empty :
                $"HMCD LIKE '{txtHMCD.Text}*'";
            if (filter != string.Empty && cmbMaterial.SelectedIndex > 0) 
                filter += " and ";
            filter += (cmbMaterial.SelectedIndex <= 0) ? string.Empty :
                $"MATESIZE LIKE '{cmbMaterial.Text}*'";
            // 複数検索条件を設定
            codeSlipDt.DefaultView.RowFilter = filter;
        }

        // セル幅のに合わせてコントロールの位置とサイズを変更
        private void Dgv_CodeSlipMst_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (0 <= e.Column.Index && e.Column.Index <= 2)
            {
                // 品番
                txtHMCD.Left = Dgv_CodeSlipMst.RowHeadersWidth 
                    - groupBox1.Left;
                txtHMCD.Width = Dgv_CodeSlipMst.Columns[0].Width;
                btnHMCDClear.Left = txtHMCD.Left
                    + txtHMCD.Width
                    + 10;
                // 材料
                cmbMaterial.Left = Dgv_CodeSlipMst.RowHeadersWidth 
                    + Dgv_CodeSlipMst.Columns[0].Width 
                    + Dgv_CodeSlipMst.Columns[1].Width 
                    - groupBox1.Left;
                cmbMaterial.Width = Dgv_CodeSlipMst.Columns[2].Width;
                // パネル
                groupBox1.Width = cmbMaterial.Left
                    + cmbMaterial.Width
                    + 30;
            }
        }

        // 標準ビュートグルボタン
        private void tglViewNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (tglViewNormal.Checked)
            {
                tglViewNormal.BackColor = Color.LightGreen;
                tglViewSimple.BackColor = SystemColors.Control;
                viewChange();
            }
        }

        // 簡易ビュートグルボタン
        private void tglViewSimple_CheckedChanged(object sender, EventArgs e)
        {
            if (tglViewSimple.Checked)
            {
                tglViewSimple.BackColor = Color.LightGreen;
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
            if (s2.Length != 17)
            {
                MessageBox.Show("設備マスタに異常があります（列が17個ではない)");
                return;
            }
            for (int i = 0; i < s2.Length; i++)
            {
                Dgv_CodeSlipMst.Columns[i + offset].HeaderText = s2[i];
                Dgv_CodeSlipMst.Columns[i + offset].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Dgv_CodeSlipMst.Columns[i + offset].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Dgv_CodeSlipMst.Columns[i + offset].Width = 40;
            }
            offset += s2.Length;

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
                if (i == 0 || i == 7 || i == 12) // 工程数, ストア, 容器
                {
                    Dgv_CodeSlipMst.Columns[i + offset].Width = 40;
                }
            }
            offset += s3.Length;

            // 新システム用工程経路情報
            string[] ktseq = { "０", "１", "２", "３", "４", "５", "６" };
            for (int j = 1; j <= 6; j++)
            {
                string[] s4 = {
                    $"工程{ktseq[j]}",
                    "設備", 
                    "CT", 
                    "LOT", 
                    "ID" 
                };
                for (int i = 0; i < s4.Length; i++)
                {
                    Dgv_CodeSlipMst.Columns[i + offset].HeaderText = s4[i];
                    Dgv_CodeSlipMst.Columns[i + offset].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Dgv_CodeSlipMst.Columns[i + offset].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Dgv_CodeSlipMst.Columns[i + offset].Width = 40;
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
            // Excelで使用している列
            for (int i = 7; i < 31; i++)
                Dgv_CodeSlipMst.Columns[i].Visible = v;
            // 工程１～工程６ 41,46,51,56,61,66,
            for (int i = 41; i < 67; i = i + 5)
            {
                Dgv_CodeSlipMst.Columns[i + 0].Visible = v; // LOT
                Dgv_CodeSlipMst.Columns[i + 1].Visible = v; // 帳票定義ID
            }
            Dgv_CodeSlipMst.Columns[24].Visible = true;     // 工程数
            Dgv_CodeSlipMst.Columns[33].Visible = v;        // HT
            Dgv_CodeSlipMst.Columns[34].Visible = v;        // 母材品番
            Dgv_CodeSlipMst.Columns[35].Visible = v;        // 母材略称
            Dgv_CodeSlipMst.Columns[37].Visible = v;        // 工程検索キー
            Dgv_CodeSlipMst.Columns[68].Visible = false;    // INSTID
            Dgv_CodeSlipMst.Columns[69].Visible = false;    // INSTDT
            Dgv_CodeSlipMst.Columns[70].Visible = false;    // UPDTID
            Dgv_CodeSlipMst.Columns[71].Visible = false;    // UPDTDT
        }

        // 新システム用に変換
        private void btnConvertMP_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in Dgv_CodeSlipMst.Rows)
            {
                var searchKeys = ""; // [MCGCD-MCCD:]
                var col = 38; // 工程1の列番号
                for (int j = 7; j <= 23; j++) //7:SW ～ 23:TN
                {
                    if (r.Cells[j].Value != null && r.Cells[j].Value != DBNull.Value)
                    {
                        var mcgcd = Dgv_CodeSlipMst.Columns[j].HeaderText;
                        var val = r.Cells[j].Value;
                        DataRow[] equip = equipMstDt.Select($"MCGCD='{mcgcd}' and MCCD='{val}'"); // DataGrid上の設備コードから設備名称取得
                        if (equip.Length == 1)
                        {
                            r.Cells[col + 0].Value = mcgcd;
                            r.Cells[col + 1].Value = equip[0]["MCCD"];
                            searchKeys += mcgcd + "-" + val + ":" ;
                            col += 5;
                        }
                        else
                        {
                            r.Cells[j].Style.BackColor = Color.Red;
                        }
                    }
                }
                // key
                if (searchKeys.Length > 0)
                    r.Cells["KTKEY"].Value = searchKeys;
            }
        }

        // 品番クリア
        private void btnHMCDClear_Click(object sender, EventArgs e)
        {
            txtHMCD.Text = string.Empty;
        }

        // 行削除
        private void Dgv_CodeSlipMst_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (loadedFlg == false) return;
            //MessageBox.Show("整合性が保たれなくなる可能性があります");
            //codeSlipDt.Rows.RemoveAt(e.RowIndex);
        }

        // データベース反映
        private void btnUpdateDatabase_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Dgv_CodeSlipMst.Rows.Count; i++)
            {
                var hmcd = Dgv_CodeSlipMst.Rows[i].Cells[0].Value;
                DataRow[] dr = codeSlipDt.Select($"HMCD='{hmcd}'");
                if (dr.Length == 1)
                {
                    for (int j = 1; j < Dgv_CodeSlipMst.Columns.Count; j++)
                    {
                        dr[0][j] = Dgv_CodeSlipMst.Rows[i].Cells[j].Value;
                    }
                }
            }
            // 一括更新
            cmn.Dba.UpdateCodeSlipMst(ref codeSlipDt);
            MessageBox.Show("更新が終了しました．");
        }

        private void btnReadExcelMaster_Click(object sender, EventArgs e)
        {
            // OpenFileDialog クラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog()
            {
                FileName = "",                           // 既定のファイル名
                InitialDirectory = Common.OFD_INIT_DIR,  // 既定のディレクトリ名
                Filter = Common.OFD_FILE_TYPE_MACRO,     // [ファイルの種類] の選択肢
                FilterIndex = 1,                         // [ファイルの種類] の既定値
                Title = Common.OFD_TITLE_OPEN,           // ダイアログのタイトル
                RestoreDirectory = true,                 // ダイアログを閉じる前に現在のディレクトリを復元
                CheckFileExists = true,                  // 存在しないファイル名前が指定されたとき警告を表示 (既定値: true)
                CheckPathExists = true                   // 存在しないパスが指定されたとき警告を表示 (既定値: true)
            };

            // ダイアログを表示
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // [開く] ボタンがクリックされたとき、選択されたファイル名を表示
                Console.WriteLine(ofd.FileName);

                //cmn.Fa.OpenExcelFile(ofd.FileName);

                //DataTable dataTable = new DataTable();

                cmn.Fa.ReadExcelToDatatble2();


                //cmn.Fa.CloseExcelFile();

                MessageBox.Show("更新が終了しました．");

                //int csvCount = cmn.Fa.ReadCSVFile(ofd.FileName, Encoding.GetEncoding("shift-jis"), true, Common.TABLE_ID_KD8430, ref dataTable);


                //                // DataGridView の内容を全行削除
                //                cmn.RemoveDagaGridViewRows(Dgv_MpOrderTbl);

                //                // DataGridView の書式設定
                //                FormatDataGridView(dataTable);

                //                // KD8430 切削コード票マスタのテーブル情報取得
                //                int dataCount = 0;
                //                DataSet dataSetTblInfo = new DataSet();
                //                dataCount = cmn.Dba.GetTableInfo(ref dataSetTblInfo, Common.TABLE_ID_KD8430);
                //                if (dataCount <= 0)
                //                {
                //                    // テーブル情報なし
                //                    Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
                //                    string msgBodyExtStr = string.Format(Common.MSG_BODY_EXT_STR_TABLE_ID, Common.TABLE_ID_KD8430);
                //                    cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_803, Common.MSG_TYPE_F, MessageBoxButtons.OK,
                //                                       Common.MSGBOX_TXT_FATAL, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, msgBodyExtStr);

                //                    // 件数表示クリア
                //                    Tsl_Msg.Text = null;
                //                }
                //                else
                //                {
                //                    // 書式チェックと数値補正
                //                    bool isValid = true;
                //                    CheckCsvData(dataTable, dataSetTblInfo, ref isValid);

                //                    // データ テーブルを DataGridView に反映して再描画
                //                    Dgv_MpOrderTbl.DataSource = dataTable;

                //                    // 再描画
                //                    Dgv_MpOrderTbl.Refresh();

                //                    if (isValid)
                //                    {
                //                        // 更新系ボタンを有効化
                //                        SetEnableDisableUpdatingButtons();

                //                        // 読み込み完了メッセージ表示
                //                        Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
                //                        cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_405, Common.MSG_TYPE_I, MessageBoxButtons.OK,
                //                                           Common.MSGBOX_TXT_INFO, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                //                        // 読み込み件数表示
                //                        Tsl_Msg.Text = csvCount + Common.TSL_TEXT_READ_FILE_COUNT;
                //                    }
                //                }
            }
        }
    }
}
