import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { FacturaComprasPageRoutingModule } from './factura-compras-routing.module';

import { FacturaComprasPage } from './factura-compras.page';
import { SharedModule } from '../../shared/shared.module';
import { PagoProveedorComponent } from '../../components/pago-proveedor/pago-proveedor.component';
import { DetallePagosProveedorComponent } from '../../components/detalle-pagos-proveedor/detalle-pagos-proveedor.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    FacturaComprasPageRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [
    FacturaComprasPage,
    PagoProveedorComponent,
    DetallePagosProveedorComponent
  ],
  exports: [
    FacturaComprasPage,
    PagoProveedorComponent,
    DetallePagosProveedorComponent
  ]
})
export class FacturaComprasPageModule { }
