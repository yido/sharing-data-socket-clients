using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A
{
       public interface IWorkflow
    {
        bool StartServer();
        bool StopServer();
        bool ShowConnectedClients();
        bool ShowSharedData();
        bool SetOnSlowMode();
        bool SetOnFastMode();
    }
}
