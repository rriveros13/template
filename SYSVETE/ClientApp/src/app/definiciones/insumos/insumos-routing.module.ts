import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../helpers/auth.guard';

import { InsumosPage } from './insumos.page';

const routes: Routes = [
  {
    path: '',
    component: InsumosPage
  },
  {
    path: 'editar-insumo/:idInsumo',
    loadChildren: () => import('./editar-insumo/editar-insumo.module').then( m => m.EditarInsumoPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InsumosPageRoutingModule {}
