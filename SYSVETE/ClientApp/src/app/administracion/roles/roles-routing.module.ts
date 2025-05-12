import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../helpers/auth.guard';

import { RolesPage } from './roles.page';

const routes: Routes = [
  {
    path: '',
    component: RolesPage
  },
  {
    path: 'editar/:idRol',
    loadChildren: () => import('./editar-rol/editar-rol.module').then(m => m.EditarRolPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RolesPageRoutingModule {}
