using K2_Biblioteka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K2_Servis.Korisnik;

namespace K2_Servis
{
    public static class Baza
    {
        private static Dictionary<string, Clan> _clanovi = new Dictionary<string, Clan>();

        public static Dictionary<string, Clan> Clanovi
        {
            get
            {
                return _clanovi;
            }

            set
            {
                _clanovi = value;
            }
        }
        
    }
}
