import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProcedimietosService } from '../../../services/procedimietos.service';
import { ToastService } from '../../../helpers/toast.service';
import { Procedimiento } from '../../../models/Procedimiento';

@Component({
  selector: 'app-editar-procedimiento',
  templateUrl: './editar-procedimiento.page.html',
  styleUrls: ['./editar-procedimiento.page.scss'],
})
export class EditarProcedimientoPage implements OnInit {

  tituloToolbar: string = 'Procedimiento'
  form: FormGroup;

  id: number = 0

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: ProcedimietosService,
    private toast: ToastService,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idProcedimiento: [0, Validators.required],
      descripcion: ['', Validators.required],
      costo: [0, Validators.required],
    })
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idProcedimiento')
    this.id = +param;

    this.initFormVacio()

    if (this.id > 0) {
      this.service.obtenerProcedimientoPorId(this.id)
        .subscribe(res => {
          this.tituloToolbar = `Procedimiento ${res.descripcion}`
          this.form.patchValue({
            idProcedimiento: res.idProcedimiento,
            descripcion: res.descripcion,
            costo: res.costo,
          })
        }, error => {
          this.toast.toastError('Error al recuperar Procedimiento.')
          console.log(error)
        })
    }
  }

  private initFormVacio() {
    this.form.patchValue({
      idProcedimiento: 0,
      descripcion: '',
      costo: 0
    })
  }

  private irAtras() {
    this.form.reset()
    this.router.navigate(['../../'], { relativeTo: this.route })
  }

  editarGuardar() {
    if (this.form.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    let model: Procedimiento = this.form.value

    if (model.idProcedimiento > 0) {
      this.service.editarProcedimiento(model)
        .subscribe(
          res => {
            this.toast.toastExitoso('Procedimiento modificado exitosamente!')
            this.irAtras()
          },
          err => this.toast.toastError(err))
      return
    }

    this.service.agregarProcedimiento(model)
      .subscribe(
        res => {
          this.toast.toastExitoso('Procedimiento creado exitosamente!')
          this.irAtras()
        },
        err => this.toast.toastError(err))
  }
}
