using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm052_PrintSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm052_PrintSettings));
            this.SuspendLayout();
            // 
            // Frm052_PrintSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(640, 273);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Frm052_PrintSettings";
            this.Text = "手配＋内示＋タナコン";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm052_FormsPrinting_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}