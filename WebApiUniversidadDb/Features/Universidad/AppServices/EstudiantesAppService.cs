using System.Security.Cryptography.Xml;
using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Features.Universidad.DomainServices;
using WebApiUniversidadDb.Features.Universidad.Interfaces;
using WebApiUniversidadDb.Infrastructure.Interfaces;
using WebApiUniversidadDb.Infrastructure.Repository;

namespace WebApiUniversidadDb.Features.Universidad.AppServices
{
    public class EstudiantesAppService : IEstudiantesAppService
    {
        private readonly EstudiantesRepository estudiantesRepository;
        private readonly EstudiantesDomainService estudiantesDomainService;
        public EstudiantesAppService(EstudiantesRepository estudiantesRepository, EstudiantesDomainService estudiantesDomainService)
        {
            this.estudiantesRepository = estudiantesRepository;
            this.estudiantesDomainService = estudiantesDomainService;
        }

        public async Task<ApiResponse<Estudiante>> ActualizarEstudiante(Estudiante estudiante)
        {
            ApiResponse<Estudiante> apiResponseResult =
                estudiantesDomainService.ActualizarEstudiante(estudiante);
            try
            {
                if (apiResponseResult.Success)
                {
                    await estudiantesRepository.ActualizarEstudiante(estudiante);
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

        public async Task<ApiResponse<Estudiante>> AgregarEstudiante(Estudiante estudiante)
        {
            ApiResponse<Estudiante> apiResponseResult =
                 estudiantesDomainService.AgregarEstudiante(estudiante);
            try
            {
                if (apiResponseResult.Success)
                {
                    await estudiantesRepository.AgregarEstudiante(estudiante);
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

        public async Task InactivarEstudiante(int id)
        {
            await estudiantesRepository.InactivarEstudiante(id);
        }

        public async Task<List<Estudiante>> ObtenerEstudiante()
        {
            return await estudiantesRepository.ObtenerEstudiantes();
        }

        public async Task<Estudiante?> ObtenerEstudianteId(int id)
        {
            return await estudiantesRepository.ObtenerEstudianteId(id);
        }
    }
}
