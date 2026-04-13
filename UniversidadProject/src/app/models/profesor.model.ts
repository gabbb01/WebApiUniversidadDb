export class Profesor {
    profesorId: number;
    nombre: string;
    apellido: string;
    especialidad: string;
    activo: boolean;
    fechaCreacion: Date;
    
    constructor(profesorId: number, nombre: string, apellido: string, especialidad: string, activo: boolean, fechaCreacion: Date) {
        this.profesorId = profesorId;
        this.nombre = nombre;
        this.apellido = apellido;
        this.especialidad = especialidad;
        this.activo = activo;
        this.fechaCreacion = fechaCreacion;
    }
}
