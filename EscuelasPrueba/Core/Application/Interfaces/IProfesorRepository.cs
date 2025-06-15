using EscuelasPrueba.Core.Domains.Entities;

namespace EscuelasPrueba.Core.Application.Interfaces.IRepository
{
    public interface IProfesorRepository
    {
        Task<IEnumerable<Profesore>> ObtenerProfesoresAsync();
        Task<Profesore?> ObtenerPorIdAsync(int id);
        Task<Profesore?> ObtenerProfesorConEscuelasYAlumnosAsync(int profesorId);
        Task<int> CrearAsync(Profesore profesor);

        Task ActualizarAsync(Profesore profesor);
        Task EliminarAsync(int id);

        Task<IEnumerable<Alumno>> ObtenerAlumnosPorProfesorAsync(int profesorId);
        Task AsignarAlumnoAsync(int profesorId, int alumnoId);
    }
}
