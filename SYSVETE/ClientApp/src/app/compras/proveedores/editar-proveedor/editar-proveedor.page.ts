import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastService } from '../../../helpers/toast.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ProveedoresService } from '../../../services/proveedores.service';
import { Proveedor } from '../../../models/Proveedor';

@Component({
  selector: 'app-editar-proveedor',
  templateUrl: './editar-proveedor.page.html',
  styleUrls: ['./editar-proveedor.page.scss'],
})
export class EditarProveedorPage implements OnInit {

  form: FormGroup;
  tituloToolbar: string = 'Nuevo Proveedor'

  selectedPersona: number
  constructor(
    private fb: FormBuilder,
    private toast: ToastService,
    private route: ActivatedRoute,
    private router: Router,
    private service: ProveedoresService
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idProveedor: [0, Validators.required],
      idPersona: [0, Validators.required],
      ruc: ['', Validators.required],
      telefono: ['', Validators.required],
      email: ['', Validators.required],
    })
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idProveedor')
    var id = +param;

    this.initFormVacio()

    if (id > 0) {
      this.service.obtenerProveedorPorId(id)
        .subscribe(res => {
          this.tituloToolbar = `Proveedor`
          this.selectedPersona = res.idPersona
          this.form.patchValue({
            idProveedor: res.idProveedor,
            idPersona: res.idPersona,
            ruc: res.ruc,
            telefono: res.telefono,
            email: res.email,
          })
        }, error => {
          this.toast.toastError('Error al recuperar proveedor.')
        })
    }
  }

  editarGuardar() {

    if (this.form.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    let proveedor: Proveedor = this.form.value

    if (proveedor.idProveedor > 0) {
      this.service.editarProveedor(proveedor)
        .subscribe(
          res => {
            this.toast.toastExitoso('Proveedor creado exitosamente!')
            this.irAtras()
          },
          err => this.toast.toastError(err))
      return
    }

    this.service.agregarProveedor(proveedor)
      .subscribe(
        res => {
          this.toast.toastExitoso('Proveedor creado exitosamente!')
          this.irAtras()
        },
        err => this.toast.toastError(err))
  }

  private initFormVacio() {
    this.form.patchValue({
      idProveedor: 0,
      idPersona: 0,
      ruc: '',
      telefono: '',
      email: '',
    })
  }

  irAtras() {
    this.form.reset()
    this.router.navigate(['../../'], { relativeTo: this.route })
  }

  seleccionPersona(idPersona) {
    this.form.get('idPersona').setValue(idPersona);
  }
}
