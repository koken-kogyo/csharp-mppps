﻿using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm043_CreateAddOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm043_CreateAddOrder));
            SuspendLayout();
            // 
            // Frm043_CreateAddOrder
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Frm043_CreateAddOrder";
            Text = "[KMD004SF] オーダー管理 - 追加オーダーの作成 - Ver.230613.01a";
            ResumeLayout(false);
        }

        #endregion
    }
}