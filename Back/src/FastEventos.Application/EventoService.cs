using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FastEventos.Application.Dtos;
using FastEventos.Application.Contratos;
using FastEventos.Domain;
using FastEventos.Persistence.Contratos;
using AutoMapper;
using System.Reflection.Metadata.Ecma335;

namespace FastEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventosPersist _eventosPersist;
        private readonly IMapper _mapper;
        public EventoService(IGeralPersist geralPersist, IEventosPersist eventosPersist, IMapper mapper)
        {
            this._mapper = mapper;
            this._geralPersist = geralPersist;
            this._eventosPersist = eventosPersist;
        }
        public async Task<EventoDto> AddEventos(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);
                _geralPersist.Add<Evento>(evento);
                if(await _geralPersist.SaveChangesAsync())
                {
                    var retorno = await _eventosPersist.GetEventoByIdAsync(evento.Id, false);
                    return _mapper.Map<EventoDto>(retorno);
                }
                return null;
            }
            catch (Exception err)
            {
                 throw new Exception(err.Message);
            }
        }

        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _eventosPersist.GetEventoByIdAsync(eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                _mapper.Map(model, evento);
                _geralPersist.Update<Evento>(evento);
                  if(await _geralPersist.SaveChangesAsync())
                {
                    var retorno = await _eventosPersist.GetEventoByIdAsync(evento.Id, false);
                    return _mapper.Map<EventoDto>(retorno);
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

        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventosPersist.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;

                var result = _mapper.Map<EventoDto[]>(eventos);

                return result;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventosPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;

                var result = _mapper.Map<EventoDto[]>(eventos);

                return result;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventosPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (eventos == null) return null;

                var result = _mapper.Map<EventoDto>(eventos);

                return result;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

    }
}