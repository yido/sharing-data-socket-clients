using System;
using System.Collections.Generic;
using System.Net.Sockets;
using A.DAL;
namespace A.SocketService
{
    public interface IAServerService
    {
        void Start();
        void Stop();
        void WaitForConnection();
        void BeginReceiveHandler(Socket socket);
        void AcceptCallback(IAsyncResult result);
        void ReceiveCallback(IAsyncResult result);
        void RefreshClientsData();
        void SendDataToClients(List<List<Record>> data, IList<Socket> clients);
        void SendDataToClient(Socket client, List<Record> data);
    }
}
