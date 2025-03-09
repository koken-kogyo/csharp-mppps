using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm092_CutStoreInvInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm092_CutStoreInvInfo));
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Dgv_InvInfo = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnHMCDClear = new System.Windows.Forms.Button();
            this.txtHMCD = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_InvInfo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 84);
            this.panel1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 492);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(900, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(151, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 433);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(900, 59);
            this.panel2.TabIndex = 2;
            // 
            // Dgv_InvInfo
            // 
            this.Dgv_InvInfo.AllowUserToAddRows = false;
            this.Dgv_InvInfo.AllowUserToDeleteRows = false;
            this.Dgv_InvInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_InvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_InvInfo.Location = new System.Drawing.Point(0, 84);
            this.Dgv_InvInfo.Name = "Dgv_InvInfo";
            this.Dgv_InvInfo.RowHeadersWidth = 51;
            this.Dgv_InvInfo.RowTemplate.Height = 24;
            this.Dgv_InvInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_InvInfo.Size = new System.Drawing.Size(900, 349);
            this.Dgv_InvInfo.TabIndex = 3;
            this.Dgv_InvInfo.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Dgv_InvInfo_RowPostPaint);
            this.Dgv_InvInfo.Sorted += new System.EventHandler(this.Dgv_InvInfo_Sorted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnHMCDClear);
            this.groupBox1.Controls.Add(this.txtHMCD);
            this.groupBox1.Font = new System.Drawing.Font("Yu Gothic UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox1.Location = new System.Drawing.Point(15, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(15);
            this.groupBox1.Size = new System.Drawing.Size(580, 69);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // btnHMCDClear
            // 
            this.btnHMCDClear.Location = new System.Drawing.Point(272, 20);
            this.btnHMCDClear.Name = "btnHMCDClear";
            this.btnHMCDClear.Size = new System.Drawing.Size(102, 31);
            this.btnHMCDClear.TabIndex = 11;
            this.btnHMCDClear.Text = "品番クリア";
            this.btnHMCDClear.UseVisualStyleBackColor = true;
            this.btnHMCDClear.Click += new System.EventHandler(this.btnHMCDClear_Click);
            // 
            // txtHMCD
            // 
            this.txtHMCD.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHMCD.Location = new System.Drawing.Point(35, 21);
            this.txtHMCD.Name = "txtHMCD";
            this.txtHMCD.Size = new System.Drawing.Size(231, 34);
            this.txtHMCD.TabIndex = 10;
            this.txtHMCD.TextChanged += new System.EventHandler(this.txtHMCD_TextChanged);
            // 
            // Frm092_CutStoreInvInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 518);
            this.Controls.Add(this.Dgv_InvInfo);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm092_CutStoreInvInfo";
            this.Text = "[KMD009SF] 切削ストア - 切削ストア在庫情報 - Ver.230613.01a";
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_InvInfo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel panel2;
        private DataGridView Dgv_InvInfo;
        private GroupBox groupBox1;
        private Button btnHMCDClear;
        private TextBox txtHMCD;
    }
}