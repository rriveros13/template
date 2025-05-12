import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../helpers/auth.guard';

import { VacunasPage } from './vacunas.page';

const routes: Routes = [
  {
    path: '',
    component: VacunasPage
  },
  {
    path: 'editar-vacuna/:idVacuna',
    loadChildren: () => import('./editar-vacuna/editar-vacuna.module').then( m => m.EditarVacunaPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VacunasPageRoutingModule {}
