import { Routes } from '@angular/router';
import { AdminEstudiantes } from './pages/admin-estudiantes/admin-estudiantes';
import { AdminAulas } from './pages/admin-aulas/admin-aulas';
import { AdminAsignaturas } from './pages/admin-asignaturas/admin-asignaturas';
import { AdminProfesores } from './pages/admin-profesores/admin-profesores';
import { Home } from './pages/home/home';

export const routes: Routes = [
    {path: '', component:  Home},
    { path: 'estudiantes', component: AdminEstudiantes },
    { path: 'aulas', component: AdminAulas },
    { path: 'asignaturas', component: AdminAsignaturas },
    { path: 'profesores' , component: AdminProfesores}
];
