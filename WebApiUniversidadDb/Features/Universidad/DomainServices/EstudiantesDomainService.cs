using WebApiUniversidadDb.Commons.Functional;
using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Features.Universidad.DomainServices
{
    public class EstudiantesDomainService
    {
        public EstudiantesDomainService()
        {
            
        }

        // PARADIGMA FUNCIONAL: Composición de funciones puras como reglas de validación
        // LongitudExacta es una función de fábrica que retorna un Func<string?, string?>
        // Cada lambda compone la extracción del campo + la función de validación
        public ApiResponse<Estudiante> AgregarEstudiante(Estudiante estudiante) =>
            ValidationHelper.Validar(estudiante,
                e => ValidationHelper.LongitudExacta("número de cuenta", 11)(e.NumeroCuenta),
                e => ValidationHelper.NoVacio("nombre")(e.Nombre),
                e => ValidationHelper.NoVacio("apellido")(e.Apellido),
                e => ValidationHelper.NoVacio("correo")(e.Correo)
            );

        // FUNCIONAL: Misma composición de funciones puras para actualización
        public ApiResponse<Estudiante> ActualizarEstudiante(Estudiante estudiante) =>
            ValidationHelper.Validar(estudiante,
                e => ValidationHelper.LongitudExacta("número de cuenta", 11)(e.NumeroCuenta),
                e => ValidationHelper.NoVacio("nombre")(e.Nombre),
                e => ValidationHelper.NoVacio("apellido")(e.Apellido),
                e => ValidationHelper.NoVacio("correo")(e.Correo)
            );
    }
}
