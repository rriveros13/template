import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProveedoresPage } from './proveedores.page';
import { AuthGuard } from '../../helpers/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: ProveedoresPage
  },
  {
    path: 'editar-proveedor/:idProveedor',
    loadChildren: () => import('./editar-proveedor/editar-proveedor.module').then( m => m.EditarProveedorPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProveedoresPageRoutingModule {}
