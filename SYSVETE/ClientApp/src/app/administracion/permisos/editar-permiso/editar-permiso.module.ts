import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarPermisoPageRoutingModule } from './editar-permiso-routing.module';

import { EditarPermisoPage } from './editar-permiso.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarPermisoPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [EditarPermisoPage]
})
export class EditarPermisoPageModule {}
