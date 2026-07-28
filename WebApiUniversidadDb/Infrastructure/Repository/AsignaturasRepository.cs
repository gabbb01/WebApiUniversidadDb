using Microsoft.Data.SqlClient;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Infrastructure.Repository
{
    // PARADIGMA IMPERATIVO: Control de flujo explícito paso a paso con ADO.NET puro
    public class AsignaturasRepository : IAsignaturasRepository
    {
        private readonly string connectionString;

        public AsignaturasRepository(IConfiguration configuration)
        {
            // Paso imperativo: obtener el connection string de la configuración
            connectionString = configuration.GetConnectionString("DbUniversidadConnectionString")!;
        }

        public async Task<List<Asignatura>> ObtenerAsignaturas()
        {
            // IMPERATIVO: Construir lista mutable paso a paso
            List<Asignatura> asignaturas = new List<Asignatura>();

            // Paso 1: Crear y abrir conexión explícitamente
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // Paso 2: Crear comando SQL
            using SqlCommand command = new SqlCommand(
                "SELECT AsignaturaId, Nombre, Creditos, ProfesorId, AulaId, Activo, FechaCreacion FROM Asignaturas",
                connection);

            // Paso 3: Ejecutar y obtener reader
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            // Paso 4: Iterar fila por fila con while loop (imperativo)
            while (await reader.ReadAsync())
            {
                // Paso 5: Construir objeto campo por campo (estado mutable)
                Asignatura asignatura = new Asignatura();
                asignatura.AsignaturaId = reader.GetInt32(0);
                asignatura.Nombre = reader.IsDBNull(1) ? null : reader.GetString(1);
                asignatura.Creditos = reader.GetInt32(2);
                asignatura.ProfesorId = reader.GetInt32(3);
                asignatura.AulaId = reader.GetInt32(4);
                asignatura.Activo = reader.GetBoolean(5);
                asignatura.FechaCreacion = reader.GetDateTime(6);

                // Paso 6: Mutar la lista agregando el elemento
                asignaturas.Add(asignatura);
            }

            // Paso 7: Retornar resultado
            return asignaturas;
        }

        public async Task<Asignatura?> ObtenerAsignaturaId(int id)
        {
            // IMPERATIVO: Inicializar variable mutable
            Asignatura? asignatura = null;

            // Paso 1: Crear y abrir conexión
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // Paso 2: Crear comando con parámetro explícito
            using SqlCommand command = new SqlCommand(
                "SELECT AsignaturaId, Nombre, Creditos, ProfesorId, AulaId, Activo, FechaCreacion FROM Asignaturas WHERE AsignaturaId = @Id",
                connection);

            // Paso 3: Agregar parámetro manualmente
            command.Parameters.AddWithValue("@Id", id);

            // Paso 4: Ejecutar y leer
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            // Paso 5: Condicional explícito (if en vez de FirstOrDefault)
            if (await reader.ReadAsync())
            {
                asignatura = new Asignatura();
                asignatura.AsignaturaId = reader.GetInt32(0);
                asignatura.Nombre = reader.IsDBNull(1) ? null : reader.GetString(1);
                asignatura.Creditos = reader.GetInt32(2);
                asignatura.ProfesorId = reader.GetInt32(3);
                asignatura.AulaId = reader.GetInt32(4);
                asignatura.Activo = reader.GetBoolean(5);
                asignatura.FechaCreacion = reader.GetDateTime(6);
            }

            return asignatura ?? new Asignatura();
        }

        public async Task AgregarAsignatura(Asignatura asignatura)
        {
            // IMPERATIVO: Secuencia explícita de pasos para insertar

            // Paso 1: Crear y abrir conexión
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // Paso 2: Definir SQL
            string sql = @"INSERT INTO Asignaturas (Nombre, Creditos, ProfesorId, AulaId, Activo, FechaCreacion)
                           VALUES (@Nombre, @Creditos, @ProfesorId, @AulaId, @Activo, @FechaCreacion)";

            // Paso 3: Crear comando
            using SqlCommand command = new SqlCommand(sql, connection);

            // Paso 4: Agregar parámetros uno por uno (imperativo: secuencia explícita)
            command.Parameters.AddWithValue("@Nombre", (object?)asignatura.Nombre ?? DBNull.Value);
            command.Parameters.AddWithValue("@Creditos", asignatura.Creditos);
            command.Parameters.AddWithValue("@ProfesorId", asignatura.ProfesorId);
            command.Parameters.AddWithValue("@AulaId", asignatura.AulaId);
            command.Parameters.AddWithValue("@Activo", asignatura.Activo);
            command.Parameters.AddWithValue("@FechaCreacion", asignatura.FechaCreacion);

            // Paso 5: Ejecutar comando
            await command.ExecuteNonQueryAsync();
        }

        public async Task ActualizarAsignatura(Asignatura asignatura)
        {
            // IMPERATIVO: Secuencia explícita de pasos para actualizar

            // Paso 1: Crear y abrir conexión
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // Paso 2: Definir SQL
            string sql = @"UPDATE Asignaturas 
                           SET Nombre = @Nombre, 
                               Creditos = @Creditos, 
                               ProfesorId = @ProfesorId, 
                               AulaId = @AulaId, 
                               Activo = @Activo 
                           WHERE AsignaturaId = @AsignaturaId";

            // Paso 3: Crear comando
            using SqlCommand command = new SqlCommand(sql, connection);

            // Paso 4: Agregar parámetros uno por uno
            command.Parameters.AddWithValue("@Nombre", (object?)asignatura.Nombre ?? DBNull.Value);
            command.Parameters.AddWithValue("@Creditos", asignatura.Creditos);
            command.Parameters.AddWithValue("@ProfesorId", asignatura.ProfesorId);
            command.Parameters.AddWithValue("@AulaId", asignatura.AulaId);
            command.Parameters.AddWithValue("@Activo", asignatura.Activo);
            command.Parameters.AddWithValue("@AsignaturaId", asignatura.AsignaturaId);

            // Paso 5: Ejecutar comando
            await command.ExecuteNonQueryAsync();
        }

        public async Task InactivarAsignatura(int id)
        {
            // IMPERATIVO: Secuencia explícita para soft-delete

            // Paso 1: Crear y abrir conexión
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // Paso 2: Crear comando con parámetro
            using SqlCommand command = new SqlCommand(
                "UPDATE Asignaturas SET Activo = 0 WHERE AsignaturaId = @Id",
                connection);

            // Paso 3: Agregar parámetro
            command.Parameters.AddWithValue("@Id", id);

            // Paso 4: Ejecutar
            await command.ExecuteNonQueryAsync();
        }
    }
}