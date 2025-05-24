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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_All_Print = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_MfgProgress
            // 
            this.Btn_MfgProgress.Enabled = false;
            this.Btn_MfgProgress.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.Btn_MfgProgress.Location = new System.Drawing.Point(12, 12);
            this.Btn_MfgProgress.Name = "Btn_MfgProgress";
            this.Btn_MfgProgress.Size = new System.Drawing.Size(300, 38);
            this.Btn_MfgProgress.TabIndex = 13;
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
            this.Btn_Close.TabIndex = 15;
            this.Btn_Close.Text = "終了";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.button2.Location = new System.Drawing.Point(12, 100);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(300, 38);
            this.button2.TabIndex = 17;
            this.button2.Text = "工程別促進表データ作成";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.button3.Location = new System.Drawing.Point(12, 144);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(300, 38);
            this.button3.TabIndex = 18;
            this.button3.Text = "工程別内示表データ作成";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btn_All_Print
            // 
            this.btn_All_Print.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.btn_All_Print.Location = new System.Drawing.Point(12, 56);
            this.btn_All_Print.Name = "btn_All_Print";
            this.btn_All_Print.Size = new System.Drawing.Size(300, 38);
            this.btn_All_Print.TabIndex = 20;
            this.btn_All_Print.Text = "手配＋内示＋タナコン状況一覧";
            this.btn_All_Print.UseVisualStyleBackColor = true;
            this.btn_All_Print.Click += new System.EventHandler(this.btn_All_Print_Click);
            // 
            // Frm050_MfgCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(321, 237);
            this.Controls.Add(this.btn_All_Print);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
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
            this.ResumeLayout(false);

        }

        #endregion
        private Button Btn_MfgProgress;
        private Button Btn_Close;
        private Button button2;
        private Button button3;
        private Button btn_All_Print;
    }
}