using System.Diagnostics;
using System;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace MPPPS
{
    public partial class Frm100_VerInfo : Form
    {
        public Frm100_VerInfo(Common cmn)
        {
            InitializeComponent();

            // 現在のパスを取得
            string dirCurrentPath = Directory.GetCurrentDirectory();

            // 対象のファイルのフルパスを指定
            string filePath = @dirCurrentPath + @"\MPPPS.exe"; // Assembly.GetExecutingAssembly().Location; // Frm100_VersionInfo.dllになってしまう

            // FileInfoオブジェクトを作成
            FileInfo fileInfo = new FileInfo(filePath);

            // 最終更新日時を取得
            DateTime lastWriteTime = fileInfo.LastWriteTime;

            // ファイルバージョン情報を取得
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);

            // バージョン情報セット
            lbl_MY_PGM_ID.Text = "プログラム ID: " + Common.MY_PGM_ID;
            lbl_MY_PGM_VER.Text = "プログラム バージョン: " + Common.MY_PGM_VER;
            lbl_FileVersion.Text = "ファイル バージョン: " + fileVersionInfo.FileVersion;
            lbl_BuildDate.Text = "ビルド日付: " + lastWriteTime;

            // バージョン情報を表示
            Console.WriteLine($"ファイル名: {fileVersionInfo.FileName}");
            Console.WriteLine($"製品名: {fileVersionInfo.ProductName}");
            Console.WriteLine($"製品バージョン: {fileVersionInfo.ProductVersion}");
            Console.WriteLine($"ファイルバージョン: {fileVersionInfo.FileVersion}");
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
