using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm041_ImportOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm041_ImportOrder));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CalendarLabel = new System.Windows.Forms.Label();
            this.NextMonthButton = new System.Windows.Forms.Button();
            this.PrevMonthButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Dgv_Calendar = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Btn_PrintCancel = new System.Windows.Forms.Button();
            this.Btn_PrintOrder = new System.Windows.Forms.Button();
            this.Btn_ImportOrder = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Calendar)).BeginInit();
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
            this.statusStrip.Location = new System.Drawing.Point(0, 542);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(851, 24);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Yu Gothic UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(139, 19);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripStatusLabel2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(697, 19);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripStatusLabel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripStatusLabel2_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CalendarLabel);
            this.panel1.Controls.Add(this.NextMonthButton);
            this.panel1.Controls.Add(this.PrevMonthButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(851, 55);
            this.panel1.TabIndex = 8;
            // 
            // CalendarLabel
            // 
            this.CalendarLabel.AutoEllipsis = true;
            this.CalendarLabel.BackColor = System.Drawing.Color.LightYellow;
            this.CalendarLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CalendarLabel.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CalendarLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CalendarLabel.Location = new System.Drawing.Point(234, 9);
            this.CalendarLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CalendarLabel.Name = "CalendarLabel";
            this.CalendarLabel.Size = new System.Drawing.Size(382, 37);
            this.CalendarLabel.TabIndex = 9;
            this.CalendarLabel.Text = "mm月 カレンダー";
            this.CalendarLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CalendarLabel.Click += new System.EventHandler(this.CalendarLabel_Click);
            // 
            // NextMonthButton
            // 
            this.NextMonthButton.BackColor = System.Drawing.SystemColors.Control;
            this.NextMonthButton.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NextMonthButton.Location = new System.Drawing.Point(711, 9);
            this.NextMonthButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NextMonthButton.Name = "NextMonthButton";
            this.NextMonthButton.Size = new System.Drawing.Size(133, 38);
            this.NextMonthButton.TabIndex = 8;
            this.NextMonthButton.Text = "翌月";
            this.NextMonthButton.UseVisualStyleBackColor = false;
            this.NextMonthButton.Click += new System.EventHandler(this.NextMonthButton_Click);
            // 
            // PrevMonthButton
            // 
            this.PrevMonthButton.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PrevMonthButton.Location = new System.Drawing.Point(5, 8);
            this.PrevMonthButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PrevMonthButton.Name = "PrevMonthButton";
            this.PrevMonthButton.Size = new System.Drawing.Size(133, 38);
            this.PrevMonthButton.TabIndex = 7;
            this.PrevMonthButton.Text = "前月";
            this.PrevMonthButton.UseVisualStyleBackColor = true;
            this.PrevMonthButton.Click += new System.EventHandler(this.PrevMonthButton_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label2.Location = new System.Drawing.Point(412, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 68);
            this.label2.TabIndex = 7;
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Dgv_Calendar
            // 
            this.Dgv_Calendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_Calendar.Location = new System.Drawing.Point(0, 55);
            this.Dgv_Calendar.Name = "Dgv_Calendar";
            this.Dgv_Calendar.RowHeadersWidth = 51;
            this.Dgv_Calendar.RowTemplate.Height = 24;
            this.Dgv_Calendar.Size = new System.Drawing.Size(851, 487);
            this.Dgv_Calendar.TabIndex = 9;
            this.Dgv_Calendar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Calendar_CellDoubleClick);
            this.Dgv_Calendar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Dgv_Calendar_MouseUp);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.Btn_PrintCancel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.Btn_PrintOrder, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Btn_ImportOrder, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 500);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(851, 42);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // Btn_PrintCancel
            // 
            this.Btn_PrintCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_PrintCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_PrintCancel.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_PrintCancel.Location = new System.Drawing.Point(683, 3);
            this.Btn_PrintCancel.Name = "Btn_PrintCancel";
            this.Btn_PrintCancel.Size = new System.Drawing.Size(165, 36);
            this.Btn_PrintCancel.TabIndex = 14;
            this.Btn_PrintCancel.Text = "印刷不要";
            this.Btn_PrintCancel.UseVisualStyleBackColor = false;
            this.Btn_PrintCancel.Click += new System.EventHandler(this.Btn_PrintCancel_Click);
            // 
            // Btn_PrintOrder
            // 
            this.Btn_PrintOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_PrintOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_PrintOrder.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_PrintOrder.Location = new System.Drawing.Point(343, 3);
            this.Btn_PrintOrder.Name = "Btn_PrintOrder";
            this.Btn_PrintOrder.Size = new System.Drawing.Size(334, 36);
            this.Btn_PrintOrder.TabIndex = 13;
            this.Btn_PrintOrder.Text = "製造指示カード印刷";
            this.Btn_PrintOrder.UseVisualStyleBackColor = false;
            this.Btn_PrintOrder.Click += new System.EventHandler(this.Btn_PrintOrder_Click);
            // 
            // Btn_ImportOrder
            // 
            this.Btn_ImportOrder.BackColor = System.Drawing.Color.MistyRose;
            this.Btn_ImportOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_ImportOrder.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_ImportOrder.Location = new System.Drawing.Point(3, 3);
            this.Btn_ImportOrder.Name = "Btn_ImportOrder";
            this.Btn_ImportOrder.Size = new System.Drawing.Size(334, 36);
            this.Btn_ImportOrder.TabIndex = 12;
            this.Btn_ImportOrder.Text = "手配データ取込";
            this.Btn_ImportOrder.UseVisualStyleBackColor = false;
            this.Btn_ImportOrder.Click += new System.EventHandler(this.Btn_ImportOrder_Click);
            // 
            // Frm041_ImportOrder
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(851, 566);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Dgv_Calendar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm041_ImportOrder";
            this.Text = "[KMD004SF] オーダー管理 - 手配情報作成 - Ver.250215";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm041_ImportOrder_KeyDown);
            this.Resize += new System.EventHandler(this.Frm041_ImportOrder_Resize);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Calendar)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel panel1;
        private Label label2;
        private DataGridView Dgv_Calendar;
        private Label CalendarLabel;
        private Button NextMonthButton;
        private Button PrevMonthButton;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private TableLayoutPanel tableLayoutPanel1;
        private Button Btn_PrintCancel;
        private Button Btn_PrintOrder;
        private Button Btn_ImportOrder;
    }
}