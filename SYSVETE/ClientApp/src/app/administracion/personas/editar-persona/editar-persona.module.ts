import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarPersonaPageRoutingModule } from './editar-persona-routing.module';

import { EditarPersonaPage } from './editar-persona.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  exports: [
  ],
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarPersonaPageRoutingModule,
    SharedModule,
  ],
  declarations: [
    EditarPersonaPage,
  ]
})
export class EditarPersonaPageModule { }
