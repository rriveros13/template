import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../../helpers/toast.service';
import { Impuesto } from '../../../models/Impuesto';
import { ImpuestoService } from '../../../services/impuesto.service';

@Component({
  selector: 'app-editar-impuesto',
  templateUrl: './editar-impuesto.page.html',
  styleUrls: ['./editar-impuesto.page.scss'],
})
export class EditarImpuestoPage implements OnInit {

  tituloToolbar: string = 'Impuesto'
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: ImpuestoService,
    private toast: ToastService,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idImpuesto: [0, Validators.required],
      descripcion: ['', Validators.required],
      valor: [0, Validators.required],
    })
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idImpuesto')
    var idImpuesto = +param;

    this.initFormVacio();

    if (idImpuesto > 0) {
      this.service.obtenerImpuestoPorId(idImpuesto)
        .subscribe(res => {
          this.tituloToolbar = `Impuesto ${res.descripcion}`
          this.form.patchValue({
            idImpuesto: res.idImpuesto,
            descripcion: res.descripcion,
            valor: res.valor
          })
        }, error => {
          this.toast.toastError('Error al recuperar impuesto.')
          console.log(error)
        })
    }

  }

  editarGuardar() {
    if (this.form.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    let model: Impuesto = this.form.value

    if (model.idImpuesto > 0) {
      this.service.editarImpuesto(model)
        .subscribe(
          res => {
            this.toast.toastExitoso('Impuesto modificado exitosamente!')
            this.irAtras()
          },
          err => this.toast.toastError(err))
      return
    }

    this.service.agregarImpuesto(model)
      .subscribe(
        res => {
          this.toast.toastExitoso('Impuesto creado exitosamente!')
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
      idImpuesto: 0,
      descripcion: '',
      valor: 0
    })
  }

}
