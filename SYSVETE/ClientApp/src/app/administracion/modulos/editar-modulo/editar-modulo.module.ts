import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarModuloPageRoutingModule } from './editar-modulo-routing.module';

import { EditarModuloPage } from './editar-modulo.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarModuloPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [EditarModuloPage]
})
export class EditarModuloPageModule {}
