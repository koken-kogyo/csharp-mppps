﻿using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm033_EqMstMaint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm033_EqMstMaint));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDisplayExpantion = new System.Windows.Forms.Button();
            this.btnDisplayReduction = new System.Windows.Forms.Button();
            this.btnColumnsAutoFit = new System.Windows.Forms.Button();
            this.cmbMCGCD = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReloadDatabase = new System.Windows.Forms.Button();
            this.btnUpdateDatabase = new System.Windows.Forms.Button();
            this.Dgv_EquipMst = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_EquipMst)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.cmbMCGCD);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 57);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnDisplayExpantion);
            this.panel3.Controls.Add(this.btnDisplayReduction);
            this.panel3.Controls.Add(this.btnColumnsAutoFit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(737, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(263, 57);
            this.panel3.TabIndex = 6;
            // 
            // btnDisplayExpantion
            // 
            this.btnDisplayExpantion.Location = new System.Drawing.Point(175, 9);
            this.btnDisplayExpantion.Name = "btnDisplayExpantion";
            this.btnDisplayExpantion.Size = new System.Drawing.Size(80, 40);
            this.btnDisplayExpantion.TabIndex = 8;
            this.btnDisplayExpantion.Text = "表示拡大";
            this.btnDisplayExpantion.UseVisualStyleBackColor = true;
            this.btnDisplayExpantion.Click += new System.EventHandler(this.btnDisplayExpantion_Click);
            // 
            // btnDisplayReduction
            // 
            this.btnDisplayReduction.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDisplayReduction.Location = new System.Drawing.Point(89, 9);
            this.btnDisplayReduction.Name = "btnDisplayReduction";
            this.btnDisplayReduction.Size = new System.Drawing.Size(80, 40);
            this.btnDisplayReduction.TabIndex = 7;
            this.btnDisplayReduction.Text = "表示縮小";
            this.btnDisplayReduction.UseVisualStyleBackColor = true;
            this.btnDisplayReduction.Click += new System.EventHandler(this.btnDisplayReduction_Click);
            // 
            // btnColumnsAutoFit
            // 
            this.btnColumnsAutoFit.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnColumnsAutoFit.Location = new System.Drawing.Point(3, 9);
            this.btnColumnsAutoFit.Name = "btnColumnsAutoFit";
            this.btnColumnsAutoFit.Size = new System.Drawing.Size(80, 40);
            this.btnColumnsAutoFit.TabIndex = 6;
            this.btnColumnsAutoFit.Text = "列幅調整";
            this.btnColumnsAutoFit.UseVisualStyleBackColor = true;
            this.btnColumnsAutoFit.Click += new System.EventHandler(this.btnColumnsAutoFit_Click);
            // 
            // cmbMCGCD
            // 
            this.cmbMCGCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMCGCD.Font = new System.Drawing.Font("Yu Gothic UI", 14F);
            this.cmbMCGCD.FormattingEnabled = true;
            this.cmbMCGCD.Location = new System.Drawing.Point(93, 12);
            this.cmbMCGCD.Name = "cmbMCGCD";
            this.cmbMCGCD.Size = new System.Drawing.Size(233, 33);
            this.cmbMCGCD.TabIndex = 0;
            this.cmbMCGCD.SelectedIndexChanged += new System.EventHandler(this.cmbMCGCD_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "設備選択：";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 310);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1000, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(101, 22);
            this.toolStripStatusLabel1.Text = "toolStripLabel1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnReloadDatabase);
            this.panel2.Controls.Add(this.btnUpdateDatabase);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 252);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1000, 58);
            this.panel2.TabIndex = 2;
            // 
            // btnReloadDatabase
            // 
            this.btnReloadDatabase.BackColor = System.Drawing.SystemColors.Control;
            this.btnReloadDatabase.Location = new System.Drawing.Point(245, 6);
            this.btnReloadDatabase.Name = "btnReloadDatabase";
            this.btnReloadDatabase.Size = new System.Drawing.Size(227, 41);
            this.btnReloadDatabase.TabIndex = 4;
            this.btnReloadDatabase.Text = "初期状態に戻す (F5)";
            this.btnReloadDatabase.UseVisualStyleBackColor = false;
            this.btnReloadDatabase.Click += new System.EventHandler(this.btnReloadDatabase_Click);
            // 
            // btnUpdateDatabase
            // 
            this.btnUpdateDatabase.BackColor = System.Drawing.Color.LightGreen;
            this.btnUpdateDatabase.Location = new System.Drawing.Point(12, 6);
            this.btnUpdateDatabase.Name = "btnUpdateDatabase";
            this.btnUpdateDatabase.Size = new System.Drawing.Size(227, 41);
            this.btnUpdateDatabase.TabIndex = 3;
            this.btnUpdateDatabase.Text = "データベースに反映 (F9)";
            this.btnUpdateDatabase.UseVisualStyleBackColor = false;
            this.btnUpdateDatabase.Click += new System.EventHandler(this.btnUpdateDatabase_Click);
            // 
            // Dgv_EquipMst
            // 
            this.Dgv_EquipMst.ColumnHeadersHeight = 24;
            this.Dgv_EquipMst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Dgv_EquipMst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_EquipMst.Location = new System.Drawing.Point(0, 57);
            this.Dgv_EquipMst.Name = "Dgv_EquipMst";
            this.Dgv_EquipMst.RowHeadersWidth = 51;
            this.Dgv_EquipMst.RowTemplate.Height = 24;
            this.Dgv_EquipMst.Size = new System.Drawing.Size(1000, 195);
            this.Dgv_EquipMst.TabIndex = 3;
            this.Dgv_EquipMst.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Dgv_EquipMst_EditingControlShowing);
            this.Dgv_EquipMst.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Dgv_EquipMst_RowPostPaint);
            this.Dgv_EquipMst.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dgv_EquipMst_KeyDown);
            // 
            // Frm033_EqMstMaint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 335);
            this.Controls.Add(this.Dgv_EquipMst);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Frm033_EqMstMaint";
            this.Text = "[KMD003SF] マスタ メンテナンス - 設備マスタ メンテ - Ver.230613.01a";
            this.Load += new System.EventHandler(this.Frm033_EqMstMaint_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm033_EqMstMaint_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_EquipMst)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripStatusLabel1;
        private Panel panel2;
        private DataGridView Dgv_EquipMst;
        private ComboBox cmbMCGCD;
        private Button btnUpdateDatabase;
        private Label label1;
        private Button btnReloadDatabase;
        private Panel panel3;
        private Button btnDisplayExpantion;
        private Button btnDisplayReduction;
        private Button btnColumnsAutoFit;
    }
}