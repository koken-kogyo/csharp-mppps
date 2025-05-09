﻿using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm044_ImportPlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm044_ImportPlan));
            this.panel1 = new System.Windows.Forms.Panel();
            this.CalendarLabel = new System.Windows.Forms.Label();
            this.NextMonthButton = new System.Windows.Forms.Button();
            this.PrevMonthButton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Btn_PrintPlan = new System.Windows.Forms.Button();
            this.Btn_ImportPlan = new System.Windows.Forms.Button();
            this.Dgv_Calendar = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Calendar)).BeginInit();
            this.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(852, 55);
            this.panel1.TabIndex = 9;
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
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 542);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(852, 24);
            this.statusStrip.TabIndex = 11;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Yu Gothic UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 19);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Btn_PrintPlan);
            this.panel2.Controls.Add(this.Btn_ImportPlan);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 500);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 42);
            this.panel2.TabIndex = 12;
            // 
            // Btn_PrintPlan
            // 
            this.Btn_PrintPlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_PrintPlan.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_PrintPlan.Location = new System.Drawing.Point(424, 4);
            this.Btn_PrintPlan.Name = "Btn_PrintPlan";
            this.Btn_PrintPlan.Size = new System.Drawing.Size(420, 35);
            this.Btn_PrintPlan.TabIndex = 13;
            this.Btn_PrintPlan.Text = "内示カード印刷";
            this.Btn_PrintPlan.UseVisualStyleBackColor = false;
            this.Btn_PrintPlan.Click += new System.EventHandler(this.Btn_PrintPlan_Click);
            // 
            // Btn_ImportPlan
            // 
            this.Btn_ImportPlan.BackColor = System.Drawing.Color.MistyRose;
            this.Btn_ImportPlan.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_ImportPlan.Location = new System.Drawing.Point(3, 3);
            this.Btn_ImportPlan.Name = "Btn_ImportPlan";
            this.Btn_ImportPlan.Size = new System.Drawing.Size(420, 35);
            this.Btn_ImportPlan.TabIndex = 11;
            this.Btn_ImportPlan.Text = "内示データ取込";
            this.Btn_ImportPlan.UseVisualStyleBackColor = false;
            this.Btn_ImportPlan.Click += new System.EventHandler(this.Btn_ImportPlan_Click);
            // 
            // Dgv_Calendar
            // 
            this.Dgv_Calendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_Calendar.Location = new System.Drawing.Point(0, 55);
            this.Dgv_Calendar.Name = "Dgv_Calendar";
            this.Dgv_Calendar.RowHeadersWidth = 51;
            this.Dgv_Calendar.RowTemplate.Height = 24;
            this.Dgv_Calendar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Calendar.Size = new System.Drawing.Size(852, 445);
            this.Dgv_Calendar.TabIndex = 13;
            this.Dgv_Calendar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Dgv_Calendar_MouseUp);
            // 
            // Frm044_ImportPlan
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(852, 566);
            this.Controls.Add(this.Dgv_Calendar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm044_ImportPlan";
            this.Text = "[KMD004SF] オーダー管理 - 内示情報作成 - Ver.230613.01a";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm044_ImportPlan_KeyDown);
            this.Resize += new System.EventHandler(this.Frm044_CreateMaybeOrder_Resize);
            this.panel1.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Calendar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Label CalendarLabel;
        private Button NextMonthButton;
        private Button PrevMonthButton;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel panel2;
        private Button Btn_ImportPlan;
        private DataGridView Dgv_Calendar;
        private Button Btn_PrintPlan;
    }
}