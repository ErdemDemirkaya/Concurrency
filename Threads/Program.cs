using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadBasics
{
    class ExamplesWeekTwo
    {
        public class ThreadsList
        {
            /// <summary>
            /// Prints the threads:
            ///
            /// </summary>
            public void printThreads()
            {
                Console.WriteLine(" This method is going to print information of threads ... ");
                // Get the current process
                Process proc = System.Diagnostics.Process.GetCurrentProcess();

                // Print the information of the process
                Console.WriteLine("process: {0},  id: {1}", proc.ProcessName, proc.Id);

                // Print basic information for each thread
                foreach (ProcessThread pt in proc.Threads)
                {
                    Console.WriteLine("-----------------------");
                    Console.WriteLine(" Thread: {0}, CPU time: {1}, Priority: {2}, Thread state: {3}", pt.Id, pt.TotalProcessorTime, pt.BasePriority, pt.ThreadState.ToString());
                }
            }

            public void runExample()
            {
                this.printThreads();
            }
        }
    

    /// <summary>
    /// Class <c>Counter</c> models a simple counter which increments its.
    /// </summary>
    public class Counter
    {
        public string Name { get; set; }
        public int State { get; set; } 

        public Counter(string n)
        {
            this.Name = n;
            this.State = 0;
        }

        /// <summary>
        /// Counts (increments by one) this instance.
        /// </summary>
        /// <returns>The State.</returns>
        public int count()
        {
            this.State++;
            return this.State;
        }

        /// <summary>
        /// Counts up to a certain point.
        /// </summary>
        public void countUntil()
        {
            for(int i=0; i<1000; i++)
            {
                Console.WriteLine(this.Name+this.count().ToString());
            }
        }
    }

    public class ThreadsJoin
    {
        public int WT { get; set; }

        public ThreadsJoin(int t)
        {
            this.WT = t;
        }

        public void runTasks()
        {
            /// We instantiate two objects from the counter.
            Counter c_A = new Counter("A");
            Counter c_B = new Counter("B");

            /// We create two threads of execution. Each has a task to count until a certain number.
            Thread t_A = new Thread(c_A.countUntil);
            Thread t_B = new Thread(c_B.countUntil);

            Console.WriteLine("Thread id is:"+  t_A.ManagedThreadId.ToString());
            Console.WriteLine("Thread id is:" + t_B.ManagedThreadId.ToString());

            Thread.Sleep(WT);

            /// We start both threads here.
            t_A.Start();
            t_B.Start();

            Thread.Sleep(WT);

            /// The main thread waits here for both threads to join.
            t_A.Join();
            t_B.Join();
        }
    }

    /// <summary>
    /// Examples week two: contains examples about:
    /// - creation and starting threads.
    /// - printing thread information. 
    /// - joining to a thread.
    /// - puting a thread to sleep.
    /// </summary>
    class ExampleThreadCreation
    {
        private static void printCounts()
        {
            int c = 0, limit = 1000; 
            for ( c = 0; c < limit; c++) 
                Console.Write("{0},", c);
        }

        private static void printChars(int n, char c)
        {
            for (int i = 0; i < n; i++)
                Console.Write("{0},", c);
        }

        /// <summary>
        /// Creates the one thread: This example presents: 
        ///  - how to define a task, 
        ///  - how to create a thread, 
        ///  - how to start the execution of a thread.
        /// </summary>
        public void createOneThread()
        {
            Console.WriteLine("Press a key to start a counting thread ... ");
            Console.Read();
            // We create an instaince of Thread, with a given task
            // Note: Here the given task is defined using a (static) method.
            Thread tOne = new Thread(printCounts);

            // Check the next statement to see how to pass a task using a lambda expression.
            //Thread tOne = new Thread(() => { for (int c = 0; c < 1000; c++) Console.Write("{0},", c); });

            // Here we start the thread to perform the task
            tOne.Start();
            Console.WriteLine(" The main thread is going to terminate ... ");

        }

        /// <summary>
        /// Creates the two Threads: This example shows:
        /// - creation of two threads.
        /// - the output result when threads are interleaved.
        /// </summary>
        public void createTwoXYThreads()
        {
            Console.WriteLine("Press a key to start two threads printing separate characters ... ");
            Console.Read();
            // Check: given tasks will be overlapped ... expect interleaved prints. Discuss why?
            Thread tOne = new Thread(() => { for (int i = 0; i < 1000; i++) Console.Write("X"); });
            Thread tTwo = new Thread(() => { for (int i = 0; i < 1000; i++) Console.Write("Y"); });

            tOne.Start();
            tTwo.Start();
            Console.WriteLine(" The main thread has terminated ... ");

        }

        /// <summary>
        /// Creates the multiple threads: this example shows:
        /// - creation of multiple threads to be running in parallel.
        /// - passing parameters to the task.
        /// - check the output. Can you predict the pattern of interleaving?
        /// </summary>
        public void createMultipleThreads()
        {
            Console.WriteLine("Press a key to start multiple threads printing separate characters ... ");
            Console.Read();

            int num = 500;
            char[] chars = new char[] { 'A', 'B', 'C', 'D', 'E' };
            Thread[] threads = new Thread[chars.Length];

            for(int i = 0; i<chars.Length; i++)
            {
                threads[i] = new Thread(() => ExamplesWeekTwo.printChars(num,chars[i]));
                threads[i].Start();
            }


            // Here the main thread waits for all to finish
            for (int i = 0; i < chars.Length; i++)
                threads[i].Join();

            Console.WriteLine(" The main thread has terminated ... ");

        }

        public void run



    }
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("The program starts now ....");
            //Creator creator = new Creator(1000);
            //creator.runTasks();
            //Console.WriteLine("The program finishes now ....");

            ExamplesWeekTwo ewTwo = new ExamplesWeekTwo();
            ewTwo.runExampleThreadsList();
            ewTwo.runExampleThreadsCreation();
            ewTwo.runExampleThreadsJoin();
            example.createOneThread();
            example.createTwoXYThreads();
            example.createMultipleThreads();

        }
    }
}
