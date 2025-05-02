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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRefreshDataGridView = new System.Windows.Forms.Button();
            this.btn_HMCDDelete = new System.Windows.Forms.Button();
            this.btn_ExportExcel = new System.Windows.Forms.Button();
            this.btnNextDiffer = new System.Windows.Forms.Button();
            this.btnUpdateDatabase = new System.Windows.Forms.Button();
            this.btnReadExcelMaster = new System.Windows.Forms.Button();
            this.btnConvertMP = new System.Windows.Forms.Button();
            this.Dgv_CodeSlipMst = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnUpdateDatabase2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tglViewSimple = new System.Windows.Forms.RadioButton();
            this.tglViewNormal = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtKTKEY = new System.Windows.Forms.TextBox();
            this.btnHMCDPaste = new System.Windows.Forms.Button();
            this.btnFilterClear = new System.Windows.Forms.Button();
            this.txtHMCD = new System.Windows.Forms.TextBox();
            this.cmbMaterial = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CodeSlipMst)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
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
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(1131, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRefreshDataGridView);
            this.panel2.Controls.Add(this.btn_HMCDDelete);
            this.panel2.Controls.Add(this.btn_ExportExcel);
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
            // btnRefreshDataGridView
            // 
            this.btnRefreshDataGridView.BackColor = System.Drawing.Color.LightGreen;
            this.btnRefreshDataGridView.Location = new System.Drawing.Point(249, 4);
            this.btnRefreshDataGridView.Name = "btnRefreshDataGridView";
            this.btnRefreshDataGridView.Size = new System.Drawing.Size(150, 41);
            this.btnRefreshDataGridView.TabIndex = 7;
            this.btnRefreshDataGridView.Text = "再読み込み (F5)";
            this.btnRefreshDataGridView.UseVisualStyleBackColor = false;
            this.btnRefreshDataGridView.Click += new System.EventHandler(this.btnRefreshDataGridView_Click);
            // 
            // btn_HMCDDelete
            // 
            this.btn_HMCDDelete.BackColor = System.Drawing.Color.LightCoral;
            this.btn_HMCDDelete.Location = new System.Drawing.Point(561, 4);
            this.btn_HMCDDelete.Name = "btn_HMCDDelete";
            this.btn_HMCDDelete.Size = new System.Drawing.Size(84, 41);
            this.btn_HMCDDelete.TabIndex = 6;
            this.btn_HMCDDelete.Text = "行削除";
            this.btn_HMCDDelete.UseVisualStyleBackColor = false;
            this.btn_HMCDDelete.Click += new System.EventHandler(this.btn_HMCDDelete_Click);
            // 
            // btn_ExportExcel
            // 
            this.btn_ExportExcel.BackColor = System.Drawing.Color.LightGreen;
            this.btn_ExportExcel.Location = new System.Drawing.Point(405, 4);
            this.btn_ExportExcel.Name = "btn_ExportExcel";
            this.btn_ExportExcel.Size = new System.Drawing.Size(150, 41);
            this.btn_ExportExcel.TabIndex = 4;
            this.btn_ExportExcel.Text = "外部出力 (F10)";
            this.btn_ExportExcel.UseVisualStyleBackColor = false;
            this.btn_ExportExcel.Click += new System.EventHandler(this.btn_ExportExcel_Click);
            // 
            // btnNextDiffer
            // 
            this.btnNextDiffer.BackColor = System.Drawing.Color.LightPink;
            this.btnNextDiffer.Location = new System.Drawing.Point(1080, 3);
            this.btnNextDiffer.Name = "btnNextDiffer";
            this.btnNextDiffer.Size = new System.Drawing.Size(170, 41);
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
            this.btnUpdateDatabase.Size = new System.Drawing.Size(240, 41);
            this.btnUpdateDatabase.TabIndex = 2;
            this.btnUpdateDatabase.Text = "編集をデータベースに反映 (F9)";
            this.btnUpdateDatabase.UseVisualStyleBackColor = false;
            this.btnUpdateDatabase.Click += new System.EventHandler(this.btnUpdateDatabase_Click);
            // 
            // btnReadExcelMaster
            // 
            this.btnReadExcelMaster.BackColor = System.Drawing.Color.LightPink;
            this.btnReadExcelMaster.Location = new System.Drawing.Point(874, 4);
            this.btnReadExcelMaster.Name = "btnReadExcelMaster";
            this.btnReadExcelMaster.Size = new System.Drawing.Size(200, 41);
            this.btnReadExcelMaster.TabIndex = 1;
            this.btnReadExcelMaster.Text = "Excelコード票と比較";
            this.btnReadExcelMaster.UseVisualStyleBackColor = false;
            this.btnReadExcelMaster.Click += new System.EventHandler(this.btnReadExcelMaster_Click);
            // 
            // btnConvertMP
            // 
            this.btnConvertMP.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnConvertMP.Location = new System.Drawing.Point(651, 4);
            this.btnConvertMP.Name = "btnConvertMP";
            this.btnConvertMP.Size = new System.Drawing.Size(213, 41);
            this.btnConvertMP.TabIndex = 0;
            this.btnConvertMP.Text = "SSからTNを連結して検索ｷｰへ";
            this.btnConvertMP.UseVisualStyleBackColor = false;
            this.btnConvertMP.Click += new System.EventHandler(this.btnConvertMP_Click);
            // 
            // Dgv_CodeSlipMst
            // 
            this.Dgv_CodeSlipMst.AllowUserToDeleteRows = false;
            this.Dgv_CodeSlipMst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_CodeSlipMst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_CodeSlipMst.Location = new System.Drawing.Point(0, 65);
            this.Dgv_CodeSlipMst.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Dgv_CodeSlipMst.Name = "Dgv_CodeSlipMst";
            this.Dgv_CodeSlipMst.RowHeadersWidth = 51;
            this.Dgv_CodeSlipMst.RowTemplate.Height = 24;
            this.Dgv_CodeSlipMst.Size = new System.Drawing.Size(1262, 212);
            this.Dgv_CodeSlipMst.TabIndex = 7;
            this.Dgv_CodeSlipMst.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Dgv_CodeSlipMst_CellMouseDown);
            this.Dgv_CodeSlipMst.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Dgv_CodeSlipMst_EditingControlShowing);
            this.Dgv_CodeSlipMst.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Dgv_CodeSlipMst_RowPostPaint);
            this.Dgv_CodeSlipMst.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dgv_CodeSlipMst_KeyDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 600F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnUpdateDatabase2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1262, 65);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // btnUpdateDatabase2
            // 
            this.btnUpdateDatabase2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnUpdateDatabase2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdateDatabase2.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnUpdateDatabase2.Location = new System.Drawing.Point(1003, 15);
            this.btnUpdateDatabase2.Margin = new System.Windows.Forms.Padding(3, 15, 3, 8);
            this.btnUpdateDatabase2.Name = "btnUpdateDatabase2";
            this.btnUpdateDatabase2.Size = new System.Drawing.Size(156, 42);
            this.btnUpdateDatabase2.TabIndex = 20;
            this.btnUpdateDatabase2.Text = "工程数と検索キーを自動作成してデータベースに反映";
            this.btnUpdateDatabase2.UseVisualStyleBackColor = false;
            this.btnUpdateDatabase2.Click += new System.EventHandler(this.btnUpdateDatabase2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tglViewSimple);
            this.groupBox2.Controls.Add(this.tglViewNormal);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Yu Gothic UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox2.Location = new System.Drawing.Point(603, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 59);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "表示";
            // 
            // tglViewSimple
            // 
            this.tglViewSimple.Appearance = System.Windows.Forms.Appearance.Button;
            this.tglViewSimple.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tglViewSimple.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tglViewSimple.Location = new System.Drawing.Point(100, 17);
            this.tglViewSimple.Name = "tglViewSimple";
            this.tglViewSimple.Size = new System.Drawing.Size(80, 34);
            this.tglViewSimple.TabIndex = 9;
            this.tglViewSimple.TabStop = true;
            this.tglViewSimple.Text = "新システム";
            this.tglViewSimple.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tglViewSimple.UseVisualStyleBackColor = true;
            this.tglViewSimple.Click += new System.EventHandler(this.tglViewSimple_CheckedChanged);
            // 
            // tglViewNormal
            // 
            this.tglViewNormal.Appearance = System.Windows.Forms.Appearance.Button;
            this.tglViewNormal.BackColor = System.Drawing.Color.LightSkyBlue;
            this.tglViewNormal.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tglViewNormal.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tglViewNormal.Location = new System.Drawing.Point(14, 17);
            this.tglViewNormal.Name = "tglViewNormal";
            this.tglViewNormal.Size = new System.Drawing.Size(80, 34);
            this.tglViewNormal.TabIndex = 8;
            this.tglViewNormal.TabStop = true;
            this.tglViewNormal.Text = "標準";
            this.tglViewNormal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tglViewNormal.UseVisualStyleBackColor = false;
            this.tglViewNormal.Click += new System.EventHandler(this.tglViewNormal_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtKTKEY);
            this.groupBox1.Controls.Add(this.btnHMCDPaste);
            this.groupBox1.Controls.Add(this.btnFilterClear);
            this.groupBox1.Controls.Add(this.txtHMCD);
            this.groupBox1.Controls.Add(this.cmbMaterial);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Yu Gothic UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(594, 59);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // txtKTKEY
            // 
            this.txtKTKEY.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtKTKEY.Location = new System.Drawing.Point(347, 18);
            this.txtKTKEY.Name = "txtKTKEY";
            this.txtKTKEY.Size = new System.Drawing.Size(55, 32);
            this.txtKTKEY.TabIndex = 13;
            this.txtKTKEY.TextChanged += new System.EventHandler(this.txtKTKEY_TextChanged);
            // 
            // btnHMCDPaste
            // 
            this.btnHMCDPaste.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnHMCDPaste.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnHMCDPaste.Location = new System.Drawing.Point(221, 19);
            this.btnHMCDPaste.Name = "btnHMCDPaste";
            this.btnHMCDPaste.Size = new System.Drawing.Size(70, 31);
            this.btnHMCDPaste.TabIndex = 12;
            this.btnHMCDPaste.Text = "貼り付け";
            this.btnHMCDPaste.UseVisualStyleBackColor = true;
            this.btnHMCDPaste.Click += new System.EventHandler(this.btnHMCDPaste_Click);
            // 
            // btnFilterClear
            // 
            this.btnFilterClear.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFilterClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnFilterClear.Location = new System.Drawing.Point(514, 18);
            this.btnFilterClear.Name = "btnFilterClear";
            this.btnFilterClear.Size = new System.Drawing.Size(70, 31);
            this.btnFilterClear.TabIndex = 11;
            this.btnFilterClear.Text = "条件クリア";
            this.btnFilterClear.UseVisualStyleBackColor = true;
            this.btnFilterClear.Click += new System.EventHandler(this.btnFilterClear_Click);
            // 
            // txtHMCD
            // 
            this.txtHMCD.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHMCD.Location = new System.Drawing.Point(38, 18);
            this.txtHMCD.Name = "txtHMCD";
            this.txtHMCD.Size = new System.Drawing.Size(177, 32);
            this.txtHMCD.TabIndex = 10;
            this.txtHMCD.TextChanged += new System.EventHandler(this.txtHMCD_TextChanged);
            // 
            // cmbMaterial
            // 
            this.cmbMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaterial.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
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
            this.cmbMaterial.Location = new System.Drawing.Point(442, 19);
            this.cmbMaterial.Name = "cmbMaterial";
            this.cmbMaterial.Size = new System.Drawing.Size(67, 29);
            this.cmbMaterial.TabIndex = 9;
            this.cmbMaterial.SelectedIndexChanged += new System.EventHandler(this.cmbMaterial_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(8, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "品番";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(297, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "設備ｺｰﾄﾞ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(412, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "材料";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton3);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Yu Gothic UI", 7.8F);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox3.Location = new System.Drawing.Point(803, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(194, 59);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "工程";
            // 
            // radioButton3
            // 
            this.radioButton3.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.radioButton3.Font = new System.Drawing.Font("游ゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.radioButton3.Location = new System.Drawing.Point(128, 18);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(55, 34);
            this.radioButton3.TabIndex = 11;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "自動機";
            this.radioButton3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton3.UseVisualStyleBackColor = false;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            this.radioButton3.Paint += new System.Windows.Forms.PaintEventHandler(this.RadioButton_Paint);
            // 
            // radioButton2
            // 
            this.radioButton2.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton2.BackColor = System.Drawing.Color.LightSalmon;
            this.radioButton2.Font = new System.Drawing.Font("游ゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.radioButton2.Location = new System.Drawing.Point(67, 18);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(55, 34);
            this.radioButton2.TabIndex = 10;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "手動機";
            this.radioButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton2.UseVisualStyleBackColor = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            this.radioButton2.Paint += new System.Windows.Forms.PaintEventHandler(this.RadioButton_Paint);
            // 
            // radioButton1
            // 
            this.radioButton1.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton1.BackColor = System.Drawing.SystemColors.Control;
            this.radioButton1.Font = new System.Drawing.Font("游ゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.radioButton1.Location = new System.Drawing.Point(6, 18);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(55, 34);
            this.radioButton1.TabIndex = 9;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "全て";
            this.radioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton1.UseVisualStyleBackColor = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            this.radioButton1.Paint += new System.Windows.Forms.PaintEventHandler(this.RadioButton_Paint);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(1165, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 15, 3, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 42);
            this.button1.TabIndex = 21;
            this.button1.Text = "前工程へ";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button2.Location = new System.Drawing.Point(1215, 15);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 15, 3, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(44, 42);
            this.button2.TabIndex = 22;
            this.button2.Text = "次工程へ";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // Frm034_CodeSlipMstMaint
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1262, 347);
            this.Controls.Add(this.Dgv_CodeSlipMst);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Frm034_CodeSlipMstMaint";
            this.Text = "[KMD003SF] マスタ メンテナンス - コード票マスタ メンテ - Ver.230613.01a";
            this.Load += new System.EventHandler(this.Frm034_CodeSlipMstMaint_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm034_CodeSlipMstMaint_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CodeSlipMst)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel panel2;
        private DataGridView Dgv_CodeSlipMst;
        private Button btnConvertMP;
        private Button btnUpdateDatabase;
        private Button btnReadExcelMaster;
        private Button btnNextDiffer;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private Button btn_ExportExcel;
        private Button btn_HMCDDelete;
        private Button btnRefreshDataGridView;
        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBox2;
        private RadioButton tglViewSimple;
        private RadioButton tglViewNormal;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtKTKEY;
        private Button btnHMCDPaste;
        private Button btnFilterClear;
        private TextBox txtHMCD;
        private ComboBox cmbMaterial;
        private Label label3;
        private Label label2;
        private Button button2;
        private Button button1;
        private Button btnUpdateDatabase2;
        private GroupBox groupBox3;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
    }
}