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
    public interface IBiblioteka
    {
        [OperationContract]
        [FaultContract(typeof(ServisBibliotekaIzuzetakDodavanje))]
        [FaultContract(typeof(BezbednosniIzuzetak))]
        void DodajClana(Clan clan, string token);

        [OperationContract]
        [FaultContract(typeof(ServisBibliotekaIzuzetakIzmena))]
        [FaultContract(typeof(BezbednosniIzuzetak))]
        void IzmeniClana(Clan clan, string token);

        [OperationContract]
        [FaultContract(typeof(ServisBibliotekaIzuzetakBrisanje))]
        [FaultContract(typeof(BezbednosniIzuzetak))]
        void IzbrisiClana(string jmbg, string token);

        [OperationContract]
        [FaultContract(typeof(BezbednosniIzuzetak))]
        void PosaljiBazu(Dictionary<string, Clan> baza, string token);

        [OperationContract]
        [FaultContract(typeof(BezbednosniIzuzetak))]
        Dictionary<string, Clan> PreuzmiBazu(string token);
    }
}
