using System;
using System.Diagnostics;
/// <summary>
/// This example shows how to define a process and start it.
/// </summary>
namespace ProcessCreation
{
    class Program
    {

        static void Main(string[] args)
        {

            // First define your process
            ProcessStartInfo prInfo = new ProcessStartInfo();
            prInfo.FileName = "../../../../Processes/bin/Debug/netcoreapp3.0/Processes"; // This is an executable program.
            prInfo.CreateNoWindow = false; // This means start the process in a new window
            prInfo.UseShellExecute = false;

            try
            {
                // Start the defined process
                using (Process pr = Process.Start(prInfo))
                {
                    pr.WaitForExit(); // Parent process waits here to have the child finished.
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
        }
    }
}
