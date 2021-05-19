using K2_Biblioteka;
using K2_Biblioteka.Izuzetak;
using K2_Servis.Korisnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace K2_Servis
{
    public class ServisBiblioteka : IBiblioteka, IBezbednosniMehanizmi
    {
        static readonly DirektorijumKorisnika direktorijum = new DirektorijumKorisnika();

        public string Autentifikacija(string korisnik, string lozinka)
        {
            return direktorijum.AutentifikacijaKorisnika(korisnik, lozinka);
        }

        public void DodajClana(Clan clan, string token)
        {
            direktorijum.KorisnikAutentifikovan(token);
            direktorijum.KorisnikAutorizovan(token, DirektorijumKorisnika.EPravaPristupa.Azuriranje);

            if (Baza.Clanovi.ContainsKey(clan.Jmbg))
            {
                ServisBibliotekaIzuzetakDodavanje ex = new ServisBibliotekaIzuzetakDodavanje()
                {
                    Razlog = "Uneti JMBG vec postoji u bazi!"
                };
                throw new FaultException<ServisBibliotekaIzuzetakDodavanje>(ex);
            }
            else
            {
                Baza.Clanovi.Add(clan.Jmbg, clan);

                Console.WriteLine("*********DODAVANJE*********");
                Console.WriteLine("Dodat je novi korisnik:");
                Console.WriteLine("\tIme-> {0}", clan.Ime);
                Console.WriteLine("\tPrezime-> {0}", clan.Prezime);
                Console.WriteLine("\tJmbg-> {0}", clan.Jmbg);
                Console.WriteLine("***************************");
            }
        }

        public void IzbrisiClana(string jmbg, string token)
        {
            direktorijum.KorisnikAutentifikovan(token);
            direktorijum.KorisnikAutorizovan(token, DirektorijumKorisnika.EPravaPristupa.Azuriranje);


            if (!Baza.Clanovi.ContainsKey(jmbg))
            {
                ServisBibliotekaIzuzetakBrisanje ex = new ServisBibliotekaIzuzetakBrisanje()
                {
                    Razlog = "Uneti JMBG ne postoji u bazi!"
                };
                throw new FaultException<ServisBibliotekaIzuzetakBrisanje>(ex);
            }
            else
            {
                Clan clan = Baza.Clanovi[jmbg];

                Baza.Clanovi.Remove(jmbg);

                Console.WriteLine("*********BRISANJE*********");
                Console.WriteLine("Obrisan je korisnik:");
                Console.WriteLine("\tIme-> {0}", clan.Ime);
                Console.WriteLine("\tPrezime-> {0}", clan.Prezime);
                Console.WriteLine("\tJmbg-> {0}", clan.Jmbg);
                Console.WriteLine("**************************");
            }
        }

        public void IzmeniClana(Clan clan, string token)
        {
            direktorijum.KorisnikAutentifikovan(token);
            direktorijum.KorisnikAutorizovan(token, DirektorijumKorisnika.EPravaPristupa.Azuriranje);
            
            if (!Baza.Clanovi.ContainsKey(clan.Jmbg))
            {
                ServisBibliotekaIzuzetakIzmena ex = new ServisBibliotekaIzuzetakIzmena()
                {
                    Razlog = "Uneti JMBG ne postoji u bazi!"
                };
                throw new FaultException<ServisBibliotekaIzuzetakIzmena>(ex);
            }
            else
            {
                Baza.Clanovi[clan.Jmbg] = clan;

                Console.WriteLine("*********IZMENA*********");
                Console.WriteLine("Izmenjen je korisnik:");
                Console.WriteLine("\tIme-> {0}", clan.Ime);
                Console.WriteLine("\tPrezime-> {0}", clan.Prezime);
                Console.WriteLine("\tJmbg-> {0}", clan.Jmbg);
                Console.WriteLine("************************");
            }
        }

        public void PosaljiBazu(Dictionary<string, Clan> baza, string token)
        {
            direktorijum.KorisnikAutentifikovan(token);
            direktorijum.KorisnikAutorizovan(token, DirektorijumKorisnika.EPravaPristupa.Repliciranje);
            
            Baza.Clanovi = baza;

            Console.WriteLine("**********SLANJE BAZE**********");

            int korisnikIndex = 0;

            foreach (Clan clan in Baza.Clanovi.Values)
            {
                Console.WriteLine("{0}. Korisnik:", ++korisnikIndex);
                Console.WriteLine("\tIme-> {0}", clan.Ime);
                Console.WriteLine("\tPrezime-> {0}", clan.Prezime);
                Console.WriteLine("\tJmbg-> {0}", clan.Jmbg);
            }
            Console.WriteLine("*******************************");
        }

        public Dictionary<string, Clan> PreuzmiBazu(string token)
        {
            direktorijum.KorisnikAutentifikovan(token);
            direktorijum.KorisnikAutorizovan(token, DirektorijumKorisnika.EPravaPristupa.Repliciranje);
            

            Console.WriteLine("**********PREUZIMANJE BAZE**********");

            int korisnikIndex = 0;

            foreach (Clan clan in Baza.Clanovi.Values)
            {
                Console.WriteLine("{0}. Korisnik:", ++korisnikIndex);
                Console.WriteLine("\tIme-> {0}", clan.Ime);
                Console.WriteLine("\tPrezime-> {0}", clan.Prezime);
                Console.WriteLine("\tJmbg-> {0}", clan.Jmbg);
            }
            Console.WriteLine("************************************");

            return Baza.Clanovi;
        }
    }
}
