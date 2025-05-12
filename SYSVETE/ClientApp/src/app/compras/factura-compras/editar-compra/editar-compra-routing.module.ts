import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarCompraPage } from './editar-compra.page';

const routes: Routes = [
  {
    path: '',
    component: EditarCompraPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarCompraPageRoutingModule {}
