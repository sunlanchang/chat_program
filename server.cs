using System;
using System.Net; // 引入这两个命名空间，以下同
using System.Net.Sockets;
namespace chat_program {
    class Server {
        static void Main (string[] args) {
            Console.WriteLine ("Server is running ... ");
            IPAddress ip = new IPAddress (new byte[] { 127, 0, 0, 1 });
            TcpListener listener = new TcpListener (ip, 8500);

            listener.Start (); // 开始侦听
            Console.WriteLine ("Start Listening ...");
            while (true) {
                // 获取一个连接，同步方法
                TcpClient remoteClient = listener.AcceptTcpClient ();
                // 打印连接到的客户端信息
                Console.WriteLine ("Client Connected！{0} <-- {1}",
                    remoteClient.Client.LocalEndPoint, remoteClient.Client.RemoteEndPoint);
            }

            Console.WriteLine ("\n\n输入\"Q\"键退出。");
            ConsoleKey key;
            do {
                key = Console.ReadKey (true).Key;
            } while (key != ConsoleKey.Q);
        }
    }
}
// 获得IPAddress对象的另外几种常用方法：
// IPAddress ip = IPAddress.Parse("127.0.0.1");
// IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];