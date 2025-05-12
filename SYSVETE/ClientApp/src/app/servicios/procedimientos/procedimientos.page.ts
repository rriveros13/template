import { Component, OnInit } from '@angular/core';
import { Procedimiento } from '../../models/Procedimiento';
import { ProcedimietosService } from '../../services/procedimietos.service';
import { ToastService } from '../../helpers/toast.service';

@Component({
  selector: 'app-procedimientos',
  templateUrl: './procedimientos.page.html',
  styleUrls: ['./procedimientos.page.scss'],
})
export class ProcedimientosPage implements OnInit {

  tituloToolbar: string = 'Procedimientos'
  procedimientos: Procedimiento[] = [];

  termino: string = '';
  filtroProp: string[] = ['nombre'];
  itemsFiltro = []


  constructor(
    private toast: ToastService,
    private service: ProcedimietosService 
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerDatos()
  }

  private obtenerDatos() {
    this.service.obtenerProcedimientos()
      .subscribe(res => {
        this.procedimientos = res
        this.itemsFiltro = res
      }, error => {
        this.toast.toastError('No se pudo obtener procedimientos');
        console.log(error)
      })
  }

  confirmarEliminar(proc: Procedimiento) {
    this.toast.alertConfirmarAccion(`Borrar ${proc.descripcion}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(proc.idProcedimiento)
        }
      })
  }

  private borrar(id: number) {
    this.service.eliminarProcedimiento(id)
      .subscribe(
        res => {
          this.toast.toastExitoso('Procedimiento borrado!')
          this.obtenerDatos()
        },
        error => { this.toast.toastError(error) }
      )
  }
}
