using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Infrastructure.Interfaces
{
    public interface IProfesoresRepository
    {
        Task<List<Profesor>> ObtenerProfesores();
        Task<Profesor?> ObtenerProfesorId(int id);
        Task AgregarProfesor(Profesor profesor);
        Task ActualizarProfesor(Profesor profesor);
        Task InactivarProfesor(int id);
    }
}