import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../helpers/toast.service';
import { Vacuna } from '../../models/Vacuna';
import { VacunasService } from '../../services/vacunas.service';

@Component({
  selector: 'app-vacunas',
  templateUrl: './vacunas.page.html',
  styleUrls: ['./vacunas.page.scss'],
})
export class VacunasPage implements OnInit {

  tituloToolbar: string = 'Vacunas'
  vacunas: Vacuna[] = [];

  termino: string = '';
  filtroProp: string[] = ['nombre'];
  itemsFiltro = []

  constructor(
    private toast: ToastService,
    private service: VacunasService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerDatos()
  }

  confirmarEliminar(vacuna: Vacuna) {
    this.toast.alertConfirmarAccion(`Borrar ${vacuna.nombre}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(vacuna.idVacuna)
        }
      })
  }

  private borrar(id: number) {
    this.service.eliminarVacuna(id)
      .subscribe(
        res => {
          this.toast.toastExitoso('Vacuna borrada!')
          this.obtenerDatos()
        },
        error => { this.toast.toastError(error) }
      )
  }

  private obtenerDatos() {
    this.service.obtenerVacunas()
      .subscribe(res => {
        this.vacunas = res
        this.itemsFiltro = res
      }, error => {
        this.toast.toastError('No se pudo obtener Vacunas');
        console.log(error)
      })
  }

}
