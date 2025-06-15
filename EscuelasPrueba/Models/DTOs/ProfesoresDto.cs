using EscuelasPrueba.Core.Domains.Entities;
using System.ComponentModel.DataAnnotations;

namespace EscuelasPrueba.Models.DTOs
{
    public class ProfesoresReadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string NumeroIdentificacion { get; set; } = null!;
        public int EscuelaId { get; set; }

        public EscuelaReadDto? Escuela { get; set; }
        public IEnumerable<EstudiantesReadDto>? Alumnos { get; set; } 
    }

    public class ProfesoresCreateUpdateDto
    {
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Apellido { get; set; } = null!;
        [Required]
        public string NumeroIdentificacion { get; set; } = null!;
        [Required]
        public int EscuelaId { get; set; }

        public List<int>? AlumnosIds { get; set; }
    }

}
