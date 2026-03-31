using Microsoft.EntityFrameworkCore;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Infrastructure.Databases;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Infrastructure.Repository
{
    public class EstudiantesRepository : IEstudiantesRepository
    {
        private readonly UniversidadDbContext _context;

        public EstudiantesRepository(UniversidadDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            return await _context.Estudiantes
                .Where(e => e.Activo)
                .ToListAsync();
        }

        public async Task<Estudiante?> GetByIdAsync(int id)
        {
            return await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.EstudianteId == id && e.Activo);
        }

        public async Task<Estudiante> CreateAsync(Estudiante estudiante)
        {
            estudiante.FechaCreacion = DateTime.Now;
            estudiante.Activo = true;
            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
            return estudiante;
        }

        public async Task<bool> UpdateAsync(Estudiante estudiante)
        {
            var existing = await _context.Estudiantes.FindAsync(estudiante.EstudianteId);
            if (existing == null) return false;

            existing.NumeroCuenta = estudiante.NumeroCuenta;
            existing.Nombre = estudiante.Nombre;
            existing.Apellido = estudiante.Apellido;
            existing.Correo = estudiante.Correo;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Estudiantes.FindAsync(id);
            if (existing == null) return false;

            existing.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}