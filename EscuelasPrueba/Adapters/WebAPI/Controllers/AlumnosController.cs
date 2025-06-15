using Microsoft.AspNetCore.Mvc;
using EscuelasPrueba.Core.Application.Interfaces.IServices;
using EscuelasPrueba.Models.DTOs;

namespace EscuelasPrueba.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnosController : ControllerBase
    {
        private readonly IAlumnoService _service;

        public AlumnosController(IAlumnoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.ObtenerTodosAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("GetAlumnoEscuela")]
        public async Task<IActionResult> GetAlumnoEscuela()
        {
            var result = await _service.ObtenerAlumnoEscuelaAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("GetProfesorAlumno")]
        public async Task<IActionResult> GetProfesorAlumno()
        {
            var result = await _service.ObtenerProfesorAlumnoAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.ObtenerPorIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }
       

        //[HttpGet("alumno-escuela/{id}")]
        //public async Task<IActionResult> GetAlumnoEscuelaById(int id)
        //{
        //    var result = await _service.ObtenerAlumnoEscuelaPorIdAsync(id);
        //    return result == null ? NotFound() : Ok(result);
        //}

        [HttpGet("alumnoPorProfesor/{id}")]
        public async Task<IActionResult> GetProfesorAlumnoById(int id)
        {
            var result = await _service.ObtenerProfesorAlumnoPorIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create(EstudiantesCreateUpdateDto dto)
        {
            await _service.CrearAsync(dto);
            return Ok();
        }
        [HttpPost("{id}/asignar-escuela/{escuelaId}")]
        public async Task<IActionResult> AsignarEscuela(int id, int escuelaId)
        {
            await _service.InscribirEscuelaAsync(id, escuelaId);
            return Ok($"Alumno {id} inscrito en escuela {escuelaId}");
        }
        [HttpPost("{id}/asignar-profesor/{profesorId}")]
        public async Task<IActionResult> AsignarProfesor(int id, int profesorId)
        {
            await _service.AsignarProfesorAsync(id, profesorId);
            return Ok($"Alumno {id} asignado al profesor {profesorId}");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EstudiantesCreateUpdateDto dto)
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
