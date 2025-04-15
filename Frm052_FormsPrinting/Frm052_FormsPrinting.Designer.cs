using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm052_FormsPrinting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm052_FormsPrinting));
            this.btn_All_Print = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_All_Print
            // 
            this.btn_All_Print.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.btn_All_Print.Location = new System.Drawing.Point(16, 15);
            this.btn_All_Print.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_All_Print.Name = "btn_All_Print";
            this.btn_All_Print.Size = new System.Drawing.Size(400, 48);
            this.btn_All_Print.TabIndex = 14;
            this.btn_All_Print.Text = "手配内示在庫一覧";
            this.btn_All_Print.UseVisualStyleBackColor = true;
            this.btn_All_Print.Click += new System.EventHandler(this.btn_All_Print_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.button1.Location = new System.Drawing.Point(16, 70);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(400, 48);
            this.button1.TabIndex = 15;
            this.button1.Text = "遅延リスト";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.button2.Location = new System.Drawing.Point(16, 125);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(400, 48);
            this.button2.TabIndex = 16;
            this.button2.Text = "工程別促進表";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.button3.Location = new System.Drawing.Point(16, 180);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(400, 48);
            this.button3.TabIndex = 17;
            this.button3.Text = "工程別内示票";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Frm052_FormsPrinting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 239);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_All_Print);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm052_FormsPrinting";
            this.Text = "[KMD005SF] 製造管理 - 帳票出力 - Ver.203418.01a";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm052_FormsPrinting_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btn_All_Print;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}