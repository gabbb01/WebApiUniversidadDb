namespace WebApiUniversidadDb.Entities
{
    // PARADIGMA OOP: Herencia — Matricula EXTIENDE EntidadBase
    // Entidad nueva que representa la inscripción de un Estudiante a una Asignatura
    public class Matricula : EntidadBase
    {
        // Encapsulamiento: propiedades específicas de Matricula
        public int MatriculaId { get; set; }
        public int EstudianteId { get; set; }
        public int AsignaturaId { get; set; }
        public decimal Nota { get; set; }

        // Propiedades de navegación (para las consultas JOIN del repositorio)
        public string? NombreEstudiante { get; set; }
        public string? ApellidoEstudiante { get; set; }
        public string? NombreAsignatura { get; set; }
        public int CreditosAsignatura { get; set; }

        // Constructor vacío para ADO.NET
        public Matricula() : base() { }

        // Constructor completo con chaining al base (constructor explícito)
        public Matricula(int matriculaId, int estudianteId, int asignaturaId,
                         decimal nota, bool activo, DateTime fechaCreacion)
            : base(activo, fechaCreacion)
        {
            MatriculaId = matriculaId;
            EstudianteId = estudianteId;
            AsignaturaId = asignaturaId;
            Nota = nota;
        }

        // POLIMORFISMO: Override del método de la clase base
        public override string ObtenerDescripcion()
            => $"[Matrícula] Estudiante: {NombreEstudiante} | Asignatura: {NombreAsignatura} | Nota: {Nota} | {base.ObtenerDescripcion()}";
    }
}
