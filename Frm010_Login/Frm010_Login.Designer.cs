using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm010_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm010_Login));
            this.Pbx_CostMgt = new System.Windows.Forms.PictureBox();
            this.Lbl_DestDB = new System.Windows.Forms.Label();
            this.Lbl_UserId = new System.Windows.Forms.Label();
            this.Lbl_Passwd = new System.Windows.Forms.Label();
            this.Chk_MemUserId = new System.Windows.Forms.CheckBox();
            this.Cbx_OracleVer = new System.Windows.Forms.ComboBox();
            this.Cbx_Schema = new System.Windows.Forms.ComboBox();
            this.Tbx_UserId = new System.Windows.Forms.TextBox();
            this.Tbx_Passwd = new System.Windows.Forms.TextBox();
            this.Btn_OK = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_CostMgt)).BeginInit();
            this.SuspendLayout();
            // 
            // Pbx_CostMgt
            // 
            this.Pbx_CostMgt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Pbx_CostMgt.BackgroundImage")));
            this.Pbx_CostMgt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pbx_CostMgt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pbx_CostMgt.Location = new System.Drawing.Point(16, 10);
            this.Pbx_CostMgt.Margin = new System.Windows.Forms.Padding(4);
            this.Pbx_CostMgt.Name = "Pbx_CostMgt";
            this.Pbx_CostMgt.Size = new System.Drawing.Size(343, 204);
            this.Pbx_CostMgt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pbx_CostMgt.TabIndex = 0;
            this.Pbx_CostMgt.TabStop = false;
            // 
            // Lbl_DestDB
            // 
            this.Lbl_DestDB.AutoSize = true;
            this.Lbl_DestDB.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Lbl_DestDB.Location = new System.Drawing.Point(380, 29);
            this.Lbl_DestDB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_DestDB.Name = "Lbl_DestDB";
            this.Lbl_DestDB.Size = new System.Drawing.Size(57, 20);
            this.Lbl_DestDB.TabIndex = 6;
            this.Lbl_DestDB.Text = "接続先:";
            // 
            // Lbl_UserId
            // 
            this.Lbl_UserId.AutoSize = true;
            this.Lbl_UserId.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Lbl_UserId.Location = new System.Drawing.Point(380, 101);
            this.Lbl_UserId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_UserId.Name = "Lbl_UserId";
            this.Lbl_UserId.Size = new System.Drawing.Size(75, 20);
            this.Lbl_UserId.TabIndex = 9;
            this.Lbl_UserId.Text = "ユーザー ID:";
            // 
            // Lbl_Passwd
            // 
            this.Lbl_Passwd.AutoSize = true;
            this.Lbl_Passwd.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Lbl_Passwd.Location = new System.Drawing.Point(380, 138);
            this.Lbl_Passwd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_Passwd.Name = "Lbl_Passwd";
            this.Lbl_Passwd.Size = new System.Drawing.Size(67, 20);
            this.Lbl_Passwd.TabIndex = 1;
            this.Lbl_Passwd.Text = "パスワード:";
            // 
            // Chk_MemUserId
            // 
            this.Chk_MemUserId.AutoSize = true;
            this.Chk_MemUserId.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Chk_MemUserId.Location = new System.Drawing.Point(384, 166);
            this.Chk_MemUserId.Margin = new System.Windows.Forms.Padding(4);
            this.Chk_MemUserId.Name = "Chk_MemUserId";
            this.Chk_MemUserId.Size = new System.Drawing.Size(159, 24);
            this.Chk_MemUserId.TabIndex = 3;
            this.Chk_MemUserId.Text = "ユーザー ID を記憶する";
            this.Chk_MemUserId.UseVisualStyleBackColor = true;
            this.Chk_MemUserId.CheckedChanged += new System.EventHandler(this.Chk_MemUserId_CheckedChanged);
            // 
            // Cbx_OracleVer
            // 
            this.Cbx_OracleVer.BackColor = System.Drawing.SystemColors.Window;
            this.Cbx_OracleVer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_OracleVer.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Cbx_OracleVer.FormattingEnabled = true;
            this.Cbx_OracleVer.Items.AddRange(new object[] {
            "Oracle 10g",
            "Oracle 11g",
            "Oracle 19c"});
            this.Cbx_OracleVer.Location = new System.Drawing.Point(472, 23);
            this.Cbx_OracleVer.Margin = new System.Windows.Forms.Padding(4);
            this.Cbx_OracleVer.Name = "Cbx_OracleVer";
            this.Cbx_OracleVer.Size = new System.Drawing.Size(134, 29);
            this.Cbx_OracleVer.TabIndex = 7;
            // 
            // Cbx_Schema
            // 
            this.Cbx_Schema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_Schema.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Cbx_Schema.FormattingEnabled = true;
            this.Cbx_Schema.Items.AddRange(new object[] {
            "KOKEN_1",
            "KOKEN_7",
            "KOKEN_3",
            "KOKEN_QA",
            "KOKEN_Y",
            "KOKEN_5"});
            this.Cbx_Schema.Location = new System.Drawing.Point(472, 60);
            this.Cbx_Schema.Margin = new System.Windows.Forms.Padding(4);
            this.Cbx_Schema.Name = "Cbx_Schema";
            this.Cbx_Schema.Size = new System.Drawing.Size(134, 29);
            this.Cbx_Schema.TabIndex = 8;
            // 
            // Tbx_UserId
            // 
            this.Tbx_UserId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Tbx_UserId.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Tbx_UserId.Location = new System.Drawing.Point(472, 98);
            this.Tbx_UserId.Margin = new System.Windows.Forms.Padding(4);
            this.Tbx_UserId.Name = "Tbx_UserId";
            this.Tbx_UserId.Size = new System.Drawing.Size(134, 29);
            this.Tbx_UserId.TabIndex = 0;
            this.Tbx_UserId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tbx_UserId_KeyDown);
            // 
            // Tbx_Passwd
            // 
            this.Tbx_Passwd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Tbx_Passwd.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Tbx_Passwd.Location = new System.Drawing.Point(472, 135);
            this.Tbx_Passwd.Margin = new System.Windows.Forms.Padding(4);
            this.Tbx_Passwd.Name = "Tbx_Passwd";
            this.Tbx_Passwd.Size = new System.Drawing.Size(134, 29);
            this.Tbx_Passwd.TabIndex = 2;
            this.Tbx_Passwd.UseSystemPasswordChar = true;
            this.Tbx_Passwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tbx_Passwd_KeyDown);
            // 
            // Btn_OK
            // 
            this.Btn_OK.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_OK.Location = new System.Drawing.Point(394, 208);
            this.Btn_OK.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(100, 30);
            this.Btn_OK.TabIndex = 4;
            this.Btn_OK.Text = "OK";
            this.Btn_OK.UseVisualStyleBackColor = true;
            this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_Cancel.Location = new System.Drawing.Point(500, 208);
            this.Btn_Cancel.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(100, 30);
            this.Btn_Cancel.TabIndex = 5;
            this.Btn_Cancel.Text = "キャンセル";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Frm010_Login
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(621, 251);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_OK);
            this.Controls.Add(this.Tbx_Passwd);
            this.Controls.Add(this.Tbx_UserId);
            this.Controls.Add(this.Cbx_Schema);
            this.Controls.Add(this.Cbx_OracleVer);
            this.Controls.Add(this.Chk_MemUserId);
            this.Controls.Add(this.Lbl_Passwd);
            this.Controls.Add(this.Lbl_UserId);
            this.Controls.Add(this.Lbl_DestDB);
            this.Controls.Add(this.Pbx_CostMgt);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm010_Login";
            this.Text = "[KMD001SF] 切削生産計画システム - Ver.yyMMdd.99 <#01: ログイン>";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm010_Login_FormClosing);
            this.Load += new System.EventHandler(this.Frm010_Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_CostMgt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Pbx_CostMgt;
        private System.Windows.Forms.Label Lbl_DestDB;
        private System.Windows.Forms.Label Lbl_UserId;
        private System.Windows.Forms.Label Lbl_Passwd;
        private System.Windows.Forms.CheckBox Chk_MemUserId;
        private System.Windows.Forms.ComboBox Cbx_OracleVer;
        private System.Windows.Forms.ComboBox Cbx_Schema;
        private System.Windows.Forms.TextBox Tbx_UserId;
        private System.Windows.Forms.TextBox Tbx_Passwd;
        private System.Windows.Forms.Button Btn_OK;
        private System.Windows.Forms.Button Btn_Cancel;
    }
}