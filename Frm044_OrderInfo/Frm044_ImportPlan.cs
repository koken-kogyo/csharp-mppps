using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm044_ImportPlan : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DateTime targetMonth;                   // 処理対象月を設定
        private DataTable orderDt = new DataTable();    // 内示状況を保持（カレンダー詳細情報で使用）
        private DataTable dtS0820working;               // カレンダーテーブルを保持

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="common">共通クラス</param>
        /// <param name="sender">送信オジェクト</param>
        public Frm044_ImportPlan(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_041 + ": " + Common.FRM_NAME_041 + ">";

            // 共通クラス
            this.cmn = cmn;

            // 稼働日カレンダーを非同期で取得
            Task.Run(() => cmn.Dba.GetWorkDays(ref dtS0820working));

            // 初期設定
            SetInitialValues();
        }

        // コントロールの初期化
        private void SetInitialValues()
        {
            // DataGridViewの初期設定
            // https://af-e.net/csharp-change-monthcalendar-color/
            Dgv_Calendar.RowTemplate.Height = 50;
            Dgv_Calendar.ColumnCount = 7;
            Dgv_Calendar.RowCount = 6;
            Dgv_Calendar.RowHeadersVisible = false;
            Dgv_Calendar.ColumnHeadersVisible = true;
            Dgv_Calendar.ReadOnly = true;
            Dgv_Calendar.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // セル内の改行を有効化

            // 列ヘッダーに曜日を設定
            string[] weeks = { "日", "月", "火", "水", "木", "金", "土" };
            for (var i = 0; i < weeks.Length; i++)
            {
                Dgv_Calendar.Columns[i].HeaderText = weeks[i];
                Dgv_Calendar.Columns[i].Width = this.Width / weeks.Length - 2;
            }

            // 処理対象月を設定
            targetMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            // カレンダーを作成
            PopulateCalendar();

            // カレンダーのサイズ調整
            Frm044_CreateMaybeOrder_Resize(this, EventArgs.Empty);
        }

        // カレンダー関連コントロールの初期化
        private void ClearCalendar()
        {
            // カレンダーラベルを設定
            CalendarLabel.Text = $"{targetMonth.Month}月 カレンダー";
            PrevMonthButton.Enabled = false;
            NextMonthButton.Enabled = false;

            // データのクリアと背景色のクリア
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (col != 0 && col != 6)
                    {
                        Dgv_Calendar[col, row].Style.BackColor = Common.FRM40_BG_COLOR_WORKING;
                    }
                    Dgv_Calendar[col, row].Value = "";
                }
            }
            // 選択セルの解除
            Dgv_Calendar.ClearSelection();

        }

        // EMデータテーブルとMPデータテーブルをマージ
        private void MargeOrderDataTable(ref DataTable emOrderDt, ref DataTable mpOrderDt)
        {
            orderDt.Clear();
            orderDt = emOrderDt.Copy();

            // EM注文データテーブルに列を追加
            if (orderDt.Columns.Count == 3)
            {
                orderDt.Columns.Add("MP本数", typeof(long));
                orderDt.Columns.Add("MP実績数", typeof(long));
            }

            // MPデータの内容をEM注文データにマージ
            foreach (DataRow r in orderDt.Rows)
            {
                if (mpOrderDt.Select($"EDDT='{r["EDDT"]}'").Length > 0)
                {
                    r["MP本数"] = mpOrderDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP本数"];
                    r["MP実績数"] = mpOrderDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP実績数"];
                }
                else
                {
                    r["MP本数"] = 0;
                    r["MP実績数"] = 0;
                }
            }
        }

        // カレンダーを作成
        private async void PopulateCalendar()
        {
            // カレンダー関連の初期化
            ClearCalendar();

            // データベースから注文情報を非同期並列処理で取得（EM_Oracle:D0440, MP_MySQL:KD8440)
            toolStripStatusLabel.Text = "内示データ確認中...";
            DataTable emPlanDt = new DataTable();
            DataTable mpPlanDt = new DataTable();
            bool retEM = false;
            bool retMP = false;
            var taskEM = Task.Run(() => retEM = cmn.Dba.GetEmPlanSummaryInfo(ref emPlanDt, targetMonth));
            var taskMP = Task.Run(() => retMP = cmn.Dba.GetMpPlanSummaryInfo(ref mpPlanDt, targetMonth));
            // 両者が完了するまで待機する
            await Task.WhenAll(taskEM, taskMP);

            if (retEM == false || retMP == false) return;

            // EMデータテーブルとMPデータテーブルをマージ
            MargeOrderDataTable(ref emPlanDt, ref mpPlanDt);

            int row = 0; // カレンダー１行目

            // 前月があれば作成
            DateTime dayOfPrevMonth = targetMonth.AddDays(-1);
            for (int week = (int)dayOfPrevMonth.DayOfWeek; week >= 0; week--)
            {
                int column = (int)dayOfPrevMonth.DayOfWeek;
                Dgv_Calendar[column, row].Value = dayOfPrevMonth.Day;
                if (orderDt.Select($"EDDT='{dayOfPrevMonth}' and MP本数=0 and EM本数>0").Count() != 0)
                {
                    // 取込データなし
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_ORDERED;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                }
                else if (orderDt.Select($"EDDT='{dayOfPrevMonth}' and MP本数>0").Count() != 0)
                {
                    // 取込済
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_PLANED;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_DIMGRAY;
                }
                else if (dtS0820working.Select($"YMD='{dayOfPrevMonth}'").Count() == 0)
                {
                    // 本社非稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_HOLIDAY;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_DIMGRAY;
                }
                else
                {
                    // 本社稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_OTHERMONTH;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_DIMGRAY;
                }
                dayOfPrevMonth = dayOfPrevMonth.AddDays(-1);
            }

            // 対象月のカレンダーを作成
            int daysInMonth = DateTime.DaysInMonth(targetMonth.Year, targetMonth.Month); // 対象月の最終日を取得
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDate = new DateTime(targetMonth.Year, targetMonth.Month, day);
                // 日付を設定
                int column = (int)currentDate.DayOfWeek;
                Dgv_Calendar[column, row].Value = day;
                // 土曜日：青
                // 日曜日とEMお休み日：ピンク
                // 取込済：薄緑
                // 未取込：薄青
                // EMに変化（追加・取消）あり：赤
                if (column == 0)
                {
                    // 日曜日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_HOLIDAY;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                }
                else if (column == 6)
                {
                    // 土曜日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_SATURDAY;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                    row++;
                }
                else if (orderDt.Select($"EDDT='{currentDate}' and MP本数=0 and EM本数>0").Count() != 0)
                {
                    // 注文あり取込データなし
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_ORDERED;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                }
                else if (orderDt.Select($"EDDT='{currentDate}' and MP本数>0").Count() != 0)
                {
                    // 注文データ取込済
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_PLANED;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                }
                else if (dtS0820working.Select($"YMD='{currentDate}'").Count() == 0)
                {
                    // 本社非稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_HOLIDAY;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                }
                else
                {
                    // 本社稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_WORKING;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                }
            }

            // 翌月の余りカレンダーを埋める
            int dayOfNextMonth = 1;
            while (row < 6)
            {
                DateTime nextDate = new DateTime(targetMonth.AddMonths(1).Year, targetMonth.AddMonths(1).Month, dayOfNextMonth);
                int column = (int)nextDate.DayOfWeek;
                Dgv_Calendar[column, row].Value = dayOfNextMonth;
                if (column == 0)
                {
                    // 日曜日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_HOLIDAY;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_DIMGRAY;
                }
                else if (column == 6)
                {
                    // 土曜日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_SATURDAY;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_DIMGRAY;
                    row++;
                }
                else if (orderDt.Select($"EDDT='{nextDate}' and MP本数=0 and EM本数>0").Count() != 0)
                {
                    // 注文あり取込データなし
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_ORDERED;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                }
                else if (orderDt.Select($"EDDT='{nextDate}' and MP本数>0").Count() != 0)
                {
                    // 注文データ取込済
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_PLANED;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_DIMGRAY;
                }
                else if (dtS0820working.Select($"YMD='{nextDate}'").Count() == 0)
                {
                    // 本社非稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_HOLIDAY;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_DIMGRAY;
                }
                else
                {
                    // 本社稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_OTHERMONTH;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_DIMGRAY;
                }
                dayOfNextMonth++;
            }

            // 集計値を土曜日欄に表示
            CalendarAddSummary(ref emPlanDt, ref mpPlanDt);

            // 当日のフォントの色を変える
            //CalendarToday();

            // 対象月の稼働日数を算出（非同期処理データテーブルの為処理の後ろの方で実行）
            int days = (int)dtS0820working.Select(String.Format(
                "#{1}# <= [{0}] AND [{0}] < #{2}#",
                "YMD", targetMonth, targetMonth.AddMonths(1))).Count();
            CalendarLabel.Text = $"{targetMonth.Month}月 カレンダー ({days}日稼働)";
            PrevMonthButton.Enabled = true;
            NextMonthButton.Enabled = true;
            toolStripStatusLabel.Text = "";
        }

        // DGV上の月曜日から金曜日までの件数をサマリーして土曜日にぶち込む
        private void CalendarAddSummary(ref DataTable emOrderDt, ref DataTable mpOrderDt)
        {
            for (int row = 0; row < 6; row++)
            {
                int totalEMQty = 0;
                int totalMPQty = 0;
                for (int col = 0; col < 7; col++)
                {
                    var eddt = GetCurrentDateTime(Dgv_Calendar[col, row]);
                    if (eddt == DateTime.Now.Date)
                    {
                        Dgv_Calendar[col, row].Style.ForeColor = Color.Red;
                    }
                    if (emOrderDt.Select($"EDDT='{eddt}'").Length > 0)
                    {
                        totalEMQty += int.Parse(emOrderDt.Select($"EDDT='{eddt}'")[0]["EM本数"].ToString());
                    }
                    if (mpOrderDt.Select($"EDDT='{eddt}'").Length > 0)
                    {
                        totalMPQty += int.Parse(mpOrderDt.Select($"EDDT='{eddt}'")[0]["MP本数"].ToString());
                    }
                }
                var diff = totalEMQty - totalMPQty;
                if (diff == 0 && totalEMQty > 0)
                {
                    Dgv_Calendar[6, row].Value += $"\n {totalEMQty.ToString("#,0")}本";
                }
                else if (diff > 0)
                {
                    Dgv_Calendar[6, row].Value += $"\n {diff}本増加" + 
                        "\n(" + totalEMQty.ToString("#,0") + "/" + totalMPQty.ToString("#,0") + ")";
                }
                else if (diff < 0)
                {
                    Dgv_Calendar[6, row].Value += $"\n {diff*-1}本減少" +
                        "\n(" + totalEMQty.ToString("#,0") + "/" + totalMPQty.ToString("#,0") + ")";
                }
            }
        }

        // 前日ボタン
        private void PrevMonthButton_Click(object sender, EventArgs e)
        {
            targetMonth = targetMonth.AddMonths(-1);
            PopulateCalendar();
            if (targetMonth <= DateTime.Now.AddMonths(-5))
            {
                PrevMonthButton.Enabled = false;
            }
            NextMonthButton.Enabled = true;
        }

        // 当月を表示
        private void CalendarLabel_Click(object sender, EventArgs e)
        {
            targetMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            PopulateCalendar();
            PrevMonthButton.Enabled = true;
            NextMonthButton.Enabled = true;
        }

        // 翌月ボタン
        private void NextMonthButton_Click(object sender, EventArgs e)
        {
            targetMonth = targetMonth.AddMonths(1);
            PopulateCalendar();
            if (targetMonth >= DateTime.Now.AddMonths(7))
            {
                NextMonthButton.Enabled = false;
            }
            PrevMonthButton.Enabled = true;
        }

        // フォームサイズの可変に対応
        private void Frm044_CreateMaybeOrder_Resize(object sender, EventArgs e)
        {
            float w = ClientSize.Height; //縦幅
            // CalendarLabel.Text = w.ToString();
            if (w > 0 && w <= 600) Dgv_Calendar.Font = new Font("Yu Gothic UI", 9);
            if (w > 600 && w <= 700) Dgv_Calendar.Font = new Font("Yu Gothic UI", 11);
            if (w > 700 && w <= 800) Dgv_Calendar.Font = new Font("Yu Gothic UI", 13);
            if (w > 800) Dgv_Calendar.Font = new Font("Yu Gothic UI", 16);
            PrevMonthButton.Width = Dgv_Calendar.Width / 6 - 5;
            CalendarLabel.Left = PrevMonthButton.Width + 10;
            CalendarLabel.Width = Dgv_Calendar.Width / 6 * 4 - 5;
            NextMonthButton.Left = 5 + PrevMonthButton.Width + 5 + CalendarLabel.Width + 5;
            NextMonthButton.Width = Dgv_Calendar.Width / 6 - 5;
            if (Dgv_Calendar.Rows.Count <= 0) return;
            for (var i = 0; i < 6; i++)
                Dgv_Calendar.Rows[i].Height = (Dgv_Calendar.Height - Dgv_Calendar.ColumnHeadersHeight) / 6 - 4;
            for (var i = 0; i < 7; i++)
                Dgv_Calendar.Columns[i].Width = (Dgv_Calendar.Width / 7);
            Btn_ImportPlan.Width = Dgv_Calendar.Width - 5;
        }

        // ステータスにDB状態（取込済、印刷済）を表示（複数選択可能）
        private void Dgv_Calendar_MouseUp(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel.Text = "";
            int emQty = 0;
            int mpQty = 0;
            int emJiQty = 0;
            int mpJiQty = 0;
            foreach (DataGridViewCell c in Dgv_Calendar.SelectedCells)
            {
                var planDay = GetCurrentDateTime(c);
                if (orderDt.Select($"EDDT='{planDay}'").Length > 0)
                {
                    DataRow r = orderDt.Select($"EDDT='{planDay}'")[0];
                    emQty += Int32.Parse(r["EM本数"].ToString());
                    mpQty += Int32.Parse(r["MP本数"].ToString());
                    emJiQty += Int32.Parse(r["EM実績数"].ToString());
                    mpJiQty += Int32.Parse(r["MP実績数"].ToString());
                }
            }
            if (emQty - mpQty == 0)
            {
                toolStripStatusLabel.Text = $"手配日程 {emQty.ToString("#,0")}本";
                toolStripStatusLabel.Text += (emJiQty > 0) ? $" ／ 実績数 {emJiQty.ToString("#,0")}本" : "";
            }
            else
            {
                toolStripStatusLabel.Text = $"手配日程 EM{emQty.ToString("#,0")}本 / MP{mpQty.ToString("#,0")}本";
            }
        }

        // DataGridViewの選択セル日付を日付型に変換して返却する関数
        private DateTime GetCurrentDateTime(DataGridViewCell c)
        {
            var s = c.Value.ToString();
            var day = int.Parse(Regex.Match(s, @"^\d+").ToString()); // 先頭の数値を正規表現で切り出し
            int offset = 0;
            if (c.RowIndex == 0 && day > 15) offset = -1;
            if (c.RowIndex >= 4 && day < 15) offset = 1;
            var planDay = new DateTime(targetMonth.AddMonths(offset).Year, targetMonth.AddMonths(offset).Month, day);
            return planDay;
        }

        // 内示データ取込直し
        private async void Btn_ImportPlan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("切削システム内の手配日程データを再作成します\n" + 
                "よろしいですか？", "手配日程データ再作成", MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question) == DialogResult.No) return;

            toolStripStatusLabel.Text = "内示データ取込中...";

            // データベースから注文情報を非同期並列処理で取得（Oracle:D0410, MySQL:KD8430)
            DataTable emOrderDt = new DataTable();

            // EMから手配日程ファイルを読み込む
            if (await Task.Run(() => cmn.Dba.GetEmPlan(ref emOrderDt)) == false) return;

            // MP切削手配日程ファイルを全削除して、EM手配日程ファイルをそのまま取り込む
            if (await Task.Run(() => cmn.Dba.ImportMpPlan(ref emOrderDt)) == false) return;

            // 取込後、再読み込みして表示
            PopulateCalendar();

        }


    }
}
