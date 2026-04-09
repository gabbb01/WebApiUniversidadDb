import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ApiUniversidad } from '../../services/api-universidad';
import { Estudiante } from '../../models/estudiante.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

declare var bootstrap: any;

@Component({
    selector: 'app-admin-estudiantes',
    imports: [CommonModule, FormsModule],
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

    constructor(private apiUniversidad: ApiUniversidad, private cdr: ChangeDetectorRef) {}

    ngOnInit(): void {
    }

}
