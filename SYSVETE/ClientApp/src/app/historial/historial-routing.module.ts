import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HistorialPage } from './historial.page';

const routes: Routes = [
  {
    path: '',
    component: HistorialPage
  },
  {
    path: 'historial-paciente/:idPaciente',
    loadChildren: () => import('./historial-paciente/historial-paciente.module').then( m => m.HistorialPacientePageModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HistorialPageRoutingModule {}
