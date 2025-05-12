import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PersonasPage } from './personas.page';

const routes: Routes = [
  {
    path: '',
    component: PersonasPage
  },
  {
    path: 'editar-persona/:idPersona',
    loadChildren: () => import('./editar-persona/editar-persona.module').then( m => m.EditarPersonaPageModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PersonasPageRoutingModule {}
