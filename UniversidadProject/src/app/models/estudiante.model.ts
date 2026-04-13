export class Estudiante {
    estudianteId: number;
    numeroCuenta: string;
    nombre: string;
    apellido: string;
    correo: string;
    activo: boolean;
    fechaCreacion: Date;
    
    constructor(estudianteId: number, numeroCuenta: string, nombre: string, apellido: string, correo: string, activo: boolean, fechaCreacion: Date) {
        this.estudianteId = estudianteId;
        this.numeroCuenta = numeroCuenta;
        this.nombre = nombre;
        this.apellido = apellido;
        this.correo = correo;
        this.activo = activo;
        this.fechaCreacion = fechaCreacion;
    }
}
