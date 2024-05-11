using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FastEventos.Application.Contratos;
using FastEventos.Domain;
using FastEventos.Persistence.Contratos;

namespace FastEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventosPersist _eventosPersist;
        public EventoService(IGeralPersist geralPersist, IEventosPersist eventosPersist)
        {
            this._geralPersist = geralPersist;
            this._eventosPersist = eventosPersist;
        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geralPersist.Add<Evento>(model);
                if(await _geralPersist.SaveChangesAsync())
                {
                    return await _eventosPersist.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception err)
            {
                 throw new Exception(err.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventosPersist.GetEventoByIdAsync(eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                _geralPersist.Update(model);
                  if(await _geralPersist.SaveChangesAsync())
                {
                    return await _eventosPersist.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception err)
            {
                
                throw new Exception(err.Message);
            }
        }
        public async Task<bool> DeleteEvento(int eventoId)
        {
             try
            {
                var evento = await _eventosPersist.GetEventoByIdAsync(eventoId, false);
                if (evento == null) throw new Exception ("Evento para delete não encontrado.");

                _geralPersist.Delete<Evento>(evento);
                return await _geralPersist.SaveChangesAsync();
               
            }
            catch (Exception err)
            {
                
                throw new Exception(err.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventosPersist.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventosPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventosPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

    }
}