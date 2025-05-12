import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../../helpers/toast.service';
import { Especie } from '../../../models/Especie';
import { EspeciesService } from '../../../services/especies.service';

@Component({
  selector: 'app-editar-especie',
  templateUrl: './editar-especie.page.html',
  styleUrls: ['./editar-especie.page.scss'],
})
export class EditarEspeciePage implements OnInit {

  form: FormGroup;
  tituloToolbar: string = 'Especie'

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: EspeciesService,
    private toast: ToastService,

  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idEspecie: [0, Validators.required],
      nombre: ['', Validators.required],
      activo: [true, Validators.required]
    })
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idEspecie')
    var idEspecie = +param;

    this.initFormVacio();

    if (idEspecie > 0) {
      this.service.obtenerEspeciesPorId(idEspecie)
        .subscribe(res => {
          this.tituloToolbar = `Especie ${res.nombre}`
          this.form.patchValue({
            idEspecie: res.idEspecie,
            nombre: res.nombre,
            activo: res.activo
          })
        })
    }
  }

  editarGuardar() {
    if (this.form.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    let model: Especie = this.form.value

    if (model.idEspecie > 0) {
      this.service.editarEspecie(model)
        .subscribe(
          res => {
            this.toast.toastExitoso('Especie modificada exitosamente!')
            this.cancelar();
          },
          err => this.toast.toastError(err))
      return
    }

    this.service.agregarEspecie(model)
      .subscribe(
        res => {
          this.toast.toastExitoso('Especie creada exitosamente!')
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
      idEspecie: 0,
      nombre: '',
      activo: true
    })
  }
}
