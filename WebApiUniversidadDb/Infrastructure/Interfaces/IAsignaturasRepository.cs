using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Infrastructure.Interfaces
{
    public interface IAsignaturasRepository
    {
        Task<List<Asignatura>> ObtenerAsignaturas();
        Task<Asignatura?> ObtenerAsignaturaId(int id);
        Task AgregarAsignatura(Asignatura asignatura);
        Task ActualizarAsignatura(Asignatura asignatura);
        Task InactivarAsignatura(int id);
    }
}