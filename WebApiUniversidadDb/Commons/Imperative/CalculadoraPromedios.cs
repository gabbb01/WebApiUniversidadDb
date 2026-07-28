namespace WebApiUniversidadDb.Commons.Imperative
{
    // PARADIGMA IMPERATIVO:
    // Uso explícito de variables mutables, ciclos for/while y condicionales if/else.
    // Cada paso modifica el estado de las variables (no es funcional ni declarativo).
    public static class CalculadoraPromedios
    {
        // Resultado mutable de la calculadora
        public class ResultadoPromedio
        {
            public double Promedio { get; set; }
            public double NotaMaxima { get; set; }
            public double NotaMinima { get; set; }
            public int TotalAprobados { get; set; }
            public int TotalReprobados { get; set; }
            public int TotalEstudiantes { get; set; }
            public string Clasificacion { get; set; } = string.Empty;
        }

        /// <summary>
        /// IMPERATIVO: Calcula estadísticas de una lista de notas usando
        /// variables mutables, un ciclo for y condicionales if/else.
        /// </summary>
        public static ResultadoPromedio CalcularEstadisticas(List<double> notas)
        {
            // IMPERATIVO: Declarar variables mutables con estado inicial
            double suma = 0;
            double notaMaxima = 0;
            double notaMinima = 100;
            int aprobados = 0;
            int reprobados = 0;

            // IMPERATIVO: Ciclo for — iteración explícita índice por índice
            for (int i = 0; i < notas.Count; i++)
            {
                double nota = notas[i]; // variable mutable en cada iteración

                // IMPERATIVO: Acumular suma
                suma += nota;

                // IMPERATIVO: Condicional if/else para clasificar cada nota
                if (nota >= 65)
                {
                    aprobados++;
                }
                else
                {
                    reprobados++;
                }

                // IMPERATIVO: Actualizar máximos y mínimos con condicionales
                if (nota > notaMaxima)
                {
                    notaMaxima = nota;
                }

                if (nota < notaMinima)
                {
                    notaMinima = nota;
                }
            }

            // IMPERATIVO: Variable mutable para el promedio
            double promedio = notas.Count > 0 ? suma / notas.Count : 0;

            // IMPERATIVO: Clasificación con cadena de if/else if
            string clasificacion;
            if (promedio >= 90)
            {
                clasificacion = "Excelente";
            }
            else if (promedio >= 80)
            {
                clasificacion = "Muy Bueno";
            }
            else if (promedio >= 70)
            {
                clasificacion = "Bueno";
            }
            else if (promedio >= 65)
            {
                clasificacion = "Suficiente";
            }
            else
            {
                clasificacion = "Insuficiente";
            }

            // Retornar resultado construido imperativo paso a paso
            return new ResultadoPromedio
            {
                Promedio = Math.Round(promedio, 2),
                NotaMaxima = notaMaxima,
                NotaMinima = notas.Count > 0 ? notaMinima : 0,
                TotalAprobados = aprobados,
                TotalReprobados = reprobados,
                TotalEstudiantes = notas.Count,
                Clasificacion = clasificacion
            };
        }

        /// <summary>
        /// IMPERATIVO: Ciclo while — busca la primera nota reprobada.
        /// Demuestra uso de while con condición de salida explícita.
        /// El umbral por defecto es 65 (nota mínima para aprobar).
        /// </summary>
        public static double? PrimeraNota(List<double> notas, double umbral = 65)
        {
            int indice = 0; // variable mutable de control

            // IMPERATIVO: Ciclo while con condición explícita
            while (indice < notas.Count)
            {
                if (notas[indice] < umbral)
                {
                    return notas[indice]; // salida temprana (break implícito)
                }
                indice++; // mutación de variable de control
            }

            return null; // no encontró ninguna por debajo del umbral
        }
    }
}
