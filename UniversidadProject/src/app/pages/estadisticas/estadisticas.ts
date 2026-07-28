import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiUniversidad } from '../../services/api-universidad';
import { EstadisticaGenerales, PromedioEstudiante } from '../../models/estadistica.model';
import { Matricula } from '../../models/matricula.model';

@Component({
    selector: 'app-estadisticas',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './estadisticas.html',
    styleUrl: './estadisticas.css'
})
export class Estadisticas implements OnInit {

    estadisticas: EstadisticaGenerales = new EstadisticaGenerales();
    promedios: PromedioEstudiante[] = [];
    todasLasMatriculas: Matricula[] = [];
    cargando: boolean = false;

    // ── FUNCIONAL: map de notas a resultado
    readonly MAP_NOTA_RESULTADO = (nota: number): string => {
        if (nota === 0)  return 'Sin nota';
        if (nota >= 65)  return 'Aprobado ✅';
        return 'Reprobado ❌';
    };

    // ── FUNCIONAL: reduce para nota más alta (con nota > 0)
    get notaMasAlta(): Matricula | null {
        const conNota = this.todasLasMatriculas.filter(m => Number(m.nota) > 0);
        if (conNota.length === 0) return null;
        return conNota.reduce((max, m) => Number(m.nota) > Number(max.nota) ? m : max);
    }

    // ── FUNCIONAL: reduce para nota más baja (con nota > 0)
    get notaMasBaja(): Matricula | null {
        const conNota = this.todasLasMatriculas.filter(m => Number(m.nota) > 0);
        if (conNota.length === 0) return null;
        return conNota.reduce((min, m) => Number(m.nota) < Number(min.nota) ? m : min);
    }

    // ── FUNCIONAL: reduce — suma total de notas individuales
    get sumaTotal(): number {
        return this.todasLasMatriculas
            .filter(m => Number(m.nota) > 0)
            .reduce((acc, m) => acc + Number(m.nota), 0);
    }

    get totalConNota(): number {
        return this.todasLasMatriculas.filter(m => Number(m.nota) > 0).length;
    }

    constructor(
        private apiUniversidad: ApiUniversidad,
        private cdr: ChangeDetectorRef
    ) { }

    ngOnInit(): void {
        this.cargarDatos();
    }

    cargarDatos(): void {
        this.cargando = true;

        this.apiUniversidad.obtenerEstadisticasGenerales().subscribe({
            next: (data) => {
                this.estadisticas = data;
                this.cargando = false;
                this.cdr.detectChanges();
            },
            error: () => { this.cargando = false; }
        });

        this.apiUniversidad.obtenerPromediosPorEstudiante().subscribe({
            next: (data) => {
                this.promedios = data;
                this.cdr.detectChanges();
            }
        });

        // Trae todas las matrículas individuales para calcular max/min reales
        this.apiUniversidad.obtenerMatriculas().subscribe({
            next: (data) => {
                this.todasLasMatriculas = data;
                this.cdr.detectChanges();
            }
        });
    }

    getBarWidth(nota: number): string {
        return Math.max(0, Math.min(100, nota)) + '%';
    }

    getNotaClass(nota: number): string {
        if (nota === 0)  return 'pendiente';
        if (nota >= 65)  return 'aprobado';
        return 'reprobado';
    }

    getLetra(nota: number): string {
        return this.MAP_NOTA_RESULTADO(nota);
    }

    getClasificacionIcon(clasificacion: string): string {
        const icons: Record<string, string> = {
            'Excelente': '🏆',
            'Muy Bueno': '⭐',
            'Bueno': '✅',
            'Suficiente': '📊',
            'Insuficiente': '⚠️'
        };
        return icons[clasificacion] || '📊';
    }
}
