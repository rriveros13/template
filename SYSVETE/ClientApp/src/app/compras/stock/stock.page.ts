import { Component, OnInit } from '@angular/core';
import { StockInsumo } from '../../models/StockInsumo';
import { StockInsumosService } from '../../services/stock-insumos.service';
import { ToastService } from '../../helpers/toast.service';
import { ModalController } from '@ionic/angular';
import { AjusteInsumoComponent } from '../../components/ajuste-insumo/ajuste-insumo.component';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.page.html',
  styleUrls: ['./stock.page.scss'],
})
export class StockPage implements OnInit {

  tituloToolbar: string = 'Stock'

  stock: StockInsumo[] = [];
  constructor(
    private service: StockInsumosService,
    private toast: ToastService,
    private modal: ModalController,
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerDatos()
  }

  private obtenerDatos() {
    this.service.obtenerstock()
      .subscribe(res => {
        this.stock = res
      }, error => {
        this.toast.toastError(`Error al obtener insumos de stock: ${error}`)
      })
  }

  async ajuste(item: StockInsumo) {
    const modal = await this.modal.create({
      component: AjusteInsumoComponent,
      componentProps: {
        ajuste: item
      }
    })

    modal.onDidDismiss()
      .then(res => {
        if (res.role == 'done') {
          this.obtenerDatos()
        }
      })

    return await modal.present();
  }
}
