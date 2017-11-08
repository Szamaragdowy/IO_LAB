using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IO_3
{
    class Program
    {
        static void Main(string[] args)
        {
            ZadaniaTAP zadania = new ZadaniaTAP();

            

            var task = zadania.Zadanie3("http://www.feedforall.com/sample.xml");
            task.Wait();

            var rss = task.GetAwaiter().GetResult();

            Console.Write(rss);


        }
    }
}
