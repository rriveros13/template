import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../helpers/auth.guard';

import { MascotasPage } from './mascotas.page';

const routes: Routes = [
  {
    path: '',
    component: MascotasPage
  },
  {
    path: 'editar-mascota/:idPaciente',
    loadChildren: () => import('./editar-mascota/editar-mascota.module').then( m => m.EditarMascotaPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MascotasPageRoutingModule {}
