using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm020_MainMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm020_MainMenu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.印刷PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.マスタメンテナンスMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KM8400切削生産計画システム利用者マスタToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KM8410切削刃具マスタ1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KM8420切削設備マスタ2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KM8430切削コード票マスタ3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ヘルプHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.バージョン情報VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_MasterMaint = new System.Windows.Forms.Button();
            this.Btn_OrderCtrl = new System.Windows.Forms.Button();
            this.Btn_MfgCtrl = new System.Windows.Forms.Button();
            this.Btn_ReceiptCtrl = new System.Windows.Forms.Button();
            this.Btn_MatlCtrl = new System.Windows.Forms.Button();
            this.Btn_CutStore = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.Lbl_User = new System.Windows.Forms.Label();
            this.Lbl_UserName = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.編集EToolStripMenuItem,
            this.マスタメンテナンスMToolStripMenuItem,
            this.ヘルプHToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(530, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.印刷PToolStripMenuItem,
            this.終了XToolStripMenuItem});
            this.ファイルFToolStripMenuItem.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 印刷PToolStripMenuItem
            // 
            this.印刷PToolStripMenuItem.Name = "印刷PToolStripMenuItem";
            this.印刷PToolStripMenuItem.Size = new System.Drawing.Size(141, 26);
            this.印刷PToolStripMenuItem.Text = "印刷(&P)";
            // 
            // 終了XToolStripMenuItem
            // 
            this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
            this.終了XToolStripMenuItem.Size = new System.Drawing.Size(141, 26);
            this.終了XToolStripMenuItem.Text = "終了(&X)";
            // 
            // 編集EToolStripMenuItem
            // 
            this.編集EToolStripMenuItem.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
            this.編集EToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.編集EToolStripMenuItem.Text = "編集(&E)";
            // 
            // マスタメンテナンスMToolStripMenuItem
            // 
            this.マスタメンテナンスMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KM8400切削生産計画システム利用者マスタToolStripMenuItem,
            this.KM8410切削刃具マスタ1ToolStripMenuItem,
            this.KM8420切削設備マスタ2ToolStripMenuItem,
            this.KM8430切削コード票マスタ3ToolStripMenuItem});
            this.マスタメンテナンスMToolStripMenuItem.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.マスタメンテナンスMToolStripMenuItem.Name = "マスタメンテナンスMToolStripMenuItem";
            this.マスタメンテナンスMToolStripMenuItem.Size = new System.Drawing.Size(149, 24);
            this.マスタメンテナンスMToolStripMenuItem.Text = "マスタ メンテナンス(&M)";
            // 
            // KM8400切削生産計画システム利用者マスタToolStripMenuItem
            // 
            this.KM8400切削生産計画システム利用者マスタToolStripMenuItem.Name = "KM8400切削生産計画システム利用者マスタToolStripMenuItem";
            this.KM8400切削生産計画システム利用者マスタToolStripMenuItem.Size = new System.Drawing.Size(382, 26);
            this.KM8400切削生産計画システム利用者マスタToolStripMenuItem.Text = "KM8400 切削生産計画システム利用者マスタ(&0)";
            // 
            // KM8410切削刃具マスタ1ToolStripMenuItem
            // 
            this.KM8410切削刃具マスタ1ToolStripMenuItem.Name = "KM8410切削刃具マスタ1ToolStripMenuItem";
            this.KM8410切削刃具マスタ1ToolStripMenuItem.Size = new System.Drawing.Size(382, 26);
            this.KM8410切削刃具マスタ1ToolStripMenuItem.Text = "KM8410 切削刃具マスタ(&1)";
            // 
            // KM8420切削設備マスタ2ToolStripMenuItem
            // 
            this.KM8420切削設備マスタ2ToolStripMenuItem.Name = "KM8420切削設備マスタ2ToolStripMenuItem";
            this.KM8420切削設備マスタ2ToolStripMenuItem.Size = new System.Drawing.Size(382, 26);
            this.KM8420切削設備マスタ2ToolStripMenuItem.Text = "KM8420 切削設備マスタ(&2)";
            // 
            // KM8430切削コード票マスタ3ToolStripMenuItem
            // 
            this.KM8430切削コード票マスタ3ToolStripMenuItem.Name = "KM8430切削コード票マスタ3ToolStripMenuItem";
            this.KM8430切削コード票マスタ3ToolStripMenuItem.Size = new System.Drawing.Size(382, 26);
            this.KM8430切削コード票マスタ3ToolStripMenuItem.Text = "KM8430 切削コード票マスタ(&3)";
            // 
            // ヘルプHToolStripMenuItem
            // 
            this.ヘルプHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.バージョン情報VToolStripMenuItem});
            this.ヘルプHToolStripMenuItem.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
            this.ヘルプHToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // バージョン情報VToolStripMenuItem
            // 
            this.バージョン情報VToolStripMenuItem.Name = "バージョン情報VToolStripMenuItem";
            this.バージョン情報VToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.バージョン情報VToolStripMenuItem.Text = "バージョン情報(&V)";
            // 
            // Btn_MasterMaint
            // 
            this.Btn_MasterMaint.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_MasterMaint.Location = new System.Drawing.Point(12, 36);
            this.Btn_MasterMaint.Name = "Btn_MasterMaint";
            this.Btn_MasterMaint.Size = new System.Drawing.Size(250, 29);
            this.Btn_MasterMaint.TabIndex = 1;
            this.Btn_MasterMaint.Text = "マスタ メンテナンス";
            this.Btn_MasterMaint.UseVisualStyleBackColor = true;
            this.Btn_MasterMaint.Click += new System.EventHandler(this.Btn_MasterMaint_Click);
            // 
            // Btn_OrderCtrl
            // 
            this.Btn_OrderCtrl.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_OrderCtrl.Location = new System.Drawing.Point(12, 71);
            this.Btn_OrderCtrl.Name = "Btn_OrderCtrl";
            this.Btn_OrderCtrl.Size = new System.Drawing.Size(250, 29);
            this.Btn_OrderCtrl.TabIndex = 2;
            this.Btn_OrderCtrl.Text = "オーダー管理";
            this.Btn_OrderCtrl.UseVisualStyleBackColor = true;
            this.Btn_OrderCtrl.Click += new System.EventHandler(this.Btn_OrderCtrl_Click);
            // 
            // Btn_MfgCtrl
            // 
            this.Btn_MfgCtrl.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_MfgCtrl.Location = new System.Drawing.Point(12, 106);
            this.Btn_MfgCtrl.Name = "Btn_MfgCtrl";
            this.Btn_MfgCtrl.Size = new System.Drawing.Size(250, 29);
            this.Btn_MfgCtrl.TabIndex = 3;
            this.Btn_MfgCtrl.Text = "製造管理";
            this.Btn_MfgCtrl.UseVisualStyleBackColor = true;
            this.Btn_MfgCtrl.Click += new System.EventHandler(this.Btn_MfgCtrl_Click);
            // 
            // Btn_ReceiptCtrl
            // 
            this.Btn_ReceiptCtrl.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_ReceiptCtrl.Location = new System.Drawing.Point(12, 141);
            this.Btn_ReceiptCtrl.Name = "Btn_ReceiptCtrl";
            this.Btn_ReceiptCtrl.Size = new System.Drawing.Size(250, 29);
            this.Btn_ReceiptCtrl.TabIndex = 4;
            this.Btn_ReceiptCtrl.Text = "実績管理";
            this.Btn_ReceiptCtrl.UseVisualStyleBackColor = true;
            this.Btn_ReceiptCtrl.Click += new System.EventHandler(this.Btn_ReceiptCtrl_Click);
            // 
            // Btn_MatlCtrl
            // 
            this.Btn_MatlCtrl.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_MatlCtrl.Location = new System.Drawing.Point(268, 71);
            this.Btn_MatlCtrl.Name = "Btn_MatlCtrl";
            this.Btn_MatlCtrl.Size = new System.Drawing.Size(250, 29);
            this.Btn_MatlCtrl.TabIndex = 5;
            this.Btn_MatlCtrl.Text = "材料管理";
            this.Btn_MatlCtrl.UseVisualStyleBackColor = true;
            this.Btn_MatlCtrl.Click += new System.EventHandler(this.Btn_MatlCtrl_Click);
            // 
            // Btn_CutStore
            // 
            this.Btn_CutStore.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_CutStore.Location = new System.Drawing.Point(268, 106);
            this.Btn_CutStore.Name = "Btn_CutStore";
            this.Btn_CutStore.Size = new System.Drawing.Size(250, 29);
            this.Btn_CutStore.TabIndex = 6;
            this.Btn_CutStore.Text = "切削ストア";
            this.Btn_CutStore.UseVisualStyleBackColor = true;
            this.Btn_CutStore.Click += new System.EventHandler(this.Btn_CutStore_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Btn_Close.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.Btn_Close.Location = new System.Drawing.Point(268, 141);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(250, 29);
            this.Btn_Close.TabIndex = 7;
            this.Btn_Close.Text = "終了";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Lbl_User
            // 
            this.Lbl_User.AutoSize = true;
            this.Lbl_User.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_User.Location = new System.Drawing.Point(317, 36);
            this.Lbl_User.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_User.Name = "Lbl_User";
            this.Lbl_User.Size = new System.Drawing.Size(55, 15);
            this.Lbl_User.TabIndex = 8;
            this.Lbl_User.Text = "利用者:";
            // 
            // Lbl_UserName
            // 
            this.Lbl_UserName.AutoSize = true;
            this.Lbl_UserName.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_UserName.Location = new System.Drawing.Point(380, 36);
            this.Lbl_UserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_UserName.Name = "Lbl_UserName";
            this.Lbl_UserName.Size = new System.Drawing.Size(92, 15);
            this.Lbl_UserName.TabIndex = 9;
            this.Lbl_UserName.Text = "氏名 (99999)";
            // 
            // Frm020_MainMenu
            // 
            this.ClientSize = new System.Drawing.Size(530, 182);
            this.Controls.Add(this.Lbl_UserName);
            this.Controls.Add(this.Lbl_User);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_CutStore);
            this.Controls.Add(this.Btn_MatlCtrl);
            this.Controls.Add(this.Btn_ReceiptCtrl);
            this.Controls.Add(this.Btn_MfgCtrl);
            this.Controls.Add(this.Btn_OrderCtrl);
            this.Controls.Add(this.Btn_MasterMaint);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Frm020_MainMenu";
            this.Text = "[KMD001SF] 切削生産計画システム - メイン メニュー - Ver.230613.01a";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private MenuStrip menuStrip1;
        private ToolStripMenuItem ファイルFToolStripMenuItem;
        private ToolStripMenuItem 編集EToolStripMenuItem;
        private ToolStripMenuItem マスタメンテナンスMToolStripMenuItem;
        private ToolStripMenuItem ヘルプHToolStripMenuItem;
        private Button Btn_MasterMaint;
        private Button Btn_OrderCtrl;
        private Button Btn_MfgCtrl;
        private Button Btn_ReceiptCtrl;
        private Button Btn_MatlCtrl;
        private Button Btn_CutStore;
        private Button Btn_Close;
        private ToolStripMenuItem 印刷PToolStripMenuItem;
        private ToolStripMenuItem 終了XToolStripMenuItem;
        private ToolStripMenuItem KM8400切削生産計画システム利用者マスタToolStripMenuItem;
        private ToolStripMenuItem KM8410切削刃具マスタ1ToolStripMenuItem;
        private ToolStripMenuItem KM8420切削設備マスタ2ToolStripMenuItem;
        private ToolStripMenuItem KM8430切削コード票マスタ3ToolStripMenuItem;
        private ToolStripMenuItem バージョン情報VToolStripMenuItem;
        private Label Lbl_User;
        private Label Lbl_UserName;
    }
}