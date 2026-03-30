using System;

namespace WebApiUniversidadDb.Entities
{
    public class Aula
    {
        public int AulaId { get; set; }
        public string? CodigoAula { get; set; }
        public string? Acpacidad { get; set; } 
        public bool Activo { get; set; }  
        public DateTime FechaCreacion { get; set; }  
    }
}