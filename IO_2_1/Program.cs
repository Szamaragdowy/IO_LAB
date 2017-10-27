using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_2_6
{
    class Program
    {
        private static Object castle = new Object();

        private static ManualResetEvent manualEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            string path = "C:\\Users\\Hubi\\Desktop\\Test.txt";
            byte[] buffer = new byte[1024];
            FileStream fs = File.OpenRead(path);

            manualEvent = new ManualResetEvent(false);
            fs.BeginRead(buffer, 0, buffer.Length, myAsyncCallback, new object[] { fs, buffer });

            manualEvent.WaitOne(5000, false);
        }

        static void myAsyncCallback(IAsyncResult state)
        {
            object[] tablica = (object[]) state.AsyncState;

            FileStream fs = (FileStream)tablica[0];
            byte[] buffer = (byte[])tablica[1];

            writeConsoleMessage(new ASCIIEncoding().GetString(buffer), ConsoleColor.Green);

            fs.Close();
            manualEvent.Set();
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
