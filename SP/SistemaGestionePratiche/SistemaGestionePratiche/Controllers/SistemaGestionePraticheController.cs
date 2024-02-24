using Microsoft.AspNetCore.Mvc;
using SistemaGestionePratiche.Models;

namespace SistemaGestionePratiche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SistemaGestionePratiche : ControllerBase
    {
        [HttpPost("CallbackStato")]
        public IActionResult CambioStatoPratica([FromBody] CallbackCambioStatoRequest callbackRequest)
        {
            // Logica per gestire la callback di cambio di stato
            // Emetti eventi o esegui le azioni necessarie

            return Ok();
        }

        //[HttpPost("CallbackStato")]
        //public IActionResult CallbackCambioStato([FromBody] CallbackCambioStatoRequest callbackRequest)
        //{
        //    // Logica per gestire la callback di cambio di stato
        //    // Emetti eventi o esegui le azioni necessarie

        //    return Ok();
        //}
    }
}
