import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../helpers/toast.service';
import { Raza } from '../../models/Raza';
import { RazaService } from '../../services/raza.service';

@Component({
  selector: 'app-razas',
  templateUrl: './razas.page.html',
  styleUrls: ['./razas.page.scss'],
})
export class RazasPage implements OnInit {

  tituloToolbar: string = 'Razas'

  razas: Raza[] = [];
  constructor(
    private service: RazaService,
    private toast: ToastService,
  ) { }

  ionViewWillEnter() {
    this.obtenerRazas()
  }

  ngOnInit() {
  }

  confirmarBorrar(raza: Raza) {
    this.toast.alertConfirmarAccion(`Borrar ${raza.nombre}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(raza.idRaza)
        }
      })
  }

  private borrar(idRaza: number) {
    this.service.eliminarRaza(idRaza)
      .subscribe(
        res => {
          this.toast.toastExitoso('Raza borrada')
          this.obtenerRazas()
        },
        error => {
          this.toast.toastError(error)
          console.log(error);
        }
      )
  }

  obtenerRazas() {
    this.service.obtenerRazas()
      .subscribe(res => {
        this.razas = res
      }, error => {
        this.toast.toastError('No se pudo recuperar razas')
        console.log(error)
      })
  }
}
