import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../../helpers/toast.service';
import { Especie } from '../../../models/Especie';
import { Raza } from '../../../models/Raza';
import { EspeciesService } from '../../../services/especies.service';
import { RazaService } from '../../../services/raza.service';

@Component({
  selector: 'app-editar-raza',
  templateUrl: './editar-raza.page.html',
  styleUrls: ['./editar-raza.page.scss'],
})
export class EditarRazaPage implements OnInit {

  form: FormGroup;
  tituloToolbar: string = 'Raza'
  especies: Especie[] = []

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: RazaService,
    private especieService: EspeciesService,
    private toast: ToastService,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idRaza: [0, Validators.required],
      idEspecie: [0, Validators.required],
      nombre: ['', Validators.required],
      activo: [true, Validators.required]
    })
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idRaza')
    var idRaza = +param;

    this.especieService.obtenerEspecies()
      .subscribe(res => this.especies = res)

    this.initFormVacio();

    if (idRaza > 0) {
      this.service.obtenerRazaPorId(idRaza)
        .subscribe(res => {
          this.tituloToolbar = `Especie ${res.nombre}`
          this.form.patchValue({
            idRaza: res.idRaza,
            idEspecie: res.idEspecie,
            nombre: res.nombre,
            activo: res.activo
          })
        }, error => {
          this.toast.toastError('No se pudo obtener Raza')
          console.log(error)
        })
    }
  }

  editarGuardar() {
    if (this.form.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    let model: Raza = this.form.value

    if (model.idRaza > 0) {
      this.service.editarRaza(model)
        .subscribe(
          res => {
            this.toast.toastExitoso('Raza modificada exitosamente!')
            this.cancelar()
          },
          err => this.toast.toastError(err))
      return
    }

    this.service.agregarRaza(model)
      .subscribe(
        res => {
          this.toast.toastExitoso('Raza creada exitosamente!')
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
      idRaza: 0,
      idEspecie: 0,
      nombre: '',
      activo: true
    })
  }
}
