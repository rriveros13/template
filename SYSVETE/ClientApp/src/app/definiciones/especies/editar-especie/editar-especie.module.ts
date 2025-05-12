import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarEspeciePageRoutingModule } from './editar-especie-routing.module';

import { EditarEspeciePage } from './editar-especie.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarEspeciePageRoutingModule,
    ReactiveFormsModule
  ],
  declarations: [EditarEspeciePage]
})
export class EditarEspeciePageModule {}
