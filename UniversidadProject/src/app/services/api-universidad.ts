import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Estudiante } from '../models/estudiante.model';
import { Asignatura } from '../models/asignatura.model';
import { Aula } from '../models/aula.model';
import { Profesor } from '../models/profesor.model';

@Injectable({
    providedIn: 'root',
})
export class ApiUniversidad {

    private apiUrl = "https://localhost:7004/api";
    constructor(private httpClient : HttpClient) {
    }

    // ESTUDIANTES
    obtenerEstudiantes() {
        return this.httpClient.get<Estudiante[]>(`${this.apiUrl}/Estudiantes`);
    }

    agregarEstudiante(estudiante: Estudiante) {
        return this.httpClient.post<any>(`${this.apiUrl}/Estudiantes`, estudiante);
    }

    actualizarEstudiante(estudiante: Estudiante) {
        return this.httpClient.put<any>(`${this.apiUrl}/Estudiantes`, estudiante);
    }

    inactivarEstudiante(id: number) {
        return this.httpClient.delete<any>(`${this.apiUrl}/Estudiantes/${id}`);
    }


    // PROFESORES
    obtenerProfesores(){
        return this.httpClient.get<Profesor[]>(`${this.apiUrl}/profesores`);
    }
    agregarProfesor(profesor: Profesor) {
        return this.httpClient.post<any>(`${this.apiUrl}/Profesores`, profesor);
    }
    
    actualizarProfesor(profesor: Profesor) {
        return this.httpClient.put<any>(`${this.apiUrl}/Profesores`, profesor);
    }
    
    inactivarProfesor(id: number) {
        return this.httpClient.delete<any>(`${this.apiUrl}/Profesores/${id}`);
    }

    // AULAS
    obtenerAulas() {
        return this.httpClient.get<Aula[]>(`${this.apiUrl}/Aulas`);
    }

    agregarAula(aula: Aula) {
        return this.httpClient.post<any>(`${this.apiUrl}/Aulas`, aula);
    }

    actualizarAula(aula: Aula) {
        return this.httpClient.put<any>(`${this.apiUrl}/Aulas`, aula);
    }

    inactivarAula(id: number) {
        return this.httpClient.delete<any>(`${this.apiUrl}/Aulas/${id}`);
    }

    // ASIGNATURAS
    obtenerAsignaturas() {
        return this.httpClient.get<Asignatura[]>(`${this.apiUrl}/Asignaturas`);
    }

    agregarAsignatura(asignatura: Asignatura) {
        return this.httpClient.post<any>(`${this.apiUrl}/Asignaturas`, asignatura);
    }

    actualizarAsignatura(asignatura: Asignatura) {
        return this.httpClient.put<any>(`${this.apiUrl}/Asignaturas`, asignatura);
    }

    inactivarAsignatura(id: number) {
        return this.httpClient.delete<any>(`${this.apiUrl}/Asignaturas/${id}`);
    }
}
