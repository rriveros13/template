import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarRolPageRoutingModule } from './editar-rol-routing.module';

import { EditarRolPage } from './editar-rol.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarRolPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [EditarRolPage]
})
export class EditarRolPageModule {}
