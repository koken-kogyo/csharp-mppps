using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm042_InformationOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm042_InformationOrder));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chk_Like = new System.Windows.Forms.CheckBox();
            this.chk_HMCD = new System.Windows.Forms.CheckBox();
            this.txt_HMCD = new System.Windows.Forms.TextBox();
            this.btn_HMCDPaste = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.pnl_EDDT = new System.Windows.Forms.Panel();
            this.chk_EDDT = new System.Windows.Forms.CheckBox();
            this.cmb_Condition = new System.Windows.Forms.ComboBox();
            this.dtp_EDDT_To = new System.Windows.Forms.DateTimePicker();
            this.dtp_EDDT_From = new System.Windows.Forms.DateTimePicker();
            this.pnl_MCCD = new System.Windows.Forms.Panel();
            this.cmb_MCCD = new System.Windows.Forms.ComboBox();
            this.cmb_MCGCD = new System.Windows.Forms.ComboBox();
            this.chk_MCCD = new System.Windows.Forms.CheckBox();
            this.pnl_HMCD = new System.Windows.Forms.Panel();
            this.txt_ODRNO = new System.Windows.Forms.TextBox();
            this.btn_ODRNOPaste = new System.Windows.Forms.Button();
            this.chk_ODRNO = new System.Windows.Forms.CheckBox();
            this.pnl_ODRSTS = new System.Windows.Forms.Panel();
            this.chk_9 = new System.Windows.Forms.CheckBox();
            this.chk_4 = new System.Windows.Forms.CheckBox();
            this.chk_3 = new System.Windows.Forms.CheckBox();
            this.chk_2 = new System.Windows.Forms.CheckBox();
            this.chk_ODRSTS = new System.Windows.Forms.CheckBox();
            this.chk_1 = new System.Windows.Forms.CheckBox();
            this.dgv_Order = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_ExportOrder = new System.Windows.Forms.Button();
            this.btn_PrintOrder = new System.Windows.Forms.Button();
            this.btn_Progress = new System.Windows.Forms.Button();
            this.btn_DetailOrder = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnl_EDDT.SuspendLayout();
            this.pnl_MCCD.SuspendLayout();
            this.pnl_HMCD.SuspendLayout();
            this.pnl_ODRSTS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Order)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Yu Gothic UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip.Location = new System.Drawing.Point(0, 537);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1306, 29);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Yu Gothic UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(171, 23);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Yu Gothic UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(1120, 23);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btn_Search);
            this.panel1.Controls.Add(this.pnl_EDDT);
            this.panel1.Controls.Add(this.pnl_MCCD);
            this.panel1.Controls.Add(this.pnl_HMCD);
            this.panel1.Controls.Add(this.pnl_ODRSTS);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1306, 96);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chk_Like);
            this.panel2.Controls.Add(this.chk_HMCD);
            this.panel2.Controls.Add(this.txt_HMCD);
            this.panel2.Controls.Add(this.btn_HMCDPaste);
            this.panel2.Location = new System.Drawing.Point(612, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(219, 82);
            this.panel2.TabIndex = 3;
            // 
            // chk_Like
            // 
            this.chk_Like.AutoSize = true;
            this.chk_Like.Checked = true;
            this.chk_Like.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Like.Enabled = false;
            this.chk_Like.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chk_Like.Location = new System.Drawing.Point(76, 4);
            this.chk_Like.Name = "chk_Like";
            this.chk_Like.Size = new System.Drawing.Size(64, 27);
            this.chk_Like.TabIndex = 2;
            this.chk_Like.Text = "LIKE";
            this.chk_Like.UseVisualStyleBackColor = true;
            // 
            // chk_HMCD
            // 
            this.chk_HMCD.AutoSize = true;
            this.chk_HMCD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_HMCD.Location = new System.Drawing.Point(7, 0);
            this.chk_HMCD.Name = "chk_HMCD";
            this.chk_HMCD.Size = new System.Drawing.Size(70, 32);
            this.chk_HMCD.TabIndex = 0;
            this.chk_HMCD.Text = "品番";
            this.chk_HMCD.UseVisualStyleBackColor = true;
            this.chk_HMCD.CheckedChanged += new System.EventHandler(this.chk_HMCD_CheckedChanged);
            // 
            // txt_HMCD
            // 
            this.txt_HMCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_HMCD.Enabled = false;
            this.txt_HMCD.Location = new System.Drawing.Point(7, 35);
            this.txt_HMCD.Name = "txt_HMCD";
            this.txt_HMCD.Size = new System.Drawing.Size(137, 34);
            this.txt_HMCD.TabIndex = 1;
            this.txt_HMCD.TextChanged += new System.EventHandler(this.txt_HMCD_TextChanged);
            // 
            // btn_HMCDPaste
            // 
            this.btn_HMCDPaste.Enabled = false;
            this.btn_HMCDPaste.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_HMCDPaste.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_HMCDPaste.Location = new System.Drawing.Point(150, 34);
            this.btn_HMCDPaste.Name = "btn_HMCDPaste";
            this.btn_HMCDPaste.Size = new System.Drawing.Size(62, 31);
            this.btn_HMCDPaste.TabIndex = 3;
            this.btn_HMCDPaste.Text = "貼り付け";
            this.btn_HMCDPaste.UseVisualStyleBackColor = true;
            this.btn_HMCDPaste.Click += new System.EventHandler(this.btn_HMCDPaste_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_Search.Location = new System.Drawing.Point(8, 7);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(76, 84);
            this.btn_Search.TabIndex = 0;
            this.btn_Search.Text = "検索(F5)";
            this.btn_Search.UseVisualStyleBackColor = false;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // pnl_EDDT
            // 
            this.pnl_EDDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_EDDT.Controls.Add(this.chk_EDDT);
            this.pnl_EDDT.Controls.Add(this.cmb_Condition);
            this.pnl_EDDT.Controls.Add(this.dtp_EDDT_To);
            this.pnl_EDDT.Controls.Add(this.dtp_EDDT_From);
            this.pnl_EDDT.Location = new System.Drawing.Point(90, 8);
            this.pnl_EDDT.Name = "pnl_EDDT";
            this.pnl_EDDT.Size = new System.Drawing.Size(300, 82);
            this.pnl_EDDT.TabIndex = 1;
            // 
            // chk_EDDT
            // 
            this.chk_EDDT.AutoSize = true;
            this.chk_EDDT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_EDDT.Location = new System.Drawing.Point(7, 0);
            this.chk_EDDT.Name = "chk_EDDT";
            this.chk_EDDT.Size = new System.Drawing.Size(130, 32);
            this.chk_EDDT.TabIndex = 0;
            this.chk_EDDT.Text = "完了予定日";
            this.chk_EDDT.UseVisualStyleBackColor = true;
            this.chk_EDDT.CheckedChanged += new System.EventHandler(this.chk_EDDT_CheckedChanged);
            // 
            // cmb_Condition
            // 
            this.cmb_Condition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Condition.FormattingEnabled = true;
            this.cmb_Condition.Items.AddRange(new object[] {
            "＝",
            "～"});
            this.cmb_Condition.Location = new System.Drawing.Point(132, 36);
            this.cmb_Condition.Name = "cmb_Condition";
            this.cmb_Condition.Size = new System.Drawing.Size(38, 36);
            this.cmb_Condition.TabIndex = 2;
            this.cmb_Condition.SelectedIndexChanged += new System.EventHandler(this.cmb_Condition_SelectedIndexChanged);
            // 
            // dtp_EDDT_To
            // 
            this.dtp_EDDT_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_EDDT_To.Location = new System.Drawing.Point(176, 36);
            this.dtp_EDDT_To.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dtp_EDDT_To.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtp_EDDT_To.Name = "dtp_EDDT_To";
            this.dtp_EDDT_To.Size = new System.Drawing.Size(108, 34);
            this.dtp_EDDT_To.TabIndex = 3;
            // 
            // dtp_EDDT_From
            // 
            this.dtp_EDDT_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_EDDT_From.Location = new System.Drawing.Point(18, 36);
            this.dtp_EDDT_From.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dtp_EDDT_From.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtp_EDDT_From.Name = "dtp_EDDT_From";
            this.dtp_EDDT_From.Size = new System.Drawing.Size(108, 34);
            this.dtp_EDDT_From.TabIndex = 1;
            this.dtp_EDDT_From.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtp_EDDT_From_KeyDown);
            // 
            // pnl_MCCD
            // 
            this.pnl_MCCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_MCCD.Controls.Add(this.cmb_MCCD);
            this.pnl_MCCD.Controls.Add(this.cmb_MCGCD);
            this.pnl_MCCD.Controls.Add(this.chk_MCCD);
            this.pnl_MCCD.Location = new System.Drawing.Point(1141, 8);
            this.pnl_MCCD.Name = "pnl_MCCD";
            this.pnl_MCCD.Size = new System.Drawing.Size(199, 82);
            this.pnl_MCCD.TabIndex = 5;
            // 
            // cmb_MCCD
            // 
            this.cmb_MCCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_MCCD.Enabled = false;
            this.cmb_MCCD.FormattingEnabled = true;
            this.cmb_MCCD.Location = new System.Drawing.Point(97, 34);
            this.cmb_MCCD.Name = "cmb_MCCD";
            this.cmb_MCCD.Size = new System.Drawing.Size(81, 36);
            this.cmb_MCCD.TabIndex = 2;
            this.cmb_MCCD.SelectedIndexChanged += new System.EventHandler(this.cmb_MCCD_SelectedIndexChanged);
            // 
            // cmb_MCGCD
            // 
            this.cmb_MCGCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_MCGCD.Enabled = false;
            this.cmb_MCGCD.FormattingEnabled = true;
            this.cmb_MCGCD.Location = new System.Drawing.Point(19, 34);
            this.cmb_MCGCD.Name = "cmb_MCGCD";
            this.cmb_MCGCD.Size = new System.Drawing.Size(72, 36);
            this.cmb_MCGCD.TabIndex = 1;
            this.cmb_MCGCD.SelectedIndexChanged += new System.EventHandler(this.cmb_MCGCD_SelectedIndexChanged);
            // 
            // chk_MCCD
            // 
            this.chk_MCCD.AutoSize = true;
            this.chk_MCCD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_MCCD.Location = new System.Drawing.Point(6, 0);
            this.chk_MCCD.Name = "chk_MCCD";
            this.chk_MCCD.Size = new System.Drawing.Size(70, 32);
            this.chk_MCCD.TabIndex = 0;
            this.chk_MCCD.Text = "設備";
            this.chk_MCCD.UseVisualStyleBackColor = true;
            this.chk_MCCD.CheckedChanged += new System.EventHandler(this.chk_MCCD_CheckedChanged);
            // 
            // pnl_HMCD
            // 
            this.pnl_HMCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_HMCD.Controls.Add(this.txt_ODRNO);
            this.pnl_HMCD.Controls.Add(this.btn_ODRNOPaste);
            this.pnl_HMCD.Controls.Add(this.chk_ODRNO);
            this.pnl_HMCD.Location = new System.Drawing.Point(398, 8);
            this.pnl_HMCD.Name = "pnl_HMCD";
            this.pnl_HMCD.Size = new System.Drawing.Size(206, 82);
            this.pnl_HMCD.TabIndex = 2;
            // 
            // txt_ODRNO
            // 
            this.txt_ODRNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ODRNO.Enabled = false;
            this.txt_ODRNO.Location = new System.Drawing.Point(7, 37);
            this.txt_ODRNO.Name = "txt_ODRNO";
            this.txt_ODRNO.Size = new System.Drawing.Size(121, 34);
            this.txt_ODRNO.TabIndex = 1;
            this.txt_ODRNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_ODRNO_KeyDown);
            // 
            // btn_ODRNOPaste
            // 
            this.btn_ODRNOPaste.Enabled = false;
            this.btn_ODRNOPaste.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_ODRNOPaste.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_ODRNOPaste.Location = new System.Drawing.Point(134, 35);
            this.btn_ODRNOPaste.Name = "btn_ODRNOPaste";
            this.btn_ODRNOPaste.Size = new System.Drawing.Size(61, 31);
            this.btn_ODRNOPaste.TabIndex = 2;
            this.btn_ODRNOPaste.Text = "貼り付け";
            this.btn_ODRNOPaste.UseVisualStyleBackColor = true;
            this.btn_ODRNOPaste.Click += new System.EventHandler(this.btn_ODRNOPaste_Click);
            // 
            // chk_ODRNO
            // 
            this.chk_ODRNO.AutoSize = true;
            this.chk_ODRNO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_ODRNO.Location = new System.Drawing.Point(7, 0);
            this.chk_ODRNO.Name = "chk_ODRNO";
            this.chk_ODRNO.Size = new System.Drawing.Size(97, 32);
            this.chk_ODRNO.TabIndex = 0;
            this.chk_ODRNO.Text = "手配No";
            this.chk_ODRNO.UseVisualStyleBackColor = true;
            this.chk_ODRNO.CheckedChanged += new System.EventHandler(this.chk_ODRNO_CheckedChanged);
            // 
            // pnl_ODRSTS
            // 
            this.pnl_ODRSTS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_ODRSTS.Controls.Add(this.chk_9);
            this.pnl_ODRSTS.Controls.Add(this.chk_4);
            this.pnl_ODRSTS.Controls.Add(this.chk_3);
            this.pnl_ODRSTS.Controls.Add(this.chk_2);
            this.pnl_ODRSTS.Controls.Add(this.chk_ODRSTS);
            this.pnl_ODRSTS.Controls.Add(this.chk_1);
            this.pnl_ODRSTS.Location = new System.Drawing.Point(838, 8);
            this.pnl_ODRSTS.Name = "pnl_ODRSTS";
            this.pnl_ODRSTS.Size = new System.Drawing.Size(297, 82);
            this.pnl_ODRSTS.TabIndex = 4;
            // 
            // chk_9
            // 
            this.chk_9.AutoSize = true;
            this.chk_9.Enabled = false;
            this.chk_9.Location = new System.Drawing.Point(221, 43);
            this.chk_9.Name = "chk_9";
            this.chk_9.Size = new System.Drawing.Size(74, 32);
            this.chk_9.TabIndex = 5;
            this.chk_9.Text = "中止";
            this.chk_9.UseVisualStyleBackColor = true;
            this.chk_9.CheckedChanged += new System.EventHandler(this.chk_9_CheckedChanged);
            // 
            // chk_4
            // 
            this.chk_4.AutoSize = true;
            this.chk_4.Enabled = false;
            this.chk_4.Location = new System.Drawing.Point(221, 18);
            this.chk_4.Name = "chk_4";
            this.chk_4.Size = new System.Drawing.Size(74, 32);
            this.chk_4.TabIndex = 4;
            this.chk_4.Text = "完了";
            this.chk_4.UseVisualStyleBackColor = true;
            this.chk_4.CheckedChanged += new System.EventHandler(this.chk_4_CheckedChanged);
            // 
            // chk_3
            // 
            this.chk_3.AutoSize = true;
            this.chk_3.Enabled = false;
            this.chk_3.Location = new System.Drawing.Point(148, 36);
            this.chk_3.Name = "chk_3";
            this.chk_3.Size = new System.Drawing.Size(74, 32);
            this.chk_3.TabIndex = 3;
            this.chk_3.Text = "着手";
            this.chk_3.UseVisualStyleBackColor = true;
            this.chk_3.CheckedChanged += new System.EventHandler(this.chk_3_CheckedChanged);
            // 
            // chk_2
            // 
            this.chk_2.AutoSize = true;
            this.chk_2.Enabled = false;
            this.chk_2.Location = new System.Drawing.Point(78, 36);
            this.chk_2.Name = "chk_2";
            this.chk_2.Size = new System.Drawing.Size(74, 32);
            this.chk_2.TabIndex = 2;
            this.chk_2.Text = "確定";
            this.chk_2.UseVisualStyleBackColor = true;
            this.chk_2.CheckedChanged += new System.EventHandler(this.chk_2_CheckedChanged);
            // 
            // chk_ODRSTS
            // 
            this.chk_ODRSTS.AutoSize = true;
            this.chk_ODRSTS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_ODRSTS.Location = new System.Drawing.Point(7, 0);
            this.chk_ODRSTS.Name = "chk_ODRSTS";
            this.chk_ODRSTS.Size = new System.Drawing.Size(110, 32);
            this.chk_ODRSTS.TabIndex = 0;
            this.chk_ODRSTS.Text = "手配状態";
            this.chk_ODRSTS.UseVisualStyleBackColor = true;
            this.chk_ODRSTS.CheckedChanged += new System.EventHandler(this.chk_ODRSTS_CheckedChanged);
            // 
            // chk_1
            // 
            this.chk_1.AutoSize = true;
            this.chk_1.Enabled = false;
            this.chk_1.Location = new System.Drawing.Point(7, 36);
            this.chk_1.Name = "chk_1";
            this.chk_1.Size = new System.Drawing.Size(74, 32);
            this.chk_1.TabIndex = 1;
            this.chk_1.Text = "追加";
            this.chk_1.UseVisualStyleBackColor = true;
            this.chk_1.CheckedChanged += new System.EventHandler(this.chk_1_CheckedChanged);
            // 
            // dgv_Order
            // 
            this.dgv_Order.AllowUserToAddRows = false;
            this.dgv_Order.AllowUserToDeleteRows = false;
            this.dgv_Order.AllowUserToResizeRows = false;
            this.dgv_Order.ColumnHeadersHeight = 50;
            this.dgv_Order.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_Order.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Order.Location = new System.Drawing.Point(0, 96);
            this.dgv_Order.Name = "dgv_Order";
            this.dgv_Order.RowHeadersWidth = 51;
            this.dgv_Order.RowTemplate.Height = 24;
            this.dgv_Order.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Order.Size = new System.Drawing.Size(1306, 397);
            this.dgv_Order.TabIndex = 6;
            this.dgv_Order.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Order_CellDoubleClick);
            this.dgv_Order.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_Order_CellMouseDown);
            this.dgv_Order.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Dgv_CodeSlipMst_RowPostPaint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btn_ExportOrder, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_PrintOrder, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Progress, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_DetailOrder, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 493);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1306, 44);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // btn_ExportOrder
            // 
            this.btn_ExportOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_ExportOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ExportOrder.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_ExportOrder.Location = new System.Drawing.Point(980, 3);
            this.btn_ExportOrder.Name = "btn_ExportOrder";
            this.btn_ExportOrder.Size = new System.Drawing.Size(318, 38);
            this.btn_ExportOrder.TabIndex = 10;
            this.btn_ExportOrder.Text = "外部出力(F10)";
            this.btn_ExportOrder.UseVisualStyleBackColor = false;
            this.btn_ExportOrder.Click += new System.EventHandler(this.btn_ExportOrder_Click);
            // 
            // btn_PrintOrder
            // 
            this.btn_PrintOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_PrintOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PrintOrder.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_PrintOrder.Location = new System.Drawing.Point(656, 3);
            this.btn_PrintOrder.Name = "btn_PrintOrder";
            this.btn_PrintOrder.Size = new System.Drawing.Size(318, 38);
            this.btn_PrintOrder.TabIndex = 9;
            this.btn_PrintOrder.Text = "製造指示カード発行(F12)";
            this.btn_PrintOrder.UseVisualStyleBackColor = false;
            this.btn_PrintOrder.Click += new System.EventHandler(this.btn_PrintOrder_Click);
            // 
            // btn_Progress
            // 
            this.btn_Progress.BackColor = System.Drawing.Color.MistyRose;
            this.btn_Progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Progress.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_Progress.Location = new System.Drawing.Point(332, 3);
            this.btn_Progress.Name = "btn_Progress";
            this.btn_Progress.Size = new System.Drawing.Size(318, 38);
            this.btn_Progress.TabIndex = 8;
            this.btn_Progress.Text = "進度盤";
            this.btn_Progress.UseVisualStyleBackColor = false;
            this.btn_Progress.Click += new System.EventHandler(this.btn_Progress_Click);
            // 
            // btn_DetailOrder
            // 
            this.btn_DetailOrder.BackColor = System.Drawing.Color.MistyRose;
            this.btn_DetailOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_DetailOrder.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_DetailOrder.Location = new System.Drawing.Point(8, 3);
            this.btn_DetailOrder.Name = "btn_DetailOrder";
            this.btn_DetailOrder.Size = new System.Drawing.Size(318, 38);
            this.btn_DetailOrder.TabIndex = 7;
            this.btn_DetailOrder.Text = "詳細";
            this.btn_DetailOrder.UseVisualStyleBackColor = false;
            this.btn_DetailOrder.Click += new System.EventHandler(this.btn_DetailOrder_Click);
            // 
            // Frm042_InformationOrder
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1306, 566);
            this.Controls.Add(this.dgv_Order);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm042_InformationOrder";
            this.Text = "[KMD004SF] オーダー管理 - 手配情報 - Ver.250215";
            this.Shown += new System.EventHandler(this.Frm042_InformationOrder_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm042_InformationOrder_KeyDown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnl_EDDT.ResumeLayout(false);
            this.pnl_EDDT.PerformLayout();
            this.pnl_MCCD.ResumeLayout(false);
            this.pnl_MCCD.PerformLayout();
            this.pnl_HMCD.ResumeLayout(false);
            this.pnl_HMCD.PerformLayout();
            this.pnl_ODRSTS.ResumeLayout(false);
            this.pnl_ODRSTS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Order)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel panel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private Panel pnl_ODRSTS;
        private CheckBox chk_2;
        private Panel pnl_MCCD;
        private Panel pnl_HMCD;
        private TextBox txt_ODRNO;
        private CheckBox chk_4;
        private CheckBox chk_3;
        private Panel pnl_EDDT;
        private DateTimePicker dtp_EDDT_To;
        private DateTimePicker dtp_EDDT_From;
        private ComboBox cmb_MCCD;
        private ComboBox cmb_MCGCD;
        private ComboBox cmb_Condition;
        private CheckBox chk_9;
        private Button btn_Search;
        private DataGridView dgv_Order;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btn_ExportOrder;
        private Button btn_PrintOrder;
        private Button btn_Progress;
        private Button btn_DetailOrder;
        private CheckBox chk_ODRSTS;
        private CheckBox chk_MCCD;
        private Button btn_ODRNOPaste;
        private CheckBox chk_ODRNO;
        private Panel panel2;
        private CheckBox chk_HMCD;
        private TextBox txt_HMCD;
        private Button btn_HMCDPaste;
        private CheckBox chk_EDDT;
        private CheckBox chk_Like;
        private CheckBox chk_1;
    }
}