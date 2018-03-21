using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiServer
{


    public class Server
    {


        static Dictionary<string, MyClient> dSockets = new Dictionary<string, MyClient>();
        static int i = 1;
        static Socket sck;
        static Socket acc;

        static void Connection()
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Bind(new IPEndPoint(IPAddress.Parse("192.168.1.4"), 1234));
            sck.Listen(0);
        }

        static void Operation()
        {
            while (true)
            {
                // Accept the incoming request
                acc = sck.Accept();
                try
                {

                    byte[] Buf = new byte[255];
                    int rec = acc.Receive(Buf, 0, Buf.Length, 0);
                    Array.Resize(ref Buf, rec);
                    string str = Encoding.Default.GetString(Buf);
                    Console.WriteLine(str + " Joined.....");
                    byte[] send = Encoding.Default.GetBytes(str);
                    acc.Send(send, 0, send.Length, 0);
                    byte[] Buf1 = new byte[255];
                    int rec1 = acc.Receive(Buf1, 0, Buf1.Length, 0);
                    Array.Resize(ref Buf1, rec1);
                    string str1 = Encoding.Default.GetString(Buf1);

                    MyClient mclnt = new MyClient(acc, str, str1, dSockets);
                    dSockets.Add(str, mclnt);

                    Thread t = new Thread(mclnt.Run);
                    Thread t1 = new Thread(mclnt.Send);
                    t.Start();
                    t1.Start();
                    i++;
                }
                catch
                {
                    Console.WriteLine("It is an exception");
                }
            }
        }

        static void Main(string[] args)
        {

            Console.WriteLine("WelCome To Server");
            Connection();
            Operation();

        }

    }
}
