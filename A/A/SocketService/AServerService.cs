using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using A.DAL;
using A.Helper;
using System.Threading;

namespace A.SocketService
{
    public class AServerService: IAServerService
    { 
        private string PATH = Path.Combine(Environment.CurrentDirectory) + "\\Input";

        const int PORT_NO = 2201;
        const int MAX_RECEIVE_ATTEMPT = 10;
        const string SERVER_IP = "127.0.0.1";
        private const int BUFFER_SIZE = 4096;

        static int _receiveAttempt = 0;
        private static byte[] buffer = new byte[BUFFER_SIZE];

        private const int FAST_MODE = 10;
        private const int SLOW_MODE = 10;


        private Socket _serverSocket;
        private FileAccessRepository _repository;
        private List<Dictionary<string, IList<Socket>>> _clientsSocket;

        public bool IsInSlowMode { get; set; }
        public bool IsStarted { get; set; }
        public AServerService()
        {
            _repository = new FileAccessRepository(true, PATH);
            _clientsSocket = new List<Dictionary<string, IList<Socket>>>();
        }
        public void Start()
        {
            if (!IsStarted)
            {
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT_NO));
                _serverSocket.Listen(100);
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null); 
                IsStarted = true;

                WaitForConnection();

            }
        }
        public void Stop()
        {
            if (IsStarted)
            {
                _serverSocket.Disconnect(true);
                IsStarted = false;
            }
        }
        public void WaitForConnection()
        {
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }
        public void BeginReceiveHandler(Socket socket)
        {
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
        }        
        public void AcceptCallback(IAsyncResult result)
        { 
            Socket socket = null;
            try
            {
                socket = _serverSocket.EndAccept(result);
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);

                WaitForConnection();
            }
            catch (Exception e)
            {         
                Console.WriteLine(e.ToString());
            }

        }
        public void ReceiveCallback(IAsyncResult result)
        {
            Socket socket = null;
            try
            {
                socket = (Socket)result.AsyncState;
                if (socket.Connected)
                { 
                    int received = socket.EndReceive(result);
                    if (received > 0)
                    {
                        byte[] data = new byte[received];
                        Buffer.BlockCopy(buffer, 0, data, 0, data.Length);
                        var type = Encoding.UTF8.GetString(data);


                        //~ Lets add this socket client with type sent back ~//
                        var clientSocket = new Dictionary<string, IList<Socket>>();
                        var sockets = new List<Socket>();
                        sockets.Add(socket);

                        clientSocket.Add(type, sockets);
                        _clientsSocket.Add(clientSocket);

                        RefreshClientsData();
                        
                        var counts = ClientsByType(type);
                        Console.WriteLine("#"+ counts.Item1 +" Client Connected with type: " + type + " Each contains: "+ counts.Item2);                                            
                        socket.Send(Encoding.ASCII.GetBytes("Hey Client B you are now connected with type: " + type));

                        _receiveAttempt = 0;
                        BeginReceiveHandler(socket);
                    }
                    else if (_receiveAttempt < MAX_RECEIVE_ATTEMPT)
                    {  
                        ++_receiveAttempt;
                        BeginReceiveHandler(socket);
                    }
                    else
                    { 
                        Console.WriteLine("ReceiveCallback fails!");
                        _receiveAttempt = 0;  
                    }
                }
            }
            catch (Exception e)
            { 
                Console.WriteLine("ReceiveCallback fails with exception! " + e.ToString());
            }
        }
        public void RefreshClientsData()
        {
            _repository = new FileAccessRepository(true, PATH);
            foreach (Dictionary<string, IList<Socket>> client in _clientsSocket)
            { 
                int sameClientsCount = client.First().Value.Count;
                var data = _repository.GenerateNSizeRamdomData(client.First().Key, sameClientsCount);
                SendDataToClients(data, client.First().Value);
            }

        }
        public void SendDataToClients(List<List<Record>> data, IList<Socket> clients)
        {
            var _clients = clients.ToArray();
            int index = 0;

            foreach (List<Record> dt in data)
            {
                var client = _clients[index];
                SendDataToClient(client, dt);
                index++;
            } 
        }
        public void SendDataToClient(Socket client, List<Record> data)
        {
            try
            {
                if (client.Connected)
                {

                    Thread.Sleep(IsInSlowMode? SLOW_MODE:FAST_MODE);
                    byte[] serializedData = data.Serialize();
                    client.Send(serializedData, 0, serializedData.Length, 0); 
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("ReceiveCallback fails with exception! " + e.ToString());
            }
        }

        public Tuple<int,int> ClientsByType(string type)
        {
            _repository = new FileAccessRepository(true, PATH);
            var counts = _clientsSocket.Where(x => x.First().Key == type).Count(); 
            var each =  _repository.GenerateNSizeRamdomData(_clientsSocket.Where(x => x.First().Key == type).First().First().Key,counts).First().Count;
            return new Tuple<int, int>(counts,each);
        }
    }
}
