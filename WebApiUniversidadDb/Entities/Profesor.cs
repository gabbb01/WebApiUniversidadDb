namespace WebApiUniversidadDb.Entities
{
    // PARADIGMA OOP: Herencia — Profesor EXTIENDE EntidadBase
    public class Profesor : EntidadBase
    {
        public int ProfesorId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Especialidad { get; set; }

        // Constructor vacío para ADO.NET
        public Profesor() : base() { }

        // Constructor completo con chaining al base
        public Profesor(int profesorId, string nombre, string apellido,
                        string especialidad, bool activo, DateTime fechaCreacion)
            : base(activo, fechaCreacion)
        {
            ProfesorId = profesorId;
            Nombre = nombre;
            Apellido = apellido;
            Especialidad = especialidad;
        }

        // POLIMORFISMO: Override del método base
        public override string ObtenerDescripcion()
            => $"[Profesor] {Nombre} {Apellido} | Especialidad: {Especialidad} | {base.ObtenerDescripcion()}";
    }
}