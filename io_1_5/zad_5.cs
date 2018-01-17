using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace io_1_5
{
    class zad_5
    {
        static int variable = 0;
        static int part = 0;
        static int[] tablica;
        private static Object castle = new Object();
        private static Object castle2 = new Object();
        static Random random  =  new Random();
        static int wynik = 0;
        private static ManualResetEvent[] manualEvents;

        static void Main(string[] args)
        {
            writeConsoleMessage("Prosze wpisac rozmiar tablicy: ", ConsoleColor.Cyan);
            variable = int.Parse(Console.ReadLine()); // wczytuje rozmiar tablicy

            create_and_fill_table();                   // tworze tablice i wypelniam 

            writeConsoleMessage("Iloma obliczeniami ma sie zajac jeden watek?: ", ConsoleColor.Yellow);
            part = int.Parse(Console.ReadLine()); //wczytuje liczbe obliczen na jedne watek

            int liczba_watkow = calculate_threads(); //obliczam ilosc watkow

            manualEvents = new ManualResetEvent[liczba_watkow];  //tworze tablice o wielkosci liczby watkow ktora 
                                                                  //pokazuje ktore watki skonczyly a ktore nie

            for(int i = 0; i < liczba_watkow; i++)
            {
                ThreadPool.QueueUserWorkItem(ThreadProc, new object[] { i * part,( i * part) + part, i });  //tworze watki
                                                                        //index poczatkowy tablicy, koncowy,  i index tablicy "manual Events"
                manualEvents[i] = new ManualResetEvent(false);
            }

            WaitHandle.WaitAll(manualEvents); //czekam na zakonczenie wszystkich watkow

            writeConsoleMessage("Wynik: " + wynik, ConsoleColor.Red); //wyswietlam wynik


            //variable = int.Parse(Console.ReadLine());
        }

        static int calculate_threads()
        {
            if (variable % part == 0)
            {
             return variable / part;
            }
            else
            {
             return variable / part + 1;
            }
        }
        static void create_and_fill_table()
        {
            tablica = new int[variable];

            for (int i = 0; i < variable; i++)
            {
                tablica[i] = 1;
                //random.Next(i, j);
            }
        }
        static void writeConsoleMessage(string message, ConsoleColor color)
        {
            lock (castle)
            {
                Console.ForegroundColor = color;
                Console.Write(message);
                Console.ResetColor();
            }
        }
        static void ThreadProc(Object stateInfo)
        {
            int i = 0;
            i = (int)((object[])stateInfo)[0];
            int a = (int)((object[])stateInfo)[1];
            int index = (int)((object[])stateInfo)[2];

            int thread_score = 0;
            for(int j =i ; j < a&&j<tablica.Length; j++)
            {
                thread_score += tablica[j];
            }
            add_to_score(thread_score);

            manualEvents[index].Set();
        }

        static void add_to_score(int a)
        {
            lock (castle2)
            {
                wynik += a;
            }
        } //zsynchronizowana metoda dodaja koncowy wynik
    }
}
