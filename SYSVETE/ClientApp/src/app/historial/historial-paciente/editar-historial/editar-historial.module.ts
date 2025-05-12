import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarHistorialPageRoutingModule } from './editar-historial-routing.module';

import { EditarHistorialPage } from './editar-historial.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarHistorialPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [EditarHistorialPage]
})
export class EditarHistorialPageModule {}
