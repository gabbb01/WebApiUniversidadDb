using WebApiUniversidadDb.Commons.Functional;
using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Features.Universidad.DomainServices
{
    public class AulasDomainService
    {
        public AulasDomainService()
        {
            
        }

        // PARADIGMA FUNCIONAL: Composición de funciones puras como reglas de validación
        // Expression-bodied member — retorno directo de la composición funcional
        public ApiResponse<Aula> AgregarAula(Aula aula) =>
            ValidationHelper.Validar(aula,
                a => ValidationHelper.NoVacio("código del aula")(a.CodigoAula),
                a => ValidationHelper.NoNegativo("capacidad")(a.Capacidad)
            );

        // FUNCIONAL: Misma composición de funciones puras para actualización
        public ApiResponse<Aula> ActualizarAula(Aula aula) =>
            ValidationHelper.Validar(aula,
                a => ValidationHelper.NoVacio("código del aula")(a.CodigoAula),
                a => ValidationHelper.NoNegativo("capacidad")(a.Capacidad)
            );
    }
}
