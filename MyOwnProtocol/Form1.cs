using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyOwnProtocol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            MyOwnServer server = new MyOwnServer("127.0.0.1", 2048, 1024);
            var t = server.start();


            MyOwnClient client = new MyOwnClient("127.0.0.1", 2048);


            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;


            client.KeepPinging("test", ct);


            Thread.Sleep(3000);

            Console.WriteLine("zakonczenia dzialania 5 s");
        }
    }
}
