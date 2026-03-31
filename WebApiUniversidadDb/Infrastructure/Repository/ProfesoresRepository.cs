using Microsoft.EntityFrameworkCore;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Infrastructure.Databases;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Infrastructure.Repository
{
    public class ProfesoresRepository : IProfesoresRepository
    {
        private readonly UniversidadDbContext _context;

        public ProfesoresRepository(UniversidadDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profesor>> GetAllAsync()
        {
            return await _context.Profesores
                .Where(p => p.Activo)
                .ToListAsync();
        }

        public async Task<Profesor?> GetByIdAsync(int id)
        {
            return await _context.Profesores
                .FirstOrDefaultAsync(p => p.ProfesorId == id && p.Activo);
        }

        public async Task<Profesor> CreateAsync(Profesor profesor)
        {
            profesor.FechaCreacion = DateTime.Now;
            profesor.Activo = true;
            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();
            return profesor;
        }

        public async Task<bool> UpdateAsync(Profesor profesor)
        {
            var existing = await _context.Profesores.FindAsync(profesor.ProfesorId);
            if (existing == null) return false;

            existing.Nombre = profesor.Nombre;
            existing.Apellido = profesor.Apellido;
            existing.Especialidad = profesor.Especialidad;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Profesores.FindAsync(id);
            if (existing == null) return false;

            existing.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}