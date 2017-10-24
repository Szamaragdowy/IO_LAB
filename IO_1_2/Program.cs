using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(Server);
            ThreadPool.QueueUserWorkItem(Client, "wiadomosc od klienta 1");
            ThreadPool.QueueUserWorkItem(Client, "wiadomosc od klienta 2");

            /*
             * Nie da się przeiwidzieć kolejności wysłania wiadomości przez klientów
             * Srver odbiera wiadomośći po kolei, na jednym wątku
             * Nie moze utrzymać kliku sesji na raz
             * 
             */
            Thread.Sleep(5000);
        }

        static void Server(Object stateInfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);

            server.Start();

            while (true)
            {

                TcpClient client = server.AcceptTcpClient();

                byte[] buffer = new byte[1024];


                client.GetStream().Read(buffer, 0, 1024);
                Console.WriteLine("--SERVER --- Odebral  " +  new ASCIIEncoding().GetString(buffer));

                client.GetStream().Write(buffer, 0, buffer.Length);

                client.Close();

            }
        }

        static void Client(Object stateInfo)
        {
            TcpClient client = new TcpClient();


            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));

            String wiadomosc = (String)stateInfo;
            Console.WriteLine("Client wyslal  \"" + wiadomosc+" \" ");
            byte[] message = new ASCIIEncoding().GetBytes(wiadomosc);
         
            client.GetStream().Write(message, 0, message.Length);

            client.GetStream().Read(message, 0, message.Length);
        }
    }
}
