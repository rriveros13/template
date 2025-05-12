import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ComprasPage } from './compras.page';
import { AuthGuard } from '../helpers/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: ComprasPage
  },
  {
    path: 'proveedores',
    loadChildren: () => import('./proveedores/proveedores.module').then( m => m.ProveedoresPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'factura-compras',
    loadChildren: () => import('./factura-compras/factura-compras.module').then(m => m.FacturaComprasPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'stock',
    loadChildren: () => import('./stock/stock.module').then( m => m.StockPageModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ComprasPageRoutingModule {}
