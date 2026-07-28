using Microsoft.Data.SqlClient;
using WebApiUniversidadDb.Entities;
using WebApiUniversidadDb.Infrastructure.Interfaces;

namespace WebApiUniversidadDb.Infrastructure.Repository
{
    // PARADIGMA IMPERATIVO: Control de flujo explícito con ADO.NET puro
    // PARADIGMA DECLARATIVO: Consultas SQL con SELECT, WHERE, GROUP BY, ORDER BY, JOIN
    public class MatriculasRepository : IMatriculasRepository
    {
        private readonly string connectionString;

        public MatriculasRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DbUniversidadConnectionString")!;
        }

        // ─────────────────────────────────────────────────────
        // DECLARATIVO: SELECT con JOIN — obtiene matrículas con datos del estudiante y asignatura
        // IMPERATIVO: Pasos explícitos con SqlConnection, SqlCommand, SqlDataReader
        // ─────────────────────────────────────────────────────
        public async Task<List<Matricula>> ObtenerMatriculas()
        {
            List<Matricula> matriculas = new List<Matricula>();

            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // DECLARATIVO: SQL con INNER JOIN, WHERE, ORDER BY
            string sql = @"
                SELECT 
                    m.MatriculaId,
                    m.EstudianteId,
                    m.AsignaturaId,
                    m.Nota,
                    m.Activo,
                    m.FechaCreacion,
                    e.Nombre        AS NombreEstudiante,
                    e.Apellido      AS ApellidoEstudiante,
                    a.Nombre        AS NombreAsignatura,
                    a.Creditos      AS CreditosAsignatura
                FROM Matriculas m
                INNER JOIN Estudiantes e ON m.EstudianteId = e.EstudianteId
                INNER JOIN Asignaturas a ON m.AsignaturaId = a.AsignaturaId
                WHERE m.Activo = 1
                ORDER BY e.Apellido, e.Nombre, a.Nombre";

            using SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            // IMPERATIVO: Ciclo while para iterar fila por fila
            while (await reader.ReadAsync())
            {
                Matricula matricula = new Matricula();
                matricula.MatriculaId          = reader.GetInt32(0);
                matricula.EstudianteId         = reader.GetInt32(1);
                matricula.AsignaturaId         = reader.GetInt32(2);
                matricula.Nota                 = reader.GetDecimal(3);
                matricula.Activo               = reader.GetBoolean(4);
                matricula.FechaCreacion        = reader.GetDateTime(5);
                matricula.NombreEstudiante     = reader.IsDBNull(6)  ? null : reader.GetString(6);
                matricula.ApellidoEstudiante   = reader.IsDBNull(7)  ? null : reader.GetString(7);
                matricula.NombreAsignatura     = reader.IsDBNull(8)  ? null : reader.GetString(8);
                matricula.CreditosAsignatura   = reader.IsDBNull(9)  ? 0    : reader.GetInt32(9);
                matriculas.Add(matricula);
            }

            return matriculas;
        }

        public async Task<Matricula?> ObtenerMatriculaId(int id)
        {
            Matricula? matricula = null;

            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // DECLARATIVO: SELECT con WHERE filtrado por Id
            string sql = @"
                SELECT 
                    m.MatriculaId, m.EstudianteId, m.AsignaturaId,
                    m.Nota, m.Activo, m.FechaCreacion,
                    e.Nombre, e.Apellido, a.Nombre, a.Creditos
                FROM Matriculas m
                INNER JOIN Estudiantes e ON m.EstudianteId = e.EstudianteId
                INNER JOIN Asignaturas a ON m.AsignaturaId = a.AsignaturaId
                WHERE m.MatriculaId = @Id";

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                matricula = new Matricula();
                matricula.MatriculaId        = reader.GetInt32(0);
                matricula.EstudianteId       = reader.GetInt32(1);
                matricula.AsignaturaId       = reader.GetInt32(2);
                matricula.Nota               = reader.GetDecimal(3);
                matricula.Activo             = reader.GetBoolean(4);
                matricula.FechaCreacion      = reader.GetDateTime(5);
                matricula.NombreEstudiante   = reader.IsDBNull(6) ? null : reader.GetString(6);
                matricula.ApellidoEstudiante = reader.IsDBNull(7) ? null : reader.GetString(7);
                matricula.NombreAsignatura   = reader.IsDBNull(8) ? null : reader.GetString(8);
                matricula.CreditosAsignatura = reader.IsDBNull(9) ? 0    : reader.GetInt32(9);
            }

