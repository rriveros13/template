import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { StockPageRoutingModule } from './stock-routing.module';

import { StockPage } from './stock.page';
import { SharedModule } from '../../shared/shared.module';
import { AjusteInsumoComponent } from '../../components/ajuste-insumo/ajuste-insumo.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    StockPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [
    StockPage,
    AjusteInsumoComponent
  ],
  exports: [
    AjusteInsumoComponent
  ]
})
export class StockPageModule {}
