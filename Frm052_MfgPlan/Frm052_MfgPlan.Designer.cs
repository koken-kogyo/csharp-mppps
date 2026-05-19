using System.Drawing;
using System.Windows.Forms;

namespace MPPPS
{
    partial class Frm052_MfgPlan
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm052_MfgPlan));
            this.splitContainerVertical = new System.Windows.Forms.SplitContainer();
            this.splitContainerHorizontal = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ButtonExcelExport = new System.Windows.Forms.Button();
            this.ButtonEntry = new System.Windows.Forms.Button();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.ButtonRowDelete = new System.Windows.Forms.Button();
            this.ButtonUndo = new System.Windows.Forms.Button();
            this.groupBoxHMCD = new System.Windows.Forms.GroupBox();
            this.labelHMNM = new System.Windows.Forms.Label();
            this.labelHmSize = new System.Windows.Forms.Label();
            this.labelHmSizeTitle = new System.Windows.Forms.Label();
            this.labelHMCD = new System.Windows.Forms.Label();
            this.labelHMCDTitle = new System.Windows.Forms.Label();
            this.labelMatSize = new System.Windows.Forms.Label();
            this.labelMatSizeTitle = new System.Windows.Forms.Label();
            this.labelCT = new System.Windows.Forms.Label();
            this.labelCTTitle = new System.Windows.Forms.Label();
            this.labelMatQty = new System.Windows.Forms.Label();
            this.labelMatQtyTitle = new System.Windows.Forms.Label();
            this.labelHour = new System.Windows.Forms.Label();
            this.labelHourTitle = new System.Windows.Forms.Label();
            this.groupBoxOrder = new System.Windows.Forms.GroupBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.labelQty = new System.Windows.Forms.Label();
            this.labelQtyTitle = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelOrderNo = new System.Windows.Forms.Label();
            this.labelTimeTitle = new System.Windows.Forms.Label();
            this.labelOrderNoTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVertical)).BeginInit();
            this.splitContainerVertical.Panel1.SuspendLayout();
            this.splitContainerVertical.Panel2.SuspendLayout();
            this.splitContainerVertical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHorizontal)).BeginInit();
            this.splitContainerHorizontal.Panel1.SuspendLayout();
            this.splitContainerHorizontal.Panel2.SuspendLayout();
            this.splitContainerHorizontal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBoxHMCD.SuspendLayout();
            this.groupBoxOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerVertical
            // 
            this.splitContainerVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerVertical.Location = new System.Drawing.Point(0, 0);
            this.splitContainerVertical.Name = "splitContainerVertical";
            // 
            // splitContainerVertical.Panel1
            // 
            this.splitContainerVertical.Panel1.Controls.Add(this.splitContainerHorizontal);
            this.splitContainerVertical.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitContainerVertical.Panel2
            // 
            this.splitContainerVertical.Panel2.Controls.Add(this.ButtonExcelExport);
            this.splitContainerVertical.Panel2.Controls.Add(this.ButtonEntry);
            this.splitContainerVertical.Panel2.Controls.Add(this.ButtonClose);
            this.splitContainerVertical.Panel2.Controls.Add(this.ButtonRowDelete);
            this.splitContainerVertical.Panel2.Controls.Add(this.ButtonUndo);
            this.splitContainerVertical.Panel2.Controls.Add(this.groupBoxHMCD);
            this.splitContainerVertical.Panel2.Controls.Add(this.groupBoxOrder);
            this.splitContainerVertical.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitContainerVertical.Size = new System.Drawing.Size(997, 514);
            this.splitContainerVertical.SplitterDistance = 833;
            this.splitContainerVertical.TabIndex = 0;
            // 
            // splitContainerHorizontal
            // 
            this.splitContainerHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerHorizontal.Location = new System.Drawing.Point(5, 5);
            this.splitContainerHorizontal.Name = "splitContainerHorizontal";
            this.splitContainerHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerHorizontal.Panel1
            // 
            this.splitContainerHorizontal.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainerHorizontal.Panel2
            // 
            this.splitContainerHorizontal.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainerHorizontal.Size = new System.Drawing.Size(823, 504);
            this.splitContainerHorizontal.SplitterDistance = 377;
            this.splitContainerHorizontal.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 32;
            this.dataGridView1.Size = new System.Drawing.Size(823, 377);
            this.dataGridView1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chart1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Size = new System.Drawing.Size(823, 123);
            this.splitContainer1.SplitterDistance = 788;
            this.splitContainer1.TabIndex = 1;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Padding = new System.Windows.Forms.Padding(60, 0, 60, 0);
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(788, 123);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // ButtonExcelExport
            // 
            this.ButtonExcelExport.BackColor = System.Drawing.Color.LightGreen;
            this.ButtonExcelExport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonExcelExport.Location = new System.Drawing.Point(5, 368);
            this.ButtonExcelExport.Margin = new System.Windows.Forms.Padding(6);
            this.ButtonExcelExport.Name = "ButtonExcelExport";
            this.ButtonExcelExport.Size = new System.Drawing.Size(150, 47);
            this.ButtonExcelExport.TabIndex = 7;
            this.ButtonExcelExport.Text = "Excel出力";
            this.ButtonExcelExport.UseVisualStyleBackColor = false;
            this.ButtonExcelExport.Click += new System.EventHandler(this.ButtonExcelExport_Click);
            // 
            // ButtonEntry
            // 
            this.ButtonEntry.BackColor = System.Drawing.Color.LightCoral;
            this.ButtonEntry.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonEntry.Location = new System.Drawing.Point(5, 415);
            this.ButtonEntry.Margin = new System.Windows.Forms.Padding(6);
            this.ButtonEntry.Name = "ButtonEntry";
            this.ButtonEntry.Size = new System.Drawing.Size(150, 47);
            this.ButtonEntry.TabIndex = 2;
            this.ButtonEntry.Text = "保存 (F9)";
            this.ButtonEntry.UseVisualStyleBackColor = false;
            this.ButtonEntry.Click += new System.EventHandler(this.ButtonEntry_Click);
            // 
            // ButtonClose
            // 
            this.ButtonClose.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonClose.Location = new System.Drawing.Point(5, 462);
            this.ButtonClose.Margin = new System.Windows.Forms.Padding(6);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(150, 47);
            this.ButtonClose.TabIndex = 1;
            this.ButtonClose.Text = "閉じる";
            this.ButtonClose.UseVisualStyleBackColor = false;
            this.ButtonClose.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // ButtonRowDelete
            // 
            this.ButtonRowDelete.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonRowDelete.Dock = System.Windows.Forms.DockStyle.Top;
            this.ButtonRowDelete.Location = new System.Drawing.Point(5, 456);
            this.ButtonRowDelete.Margin = new System.Windows.Forms.Padding(6);
            this.ButtonRowDelete.Name = "ButtonRowDelete";
            this.ButtonRowDelete.Size = new System.Drawing.Size(150, 47);
            this.ButtonRowDelete.TabIndex = 5;
            this.ButtonRowDelete.Text = "行削除";
            this.ButtonRowDelete.UseVisualStyleBackColor = false;
            this.ButtonRowDelete.Click += new System.EventHandler(this.ButtonRowDelete_Click);
            // 
            // ButtonUndo
            // 
            this.ButtonUndo.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonUndo.Dock = System.Windows.Forms.DockStyle.Top;
            this.ButtonUndo.Enabled = false;
            this.ButtonUndo.Location = new System.Drawing.Point(5, 409);
            this.ButtonUndo.Margin = new System.Windows.Forms.Padding(6);
            this.ButtonUndo.Name = "ButtonUndo";
            this.ButtonUndo.Size = new System.Drawing.Size(150, 47);
            this.ButtonUndo.TabIndex = 3;
            this.ButtonUndo.Text = "戻す (Ctrl+Z)";
            this.ButtonUndo.UseVisualStyleBackColor = false;
            this.ButtonUndo.Click += new System.EventHandler(this.ButtonUndo_Click);
            // 
            // groupBoxHMCD
            // 
            this.groupBoxHMCD.Controls.Add(this.labelHMNM);
            this.groupBoxHMCD.Controls.Add(this.labelHmSize);
            this.groupBoxHMCD.Controls.Add(this.labelHmSizeTitle);
            this.groupBoxHMCD.Controls.Add(this.labelHMCD);
            this.groupBoxHMCD.Controls.Add(this.labelHMCDTitle);
            this.groupBoxHMCD.Controls.Add(this.labelMatSize);
            this.groupBoxHMCD.Controls.Add(this.labelMatSizeTitle);
            this.groupBoxHMCD.Controls.Add(this.labelCT);
            this.groupBoxHMCD.Controls.Add(this.labelCTTitle);
            this.groupBoxHMCD.Controls.Add(this.labelMatQty);
            this.groupBoxHMCD.Controls.Add(this.labelMatQtyTitle);
            this.groupBoxHMCD.Controls.Add(this.labelHour);
            this.groupBoxHMCD.Controls.Add(this.labelHourTitle);
            this.groupBoxHMCD.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxHMCD.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBoxHMCD.Location = new System.Drawing.Point(5, 154);
            this.groupBoxHMCD.Name = "groupBoxHMCD";
            this.groupBoxHMCD.Size = new System.Drawing.Size(150, 255);
            this.groupBoxHMCD.TabIndex = 6;
            this.groupBoxHMCD.TabStop = false;
            this.groupBoxHMCD.Text = "品目情報";
            // 
            // labelHMNM
            // 
            this.labelHMNM.AutoSize = true;
            this.labelHMNM.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelHMNM.Location = new System.Drawing.Point(20, 48);
            this.labelHMNM.Name = "labelHMNM";
            this.labelHMNM.Size = new System.Drawing.Size(47, 17);
            this.labelHMNM.TabIndex = 29;
            this.labelHMNM.Text = "製品名";
            // 
            // labelHmSize
            // 
            this.labelHmSize.AutoSize = true;
            this.labelHmSize.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelHmSize.Location = new System.Drawing.Point(86, 132);
            this.labelHmSize.Name = "labelHmSize";
            this.labelHmSize.Size = new System.Drawing.Size(54, 17);
            this.labelHmSize.TabIndex = 27;
            this.labelHmSize.Text = "25.5mm";
            // 
            // labelHmSizeTitle
            // 
            this.labelHmSizeTitle.AutoSize = true;
            this.labelHmSizeTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelHmSizeTitle.Location = new System.Drawing.Point(7, 132);
            this.labelHmSizeTitle.Name = "labelHmSizeTitle";
            this.labelHmSizeTitle.Size = new System.Drawing.Size(76, 17);
            this.labelHmSizeTitle.TabIndex = 26;
            this.labelHmSizeTitle.Text = "製品サイズ：";
            // 
            // labelHMCD
            // 
            this.labelHMCD.AutoSize = true;
            this.labelHMCD.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelHMCD.Location = new System.Drawing.Point(52, 23);
            this.labelHMCD.Name = "labelHMCD";
            this.labelHMCD.Size = new System.Drawing.Size(83, 17);
            this.labelHMCD.TabIndex = 25;
            this.labelHMCD.Text = "T1855-70743";
            // 
            // labelHMCDTitle
            // 
            this.labelHMCDTitle.AutoSize = true;
            this.labelHMCDTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelHMCDTitle.Location = new System.Drawing.Point(7, 23);
            this.labelHMCDTitle.Name = "labelHMCDTitle";
            this.labelHMCDTitle.Size = new System.Drawing.Size(47, 17);
            this.labelHMCDTitle.TabIndex = 24;
            this.labelHMCDTitle.Text = "品番：";
            // 
            // labelMatSize
            // 
            this.labelMatSize.AutoSize = true;
            this.labelMatSize.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelMatSize.Location = new System.Drawing.Point(86, 161);
            this.labelMatSize.Name = "labelMatSize";
            this.labelMatSize.Size = new System.Drawing.Size(58, 17);
            this.labelMatSize.TabIndex = 23;
            this.labelMatSize.Text = "5500mm";
            // 
            // labelMatSizeTitle
            // 
            this.labelMatSizeTitle.AutoSize = true;
            this.labelMatSizeTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelMatSizeTitle.Location = new System.Drawing.Point(7, 161);
            this.labelMatSizeTitle.Name = "labelMatSizeTitle";
            this.labelMatSizeTitle.Size = new System.Drawing.Size(76, 17);
            this.labelMatSizeTitle.TabIndex = 22;
            this.labelMatSizeTitle.Text = "材料サイズ：";
            // 
            // labelCT
            // 
            this.labelCT.AutoSize = true;
            this.labelCT.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelCT.Location = new System.Drawing.Point(85, 76);
            this.labelCT.Name = "labelCT";
            this.labelCT.Size = new System.Drawing.Size(42, 17);
            this.labelCT.TabIndex = 21;
            this.labelCT.Text = "180秒";
            // 
            // labelCTTitle
            // 
            this.labelCTTitle.AutoSize = true;
            this.labelCTTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelCTTitle.Location = new System.Drawing.Point(6, 75);
            this.labelCTTitle.Name = "labelCTTitle";
            this.labelCTTitle.Size = new System.Drawing.Size(36, 17);
            this.labelCTTitle.TabIndex = 20;
            this.labelCTTitle.Text = "CT：";
            // 
            // labelMatQty
            // 
            this.labelMatQty.AutoSize = true;
            this.labelMatQty.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelMatQty.ForeColor = System.Drawing.Color.IndianRed;
            this.labelMatQty.Location = new System.Drawing.Point(85, 178);
            this.labelMatQty.Name = "labelMatQty";
            this.labelMatQty.Size = new System.Drawing.Size(35, 17);
            this.labelMatQty.TabIndex = 19;
            this.labelMatQty.Text = "60本";
            // 
            // labelMatQtyTitle
            // 
            this.labelMatQtyTitle.AutoSize = true;
            this.labelMatQtyTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelMatQtyTitle.Location = new System.Drawing.Point(6, 178);
            this.labelMatQtyTitle.Name = "labelMatQtyTitle";
            this.labelMatQtyTitle.Size = new System.Drawing.Size(79, 17);
            this.labelMatQtyTitle.TabIndex = 18;
            this.labelMatQtyTitle.Text = "材料当たり：";
            // 
            // labelHour
            // 
            this.labelHour.AutoSize = true;
            this.labelHour.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelHour.ForeColor = System.Drawing.Color.IndianRed;
            this.labelHour.Location = new System.Drawing.Point(85, 99);
            this.labelHour.Name = "labelHour";
            this.labelHour.Size = new System.Drawing.Size(35, 17);
            this.labelHour.TabIndex = 17;
            this.labelHour.Text = "30本";
            // 
            // labelHourTitle
            // 
            this.labelHourTitle.AutoSize = true;
            this.labelHourTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelHourTitle.Location = new System.Drawing.Point(6, 99);
            this.labelHourTitle.Name = "labelHourTitle";
            this.labelHourTitle.Size = new System.Drawing.Size(79, 17);
            this.labelHourTitle.TabIndex = 16;
            this.labelHourTitle.Text = "時間当たり：";
            // 
            // groupBoxOrder
            // 
            this.groupBoxOrder.Controls.Add(this.labelStatus);
            this.groupBoxOrder.Controls.Add(this.textBoxNote);
            this.groupBoxOrder.Controls.Add(this.labelQty);
            this.groupBoxOrder.Controls.Add(this.labelQtyTitle);
            this.groupBoxOrder.Controls.Add(this.labelTime);
            this.groupBoxOrder.Controls.Add(this.labelOrderNo);
            this.groupBoxOrder.Controls.Add(this.labelTimeTitle);
            this.groupBoxOrder.Controls.Add(this.labelOrderNoTitle);
            this.groupBoxOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxOrder.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBoxOrder.Location = new System.Drawing.Point(5, 5);
            this.groupBoxOrder.Name = "groupBoxOrder";
            this.groupBoxOrder.Size = new System.Drawing.Size(150, 149);
            this.groupBoxOrder.TabIndex = 4;
            this.groupBoxOrder.TabStop = false;
            this.groupBoxOrder.Text = "注文情報";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelStatus.Location = new System.Drawing.Point(90, 27);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(38, 17);
            this.labelStatus.TabIndex = 13;
            this.labelStatus.Text = "staus";
            // 
            // textBoxNote
            // 
            this.textBoxNote.Location = new System.Drawing.Point(9, 115);
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(118, 25);
            this.textBoxNote.TabIndex = 12;
            this.textBoxNote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxNote_KeyDown);
            // 
            // labelQty
            // 
            this.labelQty.AutoSize = true;
            this.labelQty.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelQty.Location = new System.Drawing.Point(86, 71);
            this.labelQty.Name = "labelQty";
            this.labelQty.Size = new System.Drawing.Size(42, 17);
            this.labelQty.TabIndex = 11;
            this.labelQty.Text = "288本";
            // 
            // labelQtyTitle
            // 
            this.labelQtyTitle.AutoSize = true;
            this.labelQtyTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelQtyTitle.Location = new System.Drawing.Point(7, 71);
            this.labelQtyTitle.Name = "labelQtyTitle";
            this.labelQtyTitle.Size = new System.Drawing.Size(47, 17);
            this.labelQtyTitle.TabIndex = 10;
            this.labelQtyTitle.Text = "本数：";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTime.ForeColor = System.Drawing.Color.IndianRed;
            this.labelTime.Location = new System.Drawing.Point(86, 92);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(32, 17);
            this.labelTime.TabIndex = 4;
            this.labelTime.Text = "1.5h";
            // 
            // labelOrderNo
            // 
            this.labelOrderNo.AutoSize = true;
            this.labelOrderNo.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelOrderNo.Location = new System.Drawing.Point(25, 48);
            this.labelOrderNo.Name = "labelOrderNo";
            this.labelOrderNo.Size = new System.Drawing.Size(78, 17);
            this.labelOrderNo.TabIndex = 3;
            this.labelOrderNo.Text = "2605000000";
            // 
            // labelTimeTitle
            // 
            this.labelTimeTitle.AutoSize = true;
            this.labelTimeTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTimeTitle.Location = new System.Drawing.Point(7, 92);
            this.labelTimeTitle.Name = "labelTimeTitle";
            this.labelTimeTitle.Size = new System.Drawing.Size(47, 17);
            this.labelTimeTitle.TabIndex = 1;
            this.labelTimeTitle.Text = "時間：";
            // 
            // labelOrderNoTitle
            // 
            this.labelOrderNoTitle.AutoSize = true;
            this.labelOrderNoTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelOrderNoTitle.Location = new System.Drawing.Point(7, 27);
            this.labelOrderNoTitle.Name = "labelOrderNoTitle";
            this.labelOrderNoTitle.Size = new System.Drawing.Size(47, 17);
            this.labelOrderNoTitle.TabIndex = 0;
            this.labelOrderNoTitle.Text = "番号：";
            // 
            // Frm052_MfgPlan
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(997, 514);
            this.Controls.Add(this.splitContainerVertical);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Frm052_MfgPlan";
            this.Text = "製造部計画表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm052_FormsPrinting_KeyDown);
            this.splitContainerVertical.Panel1.ResumeLayout(false);
            this.splitContainerVertical.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVertical)).EndInit();
            this.splitContainerVertical.ResumeLayout(false);
            this.splitContainerHorizontal.Panel1.ResumeLayout(false);
            this.splitContainerHorizontal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHorizontal)).EndInit();
            this.splitContainerHorizontal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBoxHMCD.ResumeLayout(false);
            this.groupBoxHMCD.PerformLayout();
            this.groupBoxOrder.ResumeLayout(false);
            this.groupBoxOrder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainerVertical;
        private Button ButtonUndo;
        private Button ButtonEntry;
        private Button ButtonClose;
        private GroupBox groupBoxOrder;
        private Button ButtonRowDelete;
        private Label labelTimeTitle;
        private Label labelOrderNoTitle;
        private Label labelTime;
        private Label labelOrderNo;
        private Label labelQty;
        private Label labelQtyTitle;
        private GroupBox groupBoxHMCD;
        private Label labelHMCD;
        private Label labelHMCDTitle;
        private Label labelMatSize;
        private Label labelMatSizeTitle;
        private Label labelCT;
        private Label labelCTTitle;
        private Label labelMatQty;
        private Label labelMatQtyTitle;
        private Label labelHour;
        private Label labelHourTitle;
        private Label labelHmSize;
        private Label labelHmSizeTitle;
        private SplitContainer splitContainerHorizontal;
        private DataGridView dataGridView1;
        private Button ButtonExcelExport;
        private TextBox textBoxNote;
        private SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Label labelStatus;
        private Label labelHMNM;
    }
}