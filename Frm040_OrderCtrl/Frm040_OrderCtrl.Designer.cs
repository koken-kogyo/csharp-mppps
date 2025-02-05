using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm040_OrderCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm040_OrderCtrl));
            this.Btn_Close = new System.Windows.Forms.Button();
            this.Btn_MfgProgress = new System.Windows.Forms.Button();
            this.Btn_OrderInfo = new System.Windows.Forms.Button();
            this.Btn_CreateAddOrder = new System.Windows.Forms.Button();
            this.Btn_OrderEqualize = new System.Windows.Forms.Button();
            this.Btn_CreateOrder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Close
            // 
            this.Btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Btn_Close.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_Close.Location = new System.Drawing.Point(268, 75);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(250, 29);
            this.Btn_Close.TabIndex = 13;
            this.Btn_Close.Text = "閉じる";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Btn_MfgProgress
            // 
            this.Btn_MfgProgress.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_MfgProgress.Location = new System.Drawing.Point(268, 42);
            this.Btn_MfgProgress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_MfgProgress.Name = "Btn_MfgProgress";
            this.Btn_MfgProgress.Size = new System.Drawing.Size(250, 29);
            this.Btn_MfgProgress.TabIndex = 12;
            this.Btn_MfgProgress.Text = "加工進捗情報表示";
            this.Btn_MfgProgress.UseVisualStyleBackColor = true;
            this.Btn_MfgProgress.Click += new System.EventHandler(this.Btn_MfgProgress_Click);
            // 
            // Btn_OrderInfo
            // 
            this.Btn_OrderInfo.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_OrderInfo.Location = new System.Drawing.Point(268, 9);
            this.Btn_OrderInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_OrderInfo.Name = "Btn_OrderInfo";
            this.Btn_OrderInfo.Size = new System.Drawing.Size(250, 29);
            this.Btn_OrderInfo.TabIndex = 11;
            this.Btn_OrderInfo.Text = "切削オーダー情報";
            this.Btn_OrderInfo.UseVisualStyleBackColor = true;
            this.Btn_OrderInfo.Click += new System.EventHandler(this.Btn_OrderInfo_Click);
            // 
            // Btn_CreateAddOrder
            // 
            this.Btn_CreateAddOrder.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_CreateAddOrder.Location = new System.Drawing.Point(12, 75);
            this.Btn_CreateAddOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_CreateAddOrder.Name = "Btn_CreateAddOrder";
            this.Btn_CreateAddOrder.Size = new System.Drawing.Size(250, 29);
            this.Btn_CreateAddOrder.TabIndex = 10;
            this.Btn_CreateAddOrder.Text = "追加オーダーの作成";
            this.Btn_CreateAddOrder.UseVisualStyleBackColor = true;
            this.Btn_CreateAddOrder.Click += new System.EventHandler(this.Btn_CreateOrder_Click);
            // 
            // Btn_OrderEqualize
            // 
            this.Btn_OrderEqualize.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_OrderEqualize.Location = new System.Drawing.Point(12, 42);
            this.Btn_OrderEqualize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_OrderEqualize.Name = "Btn_OrderEqualize";
            this.Btn_OrderEqualize.Size = new System.Drawing.Size(250, 29);
            this.Btn_OrderEqualize.TabIndex = 9;
            this.Btn_OrderEqualize.Text = "切削オーダーの平準化";
            this.Btn_OrderEqualize.UseVisualStyleBackColor = true;
            this.Btn_OrderEqualize.Click += new System.EventHandler(this.Btn_OrderEqualize_Click);
            // 
            // Btn_CreateOrder
            // 
            this.Btn_CreateOrder.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_CreateOrder.Location = new System.Drawing.Point(12, 9);
            this.Btn_CreateOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_CreateOrder.Name = "Btn_CreateOrder";
            this.Btn_CreateOrder.Size = new System.Drawing.Size(250, 29);
            this.Btn_CreateOrder.TabIndex = 8;
            this.Btn_CreateOrder.Text = "切削オーダーの作成";
            this.Btn_CreateOrder.UseVisualStyleBackColor = true;
            this.Btn_CreateOrder.Click += new System.EventHandler(this.Btn_CreateOrder_Click);
            // 
            // Frm040_OrderCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 126);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_MfgProgress);
            this.Controls.Add(this.Btn_OrderInfo);
            this.Controls.Add(this.Btn_CreateAddOrder);
            this.Controls.Add(this.Btn_OrderEqualize);
            this.Controls.Add(this.Btn_CreateOrder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm040_OrderCtrl";
            this.Text = "[KMD004SF] オーダー管理 - Ver.230613.01a";
            this.ResumeLayout(false);

        }

        #endregion

        private Button Btn_Close;
        private Button Btn_MfgProgress;
        private Button Btn_OrderInfo;
        private Button Btn_CreateAddOrder;
        private Button Btn_OrderEqualize;
        private Button Btn_CreateOrder;
    }
}