using B.Dto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace B
{
    public class BClientService
    {
        const int PORT_NO = 2201;
        const string SERVER_IP = "127.0.0.1";

        const int MAX_RECEIVE_ATTEMPT = 10;      
        static int ReceiveAttempt = 0;

        private const int BUFFER_SIZE = 4096;
        private static byte[] Buffer = new byte[BUFFER_SIZE];

        public string Type { get; set; }
        public static Socket ClientSocket;
        private List<Record> _records;

        public BClientService(string type)
        {
            Type = type;
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void LoopConnect(int noOfRetry, int attemptPeriodInSeconds)
        {
            int attempts = 0;
            while (!ClientSocket.Connected && attempts < noOfRetry)
            {
                try
                {
                    ++attempts;
                    IAsyncResult result = ClientSocket.BeginConnect(IPAddress.Parse(SERVER_IP), PORT_NO, EndConnectCallback,null);
                    result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(attemptPeriodInSeconds));
                    System.Threading.Thread.Sleep(attemptPeriodInSeconds * 1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.ToString());
                }
            }
            if (!ClientSocket.Connected)
            {
                Console.WriteLine("Connection attempt is unsuccessful!");
                return;
            }
        }
        public  void EndConnectCallback(IAsyncResult ar)
        {
            try
            {
                ClientSocket.EndConnect(ar);
                if (ClientSocket.Connected)
                {
                    ClientSocket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), ClientSocket);
                }
                else
                {
                    Console.WriteLine("End of connection attempt, fail to connect...");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("End-connection attempt is unsuccessful! " + e.ToString());
            }
        }
        public void BeginReceiveHandler(Socket socket)
        {
            socket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
        }
        public void ReceiveCallback(IAsyncResult result)
        {
            System.Net.Sockets.Socket socket = null;
            try
            {
                socket = (System.Net.Sockets.Socket)result.AsyncState;
                if (socket.Connected)
                {
                    int received = socket.EndReceive(result);
                    if (received > 0)
                    {
                        ReceiveAttempt = 0;
                        byte[] data = new byte[received];
                        System.Buffer.BlockCopy(Buffer, 0, data, 0, data.Length);

                        Console.WriteLine("Hi client you have received Items.");
                        Console.WriteLine(Encoding.UTF8.GetString(data));

                        //var record  = Record.Desserialize(data);
                        //Console.WriteLine("Recored shared: " + record);
                        BeginReceiveHandler(socket);

                    }
                    else if (ReceiveAttempt < MAX_RECEIVE_ATTEMPT)
                    { 
                        ++ReceiveAttempt;
                        BeginReceiveHandler(socket);
                    }
                    else
                    { 
                        Console.WriteLine("receiveCallback is failed!");
                        ReceiveAttempt = 0;
                        ClientSocket.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("receiveCallback is failed! " + e.ToString());
            }
        }
        public void SendDataToServer(string result)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(result);
            ClientSocket.Send(bytes);
        }

    }
}
