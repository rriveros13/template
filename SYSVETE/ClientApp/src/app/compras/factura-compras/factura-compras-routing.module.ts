import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { FacturaComprasPage } from './factura-compras.page';
import { AuthGuard } from '../../helpers/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: FacturaComprasPage
  },
  {
    path: 'editar-compra/:idCompra',
    loadChildren: () => import('./editar-compra/editar-compra.module').then(m => m.EditarCompraPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class FacturaComprasPageRoutingModule {}
