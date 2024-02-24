using GestionePratiche.Models;
using GestionePratiche.Repository;
using GestionePratiche.Services.PraticheService.SuperHeroAPI.Services.PraticheService;
using Microsoft.EntityFrameworkCore;

namespace GestionePratiche.Services.PraticheService
{
    public class PraticheService : IPraticheService
    {
        private readonly DataContext _context;
        public PraticheService(DataContext context)
        {
            this._context = context;
        }

        public async Task<List<Pratica>> GetAllExisistingPratiche() => await this._context.ListPratiche.ToListAsync();
        public async Task<Pratica?> GetExisistingPratica(int id)
        {
            var pratica = await this._context.ListPratiche.FindAsync(id);
            if (pratica is null) return null;
            return pratica;
        }
        public async Task<Response> AddNewPratica(PraticaRequest pratica)
        {
            var p = new Pratica
            {
                DataNascita = pratica.DataNascita,
                CodiceFiscale = pratica.CodiceFiscale,
                Cognome = pratica.Cognome,
                Nome = pratica.Nome
            };

            await this._context.ListPratiche.AddAsync(p);
            await this._context.SaveChangesAsync();
            var response = new Response
            {
                code = 200,
                message = $"Pratica aggiunta con successo"
            };
            return response;
        }

        public async Task<List<Pratica>?> UpdateExisistingPratica(int id, Pratica pratica)
        {
            var updatedPratica = await this._context.ListPratiche.FindAsync(id);
            if (updatedPratica is null) return null;
            updatedPratica.CodiceFiscale = pratica.CodiceFiscale;
            updatedPratica.Nome = pratica.Nome;
            updatedPratica.Cognome = pratica.Cognome;
            updatedPratica.DataNascita = pratica.DataNascita;
            await this._context.SaveChangesAsync();
            return await this._context.ListPratiche.ToListAsync();
        }
        public async Task<List<Pratica>?> DeleteExisistingPratica(int id)
        {
            var praticaToDelete = await this._context.ListPratiche.FindAsync(id);
            if (praticaToDelete is null) return null;
            this._context.Remove(praticaToDelete);
            await this._context.SaveChangesAsync();
            return await this._context.ListPratiche.ToListAsync();
        }
    }
}
