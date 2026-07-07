namespace MPPPS
{
    partial class Frm053_MfgPrediction
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnExcel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFilterClear = new System.Windows.Forms.Button();
            this.btnHMCDPaste = new System.Windows.Forms.Button();
            this.txtHMCD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmb_MCCD = new System.Windows.Forms.ComboBox();
            this.cmb_MCGCD = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelTop.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnExcel);
            this.panelTop.Controls.Add(this.groupBox3);
            this.panelTop.Controls.Add(this.groupBox2);
            this.panelTop.Controls.Add(this.groupBox1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1076, 68);
            this.panelTop.TabIndex = 18;
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExcel.BackColor = System.Drawing.Color.LightGreen;
            this.btnExcel.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnExcel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExcel.Location = new System.Drawing.Point(947, 20);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(117, 42);
            this.btnExcel.TabIndex = 16;
            this.btnExcel.Text = "Excel出力 (F10)";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.ButtonExcelExport_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Font = new System.Drawing.Font("Yu Gothic UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox3.Location = new System.Drawing.Point(546, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox3.Size = new System.Drawing.Size(132, 68);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "表示 ／ 非表示";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox2.Location = new System.Drawing.Point(69, 30);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(50, 19);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "手配";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox1.Location = new System.Drawing.Point(13, 30);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(50, 19);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "内示";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnFilterClear);
            this.groupBox2.Controls.Add(this.btnHMCDPaste);
            this.groupBox2.Controls.Add(this.txtHMCD);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Font = new System.Drawing.Font("Yu Gothic UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox2.Location = new System.Drawing.Point(258, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox2.Size = new System.Drawing.Size(288, 68);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "条件絞り込み";
            // 
            // btnFilterClear
            // 
            this.btnFilterClear.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFilterClear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnFilterClear.Location = new System.Drawing.Point(200, 37);
            this.btnFilterClear.Name = "btnFilterClear";
            this.btnFilterClear.Size = new System.Drawing.Size(70, 21);
            this.btnFilterClear.TabIndex = 19;
            this.btnFilterClear.Text = "条件クリア";
            this.btnFilterClear.UseVisualStyleBackColor = true;
            this.btnFilterClear.Click += new System.EventHandler(this.btnFilterClear_Click);
            // 
            // btnHMCDPaste
            // 
            this.btnHMCDPaste.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnHMCDPaste.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnHMCDPaste.Location = new System.Drawing.Point(200, 16);
            this.btnHMCDPaste.Name = "btnHMCDPaste";
            this.btnHMCDPaste.Size = new System.Drawing.Size(70, 21);
            this.btnHMCDPaste.TabIndex = 17;
            this.btnHMCDPaste.Text = "貼り付け";
            this.btnHMCDPaste.UseVisualStyleBackColor = true;
            this.btnHMCDPaste.Click += new System.EventHandler(this.btnHMCDPaste_Click);
            // 
            // txtHMCD
            // 
            this.txtHMCD.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHMCD.Location = new System.Drawing.Point(44, 23);
            this.txtHMCD.Name = "txtHMCD";
            this.txtHMCD.Size = new System.Drawing.Size(150, 32);
            this.txtHMCD.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(15, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "品番";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.cmb_MCCD);
            this.groupBox1.Controls.Add(this.cmb_MCGCD);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Yu Gothic UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(258, 68);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "初回検索";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearch.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSearch.Location = new System.Drawing.Point(175, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(76, 42);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "検索(F5)";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // cmb_MCCD
            // 
            this.cmb_MCCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_MCCD.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F);
            this.cmb_MCCD.FormattingEnabled = true;
            this.cmb_MCCD.Location = new System.Drawing.Point(92, 24);
            this.cmb_MCCD.Name = "cmb_MCCD";
            this.cmb_MCCD.Size = new System.Drawing.Size(81, 33);
            this.cmb_MCCD.TabIndex = 4;
            // 
            // cmb_MCGCD
            // 
            this.cmb_MCGCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_MCGCD.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F);
            this.cmb_MCGCD.FormattingEnabled = true;
            this.cmb_MCGCD.Location = new System.Drawing.Point(14, 24);
            this.cmb_MCGCD.Name = "cmb_MCGCD";
            this.cmb_MCGCD.Size = new System.Drawing.Size(72, 33);
            this.cmb_MCGCD.TabIndex = 3;
            this.cmb_MCGCD.SelectedIndexChanged += new System.EventHandler(this.Cmb_MCGCD_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 68);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(1076, 279);
            this.dataGridView1.TabIndex = 19;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 347);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1076, 22);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // Frm053_MfgPrediction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 369);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "Frm053_MfgPrediction";
            this.Text = "Fom053_MfgPrediction";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm053_MfgPrediction_KeyDown);
            this.panelTop.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnFilterClear;
        private System.Windows.Forms.Button btnHMCDPaste;
        private System.Windows.Forms.TextBox txtHMCD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmb_MCCD;
        private System.Windows.Forms.ComboBox cmb_MCGCD;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}