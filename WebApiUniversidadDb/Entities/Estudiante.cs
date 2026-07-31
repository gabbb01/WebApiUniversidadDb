namespace WebApiUniversidadDb.Entities
{
    // PARADIGMA POO: Herencia — Estudiante EXTIENDE EntidadBase
    // Aplica: Herencia, Encapsulamiento con propiedades, Constructor explícito
    public class Estudiante : EntidadBase
    {
        // Encapsulamiento: propiedades específicas de Estudiante
        public int EstudianteId { get; set; }
        public string? NumeroCuenta { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }

        // Constructor vacío requerido para ADO.NET (deserialización)
        public Estudiante() : base() { }

        // Constructor completo — inicializa todos los campos explícitamente
        public Estudiante(int estudianteId, string numeroCuenta, string nombre,
                          string apellido, string correo, bool activo, DateTime fechaCreacion)
            : base(activo, fechaCreacion)
        {
            EstudianteId = estudianteId;
            NumeroCuenta = numeroCuenta;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
        }

        // POLIMORFISMO: Override del método de la clase base
        public override string ObtenerDescripcion()
            => $"[Estudiante] {Nombre} {Apellido} | Cuenta: {NumeroCuenta} | {base.ObtenerDescripcion()}";
    }
}
