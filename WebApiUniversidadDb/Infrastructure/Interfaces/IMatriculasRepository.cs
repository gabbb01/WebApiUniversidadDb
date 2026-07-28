using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Infrastructure.Interfaces
{
    public interface IMatriculasRepository
    {
        Task<List<Matricula>> ObtenerMatriculas();
        Task<Matricula?> ObtenerMatriculaId(int id);
        Task<List<Matricula>> ObtenerMatriculasPorEstudiante(int estudianteId);
        Task<List<Matricula>> ObtenerPromediosPorEstudiante();
        Task AgregarMatricula(Matricula matricula);
        Task ActualizarNota(int matriculaId, decimal nota);
        Task InactivarMatricula(int id);
    }
}
