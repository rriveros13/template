import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarUnidadMedidaPage } from './editar-unidad-medida.page';

const routes: Routes = [
  {
    path: '',
    component: EditarUnidadMedidaPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarUnidadMedidaPageRoutingModule {}
