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
        [StringLength(8)]
        public string Telefono { get; set; }
        public string FileName { get; set; }
        public byte[] FileByte { get; set; }
        public Stato Stato { get; set; } = Stato.Creata;
        public DateTime DataCreazione { get; set; } = DateTime.Now;
        public DateTime DataUpdate { get; set; } = DateTime.Now;  
    }
}