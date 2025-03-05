using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace MPPPS
{
    public partial class Frm033_EqMstMaint : Form
    {
        // 共通クラス
        private readonly Common cmn;
        private DataTable equipMstDt = new DataTable(); // 設備マスタを保持
        private bool loadedFlg = false;

        public Frm033_EqMstMaint(Common cmn)
        {
            InitializeComponent();
            // フォームのアイコンを設定する
            Icon = new System.Drawing.Icon(Common.ICON_FILE);

            // フォームのタイトルを設定する
            Text = "[" + Common.MY_PGM_ID + "] " + Common.MY_PGM_NAME + " - Ver." + Common.MY_PGM_VER
                      + " <" + Common.FRM_ID_041 + ": " + Common.FRM_NAME_041 + ">";

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

            // DataGridViewの初期設定
            Dgv_EquipMst.DataSource = equipMstDt;
            // DataGridViewのヘッダー背景色を設定
            Dgv_EquipMst.EnableHeadersVisualStyles = false;
            Dgv_EquipMst.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            Dgv_EquipMst.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            // DataGridViewの明細を2行毎に背景色設定
            Dgv_EquipMst.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            //DataGridViewの画面ちらつきをおさえるため、DoubleBufferedを有効にする
            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(Dgv_EquipMst, true, null);
            // データグリッドビューの個別設定
            DataGridDetailSetting();

            // トグルボタンを標準表示側に設定
            //tglViewNormal.Select();

            loadedFlg = true;

        }

        // データグリッドビューの個別設定
        private void DataGridDetailSetting()
        {
            // 列ヘッダーの文字列を文字位置を設定
            var offset = 0;
            string[] s1 = {
                "順序",
                "ｸﾞﾙｰﾌﾟｺｰﾄﾞ",
                "ｸﾞﾙｰﾌﾟ名称",
                "順序",
                "設備ｺｰﾄﾞ",
                "設備名称",
                "稼働時間",
                "",
                "",
                "段取１",
                "CT",
                "段取２",
                "CT",
                "段取３",
                "CT",
                "登録ID",
                "登録日時",
                "更新ID",
                "更新日時"
            };
            for (int i = 0; i < s1.Length; i++)
            {
                Dgv_EquipMst.Columns[i + offset].HeaderText = s1[i];
                if (i==0 || i==3)
                {
                    Dgv_EquipMst.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Dgv_EquipMst.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Dgv_EquipMst.Columns[i].Width = 40;
                }
                if (i == 6 || i == 10 || i == 12 || i == 14)
                {
                    Dgv_EquipMst.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Dgv_EquipMst.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Dgv_EquipMst.Columns[i].Width = 60;
                }
            }
            offset += s1.Length;

            // DataGridViewの幅を個別設定
            Dgv_EquipMst.Columns[2].Width = 180;        // ｸﾞﾙｰﾌﾟ名称
            Dgv_EquipMst.Columns[5].Width = 180;        // 設備名称
            Dgv_EquipMst.Columns[9].Width = 200;        // 段取り１
            Dgv_EquipMst.Columns[11].Width = 230;       // 段取り２
            Dgv_EquipMst.Columns[13].Width = 230;       // 段取り３
            Dgv_EquipMst.Columns[16].Width = 200;       // 登録日時
            Dgv_EquipMst.Columns[18].Width = 200;       // 更新日時

            // DataGridViewの非表示設定
            Dgv_EquipMst.Columns[7].Visible = false;    // FLG1
            Dgv_EquipMst.Columns[8].Visible = false;    // FLG2
        }

    }
}
