using System.Security.Cryptography.Xml;
using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Features.Universidad.DomainServices;
using WebApiUniversidadDb.Features.Universidad.Interfaces;
using WebApiUniversidadDb.Infrastructure.Interfaces;
using WebApiUniversidadDb.Infrastructure.Repository;

namespace WebApiUniversidadDb.Features.Universidad.AppServices
{
    public class ProfesoresAppService : IProfesoresAppService
    {
        private readonly ProfesoresDomainService profesoresDomainService;
        private readonly ProfesoresRepository profesoresRepository;
        public ProfesoresAppService(ProfesoresDomainService profesoresDomainService, ProfesoresRepository profesoresRepository)
        {
            this.profesoresDomainService = profesoresDomainService;
            this.profesoresRepository = profesoresRepository;
        }
        public async Task<ApiResponse<Profesor>> ActualizarProfesor(Profesor profesor)
        {
            ApiResponse<Profesor> apiResponseResult =
                profesoresDomainService.ActualizarProfesor(profesor);
            try
            {
                if (apiResponseResult.Success)
                {
                    await profesoresRepository.ActualizarProfesor(profesor);
                }
                return apiResponseResult;
            }
            catch (Exception ex)
            {
                apiResponseResult.Success = false;
                apiResponseResult.Message = ex.Message;
                return apiResponseResult;
            }
        }

        public async Task<ApiResponse<Profesor>> AgregarProfesor(Profesor profesor)
        {
            ApiResponse<Profesor> apiResponseResult =
                 profesoresDomainService.AgregarProfesor(profesor);
            try
            {
                if (apiResponseResult.Success)
                {
                    await profesoresRepository.AgregarProfesor(profesor);
                }
                return apiResponseResult;
            }
            catch (Exception ex)
            {
                apiResponseResult.Success = false;
                apiResponseResult.Message = ex.Message;
                return apiResponseResult;
            }
        }

        public async Task InactivarProfesor(int id)
        {
            await profesoresRepository.InactivarProfesor(id);
        }

        public async Task<List<Profesor>> ObtenerProfesores()
        {
            return await profesoresRepository.ObtenerProfesores(); 
        }

        public async Task<Profesor?> ObtenerProfesorId(int id)
        {
            return await profesoresRepository.ObtenerProfesorId(id);
        }
    }
}
