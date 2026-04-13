using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Features.Universidad.DomainServices
{
    public class AulasDomainService
    {
        public AulasDomainService()
        {
            
        }
        public ApiResponse<Aula> AgregarAula(Aula aula)
        {
            ApiResponse<Aula> apiResponse = new ApiResponse<Aula>();
            apiResponse.Success = true;
            if (string.IsNullOrEmpty(aula.CodigoAula))
            {
                apiResponse.Success = false;
                apiResponse.Message = "El codigo del aula no puede estar vacio";
            }
            if (int.IsNegative(aula.Capacidad))
            {
                apiResponse.Success = false;
                apiResponse.Message = "La capacidad no puede ser negativa";
            }
            
            apiResponse.Data = aula;
            return apiResponse;

        }
        public ApiResponse<Aula> ActualizarAula(Aula aula)
        {
            ApiResponse<Aula> apiResponse = new ApiResponse<Aula>();
            apiResponse.Success = true;
            if (string.IsNullOrEmpty(aula.CodigoAula))
            {
                apiResponse.Success = false;
                apiResponse.Message = "El codigo del aula no puede estar vacio";
            }
            if (int.IsNegative(aula.Capacidad))
            {
                apiResponse.Success = false;
                apiResponse.Message = "La capacidad no puede ser negativa";
            }

            apiResponse.Data = aula;
            return apiResponse;
        }
    }
}
