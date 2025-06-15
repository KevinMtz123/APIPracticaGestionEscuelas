using EscuelasPrueba.Core.Application.Interfaces.IRepository;
using EscuelasPrueba.Core.Application.Interfaces.IServices;
using EscuelasPrueba.Models.DTOs;
using EscuelasPrueba.Core.Domains.Entities;

namespace EscuelasPrueba.Core.Application.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly IProfesorRepository _repo;

        public ProfesorService(IProfesorRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProfesoresReadDto>> ObtenerTodosAsync()
        {
            var profesores = await _repo.ObtenerProfesoresAsync();
            return profesores.Select(p => new ProfesoresReadDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                NumeroIdentificacion = p.NumeroIdentificacion,
                EscuelaId = p.EscuelaId
            });
        }

        public async Task<ProfesoresReadDto?> ObtenerPorIdAsync(int id)
        {
            var p = await _repo.ObtenerPorIdAsync(id);
            if (p == null) return null;

            return new ProfesoresReadDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                NumeroIdentificacion = p.NumeroIdentificacion,
                EscuelaId = p.EscuelaId
            };
        }
        public async Task<List<EscuelaConAlumnosDto>> ObtenerEscuelasYAlumnosPorProfesorAsync(int profesorId)
        {
            var profesor = await _repo.ObtenerProfesorConEscuelasYAlumnosAsync(profesorId);
            if (profesor == null) return [];

            var escuelasDict = new Dictionary<int, EscuelaConAlumnosDto>();

            foreach (var pa in profesor.ProfesorAlumnos)
            {
                var alumno = pa.Alumno;
                foreach (var ae in alumno.AlumnoEscuelas)
                {
                    var escuela = ae.Escuela;
                    if (!escuelasDict.ContainsKey(escuela.Id))
                    {
                        escuelasDict[escuela.Id] = new EscuelaConAlumnosDto
                        {
                            EscuelaId = escuela.Id,
                            Nombre = escuela.Nombre,
                            Alumnos = []
                        };
                    }

                    escuelasDict[escuela.Id].Alumnos.Add(new AlumnoSimpleDto
                    {
                        AlumnoId = alumno.Id,
                        Nombre = alumno.Nombre,
                        Apellido = alumno.Apellido
                    });
                }
            }

            return escuelasDict.Values.ToList();
        }
        public async Task CrearAsync(ProfesoresCreateUpdateDto dto)
        {
            var profesor = new Profesore
            {
                Nombre = dto.Nombre!,
                Apellido = dto.Apellido!,
                NumeroIdentificacion = dto.NumeroIdentificacion!,
                EscuelaId = dto.EscuelaId
            };

            await _repo.CrearAsync(profesor);

            if (dto.AlumnosIds != null)
            {
                foreach (var alumnoId in dto.AlumnosIds)
                {
                    await _repo.AsignarAlumnoAsync(profesor.Id, alumnoId);
                }
            }
        }

        public async Task ActualizarAsync(int id, ProfesoresCreateUpdateDto dto)
        {
            var profesor = new Profesore
            {
                Id = id,
                Nombre = dto.Nombre!,
                Apellido = dto.Apellido!,
                NumeroIdentificacion = dto.NumeroIdentificacion!,
                EscuelaId = dto.EscuelaId
            };

            await _repo.ActualizarAsync(profesor);
        }

        public async Task EliminarAsync(int id)
        {
            await _repo.EliminarAsync(id);
        }

        public async Task<IEnumerable<EstudiantesReadDto>> ObtenerAlumnosAsignadosAsync(int profesorId)
        {
            var alumnos = await _repo.ObtenerAlumnosPorProfesorAsync(profesorId);

            return alumnos.Select(a => new EstudiantesReadDto
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Apellido = a.Apellido,
                FechaNacimiento = a.FechaNacimiento,
                NumeroIdentificacion = a.NumeroIdentificacion
            });
        }
        public async Task AsignarAlumnoAsync(int profesorId, int alumnoId)
        {
            await _repo.AsignarAlumnoAsync(profesorId, alumnoId);
        }

    }

}
