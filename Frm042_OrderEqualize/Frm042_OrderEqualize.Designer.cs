using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm042_OrderEqualize
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Sst = new System.Windows.Forms.StatusStrip();
            this.Tsl_Msg = new System.Windows.Forms.ToolStripStatusLabel();
            this.Btn_Search = new System.Windows.Forms.Button();
            this.Btn_Delete = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.Btn_Conf = new System.Windows.Forms.Button();
            this.Btn_InsUpd = new System.Windows.Forms.Button();
            this.Tbc_FormType = new System.Windows.Forms.TabControl();
            this.Tbp_Slip = new System.Windows.Forms.TabPage();
            this.Gbx_Detail = new System.Windows.Forms.GroupBox();
            this.Gbx_Cur = new System.Windows.Forms.GroupBox();
            this.Dgv_MpCurOdrTbl = new System.Windows.Forms.DataGridView();
            this.Tbx_CurOpeAvail = new System.Windows.Forms.TextBox();
            this.Tbx_CurOpeRate = new System.Windows.Forms.TextBox();
            this.Tbx_CurOpeTm = new System.Windows.Forms.TextBox();
            this.Lbl_CurOpeAvail = new System.Windows.Forms.Label();
            this.Lbl_CurOpeRate = new System.Windows.Forms.Label();
            this.Lbl_CurOpeTm = new System.Windows.Forms.Label();
            this.Gbx_Sim = new System.Windows.Forms.GroupBox();
            this.Dgv_MpSimOdrTbl = new System.Windows.Forms.DataGridView();
            this.Tbx_SimOpeAvail = new System.Windows.Forms.TextBox();
            this.Tbx_SimOpeRate = new System.Windows.Forms.TextBox();
            this.Tbx_SimOpeTm = new System.Windows.Forms.TextBox();
            this.Lbl_SimOpeAvail = new System.Windows.Forms.Label();
            this.Lbl_SimOpeRate = new System.Windows.Forms.Label();
            this.Lbl_SimOpeTm = new System.Windows.Forms.Label();
            this.Gbx_InqKey = new System.Windows.Forms.GroupBox();
            this.Tbx_McSetupTm = new System.Windows.Forms.TextBox();
            this.Dtp_EdDt = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.Tbx_McOnTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Cbx_McCd = new System.Windows.Forms.ComboBox();
            this.Lbl_Hm = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Cbx_McGCd = new System.Windows.Forms.ComboBox();
            this.Lbl_WkGr = new System.Windows.Forms.Label();
            this.Lbl_EdDt = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Tbp_List = new System.Windows.Forms.TabPage();
            this.Lbl_BatchEdit = new System.Windows.Forms.Label();
            this.Dgv_CycleTmTbl = new System.Windows.Forms.DataGridView();
            this.Btn_SaveCsvFile = new System.Windows.Forms.Button();
            this.Btn_ReadCsvFile = new System.Windows.Forms.Button();
            this.Btn_Clear = new System.Windows.Forms.Button();
            this.Sst.SuspendLayout();
            this.Tbc_FormType.SuspendLayout();
            this.Tbp_Slip.SuspendLayout();
            this.Gbx_Detail.SuspendLayout();
            this.Gbx_Cur.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_MpCurOdrTbl)).BeginInit();
            this.Gbx_Sim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_MpSimOdrTbl)).BeginInit();
            this.Gbx_InqKey.SuspendLayout();
            this.Tbp_List.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CycleTmTbl)).BeginInit();
            this.SuspendLayout();
            // 
            // Sst
            // 
            this.Sst.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Sst.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tsl_Msg});
            this.Sst.Location = new System.Drawing.Point(0, 812);
            this.Sst.Name = "Sst";
            this.Sst.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.Sst.Size = new System.Drawing.Size(1514, 26);
            this.Sst.TabIndex = 8;
            this.Sst.Text = "Sst";
            // 
            // Tsl_Msg
            // 
            this.Tsl_Msg.Name = "Tsl_Msg";
            this.Tsl_Msg.Size = new System.Drawing.Size(59, 20);
            this.Tsl_Msg.Text = "Tsl_Msg";
            // 
            // Btn_Search
            // 
            this.Btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Search.Location = new System.Drawing.Point(974, 770);
            this.Btn_Search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(100, 30);
            this.Btn_Search.TabIndex = 3;
            this.Btn_Search.Text = "検索";
            this.Btn_Search.UseVisualStyleBackColor = true;
            this.Btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // Btn_Delete
            // 
            this.Btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Delete.Location = new System.Drawing.Point(1292, 770);
            this.Btn_Delete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Delete.Name = "Btn_Delete";
            this.Btn_Delete.Size = new System.Drawing.Size(100, 30);
            this.Btn_Delete.TabIndex = 6;
            this.Btn_Delete.Text = "削除";
            this.Btn_Delete.UseVisualStyleBackColor = true;
            this.Btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Close.Location = new System.Drawing.Point(1398, 770);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(100, 30);
            this.Btn_Close.TabIndex = 7;
            this.Btn_Close.Text = "閉じる";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Btn_Conf
            // 
            this.Btn_Conf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Conf.Location = new System.Drawing.Point(1080, 770);
            this.Btn_Conf.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Conf.Name = "Btn_Conf";
            this.Btn_Conf.Size = new System.Drawing.Size(100, 30);
            this.Btn_Conf.TabIndex = 4;
            this.Btn_Conf.Text = "確認";
            this.Btn_Conf.UseVisualStyleBackColor = true;
            this.Btn_Conf.Click += new System.EventHandler(this.Btn_Conf_Click);
            // 
            // Btn_InsUpd
            // 
            this.Btn_InsUpd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_InsUpd.Location = new System.Drawing.Point(1186, 770);
            this.Btn_InsUpd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_InsUpd.Name = "Btn_InsUpd";
            this.Btn_InsUpd.Size = new System.Drawing.Size(100, 30);
            this.Btn_InsUpd.TabIndex = 5;
            this.Btn_InsUpd.Text = "登録/更新";
            this.Btn_InsUpd.UseVisualStyleBackColor = true;
            this.Btn_InsUpd.Click += new System.EventHandler(this.Btn_InsUpd_Click);
            // 
            // Tbc_FormType
            // 
            this.Tbc_FormType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tbc_FormType.Controls.Add(this.Tbp_Slip);
            this.Tbc_FormType.Controls.Add(this.Tbp_List);
            this.Tbc_FormType.Location = new System.Drawing.Point(12, 12);
            this.Tbc_FormType.Name = "Tbc_FormType";
            this.Tbc_FormType.SelectedIndex = 0;
            this.Tbc_FormType.Size = new System.Drawing.Size(1490, 744);
            this.Tbc_FormType.TabIndex = 0;
            this.Tbc_FormType.SelectedIndexChanged += new System.EventHandler(this.Tbc_FormType_SelectedIndexChanged);
            // 
            // Tbp_Slip
            // 
            this.Tbp_Slip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tbp_Slip.Controls.Add(this.Gbx_Detail);
            this.Tbp_Slip.Controls.Add(this.Gbx_InqKey);
            this.Tbp_Slip.Location = new System.Drawing.Point(4, 25);
            this.Tbp_Slip.Name = "Tbp_Slip";
            this.Tbp_Slip.Padding = new System.Windows.Forms.Padding(3);
            this.Tbp_Slip.Size = new System.Drawing.Size(1482, 715);
            this.Tbp_Slip.TabIndex = 0;
            this.Tbp_Slip.Text = "単票形式";
            this.Tbp_Slip.UseVisualStyleBackColor = true;
            this.Tbp_Slip.Click += new System.EventHandler(this.Tbp_Slip_Click);
            // 
            // Gbx_Detail
            // 
            this.Gbx_Detail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Gbx_Detail.Controls.Add(this.Gbx_Cur);
            this.Gbx_Detail.Controls.Add(this.Gbx_Sim);
            this.Gbx_Detail.Location = new System.Drawing.Point(27, 137);
            this.Gbx_Detail.Margin = new System.Windows.Forms.Padding(4);
            this.Gbx_Detail.Name = "Gbx_Detail";
            this.Gbx_Detail.Padding = new System.Windows.Forms.Padding(4);
            this.Gbx_Detail.Size = new System.Drawing.Size(1429, 571);
            this.Gbx_Detail.TabIndex = 1;
            this.Gbx_Detail.TabStop = false;
            this.Gbx_Detail.Text = "詳細";
            // 
            // Gbx_Cur
            // 
            this.Gbx_Cur.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Gbx_Cur.Controls.Add(this.Dgv_MpCurOdrTbl);
            this.Gbx_Cur.Controls.Add(this.Tbx_CurOpeAvail);
            this.Gbx_Cur.Controls.Add(this.Tbx_CurOpeRate);
            this.Gbx_Cur.Controls.Add(this.Tbx_CurOpeTm);
            this.Gbx_Cur.Controls.Add(this.Lbl_CurOpeAvail);
            this.Gbx_Cur.Controls.Add(this.Lbl_CurOpeRate);
            this.Gbx_Cur.Controls.Add(this.Lbl_CurOpeTm);
            this.Gbx_Cur.Location = new System.Drawing.Point(7, 22);
            this.Gbx_Cur.Name = "Gbx_Cur";
            this.Gbx_Cur.Size = new System.Drawing.Size(700, 542);
            this.Gbx_Cur.TabIndex = 0;
            this.Gbx_Cur.TabStop = false;
            this.Gbx_Cur.Text = "現状";
            // 
            // Dgv_MpCurOdrTbl
            // 
            this.Dgv_MpCurOdrTbl.AccessibleDescription = "z";
            this.Dgv_MpCurOdrTbl.AllowUserToAddRows = false;
            this.Dgv_MpCurOdrTbl.AllowUserToDeleteRows = false;
            this.Dgv_MpCurOdrTbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dgv_MpCurOdrTbl.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Dgv_MpCurOdrTbl.ColumnHeadersHeight = 29;
            this.Dgv_MpCurOdrTbl.Location = new System.Drawing.Point(21, 21);
            this.Dgv_MpCurOdrTbl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dgv_MpCurOdrTbl.Name = "Dgv_MpCurOdrTbl";
            this.Dgv_MpCurOdrTbl.ReadOnly = true;
            this.Dgv_MpCurOdrTbl.RowHeadersWidth = 51;
            this.Dgv_MpCurOdrTbl.RowTemplate.Height = 24;
            this.Dgv_MpCurOdrTbl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_MpCurOdrTbl.Size = new System.Drawing.Size(661, 456);
            this.Dgv_MpCurOdrTbl.TabIndex = 0;
            this.Dgv_MpCurOdrTbl.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.Dgv_CellValidating);
            // 
            // Tbx_CurOpeAvail
            // 
            this.Tbx_CurOpeAvail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Tbx_CurOpeAvail.Location = new System.Drawing.Point(580, 496);
            this.Tbx_CurOpeAvail.Name = "Tbx_CurOpeAvail";
            this.Tbx_CurOpeAvail.ReadOnly = true;
            this.Tbx_CurOpeAvail.Size = new System.Drawing.Size(102, 22);
            this.Tbx_CurOpeAvail.TabIndex = 6;
            this.Tbx_CurOpeAvail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Tbx_CurOpeAvail.TextChanged += new System.EventHandler(this.Tbx_CurOpeAvail_TextChanged);
            // 
            // Tbx_CurOpeRate
            // 
            this.Tbx_CurOpeRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Tbx_CurOpeRate.Location = new System.Drawing.Point(411, 496);
            this.Tbx_CurOpeRate.Name = "Tbx_CurOpeRate";
            this.Tbx_CurOpeRate.ReadOnly = true;
            this.Tbx_CurOpeRate.Size = new System.Drawing.Size(102, 22);
            this.Tbx_CurOpeRate.TabIndex = 4;
            this.Tbx_CurOpeRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Tbx_CurOpeRate.TextChanged += new System.EventHandler(this.Tbx_CurOpeRate_TextChanged);
            // 
            // Tbx_CurOpeTm
            // 
            this.Tbx_CurOpeTm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Tbx_CurOpeTm.Location = new System.Drawing.Point(242, 496);
            this.Tbx_CurOpeTm.Name = "Tbx_CurOpeTm";
            this.Tbx_CurOpeTm.ReadOnly = true;
            this.Tbx_CurOpeTm.Size = new System.Drawing.Size(102, 22);
            this.Tbx_CurOpeTm.TabIndex = 2;
            this.Tbx_CurOpeTm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Tbx_CurOpeTm.TextChanged += new System.EventHandler(this.Tbx_CurOpeTm_TextChanged);
            // 
            // Lbl_CurOpeAvail
            // 
            this.Lbl_CurOpeAvail.AccessibleRole = System.Windows.Forms.AccessibleRole.Clock;
            this.Lbl_CurOpeAvail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_CurOpeAvail.AutoSize = true;
            this.Lbl_CurOpeAvail.Location = new System.Drawing.Point(519, 499);
            this.Lbl_CurOpeAvail.Name = "Lbl_CurOpeAvail";
            this.Lbl_CurOpeAvail.Size = new System.Drawing.Size(55, 15);
            this.Lbl_CurOpeAvail.TabIndex = 5;
            this.Lbl_CurOpeAvail.Text = "可動率:";
            // 
            // Lbl_CurOpeRate
            // 
            this.Lbl_CurOpeRate.AccessibleRole = System.Windows.Forms.AccessibleRole.Clock;
            this.Lbl_CurOpeRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_CurOpeRate.AutoSize = true;
            this.Lbl_CurOpeRate.Location = new System.Drawing.Point(350, 499);
            this.Lbl_CurOpeRate.Name = "Lbl_CurOpeRate";
            this.Lbl_CurOpeRate.Size = new System.Drawing.Size(55, 15);
            this.Lbl_CurOpeRate.TabIndex = 3;
            this.Lbl_CurOpeRate.Text = "稼働率:";
            // 
            // Lbl_CurOpeTm
            // 
            this.Lbl_CurOpeTm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_CurOpeTm.AutoSize = true;
            this.Lbl_CurOpeTm.Location = new System.Drawing.Point(166, 499);
            this.Lbl_CurOpeTm.Name = "Lbl_CurOpeTm";
            this.Lbl_CurOpeTm.Size = new System.Drawing.Size(70, 15);
            this.Lbl_CurOpeTm.TabIndex = 1;
            this.Lbl_CurOpeTm.Text = "稼働時間:";
            // 
            // Gbx_Sim
            // 
            this.Gbx_Sim.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Gbx_Sim.Controls.Add(this.Dgv_MpSimOdrTbl);
            this.Gbx_Sim.Controls.Add(this.Tbx_SimOpeAvail);
            this.Gbx_Sim.Controls.Add(this.Tbx_SimOpeRate);
            this.Gbx_Sim.Controls.Add(this.Tbx_SimOpeTm);
            this.Gbx_Sim.Controls.Add(this.Lbl_SimOpeAvail);
            this.Gbx_Sim.Controls.Add(this.Lbl_SimOpeRate);
            this.Gbx_Sim.Controls.Add(this.Lbl_SimOpeTm);
            this.Gbx_Sim.Location = new System.Drawing.Point(722, 22);
            this.Gbx_Sim.Name = "Gbx_Sim";
            this.Gbx_Sim.Size = new System.Drawing.Size(700, 542);
            this.Gbx_Sim.TabIndex = 1;
            this.Gbx_Sim.TabStop = false;
            this.Gbx_Sim.Text = "変更後";
            // 
            // Dgv_MpSimOdrTbl
            // 
            this.Dgv_MpSimOdrTbl.AccessibleDescription = "z";
            this.Dgv_MpSimOdrTbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dgv_MpSimOdrTbl.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Dgv_MpSimOdrTbl.ColumnHeadersHeight = 29;
            this.Dgv_MpSimOdrTbl.Location = new System.Drawing.Point(20, 21);
            this.Dgv_MpSimOdrTbl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dgv_MpSimOdrTbl.Name = "Dgv_MpSimOdrTbl";
            this.Dgv_MpSimOdrTbl.RowHeadersWidth = 51;
            this.Dgv_MpSimOdrTbl.RowTemplate.Height = 24;
            this.Dgv_MpSimOdrTbl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_MpSimOdrTbl.Size = new System.Drawing.Size(662, 456);
            this.Dgv_MpSimOdrTbl.TabIndex = 0;
            this.Dgv_MpSimOdrTbl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Dgv_MpSimOdrTbl_KeyUp);
            // 
            // Tbx_SimOpeAvail
            // 
            this.Tbx_SimOpeAvail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Tbx_SimOpeAvail.Location = new System.Drawing.Point(579, 496);
            this.Tbx_SimOpeAvail.Name = "Tbx_SimOpeAvail";
            this.Tbx_SimOpeAvail.ReadOnly = true;
            this.Tbx_SimOpeAvail.Size = new System.Drawing.Size(102, 22);
            this.Tbx_SimOpeAvail.TabIndex = 6;
            this.Tbx_SimOpeAvail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Tbx_SimOpeAvail.TextChanged += new System.EventHandler(this.Tbx_SimOpeAvail_TextChanged);
            // 
            // Tbx_SimOpeRate
            // 
            this.Tbx_SimOpeRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Tbx_SimOpeRate.Location = new System.Drawing.Point(410, 496);
            this.Tbx_SimOpeRate.Name = "Tbx_SimOpeRate";
            this.Tbx_SimOpeRate.ReadOnly = true;
            this.Tbx_SimOpeRate.Size = new System.Drawing.Size(102, 22);
            this.Tbx_SimOpeRate.TabIndex = 4;
            this.Tbx_SimOpeRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Tbx_SimOpeRate.TextChanged += new System.EventHandler(this.Tbx_SimOpeRate_TextChanged);
            // 
            // Tbx_SimOpeTm
            // 
            this.Tbx_SimOpeTm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Tbx_SimOpeTm.Location = new System.Drawing.Point(241, 496);
            this.Tbx_SimOpeTm.Name = "Tbx_SimOpeTm";
            this.Tbx_SimOpeTm.ReadOnly = true;
            this.Tbx_SimOpeTm.Size = new System.Drawing.Size(102, 22);
            this.Tbx_SimOpeTm.TabIndex = 2;
            this.Tbx_SimOpeTm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Tbx_SimOpeTm.TextChanged += new System.EventHandler(this.Tbx_SimOpeTm_TextChanged);
            // 
            // Lbl_SimOpeAvail
            // 
            this.Lbl_SimOpeAvail.AccessibleRole = System.Windows.Forms.AccessibleRole.Clock;
            this.Lbl_SimOpeAvail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_SimOpeAvail.AutoSize = true;
            this.Lbl_SimOpeAvail.Location = new System.Drawing.Point(518, 499);
            this.Lbl_SimOpeAvail.Name = "Lbl_SimOpeAvail";
            this.Lbl_SimOpeAvail.Size = new System.Drawing.Size(55, 15);
            this.Lbl_SimOpeAvail.TabIndex = 5;
            this.Lbl_SimOpeAvail.Text = "可動率:";
            // 
            // Lbl_SimOpeRate
            // 
            this.Lbl_SimOpeRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_SimOpeRate.AutoSize = true;
            this.Lbl_SimOpeRate.Location = new System.Drawing.Point(349, 499);
            this.Lbl_SimOpeRate.Name = "Lbl_SimOpeRate";
            this.Lbl_SimOpeRate.Size = new System.Drawing.Size(55, 15);
            this.Lbl_SimOpeRate.TabIndex = 3;
            this.Lbl_SimOpeRate.Text = "稼働率:";
            // 
            // Lbl_SimOpeTm
            // 
            this.Lbl_SimOpeTm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_SimOpeTm.AutoSize = true;
            this.Lbl_SimOpeTm.Location = new System.Drawing.Point(165, 499);
            this.Lbl_SimOpeTm.Name = "Lbl_SimOpeTm";
            this.Lbl_SimOpeTm.Size = new System.Drawing.Size(70, 15);
            this.Lbl_SimOpeTm.TabIndex = 1;
            this.Lbl_SimOpeTm.Text = "稼働時間:";
            // 
            // Gbx_InqKey
            // 
            this.Gbx_InqKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Gbx_InqKey.Controls.Add(this.Tbx_McSetupTm);
            this.Gbx_InqKey.Controls.Add(this.Dtp_EdDt);
            this.Gbx_InqKey.Controls.Add(this.label3);
            this.Gbx_InqKey.Controls.Add(this.Tbx_McOnTime);
            this.Gbx_InqKey.Controls.Add(this.label2);
            this.Gbx_InqKey.Controls.Add(this.Cbx_McCd);
            this.Gbx_InqKey.Controls.Add(this.Lbl_Hm);
            this.Gbx_InqKey.Controls.Add(this.label4);
            this.Gbx_InqKey.Controls.Add(this.Cbx_McGCd);
            this.Gbx_InqKey.Controls.Add(this.Lbl_WkGr);
            this.Gbx_InqKey.Controls.Add(this.Lbl_EdDt);
            this.Gbx_InqKey.Controls.Add(this.label1);
            this.Gbx_InqKey.Location = new System.Drawing.Point(27, 22);
            this.Gbx_InqKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Gbx_InqKey.Name = "Gbx_InqKey";
            this.Gbx_InqKey.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Gbx_InqKey.Size = new System.Drawing.Size(1429, 109);
            this.Gbx_InqKey.TabIndex = 0;
            this.Gbx_InqKey.TabStop = false;
            this.Gbx_InqKey.Text = "検索キー";
            // 
            // Tbx_McSetupTm
            // 
            this.Tbx_McSetupTm.Location = new System.Drawing.Point(851, 72);
            this.Tbx_McSetupTm.Name = "Tbx_McSetupTm";
            this.Tbx_McSetupTm.ReadOnly = true;
            this.Tbx_McSetupTm.Size = new System.Drawing.Size(135, 22);
            this.Tbx_McSetupTm.TabIndex = 10;
            this.Tbx_McSetupTm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Tbx_McSetupTm.TextChanged += new System.EventHandler(this.Tbx_McSetupTm_TextChanged);
            // 
            // Dtp_EdDt
            // 
            this.Dtp_EdDt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dtp_EdDt.Location = new System.Drawing.Point(151, 19);
            this.Dtp_EdDt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dtp_EdDt.Name = "Dtp_EdDt";
            this.Dtp_EdDt.Size = new System.Drawing.Size(298, 22);
            this.Dtp_EdDt.TabIndex = 1;
            this.Dtp_EdDt.CloseUp += new System.EventHandler(this.Dtp_EdDt_CloseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(992, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "(秒)";
            // 
            // Tbx_McOnTime
            // 
            this.Tbx_McOnTime.Location = new System.Drawing.Point(554, 72);
            this.Tbx_McOnTime.Name = "Tbx_McOnTime";
            this.Tbx_McOnTime.ReadOnly = true;
            this.Tbx_McOnTime.Size = new System.Drawing.Size(135, 22);
            this.Tbx_McOnTime.TabIndex = 7;
            this.Tbx_McOnTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Tbx_McOnTime.TextChanged += new System.EventHandler(this.Tbx_McOnTime_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(766, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "段取り時間:";
            // 
            // Cbx_McCd
            // 
            this.Cbx_McCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Cbx_McCd.FormattingEnabled = true;
            this.Cbx_McCd.Location = new System.Drawing.Point(151, 72);
            this.Cbx_McCd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cbx_McCd.Name = "Cbx_McCd";
            this.Cbx_McCd.Size = new System.Drawing.Size(298, 23);
            this.Cbx_McCd.TabIndex = 5;
            this.Cbx_McCd.TextChanged += new System.EventHandler(this.Cbx_McCd_TextChanged);
            this.Cbx_McCd.LostFocus += new System.EventHandler(this.Cbx_McCd_LostFocus);
            // 
            // Lbl_Hm
            // 
            this.Lbl_Hm.AutoSize = true;
            this.Lbl_Hm.Location = new System.Drawing.Point(105, 75);
            this.Lbl_Hm.Name = "Lbl_Hm";
            this.Lbl_Hm.Size = new System.Drawing.Size(40, 15);
            this.Lbl_Hm.TabIndex = 4;
            this.Lbl_Hm.Text = "設備:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(695, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "(分)";
            // 
            // Cbx_McGCd
            // 
            this.Cbx_McGCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Cbx_McGCd.FormattingEnabled = true;
            this.Cbx_McGCd.Location = new System.Drawing.Point(151, 45);
            this.Cbx_McGCd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cbx_McGCd.Name = "Cbx_McGCd";
            this.Cbx_McGCd.Size = new System.Drawing.Size(298, 23);
            this.Cbx_McGCd.TabIndex = 3;
            this.Cbx_McGCd.TextChanged += new System.EventHandler(this.Cbx_McGCd_TextChanged);
            // 
            // Lbl_WkGr
            // 
            this.Lbl_WkGr.AutoSize = true;
            this.Lbl_WkGr.Location = new System.Drawing.Point(88, 48);
            this.Lbl_WkGr.Name = "Lbl_WkGr";
            this.Lbl_WkGr.Size = new System.Drawing.Size(57, 15);
            this.Lbl_WkGr.TabIndex = 2;
            this.Lbl_WkGr.Text = "グループ:";
            // 
            // Lbl_EdDt
            // 
            this.Lbl_EdDt.AutoSize = true;
            this.Lbl_EdDt.Location = new System.Drawing.Point(75, 25);
            this.Lbl_EdDt.Name = "Lbl_EdDt";
            this.Lbl_EdDt.Size = new System.Drawing.Size(70, 15);
            this.Lbl_EdDt.TabIndex = 0;
            this.Lbl_EdDt.Text = "手配日付:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(478, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "稼働時間:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Tbp_List
            // 
            this.Tbp_List.BackColor = System.Drawing.Color.Transparent;
            this.Tbp_List.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tbp_List.Controls.Add(this.Lbl_BatchEdit);
            this.Tbp_List.Controls.Add(this.Dgv_CycleTmTbl);
            this.Tbp_List.Location = new System.Drawing.Point(4, 25);
            this.Tbp_List.Name = "Tbp_List";
            this.Tbp_List.Padding = new System.Windows.Forms.Padding(3);
            this.Tbp_List.Size = new System.Drawing.Size(1482, 715);
            this.Tbp_List.TabIndex = 1;
            this.Tbp_List.Text = "リスト形式";
            // 
            // Lbl_BatchEdit
            // 
            this.Lbl_BatchEdit.AutoSize = true;
            this.Lbl_BatchEdit.Location = new System.Drawing.Point(27, 22);
            this.Lbl_BatchEdit.Name = "Lbl_BatchEdit";
            this.Lbl_BatchEdit.Size = new System.Drawing.Size(67, 15);
            this.Lbl_BatchEdit.TabIndex = 0;
            this.Lbl_BatchEdit.Text = "一括編集";
            // 
            // Dgv_CycleTmTbl
            // 
            this.Dgv_CycleTmTbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dgv_CycleTmTbl.ColumnHeadersHeight = 29;
            this.Dgv_CycleTmTbl.Location = new System.Drawing.Point(27, 39);
            this.Dgv_CycleTmTbl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dgv_CycleTmTbl.Name = "Dgv_CycleTmTbl";
            this.Dgv_CycleTmTbl.RowHeadersWidth = 51;
            this.Dgv_CycleTmTbl.RowTemplate.Height = 24;
            this.Dgv_CycleTmTbl.Size = new System.Drawing.Size(1097, 580);
            this.Dgv_CycleTmTbl.TabIndex = 1;
            // 
            // Btn_SaveCsvFile
            // 
            this.Btn_SaveCsvFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_SaveCsvFile.Enabled = false;
            this.Btn_SaveCsvFile.Location = new System.Drawing.Point(693, 770);
            this.Btn_SaveCsvFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_SaveCsvFile.Name = "Btn_SaveCsvFile";
            this.Btn_SaveCsvFile.Size = new System.Drawing.Size(100, 30);
            this.Btn_SaveCsvFile.TabIndex = 1;
            this.Btn_SaveCsvFile.Text = "CSV 保存";
            this.Btn_SaveCsvFile.UseVisualStyleBackColor = true;
            // 
            // Btn_ReadCsvFile
            // 
            this.Btn_ReadCsvFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_ReadCsvFile.Location = new System.Drawing.Point(587, 770);
            this.Btn_ReadCsvFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_ReadCsvFile.Name = "Btn_ReadCsvFile";
            this.Btn_ReadCsvFile.Size = new System.Drawing.Size(100, 30);
            this.Btn_ReadCsvFile.TabIndex = 0;
            this.Btn_ReadCsvFile.Text = "CSV 読込";
            this.Btn_ReadCsvFile.UseVisualStyleBackColor = true;
            this.Btn_ReadCsvFile.Click += new System.EventHandler(this.Btn_ReadCsvFile_Click);
            // 
            // Btn_Clear
            // 
            this.Btn_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Clear.Location = new System.Drawing.Point(868, 770);
            this.Btn_Clear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Clear.Name = "Btn_Clear";
            this.Btn_Clear.Size = new System.Drawing.Size(100, 30);
            this.Btn_Clear.TabIndex = 2;
            this.Btn_Clear.Text = "クリア";
            this.Btn_Clear.UseVisualStyleBackColor = true;
            this.Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
            // 
            // Frm042_OrderEqualize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1514, 838);
            this.Controls.Add(this.Btn_Clear);
            this.Controls.Add(this.Btn_SaveCsvFile);
            this.Controls.Add(this.Btn_ReadCsvFile);
            this.Controls.Add(this.Tbc_FormType);
            this.Controls.Add(this.Btn_Search);
            this.Controls.Add(this.Btn_Delete);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_Conf);
            this.Controls.Add(this.Btn_InsUpd);
            this.Controls.Add(this.Sst);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm042_OrderEqualize";
            this.Text = "[KMD001SF] 切削生産計画システム (Ver. XXXXXX.XX) [#042: 切削オーダーの平準化]";
            this.Sst.ResumeLayout(false);
            this.Sst.PerformLayout();
            this.Tbc_FormType.ResumeLayout(false);
            this.Tbp_Slip.ResumeLayout(false);
            this.Gbx_Detail.ResumeLayout(false);
            this.Gbx_Cur.ResumeLayout(false);
            this.Gbx_Cur.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_MpCurOdrTbl)).EndInit();
            this.Gbx_Sim.ResumeLayout(false);
            this.Gbx_Sim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_MpSimOdrTbl)).EndInit();
            this.Gbx_InqKey.ResumeLayout(false);
            this.Gbx_InqKey.PerformLayout();
            this.Tbp_List.ResumeLayout(false);
            this.Tbp_List.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CycleTmTbl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip Sst;
        private System.Windows.Forms.ToolStripStatusLabel Tsl_Msg;
        private System.Windows.Forms.Button Btn_Search;
        private System.Windows.Forms.Button Btn_Delete;
        private System.Windows.Forms.Button Btn_Close;
        private System.Windows.Forms.Button Btn_Conf;
        private System.Windows.Forms.Button Btn_InsUpd;
        private System.Windows.Forms.TabControl Tbc_FormType;
        private System.Windows.Forms.TabPage Tbp_Slip;
        private System.Windows.Forms.TabPage Tbp_List;
        private System.Windows.Forms.GroupBox Gbx_Detail;
        private System.Windows.Forms.GroupBox Gbx_InqKey;
        private System.Windows.Forms.Label Lbl_Hm;
        private System.Windows.Forms.ComboBox Cbx_McGCd;
        private System.Windows.Forms.Label Lbl_WkGr;
        private System.Windows.Forms.DataGridView Dgv_CycleTmTbl;
        private System.Windows.Forms.Label Lbl_BatchEdit;
        private System.Windows.Forms.Button Btn_SaveCsvFile;
        private System.Windows.Forms.Button Btn_ReadCsvFile;
        private System.Windows.Forms.ComboBox Cbx_McCd;
        private System.Windows.Forms.DateTimePicker Dtp_EdDt;
        private System.Windows.Forms.Label Lbl_EdDt;
        private System.Windows.Forms.TextBox Tbx_SimOpeAvail;
        private System.Windows.Forms.TextBox Tbx_SimOpeRate;
        private System.Windows.Forms.TextBox Tbx_SimOpeTm;
        private System.Windows.Forms.TextBox Tbx_CurOpeAvail;
        private System.Windows.Forms.TextBox Tbx_CurOpeRate;
        private System.Windows.Forms.TextBox Tbx_CurOpeTm;
        private System.Windows.Forms.Label Lbl_SimOpeAvail;
        private System.Windows.Forms.Label Lbl_SimOpeRate;
        private System.Windows.Forms.Label Lbl_CurOpeAvail;
        private System.Windows.Forms.Label Lbl_CurOpeRate;
        private System.Windows.Forms.Label Lbl_CurOpeTm;
        private System.Windows.Forms.Label Lbl_SimOpeTm;
        private System.Windows.Forms.GroupBox Gbx_Cur;
        private System.Windows.Forms.GroupBox Gbx_Sim;
        private System.Windows.Forms.Button Btn_Clear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Tbx_McSetupTm;
        private System.Windows.Forms.TextBox Tbx_McOnTime;
        public System.Windows.Forms.DataGridView Dgv_MpSimOdrTbl;
        private System.Windows.Forms.DataGridView Dgv_MpCurOdrTbl;
    }
}