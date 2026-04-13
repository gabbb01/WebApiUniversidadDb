export class Aula {
    aulaId: number;
    codigoAula: string;
    capacidad: number;
    activo: boolean;
    fechaCreacion: Date;
    
    constructor(aulaId: number, codigoAula: string, capacidad: number, activo: boolean, fechaCreacion: Date) {
        this.aulaId = aulaId;
        this.codigoAula = codigoAula;
        this.capacidad = capacidad;
        this.activo = activo;
        this.fechaCreacion = fechaCreacion;
    }
}