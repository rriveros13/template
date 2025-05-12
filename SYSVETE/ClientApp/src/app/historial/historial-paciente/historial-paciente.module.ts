import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { HistorialPacientePageRoutingModule } from './historial-paciente-routing.module';

import { HistorialPacientePage } from './historial-paciente.page';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    HistorialPacientePageRoutingModule,
    SharedModule
  ],
  declarations: [HistorialPacientePage]
})
export class HistorialPacientePageModule {}
