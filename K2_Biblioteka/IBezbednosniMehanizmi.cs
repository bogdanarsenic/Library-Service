using K2_Biblioteka.Izuzetak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace K2_Biblioteka
{
    [ServiceContract]
    public interface IBezbednosniMehanizmi
    {
        [OperationContract]
        [FaultContract(typeof(BezbednosniIzuzetak))]
        string Autentifikacija(string korisnik, string lozinka);
    }
}
