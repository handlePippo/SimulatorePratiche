namespace SistemaEsterno.Services.HttpClientService
{
    public interface IHttpClientService
    {
        Task<T> GetFromJsonAsync<T>(string requestUri);
    }
}
