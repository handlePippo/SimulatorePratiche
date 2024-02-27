namespace GestionePratiche.Models
{
    public class Response
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Pratica? Pratica { get; set; }
    }
}
