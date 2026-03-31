using Microsoft.EntityFrameworkCore;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Infrastructure.Databases
{
    public class UniversidadDbContext : DbContext
    {
        public UniversidadDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Asignatura> Asignaturas => Set<Asignatura>();
        public DbSet<Aula> Aulas => Set<Aula>();
        public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
        public DbSet<Profesor> Profesores => Set<Profesor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeo para tabla asignatura
            modelBuilder.Entity<Asignatura>(entity =>
            {
                entity.ToTable("Asignaturas");
                entity.HasKey(x => x.AsignaturaId);

                entity.Property(x => x.AsignaturaId)
                .HasColumnName("AsignaturaId")
                .HasColumnType("INT");

                entity.Property(x => x.Nombre)
                .HasColumnName("Nombre")
                .HasColumnType("NVARCHAR(100)");

                entity.Property(x => x.Creditos)
                .HasColumnName("Creditos")
                .HasColumnType("INT");

                entity.Property(x => x.ProfesorId)
                .HasColumnName("ProfesorId")
                .HasColumnType("INT");

                entity.Property(x => x.AulaId)
                .HasColumnName("AulaId")
                .HasColumnType("INT");

                entity.Property(x => x.Activo)
                .HasColumnName("Activo")
                .HasColumnType("BIT")
                .HasDefaultValue(true);

                entity.Property(x => x.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .HasColumnType("DATETIME")
                .HasDefaultValue(DateTime.Now);

            });

            // Mapeo para tabla Aula
            modelBuilder.Entity<Aula>(entity =>
            {
                entity.ToTable("Aulas");
                entity.HasKey(x => x.AulaId);

                entity.Property(x => x.AulaId)
                .HasColumnName("AulaId")
                .HasColumnType("INT");

                entity.Property(x => x.CodigoAula)
                .HasColumnName("CodigoAula")
                .HasColumnType("NVARCHAR(20)");

                entity.Property(x => x.Capacidad)
                .HasColumnName("Capacidad")
                .HasColumnType("INT");

                entity.Property(x => x.Activo)
                .HasColumnName("Activo")
                .HasColumnType("BIT")
                .HasDefaultValue(true);

                entity.Property(x => x.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .HasColumnType("DATETIME")
                .HasDefaultValue(DateTime.Now);

            });
            //Mapeo para tabla estudiante
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.ToTable("Estudiantes");
                entity.HasKey(x => x.EstudianteId);

                entity.Property(x => x.EstudianteId)
                .HasColumnName("EstudianteId")
                .HasColumnType("INT");

                entity.Property(x => x.NumeroCuenta)
                .HasColumnName("NumeroCuenta")
                .HasColumnType("INT");

                entity.Property(x => x.Nombre)
                .HasColumnName("Nombre")
                .HasColumnType("NVARCHAR(100)");

                entity.Property(x => x.Apellido)
                .HasColumnName("Apellido")
                .HasColumnType("NVARCHAR(100)");

                entity.Property(x => x.Correo)
                .HasColumnName("Correo")
                .HasColumnType("NVARCHAR(100)");

                entity.Property(x => x.Activo)
                .HasColumnName("Activo")
                .HasColumnType("BIT")
                .HasDefaultValue(true);

                entity.Property(x => x.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .HasColumnType("DATETIME")
                .HasDefaultValue(DateTime.Now);

            });

            //Mapeo para tabla estudiante
            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.ToTable("Profesores");
                entity.HasKey(x => x.ProfesorId);

                entity.Property(x => x.ProfesorId)
                .HasColumnName("ProfesorId")
                .HasColumnType("INT");

                entity.Property(x => x.Nombre)
                .HasColumnName("Nombre")
                .HasColumnType("NVARCHAR(100)");

                entity.Property(x => x.Apellido)
                .HasColumnName("Apellido")
                .HasColumnType("NVARCHAR(100)");

                entity.Property(x => x.Especialidad)
                .HasColumnName("Especialidad")
                .HasColumnType("NVARCHAR(100)");

                entity.Property(x => x.Activo)
                .HasColumnName("Activo")
                .HasColumnType("BIT")
                .HasDefaultValue(true);

                entity.Property(x => x.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .HasColumnType("DATETIME")
                .HasDefaultValue(DateTime.Now);

            });
        }
    }
}