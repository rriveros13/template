import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarUnidadMedidaPageRoutingModule } from './editar-unidad-medida-routing.module';

import { EditarUnidadMedidaPage } from './editar-unidad-medida.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarUnidadMedidaPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [EditarUnidadMedidaPage]
})
export class EditarUnidadMedidaPageModule {}
