import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../helpers/toast.service';
import { Patologia } from '../../models/Patologia';
import { PatologiasService } from '../../services/patologias.service';

@Component({
  selector: 'app-patologias',
  templateUrl: './patologias.page.html',
  styleUrls: ['./patologias.page.scss'],
})
export class PatologiasPage implements OnInit {

  tituloToolbar: string = 'Patologias'
  patologias: Patologia[] = [];

  termino: string = '';
  filtroProp: string[] = ['nombre'];
  itemsFiltro = []

  constructor(
    private toast: ToastService,
    private service: PatologiasService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerPatologias()
  }

  confirmarEliminar(patologia: Patologia) {
    this.toast.alertConfirmarAccion(`Borrar ${patologia.nombre}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(patologia.idPatologia)
        }
      })
  }

  private borrar(idPatologia: number) {
    this.service.eliminarPatologia(idPatologia)
      .subscribe(
        res => {
          this.toast.toastExitoso('Patologia borrada!')
          this.obtenerPatologias()
        },
        error => { this.toast.toastError(error) }
      )
  }

  private obtenerPatologias() {
    this.service.obtenerPatologias()
      .subscribe(res => {
        this.patologias = res
        this.itemsFiltro = res
      }, error => {
        this.toast.toastError('No se pudo obtener patologias');
        console.log(error)
      })
  }
}
