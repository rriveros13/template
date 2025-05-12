import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../helpers/toast.service';
import { UnidadMedida } from '../../models/UnidadMedida';
import { UnidadMedidaService } from '../../services/unidad-medida.service';

@Component({
  selector: 'app-unidad-medida',
  templateUrl: './unidad-medida.page.html',
  styleUrls: ['./unidad-medida.page.scss'],
})
export class UnidadMedidaPage implements OnInit {

  tituloToolbar: string = 'Unidades de Medida'
  unidades: UnidadMedida[] = [];

  constructor(
    private service: UnidadMedidaService,
    private toast: ToastService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerUnidades()
  }

  private obtenerUnidades() {
    this.service.obtenerUnidadesMedida()
      .subscribe(res => {
        this.unidades = res
      }, error => {
        this.toast.toastError('No se pudo obtener unidades de medida');
        console.log(error)
      })
  }

  confirmarEliminar(unidad: UnidadMedida) {
    this.toast.alertConfirmarAccion(`Borrar ${unidad.descripcion}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(unidad.idUnidad)
        }
      })
  }

  private borrar(idUnidad: number) {
    this.service.eliminarUnidadMedida(idUnidad)
      .subscribe(
        res => {
          this.toast.toastExitoso('Unidad de medida borrada!')
          this.obtenerUnidades()
        },
        error => { this.toast.toastError(error) }
      )
  }
}
