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
        public async Task<Response> GetExisistingPratica(int id)
        {
            var pratica = await this._context.ListPratiche.FindAsync(id);
            if (pratica is not null) return new Response
            {
                Code = 200,
                Message = $"Pratica richiesta - Numero: {id}",
                Pratica = pratica,
            };
            return new Response
            {
                Code = 500,
                Message = "Errore durante l'aggiornamento della pratica",
            };

        }
        public async Task<Response> AddNewPratica(PraticaRequest pratica)
        {
            var p = new Pratica
            {
                DataNascita = pratica.DataNascita,
                CodiceFiscale = pratica.CodiceFiscale,
                Cognome = pratica.Cognome,
                Nome = pratica.Nome,
            };

            if (pratica.Allegato != null && pratica.Allegato.Length > 0)
            {
                using var stream = new MemoryStream();
                pratica.Allegato.CopyTo(stream);
                var fileData = stream.ToArray();
                p.DocByte = fileData;
                p.FileName = pratica.Allegato.FileName;
                p.FileType = pratica.Allegato.ContentType;
            }

            await this._context.ListPratiche.AddAsync(p);
            await this._context.SaveChangesAsync();

            return new Response
            {
                Code = 200,
                Message = "Pratica aggiunta con successo",
            };
        }

        public async Task<Response> UpdateExisistingPratica(int id, DatiAggiornamentoPraticaRequest partialPratica)
        {
            var updatedPratica = await this._context.ListPratiche.FindAsync(id);
            if (updatedPratica is not null)
            {
                updatedPratica.Telefono = partialPratica.Telefono ?? updatedPratica.Telefono;

                if (partialPratica.Allegato != null && partialPratica.Allegato.Length > 0)
                {
                    using var stream = new MemoryStream();
                    partialPratica.Allegato.CopyTo(stream);
                    var fileData = stream.ToArray();
                    updatedPratica.DocByte = fileData;
                    updatedPratica.FileName = partialPratica.Allegato.FileName;
                    updatedPratica.FileType = partialPratica.Allegato.ContentType;
                }

                await this._context.SaveChangesAsync();
                return new Response
                {
                    Code = 200,
                    Message = "Pratica aggiornata con successo!",
                };
            }
            return new Response
            {
                Code = 500,
                Message = "Errore durante l'aggiornamento della pratica!",
            };
        }

        public async Task<Response> DeleteExisistingPratica(int id)
        {
            var praticaToDelete = await this._context.ListPratiche.FindAsync(id);
            if (praticaToDelete is not null)
            {
                this._context.ListPratiche.Remove(praticaToDelete);
                await this._context.SaveChangesAsync();
                return new Response
                {
                    Code = 200,
                    Message = "Pratica eliminata con successo!"
                };
            }
            return new Response
            {
                Code = 500,
                Message = "Errore durante l'eliminazione della pratica!",
            };
        }
    }
}
