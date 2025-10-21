using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm041_ImportOrder : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DateTime targetMonth;                   // 処理対象月を設定
        private DataTable orderDt = new DataTable();    // 注文状況を保持（カレンダー詳細情報で使用）
        private DataTable dtS0820working;               // カレンダーテーブルを保持
        private string progressmsg;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="common">共通クラス</param>
        public Frm041_ImportOrder(Common cmn)
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
            // 初期化
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel2.Text = "";

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
            Frm041_ImportOrder_Resize(this, EventArgs.Empty);
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

            // 注文データ取り込みボタン
            Btn_ImportOrder.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_ImportOrder.Enabled = false;

            // 製造指示カード印刷ボタン
            Btn_PrintOrder.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_PrintOrder.Enabled = false;

            // 製造指示カード印刷不要ボタン
            Btn_PrintCancel.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_PrintCancel.Enabled = false;
        }

        // EMデータテーブルとMPデータテーブルをマージ
        private void MargeOrderDataTable(ref DataTable emOrderDt, ref DataTable mpOrderDt)
        {
            orderDt.Clear();
            orderDt = emOrderDt.Copy();

            // EM注文データテーブルに列を追加
            if (orderDt.Columns.Count == 11)
            {
                orderDt.Columns.Add("MP2確定件数", typeof(long));
                orderDt.Columns.Add("MP3着手件数", typeof(long));
                orderDt.Columns.Add("MP4完了件数", typeof(long));
                orderDt.Columns.Add("MP9取消件数", typeof(long));
                orderDt.Columns.Add("MP取込件数", typeof(long));

                orderDt.Columns.Add("MP2確定本数", typeof(long));
                orderDt.Columns.Add("MP3着手本数", typeof(long));
                orderDt.Columns.Add("MP4完了本数", typeof(long));
                orderDt.Columns.Add("MP9取消本数", typeof(long));
                orderDt.Columns.Add("MP取込本数", typeof(long));

                orderDt.Columns.Add("MP印刷対象", typeof(long));
                orderDt.Columns.Add("MP印刷件数", typeof(long));
            }

            // MPデータの内容をEM注文データにマージ
            foreach (DataRow r in orderDt.Rows)
            {
                if (mpOrderDt.Select($"EDDT='{r["EDDT"]}'").Length > 0)
                {
                    r["MP2確定件数"] = mpOrderDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP2確定件数"];
                    r["MP3着手件数"] = mpOrderDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP3着手件数"];
                    r["MP4完了件数"] = mpOrderDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP4完了件数"];
                    r["MP9取消件数"] = mpOrderDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP9取消件数"];
                    r["MP取込件数"] = mpOrderDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP取込件数"];

                    r["MP2確定本数"] = mpOrderDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP2確定本数"];
                    r["MP3着手本数"] = mpOrderDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP3着手本数"];
                    r["MP4完了本数"] = mpOrderDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP4完了本数"];
                    r["MP9取消本数"] = mpOrderDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP9取消本数"];
                    r["MP取込本数"] = mpOrderDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP取込本数"];

                    r["MP印刷件数"] = mpOrderDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP印刷件数"];
                    r["MP印刷対象"] = mpOrderDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP印刷対象"];
                }
                else
                {
                    r["MP2確定件数"] = 0;
                    r["MP3着手件数"] = 0;
                    r["MP4完了件数"] = 0;
                    r["MP9取消件数"] = 0;
                    r["MP取込件数"] = 0;

                    r["MP2確定本数"] = 0;
                    r["MP3着手本数"] = 0;
                    r["MP4完了本数"] = 0;
                    r["MP9取消本数"] = 0;
                    r["MP取込本数"] = 0;

                    r["MP印刷件数"] = 0;
                    r["MP印刷対象"] = 0;
                }
            }
        }

        // カレンダーを作成
        private async void PopulateCalendar()
        {
            // カレンダー関連の初期化
            ClearCalendar();

            // データベースから注文情報を非同期並列処理で取得（EM_Oracle:D0410, MP_MySQL:KD8430)
            toolStripStatusLabel1.Text = "注文データ確認中...";
            DataTable emOrderDt = new DataTable();
            DataTable mpOrderDt = new DataTable();
            var taskEM = Task.Run(() => cmn.Dba.GetEmOrderSummaryInfo(ref emOrderDt, targetMonth));
            var taskMP = Task.Run(() => cmn.Dba.GetMpOrderSummaryInfo(ref mpOrderDt, targetMonth));
            // 両者が完了するまで待機する
            await Task.WhenAll(taskEM, taskMP);

            // EMデータテーブルとMPデータテーブルをマージ
            MargeOrderDataTable(ref emOrderDt, ref mpOrderDt);

            int row = 0; // カレンダー１行目

            // 前月があれば作成
            DateTime dayOfPrevMonth = targetMonth.AddDays(-1);
            for (int week = (int)dayOfPrevMonth.DayOfWeek; week >= 0; week--)
            {
                int column = (int)dayOfPrevMonth.DayOfWeek;
                Dgv_Calendar[column, row].Value = dayOfPrevMonth.Day;
                if (orderDt.Select($"EDDT='{dayOfPrevMonth}' and EM合計件数=MP取込件数").Count() != 0)
                {
                    // 注文データ取込済
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_IMPORTED;
                    Dgv_Calendar[column, row].Style.ForeColor =  Common.FRM40_COLOR_DIMGRAY;
                }
                else if (dtS0820working.Select($"YMD='{dayOfPrevMonth}'").Count() == 0)
                {
                    // 本社非稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_HOLIDAY;
                    Dgv_Calendar[column, row].Style.ForeColor =  Common.FRM40_COLOR_DIMGRAY;
                }
                else
                {
                    // 本社稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_OTHERMONTH;
                    Dgv_Calendar[column, row].Style.ForeColor =  Common.FRM40_COLOR_DIMGRAY;
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
                else if (orderDt.Select($"EDDT='{currentDate}' and MP取込件数=0 and EM合計件数>0").Count() != 0)
                {
                    // 注文あり取込データなし
                    var emCount = Int32.Parse(orderDt.Select($"EDDT='{currentDate}'")[0]["EM合計件数"].ToString());
                    var mpCount = Int32.Parse(orderDt.Select($"EDDT='{currentDate}'")[0]["MP取込件数"].ToString());
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_ORDERED ;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                }
                else if (orderDt.Select($"EDDT='{currentDate}' and MP取込件数>0 and MP印刷件数>0 and MP印刷対象<>MP印刷件数").Count() != 0)
                {
                    // 未印刷データあり
                    var printTarget = Int32.Parse(orderDt.Select($"EDDT='{currentDate}'")[0]["MP印刷対象"].ToString());
                    var printCount = Int32.Parse(orderDt.Select($"EDDT='{currentDate}'")[0]["MP印刷件数"].ToString());
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_WARNING;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                    Dgv_Calendar[column, row].Value = day + $"\n未印刷が\n{printTarget - printCount}件あります";
                }
                else if (orderDt.Select($"EDDT='{currentDate}' and MP取込件数>0 and EM合計件数<>MP取込件数").Count() != 0)
                {
                    // 注文データ取込後にEM側に追加注文あり
                    var emCount = Int32.Parse(orderDt.Select($"EDDT='{currentDate}'")[0]["EM合計件数"].ToString());
                    var mpCount = Int32.Parse(orderDt.Select($"EDDT='{currentDate}'")[0]["MP取込件数"].ToString());
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_WARNING;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                    Dgv_Calendar[column, row].Value = day + $"\n未取込が\n{emCount - mpCount}件あります";
                }
                else if (orderDt.Select($"EDDT='{currentDate}' and EM9取消件数>0 and EM9取消件数<>MP9取消件数").Count() != 0)
                {
                    // 注文データ取込後にEM側で注文取消あり
                    var emCancel = Int32.Parse(orderDt.Select($"EDDT='{currentDate}'")[0]["EM9取消件数"].ToString());
                    var mpCancel = Int32.Parse(orderDt.Select($"EDDT='{currentDate}'")[0]["MP9取消件数"].ToString());
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_WARNING;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                    Dgv_Calendar[column, row].Value = day + $"\nEM取消が\n{emCancel - mpCancel}件あります";
                }
                else if (orderDt.Select($"EDDT='{currentDate}' and MP取込件数>0 and MP印刷対象=MP印刷件数").Count() != 0)
                {
                    // 製造指示カード印刷済み
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_PRINTED;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                }
                else if (orderDt.Select($"EDDT='{currentDate}' and EM合計件数=MP取込件数").Count() != 0)
                {
                    // 注文データ取込済
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_IMPORTED;
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
                    Dgv_Calendar[column, row].Style.ForeColor =  Common.FRM40_COLOR_DIMGRAY;
                }
                else if (column == 6)
                {
                    // 土曜日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_SATURDAY;
                    Dgv_Calendar[column, row].Style.ForeColor =  Common.FRM40_COLOR_DIMGRAY;
                    row++;
                }
                else if (orderDt.Select($"EDDT='{nextDate}' and MP取込件数=0 and EM合計件数>0").Count() != 0)
                {
                    // 注文あり取込データなし
                    var emCount = Int32.Parse(orderDt.Select($"EDDT='{nextDate}'")[0]["EM合計件数"].ToString());
                    var mpCount = Int32.Parse(orderDt.Select($"EDDT='{nextDate}'")[0]["MP取込件数"].ToString());
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_ORDERED ;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                }
                else if (orderDt.Select($"EDDT='{nextDate}' and MP取込件数>0 and EM合計件数<>MP取込件数").Count() != 0)
                {
                    // 注文データ取込後にEM側に追加注文あり
                    var emCount = Int32.Parse(orderDt.Select($"EDDT='{nextDate}'")[0]["EM合計件数"].ToString());
                    var mpCount = Int32.Parse(orderDt.Select($"EDDT='{nextDate}'")[0]["MP取込件数"].ToString());
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_WARNING;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                    Dgv_Calendar[column, row].Value = dayOfNextMonth + $"\n未取込が\n{emCount - mpCount}件あります";
                }
                else if (orderDt.Select($"EDDT='{nextDate}' and EM9取消件数>0 and EM9取消件数<>MP9取消件数").Count() != 0)
                {
                    // 注文データ取込後にEM側で注文取消あり
                    var emCancel = Int32.Parse(orderDt.Select($"EDDT='{nextDate}'")[0]["EM9取消件数"].ToString());
                    var mpCancel = Int32.Parse(orderDt.Select($"EDDT='{nextDate}'")[0]["MP9取消件数"].ToString());
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_WARNING;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                    Dgv_Calendar[column, row].Value = dayOfNextMonth + $"\nEM取消が\n{emCancel - mpCancel}件あります";
                }
                else if (orderDt.Select($"EDDT='{nextDate}' and MP取込件数>0 and MP印刷件数>MP印刷対象").Count() != 0)
                {
                    // 製造指示カード印刷済み
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_PRINTED;
                    Dgv_Calendar[column, row].Style.ForeColor = Common.FRM40_COLOR_BLACK;
                }
                else if (orderDt.Select($"EDDT='{nextDate}' and EM合計件数=MP取込件数").Count() != 0)
                {
                    // 注文データ取込済
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_IMPORTED;
                    Dgv_Calendar[column, row].Style.ForeColor =  Common.FRM40_COLOR_DIMGRAY;
                }
                else if (dtS0820working.Select($"YMD='{nextDate}'").Count() == 0)
                {
                    // 本社非稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_HOLIDAY;
                    Dgv_Calendar[column, row].Style.ForeColor =  Common.FRM40_COLOR_DIMGRAY;
                }
                else
                {
                    // 本社稼働日
                    Dgv_Calendar[column, row].Style.BackColor = Common.FRM40_BG_COLOR_OTHERMONTH;
                    Dgv_Calendar[column, row].Style.ForeColor =  Common.FRM40_COLOR_DIMGRAY;
                }
                dayOfNextMonth++;
            }

            // 集計値を土曜日欄に表示
            CalendarAddSummary(ref emOrderDt);

            // 対象月の稼働日数を算出（非同期処理データテーブルの為処理の後ろの方で実行）
            int days = (int)dtS0820working.Select(String.Format(
                "#{1}# <= [{0}] AND [{0}] < #{2}#",
                "YMD", targetMonth, targetMonth.AddMonths(1))).Count();
            CalendarLabel.Text = $"{targetMonth.Month}月 カレンダー ({days}日稼働)";
            PrevMonthButton.Enabled = true;
            NextMonthButton.Enabled = true;
            toolStripStatusLabel1.Text = "日付をクリックして注文データを取り込んでください．";
            toolStripStatusLabel2.Text = "";
        }

        // DGV上の月曜日から金曜日までの件数をサマリーして土曜日にぶち込む
        private void CalendarAddSummary(ref DataTable emOrderDt)
        {
            for (int row = 0; row < 6; row++)
            {
                int totalEMQty = 0;
                for (int col = 0; col < 7; col++)
                {
                    // 当日のカレンダーのフォント色を変更
                    var eddt = GetCurrentDateTime(Dgv_Calendar[col, row]);
                    if (eddt == DateTime.Now.Date)
                    {
                        Dgv_Calendar[col, row].Style.ForeColor = Color.Red;
                    }
                    if (emOrderDt.Select($"EDDT='{eddt}'").Length > 0)
                    {
                        totalEMQty += 
                            int.Parse(emOrderDt.Select($"EDDT='{eddt}'")[0]["EM合計本数"].ToString()) - 
                            int.Parse(emOrderDt.Select($"EDDT='{eddt}'")[0]["EM9取消本数"].ToString());
                    }
                }
                if (totalEMQty > 0) 
                    Dgv_Calendar[6, row].Value += $"\n {totalEMQty.ToString("#,0")}本";
            }
        }
        
        // 前月ボタン
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

        // Escapeでフォーム終了
        private void Frm041_ImportOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        // フォームサイズの可変に連動
        private void Frm041_ImportOrder_Resize(object sender, EventArgs e)
        {
            float w = ClientSize.Height; //縦幅
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
                Dgv_Calendar.Rows[i].Height = (Dgv_Calendar.Height - Dgv_Calendar.ColumnHeadersHeight) / 6 - 6;
            for (var i = 0; i < 7; i++)
                Dgv_Calendar.Columns[i].Width = (Dgv_Calendar.Width / 7);

        }

        // カレンダーセル複数選択で 各種件数の表示と各種ボタンの活性非活性化
        private void Dgv_Calendar_MouseUp(object sender, MouseEventArgs e)
        {
            Btn_ImportOrder.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_ImportOrder.Enabled = false;
            Btn_PrintOrder.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_PrintOrder.Enabled = false;
            Btn_PrintCancel.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_PrintCancel.Enabled = false;
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel2.Text = "";
            int emOrder = 0;
            int mpOrder = 0;
            int emCancel = 0;
            int mpCancel = 0;
            int em2qty = 0;
            int em3qty = 0;
            int em4qty = 0;
            int em9qty = 0;
            int mp2qty = 0;
            int mp3qty = 0;
            int mp4qty = 0;
            int mp9qty = 0;
            int printTarget = 0;
            int printCnt = 0;
            foreach (DataGridViewCell c in Dgv_Calendar.SelectedCells)
            {
                var planDay = GetCurrentDateTime(c);
                if (orderDt.Select($"EDDT='{planDay}'").Length > 0)
                {
                    DataRow r = orderDt.Select($"EDDT='{planDay}'")[0];
                    emOrder += Int32.Parse(r["EM合計件数"].ToString());
                    mpOrder += Int32.Parse(r["MP取込件数"].ToString());
                    emCancel += Int32.Parse(r["EM9取消件数"].ToString());
                    mpCancel += Int32.Parse(r["MP9取消件数"].ToString());

                    em2qty += Int32.Parse(r["EM2確定本数"].ToString());
                    em3qty += Int32.Parse(r["EM3着手本数"].ToString());
                    em4qty += Int32.Parse(r["EM4完了本数"].ToString());
                    em9qty += Int32.Parse(r["EM9取消本数"].ToString());

                    mp2qty += Int32.Parse(r["MP2確定本数"].ToString());
                    mp3qty += Int32.Parse(r["MP3着手本数"].ToString());
                    mp4qty += Int32.Parse(r["MP4完了本数"].ToString());
                    mp9qty += Int32.Parse(r["MP9取消本数"].ToString());

                    printTarget += Int32.Parse(r["MP印刷対象"].ToString());
                    printCnt += Int32.Parse(r["MP印刷件数"].ToString());
                }
            }
            if (emOrder > 0)
            {
                // 注文データ取り込みボタン活性化
                if (emOrder != mpOrder)
                {
                    Btn_ImportOrder.BackColor = Common.FRM40_BG_COLOR_IMPORTED;
                    Btn_ImportOrder.Enabled = true;
                    toolStripStatusLabel1.Text += $" 未取込: {emOrder - mpOrder}件";
                }
                if (emOrder == mpOrder)
                {
                    toolStripStatusLabel1.Text += $" 取込済: {mpOrder}件";
                    if (mpCancel > 0) toolStripStatusLabel1.Text += $" (取消{mpCancel}件 )";

                }
                // 注文データ印刷ボタン活性化
                if (mpOrder > 0 && printTarget - printCnt > 0)
                {
                    Btn_PrintOrder.BackColor = Common.FRM40_BG_COLOR_PRINTED;
                    Btn_PrintOrder.Enabled = true;
                    Btn_PrintCancel.BackColor = Common.FRM40_BG_COLOR_PRINTED;
                    Btn_PrintCancel.Enabled = true;
                    toolStripStatusLabel1.Text += $" 未印刷: {printTarget - printCnt}件";
                }
                if (mpOrder > 0 && printCnt > 0 && printTarget - printCnt == 0)
                {
                    toolStripStatusLabel1.Text += $" 印刷済: {printCnt}件";
                }
                // EM取消情報との差異を注意喚起
                if (emCancel > 0 && mpOrder > 0 && emCancel != mpCancel)
                {
                    toolStripStatusLabel1.Text += $" ( EM取消: {emCancel}件, MP取消{mpCancel}件 )";
                }
                // 手配件数をステータスストラップ２に表示
                toolStripStatusLabel2.Text = "[ ";
                toolStripStatusLabel2.Text += (em2qty > 0) ? $"EM確定{em2qty.ToString("#,0")}本／" : "";
                toolStripStatusLabel2.Text += (em3qty > 0) ? $"着手{em3qty.ToString("#,0")}本／" : "";
                toolStripStatusLabel2.Text += $"完了{em4qty.ToString("#,0")}本";
                toolStripStatusLabel2.Text += (em9qty > 0) ? $"／取消{em9qty.ToString("#,0")}本" : "";
                toolStripStatusLabel2.Text += " ]";
            }
        }

        // カレンダーセル ダブルクリックで手配検索画面に遷移
        private void Dgv_Calendar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DateTime dateValue = GetCurrentDateTime(Dgv_Calendar[e.ColumnIndex, e.RowIndex]);
            Frm042_InformationOrder frm042 = new Frm042_InformationOrder(cmn, dateValue);
            Thread.Sleep(500);
            frm042.Show();
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

        // 注文データ取込
        private async void Btn_ImportOrder_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "注文データ取込中...";
            int insCount = 0;
            int delCount = 0;
            // マスタ類データテーブル定義
            DataTable mpCodeMDt = new DataTable();      // コード票マスタ
            DataTable mpZaikoDt = new DataTable();      // 仕掛かり在庫テーブル（SWのみ）
            DataTable mpNaijiDt = new DataTable();      // 内示カードファイル
            DataTable mpNaijiReportDt = new DataTable();// レポート提出用
            DataTable deleteODRNODt = new DataTable();  // 削除したODRNOを控えておくための変数
            deleteODRNODt.Columns.Add("ODRNO", typeof(string));
            // 全てのマスタ類の読み込み
            Task taskC = Task.Run(() => cmn.Dba.GetCodeSlipMst(ref mpCodeMDt));
            Task taskD = Task.Run(() => cmn.Dba.GetMpZaiko(ref mpZaikoDt, "'SW'"));
            await Task.WhenAll(taskC, taskD);
            // 引当前在庫を保持
            DataTable mpZaikoReportDt = mpZaikoDt.Copy();
            // 選択セルの並び替え
            var query = from DataGridViewCell c in Dgv_Calendar.SelectedCells
                        where c.Style.BackColor == Common.FRM40_BG_COLOR_ORDERED || c.Style.BackColor == Common.FRM40_BG_COLOR_WARNING
                        orderby c.RowIndex, c.ColumnIndex
                        select c;
            foreach (DataGridViewCell c in query) // query ← Dgv_Calendar.SelectedCells)
            {
                // 背景色が薄青、薄赤を対象に条件分を作成
                if (c.Style.BackColor == Common.FRM40_BG_COLOR_ORDERED || c.Style.BackColor == Common.FRM40_BG_COLOR_WARNING)
                {
                    DateTime eddt = GetCurrentDateTime(c);

                    // データベースから手配日の情報を非同期並列処理で取得（Oracle:D0410, MySQL:KD8430)
                    DataTable emOrderDt = new DataTable();
                    DataTable mpOrderDt = new DataTable();
                    var taskA = Task.Run(() => cmn.Dba.GetEmOrder(ref emOrderDt, eddt.ToString("yyyy/MM/dd")));
                    var taskB = Task.Run(() => cmn.Dba.GetMpOrder(ref mpOrderDt, eddt.ToString("yyyy/MM/dd")));

                    // 全ての読み込みが完了するまで待機する
                    await Task.WhenAll(taskA, taskB);

                    // 内示カードファイル処理（初回のみ取得）
                    if (c.ColumnIndex == 0 && mpNaijiDt.Rows.Count > 0) mpNaijiReportDt.Merge(mpNaijiDt);
                    if (c.ColumnIndex == 0) mpNaijiDt.Rows.Clear(); // 2行に渡って選択された場合の初期化処理
                    if (mpNaijiDt.Rows.Count == 0)
                    {
                        // 今週の月曜日を計算
                        int daysSinceMonday = (int)eddt.DayOfWeek - (int)DayOfWeek.Monday;
                        if (daysSinceMonday < 0) daysSinceMonday += 7; // 日曜日の場合の調整
                        DateTime thisMonday = eddt.AddDays(-daysSinceMonday);
                        await Task.Run(() => cmn.Dba.GetMpNaiji(ref mpNaijiDt, thisMonday.ToString("yyyy/MM/dd")));
                    }

                    // 削除対象が存在するかチェック（二つのODRNOの集合差を求める）
                    DataRow[] deleteDr = mpOrderDt.AsEnumerable()
                        .Where(row =>
                            !emOrderDt.AsEnumerable().Select(col => col["ODRNO"]).ToArray()
                            .Contains(row["ODRNO"]))
                        .ToArray();
                    if (deleteDr.Length > 0)
                    {
                        DataTable exceptDt = new DataTable();
                        exceptDt = deleteDr.CopyToDataTable();
                        // MPシステム MySQLに集合差分を削除
                        delCount = cmn.Dba.DeleteMpOrder(ref exceptDt);
                        if (delCount < 0) return;
                        // 削除したODRNOを控えておく
                        deleteODRNODt.Merge(exceptDt);
                    }

                    // 追加対象が存在するかチェック（二つのODRNOの集合差を求める）
                    DataRow[] insertDr = emOrderDt.AsEnumerable()
                        .Where(row =>
                            !mpOrderDt.AsEnumerable().Select(col => col["ODRNO"]).ToArray()
                            .Contains(row["ODRNO"]))
                        .ToArray();
                    if (insertDr.Length > 0)
                    {
                        DataTable exceptDt = new DataTable();
                        exceptDt = insertDr.CopyToDataTable();

                        // 手配品番がコード票に存在するかチェック
                        string[] errHMCD = exceptDt.AsEnumerable()
                            .Where(row =>
                                !mpCodeMDt.AsEnumerable().Select(col => col["HMCD"]).ToArray()
                                .Contains(row["HMCD"])
                            ).Select(col => col["HMCD"].ToString()).ToArray();
                        if (errHMCD.Length > 0)
                        {
                            Clipboard.SetText(string.Join("\n", errHMCD));
                            var msg = "EM手配がコード票マスタに存在しません。\n" + 
                                "このまま処理を続行しますか？\n\n" + 
                                "（はい：エラー以外を取込 / いいえ：処理を中断）\n\n" + 
                                "品番：" + string.Join(" , ", errHMCD) + "\n" + 
                                "（対象品番をクリップしました．）";
                            if (MessageBox.Show(msg, "選択", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                                == DialogResult.No) return;
                        }

                        // MPシステム MySQLに集合差分を挿入
                        insCount = cmn.Dba.ImportMpOrder(ref exceptDt, ref mpCodeMDt, ref mpZaikoDt, ref mpNaijiDt, ref deleteODRNODt, c.Style.BackColor);
                        if (insCount < 0) return;
                    }
                }
            }

            // 内示カードの使用状況レポートExcelの作成とDB更新
            if (mpNaijiDt.Rows.Count > 0)
            {
                mpNaijiReportDt.Merge(mpNaijiDt);
                cmn.Dba.UpdateMpNaiji(ref mpNaijiReportDt); // KD8470:内示カードの更新
                // 内示カード使用状況レポートの作成
                // ※mpNaijiReportDtを直接加工するので以降のコードでは [mpNaijiReportDt] 使用不可
                await Task.Run(() => NaijiReport(ref mpNaijiReportDt, ref mpZaikoReportDt)); 
            }

            // 取込後、再読み込みして表示
            PopulateCalendar();

            // 処理ボタンを無効化
            Btn_ImportOrder.Enabled = false;
            Btn_ImportOrder.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_PrintOrder.Enabled = false;
            Btn_PrintOrder.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_PrintCancel.Enabled = false;
            Btn_PrintCancel.BackColor = Common.FRM40_BG_COLOR_CONTROL;

            // ステータスを表示
            toolStripStatusLabel2.Text = insCount.ToString("#,0") + "件の登録 ";
            toolStripStatusLabel2.Text += (delCount > 0) ? delCount.ToString("#,0") + "件を削除 " : "";
            toolStripStatusLabel2.Text += "しました．";
        }

        // 内示カードファイルの使用状況をレポート
        private void NaijiReport(ref DataTable mpNaijiReportDt, ref DataTable mpZaikoDt)
        {
            // データテーブルをExcelに落とした時に見やすくなるよう編集
            // 列ヘッダー名を日本語に変換
            mpNaijiReportDt.Columns["HMCD"].ColumnName = "品番";
            mpNaijiReportDt.Columns["WEEKEDDT"].ColumnName = "内示カード発行週";
            mpNaijiReportDt.Columns["PLANQTY"].ColumnName = "内示数";
            mpNaijiReportDt.Columns["ALLOCQTY"].ColumnName = "手配引当数";
            mpNaijiReportDt.Columns["PLANCARDDT"].ColumnName = "内示カード発行日";
            // 列ヘッダー名を削除
            mpNaijiReportDt.Columns.Remove("MPINSTID");
            mpNaijiReportDt.Columns.Remove("MPINSTDT");
            mpNaijiReportDt.Columns.Remove("MPUPDTID");
            mpNaijiReportDt.Columns.Remove("MPUPDTDT");

            // 仕掛かり在庫列と備考列を追加し仕掛かり在庫テーブルとコメントを設定
            mpNaijiReportDt.Columns.Add("仕掛在庫（引当前）", typeof(int));
            mpNaijiReportDt.Columns.Add("備考");
            foreach (DataRow r in mpNaijiReportDt.Rows)
            {
                if (mpZaikoDt.Select($"HMCD='{r["品番"]}'").Length > 0)
                {
                    r["仕掛在庫（引当前）"] = mpZaikoDt.Select($"HMCD='{r["品番"]}'")[0]["ZAIQTY"];
                }
                else
                {
                    r["仕掛在庫（引当前）"] = 0;
                }
                r["備考"] = string.Empty;
                if (int.Parse(r["手配引当数"].ToString()) == 0)
                {
                    r["備考"] = "内示のみ";
                }
                else
                {
                    if (int.Parse(r["内示数"].ToString()) > int.Parse(r["手配引当数"].ToString())) r["備考"] += "手配減算";
                    if (int.Parse(r["仕掛在庫（引当前）"].ToString()) >= int.Parse(r["手配引当数"].ToString()))
                    {
                        r["備考"] += (string.IsNullOrEmpty(r["備考"].ToString())) ? "" : "、";
                        r["備考"] += "仕掛かり在庫から手配完了";

                    }
                }
            }

            // Excelファイル処理
            int i = 1;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // デスクトップフォルダのパスを取得
            string FileName = $"内示カードレポート_{DateTime.Now.ToString("M")}";
            string FilePath = Path.Combine(desktopPath, FileName + ".xlsx");    // ファイルパスの取得
            while (File.Exists(@FilePath))
            {
                i++;
                FilePath = Path.Combine(desktopPath, FileName + $"({i}).xlsx"); // ファイルパスの取得
            }
            try
            {
                cmn.Fa.ExcelApplication(false);
                cmn.Fa.AddNewBook();
                cmn.Fa.ExportExcel(mpNaijiReportDt, @FilePath);
                cmn.Fa.SetNaijiReport();            // 内示カードレポートのExcel設定
                cmn.Fa.SaveWorkBook(@FilePath);     // oWBookを上書き保存
                cmn.Fa.CloseExcel2();
                // Interop.Excelではなく標準アプリケーションで開く
                Process.Start(@FilePath);
            }
            catch (Exception ex) // 例外処理
            {
                MessageBox.Show("内示カードレポートファイルの作成に失敗しました: " + ex.Message);
                toolStripStatusLabel2.Text = ex.Message;
                cmn.Fa.CloseExcel2();
            }
        }

        // 製造指示カード印刷
        private async void Btn_PrintOrder_Click(object sender, EventArgs e)
        {
            // 選択セルの並び替えクエリーサンプル（自分じゃ絶対わからないので残しておく）
            var query = from DataGridViewCell c in Dgv_Calendar.SelectedCells
                        where c.Style.BackColor == Common.FRM40_BG_COLOR_IMPORTED
                        orderby c.RowIndex, c.ColumnIndex
                        select c;
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

            // 選択セル範囲の開始日と終了日を設定
            var sortedCells = from DataGridViewCell c in Dgv_Calendar.SelectedCells
                              orderby c.RowIndex, c.ColumnIndex
                              select c;
            var firstCell = sortedCells.ToArray().First();
            var lastCell = sortedCells.ToArray().Last();
            var dayFrom = GetCurrentDateTime(firstCell);
            var dayTo = GetCurrentDateTime(lastCell);

            // 印刷前の最終確認
            var msg = (dayFrom == dayTo) ? dayFrom.ToString("M") : dayFrom.ToString("M") + "～" + dayTo.ToString("M");
            if (MessageBox.Show(msg + "を印刷します。\nよろしいですか？", "確認"
                , MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Cancel) return;

            // 印刷ボタン非活性化
            Btn_PrintOrder.BackColor = System.Drawing.SystemColors.Control;
            Btn_PrintOrder.Enabled = false;
            Btn_PrintCancel.BackColor = System.Drawing.SystemColors.Control;
            Btn_PrintCancel.Enabled = false;

            // ステータス表示
            progressmsg = "【製造指示カード】 ";
            toolStripStatusLabel1.Text = progressmsg + "準備中...";

            // Excelアプリケーションを起動
            cmn.Fa.OpenExcel2(idx);

            // 製造指示カード雛形を開く（拡縮倍率にあった帳票を選択）
            cmn.Fa.OpenExcelFile2($@"{cmn.FsCd[idx].RootPath}\{cmn.FsCd[idx].FileName}");

            // 選択セル範囲の製造指示データをDataTableに読み込む
            int ret = 0;
            toolStripStatusLabel1.Text = progressmsg + "データ読み込み中...";
            DataTable cardDt = new DataTable();
            await Task.Run(() => ret = cmn.Dba.GetOrderCardPrintInfo(ref dayFrom, ref dayTo, ref cardDt));
            if (ret <= 0)
            {
                if (ret == 0) toolStripStatusLabel1.Text = "印刷対象のデータはありませんでした.";
                cmn.Fa.CloseExcel2(); // Excelアプリケーションを閉じる
                return;
            }

            // 製造指示カード雛形に製造指示データをセット
            // 設定ファイルの場所にPDFとして保存して起動
            toolStripStatusLabel1.Text = progressmsg + $"{cardDt.Rows.Count}件 作成中...";
            await Task.Run(() => ret = PrintOrderCard(ref cardDt)); 
            if (ret != 0)
            {
                cmn.Fa.CloseExcel2(); // Excelアプリケーションを閉じる
                return;
            }

            // ExcelブックからPDFを作成
            var pdfName = cmn.FsCd[idx].FileName
                .Replace("雛形", "_" + DateTime.Now.ToString("yyyyMMddhhmm"))
                .Replace(".xlsx", ".pdf");
            cmn.Fa.ExportExcelToPDF($@"{cmn.FsCd[0].RootPath}\{pdfName}"); // 0:生産計画システム出力先フォルダ

            // 製造指示カード雛形を閉じる
            cmn.Fa.CloseExcelFile2(false);

            // 出力済ステータスに更新
            cmn.Dba.UpdatePrintOrderCardDay(ref dayFrom, ref dayTo);

            // Excelアプリケーションを閉じる
            cmn.Fa.CloseExcel2();

            // 取込後、再読み込みして表示
            PopulateCalendar();

            // 処理ボタンを無効化
            Btn_ImportOrder.Enabled = false;
            Btn_ImportOrder.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_PrintOrder.Enabled = false;
            Btn_PrintOrder.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_PrintCancel.Enabled = false;
            Btn_PrintCancel.BackColor = Common.FRM40_BG_COLOR_CONTROL;
        }

        /// <summary>
        /// 出荷指示カード作成
        /// </summary>
        /// <param name="cardDt">印刷対象データ</param>
        /// 
        /// <returns>結果 (0: 保存成功 (保存件数), -1: 保存失敗, -2: 認証失敗)</returns>
        public int PrintOrderCard(ref System.Data.DataTable cardDt)
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            int ret = 0;
            int i = 0;

            try
            {
                // Excelシートの作成
                int baseRow = 1;
                int cardCnt = 1;    // ※手配件数とカード枚数は異なる（収容数で分割するため）
                int cardRows = 21;  // 1カードの行数（余白含む）
                int row = 0;
                int col = 0;
                cmn.Fa.CreateTemplateOrderCard(); // テンプレートオブジェクトの作成（製造指示カードの雛形を作成）
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
                        // 書き込みを行う先頭行番号を計算
                        row = cardRows * (Convert.ToInt32(Math.Ceiling(cardCnt / 2d)) - 1) + baseRow;

                        // 左右の列番号を切り替え
                        col = (cardCnt % 2 != 0) ? 1 : 10;

                        // 処理速度の計測開始
                        // DateTime SW3 = DateTime.Now;
                        // Debug.WriteLine("[StopWatch] Read開始 ");

                        // カードに値をセット
                        cmn.Fa.SetOrderCard(ref r, ref row, ref col, j ,loopCnt);

                        // COMアクセスへの処理速度の計測終了
                        // Debug.WriteLine("[StopWatch] Read終了 " + DateTime.Now.ToString("HH:mm:ss") + " (" + DateTime.Now.Subtract(SW3).TotalSeconds.ToString("F3") + "秒)");

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
                toolStripStatusLabel2.Text = "手配No [" + cardDt.Rows[i]["ODRNO"].ToString() + "] で異常が発生しました．";
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

        // 印刷不要（印刷済みにしてしまう）
        private void Btn_PrintCancel_Click(object sender, EventArgs e)
        {
            // 選択セル範囲の開始日と終了日を設定
            var sortedCells = from DataGridViewCell c in Dgv_Calendar.SelectedCells
                              orderby c.RowIndex, c.ColumnIndex
                              select c;
            var firstCell = sortedCells.ToArray().First();
            var lastCell = sortedCells.ToArray().Last();
            var dayFrom = GetCurrentDateTime(firstCell);
            var dayTo = GetCurrentDateTime(lastCell);

            // 出力済ステータスに更新
            toolStripStatusLabel1.Text = progressmsg + "データ更新中...";
            cmn.Dba.UpdatePrintOrderCardDay(ref dayFrom, ref dayTo);

            // 取込後、再読み込みして表示
            PopulateCalendar();

            // 処理ボタンを無効化
            Btn_ImportOrder.Enabled = false;
            Btn_ImportOrder.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_PrintOrder.Enabled = false;
            Btn_PrintOrder.BackColor = Common.FRM40_BG_COLOR_CONTROL;
            Btn_PrintCancel.Enabled = false;
            Btn_PrintCancel.BackColor = Common.FRM40_BG_COLOR_CONTROL;
        }

        private void toolStripStatusLabel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MessageBox.Show("EMステータスの取込処理を行いますか？", "裏メニュー",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                    ) == DialogResult.Yes)
                {
                    ImportEMStatus();
                }
            }
        }

        // EMの手配状態を取り込む（裏メニュー）
        private async void ImportEMStatus()
        {
            bool ret = false;
            DataTable dtEM = new DataTable();
            await Task.Run(() => ret = cmn.Dba.GetD0410ODRSTS(ref dtEM)); // EMの手配ファイルの取込
            if (ret)
            {
                int updateCnt = cmn.Dba.UpdateODRSTS(ref dtEM);
                if (updateCnt == 0) toolStripStatusLabel2.Text = "更新はありませんでした．";
                if (updateCnt > 0) toolStripStatusLabel2.Text = updateCnt.ToString("#,0") + "件を更新しました．";
                if (updateCnt < 0) toolStripStatusLabel2.Text = "ステータス更新で異常が発生しました．";
            }
            else
            {
                toolStripStatusLabel2.Text = "EMデータベース異常が発生しました．";
            }
        }

    }
}
