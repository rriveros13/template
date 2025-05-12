import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../helpers/toast.service';
import { Rol } from '../../models/Rol';
import { RolesService } from '../../services/roles.service';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.page.html',
  styleUrls: ['./roles.page.scss'],
})
export class RolesPage implements OnInit {

  tituloToolbar: string = 'Roles del sistema'

  rolesSistema: Rol[] = []

  constructor(
    private rolesService: RolesService,
    private toast: ToastService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerRoles();
  }

  confirmarBorrar(rol: Rol) {
    this.toast.alertConfirmarAccion(`Borrar ${rol.descripcion}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(rol.idRol)
        }
      })
  }

  borrar(idRol: number) {
    this.rolesService.borrarRol(idRol)
      .subscribe(
        res => {
          this.toast.toastExitoso('Rol Borrado')
          this.obtenerRoles();
        },
        error => { this.toast.toastError(error) }
      )
  }

  private obtenerRoles() {
    this.rolesService.obtenerRoles()
      .subscribe(res => this.rolesSistema = res,
        error => {
          this.toast.toastError(`${error}`)
        })
  }
}
