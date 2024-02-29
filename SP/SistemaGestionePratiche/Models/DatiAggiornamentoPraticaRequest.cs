namespace GestionePratiche.Models
{
    public class DatiAggiornamentoPraticaRequest
    {
        public string Telefono { get; set; }
        public IFormFile? Allegato { get; set; }
    }
}