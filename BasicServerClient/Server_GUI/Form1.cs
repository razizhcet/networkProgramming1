using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Server_GUI
{
    public partial class Form1 : Form
    {
        Socket sck;
        Socket scc;
        public Form1()
        {
            InitializeComponent();
        }
        Socket socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sck = socket();
            sck.Bind(new IPEndPoint(0, 3));
            sck.Listen(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
