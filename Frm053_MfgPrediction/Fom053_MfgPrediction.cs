using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPPPS
{
    public partial class Frm053_MfgPrediction : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DataTable km8420 = new DataTable();     // 設備マスタ

        // コンストラクタ
        public Frm053_MfgPrediction(Common cmn)
        {
            InitializeComponent();

            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_053 + ": " + Common.FRM_NAME_053 + ">";
            // 共通クラス
            this.cmn = cmn;

            // 初期設定
            SetInitialValues();

            // イベント登録
            dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;           // 行番号と矢印
        }

        // 初期設定
        private void SetInitialValues()
        {
            // データ読み込み
            if (cmn.Dba.GetEquipMstDt(ref km8420)==false) Close();
            DataTable xt2OrderDt = new DataTable();
            bool ret = cmn.Dba.ReadXT2(ref xt2OrderDt);

            // 設備グループドロップダウン設定
            List<string> mcgcds = km8420.AsEnumerable()
                .OrderBy(x => x["MCGSEQ"])
                .GroupBy(grp => new {
                    MCGCD = grp["MCGCD"].ToString()
                })
                .Select(row => row.Key.MCGCD)
                .ToList();
            cmb_MCGCD.Items.AddRange(mcgcds.ToArray());
            cmb_MCGCD.Text = "MS";  // デフォルト値を設定＆イベントを発生させる
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
            // 「閉じる」
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        // 設備グループ変更イベント
        private void Cmb_MCGCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_MCCD.Items.Clear();
            var findstring = cmb_MCGCD.Text;
            List<string> mccds = km8420.AsEnumerable()
                .Where(x => x["MCGCD"].ToString().StartsWith(findstring) && x["MCSEQ"].ToString().Length < 3)
                .OrderBy(x => x["MCSEQ"])
                .Select(c => c["MCCD"].ToString())
                .ToList();
            cmb_MCCD.Items.AddRange(mccds.ToArray());
            cmb_MCCD.SelectedIndex = 0;  // デフォルト値を設定
        }

        // Excelにエクスポート
        private void ButtonExcelExport_Click(object sender, EventArgs e)
        {

        }



        // 注文詳細情報、品番詳細情報
        private void RefreshPlanInformation()
        {
            var dgv = dataGridView1;
            if (dgv.CurrentCell == null) return;

            int row = dgv.CurrentCell.RowIndex;
            int col = dgv.CurrentCell.ColumnIndex;

            if (dgv.CurrentCell.Value == null) return;

            try
            {
                string hmcd = dgv[0, row].Value.ToString();
                string hmnm = dgv[1, row].Value.ToString();

            }
            catch (Exception ex)
            {
            }
        }

        /*
①MySQLから検索対象のLike内容を取得
SELECT
    CONCAT('(', GROUP_CONCAT(CONCAT('''', hmcd, '''') SEPARATOR ','), ')') AS HMCDLIST,
    COUNT(*) AS 件数
FROM km8430
WHERE ktkey LIKE '%MS-3%';

②OracleViewから検索対象のLike内容を取得
with vbom as
(
    select * from V_BOM_LEAF_PARENT where KOHMCD in 
    ('146623-59191','146623-59200','146676-59170','172480-13731','198137-28571','1A7340-48201','1A7750-48581','1A7815-49290','32791-58271-2','3B441-18152-3','3B791-67531','3C134-69311-2','3C581-67532','3C651-62271-2','3C651-62272-2','3F243-82991-2','3U503-04222-2','4198413','44564-75511-2B','4627795','5T150-27791-1','933131-60100','K7561-33421-2','RA021-94241-1','RD819-64021-3','RP471-68651-1','TD170-33411-3','TD170-33425-3','TD270-33421-3','V0511-51781-1','V1311-65031-6') -- MS-3
)
select v.KOHMCD 切削品番
    , '内示' 区分
    , case when min(HMCD) is null then 'なし' else LISTAGG(distinct v.OYAHMCD, ',') WITHIN GROUP (ORDER BY v.OYAHMCD) end as 注文品番
    , trunc(JUDT, 'MM') 集計月
    , sum(JUQTY*原単位) 注文数
from vbom v
    left outer join D0030 on HMCD=v.OYAHMCD and JUDT between ADD_MONTHS(TRUNC(SYSDATE, 'YYYY'), -12) and  ADD_MONTHS(TRUNC(SYSDATE, 'YYYY'), 24) - 1
group by v.KOHMCD, trunc(JUDT, 'MM')
union all
select v.KOHMCD 切削品番
    , '手配' 区分
    , case when min(HMCD) is null then 'なし' else LISTAGG(distinct v.OYAHMCD, ',') WITHIN GROUP (ORDER BY v.OYAHMCD) end as 注文品番
    , trunc(JUDT, 'MM') 集計月
    , sum(JUQTY*原単位) 注文数
from vbom v
    left outer join D0010 on HMCD=v.OYAHMCD and JUDT between ADD_MONTHS(TRUNC(SYSDATE, 'YYYY'), -12) and  ADD_MONTHS(TRUNC(SYSDATE, 'YYYY'), 24) - 1 and ODRSTS<>'4' -- 4:中止
group by v.KOHMCD, trunc(JUDT, 'MM')
order by 切削品番

③C#でPivot化
DataTable src = oracleResult; // Oracleから取得した縦持ちデータ

// ピボット用の結果テーブル
DataTable pivot = new DataTable();
pivot.Columns.Add("KOHMCD");

// 月一覧を動的に抽出
var months = src.AsEnumerable()
    .Select(r => r.Field<DateTime>("集計月"))
    .Distinct()
    .OrderBy(d => d);

foreach (var m in months)
{
    pivot.Columns.Add(m.ToString("yyyy-MM"), typeof(decimal));
}

// ピボット化
var kohmcdList = src.AsEnumerable()
    .Select(r => r.Field<string>("KOHMCD"))
    .Distinct();

foreach (var code in kohmcdList)
{
    DataRow row = pivot.NewRow();
    row["KOHMCD"] = code;

    foreach (var m in months)
    {
        var qty = src.AsEnumerable()
            .Where(r => r.Field<string>("KOHMCD") == code &&
                        r.Field<DateTime>("集計月") == m)
            .Select(r => r.Field<decimal>("月合計"))
            .FirstOrDefault();

        row[m.ToString("yyyy-MM")] = qty;
    }

    pivot.Rows.Add(row);
}

// DataGridView に表示
dataGridView1.DataSource = pivot;








        */

    }
}
