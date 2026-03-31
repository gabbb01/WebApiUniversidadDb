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
            base.OnModelCreating(modelBuilder);
        }
    }
}