using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace B
{
    class Program
    {
        static void Main(string[] args)
        {
            //~ Note: I am just rushing to show functionalities ~//

            Console.WriteLine("Welcome this is B-instance: \n please select one of the following types to start: " +
                                 "\n \t 1. Red \n \t 2. Green \n \t 3. Blue");

             
            string response;
            do
            {
                Console.Write("-->: ");
                response = Console.ReadLine();
                var value = Convert.ToInt32(response); 
            } while (Convert.ToInt32(response) != 1 && Convert.ToInt32(response) != 2 && Convert.ToInt32(response) != 3);

            var type = Convert.ToInt32(response) == 1 ? "Red": Convert.ToInt32(response) == 2 ? "Green" :"Blue" ;

            BClientService BInstance = new BClientService(type);
            BInstance.LoopConnect(3, 1);
            BInstance.SendDataToServer(type);

            string result = "";
            do
            {
                result = Console.ReadLine();
            } while (result.ToLower().Trim() != "exit");
        }

    }
}
