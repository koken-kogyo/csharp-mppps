using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm030_MasterMaint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm030_MasterMaint));
            this.Btn_Close = new System.Windows.Forms.Button();
            this.Btn_ChipMstMaint = new System.Windows.Forms.Button();
            this.Btn_EqMstMaint = new System.Windows.Forms.Button();
            this.Btn_CodeSlipMstMaint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Close
            // 
            this.Btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Btn_Close.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_Close.Location = new System.Drawing.Point(268, 42);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(250, 29);
            this.Btn_Close.TabIndex = 8;
            this.Btn_Close.Text = "閉じる";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Btn_ChipMstMaint
            // 
            this.Btn_ChipMstMaint.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_ChipMstMaint.Location = new System.Drawing.Point(12, 42);
            this.Btn_ChipMstMaint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_ChipMstMaint.Name = "Btn_ChipMstMaint";
            this.Btn_ChipMstMaint.Size = new System.Drawing.Size(250, 29);
            this.Btn_ChipMstMaint.TabIndex = 10;
            this.Btn_ChipMstMaint.Text = "刃具マスタ メンテ";
            this.Btn_ChipMstMaint.UseVisualStyleBackColor = true;
            // 
            // Btn_EqMstMaint
            // 
            this.Btn_EqMstMaint.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_EqMstMaint.Location = new System.Drawing.Point(12, 9);
            this.Btn_EqMstMaint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_EqMstMaint.Name = "Btn_EqMstMaint";
            this.Btn_EqMstMaint.Size = new System.Drawing.Size(250, 29);
            this.Btn_EqMstMaint.TabIndex = 9;
            this.Btn_EqMstMaint.Text = "設備マスタ メンテ";
            this.Btn_EqMstMaint.UseVisualStyleBackColor = true;
            // 
            // Btn_CodeSlipMstMaint
            // 
            this.Btn_CodeSlipMstMaint.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_CodeSlipMstMaint.Location = new System.Drawing.Point(268, 9);
            this.Btn_CodeSlipMstMaint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_CodeSlipMstMaint.Name = "Btn_CodeSlipMstMaint";
            this.Btn_CodeSlipMstMaint.Size = new System.Drawing.Size(250, 29);
            this.Btn_CodeSlipMstMaint.TabIndex = 11;
            this.Btn_CodeSlipMstMaint.Text = "コード票マスタ メンテ";
            this.Btn_CodeSlipMstMaint.UseVisualStyleBackColor = true;
            // 
            // Frm030_MasterMaint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 91);
            this.Controls.Add(this.Btn_CodeSlipMstMaint);
            this.Controls.Add(this.Btn_ChipMstMaint);
            this.Controls.Add(this.Btn_EqMstMaint);
            this.Controls.Add(this.Btn_Close);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm030_MasterMaint";
            this.Text = "[KMD003SF] マスタ メンテナンス - Ver.230613.01a";
            this.ResumeLayout(false);

        }

        #endregion

        private Button Btn_Close;
        private Button Btn_ChipMstMaint;
        private Button Btn_EqMstMaint;
        private Button Btn_CodeSlipMstMaint;
    }
}