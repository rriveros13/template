import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarImpuestoPageRoutingModule } from './editar-impuesto-routing.module';

import { EditarImpuestoPage } from './editar-impuesto.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarImpuestoPageRoutingModule,
    SharedModule,
    ReactiveFormsModule,
  ],
  declarations: [EditarImpuestoPage]
})
export class EditarImpuestoPageModule {}
