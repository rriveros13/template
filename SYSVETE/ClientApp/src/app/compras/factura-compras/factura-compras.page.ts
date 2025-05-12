import { Component, OnInit, ViewChild } from '@angular/core';
import { Compra } from '../../models/Compra';
import { ComprasService } from '../../services/compras.service';
import { ToastService } from '../../helpers/toast.service';
import { IonSelect, ModalController } from '@ionic/angular';
import { PagoProveedorComponent } from '../../components/pago-proveedor/pago-proveedor.component';
import { DetallePagosProveedorComponent } from '../../components/detalle-pagos-proveedor/detalle-pagos-proveedor.component';
import { InformesService } from '../../services/informes.service';

@Component({
  selector: 'app-factura-compras',
  templateUrl: './factura-compras.page.html',
  styleUrls: ['./factura-compras.page.scss'],
})
export class FacturaComprasPage implements OnInit {

  @ViewChild('estadosSelect', { static: false }) estadosSelect: IonSelect

  public tituloToolbar: string = 'Compras'

  compras: Compra[] = [];
  filtroCompra: Compra[] = [];

  desde: Date
  hasta: Date = new Date();
  estado = - 1

  constructor(
    private service: ComprasService,
    private toast: ToastService,
    private modal: ModalController,
    private informe: InformesService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.estadosSelect.value = '-1'
    this.obtenerCompras()
  }

  private obtenerCompras() {
    this.service.obtenerCompras()
      .subscribe(res => {
        this.compras = res
        this.filtroCompra = res
      }, error => {
        this.toast.toastError(`Error al obtener compras: ${error}`)
      })
  }

  confirmarEliminar(compra: Compra) {
    this.toast.alertConfirmarAccion(`Borrar compra`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(compra.idCompra)
        }
      })
  }

  private borrar(id: number) {
    this.service.borrarCompra(id)
      .subscribe(
        res => {
          this.toast.toastExitoso('Compra borrada!')
          this.obtenerCompras()
        },
        error => { this.toast.toastError(error) }
      )
  }

  async pagarDeuda(compra: Compra) {
    const modal = await this.modal.create({
      component: PagoProveedorComponent,
      componentProps: {
        compra: compra
      }
    })

    modal.onDidDismiss()
      .then(res => {
        if (res.role == 'done') {
          this.obtenerCompras()
        }
      })

    return await modal.present();

  }

  async verPagos(compra: Compra) {
    const modal = await this.modal.create({
      component: DetallePagosProveedorComponent,
      componentProps: {
        idCompra: compra.idCompra
      }
    })

    modal.onDidDismiss()
      .then(async res => {
        if (res.role == 'edit') {

          const modalDeuda = await this.modal.create({
            component: PagoProveedorComponent,
            componentProps: {
              compra: compra,
              deuda: res.data
            }
          })

          modal.onDidDismiss()
            .then(res => {
              if (res.role == 'done') {
                this.obtenerCompras()
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
    this.filtroCompra = this.compras.filter(registro => {
      const filtroEstado = !this.estado || registro.facturado === (this.estado == 1 ? false : true) || this.estado == -1;

      const filtroFecha = !this.desde || !this.hasta ||
        (new Date(registro.fechaCompra) >= this.desde && new Date(registro.fechaCompra) <= this.hasta);

      return filtroEstado && filtroFecha;
    });

  }

}
