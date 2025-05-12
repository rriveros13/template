import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HistorialPacientePage } from './historial-paciente.page';
import { AuthGuard } from '../../helpers/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: HistorialPacientePage
  },
  {
    path: 'editar-historial/:idHistorial',
    loadChildren: () => import('./editar-historial/editar-historial.module').then(m => m.EditarHistorialPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HistorialPacientePageRoutingModule {}
