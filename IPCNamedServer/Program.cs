using System;
using System.IO;
using System.IO.Pipes;

/*
 * This is an example representing how two processes can communicate through NamedPipe
 */

namespace IPCNamedServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pipe Server is being executed ...");
            Console.WriteLine("[Server] Enter a message to be reversed by the client (press ENTER to exit)");

            //Client
            var client = new NamedPipeClientStream("PipesOfPiece");
            client.Connect();

            StreamWriter writer = new StreamWriter(client);

            while (true)
            {
                String input = Console.ReadLine();
                if (String.IsNullOrEmpty(input))
                    break;
                else
                {
                    writer.WriteLine(input);
                    writer.Flush();
                }
            }
        }
    }
}