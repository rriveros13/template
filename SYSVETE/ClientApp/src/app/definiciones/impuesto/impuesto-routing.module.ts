import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../helpers/auth.guard';

import { ImpuestoPage } from './impuesto.page';

const routes: Routes = [
  {
    path: '',
    component: ImpuestoPage
  },
  {
    path: 'editar-impuesto/:idImpuesto',
    loadChildren: () => import('./editar-impuesto/editar-impuesto.module').then( m => m.EditarImpuestoPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ImpuestoPageRoutingModule {}
