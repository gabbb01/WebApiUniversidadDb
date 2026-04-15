import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ApiUniversidad } from '../../services/api-universidad';
import { Asignatura } from '../../models/asignatura.model';
import { Profesor } from '../../models/profesor.model';
import { Aula } from '../../models/aula.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

declare var bootstrap: any;

@Component({
    selector: 'app-admin-asignaturas',
    imports: [CommonModule, FormsModule, RouterLink],
    standalone: true,
    templateUrl: './admin-asignaturas.html',
    styleUrl: './admin-asignaturas.css',
    })
export class AdminAsignaturas implements OnInit {
    public asignaturas: Asignatura[] = [];
    public profesores: Profesor[] = [];
    public aulas: Aula[] = [];

    public asignaturaId: number = 0;
    public nombre: string = '';
    public creditos: number = 0;
    public profesorId: number = 0;
    public aulaId: number = 0;
    public activo: boolean = true;
    
    public accionModal: string = 'Guardar';

    public modal: any;

    constructor(private apiUniversidad: ApiUniversidad, private cdr: ChangeDetectorRef) {}

    ngOnInit(): void {
        this.obtenerAsignaturas();
        this.obtenerProfesores();
        this.obtenerAulas();
    }

    ngAfterViewInit() {
        const modal = document.getElementById('modalAsignaturas');
        this.modal = new bootstrap.Modal(modal);
    }

    obtenerAsignaturas() {
        this.apiUniversidad.obtenerAsignaturas().subscribe({
        next: (data) => { this.asignaturas = data; this.cdr.detectChanges(); },
        error: (error) => console.log(error),
        });
    }

    obtenerProfesores() {
        this.apiUniversidad.obtenerProfesores().subscribe({
        next: (data) => { this.profesores = data; this.cdr.detectChanges(); },
        error: (error) => console.log(error),
        });
    }

    obtenerAulas() {
        this.apiUniversidad.obtenerAulas().subscribe({
        next: (data) => { this.aulas = data; this.cdr.detectChanges(); },
        error: (error) => console.log(error),
        });
    }

    abrirModalGuardar(): void {
        this.accionModal = 'Guardar';
        this.inicializarControles();
        this.modal.show();
    }

    guardarAsignatura(): void {
        if (this.accionModal.toUpperCase() === 'GUARDAR') {
        this.agregarAsignatura();
        } else {
        this.actualizarAsignatura();
        }
    }

    agregarAsignatura(): void {
        const asignatura = new Asignatura(0, this.nombre, this.creditos, this.profesorId, this.aulaId, true, new Date());
        this.apiUniversidad.agregarAsignatura(asignatura).subscribe({
        next: () => { this.obtenerAsignaturas(); this.modal.hide(); },
        error: (error) => console.log(error),
        });
    }

    actualizarAsignatura(): void {
        const asignatura = new Asignatura(this.asignaturaId, this.nombre, this.creditos, this.profesorId, this.aulaId, true, new Date());
        this.apiUniversidad.actualizarAsignatura(asignatura).subscribe({
        next: () => { this.obtenerAsignaturas(); this.modal.hide(); },
        error: (error) => console.log(error),
        });
    }

    cargarParaActualizar(asignatura: Asignatura): void {
        this.accionModal = 'Actualizar';
        this.asignaturaId = asignatura.asignaturaId;
        this.nombre = asignatura.nombre;
        this.creditos = asignatura.creditos;
        this.profesorId = asignatura.profesorId;
        this.aulaId = asignatura.aulaId;
        this.activo = asignatura.activo;
        this.modal.show();
    }

    inactivarAsignatura(id: number): void { 
        if (confirm('¿Desea inactivar esta asignatura?')) {         
            this.apiUniversidad.inactivarAsignatura(id).subscribe({             
                next: () => {
                        this.asignaturas = [];
                        this.obtenerAsignaturas();
                        this.cdr.detectChanges();
                    },      
                error: (error) => 
                    console.log(error),         
            });         
        }    
    }

    inicializarControles(): void {
        this.asignaturaId = 0;
        this.nombre = '';
        this.creditos = 0;
        this.profesorId = 0;
        this.aulaId = 0;
    }
    obtenerNombreProfesor(profesorId: number): string {
        const profesor = this.profesores.find(p => p.profesorId === profesorId);
        return profesor ? `${profesor.nombre} ${profesor.apellido}` : 'Desconocido';
    }

    obtenerCodigoAula(aulaId: number): string {
        const aula = this.aulas.find(a => a.aulaId === aulaId);
        return aula ? aula.codigoAula : 'N/A';
    }
}