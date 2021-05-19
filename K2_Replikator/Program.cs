using K2_Biblioteka;
using K2_Biblioteka.Izuzetak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K2_Replikator
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IBezbednosniMehanizmi> factory1 = new ChannelFactory<IBezbednosniMehanizmi>(
                new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:4000/IBezbednosniMehanizmi"));

            IBezbednosniMehanizmi kanal1 = factory1.CreateChannel();

            ChannelFactory<IBezbednosniMehanizmi> factory2 = new ChannelFactory<IBezbednosniMehanizmi>(
                new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:4001/IBezbednosniMehanizmi"));

            IBezbednosniMehanizmi kanal2 = factory2.CreateChannel();

            string token1 = string.Empty;
            string token2 = string.Empty;

            try
            {
                token1 = kanal1.Autentifikacija("nemanja", "jajajaja95");
                token2 = kanal2.Autentifikacija("nemanja", "jajajaja95");

                while (true)
                {
                    try
                    {
                        ChannelFactory<IBiblioteka> cfIzvor = new ChannelFactory<IBiblioteka>("Izvor");
                        ChannelFactory<IBiblioteka> cfOdrediste = new ChannelFactory<IBiblioteka>("Odrediste");
                        IBiblioteka kIzvor = cfIzvor.CreateChannel();
                        IBiblioteka kOdrediste = cfOdrediste.CreateChannel();
                        kOdrediste.PosaljiBazu(kIzvor.PreuzmiBazu(token1), token2);

                        Thread.Sleep(5000);
                    }
                    catch (FaultException<BezbednosniIzuzetak> ex)
                    {
                        Console.WriteLine(ex.Detail.Razlog);
                    }
                }
            }
            catch (FaultException<BezbednosniIzuzetak> ex)
            {
                Console.WriteLine(ex.Detail.Razlog);
            }
        }
    }
}
