namespace GestionePratiche.Models
{
    public class DatiAggiornamentoPraticaRequest
    {
        public StatoCivile? StatoCivile { get; set; }
        public int? Telefono { get; set; }
        public FileStream? AllegatoPratica { get; set; }
    }
}