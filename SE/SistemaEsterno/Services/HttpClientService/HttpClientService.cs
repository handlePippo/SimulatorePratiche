namespace SistemaEsterno.Services.HttpClientService
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpClientService> _logger;

        public HttpClientService(IHttpClientFactory httpClientFactory, ILogger<HttpClientService> logger, IConfiguration config)
        {
            this._configuration = config;
            this._httpClient = httpClientFactory.CreateClient();
            this._httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _configuration.GetSection("Token").Value);
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
    }
}
