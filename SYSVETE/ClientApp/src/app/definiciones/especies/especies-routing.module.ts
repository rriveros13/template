import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../helpers/auth.guard';

import { EspeciesPage } from './especies.page';

const routes: Routes = [
  {
    path: '',
    component: EspeciesPage
  },
  {
    path: 'editar-especie/:idEspecie',
    loadChildren: () => import('./editar-especie/editar-especie.module').then( m => m.EditarEspeciePageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EspeciesPageRoutingModule {}
