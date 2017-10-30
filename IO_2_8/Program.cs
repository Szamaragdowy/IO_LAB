using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_2_8
{
    class Program
    {
        delegate int DelegateType(int arguments);

        

        static int silnia_r(int n)
        {
            int factorial = 1;

            if (n <= 1) return 1;

            factorial = n * silnia_r(n - 1);

            return factorial;

        }

        static int silnia_i(int n)
        {
            int factorial = 1;
            for (int i = 1; i <= n; i++)
            {
                factorial *= i;
            }
            return factorial;
        }

        static int fibonaci_r(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            return fibonaci_r(n - 1) + fibonaci_r(n - 2);
        }

        static int fibonaci_i(int n)
        {
            int firstnumber = 0;
            int secondnumber = 1;
            int result = 0;

            if (n == 0) return 0;
            if (n == 1) return 1;

            for (int i = 2; i <= n; i++)
            {
                result = firstnumber + secondnumber;
                firstnumber = secondnumber;
                secondnumber = result;
            }
            return result;
        }

        static void Main(string[] args)
        {
            int a = 15;
            int b = 40;

            Stopwatch stopWatch1 = new Stopwatch();
            stopWatch1.Start();

            DelegateType Silnia_i = new DelegateType(silnia_i);
            IAsyncResult ar_s_i = Silnia_i.BeginInvoke(a, null, null);
            int wynik_silnia_i = Silnia_i.EndInvoke(ar_s_i);
            stopWatch1.Stop();
            Console.WriteLine("Silnia Iteracyjnie {0} is {1}. Time elapsed: {2}ms",
                a, wynik_silnia_i, stopWatch1.ElapsedMilliseconds);



            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();

            DelegateType Silnia_r = new DelegateType(silnia_r);  
            IAsyncResult ar_s_r = Silnia_r.BeginInvoke(a, null, null);
            int wynik_silnia_r = Silnia_r.EndInvoke(ar_s_r);
            stopWatch2.Stop();
            Console.WriteLine("Silnia rekurencyjnie {0} is {1}. Time elapsed: {2}ms", 
                a, wynik_silnia_r, stopWatch2.ElapsedMilliseconds);


            Stopwatch stopWatch3 = new Stopwatch();
            stopWatch3.Start();


            DelegateType Fibonaci_i = new DelegateType(fibonaci_i);
            IAsyncResult ar_f_i = Fibonaci_i.BeginInvoke(b, null, null);
            int wynik_fibonaci_i = Fibonaci_i.EndInvoke(ar_f_i);
            stopWatch3.Stop();
            Console.WriteLine("Fibonaci Iteracyjnie {0} is {1}. Time elapsed: {2}ms", 
                b, wynik_fibonaci_i, stopWatch3.ElapsedMilliseconds);


            Stopwatch stopWatch4 = new Stopwatch();
            stopWatch4.Start();


            DelegateType Fibonaci_r = new DelegateType(fibonaci_r);
            IAsyncResult ar_f_r = Fibonaci_r.BeginInvoke(b, null, null);
            int wynik_fibonaci_r = Fibonaci_r.EndInvoke(ar_f_r);
            stopWatch4.Stop();
            Console.WriteLine("fibonaci rekurencyjnie {0} is {1}. Time elapsed: {2}ms", 
                b, wynik_fibonaci_r, stopWatch4.ElapsedMilliseconds);

        }

    }
}
