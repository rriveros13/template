import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../helpers/toast.service';
import { Insumo } from '../../models/Insumo';
import { InsumoService } from '../../services/insumo.service';

@Component({
  selector: 'app-insumos',
  templateUrl: './insumos.page.html',
  styleUrls: ['./insumos.page.scss'],
})
export class InsumosPage implements OnInit {

  tituloToolbar: string = 'Insumos'
  insumos: Insumo[] = [];

  constructor(
    private service: InsumoService,
    private toast: ToastService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerDatos()
  }

  private obtenerDatos() {
    this.service.obtenerInsumos()
      .subscribe(res => {
        this.insumos = res
      }, error => {
        this.toast.toastError('No se pudo obtener insumos');
        console.log(error)
      })
  }

  confirmarEliminar(insumo: Insumo) {
    this.toast.alertConfirmarAccion(`Borrar ${insumo.descripcion}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(insumo.idInsumo)
        }
      })
  }

  private borrar(id: number) {
    this.service.eliminarInsumos(id)
      .subscribe(
        res => {
          this.toast.toastExitoso('Insumo borrado!')
          this.obtenerDatos()
        },
        error => { this.toast.toastError(error) }
      )
  }

}
