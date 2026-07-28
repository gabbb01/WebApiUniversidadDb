using Microsoft.Data.SqlClient;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Infrastructure.Repository
{
    // PARADIGMA IMPERATIVO: Control de flujo explícito paso a paso con ADO.NET puro
    public class EstudiantesRepository : IEstudiantesRepository
    {
        private readonly string connectionString;

        public EstudiantesRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DbUniversidadConnectionString")!;
        }

        public async Task<List<Estudiante>> ObtenerEstudiantes()
        {
            // IMPERATIVO: Construir lista mutable paso a paso
            List<Estudiante> estudiantes = new List<Estudiante>();

            // Paso 1: Crear y abrir conexión
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // Paso 2: Crear comando
            using SqlCommand command = new SqlCommand(
                "SELECT EstudianteId, NumeroCuenta, Nombre, Apellido, Correo, Activo, FechaCreacion FROM Estudiantes",
                connection);

            // Paso 3: Ejecutar y obtener reader
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            // Paso 4: Iterar fila por fila con while loop
            while (await reader.ReadAsync())
            {
                // Paso 5: Construir objeto campo por campo
                Estudiante estudiante = new Estudiante();
                estudiante.EstudianteId = reader.GetInt32(0);
                estudiante.NumeroCuenta = reader.IsDBNull(1) ? null : reader.GetString(1);
                estudiante.Nombre = reader.IsDBNull(2) ? null : reader.GetString(2);
                estudiante.Apellido = reader.IsDBNull(3) ? null : reader.GetString(3);
                estudiante.Correo = reader.IsDBNull(4) ? null : reader.GetString(4);
                estudiante.Activo = reader.GetBoolean(5);
                estudiante.FechaCreacion = reader.GetDateTime(6);

                // Paso 6: Mutar la lista
                estudiantes.Add(estudiante);
            }

            return estudiantes;
        }

        public async Task<Estudiante?> ObtenerEstudianteId(int id)
        {
            Estudiante? estudiante = null;

            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using SqlCommand command = new SqlCommand(
                "SELECT EstudianteId, NumeroCuenta, Nombre, Apellido, Correo, Activo, FechaCreacion FROM Estudiantes WHERE EstudianteId = @Id",
                connection);

            command.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                estudiante = new Estudiante();
                estudiante.EstudianteId = reader.GetInt32(0);
                estudiante.NumeroCuenta = reader.IsDBNull(1) ? null : reader.GetString(1);
                estudiante.Nombre = reader.IsDBNull(2) ? null : reader.GetString(2);
                estudiante.Apellido = reader.IsDBNull(3) ? null : reader.GetString(3);
                estudiante.Correo = reader.IsDBNull(4) ? null : reader.GetString(4);
                estudiante.Activo = reader.GetBoolean(5);
                estudiante.FechaCreacion = reader.GetDateTime(6);
            }

            return estudiante ?? new Estudiante();
        }

        public async Task AgregarEstudiante(Estudiante estudiante)
        {
            // IMPERATIVO: Secuencia explícita de pasos
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string sql = @"INSERT INTO Estudiantes (NumeroCuenta, Nombre, Apellido, Correo, Activo, FechaCreacion)
                           VALUES (@NumeroCuenta, @Nombre, @Apellido, @Correo, @Activo, @FechaCreacion)";

            using SqlCommand command = new SqlCommand(sql, connection);

            // Agregar parámetros uno por uno (imperativo)
            command.Parameters.AddWithValue("@NumeroCuenta", (object?)estudiante.NumeroCuenta ?? DBNull.Value);
            command.Parameters.AddWithValue("@Nombre", (object?)estudiante.Nombre ?? DBNull.Value);
            command.Parameters.AddWithValue("@Apellido", (object?)estudiante.Apellido ?? DBNull.Value);
            command.Parameters.AddWithValue("@Correo", (object?)estudiante.Correo ?? DBNull.Value);
            command.Parameters.AddWithValue("@Activo", estudiante.Activo);
            command.Parameters.AddWithValue("@FechaCreacion", estudiante.FechaCreacion);

            await command.ExecuteNonQueryAsync();
        }

        public async Task ActualizarEstudiante(Estudiante estudiante)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string sql = @"UPDATE Estudiantes 
                           SET NumeroCuenta = @NumeroCuenta, 
                               Nombre = @Nombre, 
                               Apellido = @Apellido, 
                               Correo = @Correo, 
                               Activo = @Activo 
                           WHERE EstudianteId = @EstudianteId";

            using SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@NumeroCuenta", (object?)estudiante.NumeroCuenta ?? DBNull.Value);
            command.Parameters.AddWithValue("@Nombre", (object?)estudiante.Nombre ?? DBNull.Value);
            command.Parameters.AddWithValue("@Apellido", (object?)estudiante.Apellido ?? DBNull.Value);
            command.Parameters.AddWithValue("@Correo", (object?)estudiante.Correo ?? DBNull.Value);
            command.Parameters.AddWithValue("@Activo", estudiante.Activo);
            command.Parameters.AddWithValue("@EstudianteId", estudiante.EstudianteId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task InactivarEstudiante(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using SqlCommand command = new SqlCommand(
                "UPDATE Estudiantes SET Activo = 0 WHERE EstudianteId = @Id",
                connection);

            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}