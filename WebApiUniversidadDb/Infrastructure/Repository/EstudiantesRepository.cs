using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Infrastructure.Databases;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Infrastructure.Repository
{
    public class EstudiantesRepository : IEstudiantesRepository
    {
        private readonly UniversidadDbContext universidadDbContext;

        public EstudiantesRepository(UniversidadDbContext universidadDbContext)
        {
            this.universidadDbContext = universidadDbContext;
        }

        public async Task ActualizarEstudiante(Estudiante estudiante)
        {
            Estudiante estudianteExistente =
                universidadDbContext.Estudiantes
                .FirstOrDefault(x => x.EstudianteId == estudiante.EstudianteId)!;

            estudianteExistente.NumeroCuenta = estudiante.NumeroCuenta;
            estudianteExistente.Nombre = estudiante.Nombre;
            estudianteExistente.Apellido = estudiante.Apellido;
            estudianteExistente.Correo = estudiante.Correo;
            estudianteExistente.Activo = estudiante.Activo;

            await universidadDbContext.SaveChangesAsync();
        }

        public async Task AgregarEstudiante(Estudiante estudiante)
        {
            universidadDbContext.Estudiantes.Add(estudiante);
            await universidadDbContext.SaveChangesAsync();
        }

        public async Task InactivarEstudiante(int id)
        {
            Estudiante estudianteExistente =
                universidadDbContext.Estudiantes
                .FirstOrDefault(x => x.EstudianteId == id)!;

            estudianteExistente.Activo = false;

            await universidadDbContext.SaveChangesAsync();
        }

        public async Task<Estudiante?> ObtenerEstudianteId(int id)
        {
            Estudiante? estudiante =
                await universidadDbContext.Estudiantes.FirstOrDefaultAsync(x => x.EstudianteId == id);
            return estudiante ?? new Estudiante();
        }

        public async Task<List<Estudiante>> ObtenerEstudiantes()
        {
            return await universidadDbContext.Estudiantes.ToListAsync();
        }
    }
}