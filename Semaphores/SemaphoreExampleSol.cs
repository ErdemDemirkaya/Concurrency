using System;
using System.Collections.Generic;
using System.Threading;

namespace SemaphoreExampleSol
{
    class Buffer
    {
        public int[] buffer;
        public int emptyIndex { get; set; }
        public Buffer(int size)
        {
            buffer = new int[size];
            emptyIndex = 0;
        }
        public void write(int value)
        {
            buffer[emptyIndex]++; //= value;
            emptyIndex = (emptyIndex + 1) % buffer.Length;
        }
        public int read()
        {
            int readIndex = (emptyIndex + buffer.Length - 1) % buffer.Length;
            int result = buffer[readIndex]--;
            emptyIndex = readIndex;
            return result;
        }
    }
    class Producer
    {
        private int minTime { get; set; }
        private int maxTime { get; set; }
        private Buffer buffer;
        private Semaphore producerSemaphore, consumerSemaphore;

        public Producer(int min, int max, Buffer buf, Semaphore psem, Semaphore csem)
        {
            this.minTime = min;
            this.maxTime = max;
            this.buffer = buf;
            this.producerSemaphore = psem;
            this.consumerSemaphore = csem;
        }
        public void produce()
        {
            Thread.Sleep(new Random().Next(minTime, maxTime));
            int data = new Random().Next();

            producerSemaphore.WaitOne();
            this.buffer.write(data);
            Console.Out.WriteLine("[Producer] {0} is written", data.ToString());
            consumerSemaphore.Release();
        }
        public void MultiProduce(Object n)
        {
            int num = (int)n;
            for (int i = 0; i < num; i++)
            {
                this.produce();
            }
        }
    }
    class Consumer
    {
        private int minTime { get; set; }
        private int maxTime { get; set; }
        private Buffer buffer;
        private Semaphore producerSemaphore, consumerSemaphore;

        public Consumer(int min, int max, Buffer buf, Semaphore psem, Semaphore csem)
        {
            this.minTime = min;
            this.maxTime = max;
            this.buffer = buf;
            this.producerSemaphore = psem;
            this.consumerSemaphore = csem;
        }
        public void consume()
        {
            Thread.Sleep(new Random().Next(minTime, maxTime));

            consumerSemaphore.WaitOne();
            int data = this.buffer.read();
            Console.Out.WriteLine("[Consumer] {0} is read ", data.ToString());
            producerSemaphore.Release();
        }
        public void MultiConsume(Object n)
        {
            int num = (int)n;

            for (int i = 0; i < num; i++)
            {
                this.consume();
            }

        }
    }
    class ProducerConsumerSimulator
    {
        public Buffer buffer;
        public Semaphore psem , csem;

        public int minTime { get; set; }
        public int maxTime { get; set; }

        public ProducerConsumerSimulator(int min, int max)
        {
            buffer = new Buffer(2);
            // todo: check the initial values. Why are they different?
            psem = new Semaphore(1,1);
            csem = new Semaphore(0,1); 
        }

        public void sequentialOneProducerOneConsumer()
        {
            int iterations = 100;
            Console.Out.WriteLine("[SeqSimulator] is going to start ....");
            Producer p = new Producer(this.minTime, this.maxTime, this.buffer, this.psem , this.csem);
            Consumer c = new Consumer(this.minTime, this.maxTime, this.buffer, this.psem , this.csem);

            for (int i = 0; i < iterations; i++)
            {
                p.produce();
                c.consume();
            }


        }

        public void concurrentOneProducerOneConsumer()
        {
            int iterations = 100;

            Console.Out.WriteLine("[ConcSimulator] is going to start ....");
            Producer p = new Producer(this.minTime, this.maxTime, this.buffer, this.psem, this.csem);
            Consumer c = new Consumer(this.minTime, this.maxTime, this.buffer, this.psem, this.csem);

            Thread producerThread = new Thread(() => p.MultiProduce(iterations));
            Thread consumerThread = new Thread(() => c.MultiConsume(iterations));

            Thread.Sleep(100);

            producerThread.Start();
            consumerThread.Start();

            producerThread.Join();
            consumerThread.Join();

        }

        public void concurrentMultiProducerMultiConsumer()
        {
            //todo: implement the method for multiple producers and multiple consumers
            int iterations = 100 , num = 5;

            Console.Out.WriteLine("[ConcSimulator] is going to start ....");
            Producer[] ps = new Producer[num];
            Consumer[] cs = new Consumer[num];
            LinkedList<Thread> threads = new LinkedList<Thread>();

            for(int i = 0; i< num; i++)
            {
                ps[i] = new Producer(this.minTime, this.maxTime, this.buffer, this.psem, this.csem);
                cs[i] = new Consumer(this.minTime, this.maxTime, this.buffer, this.psem, this.csem);
            }

            for(int i=0; i<num; i++)
            {
                threads.AddFirst(new Thread(ps[i].MultiProduce));
            }
            for (int i = 0; i < num; i++)
            {
                threads.AddFirst(new Thread(cs[i].MultiConsume));
            }

            foreach (Thread t in threads)
                t.Start(iterations);
            Thread.Sleep(100);

            foreach (Thread t in threads)
                t.Join();

            for(int i = 0; i< buffer.buffer.Length; i++)
                Console.WriteLine("[Buffer] {0} ",buffer.buffer[i]);

        }

    }
}