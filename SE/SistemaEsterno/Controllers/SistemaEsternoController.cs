﻿using GestionePratiche.Models;
using Microsoft.AspNetCore.Mvc;
using SistemaEsterno.Services;
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

        [HttpPost("Create")]
        public async Task<ActionResult<PraticaResponseWithStatus>> CreatePratica([FromForm] PraticaRequest pratica)
        {
            try
            {
                var uri = new UriBuilder("http://localhost:5190/api/Pratiche/Create");

                using var formContent = new MultipartFormDataContent
                {
                    { new StringContent(pratica.CodiceFiscale), "CodiceFiscale" },
                    { new StringContent(pratica.DataNascita.ToString()), "DataNascita" },
                    { new StringContent(pratica.Nome), "Nome" },
                    { new StringContent(pratica.Cognome), "Cognome" },
                };

                using var stream = pratica.Allegato.OpenReadStream();
                formContent.Add(new StreamContent(stream), "Allegato", pratica.Allegato.FileName);

                using var response = await _httpClientService.PostAsync(uri.ToString(), formContent);

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var content = response.Content.ReadAsStringAsync().Result;
                    var postResponse = JsonSerializer.Deserialize<PraticaResponseWithStatus>(content, options);
                    return postResponse;
                }
                _logger.LogError("Errore sconosciuto. La pratica non è stata creata.");
                return BadRequest("Errore sconosciuto. La pratica non è stata creata.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("Update/{idPratica}")]
        public async Task<ActionResult<PraticaResponseWithStatus>> UpdateExisistingPratica(int idPratica, [FromForm] DatiAggiornamentoPraticaRequest partialPratica)
        {
            try
            {
                var uri = new UriBuilder("http://localhost:5190/api/Pratiche/Update");
                uri.Path += $"/{idPratica}";

                using var formContent = new MultipartFormDataContent
                {
                    { new StringContent(partialPratica.Telefono.ToString() ?? "0"), "Telefono" },
                };

                if (partialPratica.Allegato is not null)
                {
                    var stream = partialPratica.Allegato.OpenReadStream();
                    formContent.Add(new StreamContent(stream), "Allegato", partialPratica.Allegato.FileName);
                }

                using var response = await _httpClientService.PutAsync(uri.ToString(), formContent);

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var content = response.Content.ReadAsStringAsync().Result;
                    var postResponse = JsonSerializer.Deserialize<PraticaResponseWithStatus>(content, options);
                    return postResponse;
                }

                _logger.LogError("Errore sconosciuto. La pratica non è stata aggiornata.");
                return BadRequest("Errore sconosciuto. La pratica non è stata aggiornata.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{idPratica}")]
        public async Task<ActionResult<PraticaResponseWithStatus>> DeleteExistingPratica(int idPratica)
        {
            try
            {
                var uri = new UriBuilder("http://localhost:5190/api/Pratiche/Delete");
                uri.Path += $"/{idPratica}";

                using var response = await _httpClientService.DeleteAsync(uri.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var content = response.Content.ReadAsStringAsync().Result;
                    var postResponse = JsonSerializer.Deserialize<PraticaResponseWithStatus>(content, options);
                    return postResponse;
                }

                _logger.LogError("Errore sconosciuto. La pratica non è stata eliminata.");
                return BadRequest("Errore sconosciuto. La pratica non è stata eliminata.");
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
