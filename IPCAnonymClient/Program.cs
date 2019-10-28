using System;
using System.IO;
using System.IO.Pipes;

namespace IPCAnonymClient
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length < 1) return;

            // Get read pipe handles
            string pipeReadHandle = args[0];

            Console.WriteLine("Child process started ...");

            // Create one anonymous pipes to read
            using (var pipeRead = new AnonymousPipeClientStream(PipeDirection.In, pipeReadHandle))
            {
                try
                {
                    // Get message from other process
                    using (var sr = new StreamReader(pipeRead))
                    {
                        string msg;
                        // Wait for 'sync message' from the other process
                        Console.WriteLine("Child process is waiting to receive the message ...");
                        do
                        {
                            msg = sr.ReadLine();
                            if(msg != null)
                                Console.WriteLine(msg);
                        } while (msg == null || !msg.StartsWith("END"));
                    }
                }
                catch (Exception ex)
                {
                    //TODO Exception handling/logging
                    throw;
                }
            }
        }
    }
}