import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../helpers/auth.guard';

import { UnidadMedidaPage } from './unidad-medida.page';

const routes: Routes = [
  {
    path: '',
    component: UnidadMedidaPage
  },
  {
    path: 'editar-unidad-medida/:idUnidad',
    loadChildren: () => import('./editar-unidad-medida/editar-unidad-medida.module').then(m => m.EditarUnidadMedidaPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UnidadMedidaPageRoutingModule {}
