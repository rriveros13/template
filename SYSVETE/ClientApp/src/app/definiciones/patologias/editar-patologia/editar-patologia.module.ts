import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarPatologiaPageRoutingModule } from './editar-patologia-routing.module';

import { EditarPatologiaPage } from './editar-patologia.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarPatologiaPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [EditarPatologiaPage]
})
export class EditarPatologiaPageModule {}
