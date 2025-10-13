using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm045_InformationPlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm045_InformationPlan));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl_HMCD = new System.Windows.Forms.Panel();
            this.chk_HMCD = new System.Windows.Forms.CheckBox();
            this.chk_Like = new System.Windows.Forms.CheckBox();
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
            this.pnl_PLNNO = new System.Windows.Forms.Panel();
            this.txt_PLNNO = new System.Windows.Forms.TextBox();
            this.btn_PLNNOPaste = new System.Windows.Forms.Button();
            this.chk_PLNNO = new System.Windows.Forms.CheckBox();
            this.pnl_ODRSTS = new System.Windows.Forms.Panel();
            this.chk_9 = new System.Windows.Forms.CheckBox();
            this.chk_4 = new System.Windows.Forms.CheckBox();
            this.chk_3 = new System.Windows.Forms.CheckBox();
            this.chk_2 = new System.Windows.Forms.CheckBox();
            this.chk_ODRSTS = new System.Windows.Forms.CheckBox();
            this.chk_1 = new System.Windows.Forms.CheckBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_ExportPlan = new System.Windows.Forms.Button();
            this.btn_PrintPlan = new System.Windows.Forms.Button();
            this.btn_Progress = new System.Windows.Forms.Button();
            this.btn_DetailPlan = new System.Windows.Forms.Button();
            this.dgv_Order = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.pnl_HMCD.SuspendLayout();
            this.pnl_EDDT.SuspendLayout();
            this.pnl_MCCD.SuspendLayout();
            this.pnl_PLNNO.SuspendLayout();
            this.pnl_ODRSTS.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Order)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnl_HMCD);
            this.panel1.Controls.Add(this.btn_Search);
            this.panel1.Controls.Add(this.pnl_EDDT);
            this.panel1.Controls.Add(this.pnl_MCCD);
            this.panel1.Controls.Add(this.pnl_PLNNO);
            this.panel1.Controls.Add(this.pnl_ODRSTS);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1925, 99);
            this.panel1.TabIndex = 9;
            // 
            // pnl_HMCD
            // 
            this.pnl_HMCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_HMCD.Controls.Add(this.chk_HMCD);
            this.pnl_HMCD.Controls.Add(this.chk_Like);
            this.pnl_HMCD.Controls.Add(this.txt_HMCD);
            this.pnl_HMCD.Controls.Add(this.btn_HMCDPaste);
            this.pnl_HMCD.Location = new System.Drawing.Point(746, 7);
            this.pnl_HMCD.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.pnl_HMCD.Name = "pnl_HMCD";
            this.pnl_HMCD.Size = new System.Drawing.Size(294, 84);
            this.pnl_HMCD.TabIndex = 3;
            // 
            // chk_HMCD
            // 
            this.chk_HMCD.Checked = true;
            this.chk_HMCD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_HMCD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_HMCD.Location = new System.Drawing.Point(12, 2);
            this.chk_HMCD.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.chk_HMCD.Name = "chk_HMCD";
            this.chk_HMCD.Size = new System.Drawing.Size(95, 32);
            this.chk_HMCD.TabIndex = 0;
            this.chk_HMCD.Text = "品番";
            this.chk_HMCD.UseVisualStyleBackColor = true;
            this.chk_HMCD.CheckedChanged += new System.EventHandler(this.chk_HMCD_CheckedChanged);
            // 
            // chk_Like
            // 
            this.chk_Like.AutoSize = true;
            this.chk_Like.Checked = true;
            this.chk_Like.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Like.Enabled = false;
            this.chk_Like.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chk_Like.Location = new System.Drawing.Point(119, 5);
            this.chk_Like.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.chk_Like.Name = "chk_Like";
            this.chk_Like.Size = new System.Drawing.Size(64, 27);
            this.chk_Like.TabIndex = 2;
            this.chk_Like.Text = "LIKE";
            this.chk_Like.UseVisualStyleBackColor = true;
            // 
            // txt_HMCD
            // 
            this.txt_HMCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_HMCD.Enabled = false;
            this.txt_HMCD.Location = new System.Drawing.Point(25, 39);
            this.txt_HMCD.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txt_HMCD.Name = "txt_HMCD";
            this.txt_HMCD.Size = new System.Drawing.Size(160, 34);
            this.txt_HMCD.TabIndex = 1;
            this.txt_HMCD.TextChanged += new System.EventHandler(this.txt_HMCD_TextChanged);
            // 
            // btn_HMCDPaste
            // 
            this.btn_HMCDPaste.Enabled = false;
            this.btn_HMCDPaste.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_HMCDPaste.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_HMCDPaste.Location = new System.Drawing.Point(197, 39);
            this.btn_HMCDPaste.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btn_HMCDPaste.Name = "btn_HMCDPaste";
            this.btn_HMCDPaste.Size = new System.Drawing.Size(82, 34);
            this.btn_HMCDPaste.TabIndex = 3;
            this.btn_HMCDPaste.Text = "貼り付け";
            this.btn_HMCDPaste.UseVisualStyleBackColor = true;
            this.btn_HMCDPaste.Click += new System.EventHandler(this.btn_HMCDPaste_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_Search.Location = new System.Drawing.Point(6, 7);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
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
            this.pnl_EDDT.Location = new System.Drawing.Point(94, 7);
            this.pnl_EDDT.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.pnl_EDDT.Name = "pnl_EDDT";
            this.pnl_EDDT.Size = new System.Drawing.Size(375, 84);
            this.pnl_EDDT.TabIndex = 1;
            // 
            // chk_EDDT
            // 
            this.chk_EDDT.Checked = true;
            this.chk_EDDT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_EDDT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_EDDT.Location = new System.Drawing.Point(12, 2);
            this.chk_EDDT.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.chk_EDDT.Name = "chk_EDDT";
            this.chk_EDDT.Size = new System.Drawing.Size(168, 32);
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
            this.cmb_Condition.Location = new System.Drawing.Point(165, 38);
            this.cmb_Condition.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.cmb_Condition.Name = "cmb_Condition";
            this.cmb_Condition.Size = new System.Drawing.Size(47, 36);
            this.cmb_Condition.TabIndex = 2;
            this.cmb_Condition.SelectedIndexChanged += new System.EventHandler(this.cmb_Condition_SelectedIndexChanged);
            // 
            // dtp_EDDT_To
            // 
            this.dtp_EDDT_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_EDDT_To.Location = new System.Drawing.Point(224, 39);
            this.dtp_EDDT_To.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.dtp_EDDT_To.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dtp_EDDT_To.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtp_EDDT_To.Name = "dtp_EDDT_To";
            this.dtp_EDDT_To.Size = new System.Drawing.Size(137, 34);
            this.dtp_EDDT_To.TabIndex = 3;
            // 
            // dtp_EDDT_From
            // 
            this.dtp_EDDT_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_EDDT_From.Location = new System.Drawing.Point(21, 38);
            this.dtp_EDDT_From.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.dtp_EDDT_From.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dtp_EDDT_From.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtp_EDDT_From.Name = "dtp_EDDT_From";
            this.dtp_EDDT_From.Size = new System.Drawing.Size(132, 34);
            this.dtp_EDDT_From.TabIndex = 1;
            this.dtp_EDDT_From.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtp_EDDT_From_KeyDown);
            // 
            // pnl_MCCD
            // 
            this.pnl_MCCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_MCCD.Controls.Add(this.cmb_MCCD);
            this.pnl_MCCD.Controls.Add(this.cmb_MCGCD);
            this.pnl_MCCD.Controls.Add(this.chk_MCCD);
            this.pnl_MCCD.Location = new System.Drawing.Point(1411, 7);
            this.pnl_MCCD.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.pnl_MCCD.Name = "pnl_MCCD";
            this.pnl_MCCD.Size = new System.Drawing.Size(224, 84);
            this.pnl_MCCD.TabIndex = 5;
            // 
            // cmb_MCCD
            // 
            this.cmb_MCCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_MCCD.Enabled = false;
            this.cmb_MCCD.FormattingEnabled = true;
            this.cmb_MCCD.Location = new System.Drawing.Point(119, 41);
            this.cmb_MCCD.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.cmb_MCCD.Name = "cmb_MCCD";
            this.cmb_MCCD.Size = new System.Drawing.Size(88, 36);
            this.cmb_MCCD.TabIndex = 2;
            this.cmb_MCCD.SelectedIndexChanged += new System.EventHandler(this.cmb_MCCD_SelectedIndexChanged);
            // 
            // cmb_MCGCD
            // 
            this.cmb_MCGCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_MCGCD.Enabled = false;
            this.cmb_MCGCD.FormattingEnabled = true;
            this.cmb_MCGCD.Location = new System.Drawing.Point(22, 41);
            this.cmb_MCGCD.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.cmb_MCGCD.Name = "cmb_MCGCD";
            this.cmb_MCGCD.Size = new System.Drawing.Size(85, 36);
            this.cmb_MCGCD.TabIndex = 1;
            this.cmb_MCGCD.SelectedIndexChanged += new System.EventHandler(this.cmb_MCGCD_SelectedIndexChanged);
            // 
            // chk_MCCD
            // 
            this.chk_MCCD.Checked = true;
            this.chk_MCCD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_MCCD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_MCCD.Location = new System.Drawing.Point(11, 2);
            this.chk_MCCD.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.chk_MCCD.Name = "chk_MCCD";
            this.chk_MCCD.Size = new System.Drawing.Size(96, 32);
            this.chk_MCCD.TabIndex = 0;
            this.chk_MCCD.Text = "設備";
            this.chk_MCCD.UseVisualStyleBackColor = true;
            this.chk_MCCD.CheckedChanged += new System.EventHandler(this.chk_MCCD_CheckedChanged);
            // 
            // pnl_PLNNO
            // 
            this.pnl_PLNNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_PLNNO.Controls.Add(this.txt_PLNNO);
            this.pnl_PLNNO.Controls.Add(this.btn_PLNNOPaste);
            this.pnl_PLNNO.Controls.Add(this.chk_PLNNO);
            this.pnl_PLNNO.Location = new System.Drawing.Point(481, 7);
            this.pnl_PLNNO.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.pnl_PLNNO.Name = "pnl_PLNNO";
            this.pnl_PLNNO.Size = new System.Drawing.Size(253, 84);
            this.pnl_PLNNO.TabIndex = 2;
            // 
            // txt_PLNNO
            // 
            this.txt_PLNNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_PLNNO.Enabled = false;
            this.txt_PLNNO.Location = new System.Drawing.Point(22, 40);
            this.txt_PLNNO.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txt_PLNNO.Name = "txt_PLNNO";
            this.txt_PLNNO.Size = new System.Drawing.Size(132, 34);
            this.txt_PLNNO.TabIndex = 1;
            this.txt_PLNNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_PLNNO_KeyDown);
            // 
            // btn_PLNNOPaste
            // 
            this.btn_PLNNOPaste.Enabled = false;
            this.btn_PLNNOPaste.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_PLNNOPaste.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_PLNNOPaste.Location = new System.Drawing.Point(166, 39);
            this.btn_PLNNOPaste.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btn_PLNNOPaste.Name = "btn_PLNNOPaste";
            this.btn_PLNNOPaste.Size = new System.Drawing.Size(75, 36);
            this.btn_PLNNOPaste.TabIndex = 2;
            this.btn_PLNNOPaste.Text = "貼り付け";
            this.btn_PLNNOPaste.UseVisualStyleBackColor = true;
            this.btn_PLNNOPaste.Click += new System.EventHandler(this.btn_PLNNOPaste_Click);
            // 
            // chk_PLNNO
            // 
            this.chk_PLNNO.Checked = true;
            this.chk_PLNNO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_PLNNO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_PLNNO.Location = new System.Drawing.Point(12, 2);
            this.chk_PLNNO.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.chk_PLNNO.Name = "chk_PLNNO";
            this.chk_PLNNO.Size = new System.Drawing.Size(133, 32);
            this.chk_PLNNO.TabIndex = 0;
            this.chk_PLNNO.Text = "計画No";
            this.chk_PLNNO.UseVisualStyleBackColor = true;
            this.chk_PLNNO.CheckedChanged += new System.EventHandler(this.chk_PLNNO_CheckedChanged);
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
            this.pnl_ODRSTS.Location = new System.Drawing.Point(1052, 7);
            this.pnl_ODRSTS.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.pnl_ODRSTS.Name = "pnl_ODRSTS";
            this.pnl_ODRSTS.Size = new System.Drawing.Size(347, 84);
            this.pnl_ODRSTS.TabIndex = 4;
            // 
            // chk_9
            // 
            this.chk_9.AutoSize = true;
            this.chk_9.Enabled = false;
            this.chk_9.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.chk_9.Location = new System.Drawing.Point(262, 43);
            this.chk_9.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.chk_9.Name = "chk_9";
            this.chk_9.Size = new System.Drawing.Size(66, 27);
            this.chk_9.TabIndex = 5;
            this.chk_9.Text = "中止";
            this.chk_9.UseVisualStyleBackColor = true;
            this.chk_9.CheckedChanged += new System.EventHandler(this.chk_9_CheckedChanged);
            // 
            // chk_4
            // 
            this.chk_4.AutoSize = true;
            this.chk_4.Enabled = false;
            this.chk_4.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.chk_4.Location = new System.Drawing.Point(262, 10);
            this.chk_4.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.chk_4.Name = "chk_4";
            this.chk_4.Size = new System.Drawing.Size(66, 27);
            this.chk_4.TabIndex = 4;
            this.chk_4.Text = "完了";
            this.chk_4.UseVisualStyleBackColor = true;
            this.chk_4.CheckedChanged += new System.EventHandler(this.chk_4_CheckedChanged);
            // 
            // chk_3
            // 
            this.chk_3.AutoSize = true;
            this.chk_3.Enabled = false;
            this.chk_3.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.chk_3.Location = new System.Drawing.Point(184, 43);
            this.chk_3.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.chk_3.Name = "chk_3";
            this.chk_3.Size = new System.Drawing.Size(66, 27);
            this.chk_3.TabIndex = 3;
            this.chk_3.Text = "着手";
            this.chk_3.UseVisualStyleBackColor = true;
            this.chk_3.CheckedChanged += new System.EventHandler(this.chk_3_CheckedChanged);
            // 
            // chk_2
            // 
            this.chk_2.AutoSize = true;
            this.chk_2.Enabled = false;
            this.chk_2.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.chk_2.Location = new System.Drawing.Point(106, 43);
            this.chk_2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.chk_2.Name = "chk_2";
            this.chk_2.Size = new System.Drawing.Size(66, 27);
            this.chk_2.TabIndex = 2;
            this.chk_2.Text = "確定";
            this.chk_2.UseVisualStyleBackColor = true;
            this.chk_2.CheckedChanged += new System.EventHandler(this.chk_2_CheckedChanged);
            // 
            // chk_ODRSTS
            // 
            this.chk_ODRSTS.Checked = true;
            this.chk_ODRSTS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ODRSTS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_ODRSTS.Location = new System.Drawing.Point(12, 2);
            this.chk_ODRSTS.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.chk_ODRSTS.Name = "chk_ODRSTS";
            this.chk_ODRSTS.Size = new System.Drawing.Size(151, 32);
            this.chk_ODRSTS.TabIndex = 0;
            this.chk_ODRSTS.Text = "手配状態";
            this.chk_ODRSTS.UseVisualStyleBackColor = true;
            this.chk_ODRSTS.CheckedChanged += new System.EventHandler(this.chk_ODRSTS_CheckedChanged);
            // 
            // chk_1
            // 
            this.chk_1.AutoSize = true;
            this.chk_1.Enabled = false;
            this.chk_1.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.chk_1.Location = new System.Drawing.Point(28, 43);
            this.chk_1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.chk_1.Name = "chk_1";
            this.chk_1.Size = new System.Drawing.Size(66, 27);
            this.chk_1.TabIndex = 1;
            this.chk_1.Text = "追加";
            this.chk_1.UseVisualStyleBackColor = true;
            this.chk_1.CheckedChanged += new System.EventHandler(this.chk_1_CheckedChanged);
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
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 26, 0);
            this.statusStrip.Size = new System.Drawing.Size(1925, 29);
            this.statusStrip.TabIndex = 14;
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
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(1727, 23);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btn_ExportPlan, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_PrintPlan, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Progress, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_DetailPlan, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 493);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1925, 44);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // btn_ExportPlan
            // 
            this.btn_ExportPlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_ExportPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ExportPlan.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_ExportPlan.Location = new System.Drawing.Point(1441, 3);
            this.btn_ExportPlan.Name = "btn_ExportPlan";
            this.btn_ExportPlan.Size = new System.Drawing.Size(471, 38);
            this.btn_ExportPlan.TabIndex = 10;
            this.btn_ExportPlan.Text = "外部出力(F10)";
            this.btn_ExportPlan.UseVisualStyleBackColor = false;
            this.btn_ExportPlan.Click += new System.EventHandler(this.btn_ExportPlan_Click);
            // 
            // btn_PrintPlan
            // 
            this.btn_PrintPlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_PrintPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PrintPlan.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_PrintPlan.Location = new System.Drawing.Point(965, 3);
            this.btn_PrintPlan.Name = "btn_PrintPlan";
            this.btn_PrintPlan.Size = new System.Drawing.Size(470, 38);
            this.btn_PrintPlan.TabIndex = 9;
            this.btn_PrintPlan.Text = "内示カード発行(F12)";
            this.btn_PrintPlan.UseVisualStyleBackColor = false;
            this.btn_PrintPlan.Click += new System.EventHandler(this.btn_PrintPlan_Click);
            // 
            // btn_Progress
            // 
            this.btn_Progress.BackColor = System.Drawing.Color.MistyRose;
            this.btn_Progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Progress.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_Progress.Location = new System.Drawing.Point(489, 3);
            this.btn_Progress.Name = "btn_Progress";
            this.btn_Progress.Size = new System.Drawing.Size(470, 38);
            this.btn_Progress.TabIndex = 8;
            this.btn_Progress.Text = "進度盤";
            this.btn_Progress.UseVisualStyleBackColor = false;
            this.btn_Progress.Click += new System.EventHandler(this.btn_Progress_Click);
            // 
            // btn_DetailPlan
            // 
            this.btn_DetailPlan.BackColor = System.Drawing.Color.MistyRose;
            this.btn_DetailPlan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_DetailPlan.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_DetailPlan.Location = new System.Drawing.Point(13, 3);
            this.btn_DetailPlan.Name = "btn_DetailPlan";
            this.btn_DetailPlan.Size = new System.Drawing.Size(470, 38);
            this.btn_DetailPlan.TabIndex = 7;
            this.btn_DetailPlan.Text = "詳細";
            this.btn_DetailPlan.UseVisualStyleBackColor = false;
            this.btn_DetailPlan.Click += new System.EventHandler(this.btn_DetailPlan_Click);
            // 
            // dgv_Order
            // 
            this.dgv_Order.AllowUserToAddRows = false;
            this.dgv_Order.AllowUserToDeleteRows = false;
            this.dgv_Order.AllowUserToResizeRows = false;
            this.dgv_Order.ColumnHeadersHeight = 50;
            this.dgv_Order.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_Order.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Order.Location = new System.Drawing.Point(0, 99);
            this.dgv_Order.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.dgv_Order.Name = "dgv_Order";
            this.dgv_Order.RowHeadersWidth = 51;
            this.dgv_Order.RowTemplate.Height = 24;
            this.dgv_Order.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Order.Size = new System.Drawing.Size(1925, 394);
            this.dgv_Order.TabIndex = 16;
            this.dgv_Order.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Order_CellDoubleClick);
            this.dgv_Order.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_Order_CellMouseDown);
            this.dgv_Order.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Dgv_CodeSlipMst_RowPostPaint);
            // 
            // Frm045_InformationPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1925, 566);
            this.Controls.Add(this.dgv_Order);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm045_InformationPlan";
            this.Text = "[KMD004SF] オーダー管理 - 内示情報 - Ver.230613.01a";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm042_InformationOrder_KeyDown);
            this.panel1.ResumeLayout(false);
            this.pnl_HMCD.ResumeLayout(false);
            this.pnl_HMCD.PerformLayout();
            this.pnl_EDDT.ResumeLayout(false);
            this.pnl_MCCD.ResumeLayout(false);
            this.pnl_PLNNO.ResumeLayout(false);
            this.pnl_PLNNO.PerformLayout();
            this.pnl_ODRSTS.ResumeLayout(false);
            this.pnl_ODRSTS.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Order)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Panel pnl_HMCD;
        private CheckBox chk_Like;
        private CheckBox chk_HMCD;
        private TextBox txt_HMCD;
        private Button btn_HMCDPaste;
        private Button btn_Search;
        private Panel pnl_EDDT;
        private CheckBox chk_EDDT;
        private ComboBox cmb_Condition;
        private DateTimePicker dtp_EDDT_To;
        private DateTimePicker dtp_EDDT_From;
        private Panel pnl_MCCD;
        private ComboBox cmb_MCCD;
        private ComboBox cmb_MCGCD;
        private CheckBox chk_MCCD;
        private Panel pnl_PLNNO;
        private TextBox txt_PLNNO;
        private Button btn_PLNNOPaste;
        private CheckBox chk_PLNNO;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btn_ExportPlan;
        private Button btn_PrintPlan;
        private Button btn_Progress;
        private Button btn_DetailPlan;
        private DataGridView dgv_Order;
        private Panel pnl_ODRSTS;
        private CheckBox chk_9;
        private CheckBox chk_4;
        private CheckBox chk_3;
        private CheckBox chk_2;
        private CheckBox chk_ODRSTS;
        private CheckBox chk_1;
    }
}