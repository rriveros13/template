import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { EditarCompraPageRoutingModule } from './editar-compra-routing.module';

import { EditarCompraPage } from './editar-compra.page';
import { SharedModule } from '../../../shared/shared.module';
import { ModalEditarCompraComponent } from '../../../components/modal-editar-compra/modal-editar-compra.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    EditarCompraPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [
    EditarCompraPage,
    ModalEditarCompraComponent
  ],
  exports: [
    EditarCompraPage,
    ModalEditarCompraComponent
  ],
})
export class EditarCompraPageModule {}
