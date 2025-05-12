import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProcedimientosPage } from './procedimientos.page';
import { AuthGuard } from '../../helpers/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: ProcedimientosPage
  },
  {
    path: 'editar-procedimiento/:idProcedimiento',
    loadChildren: () => import('./editar-procedimiento/editar-procedimiento.module').then(m => m.EditarProcedimientoPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProcedimientosPageRoutingModule { }
