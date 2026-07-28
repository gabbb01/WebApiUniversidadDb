using WebApiUniversidadDb.Commons.Functional;
using WebApiUniversidadDb.Commons.Models;
using WebApiUniversidadDb.Entities;

namespace WebApiUniversidadDb.Features.Universidad.DomainServices
{
    // PARADIGMA FUNCIONAL: Validaciones mediante composición de funciones puras
    public class MatriculasDomainService
    {
        // FUNCIONAL: Composición de funciones puras para validar una matrícula
        public ApiResponse<Matricula> AgregarMatricula(Matricula matricula) =>
            ValidationHelper.Validar(matricula,
                m => m.EstudianteId <= 0
                    ? "El EstudianteId debe ser un número válido mayor a 0"
                    : null,
                m => m.AsignaturaId <= 0
                    ? "El AsignaturaId debe ser un número válido mayor a 0"
                    : null,
                m => m.Nota < 0 || m.Nota > 100
                    ? "La nota debe estar entre 0 y 100"
                    : null
            );

        // FUNCIONAL: Función pura de validación de nota
        public ApiResponse<Matricula> ActualizarNota(Matricula matricula) =>
            ValidationHelper.Validar(matricula,
                m => m.Nota < 0 || m.Nota > 100
                    ? "La nota debe estar entre 0 y 100"
                    : null
            );
    }
}
