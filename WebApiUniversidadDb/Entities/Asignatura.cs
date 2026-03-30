using System;

namespace WebApiUniversidadDb.Entities
{
    public class Asignatura
    {
        public int Id { get; set; }
        public string NombreAsignatura { get; set; } = string.Empty;
        public int ProfesorId { get; set; }
        public Profesor? Profesor { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }                                                                                   
    }