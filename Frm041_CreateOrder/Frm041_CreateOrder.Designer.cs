using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm041_CreateOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm041_CreateOrder));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CalendarLabel = new System.Windows.Forms.Label();
            this.NextMonthButton = new System.Windows.Forms.Button();
            this.PrevMonthButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Dgv_Calendar = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Btn_ImportOrder = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Calendar)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 540);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(851, 26);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(143, 20);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CalendarLabel);
            this.panel1.Controls.Add(this.NextMonthButton);
            this.panel1.Controls.Add(this.PrevMonthButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
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
            this.CalendarLabel.Font = new System.Drawing.Font("Meiryo UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
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
            this.PrevMonthButton.Location = new System.Drawing.Point(7, 8);
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
            this.Dgv_Calendar.Size = new System.Drawing.Size(851, 485);
            this.Dgv_Calendar.TabIndex = 9;
            this.Dgv_Calendar.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Calendar_CellClick);
            this.Dgv_Calendar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Dgv_Calendar_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Btn_ImportOrder);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 498);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(851, 42);
            this.panel2.TabIndex = 10;
            // 
            // Btn_ImportOrder
            // 
            this.Btn_ImportOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_ImportOrder.Location = new System.Drawing.Point(3, 3);
            this.Btn_ImportOrder.Name = "Btn_ImportOrder";
            this.Btn_ImportOrder.Size = new System.Drawing.Size(845, 35);
            this.Btn_ImportOrder.TabIndex = 11;
            this.Btn_ImportOrder.Text = "注文データ取込";
            this.Btn_ImportOrder.UseVisualStyleBackColor = false;
            this.Btn_ImportOrder.Click += new System.EventHandler(this.Btn_ImportOrder_Click);
            // 
            // Frm041_CreateOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 566);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Dgv_Calendar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm041_CreateOrder";
            this.Text = "[KMD004SF] オーダー管理 - 注文データ取込 - Ver.230613.01a";
            this.Resize += new System.EventHandler(this.Frm041_CreateOrder_Resize);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Calendar)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private Panel panel1;
        private Label label2;
        private DataGridView Dgv_Calendar;
        private Panel panel2;
        private Button Btn_ImportOrder;
        private Label CalendarLabel;
        private Button NextMonthButton;
        private Button PrevMonthButton;
    }
}