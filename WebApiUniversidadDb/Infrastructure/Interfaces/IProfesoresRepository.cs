using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Infrastructure.Interfaces
{
    public interface IProfesoresRepository
    {
        Task<IEnumerable<Profesor>> GetAllAsync();
        Task<Profesor?> GetByIdAsync(int id);
        Task<Profesor> CreateAsync(Profesor profesor);
        Task<bool> UpdateAsync(Profesor profesor);
        Task<bool> DeleteAsync(int id);
    }
}