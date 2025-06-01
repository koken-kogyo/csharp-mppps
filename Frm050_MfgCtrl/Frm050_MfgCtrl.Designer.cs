using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm050_MfgCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm050_MfgCtrl));
            this.Btn_MfgProgress = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.btn_PickupTehai = new System.Windows.Forms.Button();
            this.btn_PickupNaiji = new System.Windows.Forms.Button();
            this.btn_All_Print = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_MfgProgress
            // 
            this.Btn_MfgProgress.Enabled = false;
            this.Btn_MfgProgress.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.Btn_MfgProgress.Location = new System.Drawing.Point(12, 12);
            this.Btn_MfgProgress.Name = "Btn_MfgProgress";
            this.Btn_MfgProgress.Size = new System.Drawing.Size(300, 38);
            this.Btn_MfgProgress.TabIndex = 0;
            this.Btn_MfgProgress.Text = "加工進捗状況 (進度盤)";
            this.Btn_MfgProgress.UseVisualStyleBackColor = true;
            this.Btn_MfgProgress.Click += new System.EventHandler(this.Btn_MfgProgress_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Btn_Close.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.Btn_Close.Location = new System.Drawing.Point(12, 188);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(300, 38);
            this.Btn_Close.TabIndex = 4;
            this.Btn_Close.Text = "終了";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // btn_PickupTehai
            // 
            this.btn_PickupTehai.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.btn_PickupTehai.Location = new System.Drawing.Point(12, 100);
            this.btn_PickupTehai.Name = "btn_PickupTehai";
            this.btn_PickupTehai.Size = new System.Drawing.Size(300, 38);
            this.btn_PickupTehai.TabIndex = 2;
            this.btn_PickupTehai.Text = "工程別促進表データ作成";
            this.btn_PickupTehai.UseVisualStyleBackColor = true;
            this.btn_PickupTehai.Click += new System.EventHandler(this.btn_PickupTehai_Click);
            // 
            // btn_PickupNaiji
            // 
            this.btn_PickupNaiji.Enabled = false;
            this.btn_PickupNaiji.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.btn_PickupNaiji.Location = new System.Drawing.Point(12, 144);
            this.btn_PickupNaiji.Name = "btn_PickupNaiji";
            this.btn_PickupNaiji.Size = new System.Drawing.Size(300, 38);
            this.btn_PickupNaiji.TabIndex = 3;
            this.btn_PickupNaiji.Text = "工程別内示表データ作成";
            this.btn_PickupNaiji.UseVisualStyleBackColor = true;
            // 
            // btn_All_Print
            // 
            this.btn_All_Print.Enabled = false;
            this.btn_All_Print.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.btn_All_Print.Location = new System.Drawing.Point(12, 56);
            this.btn_All_Print.Name = "btn_All_Print";
            this.btn_All_Print.Size = new System.Drawing.Size(300, 38);
            this.btn_All_Print.TabIndex = 1;
            this.btn_All_Print.Text = "手配＋内示＋タナコン状況一覧";
            this.btn_All_Print.UseVisualStyleBackColor = true;
            this.btn_All_Print.Click += new System.EventHandler(this.btn_All_Print_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 240);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(321, 22);
            this.statusStrip1.TabIndex = 21;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // Frm050_MfgCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(321, 262);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_All_Print);
            this.Controls.Add(this.btn_PickupNaiji);
            this.Controls.Add(this.btn_PickupTehai);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_MfgProgress);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm050_MfgCtrl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "[KMD005SF] 製造管理 - Ver.230613.01a";
            this.Activated += new System.EventHandler(this.Frm050_MfgCtrl_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm050_MfgCtrl_FormClosing);
            this.Load += new System.EventHandler(this.Frm050_MfgCtrl_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm050_MfgCtrl_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button Btn_MfgProgress;
        private Button Btn_Close;
        private Button btn_PickupTehai;
        private Button btn_PickupNaiji;
        private Button btn_All_Print;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
    }
}