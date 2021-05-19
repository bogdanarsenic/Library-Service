using K2_Biblioteka.Izuzetak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace K2_Servis.Korisnik
{
    public class DirektorijumKorisnika
    {
        private const string _secuity = "s3cur1t1";
        private static Dictionary<string, Korisnik> _korisnici = new Dictionary<string, Korisnik>();
        private static Dictionary<string, string> _autentifikovaniKorisnici = new Dictionary<string, string>();

        public enum EPravaPristupa { Repliciranje, Azuriranje };
        private Dictionary<string, SortedSet<EPravaPristupa>> prava = new Dictionary<string, SortedSet<EPravaPristupa>>();
        
        public DirektorijumKorisnika()
        {
            DodajKorisnika("nemanja", "jajajaja95");
            DodajKorisnika("admin", "4dmin");

            SortedSet<EPravaPristupa> repliciranje = new SortedSet<EPravaPristupa>();
            repliciranje.Add(EPravaPristupa.Repliciranje);
            prava.Add("nemanja", repliciranje);

            SortedSet<EPravaPristupa> azuriranje = new SortedSet<EPravaPristupa>();
            azuriranje.Add(EPravaPristupa.Azuriranje);
            prava.Add("admin", azuriranje);
        }

        #region Autentifikacija

        private string KodiranTekst(string lozinka)
        {
            using (var sha = SHA256.Create())
            {
                var computedHash = sha.ComputeHash(Encoding.Unicode.GetBytes(lozinka + _secuity));
                return Convert.ToBase64String(computedHash);
            }
        }

        public void DodajKorisnika(string ime, string lozinka)
        {
            _korisnici.Add(ime, new Korisnik(ime, this.KodiranTekst(lozinka)));
        }

        public string AutentifikacijaKorisnika(string ime, string lozinka)
        {
            if (_korisnici.ContainsKey(ime) && this.KodiranTekst(lozinka) == _korisnici[ime].Lozinka)
            {
                _korisnici[ime].Autentifikovan = true;
                string token = this.KodiranTekst(ime + _secuity);
                _autentifikovaniKorisnici.Add(token, ime);
                return token;
            }
            else
            {
                BezbednosniIzuzetak ex = new BezbednosniIzuzetak()
                {
                    Razlog = "Neispravno korisnicko ime i / ili lozinka."
                };

                throw new FaultException<BezbednosniIzuzetak>(ex);
            }
        }

        public bool KorisnikAutentifikovan(string token)
        {
            if (_autentifikovaniKorisnici.ContainsKey(token))
                return true;
            else
            {
                BezbednosniIzuzetak ex = new BezbednosniIzuzetak()
                {
                    Razlog = "Korisnik nije autentifikovan"
                };

                throw new FaultException<BezbednosniIzuzetak>(ex);
            }
        }

        #endregion Autentifikacija

        #region Autorizacija

        public bool KorisnikAutorizovan(string token, EPravaPristupa pravo)
        {
            if (_autentifikovaniKorisnici.ContainsKey(token) && prava.ContainsKey(_autentifikovaniKorisnici[token]) && prava[_autentifikovaniKorisnici[token]].Contains(pravo))
                return true;
            else
            {
                BezbednosniIzuzetak ex = new BezbednosniIzuzetak()
                {
                    Razlog = "Korisnik nema pravo:" + pravo.ToString()
                };

                throw new FaultException<BezbednosniIzuzetak>(ex);
            }
        }

        #endregion Autorizacija
    }
}
