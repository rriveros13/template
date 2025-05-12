import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../helpers/auth.guard';

import { TratamientosPage } from './tratamientos.page';

const routes: Routes = [
  {
    path: '',
    component: TratamientosPage
  },
  {
    path: 'editar-tratamiento/:idTratamiento',
    loadChildren: () => import('./editar-tratamiento/editar-tratamiento.module').then( m => m.EditarTratamientoPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TratamientosPageRoutingModule {}
