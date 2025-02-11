using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm041_CreateOrder : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DateTime targetMonth;                   // 処理対象月を設定
        private DataTable orderDt = new DataTable();    // 注文状況を保持（カレンダー詳細情報で使用）
        private DataTable dtS0820working;               // カレンダーテーブルを保持

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="common">共通クラス</param>
        /// <param name="sender">送信オジェクト</param>
        public Frm041_CreateOrder(Common cmn, object sender)
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
            Frm041_CreateOrder_Resize(this, EventArgs.Empty);
        }

        // 別スレッドにてカレンダー用のデータを取得
        private void SubThreadGetOrders()
        {
            bool ret = false;
            toolStripStatusLabel.Text = "EM注文データ確認中...";
            orderDt.Clear();
            ret = cmn.Dba.GetEMOrderInfo(ref orderDt, targetMonth);
            if (ret == false) return;

            // EM注文データテーブルに列を追加
            if (orderDt.Columns.Count == 6)
            {
                orderDt.Columns.Add("MP2確定", typeof(long));
                orderDt.Columns.Add("MP3着手", typeof(long));
                orderDt.Columns.Add("MP4完了", typeof(long));
                orderDt.Columns.Add("MP9取消", typeof(long));
                orderDt.Columns.Add("取込合計", typeof(long));
                orderDt.Columns.Add("分割合計", typeof(long));
            }

            // MPデータテーブルを取得
            DataTable mpDt = new DataTable();
            toolStripStatusLabel.Text = "MP切削データ確認中...";
            cmn.Dba.GetMPOrderInfo(ref mpDt, targetMonth);

            // MPデータの内容をEM注文データにマージ
            foreach (DataRow r in orderDt.Rows)
            {
                if (mpDt.Select($"EDDT='{r["EDDT"]}'").Length > 0)
                {
                    r["MP2確定"] = mpDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP2確定"];
                    r["MP3着手"] = mpDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP3着手"];
                    r["MP4完了"] = mpDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP4完了"];
                    r["MP9取消"] = mpDt.Select($"EDDT='{r["EDDT"]}'")[0]["MP9取消"];
                    r["取込合計"] = mpDt.Select($"EDDT='{r["EDDT"]}'")[0]["取込合計"];
                    r["分割合計"] = 0;
                }
                else
                {
                    r["MP2確定"] = 0;
                    r["MP3着手"] = 0;
                    r["MP4完了"] = 0;
                    r["MP9取消"] = 0;
                    r["取込合計"] = 0;
                    r["分割合計"] = 0;
                }
            }
            toolStripStatusLabel.Text = "日付をクリックして注文データを取り込んでください．";
        }

        // カレンダー関連の初期化
        private void ClearCalendar()
        {
            // カレンダーラベルを設定
            CalendarLabel.Text = $"{targetMonth.Month}月 カレンダー";

            // データのクリアと背景色のクリア
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (col != 0 && col != 6)
                    {
                        Dgv_Calendar[col, row].Style.BackColor = Color.White;
                    }
                    Dgv_Calendar[col, row].Value = "";
                }
            }
        }

        // カレンダーを作成
        private async void PopulateCalendar()
        {
            // カレンダー関連の初期化
            ClearCalendar();

            // 別スレッドにて注文データを取得
            await Task.Run(() => SubThreadGetOrders());

            int row = 0; // 0:日曜日

            // 前月があれば作成
            DateTime dayOfPrevMonth = targetMonth.AddDays(-1);
            for (int week = (int)dayOfPrevMonth.DayOfWeek; week >= 0; week--)
            {
                int column = (int)dayOfPrevMonth.DayOfWeek;
                Dgv_Calendar[column, row].Value = dayOfPrevMonth.Day;
                if (orderDt.Select($"EDDT='{dayOfPrevMonth}' and EM合計=取込合計").Count() != 0)
                {
                    // 注文データ取込済
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
                // 日付を設定
                int column = (int)currentDate.DayOfWeek;
                Dgv_Calendar[column, row].Value = day;
                // 土日の色とEMお休み日の色、を変える
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
                else if (orderDt.Select($"EDDT='{currentDate}' and 取込合計=0 and EM合計>0").Count() != 0)
                {
                    // 注文あり取込データなし
                    var emCount = Int32.Parse(orderDt.Select($"EDDT='{currentDate}'")[0]["EM合計"].ToString());
                    var mpCount = Int32.Parse(orderDt.Select($"EDDT='{currentDate}'")[0]["取込合計"].ToString());
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightCyan;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                }
                else if (orderDt.Select($"EDDT='{currentDate}' and 取込合計>0 and EM合計<>取込合計").Count() != 0)
                {
                    // 注文データ取込後にEM側に追加注文あり
                    var emCount = Int32.Parse(orderDt.Select($"EDDT='{currentDate}'")[0]["EM合計"].ToString());
                    var mpCount = Int32.Parse(orderDt.Select($"EDDT='{currentDate}'")[0]["取込合計"].ToString());
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightCoral;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                    Dgv_Calendar[column, row].Value = day + $"\n未取込が\n{emCount - mpCount}件あります";
                }
                else if (orderDt.Select($"EDDT='{currentDate}' and EM9取消>0 and EM9取消<>MP9取消").Count() != 0)
                {
                    // 注文データ取込後にEM側で注文取消あり
                    var emCancel = Int32.Parse(orderDt.Select($"EDDT='{currentDate}'")[0]["EM9取消"].ToString());
                    var mpCancel = Int32.Parse(orderDt.Select($"EDDT='{currentDate}'")[0]["MP9取消"].ToString());
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightCoral;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                    Dgv_Calendar[column, row].Value = day + $"\nEM取消が\n{emCancel - mpCancel}件あります";
                }
                else if (orderDt.Select($"EDDT='{currentDate}' and EM合計=取込合計").Count() != 0)
                {
                    // 注文データ取込済
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightGreen;
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
            while (row < 6)
            {
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
                else if (orderDt.Select($"EDDT='{nextDate}' and 取込合計=0 and EM合計>0").Count() != 0)
                {
                    // 注文あり取込データなし
                    var emCount = Int32.Parse(orderDt.Select($"EDDT='{nextDate}'")[0]["EM合計"].ToString());
                    var mpCount = Int32.Parse(orderDt.Select($"EDDT='{nextDate}'")[0]["取込合計"].ToString());
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightCyan;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                }
                else if (orderDt.Select($"EDDT='{nextDate}' and 取込合計>0 and EM合計<>取込合計").Count() != 0)
                {
                    // 注文データ取込後にEM側に追加注文あり
                    var emCount = Int32.Parse(orderDt.Select($"EDDT='{nextDate}'")[0]["EM合計"].ToString());
                    var mpCount = Int32.Parse(orderDt.Select($"EDDT='{nextDate}'")[0]["取込合計"].ToString());
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightCoral;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                    Dgv_Calendar[column, row].Value = dayOfNextMonth + $"\n未取込が\n{emCount - mpCount}件あります";
                }
                else if (orderDt.Select($"EDDT='{nextDate}' and EM9取消>0 and EM9取消<>MP9取消").Count() != 0)
                {
                    // 注文データ取込後にEM側で注文取消あり
                    var emCancel = Int32.Parse(orderDt.Select($"EDDT='{nextDate}'")[0]["EM9取消"].ToString());
                    var mpCancel = Int32.Parse(orderDt.Select($"EDDT='{nextDate}'")[0]["MP9取消"].ToString());
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightCoral;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.Black;
                    Dgv_Calendar[column, row].Value = dayOfNextMonth + $"\nEM取消が\n{emCancel - mpCancel}件あります";
                }
                else if (orderDt.Select($"EDDT='{nextDate}' and EM合計=取込合計").Count() != 0)
                {
                    // 注文データ取込済
                    Dgv_Calendar[column, row].Style.BackColor = Color.LightGreen;
                    Dgv_Calendar[column, row].Style.ForeColor = Color.DimGray;
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
            // 稼働日数を算出（非同期処理データテーブルの為処理の後ろの方で実行）
            int days = (int)dtS0820working.Select(String.Format(
                "#{1}# <= [{0}] AND [{0}] < #{2}#",
                "YMD", targetMonth, targetMonth.AddMonths(1))).Count();
            CalendarLabel.Text = $"{targetMonth.Month}月 カレンダー ({days}日稼働)";
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

        // フォームサイズ変更対応
        private void Frm041_CreateOrder_Resize(object sender, EventArgs e)
        {
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
            Btn_ImportOrder.Width = Dgv_Calendar.Width - 5;
        }

        // カレンダークリックイベント（1件選択の場合、ステータスに情報を表示）
        private void Dgv_Calendar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Dgv_Calendar[e.ColumnIndex, e.RowIndex].Value == null) return;
            if (Dgv_Calendar.SelectedCells.Count == 1)
            {
                // クリックされたセルの日付を取得
                var planDay = GetCurrentDateTime(Dgv_Calendar[e.ColumnIndex, e.RowIndex]);
                if (orderDt.Select($"EDDT='{planDay}'").Length > 0)
                {
                    DataRow r = orderDt.Select($"EDDT='{planDay}'")[0];
                    toolStripStatusLabel.Text = ""
                        + $"取込済: {r["取込合計"].ToString()}件 / {r["EM合計"].ToString()}件";
                    if (r["EM9取消"].ToString() != "0")
                        toolStripStatusLabel.Text += " ( "
                        + $"EM取消: {r["EM9取消"].ToString()}件, MP取消{r["MP9取消"].ToString()}件 )";
                }
                else
                {
                    toolStripStatusLabel.Text = "";
                }
            }
        }

        // 複数セルが選択された場合、ステータス上に合算した情報を表示
        private void Dgv_Calendar_MouseUp(object sender, MouseEventArgs e)
        {
            if (Dgv_Calendar.SelectedCells.Count == 1) return;
            toolStripStatusLabel.Text = "";
            int emOrder = 0;
            int mpOrder = 0;
            int emCancel = 0;
            int mpCancel = 0;
            foreach (DataGridViewCell c in Dgv_Calendar.SelectedCells)
            {
                var planDay = GetCurrentDateTime(c);
                if (orderDt.Select($"EDDT='{planDay}'").Length > 0)
                {
                    DataRow r = orderDt.Select($"EDDT='{planDay}'")[0];
                    emOrder += Int32.Parse(r["EM合計"].ToString());
                    mpOrder += Int32.Parse(r["取込合計"].ToString());
                    emCancel += Int32.Parse(r["EM9取消"].ToString());
                    mpCancel += Int32.Parse(r["MP9取消"].ToString());
                }
            }
            if (emOrder > 0)
            {
                toolStripStatusLabel.Text = $"{Dgv_Calendar.SelectedCells.Count}件選択 ⇒ "
                    + $"取込済: {mpOrder}件 / {emOrder}件";
                if (emCancel > 0)
                    toolStripStatusLabel.Text += $" ( EM取消: {emCancel}件, MP取消{mpCancel}件 )";
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

        // 注文データ取込
        private async void Btn_ImportOrder_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell c in Dgv_Calendar.SelectedCells)
            {

            }
                // 整合性チェック
                // チェックが付いた行の条件を作成しDBAccesorに渡す(Subitems[1]は'yyyy/mm/dd hh24:mi:ss'非表示列)
                string eddts = "";
            //foreach (ListViewItem item in lvw_Order.Items)
            //{
            //    if (item.Checked)
            //    {
            //        var str = item.SubItems[7].Text;
            //        if (!str.Contains("(-)") &&
            //            str.Split('(')[0].Trim() == str.Split('(', ')')[1].Trim())
            //        {
            //            // 既に取込済み
            //            Debug.WriteLine(Common.MSGBOX_TXT_ERR + ": " + MethodBase.GetCurrentMethod().Name);
            //            cmn.ShowMessageBox(Common.KCM_PGM_ID, "既に取込済みです.確認してください．", Common.MSG_TYPE_W,
            //                MessageBoxButtons.OK, Common.MSGBOX_TXT_WARN, MessageBoxIcon.Warning);
            //            return;
            //        }
            //        eddts += (string.IsNullOrEmpty(eddts) ? "(" : ",") + "to_timestamp('" + item.SubItems[1].Text + "')";
            //    }
            //}
            if (string.IsNullOrEmpty(eddts)) return;
            eddts += ")";

            // 選択行をシステムに取込
            toolStripStatusLabel.Text = "注文データ取込中...";
            bool ret = false;
            await Task.Run(() => ret = cmn.Dba.OrderImport(eddts));
            if (ret)
            {
                // 取込成功後、再読み込みして表示
                //Btn_CheckOrder_Click(sender, e);

                // 分割ボタンを有効化
                Btn_ImportOrder.Enabled = false;
                Btn_ImportOrder.BackColor = SystemColors.Control;
                //Btn_DivideOrder.Enabled = true;
                //Btn_DivideOrder.BackColor = Color.FromArgb(128, 255, 255);
            }
        }

    }
}
