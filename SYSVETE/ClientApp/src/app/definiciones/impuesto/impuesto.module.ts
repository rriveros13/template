import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ImpuestoPageRoutingModule } from './impuesto-routing.module';

import { ImpuestoPage } from './impuesto.page';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ImpuestoPageRoutingModule,
    SharedModule,
  ],
  declarations: [ImpuestoPage]
})
export class ImpuestoPageModule {}
