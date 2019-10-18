using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;

namespace PipeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pipe client is being executed ...");

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
            }
        }
    }
}
