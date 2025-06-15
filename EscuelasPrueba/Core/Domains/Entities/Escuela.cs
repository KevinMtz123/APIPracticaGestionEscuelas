using System;
using System.Collections.Generic;

namespace EscuelasPrueba.Core.Domains.Entities;

public partial class Escuela
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Codigo { get; set; } = null!;

    public virtual ICollection<Profesore> Profesores { get; set; } = new List<Profesore>();

    public virtual ICollection<AlumnoEscuela> AlumnoEscuelas { get; set; } = new List<AlumnoEscuela>();

}
