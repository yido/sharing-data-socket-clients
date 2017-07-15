using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace A.SocketService
{
    public class Server
    {
        //public bool IsStarted { get; set; }
        //public int Port { get; private set; }
        //public List<Receiver> Receivers { get; private set; }
        //public TcpListener Listener { get; private set; }

        //public Server(int port)
        //{
        //    Receivers = new List<Receiver>();
        //    Port = port;
        //}

        //public void Start()
        //{
        //    if (!IsStarted)
        //    {
        //        Listener = new TcpListener(System.Net.IPAddress.Any, Port);
        //        Listener.Start();
        //        IsStarted = true;

        //        WaitForConnection();

        //    }
        //}

        //public void Stop()
        //{
        //    if (IsStarted)
        //    {
        //        Listener.Stop();
        //        IsStarted = false;

        //    }
        //}

        //private void WaitForConnection()

        //{
        //    Listener.BeginAcceptTcpClient(new AsyncCallback(ConnectionHandler), null);
        //}

        //private void ConnectionHandler(IAsyncResult ar)
        //{
        //    lock (Receivers)
        //    {
        //        Receiver newClient = new Receiver(Listener.EndAcceptTcpClient(ar), this);
        //        newClient.Start();
        //        Receivers.Add(newClient);
        //        OnClientConnected(newClient);
        //    }

        //    WaitForConnection();
        //}
    }
}
