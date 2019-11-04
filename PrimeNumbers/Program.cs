using System;
using System.Diagnostics;
using System.Threading;

namespace PrimeNumbers
{
    public class PrimeNumbers
    {
        public static void printPrimes(int m, int M)
        {
            Boolean isPrime = true;

            if (m > M)
            {
                Console.WriteLine("invalid inputs");
                return;
            }

            for (int n = m; n <= M; n++)
            {
                if (n % 1000 == 0) // This condition mimics IO operations once a while
                    Thread.Sleep(200);

                for (int i = 2; i < n && isPrime; i++)
                    if (n % i == 0)
                    {
                        isPrime = false;
                        break;
                    }

                if (!isPrime)
                    isPrime = true; 
                //else
                  //  Console.Write(n);
            }
                
        }
    }
    class Program
    {
        private static void runSequential(int m, int M)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            PrimeNumbers.printPrimes(m,M);
            sw.Stop();
            Console.WriteLine("Time for sequential version is {0} msec,",sw.ElapsedMilliseconds);
        }

        private static void runConcurrent(int m, int M, int nt)
        {
            Stopwatch sw = new Stopwatch();

            int numTs = nt;
            int s = (M - m) / nt;
            int l = 0, u = 0;

            Thread[] ts = new Thread[numTs];
            for (int i = 0; i < numTs; i++)
            {
                l = m + s * i;
                if (i == numTs - 1)
                    u = M;
                else
                    u = m + s * (i + 1);

                ts[i] = new Thread(() => PrimeNumbers.printPrimes(l, u));
                //Console.WriteLine("Segments are {0} and {1}:",l , u);
            }

            sw.Start();
            for (int i = 0; i < numTs; i++)
                ts[i].Start();

            for (int i = 0; i < numTs; i++)
                ts[i].Join();

            sw.Stop();
            Console.WriteLine("Time for concurrent version with {0} threads is {1} msec,", nt, sw.ElapsedMilliseconds);
        }

        static void Main(string[] args)
        {
            int min = 5, max = 1000000, step = 4;

            Program.runSequential(min, max);

            for(int t = 2; t < 10; t += step)
            {
                Thread.Sleep(2000);
                Program.runConcurrent(min, max, t);
            }


        }
    }
}
