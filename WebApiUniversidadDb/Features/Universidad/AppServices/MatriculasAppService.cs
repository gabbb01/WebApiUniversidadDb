using WebApiUniversidadDb.Commons.Functional;
using WebApiUniversidadDb.Commons.Imperative;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Features.Universidad.DomainServices;
using WebApiUniversidadDb.Features.Universidad.Interfaces;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Features.Universidad.AppServices
{
    // OOP: Clase que orquesta repositorios, servicios de dominio y paradigmas
    public class MatriculasAppService : IMatriculasAppService
    {
        private readonly IMatriculasRepository matriculasRepository;
        private readonly MatriculasDomainService matriculasDomainService;

        // OOP: Constructor con inyección de dependencias
        public MatriculasAppService(
            IMatriculasRepository matriculasRepository,
            MatriculasDomainService matriculasDomainService)
        {
            this.matriculasRepository    = matriculasRepository;
            this.matriculasDomainService = matriculasDomainService;
        }

        public async Task<List<Matricula>> ObtenerMatriculas()
            => await matriculasRepository.ObtenerMatriculas();

        public async Task<Matricula?> ObtenerMatriculaId(int id)
            => await matriculasRepository.ObtenerMatriculaId(id);

        public async Task<List<Matricula>> ObtenerMatriculasPorEstudiante(int estudianteId)
            => await matriculasRepository.ObtenerMatriculasPorEstudiante(estudianteId);

        public async Task<List<Matricula>> ObtenerPromediosPorEstudiante()
            => await matriculasRepository.ObtenerPromediosPorEstudiante();

        public async Task AgregarMatricula(Matricula matricula)
        {
            // FUNCIONAL: Valida con funciones puras antes de persistir
            var resultado = matriculasDomainService.AgregarMatricula(matricula);
            if (resultado.Success)
                await matriculasRepository.AgregarMatricula(matricula);
        }

        public async Task ActualizarNota(int matriculaId, decimal nota)
        {
            var temp = new Matricula { Nota = nota };
            var resultado = matriculasDomainService.ActualizarNota(temp);
            if (resultado.Success)
                await matriculasRepository.ActualizarNota(matriculaId, nota);
        }

        public async Task InactivarMatricula(int id)
            => await matriculasRepository.InactivarMatricula(id);

        // IMPERATIVO + FUNCIONAL: Obtiene notas de BD y aplica ambos paradigmas
        public async Task<CalculadoraPromedios.ResultadoPromedio> ObtenerEstadisticasGenerales()
        {
            // Obtiene todas las matrículas (DECLARATIVO via SQL)
            var matriculas = await matriculasRepository.ObtenerMatriculas();

            // FUNCIONAL: Map — transforma matrículas a lista de notas (double)
            var notas = matriculas
                .Select(m => (double)m.Nota)   // map: Matricula → double
                .ToList();

            // IMPERATIVO: Calcula estadísticas con variables y ciclos
            return CalculadoraPromedios.CalcularEstadisticas(notas);
        }
    }
}
