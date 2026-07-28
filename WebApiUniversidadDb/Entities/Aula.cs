namespace WebApiUniversidadDb.Entities
{
    // PARADIGMA OOP: Herencia — Aula EXTIENDE EntidadBase
    public class Aula : EntidadBase
    {
        public int AulaId { get; set; }
        public string? CodigoAula { get; set; }
        public int Capacidad { get; set; }

        // Constructor vacío para ADO.NET
        public Aula() : base() { }

        // Constructor completo con chaining al base
        public Aula(int aulaId, string codigoAula, int capacidad,
                    bool activo, DateTime fechaCreacion)
            : base(activo, fechaCreacion)
        {
            AulaId = aulaId;
            CodigoAula = codigoAula;
            Capacidad = capacidad;
        }

        // POLIMORFISMO: Override del método base
        public override string ObtenerDescripcion()
            => $"[Aula] Código: {CodigoAula} | Capacidad: {Capacidad} | {base.ObtenerDescripcion()}";
    }
}