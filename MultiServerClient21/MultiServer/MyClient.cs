﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MultiServer
{
    class MyClient
    {

        private string name;
        Socket s;
        bool isloggedin;
        string[] strr;
        static MyClient kk;
        Dictionary<string, MyClient> dSockets;


        public MyClient(Socket s, string name, string con, Dictionary<string, MyClient> ch)
        {

            this.name = name;
            this.s = s;
            this.isloggedin = true;
            dSockets = ch;
            this.strr = con.Split('|');
        }

        public void Run()
        {
            string received;
            while (true)
            {
                try
                {
                    byte[] Buffer = new byte[255];
                    int rec = s.Receive(Buffer, 0, Buffer.Length, 0);
                    Array.Resize(ref Buffer, rec);
                    received = Encoding.Default.GetString(Buffer);

                    Console.WriteLine(received);
                    string[] st = received.Split('#');
                    string recipient = st[0];
                    string MsgToSend = st[1];

                    if (MsgToSend.Equals("logout"))
                    {
                        dSockets.Remove(recipient);
                        this.isloggedin = false;
                        this.s.Close();
                        break;
                    }


                    foreach (KeyValuePair<string, MyClient> val in dSockets)
                    {
                        MyClient mc = (MyClient)val.Value;

                        if (mc.name.Equals(recipient))
                        {
                            kk = mc;
                        }
                        for (int i = 0; i < kk.strr.Length; i++)
                        {

                            if (!(mc.name.Equals(recipient)) && mc.isloggedin == true && mc.name.Equals(kk.strr[i]))
                            {
                                byte[] sData = Encoding.Default.GetBytes(this.name + " : " + MsgToSend);
                                mc.s.Send(sData, 0, sData.Length, 0);
                                break;
                            }
                        }
                    }


                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }
        }

        public void Send()
        {
            if (s != null)
            {
                while (true)
                {
                    string msg = Console.ReadLine();
                    foreach (KeyValuePair<string, MyClient> val in dSockets)
                    {
                        MyClient mc = (MyClient)val.Value;
                        byte[] sData = Encoding.Default.GetBytes("Server : " + msg);
                        mc.s.Send(sData, 0, sData.Length, 0);
                    }
                }
            }
        }
    }
}
