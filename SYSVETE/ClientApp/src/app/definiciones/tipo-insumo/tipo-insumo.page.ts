import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../helpers/toast.service';
import { TipoInsumo } from '../../models/TipoInsumo';
import { TipoInsumoService } from '../../services/tipo-insumo.service';

@Component({
  selector: 'app-tipo-insumo',
  templateUrl: './tipo-insumo.page.html',
  styleUrls: ['./tipo-insumo.page.scss'],
})
export class TipoInsumoPage implements OnInit {

  tituloToolbar: string = 'Tipo de insumos';

  tipoInsumos: TipoInsumo[] = [];

  constructor(
    private service: TipoInsumoService,
    private toast: ToastService,
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerTipoInsumo();
  }

  private obtenerTipoInsumo() {
    this.service.obtenerTipoInsumo()
      .subscribe(res => {
        this.tipoInsumos = res
      }, error => {
        this.toast.toastError('No se pudo obtener los tipos de insumo');
        console.log(error)
      })
  }

  confirmarEliminar(tipo: TipoInsumo) {
    this.toast.alertConfirmarAccion(`Borrar ${tipo.nombre}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(tipo.idTipoInsumo)
        }
      })
  }

  private borrar(idTipo: number) {
    this.service.eliminarTipoInsumo(idTipo)
      .subscribe(
        res => {
          this.toast.toastExitoso('Tipo de insumo borrado!')
        },
        error => { this.toast.toastError(error) }
      )

  }
}
