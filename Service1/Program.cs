using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------WCF host----------------------");
            using (var s = new ServiceHost(typeof(Service1)))
            {
                s.Open();
                Console.WriteLine("Service is up");
                Console.WriteLine("Press Enter to stop");
                Console.ReadKey();
            }
        }
    }
}
