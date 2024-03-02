using GestionePratiche.Models;
using GestionePratiche.Services.PraticheService.SuperHeroAPI.Services.PraticheService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GestionePratiche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PraticheController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PraticheController> _logger;
        private readonly IPraticheService _praticheService;

        public PraticheController(IPraticheService praticheService, ILogger<PraticheController> logger, IConfiguration config)
        {
            _logger = logger;
            _praticheService = praticheService;
            _configuration = config;
            _connectionString = _configuration.GetSection("ConnectionString").Value;
            _logger.LogInformation("PraticheController istanziato");
            Initialization();
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
                _logger.LogError(ex.Message, ex);
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
                _logger.LogError(ex.Message, ex);
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
                _logger.LogError(ex.Message, ex);
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
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
        #endregion

        [HttpPost("AvanzaStato/{idPratica}")]
        public async Task<ActionResult> AvanzaStatoPratica(int idPratica, Stato stato)
        {
            try
            {
                var result = await this._praticheService.UpdateStatoPratica(idPratica, stato);
                if (result is null) return BadRequest("Errore durante l'aggiornamento della pratica!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        #region Callback
        private void Initialization()
        {
            // Crea una dependency connection.
            SqlDependency.Start(_connectionString);
            GetDataWithSqlDependency();
        }

        private void Termination()
        {
            // Rilascia la dipendenza.
            SqlDependency.Stop(_connectionString);
        }

        private DataTable GetDataWithSqlDependency()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT Stato FROM dbo.ListPratiche;", connection);
            var dt = new DataTable();

            // Crea una dipendenza per questo cmd ed aggiunge un event handler
            var dependency = new SqlDependency(cmd);
            dependency.OnChange += new OnChangeEventHandler(OnDependencyChange);

            // Esegue il comando per prendere i dati.
            connection.Open();
            dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));

            return dt;
        }

        // Handler method
        private void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            Console.WriteLine($"OnChange Event fired. SqlNotificationEventArgs: Info={e.Info}, Source={e.Source}, Type={e.Type}.");

            if ((e.Info != SqlNotificationInfo.Invalid) && (e.Type != SqlNotificationType.Subscribe))
            {
                Console.WriteLine("Notification Info: " + e.Info);
                Console.WriteLine("Notification source: " + e.Source);
                Console.WriteLine("Notification type: " + e.Type);

                // resubscribe
                var dt = GetDataWithSqlDependency();

                Console.WriteLine($"Data changed. {dt.Rows.Count} rows returned.");
                // TODO: Fare un httpClient verso il Sistema Esterno. 
                // Prendere la lista delle pratiche e prendere il dato piu aggiornato (order by desc di data update) (prendere solo il primo, fare top 1)

            }
            else
            {
                Console.WriteLine("SqlDependency not restarted");
            }
        }

        //[HttpPost]
        //public async Task<ActionResult> CreatePratica([FromForm] PraticaRequest pratica)
        //{
        //    try
        //    {

        //        var res = await this._praticheService.GetAllExisistingPratiche();
        //        res.Select(p => p.DataUpdate).OrderBy(p => p);

        //        var uri = new UriBuilder("http://localhost:5190/api/Pratiche/Create");

        //        var httpClient = new HttpClient();

        //        using var response = await httpClient.PostAsync(uri.ToString(), );

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var options = new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            };
        //            var content = response.Content.ReadAsStringAsync().Result;
        //            var postResponse = JsonSerializer.Deserialize<PraticaResponseWithStatus>(content, options);
        //            return postResponse;
        //        }
        //        _logger.LogError("Errore sconosciuto. La pratica non è stata creata.");
        //        return BadRequest("Errore sconosciuto. La pratica non è stata creata.");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        _logger.LogError(ex.StackTrace);
        //        return BadRequest(ex.Message);
        //    }
        //}
        #endregion
    }
}
