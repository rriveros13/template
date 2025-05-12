import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ReportesPageRoutingModule } from './reportes-routing.module';

import { ReportesPage } from './reportes.page';
import { SharedModule } from '../shared/shared.module';
import { DesdeHastaComponent } from '../shared/desde-hasta/desde-hasta.component';
import { ModalProveedorComponent } from '../components/modal-proveedor/modal-proveedor.component';
import { ModalPatologiasComponent } from '../components/modal-patologias/modal-patologias.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ReportesPageRoutingModule,
    SharedModule
  ],
  declarations: [
    ReportesPage,
    DesdeHastaComponent,
    ModalProveedorComponent,
    ModalPatologiasComponent
  ],
  exports: [
    ReportesPage,
    DesdeHastaComponent,
    ModalProveedorComponent,
    ModalPatologiasComponent
  ],
})
export class ReportesPageModule { }
