using System;

namespace WebApiUniversidadDb.Entities
{
    public class Aula
    {
        public int Id { get; set; } // Llave primaria [cite: 16]
        public string NombreAula { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true; // Campo Activo tipo bit [cite: 17]
        public DateTime FechaCreacion { get; set; } = DateTime.Now; // Campo Fecha Creacion [cite: 18]
    }
}