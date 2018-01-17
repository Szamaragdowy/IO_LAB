using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_1
{
    class zad_1
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(ThreadProc, new object[] {500,1});
            ThreadPool.QueueUserWorkItem(ThreadProc, new object[] {1000,2});
            Thread.Sleep(3000);
        }
        static void ThreadProc(Object stateInfo)
        {
            Thread.Sleep((int)((object[])stateInfo)[0]);
            Console.WriteLine("Hej jestem watek numer: " + ((object[])stateInfo)[1] + " i czekalem " + ((object[])stateInfo)[0]);
        }
    }
}
