import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarPatologiaPage } from './editar-patologia.page';

const routes: Routes = [
  {
    path: '',
    component: EditarPatologiaPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarPatologiaPageRoutingModule {}
