using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ZAD_TAP_server_client
{
    class Server
    {
        private TcpListener server;
        private String ipaddress;
        private int port;
        private bool running;
        private int buffor_size;

        public Server(String ipaddress, int port, int buffor_size)
        {
            this.server = new TcpListener(IPAddress.Parse(ipaddress), port);
            this.buffor_size = buffor_size;
        }

        public string Ipaddress {
            get => ipaddress; 
            set { if (!running) ipaddress = value; }
        }

        public int Port {
            get => port;
            set { if (!running) port = value; }
        }

        public bool Running {
            get => running;
        }

        public async Task start()
        {
            try
            {
                running = true;
                server.Start();
                while (true)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    byte[] buffer = new byte[buffor_size];
                    await client.GetStream().ReadAsync(buffer, 0, buffer.Length).ContinueWith(
                        async (t) =>
                        {
                            int i = t.Result;
                            while (true)
                            {
                                await client.GetStream().WriteAsync(buffer, 0, i);
                                i = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                            }
                        });
                }
            }
            catch ( Exception e)
            {
                running = false;
                Console.WriteLine("Błąd");
            }
        }
    }
}
