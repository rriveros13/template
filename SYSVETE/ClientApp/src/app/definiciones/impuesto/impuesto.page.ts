import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../helpers/toast.service';
import { Impuesto } from '../../models/Impuesto';
import { ImpuestoService } from '../../services/impuesto.service';

@Component({
  selector: 'app-impuesto',
  templateUrl: './impuesto.page.html',
  styleUrls: ['./impuesto.page.scss'],
})
export class ImpuestoPage implements OnInit {

  tituloToolbar: string = 'Impuestos';
  impuestos: Impuesto[] = [];

  constructor(
    private service: ImpuestoService,
    private toast: ToastService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerImpuestos()
  }

  private obtenerImpuestos() {
    this.service.obtenerImpuestos()
      .subscribe(res => {
        this.impuestos = res
      }, error => {
        this.toast.toastError('No se pudo obtener impuestos');
        console.log(error)
      })
  }

  confirmarEliminar(impuesto: Impuesto) {
    this.toast.alertConfirmarAccion(`Borrar ${impuesto.descripcion}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(impuesto.idImpuesto)
        }
      })
  }

  private borrar(idImpuesto: number) {
    this.service.eliminarImpuesto(idImpuesto)
      .subscribe(
        res => {
          this.toast.toastExitoso('Impuesto borrado!')
          this.obtenerImpuestos()
        },
        error => { this.toast.toastError(error) }
      )
  }
}
