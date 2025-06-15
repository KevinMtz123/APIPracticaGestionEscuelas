using EscuelasPrueba.Models.DTOs;

namespace EscuelasPrueba.Core.Application.Interfaces.IServices
{
    public interface IAlumnoService
    {
        Task<IEnumerable<EstudiantesReadDto>> ObtenerTodosAsync();
        Task<IEnumerable<AlumnoEscuelaDto>> ObtenerAlumnoEscuelaAsync();
        Task<IEnumerable<ProfesorAlumnoDto>> ObtenerProfesorAlumnoAsync();
        Task<EstudiantesReadDto?> ObtenerPorIdAsync(int id);
        Task<AlumnoEscuelaDto?> ObtenerAlumnoEscuelaPorIdAsync(int id);
        Task<ProfesorAlumnoDto?> ObtenerProfesorAlumnoPorIdAsync(int id);
       
        Task CrearAsync(EstudiantesCreateUpdateDto dto);
        Task ActualizarAsync(int id, EstudiantesCreateUpdateDto dto);
        Task EliminarAsync(int id);
        Task AsignarProfesorAsync(int alumnoId, int profesorId);
        Task InscribirEscuelaAsync(int alumnoId, int escuelaId);

    }
}