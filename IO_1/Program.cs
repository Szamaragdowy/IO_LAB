using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_1
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(ThreadProc, new object[] {500,1});
            ThreadPool.QueueUserWorkItem(ThreadProc, new object[] {1000,2});

            Thread.Sleep(3000);
        }

        static void ThreadProc(Object stateInfo)
        {
            var time = ((object[])stateInfo)[0];
            var number = ((object[])stateInfo)[1];
            Thread.Sleep((int)time);

            Console.WriteLine("Hej jestem watek numer: " + number + " i czekalem " + time);
        }
    }
}
