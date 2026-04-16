import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ApiUniversidad } from '../../services/api-universidad';
import { Aula } from '../../models/aula.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
declare var bootstrap: any;

@Component({
	selector: 'app-admin-aulas',
	imports: [CommonModule, FormsModule, RouterLink],
	standalone: true,
	templateUrl: './admin-aulas.html',
	styleUrl: './admin-aulas.css',
})
export class AdminAulas implements OnInit {

	public aulas: Aula[] = [];

	public aulaId: number = 0;
	public codigoAula: string = '';
	public capacidad: number = 0;
	public accionModal: string = 'Guardar';
	public activo: boolean = true;

	public modal: any;

	constructor(private apiUniversidad: ApiUniversidad, private cdr: ChangeDetectorRef) { }

	ngOnInit(): void {
		this.obtenerAulas();
	}

	ngAfterViewInit() {
		const modal = document.getElementById('modalAulas');
		this.modal = new bootstrap.Modal(modal);
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

	guardarAula(): void {
		if (this.accionModal.toUpperCase() === 'GUARDAR') {
			this.agregarAula();
		} else {
			this.actualizarAula();
		}
	}

	agregarAula(): void {
		const aula = new Aula(0, this.codigoAula, this.capacidad, true, new Date());
		this.apiUniversidad.agregarAula(aula).subscribe({
			next: () => { 
				this.obtenerAulas(); 
				this.modal.hide(); 
				Swal.fire({
					title: "¡Aula agregada exitosamente!",
					icon: "success",
				});
			},
			error: (error) => console.log(error),
		});
	}

	actualizarAula(): void {
		const aula = new Aula(this.aulaId, this.codigoAula, this.capacidad, true, new Date());
		this.apiUniversidad.actualizarAula(aula).subscribe({
			next: () => { 
				this.obtenerAulas(); 
				this.modal.hide(); 
				Swal.fire({
					title: "¡Aula actualizada exitosamente!",
					icon: "success",
				});
			},
			error: (error) => console.log(error),
		});
	}

	cargarParaActualizar(aula: Aula): void {
		this.accionModal = 'Actualizar';
		this.aulaId = aula.aulaId;
		this.codigoAula = aula.codigoAula;
		this.capacidad = aula.capacidad;
		this.activo = aula.activo;
		this.modal.show();
	}

inactivarAula(id: number): void {
    Swal.fire({
        title: '¿Inactivar aula?',
        text: "Asegúrate de que no haya clases programadas en este salón.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, inactivar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            
            this.apiUniversidad.inactivarAula(id).subscribe({
                next: () => {
                    this.obtenerAulas();
                    Swal.fire({
                        title: "Aula actualizada",
                        text: "El estado del aula ha cambiado a inactivo.",
                        icon: "success",
                    });
                },
                error: (error) => {
                    console.error(error);
                    Swal.fire({
                        title: "Error",
                        text: "No se pudo actualizar el estado del aula.",
                        icon: "error",
                    });
                },
            });
        }
    });
}

	inicializarControles(): void {
		this.aulaId = 0;
		this.codigoAula = '';
		this.capacidad = 0;
	}
}