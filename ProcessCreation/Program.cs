using System;
using System.Diagnostics;

namespace ProcessCreation
{
    class Program
    {

        static void Main(string[] args)
        {
 
            ProcessStartInfo prInfo = new ProcessStartInfo();
            prInfo.FileName = "../../../../Processes/bin/Debug/netcoreapp3.0/Processes";
            prInfo.CreateNoWindow = false; // This means start the process in a new window
            prInfo.UseShellExecute = false;

            try
            {
                using (Process pr = Process.Start(prInfo))
                {
                    pr.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
        }
    }
}
