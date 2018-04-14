using System;
using System.Net;
using System.Text; // 引入这两个命名空间，以下同
using System.Net.Sockets;
namespace chat_program {
    class Client {
        static void Main (string[] args) {
            Console.WriteLine ("Client Running ...");
            TcpClient client;

            try {
                client = new TcpClient ();
                client.Connect ("localhost", 8500); // 与服务器连接
            } catch (Exception ex) {
                Console.WriteLine (ex.Message);
                return;
            }
            // 打印连接到的服务端信息
            Console.WriteLine ("Server Connected！{0} --> {1}",
                client.Client.LocalEndPoint, client.Client.RemoteEndPoint);

            string msg = "\"Welcome To TraceFact.Net\"";
            NetworkStream streamToServer = client.GetStream ();

            byte[] buffer = Encoding.Unicode.GetBytes (msg); // 获得缓存
            streamToServer.Write (buffer, 0, buffer.Length); // 发往服务器
            Console.WriteLine ("Sent: {0}", msg);
            // 按Q退出
            Console.WriteLine ("\n\n输入\"Q\"键退出。");
            ConsoleKey key;
            do {
                key = Console.ReadKey (true).Key;
            } while (key != ConsoleKey.Q);
        }
    }
}