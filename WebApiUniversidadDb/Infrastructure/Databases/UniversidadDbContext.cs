using Microsoft.EntityFrameworkCore;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Infrastructure.Databases
{
    public class UniversidadDbContext : DbContext
    {
        public UniversidadDbContext(DbContextOptions<UniversidadDbContext> options)
            : base(options)
        {
        }

        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Profesor> Profesores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}