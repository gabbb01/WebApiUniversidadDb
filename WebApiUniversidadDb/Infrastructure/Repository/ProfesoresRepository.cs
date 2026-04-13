using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Infrastructure.Databases;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Infrastructure.Repository
{
    public class ProfesoresRepository : IProfesoresRepository
    {
        private readonly UniversidadDbContext universidadDbContext;

        public ProfesoresRepository(UniversidadDbContext universidadDbContext)
        {
            this.universidadDbContext = universidadDbContext;
        }

        public async Task<List<Profesor>> ObtenerProfesores()
        {
            return await universidadDbContext.Profesores.ToListAsync();
        }

        public async Task<Profesor?> ObtenerProfesorId(int id)
        {
            Profesor? profesor =
                await universidadDbContext.Profesores.FirstOrDefaultAsync(x => x.ProfesorId == id);
            return profesor ?? new Profesor();
        }

        public async Task AgregarProfesor(Profesor profesor)
        {
            universidadDbContext.Profesores.Add(profesor);
            await universidadDbContext.SaveChangesAsync();
        }

        public async Task ActualizarProfesor(Profesor profesor)
        {
            Profesor profesorExistente =
                universidadDbContext.Profesores
                .FirstOrDefault(x => x.ProfesorId == profesor.ProfesorId)!;

            profesorExistente.Nombre = profesor.Nombre;
            profesorExistente.Apellido = profesor.Apellido;
            profesorExistente.Especialidad = profesor.Especialidad;
            profesorExistente.Activo = profesor.Activo;

            await universidadDbContext.SaveChangesAsync();
        }

        public async Task InactivarProfesor(int id)
        {
            Profesor profesorExistente =
                universidadDbContext.Profesores
                .FirstOrDefault(x => x.ProfesorId == id)!;

            profesorExistente.Activo = false;

            await universidadDbContext.SaveChangesAsync();
        }
    }
}