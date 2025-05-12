import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../helpers/toast.service';
import { Tratamiento } from '../../models/Tratamiento';
import { TratamientosService } from '../../services/tratamientos.service';

@Component({
  selector: 'app-tratamientos',
  templateUrl: './tratamientos.page.html',
  styleUrls: ['./tratamientos.page.scss'],
})
export class TratamientosPage implements OnInit {

  tituloToolbar: string = 'Tratamientos'
  patologias: Tratamiento[] = [];

  termino: string = '';
  filtroProp: string[] = ['nombre'];
  itemsFiltro = []

  constructor(
    private toast: ToastService,
    private service: TratamientosService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerDatos()
  }

  confirmarEliminar(tratamiento: Tratamiento) {
    this.toast.alertConfirmarAccion(`Borrar ${tratamiento.nombre}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(tratamiento.idTratamiento)
        }
      })
  }

  private borrar(idTratamiento: number) {
    this.service.eliminarTratamiento(idTratamiento)
      .subscribe(
        res => {
          this.toast.toastExitoso('Tratamiento borrado!')
          this.obtenerDatos()
        },
        error => { this.toast.toastError(error) }
      )
  }

  private obtenerDatos() {
    this.service.obtenerTratamientos()
      .subscribe(res => {
        this.patologias = res
        this.itemsFiltro = res
      }, error => {
        this.toast.toastError('No se pudo obtener tratamientos');
        console.log(error)
      })
  }

}
