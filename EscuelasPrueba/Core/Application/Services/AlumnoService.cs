using EscuelasPrueba.Core.Application.Interfaces.IRepository;
using EscuelasPrueba.Core.Application.Interfaces.IServices;
using EscuelasPrueba.Models.DTOs;
using EscuelasPrueba.Core.Domains.Entities;

namespace EscuelasPrueba.Core.Application.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IAlumnoRepository _repo;

        public AlumnoService(IAlumnoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EstudiantesReadDto>> ObtenerTodosAsync()
        {
            var alumnos = await _repo.ObtenerAlumnosAsync();
            return alumnos.Select(a => new EstudiantesReadDto
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Apellido = a.Apellido,
                FechaNacimiento = a.FechaNacimiento,
                NumeroIdentificacion = a.NumeroIdentificacion
            });
        }
        public async Task<IEnumerable<AlumnoEscuelaDto>> ObtenerAlumnoEscuelaAsync()
        {
            var alumnos = await _repo.ObtenerAlumnoEscuelaAsync();
            return alumnos.Select(a => new AlumnoEscuelaDto
            {
                AlumnoId = a.AlumnoId,
                EscuelaId = a.EscuelaId
            });
        }
        public async Task<IEnumerable<ProfesorAlumnoDto>> ObtenerProfesorAlumnoAsync()
        {
            var alumnos = await _repo.ObtenerProfesorAlumnoAsync();
            return alumnos.Select(a => new ProfesorAlumnoDto
            {
                AlumnoId = a.AlumnoId,
                ProfesorId = a.ProfesorId
            });
        }

        public async Task<EstudiantesReadDto?> ObtenerPorIdAsync(int id)
        {
            var a = await _repo.ObtenerPorIdAsync(id);
            if (a == null) return null;

            return new EstudiantesReadDto
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Apellido = a.Apellido,
                FechaNacimiento = a.FechaNacimiento,
                NumeroIdentificacion = a.NumeroIdentificacion
            };
        }
        public async Task<AlumnoEscuelaDto?> ObtenerAlumnoEscuelaPorIdAsync(int id)
        {
            var a = await _repo.ObtenerAlumnoEscuelaPorIdAsync(id);
            if (a == null) return null;

            return new AlumnoEscuelaDto
            {
                AlumnoId = a.AlumnoId,
                EscuelaId = a.EscuelaId,
                ProfesorId = a.Alumno.ProfesorAlumnos.FirstOrDefault()?.ProfesorId
            };
        }
        

        public async Task<ProfesorAlumnoDto?> ObtenerProfesorAlumnoPorIdAsync(int id)
        {
            var a = await _repo.ObtenerProfesorAlumnoPorIdAsync(id);
            if (a == null) return null;

            return new ProfesorAlumnoDto
            {
                AlumnoId = a.AlumnoId,
                ProfesorId = a.ProfesorId,
                EscuelaId = a.Alumno.AlumnoEscuelas.FirstOrDefault()?.EscuelaId
            };
        }

        public async Task CrearAsync(EstudiantesCreateUpdateDto dto)
        {
            var alumno = new Alumno
            {
                Nombre = dto.Nombre!,
                Apellido = dto.Apellido!,
                FechaNacimiento = dto.FechaNacimiento,
                NumeroIdentificacion = dto.NumeroIdentificacion!
            };

            await _repo.CrearAsync(alumno);

            if (dto.ProfesoresIds != null)
                foreach (var pid in dto.ProfesoresIds)
                    await _repo.AsignarProfesorAsync(alumno.Id, pid);

            if (dto.EscuelasIds != null)
                foreach (var eid in dto.EscuelasIds)
                    await _repo.InscribirEscuelaAsync(alumno.Id, eid);
        }

        public async Task ActualizarAsync(int id, EstudiantesCreateUpdateDto dto)
        {
            var alumno = new Alumno
            {
                Id = id,
                Nombre = dto.Nombre!,
                Apellido = dto.Apellido!,
                FechaNacimiento = dto.FechaNacimiento,
                NumeroIdentificacion = dto.NumeroIdentificacion!
            };

            await _repo.ActualizarAsync(alumno);
        }

        public async Task EliminarAsync(int id)
        {
            await _repo.EliminarAsync(id);
        }
        public async Task AsignarProfesorAsync(int alumnoId, int profesorId)
        {
            await _repo.AsignarProfesorAsync(alumnoId, profesorId);
        }

        public async Task InscribirEscuelaAsync(int alumnoId, int escuelaId)
        {
            await _repo.InscribirEscuelaAsync(alumnoId, escuelaId);
        }

    }
}
