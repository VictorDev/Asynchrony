using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = Thread.CurrentThread;
            Console.WriteLine($"Имя потока: {t.Name}");
            t.Name = "Метод Main";
            Console.WriteLine($"Имя потока: {t.Name}");

            Console.WriteLine($"Запущен ли поток : {t.IsAlive}");
            Console.WriteLine($"Приоретет потока : {t.Priority}");
            Console.WriteLine($"Статус потока : {t.ThreadState}");

            int[] arr = new int[100];
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rnd.Next(0, arr.Length);
            Console.WriteLine("\nИсходный массив: ");
            for (int i = 0; i < arr.Length; i++)
                Console.Write($"{arr[i]} ");

            int n = 5;

            Thread[] sorting = new Thread[n];
            SortData SD = new SortData();
            SD.x = n;
            SD.n = 0;
            SD.sourse = arr;
            for (int i = 0; i < n; i++)
            {
                sorting[i] = new Thread(new ParameterizedThreadStart(Sort));
                sorting[i].Start(SD);
                SD.n += arr.Length / n;
            }

            for (int i = 0; i < n; i++)
            {
                sorting[i].Join();
            }
            Console.WriteLine("\n\nМассив отсортированный с помошью потоков: ");
            for (int i = 0; i < arr.Length; i++)
                Console.Write($"{arr[i]} ");
            int[] v = Marge(arr);
            Console.WriteLine("\n\nИтоговый отсортированный массив: ");
            for (int i = 0; i < v.Length; i++)
                Console.Write($"{v[i]} ");
            Console.ReadLine();
        }
        public static void Sort(object arr)
        {
            Array.Sort(((SortData)arr).sourse, ((SortData)arr).n, ((SortData)arr).sourse.Length / ((SortData)arr).x);
        }
        struct SortData
        {
            public int n;
            public int x;
            public int[] sourse;
        }
        static int[] Marge(int[] mass)
        {
            int[] b = new int[mass.Length];
            int x = 0, y = 0;
            for (int i = 0; i < 80; i += 40)
            {
                x = i; y = i + 20;
                for (int k = i; k < i + 40; k++)
                {
                    if (x < (i + 20) && y < (i + 40))
                        if (mass[x] > mass[y] && y < i + 40)
                            b[k] = mass[y++];
                        else
                            b[k] = mass[x++];
                    else
                        if (y < i + 40)
                        b[k] = mass[y++];
                    else
                        b[k] = mass[x++];
                }
            }

            int[] c = new int[100];
            x = 0; y = 40;
            for (int k = 0; k < 80; k++)
            {
                if (x < (40) && y < (80))
                    if (b[x] > b[y] && y < 80)
                        c[k] = b[y++];
                    else
                        c[k] = b[x++];
                else
                    if (y < 80)
                    c[k] = b[y++];
                else
                    c[k] = b[x++];
            }

            int[] v = new int[100];
            x = 0; y = 80;
            for (int k = 0; k < 100; k++)
            {
                if (x < (80) && y < (100))
                    if (c[x] > mass[y] && y < 100)
                        v[k] = mass[y++];
                    else
                        v[k] = c[x++];
                else if (y < 100) v[k] = mass[y++];
                else v[k] = c[x++];
            }
            return v;
        }
    }
}