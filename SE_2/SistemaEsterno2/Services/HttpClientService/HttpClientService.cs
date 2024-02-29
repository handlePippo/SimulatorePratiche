using System;

namespace SistemaEsterno.Services.HttpClientService
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpClientService> _logger;
        private readonly IConfiguration _configuration;

        public HttpClientService(IHttpClientFactory httpClientFactory, ILogger<HttpClientService> logger, IConfiguration config)
        {
            _configuration = config;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _configuration.GetSection("Token").Value);
            _logger = logger;
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, MultipartContent body)
        {
            var response = await _httpClient.PostAsync(requestUri, body);
            if (response is null || !response.IsSuccessStatusCode)
            {
                _logger.LogError(requestUri);
                _logger.LogError("Errore, la response è null oppure non sei autenticato");
                throw new EmptyResultException("Errore, la response è null");
            }
            _logger.LogError("Successo: la response è valida");
            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(string requestUri, MultipartContent body)
        {
            var response = await _httpClient.PutAsync(requestUri, body);
            if (response is null || !response.IsSuccessStatusCode)
            {
                _logger.LogError(requestUri);
                _logger.LogError("Errore, la response è null oppure non sei autenticato");
                throw new EmptyResultException("Errore, la response è null");
            }
            _logger.LogError("Successo: la response è valida");
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            var response = await _httpClient.DeleteAsync(requestUri);
            if (response is null || !response.IsSuccessStatusCode)
            {
                _logger.LogError(requestUri);
                _logger.LogError("Errore, la response è null oppure non sei autenticato");
                throw new EmptyResultException("Errore, la response è null");
            }
            _logger.LogError("Successo: la response è valida");
            return response;
        }
    }
}
