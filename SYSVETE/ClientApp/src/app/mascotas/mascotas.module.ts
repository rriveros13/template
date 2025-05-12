import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { MascotasPageRoutingModule } from './mascotas-routing.module';

import { MascotasPage } from './mascotas.page';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    MascotasPageRoutingModule,
    SharedModule
  ],
  declarations: [MascotasPage]
})
export class MascotasPageModule {}
