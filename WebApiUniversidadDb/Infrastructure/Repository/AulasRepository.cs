using Microsoft.Data.SqlClient;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Infrastructure.Repository
{
    // PARADIGMA IMPERATIVO: Control de flujo explícito paso a paso con ADO.NET puro
    public class AulasRepository : IAulasRepository
    {
        private readonly string connectionString;

        public AulasRepository(IConfiguration configuration)
        {
            // Paso imperativo: obtener el connection string de la configuración
            connectionString = configuration.GetConnectionString("DbUniversidadConnectionString")!;
        }

        public async Task<List<Aula>> ObtenerAula()
        {
            // IMPERATIVO: Construir lista mutable paso a paso
            List<Aula> aulas = new List<Aula>();

            // Paso 1: Crear y abrir conexión explícitamente
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // Paso 2: Crear comando SQL
            using SqlCommand command = new SqlCommand(
                "SELECT AulaId, CodigoAula, Capacidad, Activo, FechaCreacion FROM Aulas",
                connection);

            // Paso 3: Ejecutar y obtener reader
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            // Paso 4: Iterar fila por fila con while loop (imperativo)
            while (await reader.ReadAsync())
            {
                // Paso 5: Construir objeto campo por campo (estado mutable)
                Aula aula = new Aula();
                aula.AulaId = reader.GetInt32(0);
                aula.CodigoAula = reader.IsDBNull(1) ? null : reader.GetString(1);
                aula.Capacidad = reader.GetInt32(2);
                aula.Activo = reader.GetBoolean(3);
                aula.FechaCreacion = reader.GetDateTime(4);

                // Paso 6: Mutar la lista
                aulas.Add(aula);
            }

            return aulas;
        }

        public async Task<Aula?> ObtenerAulaId(int id)
        {
            // IMPERATIVO: Variable mutable + condicional explícito
            Aula? aula = null;

            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using SqlCommand command = new SqlCommand(
                "SELECT AulaId, CodigoAula, Capacidad, Activo, FechaCreacion FROM Aulas WHERE AulaId = @Id",
                connection);

            command.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                aula = new Aula();
                aula.AulaId = reader.GetInt32(0);
                aula.CodigoAula = reader.IsDBNull(1) ? null : reader.GetString(1);
                aula.Capacidad = reader.GetInt32(2);
                aula.Activo = reader.GetBoolean(3);
                aula.FechaCreacion = reader.GetDateTime(4);
            }

            return aula ?? new Aula();
        }

        public async Task AgregarAula(Aula aula)
        {
            // IMPERATIVO: Secuencia explícita paso a paso
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string sql = @"INSERT INTO Aulas (CodigoAula, Capacidad, Activo, FechaCreacion)
                           VALUES (@CodigoAula, @Capacidad, @Activo, @FechaCreacion)";

            using SqlCommand command = new SqlCommand(sql, connection);

            // Agregar parámetros uno por uno
            command.Parameters.AddWithValue("@CodigoAula", (object?)aula.CodigoAula ?? DBNull.Value);
            command.Parameters.AddWithValue("@Capacidad", aula.Capacidad);
            command.Parameters.AddWithValue("@Activo", aula.Activo);
            command.Parameters.AddWithValue("@FechaCreacion", aula.FechaCreacion);

            await command.ExecuteNonQueryAsync();
        }

        public async Task ActualizarAula(Aula aula)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string sql = @"UPDATE Aulas 
                           SET CodigoAula = @CodigoAula, 
                               Capacidad = @Capacidad, 
                               Activo = @Activo 
                           WHERE AulaId = @AulaId";

            using SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CodigoAula", (object?)aula.CodigoAula ?? DBNull.Value);
            command.Parameters.AddWithValue("@Capacidad", aula.Capacidad);
            command.Parameters.AddWithValue("@Activo", aula.Activo);
            command.Parameters.AddWithValue("@AulaId", aula.AulaId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task InactivarAula(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using SqlCommand command = new SqlCommand(
                "UPDATE Aulas SET Activo = 0 WHERE AulaId = @Id",
                connection);

            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}