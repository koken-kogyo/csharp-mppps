using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm100_VerInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm100_VerInfo));
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_MY_PGM_ID = new System.Windows.Forms.Label();
            this.lbl_MY_PGM_VER = new System.Windows.Forms.Label();
            this.lbl_FileVersion = new System.Windows.Forms.Label();
            this.lbl_BuildDate = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Btn_Close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F);
            this.label1.Location = new System.Drawing.Point(52, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "切削工程生産計画システム";
            // 
            // lbl_MY_PGM_ID
            // 
            this.lbl_MY_PGM_ID.AutoSize = true;
            this.lbl_MY_PGM_ID.Location = new System.Drawing.Point(156, 45);
            this.lbl_MY_PGM_ID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_MY_PGM_ID.Name = "lbl_MY_PGM_ID";
            this.lbl_MY_PGM_ID.Size = new System.Drawing.Size(127, 12);
            this.lbl_MY_PGM_ID.TabIndex = 1;
            this.lbl_MY_PGM_ID.Text = "プログラム ID: KMD001SF";
            // 
            // lbl_MY_PGM_VER
            // 
            this.lbl_MY_PGM_VER.AutoSize = true;
            this.lbl_MY_PGM_VER.Location = new System.Drawing.Point(122, 64);
            this.lbl_MY_PGM_VER.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_MY_PGM_VER.Name = "lbl_MY_PGM_VER";
            this.lbl_MY_PGM_VER.Size = new System.Drawing.Size(161, 12);
            this.lbl_MY_PGM_VER.TabIndex = 2;
            this.lbl_MY_PGM_VER.Text = "プログラム バージョン: 230613.01a";
            // 
            // lbl_FileVersion
            // 
            this.lbl_FileVersion.AutoSize = true;
            this.lbl_FileVersion.Location = new System.Drawing.Point(132, 83);
            this.lbl_FileVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_FileVersion.Name = "lbl_FileVersion";
            this.lbl_FileVersion.Size = new System.Drawing.Size(124, 12);
            this.lbl_FileVersion.TabIndex = 3;
            this.lbl_FileVersion.Text = "ファイル バージョン: 0.0.0.1";
            // 
            // lbl_BuildDate
            // 
            this.lbl_BuildDate.AutoSize = true;
            this.lbl_BuildDate.Location = new System.Drawing.Point(164, 102);
            this.lbl_BuildDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_BuildDate.Name = "lbl_BuildDate";
            this.lbl_BuildDate.Size = new System.Drawing.Size(166, 12);
            this.lbl_BuildDate.TabIndex = 4;
            this.lbl_BuildDate.Text = "ビルド日付: 2023/04/12 13:40:52";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(81, 154);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "© 2025 Koken Kogyo Co., Ltd.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 36);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(99, 109);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Btn_Close
            // 
            this.Btn_Close.Location = new System.Drawing.Point(240, 124);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(2);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(70, 23);
            this.Btn_Close.TabIndex = 7;
            this.Btn_Close.Text = "閉じる";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Frm100_VerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 173);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbl_BuildDate);
            this.Controls.Add(this.lbl_FileVersion);
            this.Controls.Add(this.lbl_MY_PGM_VER);
            this.Controls.Add(this.lbl_MY_PGM_ID);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm100_VerInfo";
            this.Text = "バージョン情報";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label lbl_MY_PGM_ID;
        private Label lbl_MY_PGM_VER;
        private Label lbl_FileVersion;
        private Label lbl_BuildDate;
        private Label label6;
        private PictureBox pictureBox1;
        private Button Btn_Close;
    }
}