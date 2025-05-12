import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { DefinicionesPageRoutingModule } from './definiciones-routing.module';

import { DefinicionesPage } from './definiciones.page';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    DefinicionesPageRoutingModule,
    SharedModule
  ],
  declarations: [DefinicionesPage]
})
export class DefinicionesPageModule {}
