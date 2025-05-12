import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../helpers/auth.guard';

import { ClientesPage } from './clientes.page';

const routes: Routes = [
  {
    path: '',
    component: ClientesPage
  },
  {
    path: 'editar-cliente/:idCliente',
    loadChildren: () => import('./editar-cliente/editar-cliente.module').then(m => m.EditarClientePageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ClientesPageRoutingModule { }
