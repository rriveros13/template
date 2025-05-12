import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../helpers/auth.guard';

import { ModulosPage } from './modulos.page';

const routes: Routes = [
  {
    path: '',
    component: ModulosPage
  },
  {
    path: 'editar-modulo/:idModulo',
    loadChildren: () => import('./editar-modulo/editar-modulo.module').then(m => m.EditarModuloPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModulosPageRoutingModule {}
