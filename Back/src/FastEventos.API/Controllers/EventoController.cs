using FastEventos.Domain;
using Microsoft.AspNetCore.Mvc;
using FastEventos.Application.Contratos;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FastEventos.Application.Dtos;

namespace FastEventos.API.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null) return NotFound("Nenhum evento encontrando");

                return Ok(eventos);
            }
            catch (Exception err)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {err.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
         try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if (evento == null) return NotFound("Nenhum evento encontrando");

                return Ok(evento);
            }
        catch (Exception err)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {err.Message}");
            }  

        }
        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
         try
            {
                var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if (evento == null) return NotFound("Nenhum tema encontrando");

                return Ok(evento);
            }
        catch (Exception err)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {err.Message}");
            }  

        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
        try
            {
                var evento = await _eventoService.AddEventos(model);
                if (evento == null) return BadRequest("Erro ao adicionar evento.");

                return Ok(evento);
            }
        catch (Exception err)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {err.Message}");
            }  

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDto model)
        {
        try
            {
                var evento = await _eventoService.UpdateEvento(id, model);
                if (evento == null) return BadRequest("Erro ao modificar evento.");

                return Ok(evento);
            }
        catch (Exception err)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {err.Message}");
            }   
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
        try
            {
                if(await _eventoService.DeleteEvento(id))
                {
                    return Ok("Deletado.");
                } else 
                {
                    return BadRequest("Evento não deletado");
                }
            }
        catch (Exception err)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {err.Message}");
            }   
        }
    }
}
