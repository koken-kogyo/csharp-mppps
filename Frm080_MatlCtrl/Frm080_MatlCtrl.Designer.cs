using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm080_MatlCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm080_MatlCtrl));
            this.Btn_MatlInvList = new System.Windows.Forms.Button();
            this.Btn_MatlOrder = new System.Windows.Forms.Button();
            this.Btn_MatlInsp = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_MatlInvList
            // 
            this.Btn_MatlInvList.Enabled = false;
            this.Btn_MatlInvList.Location = new System.Drawing.Point(14, 14);
            this.Btn_MatlInvList.Name = "Btn_MatlInvList";
            this.Btn_MatlInvList.Size = new System.Drawing.Size(281, 44);
            this.Btn_MatlInvList.TabIndex = 0;
            this.Btn_MatlInvList.Text = "材料在庫一覧";
            this.Btn_MatlInvList.UseVisualStyleBackColor = true;
            this.Btn_MatlInvList.Click += new System.EventHandler(this.Btn_MatlInvList_Click_1);
            // 
            // Btn_MatlOrder
            // 
            this.Btn_MatlOrder.Enabled = false;
            this.Btn_MatlOrder.Location = new System.Drawing.Point(14, 64);
            this.Btn_MatlOrder.Name = "Btn_MatlOrder";
            this.Btn_MatlOrder.Size = new System.Drawing.Size(281, 44);
            this.Btn_MatlOrder.TabIndex = 1;
            this.Btn_MatlOrder.Text = "材料発注処理";
            this.Btn_MatlOrder.UseVisualStyleBackColor = true;
            this.Btn_MatlOrder.Click += new System.EventHandler(this.Btn_MatlOrder_Click_1);
            // 
            // Btn_MatlInsp
            // 
            this.Btn_MatlInsp.Enabled = false;
            this.Btn_MatlInsp.Location = new System.Drawing.Point(302, 14);
            this.Btn_MatlInsp.Name = "Btn_MatlInsp";
            this.Btn_MatlInsp.Size = new System.Drawing.Size(281, 44);
            this.Btn_MatlInsp.TabIndex = 2;
            this.Btn_MatlInsp.Text = "材料検収";
            this.Btn_MatlInsp.UseVisualStyleBackColor = true;
            this.Btn_MatlInsp.Click += new System.EventHandler(this.Btn_MatlInsp_Click_1);
            // 
            // Btn_Close
            // 
            this.Btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Btn_Close.Location = new System.Drawing.Point(302, 64);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(281, 44);
            this.Btn_Close.TabIndex = 3;
            this.Btn_Close.Text = "閉じる";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Frm080_MatlCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 133);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_MatlInsp);
            this.Controls.Add(this.Btn_MatlOrder);
            this.Controls.Add(this.Btn_MatlInvList);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm080_MatlCtrl";
            this.Text = "[KMD008SF] 材料管理 - Ver.230613.01a";
            this.ResumeLayout(false);

        }

        #endregion

        private Button Btn_MatlInvList;
        private Button Btn_MatlOrder;
        private Button Btn_MatlInsp;
        private Button Btn_Close;
    }
}