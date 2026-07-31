namespace WebApiUniversidadDb.Commons.Functional
{
    // PARADIGMA FUNCIONAL:
    // Funciones puras, inmutabilidad, lambda expressions, map, filter, reduce y recursividad.
    // Las funciones NO modifican estado externo — solo transforman y retornan nuevos valores.
    public static class FunctionalHelper
    {
        // ─────────────────────────────────────────────────────────────
        // MAP: Transformar cada elemento a otro tipo (Select = map en C#)
        // Función pura: misma entrada → siempre misma salida, sin efectos secundarios
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// MAP: Transforma cada nota numérica en su calificación de letra.
        /// Usa expresión lambda con Select (equivalente a map funcional).
        /// </summary>
        public static IEnumerable<string> MapNotasALetras(IEnumerable<double> notas) =>
            notas.Select(nota =>            // lambda: double → string
                nota >= 90 ? "A (Excelente)" :
                nota >= 80 ? "B (Muy Bueno)" :
                nota >= 70 ? "C (Bueno)" :
                nota >= 65 ? "D (Suficiente)" : "F (Reprobado)");

        /// <summary>
        /// MAP: Transforma cada matrícula a un string descriptivo.
        /// Función pura de alto orden: recibe una colección, retorna nueva colección transformada.
        /// </summary>
        public static IEnumerable<string> MapMatriculasADescripciones<T>(
            IEnumerable<T> items, Func<T, string> transformador) =>
            items.Select(transformador);  // higher-order function: recibe función como parámetro

        // ─────────────────────────────────────────────────────────────
        // FILTER: Filtrar elementos según una condición (Where = filter en C#)
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// FILTER: Retorna solo las notas aprobadas (>= 60).
        /// Función pura — no modifica la lista original, retorna nueva colección.
        /// </summary>
        public static IEnumerable<double> FiltrarAprobadas(IEnumerable<double> notas) =>
            notas.Where(nota => nota >= 65);  // lambda predicado puro

        /// <summary>
        /// FILTER: Retorna solo las notas reprobadas (< 60).
        /// </summary>
        public static IEnumerable<double> FiltrarReprobadas(IEnumerable<double> notas) =>
            notas.Where(nota => nota < 65);

        /// <summary>
        /// FILTER: Función genérica de filtrado de alto orden.
        /// Recibe cualquier predicado como función (Func&lt;T, bool&gt;).
        /// </summary>
        public static IEnumerable<T> Filtrar<T>(IEnumerable<T> items, Func<T, bool> predicado) =>
            items.Where(predicado);  // higher-order function

        // ─────────────────────────────────────────────────────────────
        // REDUCE: Agregar / acumular una colección en un solo valor (Aggregate = reduce en C#)
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// REDUCE: Calcula la suma total de las notas acumulando con Aggregate.
        /// Función pura — sin efectos secundarios, retorna un nuevo valor escalar.
        /// </summary>
        public static double ReducirSuma(IEnumerable<double> notas) =>
            notas.Aggregate(0.0, (acumulado, nota) => acumulado + nota);  // reduce

        /// <summary>
        /// REDUCE: Concatena todas las letras de notas en un resumen de texto.
        /// Demuestra reduce sobre strings.
        /// </summary>
        public static string ReducirAResumen(IEnumerable<double> notas) =>
            MapNotasALetras(notas)
                .Aggregate("Resumen: ", (acumulado, letra) => acumulado + letra + " | ");

        // ─────────────────────────────────────────────────────────────
        // RECURSIVIDAD: La función se llama a sí misma para resolver subproblemas
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// RECURSIVIDAD: Calcula el factorial de n (usado para combinaciones de créditos).
        /// Caso base: n &lt;= 1 → 1. Caso recursivo: n * Factorial(n-1).
        /// </summary>
        public static long FactorialCreditos(int n)
        {
            // Caso base — detiene la recursión
            if (n <= 1) return 1;

            // Caso recursivo — se llama a sí misma con n-1
            return n * FactorialCreditos(n - 1);
        }

        /// <summary>
        /// RECURSIVIDAD: Cuenta cuántas notas son aprobadas usando recursión.
        /// Demuestra recursión sobre listas (alternativa funcional al ciclo for).
        /// </summary>
        public static int ContarAprobadasRecursivo(List<double> notas, int indice = 0)
        {
            // Caso base: llegamos al final de la lista
            if (indice >= notas.Count) return 0;

            // Caso recursivo: ¿aprobó la nota actual? + resultado del resto de la lista
            int esAprobado = notas[indice] >= 60 ? 1 : 0;
            return esAprobado + ContarAprobadasRecursivo(notas, indice + 1);
        }

        // ─────────────────────────────────────────────────────────────
        // COMPOSICIÓN DE FUNCIONES: Combinar funciones puras en pipeline
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// COMPOSICIÓN: Pipeline funcional — filtra aprobadas, las mapea a letras, las reduce a resumen.
        /// Demuestra composición de funciones puras (sin variables intermedias mutables).
        /// </summary>
        public static string PipelineAnalisisNotas(IEnumerable<double> notas) =>
            ReducirAResumen(FiltrarAprobadas(notas));

        /// <summary>
        /// INMUTABILIDAD: Retorna un nuevo IEnumerable con una nota agregada
        /// sin modificar la colección original.
        /// </summary>
        public static IEnumerable<double> AgregarNota(IEnumerable<double> notas, double nuevaNota) =>
            notas.Append(nuevaNota);  // retorna nueva secuencia, no muta la original
    }
}
