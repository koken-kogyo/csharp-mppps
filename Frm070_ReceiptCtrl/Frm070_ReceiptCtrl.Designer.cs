using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm070_ReceiptCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm070_ReceiptCtrl));
            this.Btn_ReceiptProc = new System.Windows.Forms.Button();
            this.Btn_ReceiptInfo = new System.Windows.Forms.Button();
            this.Btn_EntryShipResults = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_ReceiptProc
            // 
            this.Btn_ReceiptProc.Enabled = false;
            this.Btn_ReceiptProc.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.Btn_ReceiptProc.Location = new System.Drawing.Point(12, 12);
            this.Btn_ReceiptProc.Name = "Btn_ReceiptProc";
            this.Btn_ReceiptProc.Size = new System.Drawing.Size(251, 38);
            this.Btn_ReceiptProc.TabIndex = 0;
            this.Btn_ReceiptProc.Text = "切削ストア受入実績処理";
            this.Btn_ReceiptProc.UseVisualStyleBackColor = true;
            this.Btn_ReceiptProc.Click += new System.EventHandler(this.Btn_ReceiptProc_Click_1);
            // 
            // Btn_ReceiptInfo
            // 
            this.Btn_ReceiptInfo.Enabled = false;
            this.Btn_ReceiptInfo.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.Btn_ReceiptInfo.Location = new System.Drawing.Point(12, 57);
            this.Btn_ReceiptInfo.Name = "Btn_ReceiptInfo";
            this.Btn_ReceiptInfo.Size = new System.Drawing.Size(251, 38);
            this.Btn_ReceiptInfo.TabIndex = 1;
            this.Btn_ReceiptInfo.Text = "切削ストア受入実績情報表示";
            this.Btn_ReceiptInfo.UseVisualStyleBackColor = true;
            this.Btn_ReceiptInfo.Click += new System.EventHandler(this.Btn_ReceiptInfo_Click_1);
            // 
            // Btn_EntryShipResults
            // 
            this.Btn_EntryShipResults.Enabled = false;
            this.Btn_EntryShipResults.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.Btn_EntryShipResults.Location = new System.Drawing.Point(268, 12);
            this.Btn_EntryShipResults.Name = "Btn_EntryShipResults";
            this.Btn_EntryShipResults.Size = new System.Drawing.Size(251, 38);
            this.Btn_EntryShipResults.TabIndex = 2;
            this.Btn_EntryShipResults.Text = "EM への実績入力";
            this.Btn_EntryShipResults.UseVisualStyleBackColor = true;
            this.Btn_EntryShipResults.Click += new System.EventHandler(this.Btn_EntryShipResults_Click_1);
            // 
            // Btn_Close
            // 
            this.Btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Btn_Close.Location = new System.Drawing.Point(268, 57);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(251, 38);
            this.Btn_Close.TabIndex = 3;
            this.Btn_Close.Text = "閉じる";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Frm070_ReceiptCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 117);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_EntryShipResults);
            this.Controls.Add(this.Btn_ReceiptInfo);
            this.Controls.Add(this.Btn_ReceiptProc);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm070_ReceiptCtrl";
            this.Text = "[KMD007SF] 実績管理 - Ver.230613.01a";
            this.ResumeLayout(false);

        }

        #endregion

        private Button Btn_ReceiptProc;
        private Button Btn_ReceiptInfo;
        private Button Btn_EntryShipResults;
        private Button Btn_Close;
    }
}