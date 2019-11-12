using System;
using System.Diagnostics;

namespace ProcessBasics
{
    class ExampleProcesses
    {
        private Process[] localProcsAll;
        /// <summary>
        /// Prints all the running processes.
        /// </summary>
        public void printAllProcesses()
        {
            Console.WriteLine("This method is going to print information of current processes ... ");

            // How to get all the running processes in a local computer
            this.localProcsAll = Process.GetProcesses();
            foreach (Process pr in this.localProcsAll)
            {
                // Print some information from the process: name and id. There are more.
                Console.WriteLine("Process Name = {0}, Id = {1}", pr.ProcessName, pr.Id.ToString());
            }
        }
        /// <summary>
        /// Terminates the process: 
        /// This example shows how to terminate a running process.
        /// </summary>
        public void terminateProcess()
        {
            Console.WriteLine("This method is going to terminate a process: Choose a process ");

            bool stop = false;
            // How to get the current running process, i.e. this program.
            Process currentProc = Process.GetCurrentProcess();

            while (!stop)
            {
                this.printAllProcesses();
 
                Console.WriteLine(" Enter a process id to terminate (enter stop to finish):");
                string inp = Console.ReadLine();
                if (inp == "stop") { stop = true; }
                else
                {
                    foreach (Process p in this.localProcsAll)
                    {
                        try
                        {
                            if (p.Id == Int32.Parse(inp))
                            {
                                // Terminate a specific process
                                p.Kill();
                                Console.WriteLine("Process {0} is terminated ... ", p.ProcessName);
                                Console.ReadLine();
                            }

                        }
                        catch (Exception e)
                        {
                            Console.Out.WriteLine("Your input is not valid ...",e.Message);
                            break;
                        }
                    }
                }
            }
        }
     }
    class Program
    {
        /// <param name="args">The command-line arguments.</param>
        static void Main(string[] args)
        {
            // Uncomment the methods to see the results of the examples
            ExampleProcesses exampleProcesses = new ExampleProcesses();
            exampleProcesses.printAllProcesses();
            exampleProcesses.terminateProcess();
        }
    }
}