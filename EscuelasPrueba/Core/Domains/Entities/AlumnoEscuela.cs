using System;
using System.Collections.Generic;

namespace EscuelasPrueba.Core.Domains.Entities;

public partial class AlumnoEscuela
{
    public int AlumnoId { get; set; }
    public int EscuelaId { get; set; }

    public virtual Alumno Alumno { get; set; } = null!;
    public virtual Escuela Escuela { get; set; } = null!;
}

