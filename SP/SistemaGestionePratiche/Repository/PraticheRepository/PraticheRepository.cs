﻿using GestionePratiche.Models;
using GestionePratiche.Repository;
using GestionePratiche.Services.PraticheService.SuperHeroAPI.Services.PraticheService;
using Microsoft.EntityFrameworkCore;

namespace SistemaGestionePratiche.Repository.PraticheRepository
{
    public class PraticheRepository : IPraticheRepository
    {
        private readonly DataContext _context;

        public PraticheRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Pratica>> GetAllExisistingPratiche() => await _context.ListPratiche.ToListAsync();
        public async Task<ApiResponse> GetExisistingPratica(int id)
        {
            var pratica = await _context.ListPratiche.FindAsync(id);
            if (pratica is not null) return new ApiResponse(200, $"Pratica richiesta - Numero: {id}", pratica);
            return new ApiResponse(500, "La pratica richiesta non esiste!");
        }

        public async Task<ApiResponse> AddNewPratica(PraticaRequest pratica)
        {
            var p = new Pratica
            {
                DataNascita = pratica.DataNascita,
                CodiceFiscale = pratica.CodiceFiscale,
                Cognome = pratica.Cognome,
                Nome = pratica.Nome,
                Telefono = pratica.Telefono,
            };

            if (pratica.Allegato != null && pratica.Allegato.Length > 0)
            {
                using var stream = new MemoryStream();
                pratica.Allegato.CopyTo(stream);
                p.FileByte = stream.ToArray();
                p.FileName = pratica.Allegato.FileName;
            }

            await _context.ListPratiche.AddAsync(p);
            await _context.SaveChangesAsync();

            return new ApiResponse(200, "Pratica aggiunta con successo");
        }

        public async Task<ApiResponse> UpdateExisistingPratica(int id, DatiAggiornamentoPraticaRequest partialPratica)
        {
            var updatedPratica = await _context.ListPratiche.FindAsync(id);
            if (updatedPratica is not null)
            {
                updatedPratica.Telefono = partialPratica.Telefono ?? updatedPratica.Telefono;

                if (partialPratica.Allegato != null && partialPratica.Allegato.Length > 0)
                {
                    using var stream = new MemoryStream();
                    partialPratica.Allegato.CopyTo(stream);
                    updatedPratica.FileByte = stream.ToArray();
                    updatedPratica.FileName = partialPratica.Allegato.FileName;
                }

                await _context.SaveChangesAsync();
                return new ApiResponse(200, "Pratica aggiornata con successo!");
            }
            return new ApiResponse(500, "Errore durante l'aggiornamento della pratica!");
        }

        public async Task<ApiResponse> UpdateStatoPratica(int id, Stato stato)
        {
            var updatedPratica = await _context.ListPratiche.FindAsync(id);
            if (updatedPratica is not null)
            {
                updatedPratica.Stato = stato;
                updatedPratica.DataUpdate = DateTime.Now;

                await _context.SaveChangesAsync();
                return new ApiResponse(200, "Stato della pratica aggiornato con successo!");
            }
            return new ApiResponse(500, "Errore durante l'aggiornamento dello stato della pratica!");
        }

        public async Task<ApiResponse> DeleteExisistingPratica(int id)
        {
            var praticaToDelete = await _context.ListPratiche.FindAsync(id);
            if (praticaToDelete is not null)
            {
                _context.ListPratiche.Remove(praticaToDelete);
                await _context.SaveChangesAsync();
                return new ApiResponse(200, "Pratica eliminata con successo!");
            }
            return new ApiResponse(500, "Errore durante l'eliminazione della pratica!");
        }
    }
}
