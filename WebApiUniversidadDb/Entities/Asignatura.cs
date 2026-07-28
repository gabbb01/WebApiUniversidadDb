namespace WebApiUniversidadDb.Entities
{
    // PARADIGMA OOP: Herencia — Asignatura EXTIENDE EntidadBase
    public class Asignatura : EntidadBase
    {
        public int AsignaturaId { get; set; }
        public string? Nombre { get; set; }
        public int Creditos { get; set; }
        public int ProfesorId { get; set; }
        public int AulaId { get; set; }

        // Constructor vacío para ADO.NET
        public Asignatura() : base() { }

        // Constructor completo con chaining al base
        public Asignatura(int asignaturaId, string nombre, int creditos,
                          int profesorId, int aulaId, bool activo, DateTime fechaCreacion)
            : base(activo, fechaCreacion)
        {
            AsignaturaId = asignaturaId;
            Nombre = nombre;
            Creditos = creditos;
            ProfesorId = profesorId;
            AulaId = aulaId;
        }

        // POLIMORFISMO: Override del método base
        public override string ObtenerDescripcion()
            => $"[Asignatura] {Nombre} | Créditos: {Creditos} | {base.ObtenerDescripcion()}";
    }
}