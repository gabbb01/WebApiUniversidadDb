using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Features.Universidad.DomainServices
{
    public class AsignaturasDomainService
    {
        public AsignaturasDomainService()
        {
            
        }
        public ApiResponse<Asignatura> AgregarAsignatura(Asignatura asignatura)
        {
            ApiResponse<Asignatura> apiResponse = new ApiResponse<Asignatura>();
            apiResponse.Success = true;
            if (string.IsNullOrEmpty(asignatura.Nombre))
            {
                apiResponse.Success = false;
                apiResponse.Message = "El nombre de la asignatura no puede estar vacio";
            }
            if (int.IsNegative(asignatura.Creditos))
            {
                apiResponse.Success = false;
                apiResponse.Message = "El numero de creditos no puede ser negativo";
            }
            if (int.IsNegative(asignatura.ProfesorId) || int.IsNegative(asignatura.AulaId))
            {
                apiResponse.Success = false;
                apiResponse.Message = "AulaId o ProfesorId no puede ser negativo";
            }

            apiResponse.Data = asignatura;
            return apiResponse;

        }
        public ApiResponse<Asignatura> ActualizarAsignatura(Asignatura asignatura)
        {
            ApiResponse<Asignatura> apiResponse = new ApiResponse<Asignatura>();
            apiResponse.Success = true;
            if (string.IsNullOrEmpty(asignatura.Nombre))
            {
                apiResponse.Success = false;
                apiResponse.Message = "El nombre de la asignatura no puede estar vacio";
            }
            if (int.IsNegative(asignatura.Creditos))
            {
                apiResponse.Success = false;
                apiResponse.Message = "El numero de creditos no puede ser negativo";
            }
            if (int.IsNegative(asignatura.ProfesorId) || int.IsNegative(asignatura.AulaId))
            {
                apiResponse.Success = false;
                apiResponse.Message = "AulaId o ProfesorId no puede ser negativo";
            }
            apiResponse.Data = asignatura;
            return apiResponse;
        }
    }
}
