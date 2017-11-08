using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_3_10
{
    class Program
    {
        delegate int myDeleageType(int i);

        static async Task foo()
        {
            await foo();
        }

         static void Main(string[] args)
        {
            myDeleageType foo = x => x + 1;
        }
    }
}
