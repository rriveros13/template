import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../helpers/toast.service';
import { Presentacion } from '../../models/Presentacion';
import { PresentacionService } from '../../services/presentacion.service';

@Component({
  selector: 'app-presentacion',
  templateUrl: './presentacion.page.html',
  styleUrls: ['./presentacion.page.scss'],
})
export class PresentacionPage implements OnInit {

  tituloToolbar: string = 'Presentaciones'
  presentaciones: Presentacion[] = [];

  constructor(
    private service: PresentacionService,
    private toast: ToastService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerPresentaciones()
  }

  private obtenerPresentaciones() {
    this.service.obtenerPresentaciones()
      .subscribe(res => {
        this.presentaciones = res
      }, error => {
        this.toast.toastError('No se pudo obtener presentaciones');
        console.log(error)
      })
  }

  confirmarEliminar(presentacion: Presentacion) {
    this.toast.alertConfirmarAccion(`Borrar ${presentacion.descripcion}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(presentacion.idPresentacion)
        }
      })
  }

  private borrar(idPresentacion: number) {
    this.service.eliminarPresentacion(idPresentacion)
      .subscribe(
        res => {
          this.toast.toastExitoso('Presentacion borrada!')
          this.obtenerPresentaciones()
        },
        error => { this.toast.toastError(error) }
      )
  }

}
