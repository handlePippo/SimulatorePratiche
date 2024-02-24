using GestionePratiche.Models;

namespace GestionePratiche.Services.PraticheService
{
    namespace SuperHeroAPI.Services.PraticheService
    {
        public interface IPraticheService
        {
            //GET
            Task<List<Pratica>> GetAllExisistingPratiche();
            Task<Pratica?> GetExisistingPratica(int id);
            //POST
            Task<Response> AddNewPratica(PraticaRequest pratica);
            //UPDATE
            Task<List<Pratica>?> UpdateExisistingPratica(int id, Pratica pratica);
            //DELETE
            Task<List<Pratica>?> DeleteExisistingPratica(int id);
        }
    }
}
