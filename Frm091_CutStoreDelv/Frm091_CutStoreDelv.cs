using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm091_CutStoreDelv : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DateTime targetMonth;
        private DataTable dtS0820working;

        public Frm091_CutStoreDelv(Common cmn, object sender)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_091 + ": " + Common.FRM_NAME_091 + ">";

            // 共通クラス
            this.cmn = cmn;

            // 稼働日カレンダーを非同期で取得
            Task.Run(() => cmn.Dba.GetWorkDays(ref dtS0820working));

            // 初期設定
            SetInitialValues();
        }

        private void SetInitialValues()
        {
            // 各種変数の初期化
            // DataGridViewの初期設定
            // https://af-e.net/csharp-change-monthcalendar-color/
            Dgv_Calendar.RowTemplate.Height = 50;
            Dgv_Calendar.ColumnCount = 7;
            Dgv_Calendar.RowCount = 6;
            Dgv_Calendar.RowHeadersVisible = false;
            Dgv_Calendar.ColumnHeadersVisible = true;
            Dgv_Calendar.ReadOnly = true;
            Dgv_Calendar.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // セル内の改行を有効化

            //DataGridViewのContextMenuStripを設定する
            Dgv_Calendar.ContextMenuStrip = this.ContextMenuStrip;

            // 列ヘッダーに曜日を設定
            string[] weeks = { "日", "月", "火", "水", "木", "金", "土" };
            for (var i = 0; i < weeks.Length; i++)
            {
                Dgv_Calendar.Columns[i].HeaderText = weeks[i];
                Dgv_Calendar.Columns[i].Width = this.Width / weeks.Length - 2;
            }
            // 対象月を当月の1日に設定
            targetMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            // カレンダーを作成
            PopulateCalendar();
            // ステータスを表示
            toolStripStatusLabel.Text = "日付をクリックして計画出庫データを作成してくださいい．";
        }

        // カレンダーを作成
        private void PopulateCalendar()
        {
            // 対象月の初日を取得
            DateTime firstDayOfMonth = new DateTime(targetMonth.Year, targetMonth.Month, 1);
            // MPシステムから計画出庫データ出力済の手配日一覧を取得
            DataTable shipmentPlanDt = new DataTable();
            cmn.Dba.GetShipmentPlanSummaryInfo(ref shipmentPlanDt, firstDayOfMonth);
            int row = 0;
            // 前月があれば作成
            DateTime dayOfPrevMonth = firstDayOfMonth.AddDays(-1);
            for (int week = (int)dayOfPrevMonth.DayOfWeek; week >= 0; week--)
            {
                int column = (int)dayOfPrevMonth.DayOfWeek;
                Dgv_Calendar[column, row].Value = dayOfPrevMonth.Day;
                if (shipmentPlanDt.Select($"EDDT='{dayOfPrevMonth}' and 手配件数=発行済件数").Count() != 0)
                {
                    // 製造カード発行済
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightGreen;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.DimGray;
                }
                else if (dtS0820working.Select($"YMD='{dayOfPrevMonth}'").Count() == 0)
                {
                    // 本社非稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightPink;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.DimGray;
                }
                else
                {
                    // 本社稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Color.WhiteSmoke;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.DimGray;
                }
                dayOfPrevMonth = dayOfPrevMonth.AddDays(-1);
            }
            // 対象月のカレンダーを作成
            int daysInMonth = DateTime.DaysInMonth(targetMonth.Year, targetMonth.Month); // 対象月の最終日を取得
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDate = new DateTime(targetMonth.Year, targetMonth.Month, day);
                // 土日の色とEMお休み日の色を変える
                int column = (int)currentDate.DayOfWeek;
                Dgv_Calendar[column, row].Value = day;
                if (column == 0)
                {
                    // 日曜日
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightPink;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                }
                else if (column == 6)
                {
                    // 土曜日
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightBlue;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                    row++;
                }
                else if (shipmentPlanDt.Select($"EDDT='{currentDate}' and 手配件数=発行済件数").Count() != 0)
                {
                    // 計画出庫データ作成済
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightGreen;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                }
                else if (shipmentPlanDt.Select($"EDDT='{currentDate}' and 発行済件数>0 and 手配件数<>発行済件数").Count() != 0)
                {
                    // 計画出庫データ作成後に追加手配あり
                    var totalCount = (long)shipmentPlanDt.Select($"EDDT='{currentDate}'")[0]["手配件数"];
                    var cardPrint = (long)shipmentPlanDt.Select($"EDDT='{currentDate}'")[0]["発行済件数"];
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightCoral;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                    Dgv_Calendar[column, row].Value = day + $"\n未出力が\n{totalCount - cardPrint}件あります";
                }
                else if (shipmentPlanDt.Select($"EDDT='{currentDate}' and 発行済件数=0 and 手配件数>0").Count() != 0)
                {
                    // 手配あり計画出庫データなし
                    var totalCount = (long)shipmentPlanDt.Select($"EDDT='{currentDate}'")[0]["手配件数"];
                    var cardPrint = (long)shipmentPlanDt.Select($"EDDT='{currentDate}'")[0]["発行済件数"];
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightCyan;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                }
                else if (dtS0820working.Select($"YMD='{currentDate}'").Count() == 0)
                {
                    // 本社非稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightPink;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                }
                else
                {
                    // 本社稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Color.White;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                }
            }
            // 翌月の余りカレンダーを埋める
            int dayOfNextMonth = 1;
            while (row < 6){
                DateTime nextDate = new DateTime(targetMonth.AddMonths(1).Year, targetMonth.AddMonths(1).Month, dayOfNextMonth);
                int column = (int)nextDate.DayOfWeek;
                Dgv_Calendar[column, row].Value = dayOfNextMonth;
                if (column == 0)
                {
                    // 日曜日
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightPink;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.DimGray;
                }
                else if (column == 6)
                {
                    // 土曜日
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightBlue;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.DimGray;
                    row++;
                }
                else if (shipmentPlanDt.Select($"EDDT='{nextDate}' and 手配件数=発行済件数").Count() != 0)
                {
                    // 計画出庫データ作成済
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightGreen;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.DimGray;
                }
                else if (shipmentPlanDt.Select($"EDDT='{nextDate}' and 発行済件数>0 and 手配件数<>発行済件数").Count() != 0)
                {
                    // 計画出庫データ作成後に追加手配あり
                    var totalCount = (long)shipmentPlanDt.Select($"EDDT='{nextDate}'")[0]["手配件数"];
                    var cardPrint = (long)shipmentPlanDt.Select($"EDDT='{nextDate}'")[0]["発行済件数"];
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightCoral;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                    Dgv_Calendar[column, row].Value = dayOfNextMonth + $"\n未出力が\n{totalCount - cardPrint}件あります";
                }
                else if (shipmentPlanDt.Select($"EDDT='{nextDate}' and 発行済件数=0 and 手配件数>0").Count() != 0)
                {
                    // 手配あり計画出庫データなし
                    var totalCount = (long)shipmentPlanDt.Select($"EDDT='{nextDate}'")[0]["手配件数"];
                    var cardPrint = (long)shipmentPlanDt.Select($"EDDT='{nextDate}'")[0]["発行済件数"];
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightCyan;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                }
                else if (dtS0820working.Select($"YMD='{nextDate}'").Count() == 0)
                {
                    // 本社非稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightPink;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.DimGray;
                }
                else
                {
                    // 本社稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Color.WhiteSmoke;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.DimGray;
                }
                dayOfNextMonth++;
            }
            // 今日から明日までの範囲で取得
            int days = (int)dtS0820working.Select(String.Format(
                "#{1}# <= [{0}] AND [{0}] < #{2}#",
                "YMD", targetMonth, targetMonth.AddMonths(1))).Count();
            CalendarLabel.Text = $"{targetMonth.Month}月 カレンダー ({days}日稼働)";
        }

        // 計画出庫データ作成
        private int WriteCSVShipmentPlan(DataTable shipmentPlanDt)
        {
            int ret = -1;
            // 計画出庫データをCSVファイルに保存
            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                FileName = Common.OUTBOUND_INFO_FILE,     // 既定のファイル名
                InitialDirectory = Common.SFD_INIT_DIR,   // 既定のディレクトリ名
                Filter = Common.SFD_FILE_TYPE_CSV,        // [ファイルの種類] の選択肢
                FilterIndex = 1,                          // [ファイルの種類] の既定値
                Title = Common.SFD_TITLE_SAVE,            // ダイアログのタイトル
                RestoreDirectory = true,                  // ダイアログを閉じる前に現在のディレクトリを復元
                CheckFileExists = false,                  // 存在しないファイル名前が指定されたとき警告を表示 (既定値: true)
                CheckPathExists = true                    // 存在しないパスが指定されたとき警告を表示 (既定値: true)
            })
            {
                // ダイアログを表示
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // [保存] または上書き確認で [はい] をクリック時、ファイルを保存
                    string fileName = Path.Combine(sfd.InitialDirectory, sfd.FileName);
                    ret = cmn.Fa.SaveCSVFile(shipmentPlanDt, Path.Combine(fileName),"UTF8"); // 保存先を変更すると InitialDirectory が自動更新される

                    switch (ret)
                    {

                        case Common.SFD_RET_SAVE_FAILED: // 保存失敗
                            Debug.WriteLine(Common.MSGBOX_TXT_INFO + ": " + MethodBase.GetCurrentMethod().Name);
                            cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_402, Common.MSG_TYPE_E,
                                MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);
                            break;

                        case Common.SFD_RET_AUTH_FAILED: // 認証失敗
                            Debug.WriteLine(Common.MSGBOX_TXT_INFO + ": " + MethodBase.GetCurrentMethod().Name);
                            cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_403, Common.MSG_TYPE_E,
                                MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);
                            break;

                        case Common.SFD_RET_FILE_IN_USE: // ファイル使用中
                            Debug.WriteLine(Common.MSGBOX_TXT_INFO + ": " + MethodBase.GetCurrentMethod().Name);
                            cmn.ShowMessageBox(Common.KCM_PGM_ID, Common.MSG_CD_404, Common.MSG_TYPE_E,
                                MessageBoxButtons.OK, Common.MSGBOX_TXT_ERR, MessageBoxIcon.Error);
                            break;
                        default: // 保存成功
                            // 保存ファイルを選択した状態でフォルダーを開く
                            Process.Start(Common.APP_EXPLORER, @"/select,""" + @fileName + "");
                            // 保存件数表示
                            toolStripStatusLabel.Text = ret + Common.TSL_TEXT_SAVE_FILE_COUNT;
                            toolStripStatusLabel.Text = ret + "件の計画出庫データを出力しました.";
                            break;
                    }
                }
            }
            return ret;
        }

        private void PrevMonthButton_Click(object sender, EventArgs e)
        {
            targetMonth = targetMonth.AddMonths(-1);
            PopulateCalendar();
            if (targetMonth <= DateTime.Now.AddMonths(-5))
            {
                PrevMonthButton.Enabled = false;
            }
            NextMonthButton.Enabled = true;
            toolStripStatusLabel.Text = "日付をクリックして計画出庫データを作成してくださいい．";
        }

        private void NextMonthButton_Click(object sender, EventArgs e)
        {
            targetMonth = targetMonth.AddMonths(1);
            PopulateCalendar();
            if (targetMonth >= DateTime.Now.AddMonths(7))
            {
                NextMonthButton.Enabled = false;
            }
            PrevMonthButton.Enabled = true;
            toolStripStatusLabel.Text = "日付をクリックして計画出庫データを作成してくださいい．";
        }

        private void CalendarLabel_Click(object sender, EventArgs e)
        {
            targetMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            PopulateCalendar();
            PrevMonthButton.Enabled = true;
            NextMonthButton.Enabled = true;
        }

        private void Dgv_Calendar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Dgv_Calendar.ClearSelection();
                // 処理対象外を判定
                if (Dgv_Calendar[e.ColumnIndex, e.RowIndex].Value == null) return;
                if (Dgv_Calendar[e.ColumnIndex, e.RowIndex].Style.BackColor == Color.LightGreen)
                {
                    toolStripStatusLabel.Text = "計画出庫データは既に作成済です．";
                    return;
                }
                Color[] badColors = new Color[] { Color.LightPink, Color.LightBlue };
                if (Array.IndexOf(badColors, Dgv_Calendar[e.ColumnIndex, e.RowIndex].Style.BackColor) >= 0) return;
                // クリックされたセルの日付を取得
                var s = Dgv_Calendar[e.ColumnIndex, e.RowIndex].Value.ToString();
                var day = int.Parse(Regex.Match(s, @"^\d+").ToString()); // 先頭の数値を正規表現で切り出し
                int offset = 0;
                if (e.RowIndex == 0 && day > 15) offset = -1;
                if (e.RowIndex >= 4 && day < 15) offset = 1;
                var planDay = new DateTime(targetMonth.AddMonths(offset).Year, targetMonth.AddMonths(offset).Month, day);
                // 計画出庫データを取得
                var shipmentPlanDt = new DataTable();
                var rowsCount = cmn.Dba.GetShipmentPlanInfo(ref shipmentPlanDt, planDay);
                // 計画出庫データ作成
                if (rowsCount > 0)
                {
                    toolStripStatusLabel.Text = $"{rowsCount}件の計画出庫データがあります.";
                    if (cmn.ShowMessageBox(Common.KCM_PGM_ID, $" {planDay.ToString("M/d")}日の計画出庫データを出力しますか？", Common.MSG_TYPE_Q,
                                MessageBoxButtons.OKCancel, Common.MSGBOX_TXT_INFO, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        // CSVファイルの作成と出力
                        var writeCnt = WriteCSVShipmentPlan(shipmentPlanDt);
                        //データベースの更新
                        if (writeCnt > 0)
                        {
                            var updateCnt = cmn.Dba.UpdateShipmentPlanDay(planDay);
                            if (writeCnt == updateCnt)
                            {
                                // カレンダーの再読み込み
                                PopulateCalendar();
                            }
                            else
                            {
                                MessageBox.Show("データベースの更新で重大なエラーが発生しました\n作成済みのCSVファイルを破棄してください");
                            }
                        }
                    }
                }
                else
                {
                    toolStripStatusLabel.Text = "対象データは存在しませんでした.";
                }
            }
        }

        private void Frm091_CutStoreDelv_Activated(object sender, EventArgs e)
        {
            Dgv_Calendar.ClearSelection(); // Initialize中やLoad中には効かない
        }
    }
}
