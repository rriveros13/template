import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../../helpers/toast.service';
import { Vacuna } from '../../../models/Vacuna';
import { VacunasService } from '../../../services/vacunas.service';

@Component({
  selector: 'app-editar-vacuna',
  templateUrl: './editar-vacuna.page.html',
  styleUrls: ['./editar-vacuna.page.scss'],
})
export class EditarVacunaPage implements OnInit {

  tituloToolbar: string = 'Vacuna'
  form: FormGroup;
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: VacunasService,
    private toast: ToastService,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idVacuna: [0, Validators.required],
      nombre: ['', Validators.required],
      costo: [0, Validators.required],
    })
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idVacuna')
    var id = +param;

    this.initFormVacio()

    if (id > 0) {
      this.service.obtenerVacunaPorID(id)
        .subscribe(res => {
          this.tituloToolbar = `Vacuna ${res.nombre}`
          this.form.patchValue({
            idVacuna: res.idVacuna,
            nombre: res.nombre,
            costo: res.costo
          })
        }, error => {
          this.toast.toastError('Error al recuperar vacuna.')
          console.log(error)
        })
    }
  }

  private initFormVacio() {
    this.form.patchValue({
      idVacuna: 0,
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

    let model: Vacuna = this.form.value

    if (model.idVacuna > 0) {
      this.service.editarVacuna(model)
        .subscribe(
          res => {
            this.toast.toastExitoso('Vacuna modificada exitosamente!')
            this.irAtras()
          },
          err => this.toast.toastError(err))
      return
    }

    this.service.agregarVacuna(model)
      .subscribe(
        res => {
          this.toast.toastExitoso('Vacuna creado exitosamente!')
          this.irAtras()
        },
        err => this.toast.toastError(err))
  }
}
