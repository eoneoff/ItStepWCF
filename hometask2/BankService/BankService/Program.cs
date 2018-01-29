using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace BankService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Bank Server";
            ServiceHost sh = new ServiceHost(typeof(Service));
            try
            {
                sh.Open();
                Console.WriteLine("Bank Server Online...");
                Console.WriteLine("Press <ENTER> to shutdown");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bank Server down with error {ex.Message} at {DateTime.Now}");
            }
            finally
            {
                sh.Close();
                Console.WriteLine("Bank Server down correctly");
            }
        }
    }
}
