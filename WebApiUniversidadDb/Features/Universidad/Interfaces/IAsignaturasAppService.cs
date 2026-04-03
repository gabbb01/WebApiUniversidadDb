using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Features.Universidad.Interfaces
{
    public interface IAsignaturasAppService
    {
        Task<List<Asignatura>> ObtenerAsignatura();
        Task<ApiResponse<Asignatura>> AgregarAsignatura(Asignatura asignatura);
        Task<ApiResponse<Asignatura>> ActualizarAsignatura(Asignatura asignatura);
        Task<Asignatura?> ObtenerAsignaturaId(int id);
        Task InactivarAsignatura(int id);
    }
}
