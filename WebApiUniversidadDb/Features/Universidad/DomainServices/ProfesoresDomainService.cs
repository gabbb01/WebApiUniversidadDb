using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Features.Universidad.DomainServices
{
    public class ProfesoresDomainService
    {
        public ProfesoresDomainService()
        {
            
        }
        public ApiResponse<Profesor> AgregarProfesor(Profesor profesor)
        {
            ApiResponse<Profesor> apiResponse = new ApiResponse<Profesor>();
            apiResponse.Success = true;
            if (string.IsNullOrEmpty(profesor.Nombre) || string.IsNullOrEmpty(profesor.Apellido))
            {
                apiResponse.Success = false;
                apiResponse.Message = "El nombre del profesor no puede estar vacio";
            }
            if (string.IsNullOrEmpty(profesor.Especialidad))
            {

                apiResponse.Success = false;
                apiResponse.Message = "La especialidad no puede estar vacia";
            }

            apiResponse.Data = profesor;
            return apiResponse;

        }
        public ApiResponse<Profesor> ActualizarProfesor(Profesor profesor)
        {
            ApiResponse<Profesor> apiResponse = new ApiResponse<Profesor>();
            apiResponse.Success = true;
            if (string.IsNullOrEmpty(profesor.Nombre) || string.IsNullOrEmpty(profesor.Apellido))
            {
                apiResponse.Success = false;
                apiResponse.Message = "El nombre del profesor no puede estar vacio";
            }
            if (string.IsNullOrEmpty(profesor.Especialidad))
            {

                apiResponse.Success = false;
                apiResponse.Message = "La especialidad no puede estar vacia";
            }

            apiResponse.Data = profesor;
            return apiResponse;
        }
    }
}
