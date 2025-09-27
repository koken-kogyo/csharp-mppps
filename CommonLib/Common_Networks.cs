using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace MPPPS
{
    /// <summary>
    /// 共通クラス
    /// </summary>
    public partial class Common
    {
        public static void IsNetworkTestStub()
        {
            string host = "localhost";  // チェックしたいホスト名またはIPアドレス
            int port = 80;              // チェックしたいポート番号

            // Pingチェック
            if (IsNetworkHost(host))
            {
                Console.WriteLine($"Ping成功: {host}");
            }
            else
            {
                Console.WriteLine($"Ping失敗: {host}");
            }

            // ポートチェック
            if (IsNetworkPort(host, port))
            {
                Console.WriteLine($"ポート{port}は開いています: {host}");
            }
            else
            {
                Console.WriteLine($"ポート{port}は閉じています: {host}");
            }
        }

        public static bool IsNetworkHost(string host)
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send(host, 1000); // タイムアウトを1秒に設定
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNetworkPort(string host, int port)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(host, port);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}