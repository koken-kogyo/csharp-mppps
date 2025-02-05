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
            this.Btn_OrderEqualize = new System.Windows.Forms.Button();
            this.Btn_CreateOrder = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_OrderEqualize
            // 
            this.Btn_OrderEqualize.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_OrderEqualize.Location = new System.Drawing.Point(12, 42);
            this.Btn_OrderEqualize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_OrderEqualize.Name = "Btn_OrderEqualize";
            this.Btn_OrderEqualize.Size = new System.Drawing.Size(250, 29);
            this.Btn_OrderEqualize.TabIndex = 14;
            this.Btn_OrderEqualize.Text = "帳票出力";
            this.Btn_OrderEqualize.UseVisualStyleBackColor = true;
            this.Btn_OrderEqualize.Click += new System.EventHandler(this.Btn_OrderEqualize_Click_1);
            // 
            // Btn_CreateOrder
            // 
            this.Btn_CreateOrder.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_CreateOrder.Location = new System.Drawing.Point(12, 9);
            this.Btn_CreateOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_CreateOrder.Name = "Btn_CreateOrder";
            this.Btn_CreateOrder.Size = new System.Drawing.Size(250, 29);
            this.Btn_CreateOrder.TabIndex = 13;
            this.Btn_CreateOrder.Text = "切削オーダー指示書";
            this.Btn_CreateOrder.UseVisualStyleBackColor = true;
            this.Btn_CreateOrder.Click += new System.EventHandler(this.Btn_CreateOrder_Click_1);
            // 
            // Btn_Close
            // 
            this.Btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Btn_Close.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_Close.Location = new System.Drawing.Point(268, 42);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(250, 29);
            this.Btn_Close.TabIndex = 15;
            this.Btn_Close.Text = "終了";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Frm050_MfgCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 89);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_OrderEqualize);
            this.Controls.Add(this.Btn_CreateOrder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm050_MfgCtrl";
            this.Text = "[KMD005SF] 製造管理 - Ver.230613.01a";
            this.ResumeLayout(false);

        }

        #endregion

        private Button Btn_OrderEqualize;
        private Button Btn_CreateOrder;
        private Button Btn_Close;
    }
}