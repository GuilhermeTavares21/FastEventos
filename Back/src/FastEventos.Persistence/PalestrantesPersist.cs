using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEventos.Domain;
using FastEventos.Persistence.Contratos;
using FastEventos.Persistence.Contextos;
using Microsoft.EntityFrameworkCore;

namespace FastEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly FastEventosContext _context;
        public PalestrantePersist (FastEventosContext context)
        {
            this._context = context;
             _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);
            
            if(includeEventos)
            {
                query = query
                .Include(p => p.PalestranteEventos)
                .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);
            
            if(includeEventos)
            {
                query = query
                .Include(p => p.PalestranteEventos)
                .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id)
                        .Where( p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }


        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
                        IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);
            
            if(includeEventos)
            {
                query = query
                .Include(p => p.PalestranteEventos)
                .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id)
                        .Where( p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }

    }
}