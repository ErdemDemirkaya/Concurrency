using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;

namespace IPCAnonymServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started parent process ...");

            // Create separate process
            var childProcess = new Process
            {
                StartInfo =
                {
                    FileName = "../../../../IPCAnonymClient/bin/Debug/netcoreapp3.0/IPCAnonymClient",
                    CreateNoWindow = true,
                    UseShellExecute = true
                }
            };

            Console.WriteLine("Information of the child process is given ...");


            // Create one anonymous pipes to write: the pipe is one-way
            using (var pipeWrite = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable))
            {
                Console.WriteLine("Child process is going to be started soon ...");

                // Pass to the other process handles to the 2 pipes
                childProcess.StartInfo.Arguments = pipeWrite.GetClientHandleAsString();
                childProcess.Start();

                Console.WriteLine("Started child process (Client process)...");
                Console.WriteLine();

                pipeWrite.DisposeLocalCopyOfClientHandle();

                try
                {
                    using (var sw = new StreamWriter(pipeWrite))
                    {
                        // Send a 'sync message' and wait for the other process to receive it
                        Console.WriteLine("Sending message to the child ...");

                        sw.Write("SYNC");
                        // TODO this method raise an exception, check and fix.
                        //pipeWrite.WaitForPipeDrain();

                        //Console.ReadLine();

                        // Send message to the other process
                        sw.Write("Hello from Process your parent!");
                        //pipeWrite.WaitForPipeDrain();
                        
                        //Console.ReadLine();
                        sw.Write("END");
                        //pipeWrite.WaitForPipeDrain();

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    childProcess.WaitForExit();
                    childProcess.Close();
                }

            }
        }
    }
}