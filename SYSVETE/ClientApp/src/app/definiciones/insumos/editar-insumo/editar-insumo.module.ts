import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarInsumoPageRoutingModule } from './editar-insumo-routing.module';

import { EditarInsumoPage } from './editar-insumo.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarInsumoPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [EditarInsumoPage]
})
export class EditarInsumoPageModule {}
