import { Component, Input, OnInit } from '@angular/core';
import { DeudaProveedor } from '../../models/DeudaProveedor';
import { PagoProveedorService } from '../../services/pago-proveedor.service';
import { ToastService } from '../../helpers/toast.service';
import { ComprasService } from '../../services/compras.service';
import { registerLocaleData } from '@angular/common';
import es from '@angular/common/locales/es';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-detalle-pagos-proveedor',
  templateUrl: './detalle-pagos-proveedor.component.html',
  styleUrls: ['./detalle-pagos-proveedor.component.scss'],
})
export class DetallePagosProveedorComponent implements OnInit {

  @Input() idCompra: number
  pagos: DeudaProveedor[] = [];

  totalPagar: number = 0
  totalPagado: number = 0
  pendiente: number = 0


  constructor(
    private service: PagoProveedorService,
    private compraService: ComprasService,
    private toast: ToastService,
    private modal: ModalController
  ) { }

  ngOnInit() {
    registerLocaleData(es)
    this.obtenerPagos()
  }

  private obtenerPagos() {
    this.service.obtenerDeudasPorCompra(this.idCompra)
      .subscribe(res => {
        this.pagos = res
      }, error => {
        this.toast.toastError('Error al obtener pago de deudas!');
      })

    this.compraService.obtenerCompraConMontosPorId(this.idCompra)
      .subscribe(res => {
        this.totalPagar = res.montoTotal
        this.totalPagado = res.montoAbonado
        this.pendiente = res.saldoPendiente

      }, error => {
        this.toast.toastError('Error al obtener pago de deudas!');
      })
  }

  async editarDetalle(item: DeudaProveedor) {
    this.dismiss('edit', item)
  }

  confirmarEliminar(deuda: DeudaProveedor) {
    this.toast.alertConfirmarAccion(`Borrar pago`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(deuda.idDeuda)
        }
      })
  }

  private borrar(id: number) {
    this.service.eliminarPagoDeuda(id)
      .subscribe(
        res => {
          this.toast.toastExitoso('Pago borrado!')
          this.obtenerPagos()
        },
        error => { this.toast.toastError(error) }
      )
  }

  cerrar() {
    this.dismiss('cancel')
  }

  private dismiss(role, data?: any) {
    this.modal.dismiss(data, role)
  }
}
