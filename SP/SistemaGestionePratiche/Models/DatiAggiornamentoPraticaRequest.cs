namespace GestionePratiche.Models
{
    public class DatiAggiornamentoPraticaRequest
    {
        public long? Telefono { get; set; }
        public IFormFile? Allegato { get; set; }
    }
}