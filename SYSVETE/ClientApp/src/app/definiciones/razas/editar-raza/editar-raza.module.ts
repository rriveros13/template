import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarRazaPageRoutingModule } from './editar-raza-routing.module';

import { EditarRazaPage } from './editar-raza.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarRazaPageRoutingModule,
    SharedModule,
    ReactiveFormsModule,
  ],
  declarations: [EditarRazaPage]
})
export class EditarRazaPageModule {}
