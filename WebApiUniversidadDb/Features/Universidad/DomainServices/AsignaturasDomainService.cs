using WebApiUniversidadDb.Commons.Functional;
using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Features.Universidad.DomainServices
{
    public class AsignaturasDomainService
    {
        public AsignaturasDomainService()
        {
            
        }

        // PARADIGMA FUNCIONAL: Composición de funciones puras como reglas de validación
        // Cada lambda es una función pura (Asignatura -> string?) que se pasa como Func<> delegate
        // ValidationHelper.Validar es una higher-order function que compone todas las reglas
        // Expression-bodied member (=>) — retorno directo sin variables intermedias
        public ApiResponse<Asignatura> AgregarAsignatura(Asignatura asignatura) =>
            ValidationHelper.Validar(asignatura,
                a => ValidationHelper.NoVacio("nombre de la asignatura")(a.Nombre),
                a => ValidationHelper.NoNegativo("número de créditos")(a.Creditos),
                a => ValidationHelper.NoNegativo("ProfesorId")(a.ProfesorId),
                a => ValidationHelper.NoNegativo("AulaId")(a.AulaId)
            );

        // FUNCIONAL: Misma composición de funciones puras para actualización
        public ApiResponse<Asignatura> ActualizarAsignatura(Asignatura asignatura) =>
            ValidationHelper.Validar(asignatura,
                a => ValidationHelper.NoVacio("nombre de la asignatura")(a.Nombre),
                a => ValidationHelper.NoNegativo("número de créditos")(a.Creditos),
                a => ValidationHelper.NoNegativo("ProfesorId")(a.ProfesorId),
                a => ValidationHelper.NoNegativo("AulaId")(a.AulaId)
            );
    }
}
