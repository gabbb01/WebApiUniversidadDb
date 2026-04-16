import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ApiUniversidad } from '../../services/api-universidad';
import { Asignatura } from '../../models/asignatura.model';
import { Profesor } from '../../models/profesor.model';
import { Aula } from '../../models/aula.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
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
        next: () => { 
            this.obtenerAsignaturas(); 
            this.modal.hide(); 
            Swal.fire({
                title: "¡Asignatura agregada exitosamente!",
                icon: "success",
            });
        },
        error: (error) => console.log(error),
        });
    }

    actualizarAsignatura(): void {
        const asignatura = new Asignatura(this.asignaturaId, this.nombre, this.creditos, this.profesorId, this.aulaId, true, new Date());
        this.apiUniversidad.actualizarAsignatura(asignatura).subscribe({
        next: () => { 
            this.obtenerAsignaturas(); 
            this.modal.hide(); 
            Swal.fire({
                title: "¡Asignatura actualizada exitosamente!",
                icon: "success",
            });
        },
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
    Swal.fire({
        title: '¿Inactivar asignatura?',
        text: "Esta acción podría afectar a los grupos que tienen esta materia asignada.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, inactivar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {

            this.apiUniversidad.inactivarAsignatura(id).subscribe({
                next: () => {
                    // Mantenemos tu lógica de limpieza y refresco
                    this.asignaturas = [];
                    this.obtenerAsignaturas();
                    this.cdr.detectChanges();

                    Swal.fire({
                        title: "¡Asignatura Inactivada!",
                        text: "La materia se ha actualizado correctamente.",
                        icon: "success",
                    });
                },
                error: (error) => {
                    console.error(error);
                    Swal.fire({
                        title: "Error",
                        text: "No se pudo inactivar la asignatura.",
                        icon: "error",
                    });
                },
            });

        }
    });
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