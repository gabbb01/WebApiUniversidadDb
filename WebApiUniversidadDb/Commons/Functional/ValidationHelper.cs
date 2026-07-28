namespace WebApiUniversidadDb.Commons.Functional
{
    using WebApiUniversidadDb.Commons.Models;

    // PARADIGMA FUNCIONAL: Clase estática con funciones puras y de orden superior
    public static class ValidationHelper
    {
        /// <summary>
        /// Higher-order function: recibe datos y una lista de funciones de validación (Func delegates),
        /// las compone aplicándolas secuencialmente con LINQ funcional (.Select + .Where)
        /// y retorna un ApiResponse inmutable con el resultado.
        /// </summary>
        public static ApiResponse<T> Validar<T>(T data, params Func<T, string?>[] reglas)
        {
            // FUNCIONAL: Transformación con LINQ funcional (Select + Where + ToList)
            // Cada "regla" es una función pura: T -> string? (null = válido, string = error)
            var errores = reglas
                .Select(regla => regla(data))       // Aplica cada función pura al dato
                .Where(error => error != null)       // Filtra solo los errores (no nulos)
                .ToList();                           // Materializa la colección

            // Retorno de expresión sin mutar estado externo
            return new ApiResponse<T>
            {
                Success = errores.Count == 0,
                Message = errores.FirstOrDefault() ?? string.Empty,
                Data = data
            };
        }

        /// <summary>
        /// Retorna una función pura que valida que un string no esté vacío.
        /// Func&lt;string?, string?&gt; — recibe valor, retorna mensaje de error o null.
        /// </summary>
        public static Func<string?, string?> NoVacio(string campo) =>
            valor => string.IsNullOrEmpty(valor)
                ? $"El {campo} no puede estar vacío"
                : null;

        /// <summary>
        /// Retorna una función pura que valida que un entero no sea negativo.
        /// Func&lt;int, string?&gt; — recibe valor, retorna mensaje de error o null.
        /// </summary>
        public static Func<int, string?> NoNegativo(string campo) =>
            valor => int.IsNegative(valor)
                ? $"El {campo} no puede ser negativo"
                : null;

        /// <summary>
        /// Retorna una función pura que valida longitud exacta de un string.
        /// Combina validación de vacío + longitud en una sola función compuesta.
        /// </summary>
        public static Func<string?, string?> LongitudExacta(string campo, int longitud) =>
            valor => string.IsNullOrEmpty(valor) || valor.Length != longitud
                ? $"El {campo} no puede estar vacío y debe tener {longitud} caracteres"
                : null;
    }
}
