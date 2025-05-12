import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { TratamientosPageRoutingModule } from './tratamientos-routing.module';

import { TratamientosPage } from './tratamientos.page';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    TratamientosPageRoutingModule,
    SharedModule
  ],
  declarations: [TratamientosPage]
})
export class TratamientosPageModule {}
