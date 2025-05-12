import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarInsumoPage } from './editar-insumo.page';

const routes: Routes = [
  {
    path: '',
    component: EditarInsumoPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarInsumoPageRoutingModule {}
