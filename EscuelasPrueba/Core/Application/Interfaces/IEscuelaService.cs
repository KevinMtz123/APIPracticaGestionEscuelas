using EscuelasPrueba.Models.DTOs;

namespace EscuelasPrueba.Core.Application.Interfaces.IServices
{
    public interface IEscuelaService
    {
        Task<IEnumerable<EscuelaReadDto>> ObtenerTodosAsync();
        Task<EscuelaReadDto?> ObtenerPorIdAsync(int id);
        Task CrearAsync(EscuelaCreateUpdateDto dto);
        Task ActualizarAsync(int id, EscuelaCreateUpdateDto dto);
        Task EliminarAsync(int id);
    }
}