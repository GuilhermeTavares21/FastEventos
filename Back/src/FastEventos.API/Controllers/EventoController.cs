using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEventos.API.Data;
using FastEventos.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FastEventos.API.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly DataContext _context;
        public EventoController(DataContext context)
        {
           this._context = context;
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return  _context.Eventos;
        }

        [HttpGet("{id}")]
        public Evento GetById(int id)
        {
            
            return _context.Eventos.FirstOrDefault( item => item.Id == id);
        }
    }
}
