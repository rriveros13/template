import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ServiciosPage } from './servicios.page';
import { AuthGuard } from '../helpers/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: ServiciosPage
  },
  {
    path: 'tratamientos',
    loadChildren: () => import('./tratamientos/tratamientos.module').then(m => m.TratamientosPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'procedimientos',
    loadChildren: () => import('./procedimientos/procedimientos.module').then(m => m.ProcedimientosPageModule),
    canActivate: [AuthGuard]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ServiciosPageRoutingModule {}
