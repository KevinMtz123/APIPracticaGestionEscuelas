using System;
using System.Collections.Generic;

namespace EscuelasPrueba.Core.Domains.Entities;

public partial class Profesore
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string NumeroIdentificacion { get; set; } = null!;

    public int EscuelaId { get; set; }

    public virtual Escuela Escuela { get; set; } = null!;

    public virtual ICollection<ProfesorAlumno> ProfesorAlumnos { get; set; } = new List<ProfesorAlumno>();

}
