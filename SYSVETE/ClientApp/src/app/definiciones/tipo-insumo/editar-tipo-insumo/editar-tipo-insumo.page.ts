import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../../helpers/toast.service';
import { TipoInsumo } from '../../../models/TipoInsumo';
import { TipoInsumoService } from '../../../services/tipo-insumo.service';

@Component({
  selector: 'app-editar-tipo-insumo',
  templateUrl: './editar-tipo-insumo.page.html',
  styleUrls: ['./editar-tipo-insumo.page.scss'],
})
export class EditarTipoInsumoPage implements OnInit {

  tituloToolbar: string = 'Tipo insumo'
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: TipoInsumoService,
    private toast: ToastService,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idTipoInsumo: [0, Validators.required],
      descripcion: ['', Validators.required],
      nombre: ['', Validators.required],
      activo: [true, Validators.required]
    })
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idTipoInsumo')
    var idTipo = +param;

    this.initFormVacio();

    if (idTipo > 0) {
      this.service.obtenerTipoInsumoPorId(idTipo)
        .subscribe(res => {
          this.tituloToolbar = `Tipo inusmo ${res.nombre}`
          this.form.patchValue({
            idTipoInsumo: res.idTipoInsumo,
            nombre: res.nombre,
            descripcion: res.descripcion,
            activo: res.activo
          })
        }, error => {
          this.toast.toastError('Error al recuperar tipo de insumo.')
          console.log(error)
        })
    }
  }

  editarGuardar() {
    if (this.form.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    let model: TipoInsumo = this.form.value

    if (model.idTipoInsumo > 0) {
      this.service.editarTipoInsumo(model)
        .subscribe(
          res => {
            this.toast.toastExitoso('Tipo insumo modificado exitosamente!')
            this.cancelar()
          },
          err => this.toast.toastError(err))
      return
    }

    this.service.agregarTipoInsumo(model)
      .subscribe(
        res => {
          this.toast.toastExitoso('Tipo insumo creado exitosamente!')
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
      idTipoInsumo: 0,
      nombre: '',
      descripcion: '',
      activo: true
    })
  }

}
