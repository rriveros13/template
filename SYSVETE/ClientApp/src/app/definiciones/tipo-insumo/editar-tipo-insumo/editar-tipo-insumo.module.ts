import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarTipoInsumoPageRoutingModule } from './editar-tipo-insumo-routing.module';

import { EditarTipoInsumoPage } from './editar-tipo-insumo.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarTipoInsumoPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [EditarTipoInsumoPage]
})
export class EditarTipoInsumoPageModule {}
