using EscuelasPrueba.Models.DTOs;

namespace EscuelasPrueba.Core.Application.Interfaces.IServices
{
    public interface IProfesorService
    {
        Task<IEnumerable<ProfesoresReadDto>> ObtenerTodosAsync();
        Task<ProfesoresReadDto?> ObtenerPorIdAsync(int id);
        Task<List<EscuelaConAlumnosDto>> ObtenerEscuelasYAlumnosPorProfesorAsync(int profesorId);
        Task CrearAsync(ProfesoresCreateUpdateDto dto);
        Task ActualizarAsync(int id, ProfesoresCreateUpdateDto dto);
        Task EliminarAsync(int id);

        Task<IEnumerable<EstudiantesReadDto>> ObtenerAlumnosAsignadosAsync(int profesorId);

        Task AsignarAlumnoAsync(int profesorId, int alumnoId);
    }
}
