namespace GestionePratiche.Models
{
    public class ApiResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Pratica? Pratica { get; set; } = null;

        public ApiResponse(int code, string message, Pratica? pratica = null)
        {
            this.Code = code;
            this.Message = message;
            this.Pratica = pratica;
        }
    }
}