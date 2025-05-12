import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../helpers/toast.service';
import { Especie } from '../../models/Especie';
import { EspeciesService } from '../../services/especies.service';

@Component({
  selector: 'app-especies',
  templateUrl: './especies.page.html',
  styleUrls: ['./especies.page.scss'],
})
export class EspeciesPage implements OnInit {

  especies: Especie[] = []

  tituloToolbar: string = 'Especies';

  constructor(
    private service: EspeciesService,
    private toast: ToastService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerEspecies();
  }

  confirmarEliminar(especie: Especie) {
    this.toast.alertConfirmarAccion(`Borrar ${especie.nombre}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(especie.idEspecie)
        }
      })
  }

  private borrar(idEspecie: number) {
    this.service.eliminarEspecie(idEspecie)
      .subscribe(
        res => {
          this.toast.toastExitoso('Especie borrada!')
          this.obtenerEspecies()
        },
        error => { this.toast.toastError(error) }
      )
  }

  obtenerEspecies() {
    this.service.obtenerEspecies()
      .subscribe(res => {
        this.especies = res
      }, error => {
        this.toast.toastError('No se pudo obtener las especies');
        console.log(error)
      })
  }
}
