using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm090_CutStore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm090_CutStore));
            this.Btn_CutStoreDelv = new System.Windows.Forms.Button();
            this.Btn_CutStoreInvInfo = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_CutStoreDelv
            // 
            this.Btn_CutStoreDelv.Location = new System.Drawing.Point(12, 9);
            this.Btn_CutStoreDelv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_CutStoreDelv.Name = "Btn_CutStoreDelv";
            this.Btn_CutStoreDelv.Size = new System.Drawing.Size(250, 29);
            this.Btn_CutStoreDelv.TabIndex = 0;
            this.Btn_CutStoreDelv.Text = "切削ストア出庫";
            this.Btn_CutStoreDelv.UseVisualStyleBackColor = true;
            this.Btn_CutStoreDelv.Click += new System.EventHandler(this.Btn_CutStoreDelv_Click_1);
            // 
            // Btn_CutStoreInvInfo
            // 
            this.Btn_CutStoreInvInfo.Location = new System.Drawing.Point(12, 42);
            this.Btn_CutStoreInvInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_CutStoreInvInfo.Name = "Btn_CutStoreInvInfo";
            this.Btn_CutStoreInvInfo.Size = new System.Drawing.Size(250, 29);
            this.Btn_CutStoreInvInfo.TabIndex = 1;
            this.Btn_CutStoreInvInfo.Text = "切削ストア在庫情報";
            this.Btn_CutStoreInvInfo.UseVisualStyleBackColor = true;
            this.Btn_CutStoreInvInfo.Click += new System.EventHandler(this.Btn_CutStoreInvInfo_Click_1);
            // 
            // Btn_Close
            // 
            this.Btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Btn_Close.Location = new System.Drawing.Point(268, 42);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(250, 29);
            this.Btn_Close.TabIndex = 2;
            this.Btn_Close.Text = "閉じる";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Frm090_CutStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 87);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_CutStoreInvInfo);
            this.Controls.Add(this.Btn_CutStoreDelv);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm090_CutStore";
            this.Text = "[KMD009SF] 切削ストア - Ver.230613.01a";
            this.ResumeLayout(false);

        }

        #endregion

        private Button Btn_CutStoreDelv;
        private Button Btn_CutStoreInvInfo;
        private Button Btn_Close;
    }
}