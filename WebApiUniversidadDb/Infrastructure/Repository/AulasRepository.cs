using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Infrastructure.Databases;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Infrastructure.Repository
{
    public class AulasRepository : IAulasRepository
    {
        private readonly UniversidadDbContext universidadDbContext;

        public AulasRepository(UniversidadDbContext universidadDBContext, UniversidadDbContext universidadDbContext)
        {
            this.universidadDbContext = universidadDbContext;
        }

        public async Task ActualizarAula(Aula aula)
        {
            Aula aulaExistente =
                universidadDbContext.Aulas
                .FirstOrDefault(x => x.AulaId == aula.AulaId)!;

            aulaExistente.CodigoAula = aula.CodigoAula;
            aulaExistente.Capacidad = aula.Capacidad;
            aulaExistente.Activo = aula.Activo;

            await universidadDbContext.SaveChangesAsync();
        }

        public async Task AgregarAula(Aula aula)
        {
            universidadDbContext.Aulas.Add(aula);
            await universidadDbContext.SaveChangesAsync();
        }

        public async Task InactivarAula(int id)
        {
            Aula aulaExistente =
                universidadDbContext.Aulas
                .FirstOrDefault(x => x.AulaId == id)!;

            aulaExistente.Activo = false;

            await universidadDbContext.SaveChangesAsync();
        }

        public async Task<List<Aula>> ObtenerAula()
        {
            return await universidadDbContext.Aulas.ToListAsync();
        }

        public async Task<Aula?> ObtenerAulaId(int id)
        {
            Aula? aula =
                await universidadDbContext.Aulas.FirstOrDefaultAsync(x => x.AulaId == id);
            return aula ?? new Aula();
        }
    }
}