            return matricula;
        }

        // DECLARATIVO: SELECT con WHERE filtrado por EstudianteId
        public async Task<List<Matricula>> ObtenerMatriculasPorEstudiante(int estudianteId)
        {
            List<Matricula> matriculas = new List<Matricula>();

            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string sql = @"
                SELECT 
                    m.MatriculaId, m.EstudianteId, m.AsignaturaId,
                    m.Nota, m.Activo, m.FechaCreacion,
                    e.Nombre, e.Apellido, a.Nombre, a.Creditos
                FROM Matriculas m
                INNER JOIN Estudiantes e ON m.EstudianteId = e.EstudianteId
                INNER JOIN Asignaturas a ON m.AsignaturaId = a.AsignaturaId
                WHERE m.EstudianteId = @EstudianteId
                  AND m.Activo = 1
                ORDER BY a.Nombre";

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EstudianteId", estudianteId);
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Matricula m = new Matricula();
                m.MatriculaId        = reader.GetInt32(0);
                m.EstudianteId       = reader.GetInt32(1);
                m.AsignaturaId       = reader.GetInt32(2);
                m.Nota               = reader.GetDecimal(3);
                m.Activo             = reader.GetBoolean(4);
                m.FechaCreacion      = reader.GetDateTime(5);
                m.NombreEstudiante   = reader.IsDBNull(6) ? null : reader.GetString(6);
                m.ApellidoEstudiante = reader.IsDBNull(7) ? null : reader.GetString(7);
                m.NombreAsignatura   = reader.IsDBNull(8) ? null : reader.GetString(8);
                m.CreditosAsignatura = reader.IsDBNull(9) ? 0    : reader.GetInt32(9);
                matriculas.Add(m);
            }

            return matriculas;
        }

        // ─────────────────────────────────────────────────────
        // DECLARATIVO: SELECT con GROUP BY + ORDER BY + AVG (función agregada)
        // Consulta avanzada para estadísticas de promedio por estudiante
        // ─────────────────────────────────────────────────────
        public async Task<List<Matricula>> ObtenerPromediosPorEstudiante()
        {
            List<Matricula> resultados = new List<Matricula>();

            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // DECLARATIVO: GROUP BY + AVG + ORDER BY + HAVING
            string sql = @"
                SELECT 
                    e.EstudianteId,
                    e.Nombre        AS NombreEstudiante,
                    e.Apellido      AS ApellidoEstudiante,
                    AVG(m.Nota)     AS Promedio,
                    COUNT(m.MatriculaId) AS TotalAsignaturas
                FROM Matriculas m
                INNER JOIN Estudiantes e ON m.EstudianteId = e.EstudianteId
                WHERE m.Activo = 1
                GROUP BY e.EstudianteId, e.Nombre, e.Apellido
                ORDER BY Promedio DESC";

            using SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                // Usamos Matricula como DTO de resultado de estadística
                Matricula resultado = new Matricula();
                resultado.EstudianteId       = reader.GetInt32(0);
                resultado.NombreEstudiante   = reader.IsDBNull(1) ? null : reader.GetString(1);
                resultado.ApellidoEstudiante = reader.IsDBNull(2) ? null : reader.GetString(2);
                resultado.Nota               = reader.IsDBNull(3) ? 0    : reader.GetDecimal(3);
                resultado.CreditosAsignatura = reader.IsDBNull(4) ? 0    : reader.GetInt32(4);
                resultados.Add(resultado);
            }

            return resultados;
        }

        // IMPERATIVO + DECLARATIVO: INSERT paso a paso
        public async Task AgregarMatricula(Matricula matricula)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // DECLARATIVO: SQL INSERT
            string sql = @"
                INSERT INTO Matriculas (EstudianteId, AsignaturaId, Nota, Activo, FechaCreacion)
                VALUES (@EstudianteId, @AsignaturaId, @Nota, @Activo, @FechaCreacion)";

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EstudianteId", matricula.EstudianteId);
            command.Parameters.AddWithValue("@AsignaturaId", matricula.AsignaturaId);
            command.Parameters.AddWithValue("@Nota", matricula.Nota);
            command.Parameters.AddWithValue("@Activo", matricula.Activo);
            command.Parameters.AddWithValue("@FechaCreacion", matricula.FechaCreacion);

            await command.ExecuteNonQueryAsync();
        }

        // DECLARATIVO: UPDATE de nota
        public async Task ActualizarNota(int matriculaId, decimal nota)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using SqlCommand command = new SqlCommand(
                "UPDATE Matriculas SET Nota = @Nota WHERE MatriculaId = @Id",
                connection);

            command.Parameters.AddWithValue("@Nota", nota);
            command.Parameters.AddWithValue("@Id", matriculaId);

            await command.ExecuteNonQueryAsync();
        }

        // DECLARATIVO: Soft delete (UPDATE de Activo)
        public async Task InactivarMatricula(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using SqlCommand command = new SqlCommand(
                "UPDATE Matriculas SET Activo = 0 WHERE MatriculaId = @Id",
                connection);

            command.Parameters.AddWithValue("@Id", id);
            await command.ExecuteNonQueryAsync();
        }
    }
}
