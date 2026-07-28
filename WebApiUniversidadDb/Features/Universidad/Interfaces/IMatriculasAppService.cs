using WebApiUniversidadDb.Commons.Imperative;
using WebApiUniversidadDb.Commons.Functional;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Features.Universidad.Interfaces
{
    public interface IMatriculasAppService
    {
        Task<List<Matricula>> ObtenerMatriculas();
        Task<Matricula?> ObtenerMatriculaId(int id);
        Task<List<Matricula>> ObtenerMatriculasPorEstudiante(int estudianteId);
        Task<List<Matricula>> ObtenerPromediosPorEstudiante();
        Task AgregarMatricula(Matricula matricula);
        Task ActualizarNota(int matriculaId, decimal nota);
        Task InactivarMatricula(int id);
        Task<CalculadoraPromedios.ResultadoPromedio> ObtenerEstadisticasGenerales();
    }
}
