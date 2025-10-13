using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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
        private string progressmsg;

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
                      + " <" + Common.FRM_ID_044 + ": " + Common.FRM_NAME_044 + ">";

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

            // 内示カード印刷ボタン
            Btn_PrintPlan.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_PrintPlan.Enabled = false;
            Btn_PrintClear.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_PrintClear.Enabled = false;
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
                orderDt.Columns.Add("内示カード出力日時", typeof(DateTime));
            }

            // MPデータの内容をEM注文データにマージ
            foreach (DataRow r in orderDt.Rows)
            {
                DataRow[] mpDr = mpOrderDt.Select($"WEEKEDDT='{r["WEEKEDDT"]}' and HMCD='{r["HMCD"]}'");
                if (mpDr.Length > 0)
                {
                    r["MP本数"] = mpDr[0]["MP本数"];
                    r["内示カード出力日時"] = mpDr[0]["内示カード出力日時"];
                }
                else
                {
                    r["MP本数"] = 0;
                    r["内示カード出力日時"] = DateTime.Parse("1900/01/01");
                }
            }
        }

        // カレンダーを作成
        private async void PopulateCalendar()
        {
            // カレンダー関連の初期化
            ClearCalendar();

            // データベースから注文情報を非同期並列処理で取得（EM_Oracle:D0440, MP_MySQL:KD8440)
            toolStripStatusLabel1.Text = "内示データ確認中...";
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
                Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_DIMGRAY;
                if (column == 0)
                {
                    // 日曜日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_HOLIDAY;
                }
                else if (column == 1)
                {
                    int countHMCD = orderDt.Select($"WEEKEDDT='{dayOfPrevMonth}'").Count();
                    int cardPrint = orderDt.Select($"WEEKEDDT='{dayOfPrevMonth}' and 内示カード出力日時>'2000/1/1'").Count();
                    DateTime dayOfFriday = dayOfPrevMonth.AddDays(4);
                    if (cardPrint > 50) // 50件以上印刷されている場合に印刷済みと判断（SW工程のみ印刷対象のため）
                    {
                        // 内示カード印刷済み
                        for (int i = 1; i <= 5; i++)
                            Dgv_Calendar[i, row].Style.BackColor = Common.FRM40_BG_COLOR_PRINTED;
                    }
                    else if (orderDt.Select($"WEEKEDDT='{dayOfPrevMonth}' and MP本数>0")
                        .GroupBy(grp => grp["WEEKEDDT"].ToString()).Count() != 0)
                    {
                        // 注文データ取込済
                        for (int i = 1; i <= 5; i++)
                            Dgv_Calendar[i, row].Style.BackColor = Common.FRM40_BG_COLOR_IMPORTED;
                    }
                    else if (orderDt.Select($"WEEKEDDT='{dayOfPrevMonth}' and MP本数=0 and EM本数>0")
                        .GroupBy(grp => grp["WEEKEDDT"].ToString()).Count() != 0)
                    {
                        // 注文あり取込データなし
                        for (int i = 1; i <= 5; i++)
                            Dgv_Calendar[i, row].Style.BackColor = Common.FRM40_BG_COLOR_ORDERED;
                    }
                    else if (dtS0820working.Select($"YMD >= '{dayOfPrevMonth.ToString()}' and YMD <= '{dayOfFriday.ToString()}'").Count() == 0)
                        {
                        // 注文なし
                        for (int i = 1; i <= 5; i++)
                            Dgv_Calendar[i, row].Style.BackColor = Common.FRM40_BG_COLOR_HOLIDAY;
                    }
                }
                else if (column == 6)
                {
                    // 土曜日
                    if (dtS0820working.Select($"YMD='{dayOfPrevMonth}'").Count() == 0)
                    {
                        // 本社非稼働日
                        Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_SATURDAY;
                    }
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
                Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                // 土曜日：青
                // 日曜日とEMお休み日：ピンク
                // 取込済：薄緑
                // 未取込：薄青
                // EMに変化（追加・取消）あり：赤
                if (column == 0)
                {
                    // 日曜日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_HOLIDAY;
                }
                // 週初めの月曜日にデータを集めたので週1回判定
                else if (column == 1)
                {
                    DateTime firstDayOfWeek = currentDate.AddDays(((int)currentDate.DayOfWeek - 1) * -1);
                    int a = orderDt.Select($"WEEKEDDT='{currentDate}' and MP本数=0 and EM本数>0")
                        .GroupBy(grp => grp["WEEKEDDT"].ToString()).Count();
                    int b = orderDt.Select($"WEEKEDDT='{currentDate}' and MP本数>0 and EM本数=MP本数")
                        .GroupBy(grp => grp["WEEKEDDT"].ToString()).Count();
                    int c = orderDt.Select($"WEEKEDDT='{currentDate}' and MP本数>0 and EM本数<>MP本数")
                        .GroupBy(grp => grp["WEEKEDDT"].ToString()).Count();
                    int countHMCD = orderDt.Select($"WEEKEDDT='{currentDate}'").Count();
                    int cardPrint = orderDt.Select($"WEEKEDDT='{currentDate}' and 内示カード出力日時>'2000/1/1'").Count();
                    DateTime dayOfFriday = currentDate.AddDays(4);
                    if (cardPrint > 50) // 50件以上印刷されている場合に印刷済みと判断（SW工程のみ印刷対象のため）
                    {
                        // 内示カード印刷済み
                        for (int i = 1; i <= 5; i++)
                        Dgv_Calendar[i, row].Style.BackColor = Common.FRM40_BG_COLOR_PRINTED;
                    }
                    else if (orderDt.Select($"WEEKEDDT='{currentDate}' and MP本数>0")
                        .GroupBy(grp => grp["WEEKEDDT"].ToString()).Count() != 0)
                    {
                        // 注文データ取込済
                        for (int i = 1; i <= 5; i++)
                            Dgv_Calendar[i, row].Style.BackColor = Common.FRM40_BG_COLOR_IMPORTED;
                    }
                    else if (orderDt.Select($"WEEKEDDT='{currentDate}' and MP本数=0 and EM本数>0")
                        .GroupBy(grp => grp["WEEKEDDT"].ToString()).Count() != 0)
                    {
                        // 注文あり取込データなし
                        for (int i = 1; i <= 5; i++)
                            Dgv_Calendar[i, row].Style.BackColor = Common.FRM40_BG_COLOR_ORDERED;
                    }
                    else if (dtS0820working.Select($"YMD >= '{currentDate.ToString()}' and YMD <= '{dayOfFriday.ToString()}'").Count() == 0)
                    {
                        // 注文なし
                        for (int i = 1; i <= 5; i++)
                            Dgv_Calendar[i, row].Style.BackColor = Common.FRM40_BG_COLOR_HOLIDAY;
                    }
                }
                else if (column == 6)
                {
                    // 土曜日
                    if (dtS0820working.Select($"YMD='{currentDate}'").Count() == 0)
                    {
                        Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_SATURDAY;
                    }
                    row++;
                }
            }

            // 翌月の余りカレンダーを埋める
            int dayOfNextMonth = 1;
            while (row < 6)
            {
                DateTime nextDate = new DateTime(targetMonth.AddMonths(1).Year, targetMonth.AddMonths(1).Month, dayOfNextMonth);
                int column = (int)nextDate.DayOfWeek;
                Dgv_Calendar[column, row].Value = dayOfNextMonth;
                Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_DIMGRAY;
                if (column == 0)
                {
                    // 日曜日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_HOLIDAY;
                }
                else if (column == 1)
                {
                    DateTime firstDayOfWeek = nextDate.AddDays(((int)nextDate.DayOfWeek - 1) * -1);
                    int countHMCD = orderDt.Select($"WEEKEDDT='{firstDayOfWeek}'").Count();
                    int cardPrint = orderDt.Select($"WEEKEDDT='{firstDayOfWeek}' and 内示カード出力日時>'2000/1/1'").Count();
                    DateTime dayOfFriday = firstDayOfWeek.AddDays(4);
                    if (cardPrint > 50) // 50件以上印刷されている場合に印刷済みと判断（SW工程のみ印刷対象のため）
                    {
                        // 内示カード印刷済み
                        for (int i = 1; i <= 5; i++)
                            Dgv_Calendar[i, row].Style.BackColor = Common.FRM40_BG_COLOR_PRINTED;
                    }
                    else if (orderDt.Select($"WEEKEDDT='{nextDate}' and MP本数>0")
                        .GroupBy(grp => grp["WEEKEDDT"].ToString()).Count() != 0)
                    {
                        // 注文データ取込済
                        for (int i = 1; i <= 5; i++)
                            Dgv_Calendar[i, row].Style.BackColor = Common.FRM40_BG_COLOR_IMPORTED;
                    }
                    else if (orderDt.Select($"WEEKEDDT='{firstDayOfWeek}' and MP本数=0 and EM本数>0")
                        .GroupBy(grp => grp["WEEKEDDT"].ToString()).Count() != 0)
                    {
                        // 注文あり取込データなし
                        for (int i = 1; i <= 5; i++)
                            Dgv_Calendar[i, row].Style.BackColor = Common.FRM40_BG_COLOR_ORDERED;
                    }
                    else if (dtS0820working.Select($"YMD >= '{firstDayOfWeek.ToString()}' and YMD <= '{dayOfFriday.ToString()}'").Count() == 0)
                    {
                        // 注文なし
                        for (int i = 1; i <= 5; i++)
                            Dgv_Calendar[i, row].Style.BackColor = Common.FRM40_BG_COLOR_HOLIDAY;
                    }
                }
                else if (column == 6)
                {
                    // 土曜日
                    if (dtS0820working.Select($"YMD='{nextDate}'").Count() == 0)
                    {
                        Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_SATURDAY;
                    }
                    row++;
                }
                dayOfNextMonth++;
            }

            // 集計値を土曜日欄に表示
            CalendarAddSummary(ref emPlanDt, ref mpPlanDt);

            // 対象月の稼働日数を算出（非同期処理データテーブルの為処理の後ろの方で実行）
            int days = (int)dtS0820working.Select(String.Format(
                "#{1}# <= [{0}] AND [{0}] < #{2}#",
                "YMD", targetMonth, targetMonth.AddMonths(1))).Count();
            CalendarLabel.Text = $"{targetMonth.Month}月 カレンダー ({days}日稼働)";
            if (targetMonth >= DateTime.Now.AddMonths(-5))
                PrevMonthButton.Enabled = true;
            if (targetMonth <= DateTime.Now.AddMonths(7))
                NextMonthButton.Enabled = true;
            toolStripStatusLabel1.Text = "";
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
                    if (col == 1)
                    {
                        if (emOrderDt.Select($"WEEKEDDT='{eddt}'").Length > 0)
                        {
                            var result = orderDt.Select($"WEEKEDDT='{eddt}'")
                                .GroupBy(g => g["WEEKEDDT"].ToString())
                                .Select(grp => new
                                {
                                    KEY = grp.Key,
                                    QTY = grp.Sum(r => r.Field<decimal>("EM本数"))
                                });
                            decimal total = result.Sum(r => r.QTY);
                            totalEMQty += (int)result.Sum(r => r.QTY);
                        }
                        if (mpOrderDt.Select($"WEEKEDDT='{eddt}'").Length > 0)
                        {
                            var result = orderDt.Select($"WEEKEDDT='{eddt}' and MP本数>0")
                                .GroupBy(g => g["WEEKEDDT"].ToString())
                                .Select(grp => new
                                {
                                    KEY = grp.Key,
                                    QTY = grp.Sum(r => r.Field<Int64>("MP本数"))
                                });
                            Int64 total = result.Sum(r => r.QTY);
                            totalMPQty += (int)result.Sum(r => r.QTY);
                        }
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
        }

        // 当月を表示
        private void CalendarLabel_Click(object sender, EventArgs e)
        {
            targetMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            PopulateCalendar();
        }

        // 翌月ボタン
        private void NextMonthButton_Click(object sender, EventArgs e)
        {
            targetMonth = targetMonth.AddMonths(1);
            PopulateCalendar();
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
        }

        // ステータスにDB状態（取込済、印刷済）を表示（複数選択可能）
        private void Dgv_Calendar_MouseUp(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            Btn_PrintPlan.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_PrintPlan.Enabled = false;
            Btn_PrintClear.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_PrintClear.Enabled = false;
            int emQty = 0;
            int mpQty = 0;
            int countHMCD = 0;
            int cardPrint = 0;
            foreach (DataGridViewRow r in Dgv_Calendar.SelectedRows)
            {
                var planDay = GetCurrentDateTime(r.Cells[1]);
                if (orderDt.Select($"WEEKEDDT='{planDay}'").Length > 0)
                {
                    //DataRow r = orderDt.Select($"EDDT='{planDay}'")[0];

                    var result = orderDt.Select($"WEEKEDDT='{planDay}'")
                        .GroupBy(g => g["WEEKEDDT"].ToString())
                        .Select(grp => new
                        {
                            KEY = grp.Key,
                            EMQTY = grp.Sum(s => s.Field<decimal>("EM本数")),
                            MPQTY = grp.Sum(s => s.Field<Int64>("MP本数"))
                        });
                    emQty += (int)result.Sum(res => res.EMQTY);
                    mpQty += (int)result.Sum(res => res.MPQTY);
                    countHMCD += orderDt.Select($"WEEKEDDT='{planDay}'").Count();
                    cardPrint += orderDt.Select($"WEEKEDDT='{planDay}' and 内示カード出力日時>'2000/1/1'").Count();
                }
            }
            // 内示カードがあまり印刷されていなかったら印刷ボタンを活性化
            if (countHMCD > 0 && (countHMCD / 2) >= cardPrint)
            {
                Btn_PrintPlan.BackColor = Common.FRM40_BG_COLOR_PRINTED;
                Btn_PrintPlan.Enabled = true;
            }
            if (cardPrint > 50) // 50件以上印刷されている場合に印刷済みと判断（SW工程のみ印刷対象のため）
            {
                Btn_PrintClear.BackColor = Common.FRM40_BG_COLOR_WARNING;
                Btn_PrintClear.Enabled = true;
            }
            if (emQty - mpQty == 0)
            {
                toolStripStatusLabel1.Text = $"内示 {emQty.ToString("#,0")}本 ／ ";
                toolStripStatusLabel1.Text +=
                    (countHMCD == cardPrint) ? $"印刷済み {cardPrint.ToString("#,0")}件" : 
                    (cardPrint == 0) ? $"品番点数 {countHMCD.ToString("#,0")}件" :
                    $"未印刷 {countHMCD - cardPrint}件";
            }
            else
            {
                toolStripStatusLabel1.Text = $"内示 EM{emQty.ToString("#,0")}本 / MP{mpQty.ToString("#,0")}本";
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
            if (MessageBox.Show("切削システム内の内示データを再作成します\n" + 
                "よろしいですか？", "内示データ再作成", MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question) == DialogResult.No) return;

            toolStripStatusLabel1.Text = "内示データ取込中...";

            // データベースから注文情報を非同期並列処理で取得（Oracle:D0410, MySQL:KD8430)
            DataTable emOrderDt = new DataTable();

            // EMから手配日程ファイルを読み込む
            if (await Task.Run(() => cmn.Dba.GetEmPlan(ref emOrderDt)) == false) return;

            // MP切削手配日程ファイルを全削除して、EM手配日程ファイルをそのまま取り込む
            // トランザクション処理の為、Delete命令で削除しImport成功後Commit
            if (await Task.Run(() => cmn.Dba.ImportMpPlan(ref emOrderDt)) == false) return;

            // 取込後、再読み込みして表示
            PopulateCalendar();

        }


        // おまけ関数
        // 引数で渡された日付が、その月の第何週なのかを数値で返却する関数
        private int GetWeekNum(DateTime dt)
        {
            int weekNum = 0;
            DateTime firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            weekNum = cal.GetWeekOfYear(dt, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            weekNum -= cal.GetWeekOfYear(firstDayOfMonth, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) - 1;
            return weekNum;
        }


        // 内示カード印刷
        private async void Btn_PrintPlan_Click(object sender, EventArgs e)
        {
            // モニターサイズの倍率チェック
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

            // 印刷前の最終確認
            if (MessageBox.Show("内示カードを印刷します。\nよろしいですか？", "確認"
                , MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Cancel) return;

            // 印刷ボタン非活性化
            Btn_PrintPlan.BackColor = SystemColors.Control;
            Btn_PrintPlan.Enabled = false;
            Btn_PrintClear.BackColor = SystemColors.Control;
            Btn_PrintClear.Enabled = false;

            // ステータス表示
            toolStripStatusLabel1.Text = "内示カード印刷中...";

            // Excelアプリケーションを起動
            cmn.Fa.OpenExcel2(idx);

            // 選択セルの並び替え
            var query = from DataGridViewRow r in Dgv_Calendar.SelectedRows
                        orderby r.Index
                        select r;
            int ret = 0;
            foreach (DataGridViewRow r in query)
            {
                // 対象日の製造指示カードを作成し印刷
                DateTime cardDay = GetCurrentDateTime(r.Cells[1]); // 1:月曜日

                progressmsg = $"【{cardDay.ToString("M月")}" +
                    $"{GetWeekNum(cardDay)}週】内示カード ";

                // 雛形カードを開く（拡縮倍率にあった帳票を選択）
                cmn.Fa.OpenExcelFile2($@"{cmn.FsCd[idx].RootPath}\{cmn.FsCd[idx].FileName}");

                // 内示データをDataTableに読み込む（KD8470に存在しないデータが対象）
                toolStripStatusLabel1.Text = progressmsg + "データ読み込み中...";
                DataTable cardDt = new DataTable();
                await Task.Run(() => cmn.Dba.GetPlanCardPrintInfo(cardDay, ref cardDt));
                if (ret < 0) break;

                // 内示カード雛形に内示データをセット
                // 設定ファイルの場所にPDFとして保存して起動
                toolStripStatusLabel1.Text = progressmsg + $"{cardDt.Rows.Count}件中 - 1枚 作成中...";
                await Task.Run(() => ret = PrintPlanCard(cardDay, ref cardDt));
                if (ret != 0) break;

                // ExcelブックからPDFを作成（ファイル名は内示カードを使用）
                var pdfName = cmn.FsCd[idx + 2].FileName
                    .Replace("雛形", "_" + cardDay.ToString("yyyyMMdd")
                                   + "_" + DateTime.Now.ToString("yyyyMMddhhmm"))
                    .Replace(".xlsx", ".pdf");
                cmn.Fa.ExportExcelToPDF($@"{cmn.FsCd[0].RootPath}\{pdfName}"); // 0:生産計画システム出力先フォルダ

                // 内示カード雛形を閉じる
                cmn.Fa.CloseExcelFile2(false);

                // KD8470:切削内示カードファイルに印刷済みを出力
                cmn.Dba.InsertPlanCard(cardDay);
            }

            // Excelアプリケーションを閉じる
            cmn.Fa.CloseExcel2();

            // 取込後、再読み込みして表示
            PopulateCalendar();
        }

        /// <summary>
        /// 内示カード作成
        /// </summary>
        /// <param name="cardDay">完了予定日</param>
        /// 
        /// <returns>結果 (0: 保存成功 (保存件数), -1: 保存失敗, -2: 認証失敗)</returns>
        public int PrintPlanCardBackup(DateTime cardDay, ref System.Data.DataTable cardDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;

            try
            {
                // Excelシートの作成
                int baseRow = 1;
                int cardCnt = 1;    // ※手配件数とカード枚数は異なる（ロットで分割するため）
                int cardRows = 11;  // 1カードの行数（余白含む）
                int row = 0;
                int col = 0;

                // 材料毎の使用長さを算出し材料データテーブルを先に作成
                DataTable materialDt = new DataTable();
                CalculateMaterial(ref cardDt, ref materialDt);

                cmn.Fa.CreateTemplatePlanCard(); // テンプレートオブジェクトの作成（内示カードの雛形を作成）

                for (int i = 0; i < cardDt.Rows.Count; i++)
                {
                    DataRow r = cardDt.Rows[i];

                    // 書き込む先頭セル行番を計算
                    col = (cardCnt % 2 != 0) ? 1 : 10;
                    row = cardRows * (Convert.ToInt32(Math.Ceiling(cardCnt / 2d)) - 1) + baseRow;

                    try
                    {
                        // １カード分をExcelオブジェクトにセット（新規ページの１件目の場合はコピペ処理含む）
                        cmn.Fa.SetPlanCardBackup(ref cardDay, ref r, ref row, ref col, ref materialDt);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message + "(" + i + "行目)");
                        Debug.WriteLine(r.ToString());
                        throw ex;
                    }
                    if (cardCnt % 5 == 0)
                    {
                        toolStripStatusLabel1.Text = progressmsg + $" - {cardCnt}件 / {cardDt.Rows.Count}件中 作成中...";
                    }
                    cardCnt++;
                }
                // 最終頁の余り件数をチェック
                // 10の倍数でなかった場合、コピペした余分なデータをクリア
                // ※COMアクセスを減らす為、ループ中でのクリア処理を廃止
                if ((cardCnt - 1) % 10 != 0)
                    cmn.Fa.ClearZanPlanCard(cardCnt - 1);
                toolStripStatusLabel1.Text = progressmsg + $" {cardDt.Rows.Count}件のカードが作成されました.";
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

        /// <summary>
        /// 内示カード作成
        /// </summary>
        /// <param name="cardDay">完了予定日</param>
        /// 
        /// <returns>結果 (0: 保存成功 (保存件数), -1: 保存失敗, -2: 認証失敗)</returns>
        public int PrintPlanCard(DateTime cardDay, ref System.Data.DataTable cardDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            int i = 0;

            try
            {
                // Excelシートの作成
                int baseRow = 1;
                int cardCnt = 1;    // ※手配件数とカード枚数は異なる（ロットで分割するため）
                int cardRows = 21;  // 1カードの行数（余白含む）
                int row = 0;
                int col = 0;
                cmn.Fa.CreateTemplateOrderCard(); // テンプレートオブジェクトの作成（雛形カードを作成）
                for (i = 0; i < cardDt.Rows.Count; i++)
                {
                    DataRow r = cardDt.Rows[i];

                    // 収容数で分割
                    decimal odrqty = Decimal.Parse(r["ODRQTY"].ToString());
                    decimal boxqty = 0;
                    int loopCnt = 1;
                    if (Decimal.TryParse(r["BOXQTY"].ToString(), out boxqty)) {
                        loopCnt = Decimal.ToInt32(Math.Ceiling((odrqty / boxqty)));
                    }

                    // 収容数でループ
                    for (int j = 1; j <= loopCnt; j++)
                    {
                        // 書き込む先頭セル行番を計算
                        row = cardRows * (Convert.ToInt32(Math.Ceiling(cardCnt / 2d)) - 1) + baseRow;

                        // 左右の列番号を切り替え
                        col = (cardCnt % 2 != 0) ? 1 : 10;

                        // １カード分をExcelオブジェクトにセット（新規ページの１件目の場合はコピペ処理含む）
                        cmn.Fa.SetPlanCard(ref r, ref row, ref col, j, loopCnt);

                        if (cardCnt % 5 == 0)
                        {
                            toolStripStatusLabel1.Text = progressmsg + $"{cardDt.Rows.Count}件中 - {cardCnt}枚 作成中...";
                        }
                        cardCnt++;
                    }

                }
                // ループ終了時に最後のページの印刷枚数が４の倍数でなかった場合、
                // 残りの余分なデータをクリア（COMアクセスを減らす為にクリア処理は最後の一回だけ行う）
                cardCnt--;
                if ((cardCnt) % 4 != 0)
                    cmn.Fa.ClearZanOrderCard(cardCnt);
                toolStripStatusLabel1.Text = progressmsg + $"{cardDt.Rows.Count}件 - {cardCnt}枚のカードが作成されました.";
            }
            // ファイルの保存に失敗すると Exception が発生する
            catch (Exception e)
            {
                Debug.WriteLine("Exception Source = " + e.Source);
                Debug.WriteLine("Exception Message = " + e.Message);
                MessageBox.Show("計画No [" + cardDt.Rows[i]["PLNNO"].ToString() + "] で異常が発生しました．");
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

        // 印字する週に使用する材料種類毎の使用する合計長さを各明細を印字する前に算出
        private void CalculateMaterial(ref System.Data.DataTable cardDt, ref System.Data.DataTable materialDt)
        {
            // 材料種類ごとの必要長さを算出
            var result = cardDt
                .Select($"MATESIZE<>'' and LENGTH>0")
                .GroupBy(g => g["MATESIZE"].ToString())
                .Select(grp => new
                {
                    KEY = grp.Key,
                    NECESSARYLEN = grp.Sum(s =>
                        (
                            Convert.ToDecimal(s["LENGTH"].ToString()) + 
                            Convert.ToDecimal(s["CUTTHICKNESS"].ToString())
                        ) * Convert.ToInt32(s["ODRQTY"].ToString())
                    )
                }); 

            // 材料データテーブルに列を追加
            if (materialDt.Columns.Count == 0)
            {
                materialDt.Columns.Add("MATESIZE", typeof(string));
                materialDt.Columns.Add("NECESSARYLEN", typeof(decimal));
            }
            // 材料データテーブルに算出したキーと値を設定
            foreach (var item in result)
            {
                DataRow r = materialDt.NewRow();
                r["MATESIZE"] = item.KEY;
                r["NECESSARYLEN"] = item.NECESSARYLEN;
                materialDt.Rows.Add(r);
            }
        }

        // キーボードショートカット
        private void Frm044_ImportPlan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        // カード廃棄
        private void Btn_PrintClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("印刷済み日付をクリアします。よろしいですか？", "最終質問",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Cancel) return;

            // 選択セルの並び替え
            var query = from DataGridViewRow r in Dgv_Calendar.SelectedRows
                        orderby r.Index
                        select r;
            foreach (DataGridViewRow r in query)
            {
                // 対象日の製造指示カードを作成し印刷
                DateTime cardDay = GetCurrentDateTime(r.Cells[1]); // 1:月曜日

                progressmsg = $"【{cardDay.ToString("M月")}" +
                    $"{GetWeekNum(cardDay)}週】内示カード";

                // 切削内示カードファイル:KD8470を削除
                toolStripStatusLabel1.Text = progressmsg + $"を削除中...";
                // KD8470:切削内示カードファイルに印刷済みを出力
                if (cmn.Dba.DeletePlanCard(cardDay) == false)
                {
                    MessageBox.Show("削除に失敗しました.", "カード廃棄", MessageBoxButtons.OK , MessageBoxIcon.Error);
                    return;
                }
                // 取込後、再読み込みして表示
                PopulateCalendar();
            }
        }
    }
}
