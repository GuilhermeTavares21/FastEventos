using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEventos.Domain;
using FastEventos.Persistence.Contextos;
using FastEventos.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace FastEventos.Persistence
{
    public class EventosPersist : IEventosPersist
    {
        private readonly FastEventosContext _context;
        public EventosPersist (FastEventosContext context)
        {
            this._context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes)
                                                        .Include(e => e.RedesSociais);
            
            if(includePalestrantes)
            {
                query = query.Include(e => e.PalestranteEventos)
                .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes)
                                                        .Include(e => e.RedesSociais);
            
            if(includePalestrantes)
            {
                query = query.Include(e => e.PalestranteEventos)
                .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes)
                                                        .Include(e => e.RedesSociais);
            
            if(includePalestrantes)
            {
                query = query.Include(e => e.PalestranteEventos)
                .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id).Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }
        
    }
}