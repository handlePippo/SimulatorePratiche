namespace GestionePratiche.Models
{

    public class DatiCreazionePraticaRequest
    {
        public DateTime DataNascita { get; set; }
        public string CodiceFiscale { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public int Telefono { get; set; }
        public StatoCivile StatoCivile { get; set; }
        public int Età { get; set; }
        public FileStream AllegatoPratica { get; set; }
    }
}