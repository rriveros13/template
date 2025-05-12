import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../helpers/auth.guard';

import { RazasPage } from './razas.page';

const routes: Routes = [
  {
    path: '',
    component: RazasPage
  },
  {
    path: 'editar-raza/:idRaza',
    loadChildren: () => import('./editar-raza/editar-raza.module').then( m => m.EditarRazaPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RazasPageRoutingModule {}
