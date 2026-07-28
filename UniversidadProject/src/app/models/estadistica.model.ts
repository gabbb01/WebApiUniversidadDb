export class EstadisticaGenerales {
    promedio: number = 0;
    notaMaxima: number = 0;
    notaMinima: number = 0;
    totalAprobados: number = 0;
    totalReprobados: number = 0;
    totalEstudiantes: number = 0;
    clasificacion: string = '';
}

export class PromedioEstudiante {
    estudianteId: number = 0;
    nombreEstudiante: string = '';
    apellidoEstudiante: string = '';
    nota: number = 0;           // promedio
    creditosAsignatura: number = 0; // total asignaturas
}
