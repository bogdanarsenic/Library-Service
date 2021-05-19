using K2_Biblioteka;
using K2_Biblioteka.Izuzetak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace K2_Klijent
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IBiblioteka> factory1 = new ChannelFactory<IBiblioteka>(
                new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:4000/IBiblioteka"));

            IBiblioteka kanal = factory1.CreateChannel();

            ChannelFactory<IBezbednosniMehanizmi> factory2 = new ChannelFactory<IBezbednosniMehanizmi>(
                new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:4000/IBezbednosniMehanizmi"));

            IBezbednosniMehanizmi kanal2 = factory2.CreateChannel();

            string token = string.Empty;

            try
            {
                token = kanal2.Autentifikacija("admin", "4dmin");

            }
            catch (FaultException<BezbednosniIzuzetak> ex)
            {
                Console.WriteLine(ex.Detail.Razlog);
            }

            int broj;
            while (true)
            {
                broj = 0;

                Console.WriteLine("**********MENI*********");
                Console.WriteLine("\t1. Dodaj clana");
                Console.WriteLine("\t2. Izmeni clana");
                Console.WriteLine("\t3. Izbrisi clana");
                Console.WriteLine("\t4. Posalji bazu");
                Console.WriteLine("\t5. Preuzmi bazu");
                Console.WriteLine("\t6. IZLAZ");
                Console.WriteLine("***********************");

                try
                {
                    broj = Convert.ToInt32(Console.ReadLine());

                }
                catch
                {
                    continue;
                }
                switch (broj)
                {

                    case 1:
                        try
                        {
                            Console.WriteLine("**********DODAJ CLANA**********");
                            Console.WriteLine("\tIme:");
                            string imeDodaj = Console.ReadLine();

                            Console.WriteLine("\tPrezime:");
                            string prezimeDodaj = Console.ReadLine();

                            Console.WriteLine("\tJmbg:");
                            string jmbgDodaj = Console.ReadLine();

                            Console.WriteLine("*******************************");

                            try
                            {
                                Clan clan = new Clan(imeDodaj, prezimeDodaj, jmbgDodaj);

                                kanal.DodajClana(clan, token);
                            }
                            catch (FaultException<ServisBibliotekaIzuzetakDodavanje> ex)
                            {
                                Console.WriteLine(ex.Detail.Razlog);
                            }
                        }
                        catch (FaultException<BezbednosniIzuzetak> ex)
                        {
                            Console.WriteLine(ex.Detail.Razlog);
                        }

                        break;
                    case 2:

                        try
                        {
                            Console.WriteLine("**********IZMENI CLANA**********");

                            try
                            {
                                Console.WriteLine("\tIme:");
                                string imeIzmeni = Console.ReadLine();

                                Console.WriteLine("\tPrezime:");
                                string prezimeIzmeni = Console.ReadLine();

                                Console.WriteLine("Unesite JMBG clana kog zelite da izmenite:");
                                string jmbgIzmeni = Console.ReadLine();

                                Clan clan = new Clan(imeIzmeni, prezimeIzmeni, jmbgIzmeni);

                                kanal.IzmeniClana(clan, token);
                            }
                            catch (FaultException<ServisBibliotekaIzuzetakIzmena> ex)
                            {
                                Console.WriteLine(ex.Detail.Razlog);
                            }
                            Console.WriteLine("*******************************");
                        }
                        catch (FaultException<BezbednosniIzuzetak> ex)
                        {
                            Console.WriteLine(ex.Detail.Razlog);
                        }
                        break;
                    case 3:

                        try
                        {
                            Console.WriteLine("**********IZBRISI CLANA**********");

                            try
                            {
                                Console.WriteLine("Unesite JMBG clana kog zelite da izbrisete:");
                                string jmbgObrisi = Console.ReadLine();

                                kanal.IzbrisiClana(jmbgObrisi, token);
                            }
                            catch (FaultException<ServisBibliotekaIzuzetakBrisanje> ex)
                            {
                                Console.WriteLine(ex.Detail.Razlog);
                            }
                            Console.WriteLine("*******************************");
                        }
                        catch (FaultException<BezbednosniIzuzetak> ex)
                        {
                            Console.WriteLine(ex.Detail.Razlog);
                        }
                        break;
                    case 6:
                        return;
                    default:
                        continue;
                }
            }
        }
    }
}
