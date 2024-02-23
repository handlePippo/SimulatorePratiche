using GestionePratiche.Models;
using GestionePratiche.Services.PraticheService.SuperHeroAPI.Services.PraticheService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionePratiche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PraticheController : ControllerBase
    {
        private readonly IPraticheService _praticheService;

        public PraticheController(IPraticheService praticheService)
        {
            _praticheService = praticheService;
        }

        #region BASE CRUD

        [HttpGet]
        //[Authorize] // Aggiungi autenticazione JWT
        public async Task<ActionResult<List<Pratiche>>> GetAllExistingPratiche()
        {
            try
            {
                return await this._praticheService.GetAllExisistingPratiche();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idPratica}")]
        //[Authorize] // Aggiungi autenticazione JWT
        public async Task<ActionResult<Pratiche>> GetExistingPratica(int idPratica)
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
        //[Authorize] // Aggiungi autenticazione JWT
        public async Task<ActionResult<List<Pratiche>>> CreatePratica([FromBody] Pratiche pratica)
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
        //[Authorize] // Aggiungi autenticazione JWT
        public async Task<ActionResult<List<Pratiche>>> UpdateExisistingPratica(int idPratica, [FromBody] Pratiche pratica)
        {
            try
            {
                var result = await this._praticheService.UpdateExisistingPratica(idPratica, pratica);
                if (result is null) return BadRequest("Errore durante l'aggiornamento della pratica!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{idPratica}")]
        //[Authorize] // Aggiungi autenticazione JWT
        public async Task<ActionResult<List<Pratiche>>> DeleteExistingPratica(int idPratica)
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

        [HttpGet("download/{idPratica}")]
        [Authorize] // Aggiungi autenticazione JWT
        public IActionResult DownloadPdfPratica(string idPratica)
        {
            // Logica per il download del PDF allegato di una pratica
            // Verifica l'esistenza di idPratica nel database
            // Restituisci il file PDF
            byte[] fileStream = new byte[4];
            return File(fileStream, "application/pdf", "AllegatoPratica.pdf");
        }


        [HttpPost("AvanzaStato/{idPratica}")]
        [Authorize] // Aggiungi autenticazione JWT
        public IActionResult AvanzaStatoPratica(string idPratica)
        {
            // Logica per l'avanzamento di stato di una pratica
            // Verifica l'esistenza di idPratica nel database
            // Esegui l'avanzamento di stato

            return Ok();
        }

        [HttpPost("CallbackStato")]
        public IActionResult CallbackCambioStato([FromBody] CallbackCambioStatoRequest callbackRequest)
        {
            // Logica per gestire la callback di cambio di stato
            // Emetti eventi o esegui le azioni necessarie

            return Ok();
        }
    }
}
