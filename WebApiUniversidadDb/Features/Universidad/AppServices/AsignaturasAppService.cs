using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Features.Universidad.DomainServices;
using WebApiUniversidadDb.Features.Universidad.Interfaces;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Features.Universidad.AppServices
{
    public class AsignaturasAppService : IAsignaturasAppService
    {
        private readonly IAsignaturasRepository asignaturasRepository;
        private readonly AsignaturasDomainService asignaturasDomainService;
        public AsignaturasAppService(IAsignaturasRepository asignaturasRepository, AsignaturasDomainService asignaturasDomainService)
        {
            this.asignaturasRepository = asignaturasRepository;
            this.asignaturasDomainService = asignaturasDomainService;
        }
        public async Task<ApiResponse<Asignatura>> ActualizarAsignatura(Asignatura asignatura)
        {
            ApiResponse<Asignatura> apiResponseResult =
                asignaturasDomainService.ActualizarAsignatura(asignatura);
            try
            {
                if (apiResponseResult.Success)
                {
                    await asignaturasRepository.ActualizarAsignatura(asignatura);
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

        public async Task<ApiResponse<Asignatura>> AgregarAsignatura(Asignatura asignatura)
        {
            ApiResponse<Asignatura> apiResponseResult =
                 asignaturasDomainService.AgregarAsignatura(asignatura);
            try
            {
                if (apiResponseResult.Success)
                {
                    await asignaturasRepository.AgregarAsignatura(asignatura);
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

        public async Task InactivarAsignatura(int id)
        {
            await asignaturasRepository.InactivarAsignatura(id);
        }

        public async Task<List<Asignatura>> ObtenerAsignatura()
        {
            return await asignaturasRepository.ObtenerAsignaturas();
        }

        public async Task<Asignatura?> ObtenerAsignaturaId(int id)
        {
            return await asignaturasRepository.ObtenerAsignaturaId(id);
        }
    }
}
