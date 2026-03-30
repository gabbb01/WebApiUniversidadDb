using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Infrastructure.Interfaces
{
    public interface IAsignaturasRepository
    {
        Task<IEnumerable<Asignatura>> GetAllAsync();
        Task<Asignatura?> GetByIdAsync(int id);
        Task<Asignatura> CreateAsync(Asignatura asignatura);
        Task<bool> UpdateAsync(Asignatura asignatura);
        Task<bool> DeleteAsync(int id);
    }
}