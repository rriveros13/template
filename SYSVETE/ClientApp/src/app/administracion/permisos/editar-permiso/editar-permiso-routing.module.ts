import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EditarPermisoPage } from './editar-permiso.page';

const routes: Routes = [
  {
    path: '',
    component: EditarPermisoPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarPermisoPageRoutingModule {}
