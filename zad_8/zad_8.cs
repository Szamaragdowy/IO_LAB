using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZAD_8
{
    class zad_8
    {
        delegate Int64 DelegateType(int argument);
        static DelegateType Silnia_i;
        static DelegateType Silnia_r;
        static DelegateType Fibonaci_i;
        static DelegateType Fibonaci_r;

        static Int64 silnia_r(int n)
        {
            if (n == 0)  return 1;
            else   return n * silnia_r(n - 1); 
        }

        static Int64 silnia_i(int n)
        {
            Int64 factorial = 1;
            for (int i = 1; i <= n; i++)
            {
                factorial *= i;
            }
            return factorial;
        }

        static Int64 fibonaci_r(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            return fibonaci_r(n - 1) + fibonaci_r(n - 2);
        }

        static Int64 fibonaci_i(int n)
        {
            if (n <= 1) return n;
            Int64 secondnumber = 1;
            Int64 result = 1;
            Int64 number;

            for (int i = 2; i < n; i++)
            {
                number = result;
                result += secondnumber;
                secondnumber = number;
            }
            return result;
        }

        static void ThreadProc(IAsyncResult stateInfo)
        {
            DelegateType delegat = ((object[])(stateInfo.AsyncState))[0] as DelegateType;
            string info = ((object[])(stateInfo.AsyncState))[1] as string;
            AutoResetEvent ev = ((object[])(stateInfo.AsyncState))[2] as AutoResetEvent;

            var result = delegat.EndInvoke(stateInfo).ToString();
            Console.WriteLine(info + ": " + result);

            ev.Set();
        }

        static void Main(string[] args)
        {
            Silnia_i   += silnia_i;
            Silnia_r   += silnia_r;
            Fibonaci_i += fibonaci_i;
            Fibonaci_r += fibonaci_r;

            int a = 10;
            int b = 40;

            AutoResetEvent[] events = new AutoResetEvent[4];

            for (int i = 0; i < 4; i++)
            {
                events[i] = new AutoResetEvent(false);
            }

            var res1 = Fibonaci_i.BeginInvoke(b, ThreadProc, new object[] { Fibonaci_i, "Fibonaci  iteracyjnie", events[0] });

            var res2 = Fibonaci_r.BeginInvoke(b, ThreadProc, new object[] { Fibonaci_r, "Fibonaci  rekurencyjnie", events[1] });

            var res3 = Silnia_i.BeginInvoke(a, ThreadProc, new object[] { Silnia_i, "Silnia iteracyjnie", events[2] });

            var res4 = Silnia_r.BeginInvoke(a, ThreadProc, new object[] { Silnia_r, "Silnia rekurencja", events[3] });

            WaitHandle.WaitAll(events);

            Thread.Sleep(5000);
        }

    }
}
