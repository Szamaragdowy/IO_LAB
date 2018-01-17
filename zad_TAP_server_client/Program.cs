using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZAD_TAP_server_client
{
    class Program
    {       
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 2048,1024);
            var t = server.start();

            //Task.WaitAll();

            Client client = new Client("127.0.0.1", 2048);


            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;


            client.KeepPinging("test", ct);


            Thread.Sleep(3000);

            Console.WriteLine("zakonczenia dzialania 5 s");
        }
    }
}


