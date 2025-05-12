import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../../helpers/toast.service';
import { Patologia } from '../../../models/Patologia';
import { PatologiasService } from '../../../services/patologias.service';

@Component({
  selector: 'app-editar-patologia',
  templateUrl: './editar-patologia.page.html',
  styleUrls: ['./editar-patologia.page.scss'],
})
export class EditarPatologiaPage implements OnInit {

  tituloToolbar: string = 'Patologia'
  form: FormGroup;
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: PatologiasService,
    private toast: ToastService,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idPatologia: [0, Validators.required],
      nombre: ['', Validators.required],
    })
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idPatologia')
    var idPatologia = +param;

    this.initFormVacio()

    if (idPatologia > 0) {
      this.service.obtenerPatologiaPorID(idPatologia)
        .subscribe(res => {
          this.tituloToolbar = `Patologia ${res.nombre}`
          this.form.patchValue({
            idPatologia: res.idPatologia,
            nombre: res.nombre,
          })
        }, error => {
          this.toast.toastError('Error al recuperar tratamiento.')
          console.log(error)
        })
    }
  }

  private initFormVacio() {
    this.form.patchValue({
      idPatologia: 0,
      nombre: '',
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

    let model: Patologia = this.form.value

    if (model.idPatologia > 0) {
      this.service.editarPatologia(model)
        .subscribe(
          res => {
            this.toast.toastExitoso('Patologia modificada exitosamente!')
            this.irAtras()
          },
          err => this.toast.toastError(err))
      return
    }

    this.service.agregarPatologia(model)
      .subscribe(
        res => {
          this.toast.toastExitoso('Patologia creado exitosamente!')
          this.irAtras()
        },
        err => this.toast.toastError(err))
  }
}
