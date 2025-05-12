import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../helpers/auth.guard';

import { PresentacionPage } from './presentacion.page';

const routes: Routes = [
  {
    path: '',
    component: PresentacionPage
  },
  {
    path: 'editar-presentacion/:idPresentacion',
    loadChildren: () => import('./editar-presentacion/editar-presentacion.module').then( m => m.EditarPresentacionPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PresentacionPageRoutingModule {}
