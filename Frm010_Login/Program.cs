using System;
using System.Windows.Forms;

namespace MPPPS
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // [ログイン] フォームを表示
            Frm010_Login frm010_Login = new Frm010_Login();

            // [ログイン] フォームを閉じる
            frm010_Login.Close();
        }
    }
}
