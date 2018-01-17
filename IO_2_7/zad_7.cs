using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_2_7
{
    class zad_7
    {
        private static Object castle = new Object();
        private static ManualResetEvent manualEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            string path = "C:\\Users\\Hubi\\Desktop\\Test.txt";
            byte[] buffer = new byte[1024];
            FileStream fs = File.OpenRead(path);
   
            var ar = fs.BeginRead(buffer, 0, buffer.Length, null /*new AsyncCallback(EndReadCallback)*/, new object[] { fs, buffer, manualEvent });

            Thread.Sleep(200);

            fs.EndRead(ar);

            writeConsoleMessage(new ASCIIEncoding().GetString(buffer), ConsoleColor.Green);
            fs.Close();

            writeConsoleMessage(" skonczyl sie glowny watek", ConsoleColor.Red);
        }

        static void writeConsoleMessage(string message, ConsoleColor color)
        {
            lock (castle)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }

}
