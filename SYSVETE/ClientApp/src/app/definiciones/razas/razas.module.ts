import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { RazasPageRoutingModule } from './razas-routing.module';

import { RazasPage } from './razas.page';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RazasPageRoutingModule,
    SharedModule
  ],
  declarations: [RazasPage]
})
export class RazasPageModule {}
