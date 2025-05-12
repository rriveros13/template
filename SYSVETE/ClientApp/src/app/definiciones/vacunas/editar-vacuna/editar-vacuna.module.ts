import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarVacunaPageRoutingModule } from './editar-vacuna-routing.module';

import { EditarVacunaPage } from './editar-vacuna.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarVacunaPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [EditarVacunaPage]
})
export class EditarVacunaPageModule {}
