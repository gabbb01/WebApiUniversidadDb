using System;

namespace WebApiUniversidadDb.Entities
{
    public class Profesor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}   