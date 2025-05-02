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
            this.Btn_Printing = new System.Windows.Forms.Button();
            this.Btn_MfgProgress = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Printing
            // 
            this.Btn_Printing.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.Btn_Printing.Location = new System.Drawing.Point(12, 57);
            this.Btn_Printing.Name = "Btn_Printing";
            this.Btn_Printing.Size = new System.Drawing.Size(300, 38);
            this.Btn_Printing.TabIndex = 14;
            this.Btn_Printing.Text = "帳票出力";
            this.Btn_Printing.UseVisualStyleBackColor = true;
            this.Btn_Printing.Click += new System.EventHandler(this.Btn_Printing_Click);
            // 
            // Btn_MfgProgress
            // 
            this.Btn_MfgProgress.Enabled = false;
            this.Btn_MfgProgress.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.Btn_MfgProgress.Location = new System.Drawing.Point(12, 12);
            this.Btn_MfgProgress.Name = "Btn_MfgProgress";
            this.Btn_MfgProgress.Size = new System.Drawing.Size(300, 38);
            this.Btn_MfgProgress.TabIndex = 13;
            this.Btn_MfgProgress.Text = "加工進捗状況";
            this.Btn_MfgProgress.UseVisualStyleBackColor = true;
            this.Btn_MfgProgress.Click += new System.EventHandler(this.Btn_MfgProgress_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Btn_Close.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.Btn_Close.Location = new System.Drawing.Point(316, 57);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(300, 38);
            this.Btn_Close.TabIndex = 15;
            this.Btn_Close.Text = "終了";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Frm050_MfgCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(628, 101);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_Printing);
            this.Controls.Add(this.Btn_MfgProgress);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Frm050_MfgCtrl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "[KMD005SF] 製造管理 - Ver.230613.01a";
            this.Load += new System.EventHandler(this.Frm050_MfgCtrl_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm050_MfgCtrl_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Button Btn_Printing;
        private Button Btn_MfgProgress;
        private Button Btn_Close;
    }
}