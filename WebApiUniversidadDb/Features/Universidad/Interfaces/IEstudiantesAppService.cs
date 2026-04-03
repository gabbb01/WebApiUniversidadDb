using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Features.Universidad.Interfaces
{
    public interface IEstudiantesAppService
    {
        Task<List<Estudiante>> ObtenerEstudiante();
        Task<ApiResponse<Estudiante>> AgregarEstudiante(Estudiante estudiante);
        Task<ApiResponse<Estudiante>> ActualizarEstudiante(Estudiante estudiante);
        Task<Estudiante?> ObtenerEstudianteId(int id);
        Task InactivarEstudiante(int id);
    }
}
