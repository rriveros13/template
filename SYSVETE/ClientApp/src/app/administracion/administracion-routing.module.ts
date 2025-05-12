import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../helpers/auth.guard';

import { AdministracionPage } from './administracion.page';

const routes: Routes = [
  {
    path: '',
    component: AdministracionPage
  },
  {
    path: 'roles',
    loadChildren: () => import('./roles/roles.module').then(m => m.RolesPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'permisos',
    loadChildren: () => import('./permisos/permisos.module').then(m => m.PermisosPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'modulos',
    loadChildren: () => import('./modulos/modulos.module').then(m => m.ModulosPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'personas',
    loadChildren: () => import('./personas/personas.module').then(m => m.PersonasPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdministracionPageRoutingModule {}
