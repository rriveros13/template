import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarProcedimientoPage } from './editar-procedimiento.page';

const routes: Routes = [
  {
    path: '',
    component: EditarProcedimientoPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarProcedimientoPageRoutingModule {}
