using GestionePratiche.Models;

namespace GestionePratiche.Services.PraticheService
{
    namespace SuperHeroAPI.Services.PraticheService
    {
        public interface IPraticheRepository
        {
            //GET
            Task<List<Pratica>> GetAllExisistingPratiche();
            Task<ApiResponse> GetExisistingPratica(int id);
            //POST
            Task<ApiResponse> AddNewPratica(PraticaRequest pratica);
            //UPDATE
            Task<ApiResponse> UpdateExisistingPratica(int id, DatiAggiornamentoPraticaRequest pratica);
            //UPDATE STATO
            Task<ApiResponse> UpdateStatoPratica(int id, Stato stato);
            //DELETE
            Task<ApiResponse> DeleteExisistingPratica(int id);
        }
    }
}
