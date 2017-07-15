using A.Helper;
using System;

namespace A
{
    class Program
    {

        static void Main(string[] args)
        {
            var options = new Options();
            bool bSuccess = CommandLine.Parser.Default.ParseArguments(args, options);

            if (bSuccess)
            { 
                if(!BindWorkflow(options))
                    Console.WriteLine(options.GetUsage());
            }
            else
                Console.WriteLine(options.GetUsage());
        }
        
        private static bool BindWorkflow(Options op)
        {
            var wflow = new Workflow();
            bool _out = false;

            foreach (var prop in op.GetType().GetProperties())
            {
                var value = Convert.ToBoolean(prop.GetValue(op, null));
                

                if (prop.Name == WorkflowItems.START && value)
                { _out = wflow.StartServer(); break; }
                if (prop.Name == WorkflowItems.STOP && value)
                { _out = wflow.StopServer(); break; }
                if (prop.Name == WorkflowItems.SLOW && value)
                { _out = wflow.SetOnSlowMode(); break; }
                if (prop.Name == WorkflowItems.FAST && value)
                { _out = wflow.SetOnFastMode(); break; }
                if (prop.Name == WorkflowItems.CLIENTS && value)
                { _out = wflow.ShowConnectedClients(); break; }
            }

            return _out;
        }

    }    

}