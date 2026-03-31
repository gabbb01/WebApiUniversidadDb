
namespace WebApiUniversidadDb.Entities
{
    public class Asignatura
    {
        public int AsignaturaId { get; set; }
        public string? Nombre { get; set; }
        public int Creditos { get; set; }
        public int ProfesorId { get; set; }
        public int AulaId { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }                                                                                   
}