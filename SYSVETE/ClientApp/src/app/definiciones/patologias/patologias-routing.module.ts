import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../helpers/auth.guard';

import { PatologiasPage } from './patologias.page';

const routes: Routes = [
  {
    path: '',
    component: PatologiasPage
  },
  {
    path: 'editar-patologia/:idPatologia',
    loadChildren: () => import('./editar-patologia/editar-patologia.module').then( m => m.EditarPatologiaPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PatologiasPageRoutingModule {}
