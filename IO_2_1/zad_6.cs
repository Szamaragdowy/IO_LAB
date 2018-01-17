using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZAD_6
{
    class Program
    {
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

            Console.WriteLine(new ASCIIEncoding().GetString(buffer), ConsoleColor.Green);

            fs.Close();
            manualEvent.Set();
        }
    }
    
}
