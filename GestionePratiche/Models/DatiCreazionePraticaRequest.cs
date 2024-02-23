namespace GestionePratiche.Models
{

    public class DatiCreazionePraticaRequest 
    {
        public int IdPratica { get; private set; }
        public string CodiceFiscale { get; set; }
        public DateTime DataNascita { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public int Telefono { get; set; }
        public StatoCivile StatoCivile { get; set; }
        public int Eta { get; set; }
        //public FileStream AllegatoPratica { get; set; }
    }
}