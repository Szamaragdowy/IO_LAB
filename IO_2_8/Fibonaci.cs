using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_2_8
{
    class Fibonaci
    {
        public  int fib_r(int n)
        {
            if ((n == 1) || (n == 2))
                return 1;
            else
                return fib_r(n - 1) + fib_r(n - 2);
        }

        public int fib_i(int n)
        {
            if ((n == 1) || (n == 2))
                return 1;
            else
                return fib_r(n - 1) + fib_r(n - 2);
        }


    }
}
