﻿using System;
using System.Diagnostics;
/// <summary>
/// This example shows how to define a process and start it.
/// Check here: https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process?view=netframework-4.8
/// </summary>
namespace ProcessCreation
{
    class Program
    {

        static void Main(string[] args)
        {

            // Todo: Create an object from ProcessStartInfo
            // Implement your code here ...


            // Todo: Provide the path and the name of your executable file
            // Implement your code here


            //prInfo.CreateNoWindow = false; // This means start the process in a new window
            //prInfo.UseShellExecute = false;

            try
            {
                // Todo: Start your process and then wait for its exit
                // Implement your code here

            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
        }
    }
}
