using EscuelasPrueba.Core.Domains.Entities;

namespace EscuelasPrueba.Core.Application.Interfaces.IRepository
{
    public interface IEscuelaRepository
    {
        Task<IEnumerable<Escuela>> ObtenerEscuelasAsync();
        Task<Escuela?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Escuela escuela);
        Task ActualizarAsync(Escuela escuela);
        Task EliminarAsync(int id);
    }
}