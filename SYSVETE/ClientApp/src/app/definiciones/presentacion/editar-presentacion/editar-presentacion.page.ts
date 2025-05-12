import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../../helpers/toast.service';
import { Presentacion } from '../../../models/Presentacion';
import { UnidadMedida } from '../../../models/UnidadMedida';
import { PresentacionService } from '../../../services/presentacion.service';
import { UnidadMedidaService } from '../../../services/unidad-medida.service';

@Component({
  selector: 'app-editar-presentacion',
  templateUrl: './editar-presentacion.page.html',
  styleUrls: ['./editar-presentacion.page.scss'],
})
export class EditarPresentacionPage implements OnInit {

  tituloToolbar: string = 'Presentacion'
  form: FormGroup;

  unidades: UnidadMedida[] = [];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: PresentacionService,
    private toast: ToastService,
    private unidadService: UnidadMedidaService,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idPresentacion: [0, Validators.required],
      idUnidad: [0, Validators.required],
      cantidadPresentacion: [0, Validators.required],
      descripcion: ['', Validators.required],
      activo: [true, Validators.required],
    })
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idPresentacion')
    var idPresentacion = +param;

    this.initFormVacio();

    this.unidadService.obtenerUnidadesMedida()
      .subscribe(res => {
        this.unidades = res
      }
        , error => {
          this.toast.toastError('Error al recuperar unidades de medida.')
          console.log(error)
        })

    if (idPresentacion > 0) {
      this.service.obtenerPresentacionPorId(idPresentacion)
        .subscribe(res => {
          this.tituloToolbar = `Presentacion ${res.descripcion}`
          this.form.patchValue({
            idPresentacion: res.idPresentacion,
            idUnidad: res.idUnidad,
            cantidadPresentacion: res.cantidadPresentacion,
            descripcion: res.descripcion,
            activo: res.activo,
          })
        }, error => {
          this.toast.toastError('Error al recuperar presentacion.')
          console.log(error)
        })
    }
  }

  irAtras() {
    this.form.reset()
    this.router.navigate(['../../'], { relativeTo: this.route })
  }

  editarGuardar() {
    if (this.form.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    let model: Presentacion = this.form.value

    if (model.idPresentacion > 0) {
      this.service.editarPresentacion(model)
        .subscribe(
          res => {
            this.toast.toastExitoso('Presentacion modificada exitosamente!')
            this.irAtras()
          },
          err => this.toast.toastError(err))
      return
    }

    this.service.agregarPresentacion(model)
      .subscribe(
        res => {
          this.toast.toastExitoso('Presentacion creado exitosamente!')
          this.irAtras()
        },
        err => this.toast.toastError(err))
  }

  private initFormVacio() {
    this.form.patchValue({
      idPresentacion: 0,
      idUnidad: 0,
      cantidadPresentacion: 0,
      descripcion: '',
      activo: true
    })
  }
}
