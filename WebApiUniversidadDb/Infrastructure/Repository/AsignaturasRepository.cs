using Microsoft.EntityFrameworkCore;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Infrastructure.Databases;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Infrastructure.Repository
{
    public class AsignaturasRepository : IAsignaturasRepository
    {
        private readonly UniversidadDbContext universidadDbContext;

        public AsignaturasRepository(UniversidadDbContext universidadDbContext)
        {
            this.universidadDbContext = universidadDbContext;
        }

        public async Task<List<Asignatura>> ObtenerAsignaturas()
        {
            return await universidadDbContext.Asignaturas.ToListAsync();
        }

        public async Task<Asignatura?> ObtenerAsignaturaId(int id)
        {
            Asignatura? asignatura =
                await universidadDbContext.Asignaturas.FirstOrDefaultAsync(x => x.AsignaturaId == id);
            return asignatura ?? new Asignatura();
        }

        public async Task AgregarAsignatura(Asignatura asignatura)
        {
            universidadDbContext.Asignaturas.Add(asignatura);
            await universidadDbContext.SaveChangesAsync();
        }

        public async Task ActualizarAsignatura(Asignatura asignatura)
        {
            Asignatura asignaturaExistente =
                universidadDbContext.Asignaturas
                .FirstOrDefault(x => x.AsignaturaId == asignatura.AsignaturaId)!;

            asignaturaExistente.Nombre = asignatura.Nombre;
            asignaturaExistente.Creditos = asignatura.Creditos;
            asignaturaExistente.ProfesorId = asignatura.ProfesorId;
            asignaturaExistente.AulaId = asignatura.AulaId;
            asignaturaExistente.Activo = asignatura.Activo;

            await universidadDbContext.SaveChangesAsync();
        }

        public async Task InactivarAsignatura(int id)
        {
            Asignatura asignaturaExistente =
                universidadDbContext.Asignaturas
                .FirstOrDefault(x => x.AsignaturaId == id)!;

            asignaturaExistente.Activo = false;

            await universidadDbContext.SaveChangesAsync();
        }
    }
}