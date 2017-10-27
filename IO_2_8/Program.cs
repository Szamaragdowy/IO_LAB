using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_2_8
{
    class Program
    {
        delegate int DelegateType(int arguments);

        static DelegateType Silnia_i;
        static DelegateType Silnia_r;
        static DelegateType Fibonaci_i;
        static DelegateType Fibonaci_r;

        static int silnia_r(int arguments)
        {
            int i = arguments;

            if (i < 1)
                return 1;
            else
                return i * silnia_i(i - 1);

        }

        static int silnia_i(int arguments)
        {
            int n = arguments;
           
            int result = 1;
            for (int i = 1; i < n; i++)
            {
                result *= i;
            }
            return result;

        }

        static int fibonaci_r(int arguments)
        {
            int n =  arguments;
            if ((n == 1) || (n == 2))
                return 1;
            else
                return fibonaci_r(n - 1) + fibonaci_r(n - 2);
        }

        static int fibonaci_i(int arguments)
        {
            return 0;
        }

        static void Main(string[] args)
        {
            Silnia_i = new DelegateType(silnia_i);
            Silnia_r = new DelegateType(silnia_r);
            Fibonaci_i = new DelegateType(fibonaci_i);
            Fibonaci_r= new DelegateType(fibonaci_r);

            IAsyncResult ar_s_i = Silnia_i.BeginInvoke(5, null, null);
            IAsyncResult ar_s_r = Silnia_r.BeginInvoke(5, null, null);
            IAsyncResult ar_f_i = Fibonaci_i.BeginInvoke(20, null, null);
            IAsyncResult ar_f_r = Fibonaci_r.BeginInvoke(20, null, null);

         


            int wynik_silnia_i = Silnia_i.EndInvoke(ar_s_i);
            int wynik_silnia_r = Silnia_r.EndInvoke(ar_s_r);
            int wynik_fibonaci_i = Fibonaci_i.EndInvoke(ar_f_i);
            int wynik_fibonaci_r = Fibonaci_r.EndInvoke(ar_f_r);

            Console.WriteLine("wynik silni iteracyjnie: " + wynik_silnia_i);
            Console.WriteLine("wynik silni rekurencyjnie: " + wynik_silnia_r);
            Console.WriteLine("wynik fibonaci iteracyjnie: " + wynik_fibonaci_i);
            Console.WriteLine("wynik fibonaci rekurencyjnie: " + wynik_fibonaci_r);
        }

    }
}
