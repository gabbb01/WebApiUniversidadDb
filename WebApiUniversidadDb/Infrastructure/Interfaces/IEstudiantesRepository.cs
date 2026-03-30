using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Infrastructure.Interfaces
{
    public interface IEstudiantesRepository
    {
        Task<IEnumerable<Estudiante>> GetAllAsync();
        Task<Estudiante?> GetByIdAsync(int id);
        Task<Estudiante> CreateAsync(Estudiante estudiante);
        Task<bool> UpdateAsync(Estudiante estudiante);
        Task<bool> DeleteAsync(int id);
    }
}