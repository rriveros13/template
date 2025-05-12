import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../helpers/auth.guard';

import { TipoInsumoPage } from './tipo-insumo.page';

const routes: Routes = [
  {
    path: '',
    component: TipoInsumoPage
  },
  {
    path: 'editar-tipo-insumo/:idTipoInsumo',
    loadChildren: () => import('./editar-tipo-insumo/editar-tipo-insumo.module').then( m => m.EditarTipoInsumoPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TipoInsumoPageRoutingModule {}
