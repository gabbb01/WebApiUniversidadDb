import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ApiUniversidad } from '../../services/api-universidad';
import { Profesor } from '../../models/profesor.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
    
    declare var bootstrap: any;
    
@Component({
    selector: 'app-admin-profesores',
    imports: [CommonModule, FormsModule],
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
        next: () => { this.obtenerProfesores(); this.modal.hide(); },
        error: (error) => console.log(error),
        });
    }
    
    actualizarProfesor(): void {
        const profesor = new Profesor(this.profesorId, this.nombre, this.apellido, this.especialidad, true, new Date());
        this.apiUniversidad.actualizarProfesor(profesor).subscribe({
        next: () => { this.obtenerProfesores(); this.modal.hide(); },
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
        if (confirm('¿Desea inactivar este profesor?')) {
        this.apiUniversidad.inactivarProfesor(id).subscribe({
            next: () => this.obtenerProfesores(),
            error: (error) => console.log(error),
        });
        }
    }
    
    inicializarControles(): void {
        this.profesorId = 0;
        this.nombre = '';
        this.apellido = '';
        this.especialidad = '';
    }
}