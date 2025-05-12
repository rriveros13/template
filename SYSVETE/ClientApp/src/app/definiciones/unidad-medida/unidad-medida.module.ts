import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { UnidadMedidaPageRoutingModule } from './unidad-medida-routing.module';

import { UnidadMedidaPage } from './unidad-medida.page';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    UnidadMedidaPageRoutingModule,
    SharedModule
  ],
  declarations: [UnidadMedidaPage]
})
export class UnidadMedidaPageModule {}
