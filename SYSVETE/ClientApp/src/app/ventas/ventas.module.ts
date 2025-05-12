import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { VentasPageRoutingModule } from './ventas-routing.module';

import { VentasPage } from './ventas.page';
import { SharedModule } from '../shared/shared.module';
import { PagoVentaComponent } from '../components/pago-venta/pago-venta.component';
import { DetallePagosVentaComponent } from '../components/detalle-pagos-venta/detalle-pagos-venta.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    VentasPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [
    VentasPage,
    PagoVentaComponent,
    DetallePagosVentaComponent
  ],
  exports: [
    VentasPage,
    PagoVentaComponent,
    DetallePagosVentaComponent
  ]
})
export class VentasPageModule {}
