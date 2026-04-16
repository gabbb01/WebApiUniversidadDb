import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ApiUniversidad } from '../../services/api-universidad';
import { Profesor } from '../../models/profesor.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
    
    declare var bootstrap: any;
    
@Component({
    selector: 'app-admin-profesores',
    imports: [CommonModule, FormsModule, RouterLink],
    standalone: true,
    templateUrl: './admin-profesores.html',
    styleUrl: './admin-profesores.css',
})
export class AdminProfesores implements OnInit {
    
    public profesores: Profesor[] = [];
    
    public profesorId: number = 0;
    public nombre: string = '';
    public apellido: string = '';
    public especialidad: string = '';
    public activo: boolean = true;
    public accionModal: string = 'Guardar';
    
    public modal: any;
    
    constructor(private apiUniversidad: ApiUniversidad, private cdr: ChangeDetectorRef) {}
    
    ngOnInit(): void {
        this.obtenerProfesores();
    }
    
    ngAfterViewInit() {
        const modal = document.getElementById('modalProfesores');
        this.modal = new bootstrap.Modal(modal);
    }
    
    obtenerProfesores() {
        this.apiUniversidad.obtenerProfesores().subscribe({
        next: (data) => {
            this.profesores = data;
            this.cdr.detectChanges();
        },
        error: (error) => console.log(error),
        });
    }
    
    abrirModalGuardar(): void {
        this.accionModal = 'Guardar';
        this.inicializarControles();
        this.modal.show();
    }
    
    guardarProfesor(): void {
        if (this.accionModal.toUpperCase() === 'GUARDAR') {
        this.agregarProfesor();
        } else {
        this.actualizarProfesor();
        }
    }
    
    agregarProfesor(): void {
        const profesor = new Profesor(0, this.nombre, this.apellido, this.especialidad, true, new Date());
        this.apiUniversidad.agregarProfesor(profesor).subscribe({
        next: () => { 
            this.obtenerProfesores(); 
            this.modal.hide(); 
            Swal.fire({
                title: "Profesor agregado exitosamente",
                icon: "success",
            });
        },
        error: (error) => console.log(error),
        });
    }
    
    actualizarProfesor(): void {
        const profesor = new Profesor(this.profesorId, this.nombre, this.apellido, this.especialidad, true, new Date());
        this.apiUniversidad.actualizarProfesor(profesor).subscribe({
        next: () => { 
            this.obtenerProfesores(); 
            this.modal.hide(); 
            Swal.fire({
                title: "Profesor actualizado exitosamente",
                icon: "success",
            });
        },
        error: (error) => console.log(error),
        });
    }
    
    cargarParaActualizar(profesor: Profesor): void {
        this.accionModal = 'Actualizar';
        this.profesorId = profesor.profesorId;
        this.nombre = profesor.nombre;
        this.apellido = profesor.apellido;
        this.especialidad = profesor.especialidad;
        this.activo = profesor.activo;
        this.modal.show();
    }
    
    inactivarProfesor(id: number): void {
    Swal.fire({
        title: '¿Estás seguro?',
        text: "El profesor será marcado como inactivo.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, inactivar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            
            this.apiUniversidad.inactivarProfesor(id).subscribe({
                next: () => {
                    this.obtenerProfesores();
                    Swal.fire({
                        title: "¡Inactivado!",
                        text: "El profesor ha sido inactivado correctamente.",
                        icon: "success",
                    });
                },
                error: (error) => {
                    console.error(error);
                    Swal.fire({
                        title: "Error",
                        text: "No se pudo inactivar al profesor.",
                        icon: "error",
                    });
                },
            });
        }
    });
}
    
    inicializarControles(): void {
        this.profesorId = 0;
        this.nombre = '';
        this.apellido = '';
        this.especialidad = '';
    }
}