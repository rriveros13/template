import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarTratamientoPage } from './editar-tratamiento.page';

const routes: Routes = [
  {
    path: '',
    component: EditarTratamientoPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarTratamientoPageRoutingModule {}
