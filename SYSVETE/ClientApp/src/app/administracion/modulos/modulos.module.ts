import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ModulosPageRoutingModule } from './modulos-routing.module';

import { ModulosPage } from './modulos.page';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ModulosPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [ModulosPage]
})
export class ModulosPageModule {}
