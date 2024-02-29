using GestionePratiche.Models;
using Microsoft.AspNetCore.Mvc;
using SistemaEsterno.Services.HttpClientService;
using System.Text.Json;

namespace SistemaEsternoController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SistemaEsternoController : ControllerBase
    {
        private readonly ILogger<SistemaEsternoController> _logger;
        private readonly HttpClientService _httpClientService;

        public SistemaEsternoController(ILogger<SistemaEsternoController> logger, HttpClientService httpCs)
        {
            _logger = logger;
            _httpClientService = httpCs;
            _logger.LogInformation("SistemaEsternoController istanziato");
        }

        [HttpGet]
        public async Task<ActionResult<List<PraticaResponse>>> GetAllExistingPratiche()
        {
            try
            {
                var uri = new UriBuilder("http://localhost:5190/api/Pratiche");
                return await _httpClientService.GetFromJsonAsync<List<PraticaResponse>>(uri.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idPratica}")]
        public async Task<ActionResult<PraticaResponseWithStatus>> GetExistingPratica(int idPratica)
        {
            try
            {
                var uri = new UriBuilder("http://localhost:5190/api/Pratiche");
                uri.Path += $"/{idPratica}";
                return await _httpClientService.GetFromJsonAsync<PraticaResponseWithStatus>(uri.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }
    }
}
