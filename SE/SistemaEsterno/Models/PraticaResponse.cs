namespace GestionePratiche.Models
{
    public class PraticaResponse
    {
        public int IdPratica { get; set; }
        public string CodiceFiscale { get; set; }
        public DateTime DataNascita { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public long Telefono { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] FileByte { get; set; }
    }

    public class PraticaResponseWithStatus
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public PraticaResponse? Pratica { get; set; }
    }
}
