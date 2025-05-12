import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../../helpers/toast.service';
import { Modulo } from '../../../models/Modulo';
import { Permiso } from '../../../models/Permiso';
import { Rol } from '../../../models/Rol';
import { ModuloService } from '../../../services/modulo.service';
import { PermisosService } from '../../../services/permisos.service';
import { RolesService } from '../../../services/roles.service';

@Component({
  selector: 'app-editar-permiso',
  templateUrl: './editar-permiso.page.html',
  styleUrls: ['./editar-permiso.page.scss'],
})
export class EditarPermisoPage implements OnInit {

  tituloToolbar: string = 'Permiso'

  roles: Rol[] = [];
  modulos: Modulo[] = [];

  form: FormGroup

  edicion: boolean = false;

  constructor(
    private rolService: RolesService,
    private permisoService: PermisosService,
    private fb: FormBuilder,
    private toast: ToastService,
    private router: Router,
    private route: ActivatedRoute,
    private moduloService: ModuloService,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idPermiso: [0, Validators.required],
      idRol: [0, Validators.required],
      idModulo: ['', Validators.required],
      consultar: [false, Validators.required],
      agregar: [false, Validators.required],
      editar: [false, Validators.required],
      borrar: [false, Validators.required],
    })
  }

  ionViewWillEnter() {
    this.edicion = false;

    const param = this.route.snapshot.paramMap.get('idPermiso')
    var idPermiso = +param;

    this.initFormVacio();

    this.obtenerRoles();
    this.obtenerModulos();

    if (idPermiso > 0) {
      this.edicion = true;
      this.permisoService.obtenerPermisoPorId(idPermiso)
        .subscribe(permiso => {
          this.form.patchValue({
            idPermiso: permiso.idPermiso,
            idRol: permiso.idRol,
            idModulo: permiso.idModulo,
            consultar: permiso.consultar,
            agregar: permiso.agregar,
            editar: permiso.editar,
            borrar: permiso.borrar,
          })
        }, error => {
          this.toast.toastError('Error al recuperar permiso.')
          console.log(error)
        })
    }
  }

  obtenerRoles() {
    this.rolService.obtenerRoles()
      .subscribe(res => this.roles = res,
        error => {
          this.toast.toastError(`${error}`)
          console.log(`${error}`)
        })
  }

  obtenerModulos() {
    this.moduloService.obtenerModulos()
      .subscribe(res => this.modulos = res,
        error => {
          this.toast.toastError(`${error}`)
          console.log(`${error}`)
        })
  }

  editarGuardar() {

    if (this.form.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    var dto: Permiso = this.form.value
    if (dto.idPermiso > 0) {
      this.permisoService.editarPermiso(dto)
        .subscribe(
          res => {
            this.toast.toastExitoso(`Permiso modificado!`)
            this.cancelar()
          },
          error => {
            this.toast.toastError(`${error}`)
          },
        )
      return
    }

    this.permisoService.nuevoPermiso(dto)
      .subscribe(
        res => {
          this.toast.toastExitoso(`Permiso creado!`)
          this.cancelar()
        },
        error => {
          this.toast.toastError(`${error}`)
        },
      )
  }

  cancelar() {
    this.form.reset()
    this.router.navigate(['../../'], { relativeTo: this.route })
  }

  private initFormVacio() {
    this.form.patchValue({
      idPermiso: 0,
      idRol: 0,
      idModulo: 0,
      consultar: false,
      agregar: false,
      editar: false,
      borrar: false,
    })
  }
}
