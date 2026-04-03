using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Features.Universidad.DomainServices
{
    public class EstudiantesDomainService
    {
        public EstudiantesDomainService()
        {
            
        }
        public ApiResponse<Estudiante> AgregarEstudiante(Estudiante estudiante)
        {
            ApiResponse<Estudiante> apiResponse = new ApiResponse<Estudiante>();
            apiResponse.Success = true;
            if (string.IsNullOrEmpty(estudiante.NumeroCuenta) || estudiante.NumeroCuenta.Length != 11)
            {
                apiResponse.Success = false;
                apiResponse.Message = "El numero de cuenta no puede estar vacio y deben ser 11 caracteres";
            }
            if (string.IsNullOrEmpty(estudiante.Nombre) || string.IsNullOrEmpty(estudiante.Apellido))
            {
                apiResponse.Success = false;
                apiResponse.Message = "El nombre no puede estar vacio";
            }
            if (string.IsNullOrEmpty(estudiante.Correo))
            {
                apiResponse.Success = false;
                apiResponse.Message = "El correo no puede estar vacio";
            }

            apiResponse.Data = estudiante;
            return apiResponse;

        }
        public ApiResponse<Estudiante> ActualizarEstudiante(Estudiante estudiante)
        {
            ApiResponse<Estudiante> apiResponse = new ApiResponse<Estudiante>();
            apiResponse.Success = true;
            if (string.IsNullOrEmpty(estudiante.NumeroCuenta) || estudiante.NumeroCuenta.Length != 11)
            {
                apiResponse.Success = false;
                apiResponse.Message = "El numero de cuenta no puede estar vacio y deben ser 11 caracteres";
            }
            if (string.IsNullOrEmpty(estudiante.Nombre) || string.IsNullOrEmpty(estudiante.Apellido))
            {
                apiResponse.Success = false;
                apiResponse.Message = "El nombre no puede estar vacio";
            }
            if (string.IsNullOrEmpty(estudiante.Correo))
            {
                apiResponse.Success = false;
                apiResponse.Message = "El correo no puede estar vacio";
            }

            apiResponse.Data = estudiante;
            return apiResponse;
        }
    }
}
