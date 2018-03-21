using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sck.Bind(new IPEndPoint(0, 1999));
                sck.Listen(0);

                Socket acc = sck.Accept();

                byte[] buffer = new byte[255];

                int rec = acc.Receive(buffer, 0, buffer.Length, 0);
                Array.Resize(ref buffer, rec);
                Console.WriteLine("Received : {0}", Encoding.Default.GetString(buffer));
                sck.Close();
                acc.Close();
                Console.Read();
            }
        }
    }
}
