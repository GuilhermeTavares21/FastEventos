using AutoMapper;
using FastEventos.Application.Dtos;
using FastEventos.Domain;

namespace FastEventos.API.Helpers
{
    public class FastEventosProfile : Profile
    {
        public FastEventosProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
               
        }
    }
}