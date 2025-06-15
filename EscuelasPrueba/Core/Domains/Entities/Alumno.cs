using System;
using System.Collections.Generic;

namespace EscuelasPrueba.Core.Domains.Entities;

public partial class Alumno
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string NumeroIdentificacion { get; set; } = null!;

    public virtual ICollection<ProfesorAlumno> ProfesorAlumnos { get; set; } = new List<ProfesorAlumno>();
    public virtual ICollection<AlumnoEscuela> AlumnoEscuelas { get; set; } = new List<AlumnoEscuela>();

}
