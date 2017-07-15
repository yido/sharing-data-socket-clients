using A.SocketService;
using System;
using System.Threading;

namespace A
{
    public class Workflow : IWorkflow
    {
        private AServerService _AServerService;

        public Workflow()
        {
            _AServerService = new AServerService();
        }
        public bool StartServer()
        {
            Console.WriteLine("StartServer...");
            ShowProgress();

            _AServerService.Start();
            WaitForUserResponse("Server is listening clients...");
            return true;
        }

        public bool StopServer()
        {
            Console.WriteLine("StopServer...");
            _AServerService.Stop();
            ShowProgress();
            return true;
        }

        public bool SetOnSlowMode()
        {
            Console.WriteLine("Applying slow-mode...");
            _AServerService.IsInSlowMode = true;
            ShowProgress();
            Console.WriteLine("Done!"); 
            return false;
        }

        public bool ShowConnectedClients()
        {
            Console.WriteLine("ShowConnectedClients...");
            ShowProgress();
            WaitForUserResponse();
            return false;
        }

        public bool ShowSharedData()
        {
            Console.WriteLine("ShowSharedData...");
            ShowProgress();
            WaitForUserResponse();
            return false;
        }

        public bool SetOnFastMode()
        {
            Console.WriteLine("Applying fast-mode...");
            _AServerService.IsInSlowMode = false;
            ShowProgress();
            Console.WriteLine("Done!");
            WaitForUserResponse();
            return false;
        }

        private static void ShowProgress()
        {
            for (int i = 0; i < 100; i++)
            {
                ShowPercentProgress("Processing...", i, 100);
                Thread.Sleep(5);
            }
        }
        private static void ShowPercentProgress(string message, int currElementIndex, int totalElementCount)
        {
            if (currElementIndex < 0 || currElementIndex >= totalElementCount)
            {
                throw new InvalidOperationException("currElement out of range");
            }
            int percent = (100 * (currElementIndex + 1)) / totalElementCount;
            Console.Write("\r{0}{1}% complete", message, percent);
            if (currElementIndex == totalElementCount - 1)
            {
                Console.WriteLine(Environment.NewLine);
            }
        }
        private void WaitForUserResponse(string info = "")
        {
            if (info.Length > 0)
                Console.WriteLine(info);

            string result = "";
            do
            {
                result = Console.ReadLine();
            } while (result.ToLower().Trim() != "exit"); 
        }
    }
}
