using EscuelasPrueba.Core.Application.Interfaces.IRepository;
using EscuelasPrueba.Core.Domains.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EscuelasPrueba.Infraestructure.Data;

namespace EscuelasPrueba.Infraestructure.Repositories
{
    public class EscuelaRepository : IEscuelaRepository
    {
        private readonly MusicaDbContext _context;

        public EscuelaRepository(MusicaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Escuela>> ObtenerEscuelasAsync()
        {
            return await _context.Escuelas.FromSqlRaw("EXEC ObtenerEscuelas").ToListAsync();
        }

        public async Task<Escuela?> ObtenerPorIdAsync(int id)
        {
            return await _context.Escuelas.FindAsync(id);
        }

        public async Task CrearAsync(Escuela escuela)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC CrearEscuela @Nombre, @Descripcion, @Codigo",
                new SqlParameter("@Nombre", escuela.Nombre),
                new SqlParameter("@Descripcion", escuela.Descripcion ?? ""),
                new SqlParameter("@Codigo", escuela.Codigo));
        }

        public async Task ActualizarAsync(Escuela escuela)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC ActualizarEscuela @Id, @Nombre, @Descripcion, @Codigo",
                new SqlParameter("@Id", escuela.Id),
                new SqlParameter("@Nombre", escuela.Nombre),
                new SqlParameter("@Descripcion", escuela.Descripcion ?? ""),
                new SqlParameter("@Codigo", escuela.Codigo));
        }

        public async Task EliminarAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC EliminarEscuela @Id", new SqlParameter("@Id", id));
        }
    }
}
