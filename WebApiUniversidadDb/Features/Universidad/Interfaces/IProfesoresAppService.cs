using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Features.Universidad.Interfaces
{
    public interface IProfesoresAppService
    {
        Task<List<Profesor>> ObtenerProfesores();
        Task<ApiResponse<Profesor>> AgregarProfesor(Profesor profesor);
        Task<ApiResponse<Profesor>> ActualizarProfesor(Profesor profesor);
        Task<Profesor?> ObtenerProfesorId(int id);
        Task InactivarProfesor(int id);
    }
}
