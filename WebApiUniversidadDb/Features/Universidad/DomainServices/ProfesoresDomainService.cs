using WebApiUniversidadDb.Commons.Functional;
using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Features.Universidad.DomainServices
{
    public class ProfesoresDomainService
    {
        public ProfesoresDomainService()
        {
            
        }

        // PARADIGMA FUNCIONAL: Composición de funciones puras como reglas de validación
        // NoVacio retorna Func<string?, string?> — una función pura reutilizable
        public ApiResponse<Profesor> AgregarProfesor(Profesor profesor) =>
            ValidationHelper.Validar(profesor,
                p => ValidationHelper.NoVacio("nombre del profesor")(p.Nombre),
                p => ValidationHelper.NoVacio("apellido del profesor")(p.Apellido),
                p => ValidationHelper.NoVacio("especialidad")(p.Especialidad)
            );

        // FUNCIONAL: Misma composición de funciones puras para actualización
        public ApiResponse<Profesor> ActualizarProfesor(Profesor profesor) =>
            ValidationHelper.Validar(profesor,
                p => ValidationHelper.NoVacio("nombre del profesor")(p.Nombre),
                p => ValidationHelper.NoVacio("apellido del profesor")(p.Apellido),
                p => ValidationHelper.NoVacio("especialidad")(p.Especialidad)
            );
    }
}
