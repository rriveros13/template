import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarProveedorPageRoutingModule } from './editar-proveedor-routing.module';

import { EditarProveedorPage } from './editar-proveedor.page';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarProveedorPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [EditarProveedorPage]
})
export class EditarProveedorPageModule {}
