using GestionePratiche.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace SistemaGestionePratiche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SistemaEsternoController : ControllerBase
    {
        private readonly ILogger<SistemaEsternoController> _logger;

        public SistemaEsternoController( ILogger<SistemaEsternoController> logger)
        {
            _logger = logger;
            _logger.LogInformation("PraticheController istanziato");
        }

        [HttpGet]
        public async Task<ActionResult<List<PraticaRequest>>> GetAllExistingPratiche()
        {
            try
            {
                using HttpResponseMessage response = await httpClient.GetAsync("todos/3");
                return await this._praticheService.GetAllExisistingPratiche();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idPratica}")]
        public async Task<ActionResult<PraticaRequest>> GetExistingPratica(int idPratica)
        {
            try
            {
                var result = await this._praticheService.GetExisistingPratica(idPratica);
                if (result is null) return NotFound("La pratica richiesta non esiste!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Response>> CreatePratica([FromForm] PraticaRequest pratica)
        {
            try
            {
                var result = await this._praticheService.AddNewPratica(pratica);
                if (result is null) return NotFound("Errore durante la creazione della pratica!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("Update/{idPratica}")]
        public async Task<ActionResult<List<PraticaRequest>>> UpdateExisistingPratica(int idPratica, [FromForm] DatiAggiornamentoPraticaRequest partialPratica)
        {
            try
            {
                var result = await this._praticheService.UpdateExisistingPratica(idPratica, partialPratica);
                if (result is null) return BadRequest("Errore durante l'aggiornamento della pratica!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{idPratica}")]
        public async Task<ActionResult<List<PraticaRequest>>> DeleteExistingPratica(int idPratica)
        {
            try
            {
                var result = await this._praticheService.DeleteExisistingPratica(idPratica);
                if (result is null)
                    return NotFound("Pratica da cancellare non trovata nel database!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
