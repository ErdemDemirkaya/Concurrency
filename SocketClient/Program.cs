using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    class Program
    {
        // ExecuteClient() Method 
        static void startClient()
        {
            try
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 11111 on the local computer. 
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);
                // Creation TCP/IP Socket using  
                Socket client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    // Connect Socket to the remote endpoint 
                    client.Connect(localEndPoint);
                    // print connected EndPoint information  
                    Console.WriteLine("Socket connected to -> {0} ", client.RemoteEndPoint.ToString());
                    // Create a to send 
                    byte[] messageSent = Encoding.ASCII.GetBytes("Test Client<EOF>");
                    int byteSent = client.Send(messageSent);

                    // Data buffer 
                    byte[] messageReceived = new byte[1024];
                    // Receive the messagge using the method Receive().
                    int byteRecv = client.Receive(messageReceived);
                    Console.WriteLine("Message from Server -> {0}",Encoding.ASCII.GetString(messageReceived,0, byteRecv));

                    // Close Socket  
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
                catch (Exception e)
                {   Console.WriteLine("Unexpected exception : {0}", e.ToString()); }
            }
            catch (Exception e)
            {   Console.WriteLine(e.ToString()); }
        }
        // Main Method 
        static void Main(string[] args)
        {
            startClient();
        }

    }
}
