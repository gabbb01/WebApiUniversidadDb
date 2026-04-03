using System;

namespace WebApiUniversidadDb.Entities
{
    public class Estudiante
    {
        public int EstudianteId { get; set; }
        public string? NumeroCuenta { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
