using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm052_MfgPlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm052_MfgPlan));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ButtonUndo = new System.Windows.Forms.Button();
            this.ButtonRowDelete = new System.Windows.Forms.Button();
            this.ButtonEntry = new System.Windows.Forms.Button();
            this.ButtonClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.ButtonUndo);
            this.splitContainer1.Panel2.Controls.Add(this.ButtonRowDelete);
            this.splitContainer1.Panel2.Controls.Add(this.ButtonEntry);
            this.splitContainer1.Panel2.Controls.Add(this.ButtonClose);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitContainer1.Size = new System.Drawing.Size(846, 420);
            this.splitContainer1.SplitterDistance = 699;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(5, 5);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 32;
            this.dataGridView1.Size = new System.Drawing.Size(689, 410);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView1_CellFormatting);
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(133, 193);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "商品詳細";
            // 
            // ButtonUndo
            // 
            this.ButtonUndo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonUndo.Enabled = false;
            this.ButtonUndo.Location = new System.Drawing.Point(5, 227);
            this.ButtonUndo.Margin = new System.Windows.Forms.Padding(6);
            this.ButtonUndo.Name = "ButtonUndo";
            this.ButtonUndo.Size = new System.Drawing.Size(133, 47);
            this.ButtonUndo.TabIndex = 3;
            this.ButtonUndo.Text = "戻す (Ctrl+Z)";
            this.ButtonUndo.UseVisualStyleBackColor = true;
            this.ButtonUndo.Click += new System.EventHandler(this.ButtonUndo_Click);
            // 
            // ButtonRowDelete
            // 
            this.ButtonRowDelete.BackColor = System.Drawing.Color.LightCoral;
            this.ButtonRowDelete.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonRowDelete.Location = new System.Drawing.Point(5, 274);
            this.ButtonRowDelete.Margin = new System.Windows.Forms.Padding(6);
            this.ButtonRowDelete.Name = "ButtonRowDelete";
            this.ButtonRowDelete.Size = new System.Drawing.Size(133, 47);
            this.ButtonRowDelete.TabIndex = 5;
            this.ButtonRowDelete.Text = "行削除";
            this.ButtonRowDelete.UseVisualStyleBackColor = false;
            this.ButtonRowDelete.Click += new System.EventHandler(this.ButtonRowDelete_Click);
            // 
            // ButtonEntry
            // 
            this.ButtonEntry.BackColor = System.Drawing.Color.LightCoral;
            this.ButtonEntry.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonEntry.Location = new System.Drawing.Point(5, 321);
            this.ButtonEntry.Margin = new System.Windows.Forms.Padding(6);
            this.ButtonEntry.Name = "ButtonEntry";
            this.ButtonEntry.Size = new System.Drawing.Size(133, 47);
            this.ButtonEntry.TabIndex = 2;
            this.ButtonEntry.Text = "登録 (F9)";
            this.ButtonEntry.UseVisualStyleBackColor = false;
            // 
            // ButtonClose
            // 
            this.ButtonClose.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ButtonClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonClose.Location = new System.Drawing.Point(5, 368);
            this.ButtonClose.Margin = new System.Windows.Forms.Padding(6);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(133, 47);
            this.ButtonClose.TabIndex = 1;
            this.ButtonClose.Text = "閉じる";
            this.ButtonClose.UseVisualStyleBackColor = false;
            this.ButtonClose.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Frm052_MfgPlan
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(846, 420);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Frm052_MfgPlan";
            this.Text = "製造部計画表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm052_FormsPrinting_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer1;
        private DataGridView dataGridView1;
        private Button ButtonUndo;
        private Button ButtonEntry;
        private Button ButtonClose;
        private GroupBox groupBox1;
        private Button ButtonRowDelete;
    }
}