using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEventos.Domain;

namespace FastEventos.Persistence.Contratos
{
    public interface IPalestrantePersist
    {
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string Nome, bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesAsync( bool includeEventos);
        Task<Palestrante> GetPalestranteByIdAsync(int PalestranteId, bool includeEventos);
    }
}