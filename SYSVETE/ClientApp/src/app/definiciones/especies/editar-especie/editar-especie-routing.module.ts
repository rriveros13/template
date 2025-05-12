import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarEspeciePage } from './editar-especie.page';

const routes: Routes = [
  {
    path: '',
    component: EditarEspeciePage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarEspeciePageRoutingModule {}
