import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../../helpers/toast.service';
import { Impuesto } from '../../../models/Impuesto';
import { Insumo } from '../../../models/Insumo';
import { Presentacion } from '../../../models/Presentacion';
import { TipoInsumo } from '../../../models/TipoInsumo';
import { ImpuestoService } from '../../../services/impuesto.service';
import { InsumoService } from '../../../services/insumo.service';
import { PresentacionService } from '../../../services/presentacion.service';
import { TipoInsumoService } from '../../../services/tipo-insumo.service';

@Component({
  selector: 'app-editar-insumo',
  templateUrl: './editar-insumo.page.html',
  styleUrls: ['./editar-insumo.page.scss'],
})
export class EditarInsumoPage implements OnInit {

  tituloToolbar: string = 'Insumo'

  tipoInsumo: TipoInsumo[] = []
  presentaciones: Presentacion[] = []
  impuestos: Impuesto[] = []

  form: FormGroup

  constructor(
    private fb: FormBuilder,
    private toast: ToastService,
    private router: Router,
    private route: ActivatedRoute,
    private tipoInService: TipoInsumoService,
    private presService: PresentacionService,
    private impService: ImpuestoService,
    private service: InsumoService,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idInsumo: [0, Validators.required],
      idTipoInsumo: [0, Validators.required],
      idPresentacion: [0, Validators.required],
      idImpuesto: [0, Validators.required],
      codigo: ['', Validators.required],
      descripcion: ['', Validators.required],
      activo: [false, Validators.required],
    })
  }

  ionViewWillEnter() {

    const param = this.route.snapshot.paramMap.get('idInsumo')
    var id = +param;

    this.initFormVacio()

    this.tipoInService.obtenerTipoInsumo()
      .subscribe(res => this.tipoInsumo = res, error => {
        this.toast.toastError(`${error}`)
        console.log(`${error}`)
      })

    this.presService.obtenerPresentaciones()
      .subscribe(res => this.presentaciones = res, error => {
        this.toast.toastError(`${error}`)
        console.log(`${error}`)
      })

    this.impService.obtenerImpuestos()
      .subscribe(res => this.impuestos = res, error => {
        this.toast.toastError(`${error}`)
        console.log(`${error}`)
      })

    if (id > 0) {
      this.service.obtenerInsumosPorId(id)
        .subscribe(res => {
          this.form.patchValue({
            idInsumo: res.idInsumo,
            idTipoInsumo: res.idTipoInsumo,
            idPresentacion: res.idPresentacion,
            idImpuesto: res.idImpuesto,
            codigo: res.codigo,
            descripcion: res.descripcion,
            activo: res.activo,
          })
        }, error => {
          this.toast.toastError('Error al recuperar insumo.')
          console.log(error)
        })
    }
  }

  editarGuardar() {
    if (this.form.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    let model: Insumo = this.form.value

    if (model.idInsumo > 0) {
      this.service.editarInsumos(model)
        .subscribe(
          res => {
            this.toast.toastExitoso('Insumo modificado exitosamente!')
            this.cancelar();
          },
          err => this.toast.toastError(err))
      return
    }

    this.service.agregarInsumos(model)
      .subscribe(
        res => {
          this.toast.toastExitoso('Insumo creado exitosamente!')
          this.cancelar()
        },
        err => this.toast.toastError(err))
  }

  cancelar() {
    this.form.reset()
    this.router.navigate(['../../'], { relativeTo: this.route })
  }
  private initFormVacio() {
    this.form.patchValue({
      idInsumo: 0,
      idTipoInsumo: 0,
      idPresentacion: 0,
      idImpuesto: 0,
      codigo: '',
      descripcion: '',
      activo: true,
    })
  }
}
