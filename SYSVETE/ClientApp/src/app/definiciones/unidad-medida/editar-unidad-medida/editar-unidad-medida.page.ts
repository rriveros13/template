import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../../helpers/toast.service';
import { UnidadMedida } from '../../../models/UnidadMedida';
import { UnidadMedidaService } from '../../../services/unidad-medida.service';

@Component({
  selector: 'app-editar-unidad-medida',
  templateUrl: './editar-unidad-medida.page.html',
  styleUrls: ['./editar-unidad-medida.page.scss'],
})
export class EditarUnidadMedidaPage implements OnInit {

  tituloToolbar: string = 'Unidad de Medida'
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: UnidadMedidaService,
    private toast: ToastService,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idUnidad: [0, Validators.required],
      descripcion: ['', Validators.required],
      abreviatura: ['', Validators.required],
    })
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idUnidad')
    var idUnidad = +param;

    this.initFormVacio();

    if (idUnidad > 0) {
      this.service.obtenerUnidadMedidaPorId(idUnidad)
        .subscribe(res => {
          this.tituloToolbar = `Impuesto ${res.descripcion}`
          this.form.patchValue({
            idUnidad: res.idUnidad,
            descripcion: res.descripcion,
            abreviatura: res.abreviatura,
          })
        }, error => {
          this.toast.toastError('Error al recuperar unidad de medida.')
          console.log(error)
        })
    }
  }

  editarGuardar() {
    if (this.form.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    let model: UnidadMedida = this.form.value

    if (model.idUnidad > 0) {
      this.service.editarUnidadMedida(model)
        .subscribe(
          res => {
            this.toast.toastExitoso('Unidad de Medida modificada exitosamente!')
            this.irAtras()
          },
          err => this.toast.toastError(err))
      return
    }

    this.service.agregarUnidadMedida(model)
      .subscribe(
        res => {
          this.toast.toastExitoso('Unidad de Medida creado exitosamente!')
          this.irAtras()
        },
        err => this.toast.toastError(err))
  }

  irAtras() {
    this.form.reset()
    this.router.navigate(['../../'], { relativeTo: this.route })
  }

  private initFormVacio() {
    this.form.patchValue({
      idUnidad: 0,
      descripcion: '',
      abreviatura: ''
    })
  }
}
