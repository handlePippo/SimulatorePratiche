using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionePratiche.Models
{
    public class Pratica
    {
        [Key]
        public int IdPratica { get; private set; }
        [Required]
        [StringLength(16)]
        public string CodiceFiscale { get; set; }
        [Required]
        public DateTime DataNascita { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cognome { get; set; }
        [Required]
        public long Telefono { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] DocByte { get; set; }
    }
}