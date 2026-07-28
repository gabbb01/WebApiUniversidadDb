import { Component, OnInit, ChangeDetectorRef, NgZone } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { ApiUniversidad } from '../../services/api-universidad';
import { Matricula } from '../../models/matricula.model';
import { Estudiante } from '../../models/estudiante.model';
import { Asignatura } from '../../models/asignatura.model';

@Component({
    selector: 'app-admin-matriculas',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './admin-matriculas.html',
    styleUrl: './admin-matriculas.css'
})
export class AdminMatriculas implements OnInit {

    matriculas: Matricula[] = [];
    estudiantes: Estudiante[] = [];
    asignaturas: Asignatura[] = [];

    nuevaMatricula: Matricula = new Matricula(0, 0, 0, 0, true, new Date());
    cargando: boolean = false;

    // Estado de edición inline de nota
    editandoNotaId: number | null = null;
    notaTemp: number = 0;

    // FUNCIONAL: getters puros — filter sin mutar el array original
    // Solo cuentan matriculas con nota asignada (> 0)
    get totalAprobados(): number {
        return this.matriculas.filter(m => Number(m.nota) >= 65).length;
    }

    get totalReprobados(): number {
        return this.matriculas.filter(m => Number(m.nota) > 0 && Number(m.nota) < 65).length;
    }

    get totalPendientes(): number {
        return this.matriculas.filter(m => Number(m.nota) === 0).length;
    }

    constructor(
        private apiUniversidad: ApiUniversidad,
        private cdr: ChangeDetectorRef,
        private zone: NgZone
    ) { }

    ngOnInit(): void {
        this.obtenerMatriculas();
        this.obtenerEstudiantes();
        this.obtenerAsignaturas();
    }

    obtenerMatriculas(): void {
        this.cargando = true;
        this.apiUniversidad.obtenerMatriculas().subscribe({
            next: (data) => {
                this.matriculas = data;
                this.cargando = false;
                this.cdr.detectChanges();
            },
            error: () => { this.cargando = false; }
        });
    }

    obtenerEstudiantes(): void {
        this.apiUniversidad.obtenerEstudiantes().subscribe({
            next: (data) => {
                this.estudiantes = data.filter(e => e.activo);
                this.cdr.detectChanges();
            }
        });
    }

    obtenerAsignaturas(): void {
        this.apiUniversidad.obtenerAsignaturas().subscribe({
            next: (data) => {
                this.asignaturas = data.filter(a => a.activo);
                this.cdr.detectChanges();
            }
        });
    }

    agregarMatricula(): void {
        if (!this.nuevaMatricula.estudianteId || !this.nuevaMatricula.asignaturaId) {
            Swal.fire({ title: 'Datos incompletos', text: 'Selecciona estudiante y asignatura.', icon: 'warning' });
            return;
        }
        this.nuevaMatricula.activo = true;
        this.nuevaMatricula.nota = 0;
        this.nuevaMatricula.fechaCreacion = new Date();

        this.apiUniversidad.agregarMatricula(this.nuevaMatricula).subscribe({
            next: () => {
                this.zone.run(() => {
                    this.nuevaMatricula = new Matricula(0, 0, 0, 0, true, new Date());
                    this.obtenerMatriculas();
                    Swal.fire({ title: '¡Matrícula registrada!', text: 'Se registró correctamente.', icon: 'success' });
                });
            },
            error: () => {
                Swal.fire({ title: 'Error', text: 'No se pudo registrar la matrícula.', icon: 'error' });
            }
        });
    }

    // ── Edición inline de nota ──────────────────────────────────────────
    abrirEdicionNota(matricula: Matricula): void {
        this.editandoNotaId = matricula.matriculaId;
        this.notaTemp = Number(matricula.nota);
        this.cdr.detectChanges();
    }

    cancelarEdicionNota(): void {
        this.editandoNotaId = null;
        this.notaTemp = 0;
        this.cdr.detectChanges();
    }

    guardarNota(matriculaId: number): void {
        if (this.notaTemp < 0 || this.notaTemp > 100) {
            Swal.fire({ title: 'Nota inválida', text: 'La nota debe estar entre 0 y 100.', icon: 'warning' });
            return;
        }

        this.apiUniversidad.actualizarNota(matriculaId, this.notaTemp).subscribe({
            next: () => {
                // Actualiza el valor local sin re-fetch
                const m = this.matriculas.find(x => x.matriculaId === matriculaId);
                if (m) m.nota = this.notaTemp;
                this.editandoNotaId = null;
                this.notaTemp = 0;
                this.cdr.detectChanges();
                Swal.fire({ title: '¡Nota guardada!', icon: 'success', timer: 1500, showConfirmButton: false });
            },
            error: () => {
                Swal.fire({ title: 'Error', text: 'No se pudo guardar la nota.', icon: 'error' });
            }
        });
    }

    // ── Eliminar ────────────────────────────────────────────────────────
    inactivarMatricula(id: number): void {
        Swal.fire({
            title: '¿Eliminar matrícula?',
            text: 'Esta acción no se puede deshacer.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                this.zone.run(() => {
                    this.apiUniversidad.inactivarMatricula(id).subscribe({
                        next: () => {
                            this.apiUniversidad.obtenerMatriculas().subscribe({
                                next: (data) => {
                                    this.matriculas = data;
                                    this.cdr.detectChanges();
                                    Swal.fire({ title: '¡Eliminada!', icon: 'success', timer: 1500, showConfirmButton: false });
                                }
                            });
                        },
                        error: () => {
                            Swal.fire({ title: 'Error', text: 'No se pudo eliminar.', icon: 'error' });
                        }
                    });
                });
            }
        });
    }

    // ── Helpers de estilos ──────────────────────────────────────────────
    getNotaClass(nota: number): string {
        if (nota === 0)   return 'nota-pendiente';
        if (nota >= 65)   return 'nota-aprobado';
        return 'nota-reprobado';
    }

    getNotaLabel(nota: number): string {
        if (nota === 0)   return 'Pendiente';
        if (nota >= 65)   return 'Aprobado';
        return 'Reprobado';
    }
}
