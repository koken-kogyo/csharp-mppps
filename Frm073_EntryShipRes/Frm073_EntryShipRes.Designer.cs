using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm073_EntryShipRes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm073_EntryShipRes));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefreshDataGridView = new System.Windows.Forms.Button();
            this.btn_ExportExcel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBUCD = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Dgv_TanaInfo = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_TanaInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefreshDataGridView);
            this.panel1.Controls.Add(this.btn_ExportExcel);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1001, 94);
            this.panel1.TabIndex = 1;
            // 
            // btnRefreshDataGridView
            // 
            this.btnRefreshDataGridView.BackColor = System.Drawing.Color.LightGreen;
            this.btnRefreshDataGridView.Location = new System.Drawing.Point(571, 26);
            this.btnRefreshDataGridView.Name = "btnRefreshDataGridView";
            this.btnRefreshDataGridView.Size = new System.Drawing.Size(200, 51);
            this.btnRefreshDataGridView.TabIndex = 15;
            this.btnRefreshDataGridView.Text = "再読み込み (F5)";
            this.btnRefreshDataGridView.UseVisualStyleBackColor = false;
            this.btnRefreshDataGridView.Click += new System.EventHandler(this.btnRefreshDataGridView_Click);
            // 
            // btn_ExportExcel
            // 
            this.btn_ExportExcel.BackColor = System.Drawing.Color.LightGreen;
            this.btn_ExportExcel.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.btn_ExportExcel.Location = new System.Drawing.Point(786, 25);
            this.btn_ExportExcel.Name = "btn_ExportExcel";
            this.btn_ExportExcel.Size = new System.Drawing.Size(200, 52);
            this.btn_ExportExcel.TabIndex = 14;
            this.btn_ExportExcel.Text = "外部出力 (F10)";
            this.btn_ExportExcel.UseVisualStyleBackColor = false;
            this.btn_ExportExcel.Click += new System.EventHandler(this.btn_ExportExcel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtBUCD);
            this.groupBox1.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox1.Location = new System.Drawing.Point(15, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(15);
            this.groupBox1.Size = new System.Drawing.Size(165, 74);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(18, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 23);
            this.label2.TabIndex = 16;
            this.label2.Text = "棚番号";
            // 
            // txtBUCD
            // 
            this.txtBUCD.Font = new System.Drawing.Font("Yu Gothic UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBUCD.Location = new System.Drawing.Point(85, 23);
            this.txtBUCD.MaxLength = 2;
            this.txtBUCD.Name = "txtBUCD";
            this.txtBUCD.Size = new System.Drawing.Size(42, 39);
            this.txtBUCD.TabIndex = 10;
            this.txtBUCD.Text = "K";
            this.txtBUCD.TextChanged += new System.EventHandler(this.txtBUCD_TextChanged);
            this.txtBUCD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBUCD_KeyDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 524);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1001, 29);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(171, 23);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // Dgv_TanaInfo
            // 
            this.Dgv_TanaInfo.AllowUserToAddRows = false;
            this.Dgv_TanaInfo.AllowUserToDeleteRows = false;
            this.Dgv_TanaInfo.ColumnHeadersHeight = 29;
            this.Dgv_TanaInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Dgv_TanaInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_TanaInfo.Location = new System.Drawing.Point(0, 94);
            this.Dgv_TanaInfo.Name = "Dgv_TanaInfo";
            this.Dgv_TanaInfo.RowHeadersWidth = 51;
            this.Dgv_TanaInfo.RowTemplate.Height = 24;
            this.Dgv_TanaInfo.Size = new System.Drawing.Size(1001, 430);
            this.Dgv_TanaInfo.TabIndex = 4;
            this.Dgv_TanaInfo.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Dgv_TanaInfo_RowPostPaint);
            // 
            // Frm073_EntryShipRes
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1001, 553);
            this.Controls.Add(this.Dgv_TanaInfo);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 10.2F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm073_EntryShipRes";
            this.Text = "棚卸情報";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm073_EntryShipRes_KeyDown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_TanaInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox1;
        private Label label2;
        private TextBox txtBUCD;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private DataGridView Dgv_TanaInfo;
        private Button btn_ExportExcel;
        private Button btnRefreshDataGridView;
    }
}