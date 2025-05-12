import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../../helpers/toast.service';
import { Tratamiento } from '../../../models/Tratamiento';
import { TratamientosService } from '../../../services/tratamientos.service';

@Component({
  selector: 'app-editar-tratamiento',
  templateUrl: './editar-tratamiento.page.html',
  styleUrls: ['./editar-tratamiento.page.scss'],
})
export class EditarTratamientoPage implements OnInit {

  tituloToolbar: string = 'Tratamiento'
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: TratamientosService,
    private toast: ToastService,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idTratamiento: [0, Validators.required],
      nombre: ['', Validators.required],
      costo: [0, Validators.required],
    })
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idTratamiento')
    var id = +param;

    this.initFormVacio()

    if (id > 0) {
      this.service.obtenerTratamientoPorID(id)
        .subscribe(res => {
          this.tituloToolbar = `Tratamiento ${res.nombre}`
          this.form.patchValue({
            idTratamiento: res.idTratamiento,
            nombre: res.nombre,
            costo: res.costo,
          })
        }, error => {
          this.toast.toastError('Error al recuperar tratamiento.')
          console.log(error)
        })
    }
  }

  private initFormVacio() {
    this.form.patchValue({
      idTratamiento: 0,
      nombre: '',
      costo: 0,
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

    let model: Tratamiento = this.form.value

    if (model.idTratamiento > 0) {
      this.service.editarTratamiento(model)
        .subscribe(
          res => {
            this.toast.toastExitoso('Tratamiento modificado exitosamente!')
            this.irAtras()
          },
          err => this.toast.toastError(err))
      return
    }

    this.service.agregarTratamiento(model)
      .subscribe(
        res => {
          this.toast.toastExitoso('Tratamiento creado exitosamente!')
          this.irAtras()
        },
        err => this.toast.toastError(err))
  }

}
