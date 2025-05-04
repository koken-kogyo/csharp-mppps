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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtHMCD = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Dgv_InvInfo = new System.Windows.Forms.DataGridView();
            this.btnReloadDatabase = new System.Windows.Forms.Button();
            this.btnUpdateDatabase = new System.Windows.Forms.Button();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnHMCDPaste = new System.Windows.Forms.Button();
            this.btnFilterClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_InvInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 78);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnFilterClear);
            this.groupBox1.Controls.Add(this.btnHMCDPaste);
            this.groupBox1.Controls.Add(this.txtHMCD);
            this.groupBox1.Font = new System.Drawing.Font("Yu Gothic UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox1.Location = new System.Drawing.Point(15, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(15);
            this.groupBox1.Size = new System.Drawing.Size(438, 62);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // txtHMCD
            // 
            this.txtHMCD.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHMCD.Location = new System.Drawing.Point(35, 21);
            this.txtHMCD.Name = "txtHMCD";
            this.txtHMCD.Size = new System.Drawing.Size(231, 29);
            this.txtHMCD.TabIndex = 10;
            this.txtHMCD.TextChanged += new System.EventHandler(this.txtHMCD_TextChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 328);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(900, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnReloadDatabase);
            this.panel2.Controls.Add(this.btnUpdateDatabase);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 275);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(900, 53);
            this.panel2.TabIndex = 2;
            // 
            // Dgv_InvInfo
            // 
            this.Dgv_InvInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_InvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_InvInfo.Location = new System.Drawing.Point(0, 78);
            this.Dgv_InvInfo.Name = "Dgv_InvInfo";
            this.Dgv_InvInfo.RowHeadersWidth = 51;
            this.Dgv_InvInfo.RowTemplate.Height = 24;
            this.Dgv_InvInfo.Size = new System.Drawing.Size(900, 197);
            this.Dgv_InvInfo.TabIndex = 3;
            this.Dgv_InvInfo.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Dgv_InvInfo_CellMouseDown);
            this.Dgv_InvInfo.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Dgv_InvInfo_EditingControlShowing);
            this.Dgv_InvInfo.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Dgv_InvInfo_RowPostPaint);
            this.Dgv_InvInfo.Sorted += new System.EventHandler(this.Dgv_InvInfo_Sorted);
            this.Dgv_InvInfo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dgv_InvInfo_KeyDown);
            // 
            // btnReloadDatabase
            // 
            this.btnReloadDatabase.BackColor = System.Drawing.SystemColors.Control;
            this.btnReloadDatabase.Location = new System.Drawing.Point(245, 6);
            this.btnReloadDatabase.Name = "btnReloadDatabase";
            this.btnReloadDatabase.Size = new System.Drawing.Size(227, 41);
            this.btnReloadDatabase.TabIndex = 6;
            this.btnReloadDatabase.Text = "再読み込み (F5)";
            this.btnReloadDatabase.UseVisualStyleBackColor = false;
            this.btnReloadDatabase.Click += new System.EventHandler(this.btnReloadDatabase_Click);
            // 
            // btnUpdateDatabase
            // 
            this.btnUpdateDatabase.BackColor = System.Drawing.Color.LightGreen;
            this.btnUpdateDatabase.Location = new System.Drawing.Point(12, 6);
            this.btnUpdateDatabase.Name = "btnUpdateDatabase";
            this.btnUpdateDatabase.Size = new System.Drawing.Size(227, 41);
            this.btnUpdateDatabase.TabIndex = 5;
            this.btnUpdateDatabase.Text = "データベースに反映 (F9)";
            this.btnUpdateDatabase.UseVisualStyleBackColor = false;
            this.btnUpdateDatabase.Click += new System.EventHandler(this.btnUpdateDatabase_Click);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(752, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnHMCDPaste
            // 
            this.btnHMCDPaste.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnHMCDPaste.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnHMCDPaste.Location = new System.Drawing.Point(272, 20);
            this.btnHMCDPaste.Name = "btnHMCDPaste";
            this.btnHMCDPaste.Size = new System.Drawing.Size(70, 31);
            this.btnHMCDPaste.TabIndex = 13;
            this.btnHMCDPaste.Text = "貼り付け";
            this.btnHMCDPaste.UseVisualStyleBackColor = true;
            this.btnHMCDPaste.Click += new System.EventHandler(this.btnHMCDPaste_Click);
            // 
            // btnFilterClear
            // 
            this.btnFilterClear.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFilterClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnFilterClear.Location = new System.Drawing.Point(348, 20);
            this.btnFilterClear.Name = "btnFilterClear";
            this.btnFilterClear.Size = new System.Drawing.Size(70, 31);
            this.btnFilterClear.TabIndex = 14;
            this.btnFilterClear.Text = "条件クリア";
            this.btnFilterClear.UseVisualStyleBackColor = true;
            this.btnFilterClear.Click += new System.EventHandler(this.btnFilterClear_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "品番";
            // 
            // Frm092_CutStoreInvInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 350);
            this.Controls.Add(this.Dgv_InvInfo);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Frm092_CutStoreInvInfo";
            this.Text = "[KMD009SF] 切削ストア - 切削ストア在庫情報 - Ver.230613.01a";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm092_CutStoreInvInfo_KeyDown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_InvInfo)).EndInit();
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
        private TextBox txtHMCD;
        private Button btnReloadDatabase;
        private Button btnUpdateDatabase;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private Button btnHMCDPaste;
        private Button btnFilterClear;
        private Label label2;
    }
}