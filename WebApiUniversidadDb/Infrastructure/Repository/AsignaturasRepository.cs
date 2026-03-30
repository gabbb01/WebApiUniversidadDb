using Microsoft.EntityFrameworkCore;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Infrastructure.Databases;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Infrastructure.Repository
{
    public class AsignaturasRepository : IAsignaturasRepository
    {
        private readonly UniversidadDbContext _context;

        public AsignaturasRepository(UniversidadDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asignatura>> GetAllAsync()
        {
            return await _context.Asignaturas
                .Where(a => a.Activo)
                .Include(a => a.ProfesorId)
                .Include(a => a.AulaId)
                .ToListAsync();
        }

        public async Task<Asignatura?> GetByIdAsync(int id)
        {
            return await _context.Asignaturas
                .Include(a => a.ProfesorId)
                .Include(a => a.AulaId)
                .FirstOrDefaultAsync(a => a.AsignaturaId == id && a.Activo);
        }

        public async Task<Asignatura> CreateAsync(Asignatura asignatura)
        {
            asignatura.FechaCreacion = DateTime.Now;
            asignatura.Activo = true;
            _context.Asignaturas.Add(asignatura);
            await _context.SaveChangesAsync();
            return asignatura;
        }

        public async Task<bool> UpdateAsync(Asignatura asignatura)
        {
            var existing = await _context.Asignaturas.FindAsync(asignatura.AsignaturaId);
            if (existing == null) return false;

            existing.Nombre = asignatura.Nombre;
            existing.Creditos = asignatura.Creditos;
            existing.ProfesorId = asignatura.ProfesorId;
            existing.AulaId = asignatura.AulaId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Asignaturas.FindAsync(id);
            if (existing == null) return false;

            existing.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}