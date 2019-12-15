using System;
using SemaphoreExampleSol;

namespace Semaphores
{
    class Program
    {
        static void Main(string[] args)
        {
            int m = 10, M = 1000;

            ProducerConsumerSimulator pcSimulator = new ProducerConsumerSimulator(m,M);

            pcSimulator.sequentialOneProducerOneConsumer();
            pcSimulator.concurrentOneProducerOneConsumer();
            pcSimulator.concurrentMultiProducerMultiConsumer();

            Console.WriteLine("[END]");
        }
    }
}
