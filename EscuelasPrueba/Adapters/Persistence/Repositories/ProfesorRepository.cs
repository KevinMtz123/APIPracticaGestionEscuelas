using EscuelasPrueba.Core.Application.Interfaces.IRepository;
using EscuelasPrueba.Core.Domains.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EscuelasPrueba.Infraestructure.Data;

namespace EscuelasPrueba.Infraestructure.Repositories
{
    public class ProfesorRepository : IProfesorRepository
    {
        private readonly MusicaDbContext _context;

        public ProfesorRepository(MusicaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profesore>> ObtenerProfesoresAsync()
        {
            return await _context.Profesores.FromSqlRaw("EXEC ObtenerProfesores").ToListAsync();
        }
        public async Task<Profesore?> ObtenerProfesorConEscuelasYAlumnosAsync(int profesorId)
        {
            return await _context.Profesores
                .Include(p => p.ProfesorAlumnos)
                    .ThenInclude(pa => pa.Alumno)
                        .ThenInclude(a => a.AlumnoEscuelas)
                            .ThenInclude(ae => ae.Escuela)
                .FirstOrDefaultAsync(p => p.Id == profesorId);
        }

        public async Task<Profesore?> ObtenerPorIdAsync(int id)
        {
            return await _context.Profesores.FindAsync(id);
        }

        public async Task<int> CrearAsync(Profesore profesor)
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "EXEC CrearProfesor @Nombre, @Apellido, @NumeroIdentificacion, @EscuelaId";
            command.CommandType = System.Data.CommandType.Text;

            command.Parameters.Add(new SqlParameter("@Nombre", profesor.Nombre));
            command.Parameters.Add(new SqlParameter("@Apellido", profesor.Apellido));
            command.Parameters.Add(new SqlParameter("@NumeroIdentificacion", profesor.NumeroIdentificacion));
            command.Parameters.Add(new SqlParameter("@EscuelaId", profesor.EscuelaId));

            var id = 0;
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    id = Convert.ToInt32(reader["Id"]);
                }
            }

            return id;
        }


        public async Task ActualizarAsync(Profesore profesor)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC ActualizarProfesor @Id, @Nombre, @Apellido, @NumeroIdentificacion, @EscuelaId",
                new SqlParameter("@Id", profesor.Id),
                new SqlParameter("@Nombre", profesor.Nombre),
                new SqlParameter("@Apellido", profesor.Apellido),
                new SqlParameter("@NumeroIdentificacion", profesor.NumeroIdentificacion),
                new SqlParameter("@EscuelaId", profesor.EscuelaId));
        }

        public async Task EliminarAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC EliminarProfesor @Id", new SqlParameter("@Id", id));
        }

        public async Task<IEnumerable<Alumno>> ObtenerAlumnosPorProfesorAsync(int profesorId)
        {
            return await _context.Alumnos
                .FromSqlRaw("EXEC ConsultaAlumnosPorProfesor @ProfesorId", new SqlParameter("@ProfesorId", profesorId))
                .ToListAsync();
        }

        public async Task AsignarAlumnoAsync(int profesorId, int alumnoId)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC AsignarAlumnoAProfesor @ProfesorId, @AlumnoId",
                new SqlParameter("@ProfesorId", profesorId),
                new SqlParameter("@AlumnoId", alumnoId));
        }
    }
}
