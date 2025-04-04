using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm034_CodeSlipMstMaint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm034_CodeSlipMstMaint));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUpdateDatabase2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tglViewSimple = new System.Windows.Forms.RadioButton();
            this.tglViewNormal = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnHMCDPaste = new System.Windows.Forms.Button();
            this.btnHMCDClear = new System.Windows.Forms.Button();
            this.txtHMCD = new System.Windows.Forms.TextBox();
            this.cmbMaterial = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnNextDiffer = new System.Windows.Forms.Button();
            this.btnUpdateDatabase = new System.Windows.Forms.Button();
            this.btnReadExcelMaster = new System.Windows.Forms.Button();
            this.btnConvertMP = new System.Windows.Forms.Button();
            this.Dgv_CodeSlipMst = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CodeSlipMst)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUpdateDatabase2);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1262, 59);
            this.panel1.TabIndex = 3;
            // 
            // btnUpdateDatabase2
            // 
            this.btnUpdateDatabase2.BackColor = System.Drawing.Color.LightGreen;
            this.btnUpdateDatabase2.Location = new System.Drawing.Point(824, 12);
            this.btnUpdateDatabase2.Name = "btnUpdateDatabase2";
            this.btnUpdateDatabase2.Size = new System.Drawing.Size(400, 41);
            this.btnUpdateDatabase2.TabIndex = 14;
            this.btnUpdateDatabase2.Text = "工程数と検索キーを自動作成してデータベースに反映";
            this.btnUpdateDatabase2.UseVisualStyleBackColor = false;
            this.btnUpdateDatabase2.Click += new System.EventHandler(this.btnUpdateDatabase2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tglViewSimple);
            this.groupBox2.Controls.Add(this.tglViewNormal);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Font = new System.Drawing.Font("Yu Gothic UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox2.Location = new System.Drawing.Point(580, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 59);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "表示";
            // 
            // tglViewSimple
            // 
            this.tglViewSimple.Appearance = System.Windows.Forms.Appearance.Button;
            this.tglViewSimple.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tglViewSimple.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tglViewSimple.Location = new System.Drawing.Point(123, 17);
            this.tglViewSimple.Name = "tglViewSimple";
            this.tglViewSimple.Size = new System.Drawing.Size(100, 34);
            this.tglViewSimple.TabIndex = 9;
            this.tglViewSimple.TabStop = true;
            this.tglViewSimple.Text = "新システム";
            this.tglViewSimple.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tglViewSimple.UseVisualStyleBackColor = true;
            this.tglViewSimple.CheckedChanged += new System.EventHandler(this.tglViewSimple_CheckedChanged);
            // 
            // tglViewNormal
            // 
            this.tglViewNormal.Appearance = System.Windows.Forms.Appearance.Button;
            this.tglViewNormal.BackColor = System.Drawing.Color.LightGreen;
            this.tglViewNormal.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tglViewNormal.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tglViewNormal.Location = new System.Drawing.Point(17, 17);
            this.tglViewNormal.Name = "tglViewNormal";
            this.tglViewNormal.Size = new System.Drawing.Size(100, 34);
            this.tglViewNormal.TabIndex = 8;
            this.tglViewNormal.TabStop = true;
            this.tglViewNormal.Text = "標準";
            this.tglViewNormal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tglViewNormal.UseVisualStyleBackColor = false;
            this.tglViewNormal.CheckedChanged += new System.EventHandler(this.tglViewNormal_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnHMCDPaste);
            this.groupBox1.Controls.Add(this.btnHMCDClear);
            this.groupBox1.Controls.Add(this.txtHMCD);
            this.groupBox1.Controls.Add(this.cmbMaterial);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Yu Gothic UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(580, 59);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // btnHMCDPaste
            // 
            this.btnHMCDPaste.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnHMCDPaste.Location = new System.Drawing.Point(354, 21);
            this.btnHMCDPaste.Name = "btnHMCDPaste";
            this.btnHMCDPaste.Size = new System.Drawing.Size(88, 31);
            this.btnHMCDPaste.TabIndex = 12;
            this.btnHMCDPaste.Text = "貼り付け";
            this.btnHMCDPaste.UseVisualStyleBackColor = true;
            this.btnHMCDPaste.Click += new System.EventHandler(this.btnHMCDPaste_Click);
            // 
            // btnHMCDClear
            // 
            this.btnHMCDClear.Location = new System.Drawing.Point(193, 21);
            this.btnHMCDClear.Name = "btnHMCDClear";
            this.btnHMCDClear.Size = new System.Drawing.Size(88, 31);
            this.btnHMCDClear.TabIndex = 11;
            this.btnHMCDClear.Text = "品番クリア";
            this.btnHMCDClear.UseVisualStyleBackColor = true;
            this.btnHMCDClear.Click += new System.EventHandler(this.btnHMCDClear_Click);
            // 
            // txtHMCD
            // 
            this.txtHMCD.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHMCD.Location = new System.Drawing.Point(27, 21);
            this.txtHMCD.Name = "txtHMCD";
            this.txtHMCD.Size = new System.Drawing.Size(165, 32);
            this.txtHMCD.TabIndex = 10;
            this.txtHMCD.TextChanged += new System.EventHandler(this.txtHMCD_TextChanged);
            // 
            // cmbMaterial
            // 
            this.cmbMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaterial.Font = new System.Drawing.Font("Yu Gothic UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbMaterial.FormattingEnabled = true;
            this.cmbMaterial.Items.AddRange(new object[] {
            "全て",
            "■",
            "●",
            "○",
            "H",
            "平",
            "ﾌﾟﾚｽ",
            "ﾚｰｻﾞｰ"});
            this.cmbMaterial.Location = new System.Drawing.Point(321, 18);
            this.cmbMaterial.Name = "cmbMaterial";
            this.cmbMaterial.Size = new System.Drawing.Size(79, 33);
            this.cmbMaterial.TabIndex = 9;
            this.cmbMaterial.SelectedIndexChanged += new System.EventHandler(this.cmbMaterial_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 325);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1262, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(112, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnNextDiffer);
            this.panel2.Controls.Add(this.btnUpdateDatabase);
            this.panel2.Controls.Add(this.btnReadExcelMaster);
            this.panel2.Controls.Add(this.btnConvertMP);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 277);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1262, 48);
            this.panel2.TabIndex = 6;
            // 
            // btnNextDiffer
            // 
            this.btnNextDiffer.BackColor = System.Drawing.Color.LightPink;
            this.btnNextDiffer.Location = new System.Drawing.Point(635, 4);
            this.btnNextDiffer.Name = "btnNextDiffer";
            this.btnNextDiffer.Size = new System.Drawing.Size(164, 41);
            this.btnNextDiffer.TabIndex = 3;
            this.btnNextDiffer.Text = "次の相違点";
            this.btnNextDiffer.UseVisualStyleBackColor = false;
            this.btnNextDiffer.Click += new System.EventHandler(this.btnNextDiffer_Click);
            // 
            // btnUpdateDatabase
            // 
            this.btnUpdateDatabase.BackColor = System.Drawing.Color.LightGreen;
            this.btnUpdateDatabase.Location = new System.Drawing.Point(3, 4);
            this.btnUpdateDatabase.Name = "btnUpdateDatabase";
            this.btnUpdateDatabase.Size = new System.Drawing.Size(227, 41);
            this.btnUpdateDatabase.TabIndex = 2;
            this.btnUpdateDatabase.Text = "データベースに反映";
            this.btnUpdateDatabase.UseVisualStyleBackColor = false;
            this.btnUpdateDatabase.Click += new System.EventHandler(this.btnUpdateDatabase_Click);
            // 
            // btnReadExcelMaster
            // 
            this.btnReadExcelMaster.BackColor = System.Drawing.Color.LightPink;
            this.btnReadExcelMaster.Location = new System.Drawing.Point(236, 4);
            this.btnReadExcelMaster.Name = "btnReadExcelMaster";
            this.btnReadExcelMaster.Size = new System.Drawing.Size(393, 41);
            this.btnReadExcelMaster.TabIndex = 1;
            this.btnReadExcelMaster.Text = "最新のコード票マスタを読み込み変更点をチェック";
            this.btnReadExcelMaster.UseVisualStyleBackColor = false;
            this.btnReadExcelMaster.Click += new System.EventHandler(this.btnReadExcelMaster_Click);
            // 
            // btnConvertMP
            // 
            this.btnConvertMP.BackColor = System.Drawing.Color.LightPink;
            this.btnConvertMP.Location = new System.Drawing.Point(805, 3);
            this.btnConvertMP.Name = "btnConvertMP";
            this.btnConvertMP.Size = new System.Drawing.Size(227, 41);
            this.btnConvertMP.TabIndex = 0;
            this.btnConvertMP.Text = "新システム用に変換";
            this.btnConvertMP.UseVisualStyleBackColor = false;
            this.btnConvertMP.Click += new System.EventHandler(this.btnConvertMP_Click);
            // 
            // Dgv_CodeSlipMst
            // 
            this.Dgv_CodeSlipMst.AllowUserToAddRows = false;
            this.Dgv_CodeSlipMst.AllowUserToDeleteRows = false;
            this.Dgv_CodeSlipMst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_CodeSlipMst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_CodeSlipMst.Location = new System.Drawing.Point(0, 59);
            this.Dgv_CodeSlipMst.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Dgv_CodeSlipMst.Name = "Dgv_CodeSlipMst";
            this.Dgv_CodeSlipMst.RowHeadersWidth = 51;
            this.Dgv_CodeSlipMst.RowTemplate.Height = 24;
            this.Dgv_CodeSlipMst.Size = new System.Drawing.Size(1262, 218);
            this.Dgv_CodeSlipMst.TabIndex = 7;
            this.Dgv_CodeSlipMst.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Dgv_CodeSlipMst_CellMouseDown);
            this.Dgv_CodeSlipMst.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.Dgv_CodeSlipMst_ColumnWidthChanged);
            this.Dgv_CodeSlipMst.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Dgv_CodeSlipMst_RowPostPaint);
            this.Dgv_CodeSlipMst.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Dgv_CodeSlipMst_RowsRemoved);
            this.Dgv_CodeSlipMst.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dgv_CodeSlipMst_KeyDown);
            // 
            // Frm034_CodeSlipMstMaint
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1262, 347);
            this.Controls.Add(this.Dgv_CodeSlipMst);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Frm034_CodeSlipMstMaint";
            this.Text = "[KMD003SF] マスタ メンテナンス - コード票マスタ メンテ - Ver.230613.01a";
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CodeSlipMst)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Panel panel1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel panel2;
        private DataGridView Dgv_CodeSlipMst;
        private Button btnConvertMP;
        private Button btnUpdateDatabase;
        private Button btnReadExcelMaster;
        private GroupBox groupBox1;
        private Button btnHMCDClear;
        private TextBox txtHMCD;
        private ComboBox cmbMaterial;
        private GroupBox groupBox2;
        private RadioButton tglViewSimple;
        private RadioButton tglViewNormal;
        private Button btnNextDiffer;
        private Button btnUpdateDatabase2;
        private Button btnHMCDPaste;
    }
}