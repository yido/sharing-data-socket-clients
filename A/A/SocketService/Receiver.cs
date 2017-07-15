using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace A.SocketService
{
    public class Receiver {


        //public Server Server { get; set; }
        //public TcpClient Client { get; set; }

        //private Thread receivingThread;
        //private Thread sendingThread;

        //public Receiver(TcpClient client, Server server)
        //        : this()
        //{
        //    Server = server;
        //    Client = client;
        //    Client.ReceiveBufferSize = 1024;
        //    Client.SendBufferSize = 1024;
        //}

        //public void Start()
        //{
        //    receivingThread = new Thread(ReceivingMethod);
        //    receivingThread.IsBackground = true;
        //    receivingThread.Start();

        //    sendingThread = new Thread(SendingMethod);
        //    sendingThread.IsBackground = true;
        //    sendingThread.Start();
        //}

        //public void SendMessage(MessageBase message)
        //{
        //    MessageQueue.Add(message);
        //}

        //private void SendingMethod()
        //{
        //    while (Status != StatusEnum.Disconnected)
        //    {
        //        if (MessageQueue.Count > 0)
        //        {
        //            var message = MessageQueue[0];

        //            try
        //            {
        //                BinaryFormatter f = new BinaryFormatter();
        //                f.Serialize(Client.GetStream(), message);
        //            }
        //            catch
        //            {
        //                Disconnect();
        //            }
        //            finally
        //            {
        //                MessageQueue.Remove(message);
        //            }
        //        }
        //        Thread.Sleep(30);
        //    }
        //}

        //private void ReceivingMethod()
        //{
        //    while (Status != StatusEnum.Disconnected)
        //    {
        //        if (Client.Available > 0)
        //        {
        //            TotalBytesUsage += Client.Available;

        //            try
        //            {
        //                BinaryFormatter f = new BinaryFormatter();
        //                MessageBase msg = f.Deserialize(Client.GetStream()) as MessageBase;
        //                OnMessageReceived(msg);
        //            }
        //            catch (Exception e)
        //            {
        //                Exception ex = new Exception("Unknown message recieved. Could not deserialize 



        //                the stream.", e);



        //                Debug.WriteLine(ex.Message);
        //            }
        //        }

        //        Thread.Sleep(30);
        //    }
        //}
    }
}
