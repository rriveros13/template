import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarVacunaPage } from './editar-vacuna.page';

const routes: Routes = [
  {
    path: '',
    component: EditarVacunaPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarVacunaPageRoutingModule {}
