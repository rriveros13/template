import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastService } from '../helpers/toast.service';
import { Usuario } from '../models/Usuario';
import { UsuariosService } from '../services/usuarios.service';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.page.html',
  styleUrls: ['./usuarios.page.scss'],
})
export class UsuariosPage implements OnInit {

  public tituloToolbar: string = 'Usuarios del Sistema'
  public usuariosSistema: Usuario[] = []

  constructor(
    private usuarioServ: UsuariosService,
    private router: Router,
    private toast: ToastService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerUsuarios()
  }

  irAPerfil(usuario) {
    this.router.navigateByUrl(`./perfil/${usuario.idUsuario}`)
  }

  confirmarEliminar(usuario) {
    this.toast.alertConfirmarAccion(`Eliminar Usuario: ${usuario.usuario}`).
      then(res => {
        if (res.role == 'done') {
          this.eliminarUsuario(usuario)
        }
      })
  }

  eliminarUsuario(usuario: Usuario) {
    this.usuarioServ.eliminarUsuario(usuario.idUsuario)
      .subscribe(res => {
        this.toast.toastExitoso('Usuario borrado')
        this.obtenerUsuarios();
      },
        error => {
          this.toast.toastError('Error al eliminar usuario!')
          console.log(error);
        })

  }

  obtenerUsuarios() {
    this.usuarioServ.obtenerUsuarios()
      .subscribe(res => this.usuariosSistema = res,
        error => {
          this.toast.toastError(`${error}`)
        })
  }

  nuevoUsuario() {
    this.router.navigateByUrl(`./perfil/0`)
  }
}
