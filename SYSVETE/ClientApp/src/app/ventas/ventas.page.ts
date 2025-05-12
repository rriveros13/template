import { Component, OnInit, ViewChild } from '@angular/core';
import { Venta } from '../models/Venta';
import { VentasService } from '../services/ventas.service';
import { ToastService } from '../helpers/toast.service';
import { PagoVentaComponent } from '../components/pago-venta/pago-venta.component';
import { IonSelect, ModalController } from '@ionic/angular';
import { DetallePagosVentaComponent } from '../components/detalle-pagos-venta/detalle-pagos-venta.component';

@Component({
  selector: 'app-ventas',
  templateUrl: './ventas.page.html',
  styleUrls: ['./ventas.page.scss'],
})
export class VentasPage implements OnInit {

  @ViewChild('estadosSelect', { static: false }) estadosSelect: IonSelect

  public tituloToolbar: string = 'Venta'

  venta: Venta[] = [];
  filtroVenta: Venta[] = [];

  desde: Date
  hasta: Date = new Date();
  estado = - 1

  constructor(
    private service: VentasService,
    private toast: ToastService,
    private modal: ModalController
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.estadosSelect.value = '-1'
    this.obtenerVentas()
  }

  private obtenerVentas() {
    this.service.obtenerVenta()
      .subscribe(res => {
        this.venta = res
        this.filtroVenta = res
      }, error => {
        this.toast.toastError(`Error al obtener Venta: ${error}`)
      })
  }

  confirmarEliminar(venta: Venta) {
    this.toast.alertConfirmarAccion(`Borrar venta`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(venta.idVenta)
        }
      })
  }

  private borrar(id: number) {
    this.service.borrar(id)
      .subscribe(
        res => {
          this.toast.toastExitoso('Compra borrada!')
          this.obtenerVentas()
        },
        error => { this.toast.toastError(error) }
      )
  }

  async pagar(item: Venta) {
    const modal = await this.modal.create({
      component: PagoVentaComponent,
      componentProps: {
        idVenta: item.idVenta
      }
    })

    modal.onDidDismiss()
      .then(res => {
        if (res.role == 'done') {
          this.obtenerVentas()
        }
      })

    return await modal.present();
  }

  async verPagos(item: Venta) {
    const modal = await this.modal.create({
      component: DetallePagosVentaComponent,
      componentProps: {
        idVenta: item.idVenta
      }
    })

    modal.onDidDismiss()
      .then(async res => {
        if (res.role == 'edit') {

          const modalDeuda = await this.modal.create({
            component: PagoVentaComponent,
            componentProps: {
              idVenta: item.idVenta,
              pago: res.data
            }
          })

          modalDeuda.onDidDismiss()
            .then(res => {
              if (res.role == 'done') {
                this.obtenerVentas()
              }
            })

          return await modalDeuda.present()
        }
      })

    return await modal.present();
  }

  filtrarPorEstado(ev: any) {
    let estado = ev.detail.value
    if (estado) {
      this.estado = parseInt(estado)
      this.aplicarFiltro()
    }
  }

  filtroDesde(ev) {
    if (ev.detail.value) {
      this.desde = new Date(ev.detail.value)
      this.aplicarFiltro()
    }
  }

  filtroHasta(ev) {
    if (ev.detail.value) {
      this.hasta = new Date(ev.detail.value)
      this.aplicarFiltro()
    }
  }

  aplicarFiltro() {
    this.filtroVenta = this.venta.filter(registro => {
      const filtroEstado = !this.estado || registro.facturado === (this.estado == 1 ? false : true) || this.estado == -1;

      const filtroFecha = !this.desde || !this.hasta ||
        (new Date(registro.fechaVenta) >= this.desde && new Date(registro.fechaVenta) <= this.hasta);

      return filtroEstado && filtroFecha;
    });

  }
}
