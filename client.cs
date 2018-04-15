using System;
using System.Net;
using System.Text; // 引入这两个命名空间，以下同
using System.Net.Sockets;
namespace chat_program {
    class Client {
        static void Main (string[] args) {
            Console.WriteLine ("Client Running ...");
            TcpClient client;
            ConsoleKey key;
            const int BufferSize = 8192;

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

            NetworkStream streamToServer = client.GetStream ();
            Console.WriteLine ("Menu: S - Send, X - Exit");

            do {
                key = Console.ReadKey (true).Key;

                if (key == ConsoleKey.S) {
                    // 获取输入的字符串
                    Console.Write ("Input the message: ");
                    string msg = Console.ReadLine ();

                    byte[] buffer = Encoding.Unicode.GetBytes (msg); // 获得缓存
                    try {
                        lock (streamToServer) {
                            streamToServer.Write (buffer, 0, buffer.Length); // 发往服务器
                        }
                        Console.WriteLine ("Sent: {0}", msg);

                        int bytesRead;
                        buffer = new byte[BufferSize];
                        lock (streamToServer) {
                            bytesRead = streamToServer.Read (buffer, 0, BufferSize);
                        }
                        msg = Encoding.Unicode.GetString (buffer, 0, bytesRead);
                        Console.WriteLine ("Received: {0}", msg);

                    } catch (Exception ex) {
                        Console.WriteLine (ex.Message);
                        break;
                    }
                }
            } while (key != ConsoleKey.X);

            streamToServer.Dispose ();
            client.Close ();

            Console.WriteLine ("\n\n输入\"Q\"键退出。");
            do {
                key = Console.ReadKey (true).Key;
            } while (key != ConsoleKey.Q);
        }
    }
}