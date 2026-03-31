using Microsoft.EntityFrameworkCore;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Infrastructure.Databases;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Infrastructure.Repository
{
    public class AulasRepository : IAulasRepository
    {
        private readonly UniversidadDbContext _context;

        public AulasRepository(UniversidadDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aula>> GetAllAsync()
        {
            return await _context.Aulas
                .Where(a => a.Activo)
                .ToListAsync();
        }

        public async Task<Aula?> GetByIdAsync(int id)
        {
            return await _context.Aulas
                .FirstOrDefaultAsync(a => a.AulaId == id && a.Activo);
        }

        public async Task<Aula> CreateAsync(Aula aula)
        {
            aula.FechaCreacion = DateTime.Now;
            aula.Activo = true;
            _context.Aulas.Add(aula);
            await _context.SaveChangesAsync();
            return aula;
        }

        public async Task<bool> UpdateAsync(Aula aula)
        {
            var existing = await _context.Aulas.FindAsync(aula.AulaId);
            if (existing == null) return false;

            existing.CodigoAula = aula.CodigoAula;
            existing.Capacidad = aula.Capacidad;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Aulas.FindAsync(id);
            if (existing == null) return false;

            existing.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}