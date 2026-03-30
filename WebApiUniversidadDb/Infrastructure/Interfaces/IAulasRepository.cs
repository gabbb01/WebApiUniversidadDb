using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Infrastructure.Interfaces
{
    public interface IAulasRepository
    {
        Task<IEnumerable<Aula>> GetAllAsync();
        Task<Aula?> GetByIdAsync(int id);
        Task<Aula> CreateAsync(Aula aula);
        Task<bool> UpdateAsync(Aula aula);
        Task<bool> DeleteAsync(int id);
    }
}