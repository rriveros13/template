import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../helpers/toast.service';
import { Rol } from '../../models/Rol';
import { Usuario } from '../../models/Usuario';
import { RolesService } from '../../services/roles.service';
import { UsuariosService } from '../../services/usuarios.service';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.page.html',
  styleUrls: ['./perfil.page.scss'],
})
export class PerfilPage implements OnInit {

  public tituloToolbar: string = 'Perfil de usuario'

  public esNuevo: boolean = true

  public usuarioForm: FormGroup

  public roles: Rol[] = []

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router,
    private usuarioServ: UsuariosService,
    private rolesServ: RolesService,
    private toast: ToastService,
  ) {
  }

  ngOnInit() {
    this.usuarioForm = this.fb.group({
      idUsuario: [0, Validators.required],
      alias: ['', [Validators.required]],
      nombre: ['', [Validators.required]],
      contrasena: ['', [Validators.required]],
      activo: [true, Validators.required],
      idRol: [undefined, Validators.required]
    })
  }

  ionViewWillEnter() {

    this.usuarioForm.patchValue({
      idUsuario: 0,
      alias: '',
      nombre: '',
      activo: true,
      idRol: 0
    })

    this.obtenerRoles()
    this.esNuevo = true
    const idUsuario = parseInt(this.route.snapshot.paramMap.get('idUsuario'))
    if (idUsuario > 0) {
      this.esNuevo = false
      this.usuarioServ.obtenerUsuariosPorId(idUsuario)
        .subscribe(res => {
          this.usuarioForm.patchValue({
            idUsuario: res.idUsuario,
            alias: res.alias,
            nombre: res.nombre,
            activo: res.activo,
            idRol: res.idRol
          })
        }, error => {
          this.toast.toastError(`${error}`)
        })

    }
  }

  editarGuardar() {
    console.log(this.usuarioForm.value)

    let dto: Usuario = this.usuarioForm.value;

    if (dto.idUsuario > 0) {
      return
    }

    this.usuarioServ.insertarUsuario(dto)
      .subscribe(
        res => {
          this.toast.toastExitoso('Usuario Creado')
          this.router.navigateByUrl('/usuarios')
          console.log(res)
        },
        err => {
          this.toast.toastError('Error al crear usuario!')
          console.log(err)
        }
      )
  }

  cancelar() {
    this.usuarioForm.reset()
    this.router.navigateByUrl('/usuarios')
  }

  obtenerRoles() {
    this.rolesServ.obtenerRoles()
      .subscribe(res => this.roles = res,
        error => {
          this.toast.toastError(`${error}`)
        })
  }
}
