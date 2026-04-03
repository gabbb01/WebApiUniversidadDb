using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Features.Universidad.Interfaces
{
    public interface IAulasAppService
    {
        Task<List<Aula>> ObtenerAula();
        Task<ApiResponse<Aula>> AgregarAula(Aula aula);
        Task<ApiResponse<Aula>> ActualizarAula(Aula aula);
        Task<Aula?> ObtenerAulaId(int id);
        Task InactivarAula(int id);
    }
}
