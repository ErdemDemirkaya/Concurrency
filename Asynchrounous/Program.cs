using System;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchrounous
{
    class ExamplesWeekFour
    {
        public class ExampleTask
        {
            public class Coffee { }
            public class Toast { }
            public class Juice { }
            public class Bacon { }

            public class Breakfast
            {
                private readonly int tunit = 1000;
                private int coffeeTime = 1, toastTime = 1, baconTime = 1, juiceTime = 1;
                public Breakfast(int c = 5, int t = 8, int b = 10, int j = 3)
                {
                    coffeeTime = c; toastTime = t; baconTime = b; juiceTime = j;
                }
                private void prepare(int t,string m)
                {
                    Console.Write("\nstarted with preparing {0}",m);
                    Thread.Sleep(tunit*t);
                    Console.Write("\n{0} is ready ... ", m);
                }
                public Coffee prepareCoffee(){ this.prepare(coffeeTime,"coffee"); return new Coffee(); }
                public Toast prepareToast() { this.prepare(toastTime,"toast"); return new Toast(); }
                public Juice prepareJuice() { this.prepare(juiceTime,"juice"); return new Juice(); }
                public Bacon prepareBacon() { this.prepare(baconTime,"bacon"); return new Bacon(); }
            }

            public class AsyncBreakfast
            {
                private readonly int tunit = 1000;
                private int coffeeTime = 1, toastTime = 1, baconTime = 1, juiceTime = 1;
                public AsyncBreakfast(int c = 7, int t = 10, int b = 15, int j = 5)
                {
                    coffeeTime = c; toastTime = t; baconTime = b; juiceTime = j;
                }
                private void prepare(int t, string m)
                {
                    Console.Write("\nstarted with preparing {0}", m);
                    Thread.Sleep(tunit * t); // Time needed to do the task.
                    Console.Write("\n{0} is ready ... ", m);
                }
                public async Task<Coffee> prepareCoffee()
                {
                    var coffee = Task.Run(() => { this.prepare(coffeeTime, "coffee"); return new Coffee(); });
                    var result = await coffee;
                    return result;
                }

                public async Task<Toast> prepareToast()
                {
                    var toast = Task.Run(() => { this.prepare(toastTime, "toast"); return new Toast(); });
                    var result = await toast;
                    return result;
                }

                public async Task<Juice> prepareJuice()
                {
                    var juice = Task.Run(() => { this.prepare(juiceTime, "juice"); return new Juice(); });
                    var result = await juice;
                    return result;
                }

                public async Task<Bacon> prepareBacon()
                {
                    var bacon = Task.Run(() => { this.prepare(baconTime, "bacon"); return new Bacon(); });
                    var result = await bacon;
                    return result;
                }
            }

            public void prepareBreakfast()
            {
                Console.WriteLine("\n Breakfast is going to be prepared synchronously ....");
                Breakfast breakfast = new Breakfast();
                Coffee c = breakfast.prepareCoffee();
                Bacon b = breakfast.prepareBacon();
                Juice j = breakfast.prepareJuice();
                Toast t = breakfast.prepareToast();
            }

            public void prepareAsyncBreakfast()
            {
                Console.WriteLine("\n Breakfast is going to be prepared asynchronously ....");
                AsyncBreakfast breakfast = new AsyncBreakfast();
                Task<Coffee> c = breakfast.prepareCoffee();
                Task<Bacon> b = breakfast.prepareBacon();
                Task<Juice> j = breakfast.prepareJuice();
                Task<Toast> t = breakfast.prepareToast();

                Console.WriteLine("\n Prepare the table while the breakfast ie getting ready ...");

                c.Wait();
                b.Wait();
                j.Wait();
                t.Wait();

            }
 
            /// <summary>
            /// This method presents how to define and run couple of synchronous tasks.
            /// </summary>
            public void runSyncTasks()
            {
                // Tasks are potentially asychronous
                Task printingTask = Task.Run(() => { Console.WriteLine("\n A separate task is printing this ..."); } );

                Task<string> taskWithStringResult = Task.Run( () => { return "\n Hello World from a task."; });
                Console.WriteLine("\n task with string result returns {0} ",taskWithStringResult.Result);

                Task<int> timeConsumingTask = Task.Run( () =>
                {
                    // Assume a very heavy calculation is happening here ...
                    int r = 0;
                    for (int i = 0; i < 1_000_000; i++)
                        r += i;
                    Thread.Sleep(3000); // wait for a few seconds ...
                    return r;
                });
                Console.WriteLine( " task with a heavy calculation returns {0}", timeConsumingTask.Result );
               
            }

            public void runAsyncTasks()
            {
                // Tasks are potentially asychronous
                Task printingTask = Task.Run(() => { Console.WriteLine("\n A separate task is printing this ..."); });

                Task<string> taskWithStringResult = Task.Run(() => { return "\n Hello World from a task."; });
                Console.WriteLine("\n task with string result returns {0} ", taskWithStringResult.Result);

                Task<int> timeConsumingTask = Task.Run(() =>
                {
                    // Assume a very heavy calculation is happening here ...
                    int r = 0;
                    for (int i = 0; i < 1_000_000; i++)
                        r += i;
                    Thread.Sleep(3000); // wait for a few seconds ...
                    return r;
                });
                Console.WriteLine(" task with a heavy calculation returns {0}", timeConsumingTask.Result);

            }

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string examplesMsg = "Examples of Week four: Synchronous vs. Asynchronous programming.";

            Console.WriteLine(examplesMsg);

            ExamplesWeekFour.ExampleTask taskExamples = new ExamplesWeekFour.ExampleTask();

            taskExamples.prepareBreakfast();
            taskExamples.prepareAsyncBreakfast();
            //taskExamples.runATask();
        }
    }
}
