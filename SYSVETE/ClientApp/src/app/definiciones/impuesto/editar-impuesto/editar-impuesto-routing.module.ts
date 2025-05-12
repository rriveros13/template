import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarImpuestoPage } from './editar-impuesto.page';

const routes: Routes = [
  {
    path: '',
    component: EditarImpuestoPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarImpuestoPageRoutingModule {}
