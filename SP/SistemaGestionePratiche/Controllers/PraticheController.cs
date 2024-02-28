using GestionePratiche.Models;
using GestionePratiche.Services.PraticheService.SuperHeroAPI.Services.PraticheService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionePratiche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PraticheController : ControllerBase
    {
        private readonly ILogger<PraticheController> _logger;
        private readonly IPraticheService _praticheService;

        public PraticheController(IPraticheService praticheService, ILogger<PraticheController> logger)
        {
            _logger = logger;
            _praticheService = praticheService;
            _logger.LogInformation("PraticheController istanziato");
        }

        #region BASE CRUD

        [HttpGet]
        public async Task<ActionResult<List<Pratica>>> GetAllExistingPratiche()
        {
            try
            {
                return await this._praticheService.GetAllExisistingPratiche();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idPratica}")]
        public async Task<ActionResult<Pratica>> GetExistingPratica(int idPratica)
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
        public async Task<ActionResult<ApiResponse>> CreatePratica([FromForm] PraticaRequest pratica)
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
        public async Task<ActionResult<List<Pratica>>> UpdateExisistingPratica(int idPratica, [FromForm] DatiAggiornamentoPraticaRequest partialPratica)
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
        public async Task<ActionResult<List<Pratica>>> DeleteExistingPratica(int idPratica)
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
        #endregion

        //[HttpPost("AvanzaStato/{idPratica}")]
        //public IActionResult AvanzaStatoPratica(string idPratica)
        //{
        //    // Logica per l'avanzamento di stato di una pratica
        //    // Verifica l'esistenza di idPratica nel database
        //    // Esegui l'avanzamento di stato

        //    return Ok();
        //}

        //[HttpPost("CallbackStato")]
        //public IActionResult CallbackCambioStato([FromBody] CallbackCambioStatoRequest callbackRequest)
        //{
        //    // Logica per gestire la callback di cambio di stato
        //    // Emetti eventi o esegui le azioni necessarie

        //    return Ok();
        //}
    }
}
