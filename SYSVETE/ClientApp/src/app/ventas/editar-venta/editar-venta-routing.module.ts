import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarVentaPage } from './editar-venta.page';

const routes: Routes = [
  {
    path: '',
    component: EditarVentaPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarVentaPageRoutingModule {}
