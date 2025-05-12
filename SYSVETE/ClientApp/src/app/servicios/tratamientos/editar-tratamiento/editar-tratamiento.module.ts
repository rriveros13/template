import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarTratamientoPageRoutingModule } from './editar-tratamiento-routing.module';

import { EditarTratamientoPage } from './editar-tratamiento.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarTratamientoPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [EditarTratamientoPage]
})
export class EditarTratamientoPageModule {}
