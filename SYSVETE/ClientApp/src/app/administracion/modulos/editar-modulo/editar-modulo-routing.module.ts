import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarModuloPage } from './editar-modulo.page';

const routes: Routes = [
  {
    path: '',
    component: EditarModuloPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarModuloPageRoutingModule {}
