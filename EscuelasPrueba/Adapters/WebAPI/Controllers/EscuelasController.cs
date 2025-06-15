using Microsoft.AspNetCore.Mvc;
using EscuelasPrueba.Core.Application.Interfaces.IServices;
using EscuelasPrueba.Models.DTOs;

namespace EscuelasPrueba.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EscuelasController : ControllerBase
    {
        private readonly IEscuelaService _service;

        public EscuelasController(IEscuelaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.ObtenerTodosAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.ObtenerPorIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EscuelaCreateUpdateDto dto)
        {
            await _service.CrearAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EscuelaCreateUpdateDto dto)
        {
            await _service.ActualizarAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.EliminarAsync(id);
            return NoContent();
        }
    }
}
