import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarTipoInsumoPage } from './editar-tipo-insumo.page';

const routes: Routes = [
  {
    path: '',
    component: EditarTipoInsumoPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarTipoInsumoPageRoutingModule {}
