import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VentasPage } from './ventas.page';

const routes: Routes = [
  {
    path: '',
    component: VentasPage
  },
  {
    path: 'editar-venta/:idVenta',
    loadChildren: () => import('./editar-venta/editar-venta.module').then( m => m.EditarVentaPageModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VentasPageRoutingModule {}
