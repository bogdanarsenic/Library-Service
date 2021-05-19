using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace K2_Biblioteka.Izuzetak
{
    [DataContract]
    public class ServisBibliotekaIzuzetakDodavanje
    {
        private string razlog;

        [DataMember]
        public string Razlog
        {
            get
            {
                return razlog;
            }

            set
            {
                razlog = value;
            }
        }
    }
}
