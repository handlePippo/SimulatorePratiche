using GestionePratiche.Models;
using Microsoft.AspNetCore.Mvc;
using SistemaEsterno.Services.HttpClientService;
using SistemaEsterno2.Models;

namespace SistemaEsterno2Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackSpController : ControllerBase
    {
        private readonly ILogger<CallbackSpController> _logger;
        private readonly HttpClientService _httpClientService;

        public CallbackSpController(ILogger<CallbackSpController> logger, HttpClientService httpCs)
        {
            _logger = logger;
            _httpClientService = httpCs;
            _logger.LogInformation("CallbackSpController istanziato");
        }



        [HttpPost("CallbackStato")]
        public IActionResult CallbackCambioStato([FromBody] CallbackCambioStatoRequest callbackRequest)
        {

            return Ok();
        }
    }
}
//http://localhost:5013/api/CallbackSp/CallbackStato
