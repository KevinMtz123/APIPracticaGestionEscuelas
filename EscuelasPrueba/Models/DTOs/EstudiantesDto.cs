using EscuelasPrueba.Core.Domains.Entities;
using System.ComponentModel.DataAnnotations;

namespace EscuelasPrueba.Models.DTOs
{
    public class EstudiantesReadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Apellido { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string NumeroIdentificacion { get; set; } = null!;

        public IEnumerable<ProfesoresReadDto>? Profesores { get; set; }

        public IEnumerable<EscuelaReadDto>? Escuelas { get; set; }
    }

    public class EstudiantesCreateUpdateDto
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido es obligatorio")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "El Número de Identificación es obligatorio")]
        public string? NumeroIdentificacion { get; set; }

        [Required(ErrorMessage = "La Fecha de nacimiento es obligatoria")]
        public DateOnly FechaNacimiento { get; set; }

        public List<int>? ProfesoresIds { get; set; }
        public List<int>? EscuelasIds { get; set; }
    }

}
