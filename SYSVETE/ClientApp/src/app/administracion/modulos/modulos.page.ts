import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../helpers/toast.service';
import { Modulo } from '../../models/Modulo';
import { ModuloService } from '../../services/modulo.service';

@Component({
  selector: 'app-modulos',
  templateUrl: './modulos.page.html',
  styleUrls: ['./modulos.page.scss'],
})
export class ModulosPage implements OnInit {

  public tituloToolbar: string = 'Modulos'

  modulos: Modulo[] = []

  constructor(
    private service: ModuloService,
    private toast: ToastService,
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.service.obtenerModulos()
      .subscribe(res => this.modulos = res,
        error => {
          this.toast.toastError(`Error al intentar traer modulos:${error}`)
          console.log(error)
        })
  }

  confirmarBorrar(modulo: Modulo) {
    this.toast.alertConfirmarAccion(`Borrar ${modulo.nombre}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(modulo.idModulo)
        }
      })
  }

  private borrar(idModulo: number) {
    this.service.eliminarModulo(idModulo)
      .subscribe(
        res => {
          this.toast.toastExitoso('Modulo Borrado')
        },
        error => { this.toast.toastError(error) }
      )

  }

}
