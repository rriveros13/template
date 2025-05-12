import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarPresentacionPageRoutingModule } from './editar-presentacion-routing.module';

import { EditarPresentacionPage } from './editar-presentacion.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarPresentacionPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [EditarPresentacionPage]
})
export class EditarPresentacionPageModule {}
