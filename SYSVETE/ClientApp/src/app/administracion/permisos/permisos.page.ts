import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../helpers/toast.service';
import { Permiso } from '../../models/Permiso';
import { PermisosService } from '../../services/permisos.service';

@Component({
  selector: 'app-permisos',
  templateUrl: './permisos.page.html',
  styleUrls: ['./permisos.page.scss'],
})
export class PermisosPage implements OnInit {

  tituloToolbar: string = 'Permisos'

  permisos: Permiso[] = []
  constructor(
    private toast: ToastService,
    private permisoService: PermisosService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerPermisos()
  }

  confirmarEliminar(permiso: Permiso) {
    this.toast.alertConfirmarAccion(`Eliminar Permiso`).
      then(res => {
        if (res.role == 'done') {
          this.eliminarPermiso(permiso)
        }
      })
  }

  eliminarPermiso(permiso: Permiso) {
    this.permisoService.eliminarPermiso(permiso.idPermiso)
      .subscribe(res => {
        this.toast.toastExitoso('Permiso Eliminado')
        this.obtenerPermisos()
      },
        error => {
          this.toast.toastError('Error al eliminar permisos')
          console.log(error);
        })
  }

  private obtenerPermisos() {
    this.permisoService.obtenerPermisos()
      .subscribe(res => this.permisos = res, error => {
        this.toast.toastError('No se pudo obtener permisos');
        console.log(error)
      })
  }
}
