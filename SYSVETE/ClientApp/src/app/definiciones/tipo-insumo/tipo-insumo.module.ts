import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { TipoInsumoPageRoutingModule } from './tipo-insumo-routing.module';

import { TipoInsumoPage } from './tipo-insumo.page';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    TipoInsumoPageRoutingModule,
    SharedModule
  ],
  declarations: [TipoInsumoPage]
})
export class TipoInsumoPageModule {}
