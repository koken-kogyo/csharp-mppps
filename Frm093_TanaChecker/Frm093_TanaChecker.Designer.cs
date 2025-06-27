namespace MPPPS
{
    partial class Frm093_TanaChecker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm093_TanaChecker));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Result = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtLikeHMCD = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHMCD = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblODRSTS = new System.Windows.Forms.TextBox();
            this.lblODRQTY = new System.Windows.Forms.TextBox();
            this.lblHMCD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQRCD = new System.Windows.Forms.TextBox();
            this.Dgv_InvInfo = new System.Windows.Forms.DataGridView();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_InvInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(960, 26);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(151, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 309F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_Result, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(960, 213);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // lbl_Result
            // 
            this.lbl_Result.AutoSize = true;
            this.lbl_Result.BackColor = System.Drawing.Color.DarkGray;
            this.lbl_Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Result.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Result.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_Result.Location = new System.Drawing.Point(657, 7);
            this.lbl_Result.Margin = new System.Windows.Forms.Padding(7);
            this.lbl_Result.Name = "lbl_Result";
            this.lbl_Result.Size = new System.Drawing.Size(296, 199);
            this.lbl_Result.TabIndex = 16;
            this.lbl_Result.Text = "登録なし";
            this.lbl_Result.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtLikeHMCD);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtHMCD);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(362, 6);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(284, 201);
            this.panel2.TabIndex = 14;
            // 
            // txtLikeHMCD
            // 
            this.txtLikeHMCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLikeHMCD.Location = new System.Drawing.Point(40, 136);
            this.txtLikeHMCD.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtLikeHMCD.Name = "txtLikeHMCD";
            this.txtLikeHMCD.Size = new System.Drawing.Size(229, 34);
            this.txtLikeHMCD.TabIndex = 17;
            this.txtLikeHMCD.TextChanged += new System.EventHandler(this.txtLikeHMCD_TextChanged);
            this.txtLikeHMCD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLikeHMCD_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(13, 105);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(210, 25);
            this.label6.TabIndex = 16;
            this.label6.Text = "品番の LIKE(あいまい) 検索";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(13, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 25);
            this.label5.TabIndex = 15;
            this.label5.Text = "品番の検索";
            // 
            // txtHMCD
            // 
            this.txtHMCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHMCD.Location = new System.Drawing.Point(40, 51);
            this.txtHMCD.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtHMCD.Name = "txtHMCD";
            this.txtHMCD.Size = new System.Drawing.Size(229, 34);
            this.txtHMCD.TabIndex = 1;
            this.txtHMCD.TextChanged += new System.EventHandler(this.TextHMCD_TextChanged);
            this.txtHMCD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextHMCD_KeyDown);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblODRSTS);
            this.panel1.Controls.Add(this.lblODRQTY);
            this.panel1.Controls.Add(this.lblHMCD);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtQRCD);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 6);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 201);
            this.panel1.TabIndex = 12;
            // 
            // lblODRSTS
            // 
            this.lblODRSTS.Enabled = false;
            this.lblODRSTS.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblODRSTS.Location = new System.Drawing.Point(123, 151);
            this.lblODRSTS.Name = "lblODRSTS";
            this.lblODRSTS.Size = new System.Drawing.Size(211, 34);
            this.lblODRSTS.TabIndex = 17;
            // 
            // lblODRQTY
            // 
            this.lblODRQTY.Enabled = false;
            this.lblODRQTY.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblODRQTY.Location = new System.Drawing.Point(123, 111);
            this.lblODRQTY.Name = "lblODRQTY";
            this.lblODRQTY.Size = new System.Drawing.Size(211, 34);
            this.lblODRQTY.TabIndex = 16;
            // 
            // lblHMCD
            // 
            this.lblHMCD.Enabled = false;
            this.lblHMCD.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHMCD.Location = new System.Drawing.Point(123, 71);
            this.lblHMCD.Name = "lblHMCD";
            this.lblHMCD.Size = new System.Drawing.Size(211, 34);
            this.lblHMCD.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(24, 77);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 25);
            this.label2.TabIndex = 14;
            this.label2.Text = "品番";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(24, 157);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "手配状態";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(24, 117);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "手配数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 25);
            this.label1.TabIndex = 11;
            this.label1.Text = "手配QR";
            // 
            // txtQRCD
            // 
            this.txtQRCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQRCD.Location = new System.Drawing.Point(123, 15);
            this.txtQRCD.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtQRCD.Name = "txtQRCD";
            this.txtQRCD.Size = new System.Drawing.Size(211, 34);
            this.txtQRCD.TabIndex = 1;
            this.txtQRCD.Text = "2505047994MPCNL";
            this.txtQRCD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextQRCD_KeyDown);
            // 
            // Dgv_InvInfo
            // 
            this.Dgv_InvInfo.AllowUserToAddRows = false;
            this.Dgv_InvInfo.AllowUserToDeleteRows = false;
            this.Dgv_InvInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_InvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_InvInfo.Location = new System.Drawing.Point(0, 213);
            this.Dgv_InvInfo.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Dgv_InvInfo.Name = "Dgv_InvInfo";
            this.Dgv_InvInfo.RowHeadersVisible = false;
            this.Dgv_InvInfo.RowHeadersWidth = 51;
            this.Dgv_InvInfo.RowTemplate.Height = 24;
            this.Dgv_InvInfo.Size = new System.Drawing.Size(960, 247);
            this.Dgv_InvInfo.TabIndex = 14;
            // 
            // Frm093_TanaChecker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 486);
            this.Controls.Add(this.Dgv_InvInfo);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "Frm093_TanaChecker";
            this.Text = "Frm093_TanaChecker";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm093_TanaChecker_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_InvInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQRCD;
        private System.Windows.Forms.TextBox lblODRSTS;
        private System.Windows.Forms.TextBox lblODRQTY;
        private System.Windows.Forms.TextBox lblHMCD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_Result;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtHMCD;
        private System.Windows.Forms.DataGridView Dgv_InvInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLikeHMCD;
        private System.Windows.Forms.Label label6;
    }
}