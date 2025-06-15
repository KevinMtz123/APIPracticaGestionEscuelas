using EscuelasPrueba.Core.Application.Interfaces.IRepository;
using EscuelasPrueba.Core.Application.Interfaces.IServices;
using EscuelasPrueba.Models.DTOs;
using EscuelasPrueba.Core.Domains.Entities;

namespace EscuelasPrueba.Core.Application.Services
{
    public class EscuelaService : IEscuelaService
    {
        private readonly IEscuelaRepository _repo;

        public EscuelaService(IEscuelaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EscuelaReadDto>> ObtenerTodosAsync()
        {
            var escuelas = await _repo.ObtenerEscuelasAsync();
            return escuelas.Select(e => new EscuelaReadDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,
                Codigo = e.Codigo
            });
        }

        public async Task<EscuelaReadDto?> ObtenerPorIdAsync(int id)
        {
            var e = await _repo.ObtenerPorIdAsync(id);
            if (e == null) return null;

            return new EscuelaReadDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,
                Codigo = e.Codigo
            };
        }

        public async Task CrearAsync(EscuelaCreateUpdateDto dto)
        {
            var escuela = new Escuela
            {
                Nombre = dto.Nombre!,
                Descripcion = dto.Descripcion!,
                Codigo = dto.Codigo!
            };

            await _repo.CrearAsync(escuela);
        }

        public async Task ActualizarAsync(int id, EscuelaCreateUpdateDto dto)
        {
            var escuela = new Escuela
            {
                Id = id,
                Nombre = dto.Nombre!,
                Descripcion = dto.Descripcion!,
                Codigo = dto.Codigo!
            };

            await _repo.ActualizarAsync(escuela);
        }

        public async Task EliminarAsync(int id)
        {
            await _repo.EliminarAsync(id);
        }
    }
}
