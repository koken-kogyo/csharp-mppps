namespace RegCycleTime
{
    partial class Frm24_RegCycleTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm24_RegCycleTime));
            this.Sst = new System.Windows.Forms.StatusStrip();
            this.Tsl_Msg = new System.Windows.Forms.ToolStripStatusLabel();
            this.Btn_Search = new System.Windows.Forms.Button();
            this.Btn_Delete = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.Btn_Clear = new System.Windows.Forms.Button();
            this.Btn_InsUpd = new System.Windows.Forms.Button();
            this.Tbc_FormType = new System.Windows.Forms.TabControl();
            this.Tbp_Slip = new System.Windows.Forms.TabPage();
            this.Gbx_Detail = new System.Windows.Forms.GroupBox();
            this.Tbx_Note = new System.Windows.Forms.TextBox();
            this.Lbl_Note = new System.Windows.Forms.Label();
            this.Lbl_CTUnit = new System.Windows.Forms.Label();
            this.Tbx_CT = new System.Windows.Forms.TextBox();
            this.Lbl_CT = new System.Windows.Forms.Label();
            this.Gbx_InqKey = new System.Windows.Forms.GroupBox();
            this.Lbl_WkGrCaution = new System.Windows.Forms.Label();
            this.Gbx_ValDtF = new System.Windows.Forms.GroupBox();
            this.Cbx_RegDt = new System.Windows.Forms.ComboBox();
            this.Rbt_DsnDt = new System.Windows.Forms.RadioButton();
            this.Rbt_RegDt = new System.Windows.Forms.RadioButton();
            this.Dtp_DsnDt = new System.Windows.Forms.DateTimePicker();
            this.Cbx_WkSeq = new System.Windows.Forms.ComboBox();
            this.Lbl_WkSeq = new System.Windows.Forms.Label();
            this.Cbx_Hm = new System.Windows.Forms.ComboBox();
            this.Lbl_Hm = new System.Windows.Forms.Label();
            this.Cbx_WkGr = new System.Windows.Forms.ComboBox();
            this.Cbx_Od = new System.Windows.Forms.ComboBox();
            this.Lbl_WkGr = new System.Windows.Forms.Label();
            this.Lbl_Od = new System.Windows.Forms.Label();
            this.Tbp_List = new System.Windows.Forms.TabPage();
            this.Lbl_BatchEdit = new System.Windows.Forms.Label();
            this.Dgv_CycleTmTbl = new System.Windows.Forms.DataGridView();
            this.Btn_SaveCsvFile = new System.Windows.Forms.Button();
            this.Btn_ReadCsvFile = new System.Windows.Forms.Button();
            this.Sst.SuspendLayout();
            this.Tbc_FormType.SuspendLayout();
            this.Tbp_Slip.SuspendLayout();
            this.Gbx_Detail.SuspendLayout();
            this.Gbx_InqKey.SuspendLayout();
            this.Gbx_ValDtF.SuspendLayout();
            this.Tbp_List.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CycleTmTbl)).BeginInit();
            this.SuspendLayout();
            // 
            // Sst
            // 
            this.Sst.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Sst.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tsl_Msg});
            this.Sst.Location = new System.Drawing.Point(0, 729);
            this.Sst.Name = "Sst";
            this.Sst.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.Sst.Size = new System.Drawing.Size(1182, 26);
            this.Sst.TabIndex = 8;
            this.Sst.Text = "Sst";
            // 
            // Tsl_Msg
            // 
            this.Tsl_Msg.Name = "Tsl_Msg";
            this.Tsl_Msg.Size = new System.Drawing.Size(59, 20);
            this.Tsl_Msg.Text = "Tsl_Msg";
            // 
            // Btn_Search
            // 
            this.Btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Search.Location = new System.Drawing.Point(748, 687);
            this.Btn_Search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(100, 30);
            this.Btn_Search.TabIndex = 4;
            this.Btn_Search.Text = "検索";
            this.Btn_Search.UseVisualStyleBackColor = true;
            this.Btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // Btn_Delete
            // 
            this.Btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Delete.Location = new System.Drawing.Point(960, 687);
            this.Btn_Delete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Delete.Name = "Btn_Delete";
            this.Btn_Delete.Size = new System.Drawing.Size(100, 30);
            this.Btn_Delete.TabIndex = 6;
            this.Btn_Delete.Text = "削除";
            this.Btn_Delete.UseVisualStyleBackColor = true;
            this.Btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Close.Location = new System.Drawing.Point(1066, 687);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(100, 30);
            this.Btn_Close.TabIndex = 7;
            this.Btn_Close.Text = "閉じる";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Btn_Clear
            // 
            this.Btn_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Clear.Location = new System.Drawing.Point(642, 687);
            this.Btn_Clear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Clear.Name = "Btn_Clear";
            this.Btn_Clear.Size = new System.Drawing.Size(100, 30);
            this.Btn_Clear.TabIndex = 3;
            this.Btn_Clear.Text = "クリア";
            this.Btn_Clear.UseVisualStyleBackColor = true;
            this.Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
            // 
            // Btn_InsUpd
            // 
            this.Btn_InsUpd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_InsUpd.Location = new System.Drawing.Point(854, 687);
            this.Btn_InsUpd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_InsUpd.Name = "Btn_InsUpd";
            this.Btn_InsUpd.Size = new System.Drawing.Size(100, 30);
            this.Btn_InsUpd.TabIndex = 5;
            this.Btn_InsUpd.Text = "登録/更新";
            this.Btn_InsUpd.UseVisualStyleBackColor = true;
            this.Btn_InsUpd.Click += new System.EventHandler(this.Btn_InsUpd_Click);
            // 
            // Tbc_FormType
            // 
            this.Tbc_FormType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tbc_FormType.Controls.Add(this.Tbp_Slip);
            this.Tbc_FormType.Controls.Add(this.Tbp_List);
            this.Tbc_FormType.Location = new System.Drawing.Point(12, 12);
            this.Tbc_FormType.Name = "Tbc_FormType";
            this.Tbc_FormType.SelectedIndex = 0;
            this.Tbc_FormType.Size = new System.Drawing.Size(1158, 661);
            this.Tbc_FormType.TabIndex = 0;
            this.Tbc_FormType.SelectedIndexChanged += new System.EventHandler(this.Tbc_FormType_SelectedIndexChanged);
            // 
            // Tbp_Slip
            // 
            this.Tbp_Slip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Tbp_Slip.BackgroundImage")));
            this.Tbp_Slip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tbp_Slip.Controls.Add(this.Gbx_Detail);
            this.Tbp_Slip.Controls.Add(this.Gbx_InqKey);
            this.Tbp_Slip.Location = new System.Drawing.Point(4, 25);
            this.Tbp_Slip.Name = "Tbp_Slip";
            this.Tbp_Slip.Padding = new System.Windows.Forms.Padding(3);
            this.Tbp_Slip.Size = new System.Drawing.Size(1150, 632);
            this.Tbp_Slip.TabIndex = 0;
            this.Tbp_Slip.Text = "単票形式";
            this.Tbp_Slip.UseVisualStyleBackColor = true;
            // 
            // Gbx_Detail
            // 
            this.Gbx_Detail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Gbx_Detail.Controls.Add(this.Tbx_Note);
            this.Gbx_Detail.Controls.Add(this.Lbl_Note);
            this.Gbx_Detail.Controls.Add(this.Lbl_CTUnit);
            this.Gbx_Detail.Controls.Add(this.Tbx_CT);
            this.Gbx_Detail.Controls.Add(this.Lbl_CT);
            this.Gbx_Detail.Location = new System.Drawing.Point(27, 383);
            this.Gbx_Detail.Margin = new System.Windows.Forms.Padding(4);
            this.Gbx_Detail.Name = "Gbx_Detail";
            this.Gbx_Detail.Padding = new System.Windows.Forms.Padding(4);
            this.Gbx_Detail.Size = new System.Drawing.Size(1097, 198);
            this.Gbx_Detail.TabIndex = 1;
            this.Gbx_Detail.TabStop = false;
            this.Gbx_Detail.Text = "詳細";
            // 
            // Tbx_Note
            // 
            this.Tbx_Note.Location = new System.Drawing.Point(111, 74);
            this.Tbx_Note.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Tbx_Note.MaxLength = 100;
            this.Tbx_Note.Name = "Tbx_Note";
            this.Tbx_Note.Size = new System.Drawing.Size(876, 22);
            this.Tbx_Note.TabIndex = 4;
            this.Tbx_Note.TextChanged += new System.EventHandler(this.Tbx_Note_TextChanged);
            // 
            // Lbl_Note
            // 
            this.Lbl_Note.AutoSize = true;
            this.Lbl_Note.Location = new System.Drawing.Point(66, 77);
            this.Lbl_Note.Name = "Lbl_Note";
            this.Lbl_Note.Size = new System.Drawing.Size(40, 15);
            this.Lbl_Note.TabIndex = 3;
            this.Lbl_Note.Text = "備考:";
            // 
            // Lbl_CTUnit
            // 
            this.Lbl_CTUnit.AutoSize = true;
            this.Lbl_CTUnit.Location = new System.Drawing.Point(364, 29);
            this.Lbl_CTUnit.Name = "Lbl_CTUnit";
            this.Lbl_CTUnit.Size = new System.Drawing.Size(32, 15);
            this.Lbl_CTUnit.TabIndex = 2;
            this.Lbl_CTUnit.Text = "(秒)";
            // 
            // Tbx_CT
            // 
            this.Tbx_CT.Location = new System.Drawing.Point(112, 26);
            this.Tbx_CT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Tbx_CT.Name = "Tbx_CT";
            this.Tbx_CT.Size = new System.Drawing.Size(245, 22);
            this.Tbx_CT.TabIndex = 1;
            this.Tbx_CT.TextChanged += new System.EventHandler(this.Tbx_CT_TextChanged);
            // 
            // Lbl_CT
            // 
            this.Lbl_CT.AutoSize = true;
            this.Lbl_CT.Location = new System.Drawing.Point(18, 29);
            this.Lbl_CT.Name = "Lbl_CT";
            this.Lbl_CT.Size = new System.Drawing.Size(88, 15);
            this.Lbl_CT.TabIndex = 0;
            this.Lbl_CT.Text = "サイクルタイム:";
            // 
            // Gbx_InqKey
            // 
            this.Gbx_InqKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Gbx_InqKey.Controls.Add(this.Lbl_WkGrCaution);
            this.Gbx_InqKey.Controls.Add(this.Gbx_ValDtF);
            this.Gbx_InqKey.Controls.Add(this.Cbx_WkSeq);
            this.Gbx_InqKey.Controls.Add(this.Lbl_WkSeq);
            this.Gbx_InqKey.Controls.Add(this.Cbx_Hm);
            this.Gbx_InqKey.Controls.Add(this.Lbl_Hm);
            this.Gbx_InqKey.Controls.Add(this.Cbx_WkGr);
            this.Gbx_InqKey.Controls.Add(this.Cbx_Od);
            this.Gbx_InqKey.Controls.Add(this.Lbl_WkGr);
            this.Gbx_InqKey.Controls.Add(this.Lbl_Od);
            this.Gbx_InqKey.Location = new System.Drawing.Point(27, 22);
            this.Gbx_InqKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Gbx_InqKey.Name = "Gbx_InqKey";
            this.Gbx_InqKey.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Gbx_InqKey.Size = new System.Drawing.Size(1097, 333);
            this.Gbx_InqKey.TabIndex = 0;
            this.Gbx_InqKey.TabStop = false;
            this.Gbx_InqKey.Text = "検索キー";
            // 
            // Lbl_WkGrCaution
            // 
            this.Lbl_WkGrCaution.AutoSize = true;
            this.Lbl_WkGrCaution.Location = new System.Drawing.Point(556, 287);
            this.Lbl_WkGrCaution.Name = "Lbl_WkGrCaution";
            this.Lbl_WkGrCaution.Size = new System.Drawing.Size(431, 15);
            this.Lbl_WkGrCaution.TabIndex = 9;
            this.Lbl_WkGrCaution.Text = "※新規登録時は作業順序 (3 桁以内の整数) のみを入力してください。";
            // 
            // Gbx_ValDtF
            // 
            this.Gbx_ValDtF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Gbx_ValDtF.Controls.Add(this.Cbx_RegDt);
            this.Gbx_ValDtF.Controls.Add(this.Rbt_DsnDt);
            this.Gbx_ValDtF.Controls.Add(this.Rbt_RegDt);
            this.Gbx_ValDtF.Controls.Add(this.Dtp_DsnDt);
            this.Gbx_ValDtF.Location = new System.Drawing.Point(22, 158);
            this.Gbx_ValDtF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Gbx_ValDtF.Name = "Gbx_ValDtF";
            this.Gbx_ValDtF.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Gbx_ValDtF.Size = new System.Drawing.Size(1048, 106);
            this.Gbx_ValDtF.TabIndex = 6;
            this.Gbx_ValDtF.TabStop = false;
            this.Gbx_ValDtF.Text = "適用年月日";
            // 
            // Cbx_RegDt
            // 
            this.Cbx_RegDt.FormattingEnabled = true;
            this.Cbx_RegDt.Location = new System.Drawing.Point(204, 23);
            this.Cbx_RegDt.Margin = new System.Windows.Forms.Padding(4);
            this.Cbx_RegDt.Name = "Cbx_RegDt";
            this.Cbx_RegDt.Size = new System.Drawing.Size(298, 23);
            this.Cbx_RegDt.TabIndex = 1;
            this.Cbx_RegDt.LostFocus += new System.EventHandler(this.Cbx_RegDt_LostFocus);
            // 
            // Rbt_DsnDt
            // 
            this.Rbt_DsnDt.AutoSize = true;
            this.Rbt_DsnDt.Location = new System.Drawing.Point(90, 70);
            this.Rbt_DsnDt.Margin = new System.Windows.Forms.Padding(4);
            this.Rbt_DsnDt.Name = "Rbt_DsnDt";
            this.Rbt_DsnDt.Size = new System.Drawing.Size(91, 19);
            this.Rbt_DsnDt.TabIndex = 2;
            this.Rbt_DsnDt.TabStop = true;
            this.Rbt_DsnDt.Text = "指定日付:";
            this.Rbt_DsnDt.UseVisualStyleBackColor = true;
            this.Rbt_DsnDt.CheckedChanged += new System.EventHandler(this.Rbt_DsnDt_CheckedChanged);
            // 
            // Rbt_RegDt
            // 
            this.Rbt_RegDt.AutoSize = true;
            this.Rbt_RegDt.Location = new System.Drawing.Point(90, 24);
            this.Rbt_RegDt.Margin = new System.Windows.Forms.Padding(4);
            this.Rbt_RegDt.Name = "Rbt_RegDt";
            this.Rbt_RegDt.Size = new System.Drawing.Size(106, 19);
            this.Rbt_RegDt.TabIndex = 0;
            this.Rbt_RegDt.TabStop = true;
            this.Rbt_RegDt.Text = "登録済日付:";
            this.Rbt_RegDt.UseVisualStyleBackColor = true;
            this.Rbt_RegDt.CheckedChanged += new System.EventHandler(this.Rbt_RegDt_CheckedChanged);
            // 
            // Dtp_DsnDt
            // 
            this.Dtp_DsnDt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dtp_DsnDt.Location = new System.Drawing.Point(204, 66);
            this.Dtp_DsnDt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dtp_DsnDt.Name = "Dtp_DsnDt";
            this.Dtp_DsnDt.Size = new System.Drawing.Size(298, 22);
            this.Dtp_DsnDt.TabIndex = 3;
            this.Dtp_DsnDt.CloseUp += new System.EventHandler(this.Dtp_DsnDt_CloseUp);
            // 
            // Cbx_WkSeq
            // 
            this.Cbx_WkSeq.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Cbx_WkSeq.FormattingEnabled = true;
            this.Cbx_WkSeq.Location = new System.Drawing.Point(112, 284);
            this.Cbx_WkSeq.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cbx_WkSeq.MaxLength = 3;
            this.Cbx_WkSeq.Name = "Cbx_WkSeq";
            this.Cbx_WkSeq.Size = new System.Drawing.Size(412, 23);
            this.Cbx_WkSeq.TabIndex = 8;
            this.Cbx_WkSeq.TextChanged += new System.EventHandler(this.Cbx_WkSeq_TextChanged);
            this.Cbx_WkSeq.LostFocus += new System.EventHandler(this.Cbx_WkSeq_LostFocus);
            // 
            // Lbl_WkSeq
            // 
            this.Lbl_WkSeq.AutoSize = true;
            this.Lbl_WkSeq.Location = new System.Drawing.Point(36, 287);
            this.Lbl_WkSeq.Name = "Lbl_WkSeq";
            this.Lbl_WkSeq.Size = new System.Drawing.Size(70, 15);
            this.Lbl_WkSeq.TabIndex = 7;
            this.Lbl_WkSeq.Text = "作業順序:";
            // 
            // Cbx_Hm
            // 
            this.Cbx_Hm.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Cbx_Hm.FormattingEnabled = true;
            this.Cbx_Hm.Location = new System.Drawing.Point(112, 109);
            this.Cbx_Hm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cbx_Hm.Name = "Cbx_Hm";
            this.Cbx_Hm.Size = new System.Drawing.Size(412, 23);
            this.Cbx_Hm.TabIndex = 5;
            this.Cbx_Hm.SelectedIndexChanged += new System.EventHandler(this.Cbx_Hm_LostFocus);
            // 
            // Lbl_Hm
            // 
            this.Lbl_Hm.AutoSize = true;
            this.Lbl_Hm.Location = new System.Drawing.Point(65, 112);
            this.Lbl_Hm.Name = "Lbl_Hm";
            this.Lbl_Hm.Size = new System.Drawing.Size(40, 15);
            this.Lbl_Hm.TabIndex = 4;
            this.Lbl_Hm.Text = "品目:";
            // 
            // Cbx_WkGr
            // 
            this.Cbx_WkGr.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Cbx_WkGr.FormattingEnabled = true;
            this.Cbx_WkGr.Location = new System.Drawing.Point(112, 66);
            this.Cbx_WkGr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cbx_WkGr.Name = "Cbx_WkGr";
            this.Cbx_WkGr.Size = new System.Drawing.Size(412, 23);
            this.Cbx_WkGr.TabIndex = 3;
            this.Cbx_WkGr.TextChanged += new System.EventHandler(this.Cbx_WkGr_TextChanged);
            // 
            // Cbx_Od
            // 
            this.Cbx_Od.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Cbx_Od.FormattingEnabled = true;
            this.Cbx_Od.Location = new System.Drawing.Point(112, 23);
            this.Cbx_Od.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cbx_Od.Name = "Cbx_Od";
            this.Cbx_Od.Size = new System.Drawing.Size(412, 23);
            this.Cbx_Od.TabIndex = 1;
            this.Cbx_Od.SelectedIndexChanged += new System.EventHandler(this.Cbx_Od_TextChanged);
            // 
            // Lbl_WkGr
            // 
            this.Lbl_WkGr.AutoSize = true;
            this.Lbl_WkGr.Location = new System.Drawing.Point(18, 69);
            this.Lbl_WkGr.Name = "Lbl_WkGr";
            this.Lbl_WkGr.Size = new System.Drawing.Size(87, 15);
            this.Lbl_WkGr.TabIndex = 2;
            this.Lbl_WkGr.Text = "作業グループ:";
            // 
            // Lbl_Od
            // 
            this.Lbl_Od.AutoSize = true;
            this.Lbl_Od.Location = new System.Drawing.Point(50, 26);
            this.Lbl_Od.Name = "Lbl_Od";
            this.Lbl_Od.Size = new System.Drawing.Size(55, 15);
            this.Lbl_Od.TabIndex = 0;
            this.Lbl_Od.Text = "手配先:";
            // 
            // Tbp_List
            // 
            this.Tbp_List.BackColor = System.Drawing.Color.Transparent;
            this.Tbp_List.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Tbp_List.BackgroundImage")));
            this.Tbp_List.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tbp_List.Controls.Add(this.Lbl_BatchEdit);
            this.Tbp_List.Controls.Add(this.Dgv_CycleTmTbl);
            this.Tbp_List.Location = new System.Drawing.Point(4, 25);
            this.Tbp_List.Name = "Tbp_List";
            this.Tbp_List.Padding = new System.Windows.Forms.Padding(3);
            this.Tbp_List.Size = new System.Drawing.Size(1150, 632);
            this.Tbp_List.TabIndex = 1;
            this.Tbp_List.Text = "リスト形式";
            // 
            // Lbl_BatchEdit
            // 
            this.Lbl_BatchEdit.AutoSize = true;
            this.Lbl_BatchEdit.Location = new System.Drawing.Point(27, 22);
            this.Lbl_BatchEdit.Name = "Lbl_BatchEdit";
            this.Lbl_BatchEdit.Size = new System.Drawing.Size(67, 15);
            this.Lbl_BatchEdit.TabIndex = 0;
            this.Lbl_BatchEdit.Text = "一括編集";
            // 
            // Dgv_CycleTmTbl
            // 
            this.Dgv_CycleTmTbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dgv_CycleTmTbl.ColumnHeadersHeight = 29;
            this.Dgv_CycleTmTbl.Location = new System.Drawing.Point(27, 39);
            this.Dgv_CycleTmTbl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dgv_CycleTmTbl.Name = "Dgv_CycleTmTbl";
            this.Dgv_CycleTmTbl.RowHeadersWidth = 51;
            this.Dgv_CycleTmTbl.RowTemplate.Height = 24;
            this.Dgv_CycleTmTbl.Size = new System.Drawing.Size(1097, 580);
            this.Dgv_CycleTmTbl.TabIndex = 1;
            this.Dgv_CycleTmTbl.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.Dgv_CellValidating);
            // 
            // Btn_SaveCsvFile
            // 
            this.Btn_SaveCsvFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_SaveCsvFile.Enabled = false;
            this.Btn_SaveCsvFile.Location = new System.Drawing.Point(516, 687);
            this.Btn_SaveCsvFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_SaveCsvFile.Name = "Btn_SaveCsvFile";
            this.Btn_SaveCsvFile.Size = new System.Drawing.Size(100, 30);
            this.Btn_SaveCsvFile.TabIndex = 2;
            this.Btn_SaveCsvFile.Text = "CSV 保存";
            this.Btn_SaveCsvFile.UseVisualStyleBackColor = true;
            this.Btn_SaveCsvFile.Click += new System.EventHandler(this.Btn_SaveCsvFile_Click);
            // 
            // Btn_ReadCsvFile
            // 
            this.Btn_ReadCsvFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_ReadCsvFile.Location = new System.Drawing.Point(410, 687);
            this.Btn_ReadCsvFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_ReadCsvFile.Name = "Btn_ReadCsvFile";
            this.Btn_ReadCsvFile.Size = new System.Drawing.Size(100, 30);
            this.Btn_ReadCsvFile.TabIndex = 1;
            this.Btn_ReadCsvFile.Text = "CSV 読込";
            this.Btn_ReadCsvFile.UseVisualStyleBackColor = true;
            this.Btn_ReadCsvFile.Click += new System.EventHandler(this.Btn_ReadCsvFile_Click);
            // 
            // Frm24_RegCycleTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 755);
            this.Controls.Add(this.Btn_SaveCsvFile);
            this.Controls.Add(this.Btn_ReadCsvFile);
            this.Controls.Add(this.Tbc_FormType);
            this.Controls.Add(this.Btn_Search);
            this.Controls.Add(this.Btn_Delete);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_Clear);
            this.Controls.Add(this.Btn_InsUpd);
            this.Controls.Add(this.Sst);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm24_RegCycleTime";
            this.Text = "[KED001SF] 原価管理システム (Ver. XXXXXX.XX) [#22: 標準作業時間登録]";
            this.Sst.ResumeLayout(false);
            this.Sst.PerformLayout();
            this.Tbc_FormType.ResumeLayout(false);
            this.Tbp_Slip.ResumeLayout(false);
            this.Gbx_Detail.ResumeLayout(false);
            this.Gbx_Detail.PerformLayout();
            this.Gbx_InqKey.ResumeLayout(false);
            this.Gbx_InqKey.PerformLayout();
            this.Gbx_ValDtF.ResumeLayout(false);
            this.Gbx_ValDtF.PerformLayout();
            this.Tbp_List.ResumeLayout(false);
            this.Tbp_List.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CycleTmTbl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip Sst;
        private System.Windows.Forms.ToolStripStatusLabel Tsl_Msg;
        private System.Windows.Forms.Button Btn_Search;
        private System.Windows.Forms.Button Btn_Delete;
        private System.Windows.Forms.Button Btn_Close;
        private System.Windows.Forms.Button Btn_Clear;
        private System.Windows.Forms.Button Btn_InsUpd;
        private System.Windows.Forms.TabControl Tbc_FormType;
        private System.Windows.Forms.TabPage Tbp_Slip;
        private System.Windows.Forms.TabPage Tbp_List;
        private System.Windows.Forms.GroupBox Gbx_Detail;
        private System.Windows.Forms.Label Lbl_CTUnit;
        private System.Windows.Forms.TextBox Tbx_CT;
        private System.Windows.Forms.Label Lbl_CT;
        private System.Windows.Forms.GroupBox Gbx_InqKey;
        private System.Windows.Forms.Label Lbl_Hm;
        private System.Windows.Forms.ComboBox Cbx_WkGr;
        private System.Windows.Forms.ComboBox Cbx_Od;
        private System.Windows.Forms.Label Lbl_WkGr;
        private System.Windows.Forms.Label Lbl_Od;
        private System.Windows.Forms.DataGridView Dgv_CycleTmTbl;
        private System.Windows.Forms.Label Lbl_BatchEdit;
        private System.Windows.Forms.Button Btn_SaveCsvFile;
        private System.Windows.Forms.Button Btn_ReadCsvFile;
        private System.Windows.Forms.ComboBox Cbx_Hm;
        private System.Windows.Forms.Label Lbl_WkGrCaution;
        private System.Windows.Forms.GroupBox Gbx_ValDtF;
        private System.Windows.Forms.ComboBox Cbx_RegDt;
        private System.Windows.Forms.RadioButton Rbt_DsnDt;
        private System.Windows.Forms.RadioButton Rbt_RegDt;
        private System.Windows.Forms.DateTimePicker Dtp_DsnDt;
        private System.Windows.Forms.ComboBox Cbx_WkSeq;
        private System.Windows.Forms.Label Lbl_WkSeq;
        private System.Windows.Forms.TextBox Tbx_Note;
        private System.Windows.Forms.Label Lbl_Note;
    }
}