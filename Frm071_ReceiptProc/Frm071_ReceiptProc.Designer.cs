using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm071_ReceiptProc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm071_ReceiptProc));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblResult = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtODRSTS = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtODRQTY = new System.Windows.Forms.TextBox();
            this.txtJIQTY = new System.Windows.Forms.TextBox();
            this.txtEDDT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtHMRNM = new System.Windows.Forms.TextBox();
            this.txtHMNM = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTKRNM = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHMCD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQRCD = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDT6 = new System.Windows.Forms.Label();
            this.lblDT5 = new System.Windows.Forms.Label();
            this.lblDT4 = new System.Windows.Forms.Label();
            this.lblDT3 = new System.Windows.Forms.Label();
            this.lblDT2 = new System.Windows.Forms.Label();
            this.lblDT1 = new System.Windows.Forms.Label();
            this.lblKT6 = new System.Windows.Forms.Label();
            this.lblKT5 = new System.Windows.Forms.Label();
            this.lblKT4 = new System.Windows.Forms.Label();
            this.lblKT3 = new System.Windows.Forms.Label();
            this.lblTitle6 = new System.Windows.Forms.Label();
            this.lblTitle5 = new System.Windows.Forms.Label();
            this.lblTitle4 = new System.Windows.Forms.Label();
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.lblKT1 = new System.Windows.Forms.Label();
            this.lblKT2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.2F));
            this.tableLayoutPanel1.Controls.Add(this.lblResult, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(960, 273);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.BackColor = System.Drawing.Color.DarkGray;
            this.lblResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResult.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblResult.ForeColor = System.Drawing.Color.DimGray;
            this.lblResult.Location = new System.Drawing.Point(638, 7);
            this.lblResult.Margin = new System.Windows.Forms.Padding(7);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(315, 259);
            this.lblResult.TabIndex = 16;
            this.lblResult.Text = "登録なし";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtODRSTS);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtODRQTY);
            this.panel2.Controls.Add(this.txtJIQTY);
            this.panel2.Controls.Add(this.txtEDDT);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(396, 6);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 6, 0, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(235, 261);
            this.panel2.TabIndex = 14;
            // 
            // txtODRSTS
            // 
            this.txtODRSTS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtODRSTS.Enabled = false;
            this.txtODRSTS.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtODRSTS.Location = new System.Drawing.Point(109, 179);
            this.txtODRSTS.Name = "txtODRSTS";
            this.txtODRSTS.Size = new System.Drawing.Size(100, 29);
            this.txtODRSTS.TabIndex = 24;
            this.txtODRSTS.Text = "確定";
            this.txtODRSTS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(20, 183);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 20);
            this.label8.TabIndex = 23;
            this.label8.Text = "手配状態";
            // 
            // txtODRQTY
            // 
            this.txtODRQTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtODRQTY.Enabled = false;
            this.txtODRQTY.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtODRQTY.Location = new System.Drawing.Point(109, 101);
            this.txtODRQTY.Name = "txtODRQTY";
            this.txtODRQTY.Size = new System.Drawing.Size(100, 29);
            this.txtODRQTY.TabIndex = 22;
            this.txtODRQTY.Text = "12";
            this.txtODRQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtJIQTY
            // 
            this.txtJIQTY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJIQTY.Enabled = false;
            this.txtJIQTY.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtJIQTY.Location = new System.Drawing.Point(109, 140);
            this.txtJIQTY.Name = "txtJIQTY";
            this.txtJIQTY.Size = new System.Drawing.Size(100, 29);
            this.txtJIQTY.TabIndex = 21;
            this.txtJIQTY.Text = "0";
            this.txtJIQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEDDT
            // 
            this.txtEDDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEDDT.Enabled = false;
            this.txtEDDT.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtEDDT.Location = new System.Drawing.Point(109, 64);
            this.txtEDDT.Name = "txtEDDT";
            this.txtEDDT.Size = new System.Drawing.Size(100, 29);
            this.txtEDDT.TabIndex = 20;
            this.txtEDDT.Text = "08/07";
            this.txtEDDT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(20, 144);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "実績数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(20, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "指示日";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(20, 105);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "指示数";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtHMRNM);
            this.panel1.Controls.Add(this.txtHMNM);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtTKRNM);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtHMCD);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtQRCD);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 261);
            this.panel1.TabIndex = 12;
            // 
            // txtHMRNM
            // 
            this.txtHMRNM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHMRNM.Enabled = false;
            this.txtHMRNM.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHMRNM.Location = new System.Drawing.Point(99, 202);
            this.txtHMRNM.Name = "txtHMRNM";
            this.txtHMRNM.Size = new System.Drawing.Size(262, 29);
            this.txtHMRNM.TabIndex = 23;
            this.txtHMRNM.Text = "JOINT〇44X69SW-NC-MC";
            // 
            // txtHMNM
            // 
            this.txtHMNM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHMNM.Enabled = false;
            this.txtHMNM.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHMNM.Location = new System.Drawing.Point(99, 163);
            this.txtHMNM.Name = "txtHMNM";
            this.txtHMNM.Size = new System.Drawing.Size(262, 29);
            this.txtHMNM.TabIndex = 22;
            this.txtHMNM.Text = "ｼﾞｮｲﾝﾄ/SW-NC-MC";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(18, 167);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 20);
            this.label5.TabIndex = 21;
            this.label5.Text = "品名";
            // 
            // txtTKRNM
            // 
            this.txtTKRNM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTKRNM.Enabled = false;
            this.txtTKRNM.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTKRNM.Location = new System.Drawing.Point(99, 64);
            this.txtTKRNM.Name = "txtTKRNM";
            this.txtTKRNM.Size = new System.Drawing.Size(262, 29);
            this.txtTKRNM.TabIndex = 19;
            this.txtTKRNM.Text = "ｸﾎﾞﾀ枚方";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(18, 68);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = "得意先";
            // 
            // txtHMCD
            // 
            this.txtHMCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHMCD.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F);
            this.txtHMCD.ForeColor = System.Drawing.Color.DimGray;
            this.txtHMCD.Location = new System.Drawing.Point(99, 105);
            this.txtHMCD.Name = "txtHMCD";
            this.txtHMCD.ReadOnly = true;
            this.txtHMCD.Size = new System.Drawing.Size(262, 43);
            this.txtHMCD.TabIndex = 15;
            this.txtHMCD.Text = "RD138-62131-4";
            this.txtHMCD.Click += new System.EventHandler(this.txtHMCD_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(18, 117);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "品番";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(18, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "手配QR";
            // 
            // txtQRCD
            // 
            this.txtQRCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQRCD.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.txtQRCD.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtQRCD.Location = new System.Drawing.Point(99, 19);
            this.txtQRCD.Margin = new System.Windows.Forms.Padding(10);
            this.txtQRCD.Name = "txtQRCD";
            this.txtQRCD.Size = new System.Drawing.Size(262, 29);
            this.txtQRCD.TabIndex = 1;
            this.txtQRCD.Text = "2608014441MPMCL";
            this.txtQRCD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQRCD_KeyPress);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 464);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(960, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.lblDT6, 5, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblDT5, 4, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblDT4, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblDT3, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblDT2, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblDT1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblKT6, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblKT5, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblKT4, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblKT3, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblTitle6, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTitle5, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTitle4, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTitle3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTitle2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTitle1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblKT1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblKT2, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 273);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(960, 191);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // lblDT6
            // 
            this.lblDT6.AutoSize = true;
            this.lblDT6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDT6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDT6.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F);
            this.lblDT6.Location = new System.Drawing.Point(801, 115);
            this.lblDT6.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblDT6.Name = "lblDT6";
            this.lblDT6.Size = new System.Drawing.Size(153, 73);
            this.lblDT6.TabIndex = 17;
            this.lblDT6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDT6.Click += new System.EventHandler(this.CustomeURLJump);
            // 
            // lblDT5
            // 
            this.lblDT5.AutoSize = true;
            this.lblDT5.BackColor = System.Drawing.Color.DarkGray;
            this.lblDT5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDT5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDT5.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F);
            this.lblDT5.ForeColor = System.Drawing.Color.DimGray;
            this.lblDT5.Location = new System.Drawing.Point(642, 115);
            this.lblDT5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblDT5.Name = "lblDT5";
            this.lblDT5.Size = new System.Drawing.Size(153, 73);
            this.lblDT5.TabIndex = 16;
            this.lblDT5.Text = "-";
            this.lblDT5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDT5.Click += new System.EventHandler(this.CustomeURLJump);
            // 
            // lblDT4
            // 
            this.lblDT4.AutoSize = true;
            this.lblDT4.BackColor = System.Drawing.Color.PaleGreen;
            this.lblDT4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDT4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDT4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDT4.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F);
            this.lblDT4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblDT4.Location = new System.Drawing.Point(483, 115);
            this.lblDT4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblDT4.Name = "lblDT4";
            this.lblDT4.Size = new System.Drawing.Size(153, 73);
            this.lblDT4.TabIndex = 15;
            this.lblDT4.Text = "8/7";
            this.lblDT4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDT4.Click += new System.EventHandler(this.CustomeURLJump);
            // 
            // lblDT3
            // 
            this.lblDT3.AutoSize = true;
            this.lblDT3.BackColor = System.Drawing.Color.LightCoral;
            this.lblDT3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDT3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDT3.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F);
            this.lblDT3.ForeColor = System.Drawing.Color.Yellow;
            this.lblDT3.Location = new System.Drawing.Point(324, 115);
            this.lblDT3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblDT3.Name = "lblDT3";
            this.lblDT3.Size = new System.Drawing.Size(153, 73);
            this.lblDT3.TabIndex = 14;
            this.lblDT3.Text = "-";
            this.lblDT3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDT3.Click += new System.EventHandler(this.CustomeURLJump);
            // 
            // lblDT2
            // 
            this.lblDT2.AutoSize = true;
            this.lblDT2.BackColor = System.Drawing.Color.PaleGreen;
            this.lblDT2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDT2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDT2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDT2.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F);
            this.lblDT2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblDT2.Location = new System.Drawing.Point(165, 115);
            this.lblDT2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblDT2.Name = "lblDT2";
            this.lblDT2.Size = new System.Drawing.Size(153, 73);
            this.lblDT2.TabIndex = 13;
            this.lblDT2.Tag = "https://nabev2:53030/mp/order/nc#jump4";
            this.lblDT2.Text = "8/7";
            this.lblDT2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDT2.Click += new System.EventHandler(this.CustomeURLJump);
            // 
            // lblDT1
            // 
            this.lblDT1.AutoSize = true;
            this.lblDT1.BackColor = System.Drawing.Color.PaleGreen;
            this.lblDT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDT1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDT1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDT1.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F);
            this.lblDT1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblDT1.Location = new System.Drawing.Point(6, 115);
            this.lblDT1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblDT1.Name = "lblDT1";
            this.lblDT1.Size = new System.Drawing.Size(153, 73);
            this.lblDT1.TabIndex = 12;
            this.lblDT1.Tag = "https://nabev2:53030/mp/order/sw";
            this.lblDT1.Text = "8/6";
            this.lblDT1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDT1.Click += new System.EventHandler(this.CustomeURLJump);
            // 
            // lblKT6
            // 
            this.lblKT6.AutoSize = true;
            this.lblKT6.BackColor = System.Drawing.SystemColors.Control;
            this.lblKT6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKT6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKT6.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F);
            this.lblKT6.Location = new System.Drawing.Point(801, 40);
            this.lblKT6.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblKT6.Name = "lblKT6";
            this.lblKT6.Size = new System.Drawing.Size(153, 72);
            this.lblKT6.TabIndex = 11;
            this.lblKT6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKT5
            // 
            this.lblKT5.AutoSize = true;
            this.lblKT5.BackColor = System.Drawing.Color.DarkGray;
            this.lblKT5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKT5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKT5.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F);
            this.lblKT5.ForeColor = System.Drawing.Color.DimGray;
            this.lblKT5.Location = new System.Drawing.Point(642, 40);
            this.lblKT5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblKT5.Name = "lblKT5";
            this.lblKT5.Size = new System.Drawing.Size(153, 72);
            this.lblKT5.TabIndex = 10;
            this.lblKT5.Text = "BT2";
            this.lblKT5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKT4
            // 
            this.lblKT4.AutoSize = true;
            this.lblKT4.BackColor = System.Drawing.Color.PaleGreen;
            this.lblKT4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKT4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKT4.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F);
            this.lblKT4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblKT4.Location = new System.Drawing.Point(483, 40);
            this.lblKT4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblKT4.Name = "lblKT4";
            this.lblKT4.Size = new System.Drawing.Size(153, 72);
            this.lblKT4.TabIndex = 9;
            this.lblKT4.Text = "3BI";
            this.lblKT4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKT3
            // 
            this.lblKT3.AutoSize = true;
            this.lblKT3.BackColor = System.Drawing.Color.LightCoral;
            this.lblKT3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKT3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKT3.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F);
            this.lblKT3.ForeColor = System.Drawing.Color.Yellow;
            this.lblKT3.Location = new System.Drawing.Point(324, 40);
            this.lblKT3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblKT3.Name = "lblKT3";
            this.lblKT3.Size = new System.Drawing.Size(153, 72);
            this.lblKT3.TabIndex = 8;
            this.lblKT3.Text = "TN-6";
            this.lblKT3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle6
            // 
            this.lblTitle6.AutoSize = true;
            this.lblTitle6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle6.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F);
            this.lblTitle6.Location = new System.Drawing.Point(801, 0);
            this.lblTitle6.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblTitle6.Name = "lblTitle6";
            this.lblTitle6.Size = new System.Drawing.Size(153, 37);
            this.lblTitle6.TabIndex = 5;
            this.lblTitle6.Text = "工程⑥";
            this.lblTitle6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle5
            // 
            this.lblTitle5.AutoSize = true;
            this.lblTitle5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle5.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F);
            this.lblTitle5.Location = new System.Drawing.Point(642, 0);
            this.lblTitle5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblTitle5.Name = "lblTitle5";
            this.lblTitle5.Size = new System.Drawing.Size(153, 37);
            this.lblTitle5.TabIndex = 4;
            this.lblTitle5.Text = "工程⑤";
            this.lblTitle5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle4
            // 
            this.lblTitle4.AutoSize = true;
            this.lblTitle4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle4.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F);
            this.lblTitle4.Location = new System.Drawing.Point(483, 0);
            this.lblTitle4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblTitle4.Name = "lblTitle4";
            this.lblTitle4.Size = new System.Drawing.Size(153, 37);
            this.lblTitle4.TabIndex = 3;
            this.lblTitle4.Text = "工程④";
            this.lblTitle4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle3
            // 
            this.lblTitle3.AutoSize = true;
            this.lblTitle3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle3.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F);
            this.lblTitle3.Location = new System.Drawing.Point(324, 0);
            this.lblTitle3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(153, 37);
            this.lblTitle3.TabIndex = 2;
            this.lblTitle3.Text = "工程③";
            this.lblTitle3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle2.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F);
            this.lblTitle2.Location = new System.Drawing.Point(165, 0);
            this.lblTitle2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(153, 37);
            this.lblTitle2.TabIndex = 1;
            this.lblTitle2.Text = "工程②";
            this.lblTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle1.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F);
            this.lblTitle1.Location = new System.Drawing.Point(6, 0);
            this.lblTitle1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(153, 37);
            this.lblTitle1.TabIndex = 0;
            this.lblTitle1.Text = "工程①";
            this.lblTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKT1
            // 
            this.lblKT1.AutoSize = true;
            this.lblKT1.BackColor = System.Drawing.Color.PaleGreen;
            this.lblKT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKT1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKT1.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F);
            this.lblKT1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblKT1.Location = new System.Drawing.Point(6, 40);
            this.lblKT1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblKT1.Name = "lblKT1";
            this.lblKT1.Size = new System.Drawing.Size(153, 72);
            this.lblKT1.TabIndex = 6;
            this.lblKT1.Text = "SW";
            this.lblKT1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKT2
            // 
            this.lblKT2.AutoSize = true;
            this.lblKT2.BackColor = System.Drawing.Color.PaleGreen;
            this.lblKT2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKT2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKT2.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 20F);
            this.lblKT2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblKT2.Location = new System.Drawing.Point(165, 40);
            this.lblKT2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblKT2.Name = "lblKT2";
            this.lblKT2.Size = new System.Drawing.Size(153, 72);
            this.lblKT2.TabIndex = 7;
            this.lblKT2.Text = "NC-8";
            this.lblKT2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Frm071_ReceiptProc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 486);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Frm071_ReceiptProc";
            this.Text = "Frm071 入出庫確認";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm071_ReceiptProc_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label lblResult;
        private Panel panel2;
        private Label label6;
        private Panel panel1;
        private TextBox txtHMCD;
        private Label label2;
        private Label label1;
        private TextBox txtQRCD;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private TextBox txtJIQTY;
        private TextBox txtEDDT;
        private Label label4;
        private Label label3;
        private TextBox txtHMNM;
        private Label label5;
        private TextBox txtTKRNM;
        private Label label7;
        private TextBox txtODRSTS;
        private Label label8;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lblTitle1;
        private Label lblKT1;
        private Label lblTitle6;
        private Label lblTitle5;
        private Label lblTitle4;
        private Label lblTitle3;
        private Label lblTitle2;
        private Label lblKT2;
        private Label lblDT6;
        private Label lblDT5;
        private Label lblDT4;
        private Label lblDT3;
        private Label lblDT2;
        private Label lblDT1;
        private Label lblKT6;
        private Label lblKT5;
        private Label lblKT4;
        private Label lblKT3;
        private TextBox txtODRQTY;
        private TextBox txtHMRNM;
    }
}