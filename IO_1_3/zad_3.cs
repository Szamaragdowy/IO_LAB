using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// przez nie zsynchronizowaną metodę do kolorowania kolory są losowe

namespace IO_1_3
{
    class zad_3
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();

            ThreadPool.QueueUserWorkItem(Client, "#1 Client");
            ThreadPool.QueueUserWorkItem(Client, "#2 Client");
            ThreadPool.QueueUserWorkItem(Client, "#3 Client");
            ThreadPool.QueueUserWorkItem(Client, "#4 Client");

            while (true)
            {
                ThreadPool.QueueUserWorkItem(Obsluga, server.AcceptTcpClient());
            }
        }

        static void Obsluga(Object stateInfo)
        {
            TcpClient client = (TcpClient)stateInfo;

            byte[] buffer = new byte[1024];

            client.GetStream().Read(buffer, 0, 1024);
            writeConsoleMessage("#S  Otrzymalem wiadomosc: " + new ASCIIEncoding().GetString(buffer), ConsoleColor.Red);
            client.GetStream().Write(buffer, 0, buffer.Length);
            client.Close();
        }

        static void Client(Object stateInfo)
        {
            TcpClient client = new TcpClient();

            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));

            String wiadomosc = (String)stateInfo;
            byte[] message = new ASCIIEncoding().GetBytes(wiadomosc);
            client.GetStream().Write(message, 0, message.Length);
            client.GetStream().Read(message, 0, message.Length);
            writeConsoleMessage("#C  Otrzymalem wiadomosc: " + new ASCIIEncoding().GetString(message), ConsoleColor.Green);
        }

        static void writeConsoleMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

