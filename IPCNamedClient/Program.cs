using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;

namespace IPCNamedClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pipe Client is being executed ...");
            Console.WriteLine("[Client] waiting to receive a message (press ENTER to exit)");

            var server = new NamedPipeServerStream("PipesOfPiece");
            server.WaitForConnection();
            StreamReader reader = new StreamReader(server);
            StreamWriter writer = new StreamWriter(server);
            while (true){
                var line = reader.ReadLine();
                Console.WriteLine(line);
                writer.WriteLine(String.Join("", line.Reverse()));
                // Complete this line to open a url given by the pipe server 
                //System.Diagnostics.Process.Start("chrome",line);
                writer.Flush();
                //if(Console.ReadLine())
            }
        }
    }
}
