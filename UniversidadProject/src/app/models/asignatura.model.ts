export class Asignatura {
    asignaturaId: number;
    nombre: string;
    creditos: number;
    profesorId: number;
    aulaId: number;
    activo: boolean;
    fechaCreacion: Date;
    
    constructor(asignaturaId: number, nombre: string, creditos: number, profesorId: number, aulaId: number, activo: boolean, fechaCreacion: Date) {
        this.asignaturaId = asignaturaId;
        this.nombre = nombre;
        this.creditos = creditos;
        this.profesorId = profesorId;
        this.aulaId = aulaId;
        this.activo = activo;
        this.fechaCreacion = fechaCreacion;
    }
}