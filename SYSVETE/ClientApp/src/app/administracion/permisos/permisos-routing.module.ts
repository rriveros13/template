import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../helpers/auth.guard';

import { PermisosPage } from './permisos.page';

const routes: Routes = [
  {
    path: '',
    component: PermisosPage
  },
  {
    path: 'editar-permiso/:idPermiso',
    loadChildren: () => import('./editar-permiso/editar-permiso.module').then( m => m.EditarPermisoPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PermisosPageRoutingModule {}
