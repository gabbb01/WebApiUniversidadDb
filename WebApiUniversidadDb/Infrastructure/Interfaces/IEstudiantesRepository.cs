using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Infrastructure.Interfaces
{
    public interface IEstudiantesRepository
    {
        Task<List<Estudiante>> ObtenerEstudiantes();
        Task<Estudiante?> ObtenerEstudianteId(int id);
        Task AgregarEstudiante(Estudiante estudiante);
        Task ActualizarEstudiante(Estudiante estudiante);
        Task InactivarEstudiante(int id);
    }
}