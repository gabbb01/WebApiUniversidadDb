namespace WebApiUniversidadDb.Entities
{
    // PARADIGMA ORIENTADO A OBJETOS:
    // Clase base abstracta que encapsula propiedades comunes a todas las entidades.
    // Aplica: Encapsulamiento (propiedades con get/set), Abstracción (clase abstracta),
    // y Herencia (todas las entidades derivan de esta clase).
    public abstract class EntidadBase
    {
        // Encapsulamiento: propiedades con acceso controlado
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Constructor protegido — solo las subclases pueden usarlo
        protected EntidadBase()
        {
            Activo = true;
            FechaCreacion = DateTime.Now;
        }

        protected EntidadBase(bool activo, DateTime fechaCreacion)
        {
            Activo = activo;
            FechaCreacion = fechaCreacion;
        }

        // Método virtual — puede ser sobreescrito por subclases (polimorfismo)
        public virtual string ObtenerDescripcion()
            => $"[Entidad] Activo: {Activo} | Creado: {FechaCreacion:dd/MM/yyyy}";
    }
}
