import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { PatologiasPageRoutingModule } from './patologias-routing.module';

import { PatologiasPage } from './patologias.page';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    PatologiasPageRoutingModule,
    SharedModule
  ],
  declarations: [PatologiasPage]
})
export class PatologiasPageModule {}
