import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarVentaPageRoutingModule } from './editar-venta-routing.module';

import { EditarVentaPage } from './editar-venta.page';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarVentaPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [EditarVentaPage]
})
export class EditarVentaPageModule {}
