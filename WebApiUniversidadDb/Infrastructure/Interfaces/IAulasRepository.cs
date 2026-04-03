using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Infrastructure.Interfaces
{
    public interface IAulasRepository
    {
        Task<List<Aula>> ObtenerAula();
        Task<Aula?> ObtenerAulaId(int id);
        Task AgregarAula(Aula aula);
        Task ActualizarAula(Aula aula);
        Task InactivarAula (int id);
    }
}