using K2_Biblioteka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace K2_Servis
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost svc2 = new ServiceHost(typeof(ServisBiblioteka));

            svc2.AddServiceEndpoint(typeof(IBezbednosniMehanizmi),
                new NetTcpBinding(),
                new Uri(string.Format("net.tcp://localhost:{0}/IBezbednosniMehanizmi", args[0])));

            svc2.Open();

            ServiceHost svc1 = new ServiceHost(typeof(ServisBiblioteka));

            svc1.AddServiceEndpoint(typeof(IBiblioteka),
                new NetTcpBinding(),
                new Uri(string.Format("net.tcp://localhost:{0}/IBiblioteka", args[0])));

            svc1.Open();

            Console.WriteLine("Pritisnite [Enter] za zaustavljanje servisa.");

            Console.ReadLine();
        }
    }
}
