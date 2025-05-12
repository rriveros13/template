import { Component, Input, OnInit } from '@angular/core';
import { PagoVenta } from '../../models/PagoVenta';
import { PagoVentaService } from '../../services/pago-venta.service';
import { ToastService } from '../../helpers/toast.service';
import { ModalController } from '@ionic/angular';
import { registerLocaleData } from '@angular/common';
import es from '@angular/common/locales/es';
import { VentasService } from '../../services/ventas.service';

@Component({
  selector: 'app-detalle-pagos-venta',
  templateUrl: './detalle-pagos-venta.component.html',
  styleUrls: ['./detalle-pagos-venta.component.scss'],
})
export class DetallePagosVentaComponent implements OnInit {

  @Input() idVenta: number
  pagos: PagoVenta[] = [];

  totalPagar: number = 0
  totalPagado: number = 0
  pendiente: number = 0

  constructor(
    private service: PagoVentaService,
    private toast: ToastService,
    private modal: ModalController,
    private serviceVentas: VentasService

  ) { }

  ngOnInit() {
    registerLocaleData(es)
    this.obtenerPagos()
  }

  obtenerPagos() {
    this.service.obtenerPagosPorVenta(this.idVenta)
      .subscribe(res => {
        this.pagos = res
      }, error => {
        this.toast.toastError('Error al obtener pagos!');
      })

    this.serviceVentas.obtenerMontoTotal(this.idVenta)
      .subscribe(res => {

        this.totalPagar = res.montoTotal
        this.totalPagado = res.montoAbonado
        this.pendiente = res.saldoPendiente

      }, error => {
        this.toast.toastError('Error al obtener pagos!');
      })
  }

  cerrar() {
    this.dismiss('cancel')
  }

  async editarDetalle(item: PagoVenta) {
    this.dismiss('edit', item)
  }

  confirmarEliminar(deuda: PagoVenta) {
    this.toast.alertConfirmarAccion(`Borrar pago`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(deuda.idPago)
        }
      })
  }

  private borrar(id: number) {
    this.service.eliminarPagosPorVenta(id)
      .subscribe(
        res => {
          this.toast.toastExitoso('Pago borrado!')
          this.obtenerPagos()
        },
        error => { this.toast.toastError(error) }
      )
  }

  private dismiss(role, data?: any) {
    this.modal.dismiss(data, role)
  }
}
