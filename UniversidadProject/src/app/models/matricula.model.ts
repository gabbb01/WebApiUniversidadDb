export class Matricula {
    matriculaId: number;
    estudianteId: number;
    asignaturaId: number;
    nota: number;
    activo: boolean;
    fechaCreacion: Date;
    // Campos de navegación (JOIN)
    nombreEstudiante?: string;
    apellidoEstudiante?: string;
    nombreAsignatura?: string;
    creditosAsignatura?: number;

    constructor(
        matriculaId: number,
        estudianteId: number,
        asignaturaId: number,
        nota: number,
        activo: boolean,
        fechaCreacion: Date
    ) {
        this.matriculaId = matriculaId;
        this.estudianteId = estudianteId;
        this.asignaturaId = asignaturaId;
        this.nota = nota;
        this.activo = activo;
        this.fechaCreacion = fechaCreacion;
    }
}
