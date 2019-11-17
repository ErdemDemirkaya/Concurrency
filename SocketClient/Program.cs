using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace SocketClient
{
    public class ClientInfo
    {
        public string name { get; set; }
        public string number { get; set; }
        public int groupNumber { get; set; }
    }

    public class Client
    {
        public Socket clientSocket;
        public IPEndPoint localEndPoint;
        public IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        public readonly int portNumber = 11111;
        public readonly int minWaitingTime = 100, maxWaitingTime = 1000;
        public int waitingTime = 0; 
        public readonly String stopMsg = "<STOP>";
        private String msgToSend;

        public Thread workerThread;

        public Client(bool finishing, int n)
        {
            waitingTime = new Random().Next(minWaitingTime, maxWaitingTime);
            if (!finishing)
            {
                ClientInfo info = new ClientInfo();
                info.name = " Client " + n.ToString();
                info.number = " 0723098 ";
                info.groupNumber = 2;

                msgToSend = JsonSerializer.Serialize<ClientInfo>(info);
            }
            else
            {
                msgToSend = stopMsg;
            }
        }
        public void prepareClient()
        {
            try
            {
                // Establish the remote endpoint for the socket.
                localEndPoint = new IPEndPoint(ipAddress, portNumber);
                // Creation TCP/IP Socket using  
                clientSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            }
            catch(Exception e)
            {
                Console.Out.WriteLine("[Client] Preparation failed: {0}",e.Message);
            }
        }

        public void startCommunication()
        {
            Thread.Sleep(waitingTime);

            try
            {
                // Connect Socket to the remote endpoint 
                clientSocket.Connect(localEndPoint);
                // print connected EndPoint information  
                Console.WriteLine("[Client] Socket connected to -> {0} ", clientSocket.RemoteEndPoint.ToString());
                // Create the message to send
                string msg = msgToSend.ToString();
                Console.Out.WriteLine("[Client] Message to be sent: {0}", msg);
                byte[] messageSent = Encoding.ASCII.GetBytes(msg.ToString());
                int byteSent = clientSocket.Send(messageSent);

                // Data buffer 
                byte[] messageReceived = new byte[1024];
                // Receive the messagge using the method Receive().
                int byteRecv = clientSocket.Receive(messageReceived);
                String rcvdMsg = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
                Console.WriteLine("[Client] Message from Server -> {0}", rcvdMsg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
         }
        public void endCommunication()
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }

        public void run()
        {
            workerThread = new Thread(()=>startCommunication());
        }

    }

    public class ClientsSimulator
    {
        private int numberOfClients, time;
        private Client[] clients;

        public ClientsSimulator(int n, int t)
        {
            numberOfClients = n;
            time = t;
        }

        public void SequentialSimulation()
        {
            Console.Out.WriteLine("[ClientSimulator] Sequential simulator is going to start ...");
            clients = new Client[numberOfClients];

            for (int i = 0; i < numberOfClients; i++)
            {
                clients[i] = new Client(false,i);
                clients[i].prepareClient();
                clients[i].startCommunication();
                clients[i].endCommunication();
            }
            Console.Out.WriteLine("[Client] All clients started with their communications ....waiting to finish");

            Client endClient = new Client(true,-1);
            endClient.prepareClient();
            endClient.startCommunication();
            endClient.endCommunication();
        }

        public void ConcurrentSimulation()
        {
            Console.Out.WriteLine("[ClientSimulator] Sequential simulator is going to start ...");
            clients = new Client[numberOfClients];

            try
            {
                for (int i = 0; i < numberOfClients; i++)
                {
                    clients[i] = new Client(false,i);
                    clients[i].prepareClient();
                    clients[i].run();
                }
                for (int i = 0; i < numberOfClients; i++)
                {
                    clients[i].workerThread.Start();     
                }

                Thread.Sleep(time);

                for (int i = 0; i < numberOfClients; i++)
                {
                    clients[i].endCommunication();
                    clients[i].workerThread.Join();
                }

                Client endClient = new Client(true,-1);
                endClient.prepareClient();
                endClient.run();
                endClient.workerThread.Start();
                Thread.Sleep(time);
                endClient.endCommunication();
                endClient.workerThread.Join();

            }
            catch(Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }

        }

    }
    class Program
    { 
        // Main Method 
        static void Main(string[] args)
        {
            Console.Clear();
            int wt = 5000 , nc = 150;
            ClientsSimulator clientsSimulator = new ClientsSimulator(nc,wt);
            //clientsSimulator.SequentialSimulation();
            Thread.Sleep(wt);
            clientsSimulator.ConcurrentSimulation();

        }

    }
}
