using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace SocketServer
{
    public class ClientInfo
    {
        public string name { get; set; }
        public string number { get; set; }
        public int groupNumber { get; set; }
    }

    public class SequentialServer
    {
        public Socket listener;
        public IPEndPoint localEndPoint;
        public IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        public readonly int portNumber = 11111;

        public String results = "";
        public LinkedList<ClientInfo> clients = new LinkedList<ClientInfo>();

        public readonly String stopMsg = "<STOP>";
        private Boolean stopCond = false;
        private int processingTime = 1000;
        private int listeningQueueSize = 200;



        public void printClients()
        {
            string delimiter = " , ";
            Console.Out.WriteLine("[Server] This is the list of clients communicated");
            foreach (ClientInfo c in clients)
            {
                Console.WriteLine(c.name + delimiter + c.number + delimiter + c.groupNumber.ToString());
            }
            Console.Out.WriteLine("[Server] Number of handled clients: {0}", clients.Count);

            clients.Clear();
            stopCond = false;

        }
        public void prepareServer()
        {
            try
            {
                Console.WriteLine("[Server] is ready to start ...");
                // Establish the local endpoint
                localEndPoint = new IPEndPoint(ipAddress, portNumber);
                listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                Console.Out.WriteLine("[Server] A socket is established ...");
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
        }
        public void processMessage(String msg)
        {
            try
            {
                Console.WriteLine("[Server] Text received from client -> {0} ", msg);
                Thread.Sleep(processingTime);

                if (msg.Length > this.stopMsg.Length)
                {
                    ClientInfo c = JsonSerializer.Deserialize<ClientInfo>(msg.ToString());
                    clients.AddLast(c);
                }
                if (msg.Equals(stopMsg))
                {
                    stopCond = true;
                    exportResults();

                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("[Server] processMessage {0}", e.Message);
            }
        }
        public void sendReply(Socket connection)
        {
            byte[] msg = Encoding.ASCII.GetBytes("Hello. I am the new Server sending this message.");
            // Send a message to Client  
            connection.Send(msg);
        }
        public void handleClient(Socket connection)
        {
        }
        public void startServer()
        {
            try
            {
                // associate a network address to the Server Socket. All clients must know this address
                listener.Bind(localEndPoint);
                // This is a non-blocking listen with max number of pending requests
                listener.Listen(listeningQueueSize);
                while (true)
                {
                    Console.WriteLine("Waiting connection ... ");
                    // Suspend while waiting for incoming connection Using  
                    Socket connection = listener.Accept();
                    // Data buffer 
                    byte[] bytes = new Byte[1024];
                    String data = null;
                    int numByte = connection.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, numByte);

                    processMessage(data);
                    sendReply(connection);
                    // Close client Socket. After closing, we can use the closed Socket for a new Client Connection 
                    connection.Shutdown(SocketShutdown.Both);
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                listener.Shutdown(SocketShutdown.Both);
                Console.WriteLine("[Server] Listening socket:{0}", e.ToString());
            }
            finally
            {
                listener.Close();
            }
        }
        public void exportResults()
        {
            if (stopCond)
            {
                this.printClients();
            }
        }
    }


    public class ConcurrentServer
    {
        // Todo: Make the class complete
    }

    public class ServerSimulator
    {
        public static void sequentialRun()
        {
            Console.Out.WriteLine("[Server] A sample server, sequential version ...");
            SequentialServer server = new SequentialServer();
            server.prepareServer();
            server.startServer();
        }
        public static void concurrentRun()
        {
            ConcurrentServer server = new ConcurrentServer();
            // Todo: Make the concurrent run complete
            Console.WriteLine("[Server] All the threads are joind");
        }
    }
    class Program
    {
        // Main Method 
        static void Main(string[] args)
        {
            Console.Clear();
            ServerSimulator.sequentialRun();
//            ServerSimulator.concurrentRun();
        }
    }
}
