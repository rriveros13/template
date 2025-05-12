import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../helpers/auth.guard';

import { UsuariosPage } from './usuarios.page';

const routes: Routes = [
  {
    path: '',
    component: UsuariosPage
  },
  {
    path: 'perfil/:idUsuario',
    loadChildren: () => import('./perfil/perfil.module').then(m => m.PerfilPageModule),
    canActivate: [AuthGuard]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UsuariosPageRoutingModule {}
