using GestionePratiche.Models;

namespace GestionePratiche.Services.PraticheService
{
    namespace SuperHeroAPI.Services.PraticheService
    {
        public interface IPraticheService
        {
            //GET
            Task<List<Pratiche>> GetAllExisistingPratiche();
            Task<Pratiche?> GetExisistingPratica(int id);
            //POST
            Task<List<Pratiche>?> AddNewPratica(Pratiche pratica);
            //UPDATE
            Task<List<Pratiche>?> UpdateExisistingPratica(int id, Pratiche pratica);
            //DELETE
            Task<List<Pratiche>?> DeleteExisistingPratica(int id);
        }
    }
}
