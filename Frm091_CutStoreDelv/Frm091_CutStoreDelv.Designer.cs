﻿using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm091_CutStoreDelv
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm091_CutStoreDelv));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PrevMonthButton = new System.Windows.Forms.Button();
            this.NextMonthButton = new System.Windows.Forms.Button();
            this.Dgv_Calendar = new System.Windows.Forms.DataGridView();
            this.CalendarLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Calendar)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 456);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip.Size = new System.Drawing.Size(685, 25);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(143, 20);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel";
            // 
            // PrevMonthButton
            // 
            this.PrevMonthButton.Location = new System.Drawing.Point(11, 8);
            this.PrevMonthButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PrevMonthButton.Name = "PrevMonthButton";
            this.PrevMonthButton.Size = new System.Drawing.Size(133, 38);
            this.PrevMonthButton.TabIndex = 2;
            this.PrevMonthButton.Text = "前月";
            this.PrevMonthButton.UseVisualStyleBackColor = true;
            this.PrevMonthButton.Click += new System.EventHandler(this.PrevMonthButton_Click);
            // 
            // NextMonthButton
            // 
            this.NextMonthButton.BackColor = System.Drawing.SystemColors.Control;
            this.NextMonthButton.Location = new System.Drawing.Point(540, 8);
            this.NextMonthButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NextMonthButton.Name = "NextMonthButton";
            this.NextMonthButton.Size = new System.Drawing.Size(133, 38);
            this.NextMonthButton.TabIndex = 3;
            this.NextMonthButton.Text = "翌月";
            this.NextMonthButton.UseVisualStyleBackColor = false;
            this.NextMonthButton.Click += new System.EventHandler(this.NextMonthButton_Click);
            // 
            // Dgv_Calendar
            // 
            this.Dgv_Calendar.AllowUserToResizeColumns = false;
            this.Dgv_Calendar.AllowUserToResizeRows = false;
            this.Dgv_Calendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Calendar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Dgv_Calendar.Location = new System.Drawing.Point(0, 54);
            this.Dgv_Calendar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dgv_Calendar.Name = "Dgv_Calendar";
            this.Dgv_Calendar.RowHeadersWidth = 51;
            this.Dgv_Calendar.RowTemplate.Height = 24;
            this.Dgv_Calendar.Size = new System.Drawing.Size(685, 402);
            this.Dgv_Calendar.TabIndex = 5;
            this.Dgv_Calendar.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Calendar_CellClick);
            // 
            // CalendarLabel
            // 
            this.CalendarLabel.BackColor = System.Drawing.Color.LightYellow;
            this.CalendarLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CalendarLabel.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CalendarLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CalendarLabel.Location = new System.Drawing.Point(151, 8);
            this.CalendarLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CalendarLabel.Name = "CalendarLabel";
            this.CalendarLabel.Size = new System.Drawing.Size(382, 37);
            this.CalendarLabel.TabIndex = 6;
            this.CalendarLabel.Text = "mm月 カレンダー";
            this.CalendarLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CalendarLabel.Click += new System.EventHandler(this.CalendarLabel_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(140, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem1.Text = "再出力を行う";
            // 
            // Frm091_CutStoreDelv
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(685, 481);
            this.Controls.Add(this.CalendarLabel);
            this.Controls.Add(this.Dgv_Calendar);
            this.Controls.Add(this.NextMonthButton);
            this.Controls.Add(this.PrevMonthButton);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm091_CutStoreDelv";
            this.Text = "[KMD009SF] スマート棚コン - 計画出庫データ作成 - Ver.230613.01a";
            this.Activated += new System.EventHandler(this.Frm091_CutStoreDelv_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm091_CutStoreDelv_KeyDown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Calendar)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip;
        private Button PrevMonthButton;
        private Button NextMonthButton;
        private ToolStripStatusLabel toolStripStatusLabel;
        private DataGridView Dgv_Calendar;
        private Label CalendarLabel;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem toolStripMenuItem1;
    }
}