using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_3_11
{
    class Program
    {
        delegate int myDeleageType(int i);

        static void Main(string[] args)
        {
            myDeleageType foo = x => x + 1;
        }
    }
}
