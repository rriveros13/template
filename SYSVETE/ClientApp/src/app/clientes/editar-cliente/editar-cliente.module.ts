import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarClientePageRoutingModule } from './editar-cliente-routing.module';

import { EditarClientePage } from './editar-cliente.page';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  exports: [
  ],
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarClientePageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [
    EditarClientePage,
  ]
})
export class EditarClientePageModule { }
