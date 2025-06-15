using System;
using System.Collections.Generic;
using EscuelasPrueba.Core.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace EscuelasPrueba.Infraestructure.Data;

public partial class MusicaDbContext : DbContext
{
    public MusicaDbContext()
    {
    }

    public MusicaDbContext(DbContextOptions<MusicaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Escuela> Escuelas { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    public virtual DbSet<AlumnoEscuela> AlumnoEscuelas { get; set; }

    public virtual DbSet<ProfesorAlumno> ProfesorAlumnos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Alumnos__3214EC0750A61426");

            entity.HasIndex(e => e.NumeroIdentificacion, "UQ__Alumnos__FCA68D911E9E09D1").IsUnique();

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.NumeroIdentificacion).HasMaxLength(50);
        });

        modelBuilder.Entity<Escuela>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Escuelas__3214EC07098B14E6");

            entity.HasIndex(e => e.Codigo, "UQ__Escuelas__06370DAC98174072").IsUnique();

            entity.Property(e => e.Codigo).HasMaxLength(20);
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Profesor__3214EC07A77BF848");

            entity.HasIndex(e => e.NumeroIdentificacion, "UQ__Profesor__FCA68D916F42E7A1").IsUnique();

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.NumeroIdentificacion).HasMaxLength(50);

            entity.HasOne(d => d.Escuela)
                .WithMany(p => p.Profesores)
                .HasForeignKey(d => d.EscuelaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profesore__Escue__4D94879B");
        });

        modelBuilder.Entity<AlumnoEscuela>(entity =>
        {
            entity.HasKey(e => new { e.AlumnoId, e.EscuelaId }).HasName("PK__AlumnoEs__D6C50B152A81CBEE");

            entity.ToTable("AlumnoEscuela");

            entity.HasOne(e => e.Alumno)
                .WithMany(a => a.AlumnoEscuelas)
                .HasForeignKey(e => e.AlumnoId)
                .HasConstraintName("FK__AlumnoEsc__Alumn__571DF1D5");

            entity.HasOne(e => e.Escuela)
                .WithMany(e => e.AlumnoEscuelas)
                .HasForeignKey(e => e.EscuelaId)
                .HasConstraintName("FK__AlumnoEsc__Escue__5812160E");
        });

        modelBuilder.Entity<ProfesorAlumno>(entity =>
        {
            entity.HasKey(e => new { e.ProfesorId, e.AlumnoId }).HasName("PK__Profesor__64F99A6932D8627B");

            entity.ToTable("ProfesorAlumno");

            entity.HasOne(e => e.Profesor)
                .WithMany(p => p.ProfesorAlumnos)
                .HasForeignKey(e => e.ProfesorId)
                .HasConstraintName("FK__ProfesorA__Profe__534D60F1");

            entity.HasOne(e => e.Alumno)
                .WithMany(a => a.ProfesorAlumnos)
                .HasForeignKey(e => e.AlumnoId)
                .HasConstraintName("FK__ProfesorA__Alumn__5441852A");
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
