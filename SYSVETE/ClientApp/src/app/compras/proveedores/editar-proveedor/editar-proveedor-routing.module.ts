import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarProveedorPage } from './editar-proveedor.page';

const routes: Routes = [
  {
    path: '',
    component: EditarProveedorPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarProveedorPageRoutingModule {}
