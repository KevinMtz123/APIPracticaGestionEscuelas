using EscuelasPrueba.Core.Domains.Entities;

namespace EscuelasPrueba.Core.Application.Interfaces.IRepository
{
    public interface IAlumnoRepository
    {
        Task<IEnumerable<Alumno>> ObtenerAlumnosAsync();
        Task<IEnumerable<AlumnoEscuela>> ObtenerAlumnoEscuelaAsync();
        Task<IEnumerable<ProfesorAlumno>> ObtenerProfesorAlumnoAsync();
        Task<Alumno?> ObtenerPorIdAsync(int id);
        Task<AlumnoEscuela?> ObtenerAlumnoEscuelaPorIdAsync(int id);
        
        Task<ProfesorAlumno?> ObtenerProfesorAlumnoPorIdAsync(int id);
        Task CrearAsync(Alumno alumno);
        Task ActualizarAsync(Alumno alumno);
        Task EliminarAsync(int id);
        Task AsignarProfesorAsync(int alumnoId, int profesorId);
        Task InscribirEscuelaAsync(int alumnoId, int escuelaId);
    }
}