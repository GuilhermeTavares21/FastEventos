using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEventos.Domain;

namespace FastEventos.Persistence.Contratos
{
    public interface IEventosPersist
    {
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosAsync( bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false);

    }
}