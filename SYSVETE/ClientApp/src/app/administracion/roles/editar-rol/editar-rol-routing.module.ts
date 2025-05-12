import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarRolPage } from './editar-rol.page';

const routes: Routes = [
  {
    path: '',
    component: EditarRolPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarRolPageRoutingModule {}
