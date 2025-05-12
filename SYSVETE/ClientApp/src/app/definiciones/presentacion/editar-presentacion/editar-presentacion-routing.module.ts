import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarPresentacionPage } from './editar-presentacion.page';

const routes: Routes = [
  {
    path: '',
    component: EditarPresentacionPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarPresentacionPageRoutingModule {}
