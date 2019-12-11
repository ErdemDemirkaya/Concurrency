using System;
using System.Threading;
//using DiningNormal;
using DiningDeadlock;

namespace DiningPhilosophers
{
    class Program
    {
        static void Main(string[] args)
        {
            int numPhilosphers = 5 , iteration = 1000;
            Table table = new Table(numPhilosphers);
            //table.startSequentialDining(iteration);
            table.startConcurrentDining(iteration);

            Console.WriteLine("[Dinining Philospher] End of the dinner ...");
        }
    }
}
