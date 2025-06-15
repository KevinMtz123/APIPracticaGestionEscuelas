using EscuelasPrueba.Core.Domains.Entities;
using System.ComponentModel.DataAnnotations;

namespace EscuelasPrueba.Models.DTOs
{
    public class EscuelaReadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string Codigo { get; set; } = null!;

        public IEnumerable<ProfesoresReadDto>? Profesores { get; set; }
        public IEnumerable<EstudiantesReadDto>? Alumnos { get; set; }
    }

    public class EscuelaCreateUpdateDto
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El código es obligatorio")]
        public string? Codigo { get; set; }
    }
    public class AlumnoEscuelaDto
    {
        public int AlumnoId { get; set; }
        public int EscuelaId { get; set; }
        public int? ProfesorId { get; set; }

    }
    public class ProfesorAlumnoDto
    {
        public int ProfesorId { get; set; }
        public int AlumnoId { get; set; }
        public int? EscuelaId { get; set; }
    }

    public class EscuelaConAlumnosDto
    {
        public int EscuelaId { get; set; }
        public string Nombre { get; set; } = null!;
        public List<AlumnoSimpleDto> Alumnos { get; set; } = [];
    }

    public class AlumnoSimpleDto
    {
        public int AlumnoId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
    }

}
