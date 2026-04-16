import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ApiUniversidad } from '../../services/api-universidad';
import { Estudiante } from '../../models/estudiante.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
declare var bootstrap: any;

@Component({
    selector: 'app-admin-estudiantes',
    imports: [CommonModule, FormsModule,RouterLink],
    standalone: true,
    templateUrl: './admin-estudiantes.html',
    styleUrl: './admin-estudiantes.css',
})
export class AdminEstudiantes implements OnInit {

    public estudiantes: Estudiante[] = [];

    public estudianteId: number = 0;
    public numeroCuenta: string = '';
    public nombre: string = '';
    public apellido: string = '';
    public correo: string = '';
    public activo: boolean = true;
    public accionModal: string = 'Guardar';

    public modal: any;

    constructor(private apiUniversidad: ApiUniversidad, private cdr: ChangeDetectorRef) { }

    ngOnInit(): void {
        this.obtenerEstudiantes();
    }

    ngAfterViewInit() {
        const modal = document.getElementById('modalEstudiantes');
        this.modal = new bootstrap.Modal(modal);
    }

    obtenerEstudiantes() {
        this.apiUniversidad.obtenerEstudiantes().subscribe({
            next: (data) => { this.estudiantes = data; this.cdr.detectChanges(); },
            error: (error) => console.log(error),
        });
    }

    abrirModalGuardar(): void {
        this.accionModal = 'Guardar';
        this.inicializarControles();
        this.modal.show();
    }

    guardarEstudiante(): void {
        if (this.accionModal.toUpperCase() === 'GUARDAR') {
            this.agregarEstudiante();
        } else {
            this.actualizarEstudiante();
        }
    }

    agregarEstudiante(): void {
        const estudiante = new Estudiante(0, this.numeroCuenta, this.nombre, this.apellido, this.correo, true, new Date());
        this.apiUniversidad.agregarEstudiante(estudiante).subscribe({
            next: () => { 
                this.obtenerEstudiantes(); 
                this.modal.hide(); 
                Swal.fire({
                    title: "¡Estudiante agregado exitosamente!",
                    icon: "success",
                });
            },
            error: (error) => console.log(error),
        });
    }

    actualizarEstudiante(): void {
        const estudiante = new Estudiante(this.estudianteId, this.numeroCuenta, this.nombre, this.apellido, this.correo, true, new Date());
        this.apiUniversidad.actualizarEstudiante(estudiante).subscribe({
            next: () => { 
                this.obtenerEstudiantes(); 
                this.modal.hide(); 
                Swal.fire({
                    title: "¡Estudiante actualizado exitosamente!",
                    icon: "success",
                });
            },
            error: (error) => console.log(error),
        });
    }

    cargarParaActualizar(estudiante: Estudiante): void {
        this.accionModal = 'Actualizar';
        this.estudianteId = estudiante.estudianteId;
        this.numeroCuenta = estudiante.numeroCuenta;
        this.nombre = estudiante.nombre;
        this.apellido = estudiante.apellido;
        this.correo = estudiante.correo;
        this.activo = estudiante.activo;
        this.modal.show();
    }

inactivarEstudiante(id: number): void {
    Swal.fire({
        title: '¿Confirmar inactivación?',
        text: "El estudiante ya no aparecerá en las listas activas.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Sí, inactivar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            
            this.apiUniversidad.inactivarEstudiante(id).subscribe({
                next: () => {
                    this.obtenerEstudiantes();
                    Swal.fire({
                        title: "¡Logrado!",
                        text: "Estudiante inactivado exitosamente.",
                        icon: "success",
                    });
                },
                error: (error) => {
                    console.error(error);
                    Swal.fire({
                        title: "Error",
                        text: "Hubo un problema al procesar la solicitud.",
                        icon: "error",
                    });
                },
            });
        }
    });
}

    inicializarControles(): void {
        this.estudianteId = 0;
        this.numeroCuenta = '';
        this.nombre = '';
        this.apellido = '';
        this.correo = '';
    }
}