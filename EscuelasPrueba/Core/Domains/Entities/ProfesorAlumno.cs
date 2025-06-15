using System;
using System.Collections.Generic;

namespace EscuelasPrueba.Core.Domains.Entities;

public partial class ProfesorAlumno
{
    public int ProfesorId { get; set; }
    public int AlumnoId { get; set; }

    public virtual Profesore Profesor { get; set; } = null!;
    public virtual Alumno Alumno { get; set; } = null!;
}

