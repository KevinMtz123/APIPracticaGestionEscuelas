using EscuelasPrueba.Core.Application.Interfaces.IRepository;
using EscuelasPrueba.Core.Domains.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EscuelasPrueba.Infraestructure.Data;

namespace EscuelasPrueba.Infraestructure.Repositories
{
    public class AlumnoRepository : IAlumnoRepository
    {
        private readonly MusicaDbContext _context;

        public AlumnoRepository(MusicaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Alumno>> ObtenerAlumnosAsync()
        {
            return await _context.Alumnos.FromSqlRaw("EXEC ObtenerAlumnos").ToListAsync();
        }
        public async Task<IEnumerable<AlumnoEscuela>> ObtenerAlumnoEscuelaAsync()
        {
            return await _context.AlumnoEscuelas.FromSqlRaw("EXEC ObtenerAlumnoEscuela").ToListAsync();
        }
        public async Task<IEnumerable<ProfesorAlumno>> ObtenerProfesorAlumnoAsync()
        {
            return await _context.ProfesorAlumnos.FromSqlRaw("EXEC ObtenerProfesorAlumno").ToListAsync();
        }
        public async Task<Alumno?> ObtenerPorIdAsync(int id)
        {
            return await _context.Alumnos.FindAsync(id);
        }
        public async Task<AlumnoEscuela?> ObtenerAlumnoEscuelaPorIdAsync(int id)
        {
            return await _context.AlumnoEscuelas.FirstOrDefaultAsync(al => al.EscuelaId == id);
        }
        public async Task<ProfesorAlumno?> ObtenerProfesorAlumnoPorIdAsync(int profesorId)
        {
            return await _context.ProfesorAlumnos
                .Include(pa => pa.Alumno)
                    .ThenInclude(a => a.AlumnoEscuelas)
                        .ThenInclude(ae => ae.Escuela) 
                .FirstOrDefaultAsync(pa => pa.ProfesorId == profesorId);
        }

        


        public async Task CrearAsync(Alumno alumno)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC CrearAlumno @Nombre, @Apellido, @FechaNacimiento, @NumeroIdentificacion",
                new SqlParameter("@Nombre", alumno.Nombre),
                new SqlParameter("@Apellido", alumno.Apellido),
                new SqlParameter("@FechaNacimiento", alumno.FechaNacimiento),
                new SqlParameter("@NumeroIdentificacion", alumno.NumeroIdentificacion));
        }

        public async Task ActualizarAsync(Alumno alumno)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC ActualizarAlumno @Id, @Nombre, @Apellido, @FechaNacimiento, @NumeroIdentificacion",
                new SqlParameter("@Id", alumno.Id),
                new SqlParameter("@Nombre", alumno.Nombre),
                new SqlParameter("@Apellido", alumno.Apellido),
                new SqlParameter("@FechaNacimiento", alumno.FechaNacimiento),
                new SqlParameter("@NumeroIdentificacion", alumno.NumeroIdentificacion));
        }

        public async Task EliminarAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC EliminarAlumno @Id", new SqlParameter("@Id", id));
        }

        public async Task AsignarProfesorAsync(int alumnoId, int profesorId)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC AsignarAlumnoAProfesor @ProfesorId, @AlumnoId",
                new SqlParameter("@ProfesorId", profesorId),
                new SqlParameter("@AlumnoId", alumnoId));
        }

        public async Task InscribirEscuelaAsync(int alumnoId, int escuelaId)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC InscribirAlumnoAEscuela @AlumnoId, @EscuelaId",
                new SqlParameter("@AlumnoId", alumnoId),
                new SqlParameter("@EscuelaId", escuelaId));
        }
    }
}