import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarProcedimientoPageRoutingModule } from './editar-procedimiento-routing.module';

import { EditarProcedimientoPage } from './editar-procedimiento.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarProcedimientoPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [EditarProcedimientoPage]
})
export class EditarProcedimientoPageModule {}
