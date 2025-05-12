import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../../helpers/toast.service';
import { Rol } from '../../../models/Rol';
import { RolesService } from '../../../services/roles.service';

@Component({
  selector: 'app-editar-rol',
  templateUrl: './editar-rol.page.html',
  styleUrls: ['./editar-rol.page.scss'],
})
export class EditarRolPage implements OnInit {

  tituloToolbar: string = ''
  idRol: number = 0
  rolForm: FormGroup

  constructor(
    private rolesService: RolesService,
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private toast: ToastService
  ) { }

  ngOnInit() {
    this.rolForm = this.fb.group({
      idRol: [0, [Validators.required]],
      codigo: [0, [Validators.required]],
      descripcion: ['', Validators.required],
      activo: [true, Validators.required],
    })
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idRol')
    this.idRol = +param

    this.initFormVacio();

    if (this.idRol > 0) {
      this.rolesService.obtenerPorId(this.idRol)
        .subscribe(res => {
          this.tituloToolbar = `Rol ${res.descripcion}`
          this.rolForm.patchValue({
            idRol: res.idRol,
            codigo: res.codigo,
            descripcion: res.descripcion,
            activo: res.activo
          })
        })
      return
    }
    this.tituloToolbar = 'Nuevo Rol'
  }

  editarGuardar() {
    if (this.rolForm.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    let model: Rol = this.rolForm.value

    if (model.codigo <= 0) {
      this.toast.toastError('Codigo debe ser mayor a 0!')
      return
    }

    if (model.idRol > 0) {
      this.rolesService.editarRol(model)
        .subscribe(
          res => {
            this.toast.toastExitoso('Rol modificado exitosamente!')
            this.cancelar()
          },
          err => this.toast.toastError(err))
      return
    }

    this.rolesService.agregarRol(model)
      .subscribe(
        res => {
          this.toast.toastExitoso('Rol creado exitosamente!')
          this.cancelar()
        },
        err => this.toast.toastError(err))
  }

  cancelar() {
    this.rolForm.reset()
    this.router.navigate(['../../'], { relativeTo: this.route })
  }

  private initFormVacio() {
    this.rolForm.patchValue({
      idRol: 0,
      codigo: 0,
      descripcion: '',
      activo: true
    })
  }
}
