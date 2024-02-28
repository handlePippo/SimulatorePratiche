namespace SistemaEsterno.Services.HttpClientService
{
    public interface IHttpClientService
    {
        Task<T> GetFromJsonAsync<T>(string requestUri);
        Task<HttpResponseMessage> PostAsync(string requestUri, MultipartContent body);
        Task<HttpResponseMessage> PutAsync(string requestUri, MultipartContent body);
        Task<HttpResponseMessage> DeleteAsync(string requestUri);
    }
}
