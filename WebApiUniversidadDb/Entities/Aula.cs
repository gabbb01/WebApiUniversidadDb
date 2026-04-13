using System;

namespace WebApiUniversidadDb.Entities
{
    public class Aula
    {
        public int AulaId { get; set; }
        public string? CodigoAula { get; set; }
        public int Capacidad { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}