import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarMascotaPageRoutingModule } from './editar-mascota-routing.module';

import { EditarMascotaPage } from './editar-mascota.page';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarMascotaPageRoutingModule,
    SharedModule,
    ReactiveFormsModule,
  ],
  declarations: [EditarMascotaPage]
})
export class EditarMascotaPageModule {}
