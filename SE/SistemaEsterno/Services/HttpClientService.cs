namespace SistemaEsterno.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpClientService> _logger;

        public HttpClientService(IHttpClientFactory httpClientFactory, ILogger<HttpClientService> logger)
        {
            this._httpClient = httpClientFactory.CreateClient();
            this._logger = logger;
        }

        public async Task<T> GetFromJsonAsync<T>(string requestUri)
        {
            var response = await _httpClient.GetFromJsonAsync<T>(requestUri);
            if (response is null)
            {
                _logger.LogError(requestUri);
                _logger.LogError("Errore, la response è null");
                throw new EmptyResultException("Errore, la response è null");
            }
            _logger.LogError("Successo: la response è valida");
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, MultipartContent body)
        {
            var response = await _httpClient.PostAsync(requestUri, body);
            if (response is null)
            {
                _logger.LogError(requestUri);
                _logger.LogError("Errore, la response è null");
                throw new EmptyResultException("Errore, la response è null");
            }
            _logger.LogError("Successo: la response è valida");
            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(string requestUri, MultipartContent body)
        {
            var response = await _httpClient.PutAsync(requestUri, body);
            if (response is null)
            {
                _logger.LogError(requestUri);
                _logger.LogError("Errore, la response è null");
                throw new EmptyResultException("Errore, la response è null");
            }
            _logger.LogError("Successo: la response è valida");
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            var response = await _httpClient.DeleteAsync(requestUri);
            if (response is null)
            {
                _logger.LogError(requestUri);
                _logger.LogError("Errore, la response è null");
                throw new EmptyResultException("Errore, la response è null");
            }
            _logger.LogError("Successo: la response è valida");
            return response;
        }
    }
}
