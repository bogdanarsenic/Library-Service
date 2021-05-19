using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_Servis.Korisnik
{
    public class Korisnik
    {
        string _korisnikckoIme;
        string _lozinka;
        bool _autentifikovan = false;
        string _token;

        public Korisnik(string ime, string lozinka)
        {
            KorisnikckoIme = ime;
            Lozinka = lozinka;
        }

        public string KorisnikckoIme
        {
            get
            {
                return _korisnikckoIme;
            }

            set
            {
                _korisnikckoIme = value;
            }
        }

        public string Lozinka
        {
            get
            {
                return _lozinka;
            }

            set
            {
                _lozinka = value;
            }
        }

        public bool Autentifikovan
        {
            get
            {
                return _autentifikovan;
            }

            set
            {
                _autentifikovan = value;
            }
        }

        public string Token
        {
            get
            {
                return _token;
            }

            set
            {
                _token = value;
            }
        }
    }
}
