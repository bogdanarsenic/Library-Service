using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace K2_Biblioteka
{
    [DataContract]
    public class Clan
    {
        private string _ime;
        private string _prezime;
        private string _jmbg;
        private List<string> _knjige;

        public Clan(string ime, string prezime, string jmbg)
        {
            Ime = ime;
            Prezime = prezime;
            Jmbg = jmbg;
            Knjige = new List<string>();
        }

        [DataMember]
        public string Ime
        {
            get
            {
                return _ime;
            }

            set
            {
                _ime = value;
            }
        }

        [DataMember]
        public string Prezime
        {
            get
            {
                return _prezime;
            }

            set
            {
                _prezime = value;
            }
        }

        [DataMember]
        public string Jmbg
        {
            get
            {
                return _jmbg;
            }

            set
            {
                _jmbg = value;
            }
        }

        [DataMember]
        public List<string> Knjige
        {
            get
            {
                return _knjige;
            }

            set
            {
                _knjige = value;
            }
        }
    }
}
