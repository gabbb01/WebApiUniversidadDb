using Microsoft.Data.SqlClient;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Infrastructure.Repository
{
    // PARADIGMA IMPERATIVO: Control de flujo explícito paso a paso con ADO.NET puro
    public class ProfesoresRepository : IProfesoresRepository
    {
        private readonly string connectionString;

        public ProfesoresRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DbUniversidadConnectionString")!;
        }

        public async Task<List<Profesor>> ObtenerProfesores()
        {
            // IMPERATIVO: Construir lista mutable paso a paso
            List<Profesor> profesores = new List<Profesor>();

            // Paso 1: Crear y abrir conexión
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // Paso 2: Crear comando
            using SqlCommand command = new SqlCommand(
                "SELECT ProfesorId, Nombre, Apellido, Especialidad, Activo, FechaCreacion FROM Profesores",
                connection);

            // Paso 3: Ejecutar y obtener reader
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            // Paso 4: Iterar fila por fila con while loop
            while (await reader.ReadAsync())
            {
                // Paso 5: Construir objeto campo por campo
                Profesor profesor = new Profesor();
                profesor.ProfesorId = reader.GetInt32(0);
                profesor.Nombre = reader.IsDBNull(1) ? null : reader.GetString(1);
                profesor.Apellido = reader.IsDBNull(2) ? null : reader.GetString(2);
                profesor.Especialidad = reader.IsDBNull(3) ? null : reader.GetString(3);
                profesor.Activo = reader.GetBoolean(4);
                profesor.FechaCreacion = reader.GetDateTime(5);

                // Paso 6: Mutar la lista
                profesores.Add(profesor);
            }

            return profesores;
        }

        public async Task<Profesor?> ObtenerProfesorId(int id)
        {
            Profesor? profesor = null;

            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using SqlCommand command = new SqlCommand(
                "SELECT ProfesorId, Nombre, Apellido, Especialidad, Activo, FechaCreacion FROM Profesores WHERE ProfesorId = @Id",
                connection);

            command.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                profesor = new Profesor();
                profesor.ProfesorId = reader.GetInt32(0);
                profesor.Nombre = reader.IsDBNull(1) ? null : reader.GetString(1);
                profesor.Apellido = reader.IsDBNull(2) ? null : reader.GetString(2);
                profesor.Especialidad = reader.IsDBNull(3) ? null : reader.GetString(3);
                profesor.Activo = reader.GetBoolean(4);
                profesor.FechaCreacion = reader.GetDateTime(5);
            }

            return profesor ?? new Profesor();
        }

        public async Task AgregarProfesor(Profesor profesor)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string sql = @"INSERT INTO Profesores (Nombre, Apellido, Especialidad, Activo, FechaCreacion)
                           VALUES (@Nombre, @Apellido, @Especialidad, @Activo, @FechaCreacion)";

            using SqlCommand command = new SqlCommand(sql, connection);

            // Agregar parámetros uno por uno (imperativo)
            command.Parameters.AddWithValue("@Nombre", (object?)profesor.Nombre ?? DBNull.Value);
            command.Parameters.AddWithValue("@Apellido", (object?)profesor.Apellido ?? DBNull.Value);
            command.Parameters.AddWithValue("@Especialidad", (object?)profesor.Especialidad ?? DBNull.Value);
            command.Parameters.AddWithValue("@Activo", profesor.Activo);
            command.Parameters.AddWithValue("@FechaCreacion", profesor.FechaCreacion);

            await command.ExecuteNonQueryAsync();
        }

        public async Task ActualizarProfesor(Profesor profesor)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string sql = @"UPDATE Profesores 
                           SET Nombre = @Nombre, 
                               Apellido = @Apellido, 
                               Especialidad = @Especialidad, 
                               Activo = @Activo 
                           WHERE ProfesorId = @ProfesorId";

            using SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Nombre", (object?)profesor.Nombre ?? DBNull.Value);
            command.Parameters.AddWithValue("@Apellido", (object?)profesor.Apellido ?? DBNull.Value);
            command.Parameters.AddWithValue("@Especialidad", (object?)profesor.Especialidad ?? DBNull.Value);
            command.Parameters.AddWithValue("@Activo", profesor.Activo);
            command.Parameters.AddWithValue("@ProfesorId", profesor.ProfesorId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task InactivarProfesor(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using SqlCommand command = new SqlCommand(
                "UPDATE Profesores SET Activo = 0 WHERE ProfesorId = @Id",
                connection);

            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}