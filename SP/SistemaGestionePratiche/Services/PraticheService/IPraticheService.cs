using GestionePratiche.Models;

namespace GestionePratiche.Services.PraticheService
{
    namespace SuperHeroAPI.Services.PraticheService
    {
        public interface IPraticheService
        {
            //GET
            Task<List<Pratica>> GetAllExisistingPratiche();
            Task<Response> GetExisistingPratica(int id);
            //POST
            Task<Response> AddNewPratica(PraticaRequest pratica);
            //UPDATE
            Task<Response> UpdateExisistingPratica(int id, DatiAggiornamentoPraticaRequest pratica);
            //DELETE
            Task<Response> DeleteExisistingPratica(int id);
        }
    }
}
