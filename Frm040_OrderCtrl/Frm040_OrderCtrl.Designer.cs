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
            this.Btn_InformationPlan = new System.Windows.Forms.Button();
            this.Btn_ImportPlan = new System.Windows.Forms.Button();
            this.Btn_CreateAddOrder = new System.Windows.Forms.Button();
            this.Btn_InformationOrder = new System.Windows.Forms.Button();
            this.Btn_ImportOrder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Close
            // 
            this.Btn_Close.AutoSize = true;
            this.Btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Btn_Close.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_Close.Location = new System.Drawing.Point(12, 221);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(300, 38);
            this.Btn_Close.TabIndex = 5;
            this.Btn_Close.Text = "閉じる";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Btn_InformationPlan
            // 
            this.Btn_InformationPlan.Enabled = false;
            this.Btn_InformationPlan.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_InformationPlan.Location = new System.Drawing.Point(12, 137);
            this.Btn_InformationPlan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_InformationPlan.Name = "Btn_InformationPlan";
            this.Btn_InformationPlan.Size = new System.Drawing.Size(300, 38);
            this.Btn_InformationPlan.TabIndex = 3;
            this.Btn_InformationPlan.Text = "内示検索";
            this.Btn_InformationPlan.UseVisualStyleBackColor = true;
            this.Btn_InformationPlan.Click += new System.EventHandler(this.Btn_InformationPlan_Click);
            // 
            // Btn_ImportPlan
            // 
            this.Btn_ImportPlan.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_ImportPlan.Location = new System.Drawing.Point(12, 53);
            this.Btn_ImportPlan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_ImportPlan.Name = "Btn_ImportPlan";
            this.Btn_ImportPlan.Size = new System.Drawing.Size(300, 38);
            this.Btn_ImportPlan.TabIndex = 1;
            this.Btn_ImportPlan.Text = "内示情報";
            this.Btn_ImportPlan.UseVisualStyleBackColor = true;
            this.Btn_ImportPlan.Click += new System.EventHandler(this.Btn_ImportPlan_Click);
            // 
            // Btn_CreateAddOrder
            // 
            this.Btn_CreateAddOrder.Enabled = false;
            this.Btn_CreateAddOrder.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_CreateAddOrder.Location = new System.Drawing.Point(12, 179);
            this.Btn_CreateAddOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_CreateAddOrder.Name = "Btn_CreateAddOrder";
            this.Btn_CreateAddOrder.Size = new System.Drawing.Size(300, 38);
            this.Btn_CreateAddOrder.TabIndex = 4;
            this.Btn_CreateAddOrder.Text = "追加オーダーの入力";
            this.Btn_CreateAddOrder.UseVisualStyleBackColor = true;
            this.Btn_CreateAddOrder.Click += new System.EventHandler(this.Btn_CreateAddOrder_Click);
            // 
            // Btn_InformationOrder
            // 
            this.Btn_InformationOrder.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_InformationOrder.Location = new System.Drawing.Point(12, 95);
            this.Btn_InformationOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_InformationOrder.Name = "Btn_InformationOrder";
            this.Btn_InformationOrder.Size = new System.Drawing.Size(300, 38);
            this.Btn_InformationOrder.TabIndex = 2;
            this.Btn_InformationOrder.Text = "手配検索";
            this.Btn_InformationOrder.UseVisualStyleBackColor = true;
            this.Btn_InformationOrder.Click += new System.EventHandler(this.Btn_InformationOrder_Click);
            // 
            // Btn_ImportOrder
            // 
            this.Btn_ImportOrder.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_ImportOrder.Location = new System.Drawing.Point(12, 11);
            this.Btn_ImportOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_ImportOrder.Name = "Btn_ImportOrder";
            this.Btn_ImportOrder.Size = new System.Drawing.Size(300, 38);
            this.Btn_ImportOrder.TabIndex = 0;
            this.Btn_ImportOrder.Text = "手配情報";
            this.Btn_ImportOrder.UseVisualStyleBackColor = true;
            this.Btn_ImportOrder.Click += new System.EventHandler(this.Btn_ImportOrder_Click);
            // 
            // Frm040_OrderCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(322, 268);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_InformationPlan);
            this.Controls.Add(this.Btn_ImportPlan);
            this.Controls.Add(this.Btn_CreateAddOrder);
            this.Controls.Add(this.Btn_InformationOrder);
            this.Controls.Add(this.Btn_ImportOrder);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm040_OrderCtrl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "[KMD004SF] オーダー管理 - Ver.230613.01a";
            this.Activated += new System.EventHandler(this.Frm040_OrderCtrl_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm040_OrderCtrl_FormClosing);
            this.Load += new System.EventHandler(this.Frm040_OrderCtrl_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm040_OrderCtrl_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button Btn_Close;
        private Button Btn_InformationPlan;
        private Button Btn_ImportPlan;
        private Button Btn_CreateAddOrder;
        private Button Btn_InformationOrder;
        private Button Btn_ImportOrder;
    }
}