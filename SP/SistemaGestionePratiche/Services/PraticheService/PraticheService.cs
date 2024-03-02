using GestionePratiche.Models;
using GestionePratiche.Services.PraticheService.SuperHeroAPI.Services.PraticheService;

namespace GestionePratiche.Services.PraticheService
{
    public class PraticheService : IPraticheService
    {
        private readonly IPraticheRepository _praticheRepository;

        public PraticheService(IPraticheRepository praticheRepository)
        {
            this._praticheRepository = praticheRepository;
        }

        public Task<List<Pratica>> GetAllExisistingPratiche() => this._praticheRepository.GetAllExisistingPratiche();
        public Task<ApiResponse> GetExisistingPratica(int id) => this._praticheRepository.GetExisistingPratica(id);
        public Task<ApiResponse> AddNewPratica(PraticaRequest pratica) => this._praticheRepository.AddNewPratica(pratica);
        public  Task<ApiResponse> UpdateExisistingPratica(int id, DatiAggiornamentoPraticaRequest partialPratica) => this._praticheRepository.UpdateExisistingPratica(id, partialPratica);
        public Task<ApiResponse> UpdateStatoPratica(int id, Stato stato) => this._praticheRepository.UpdateStatoPratica(id,stato);
        public Task<ApiResponse> DeleteExisistingPratica(int id) => this._praticheRepository.DeleteExisistingPratica(id);
    }
}
