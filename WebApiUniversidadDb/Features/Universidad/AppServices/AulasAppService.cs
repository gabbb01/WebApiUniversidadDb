using System.Security.Cryptography.Xml;
using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Features.Universidad.DomainServices;
using WebApiUniversidadDb.Features.Universidad.Interfaces;
using WebApiUniversidadDb.Infrastructure.Interfaces;
using WebApiUniversidadDb.Infrastructure.Repository;

namespace WebApiUniversidadDb.Features.Universidad.AppServices
{
    public class AulasAppService : IAulasAppService
    {
        private readonly IAulasRepository aulasRepository;
        private readonly AulasDomainService aulasDomainService;
        public AulasAppService(IAulasRepository aulasRepository, AulasDomainService aulasDomainService)
        {
            this.aulasRepository = aulasRepository;
            this.aulasDomainService = aulasDomainService;
        }

        public async Task<ApiResponse<Aula>> ActualizarAula(Aula aula)
        {
            ApiResponse<Aula> apiResponseResult =
                aulasDomainService.ActualizarAula(aula);
            try
            {
                if (apiResponseResult.Success)
                {
                    await aulasRepository.ActualizarAula(aula);
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

        public async Task<ApiResponse<Aula>> AgregarAula(Aula aula)
        {
            ApiResponse<Aula> apiResponseResult =
                 aulasDomainService.AgregarAula(aula);
            try
            {
                if (apiResponseResult.Success)
                {
                    await aulasRepository.AgregarAula(aula);
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

        public async Task InactivarAula(int id)
        {
            await aulasRepository.InactivarAula(id);
        }

        public async Task<List<Aula>> ObtenerAula()
        {
            return await aulasRepository.ObtenerAula();
        }

        public async Task<Aula?> ObtenerAulaId(int id)
        {
            return await aulasRepository.ObtenerAulaId(id);
        }
    }
}
