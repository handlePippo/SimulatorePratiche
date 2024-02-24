using System.ComponentModel.DataAnnotations;

namespace GestionePratiche.Models
{
    public class PraticaRequest
    {
        [Required]
        [MinLength(16)]
        public string CodiceFiscale { get; set; }
        [Required]
        public DateTime DataNascita { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cognome { get; set; }
        //public FileStream AllegatoPratica { get; set; }
    }
}
