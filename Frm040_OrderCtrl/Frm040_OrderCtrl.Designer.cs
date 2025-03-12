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
            this.Btn_InfoPlan = new System.Windows.Forms.Button();
            this.Btn_ImportPlan = new System.Windows.Forms.Button();
            this.Btn_MfgProgress = new System.Windows.Forms.Button();
            this.Btn_InfoOrder = new System.Windows.Forms.Button();
            this.Btn_ImportOrder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Close
            // 
            this.Btn_Close.AutoSize = true;
            this.Btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Btn_Close.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_Close.Location = new System.Drawing.Point(318, 95);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(300, 38);
            this.Btn_Close.TabIndex = 13;
            this.Btn_Close.Text = "閉じる";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Btn_InfoPlan
            // 
            this.Btn_InfoPlan.Enabled = false;
            this.Btn_InfoPlan.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_InfoPlan.Location = new System.Drawing.Point(317, 53);
            this.Btn_InfoPlan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_InfoPlan.Name = "Btn_InfoPlan";
            this.Btn_InfoPlan.Size = new System.Drawing.Size(300, 38);
            this.Btn_InfoPlan.TabIndex = 12;
            this.Btn_InfoPlan.Text = "内示情報";
            this.Btn_InfoPlan.UseVisualStyleBackColor = true;
            this.Btn_InfoPlan.Click += new System.EventHandler(this.Btn_MfgProgress_Click);
            // 
            // Btn_ImportPlan
            // 
            this.Btn_ImportPlan.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_ImportPlan.Location = new System.Drawing.Point(317, 11);
            this.Btn_ImportPlan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_ImportPlan.Name = "Btn_ImportPlan";
            this.Btn_ImportPlan.Size = new System.Drawing.Size(300, 38);
            this.Btn_ImportPlan.TabIndex = 11;
            this.Btn_ImportPlan.Text = "内示取込印刷";
            this.Btn_ImportPlan.UseVisualStyleBackColor = true;
            this.Btn_ImportPlan.Click += new System.EventHandler(this.Btn_CreateMaybeOrder_Click);
            // 
            // Btn_MfgProgress
            // 
            this.Btn_MfgProgress.Enabled = false;
            this.Btn_MfgProgress.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_MfgProgress.Location = new System.Drawing.Point(12, 95);
            this.Btn_MfgProgress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_MfgProgress.Name = "Btn_MfgProgress";
            this.Btn_MfgProgress.Size = new System.Drawing.Size(300, 38);
            this.Btn_MfgProgress.TabIndex = 10;
            this.Btn_MfgProgress.Text = "加工進捗情報表示";
            this.Btn_MfgProgress.UseVisualStyleBackColor = true;
            this.Btn_MfgProgress.Click += new System.EventHandler(this.Btn_CreateOrder_Click);
            // 
            // Btn_InfoOrder
            // 
            this.Btn_InfoOrder.Enabled = false;
            this.Btn_InfoOrder.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_InfoOrder.Location = new System.Drawing.Point(12, 53);
            this.Btn_InfoOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_InfoOrder.Name = "Btn_InfoOrder";
            this.Btn_InfoOrder.Size = new System.Drawing.Size(300, 38);
            this.Btn_InfoOrder.TabIndex = 9;
            this.Btn_InfoOrder.Text = "手配情報";
            this.Btn_InfoOrder.UseVisualStyleBackColor = true;
            this.Btn_InfoOrder.Click += new System.EventHandler(this.Btn_OrderEqualize_Click);
            // 
            // Btn_ImportOrder
            // 
            this.Btn_ImportOrder.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_ImportOrder.Location = new System.Drawing.Point(12, 11);
            this.Btn_ImportOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_ImportOrder.Name = "Btn_ImportOrder";
            this.Btn_ImportOrder.Size = new System.Drawing.Size(300, 38);
            this.Btn_ImportOrder.TabIndex = 8;
            this.Btn_ImportOrder.Text = "手配取込印刷";
            this.Btn_ImportOrder.UseVisualStyleBackColor = true;
            this.Btn_ImportOrder.Click += new System.EventHandler(this.Btn_CreateOrder_Click);
            // 
            // Frm040_OrderCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(629, 145);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_InfoPlan);
            this.Controls.Add(this.Btn_ImportPlan);
            this.Controls.Add(this.Btn_MfgProgress);
            this.Controls.Add(this.Btn_InfoOrder);
            this.Controls.Add(this.Btn_ImportOrder);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm040_OrderCtrl";
            this.Text = "[KMD004SF] オーダー管理 - Ver.230613.01a";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button Btn_Close;
        private Button Btn_InfoPlan;
        private Button Btn_ImportPlan;
        private Button Btn_MfgProgress;
        private Button Btn_InfoOrder;
        private Button Btn_ImportOrder;
    }
}