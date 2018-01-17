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
    class Client
    {
        private TcpClient client;
        private String ipaddress;
        private int port;
        private bool running;

        public Client(String ipaddress, int port)
        {
            this.client = new TcpClient();
            this.port = port;
            this.ipaddress = ipaddress;
        }

        public string Ipaddress
        {
            get => ipaddress;
            set { if (!running) ipaddress = value; }
        }

        public int Port
        {
            get => port;
            set { if (!running) port = value; }
        }

        public bool Running
        {
            get => running;
        }


        //connect / ping / keeppinging

        public async Task Ping(String wiadomosc)
        {
            client.Connect(IPAddress.Parse(ipaddress), port);
            
            byte[] message = new ASCIIEncoding().GetBytes(wiadomosc);

           await  client.GetStream().WriteAsync(message, 0, message.Length).ContinueWith(
                async (t) =>
                {
                   await client.GetStream().ReadAsync(message, 0, message.Length);
                    Console.WriteLine("Odebrałem   " + new ASCIIEncoding().GetString(message));
                });  
        }

        public async Task KeepPinging(String wiadomosc, CancellationToken cancellationtoken)
        {
            client.Connect(IPAddress.Parse(ipaddress), port);
            while (cancellationtoken.IsCancellationRequested) { 
                byte[] message = new ASCIIEncoding().GetBytes(wiadomosc);

                await client.GetStream().WriteAsync(message, 0, message.Length, cancellationtoken).ContinueWith(
                     async (t) =>
                     {
                         await client.GetStream().ReadAsync(message, 0, message.Length);
                         Console.WriteLine("Odebrałem   " + new ASCIIEncoding().GetString(message));
                     });
            }
        }
    }
}
