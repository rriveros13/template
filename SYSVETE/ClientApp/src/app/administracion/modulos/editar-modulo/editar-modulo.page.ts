import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../../helpers/toast.service';
import { Modulo } from '../../../models/Modulo';
import { ModuloService } from '../../../services/modulo.service';

@Component({
  selector: 'app-editar-modulo',
  templateUrl: './editar-modulo.page.html',
  styleUrls: ['./editar-modulo.page.scss'],
})
export class EditarModuloPage implements OnInit {

  tituloToolbar: string = 'Modulo'
  idModulo: number = 0
  moduloForm: FormGroup

  constructor(
    private service: ModuloService,
    private fb: FormBuilder,
    private toast: ToastService,
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit() {
    this.moduloForm = this.fb.group({
      idModulo: [0, [Validators.required]],
      nombre: ['', Validators.required],
    })
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idModulo')
    this.idModulo = +param

    this.initFormVacio();
    if (this.idModulo > 0) {
      this.service.obtenerModuloPorID(this.idModulo)
        .subscribe(res => {
          this.tituloToolbar = `Modulo ${res.nombre}`
          this.moduloForm.patchValue({
            idModulo: res.idModulo,
            nombre: res.nombre,
          })
        }, error => {
          this.toast.toastError('Error al recuperar modulo')
          console.log(error);
        })
    }
  }

  private initFormVacio() {
    this.moduloForm.patchValue({
      idModulo: 0,
      nombre: '',
    })
  }

  editarGuardar() {
    if (this.moduloForm.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    let model: Modulo = this.moduloForm.value

    if (model.idModulo > 0) {
      this.service.editarModulo(model)
        .subscribe(
          res => this.toast.toastExitoso('Modulo modificado exitosamente!'),
          err => this.toast.toastError(err))
      return
    }
    this.service.agregarModulo(model)
      .subscribe(
        res => this.toast.toastExitoso('Modulo creado exitosamente!'),
        err => this.toast.toastError(err))
  }

  cancelar() {
    this.moduloForm.reset()
    this.router.navigate(['../../'], { relativeTo: this.route })
  }
}
