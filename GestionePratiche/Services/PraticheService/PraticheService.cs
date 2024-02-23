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

        public async Task<List<Pratiche>> GetAllExisistingPratiche() => await this._context.ListPratiche.ToListAsync();
        public async Task<Pratiche?> GetExisistingPratica(int id)
        {
            var pratica = await this._context.ListPratiche.FindAsync(id);
            if (pratica is null) return null;
            return pratica;
        }
        public async Task<List<Pratiche>?> AddNewPratica(Pratiche pratica)
        {
            if (pratica is null) return null;
            await this._context.ListPratiche.AddAsync(pratica);
            await this._context.SaveChangesAsync();
            return await this._context.ListPratiche.ToListAsync();
        }

        public async Task<List<Pratiche>?> UpdateExisistingPratica(int id, Pratiche pratica)
        {
            var updatedPratica = await this._context.ListPratiche.FindAsync(id);
            if (updatedPratica is null) return null;
            updatedPratica.CodiceFiscale = pratica.CodiceFiscale;
            updatedPratica.Nome = pratica.Nome;
            updatedPratica.Cognome = pratica.Cognome;
            updatedPratica.Eta = pratica.Eta;
            updatedPratica.DataNascita = pratica.DataNascita;
            updatedPratica.StatoCivile = pratica.StatoCivile;
            updatedPratica.Telefono = pratica.Telefono;
            await this._context.SaveChangesAsync();
            return await this._context.ListPratiche.ToListAsync();
        }
        public async Task<List<Pratiche>?> DeleteExisistingPratica(int id)
        {
            var praticaToDelete = await this._context.ListPratiche.FindAsync(id);
            if (praticaToDelete is null) return null;
            this._context.Remove(praticaToDelete);
            await this._context.SaveChangesAsync();
            return await this._context.ListPratiche.ToListAsync();
        }
    }
}
