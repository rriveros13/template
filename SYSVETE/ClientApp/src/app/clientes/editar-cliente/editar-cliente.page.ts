import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonaComponent } from '../../components/persona/persona.component';
import { ToastService } from '../../helpers/toast.service';
import { Cliente } from '../../models/Cliente';
import { Persona } from '../../models/Persona';
import { ClientesService } from '../../services/clientes.service';

@Component({
  selector: 'app-editar-cliente',
  templateUrl: './editar-cliente.page.html',
  styleUrls: ['./editar-cliente.page.scss'],
})
export class EditarClientePage implements OnInit {


  form: FormGroup;

  tituloToolbar: string = 'Nuevo cliente'
  selectedPersona: number


  constructor(
    private fb: FormBuilder,
    private toast: ToastService,
    private route: ActivatedRoute,
    private router: Router,
    private service: ClientesService,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idCliente: [0, Validators.required],
      idPersona: [0, Validators.required],
      ruc: ['', Validators.required],
      telefono: ['', Validators.required],
      email: ['', Validators.required],
    })
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idCliente')
    var id = +param;

    this.initFormVacio()

    if (id > 0) {
      this.service.obtenerClientePorId(id)
        .subscribe(res => {
          this.tituloToolbar = `Cliente ${res.idPersonaNavigation.nombre}`
          this.selectedPersona = res.idPersona
          this.form.patchValue({
            idCliente: res.idCliente,
            idPersona: res.idPersona,
            ruc: res.ruc,
            telefono: res.telefono,
            email: res.email,
          })
        }, error => {
          this.toast.toastError('Error al recuperar cliente.')
          console.log(error)
        })
    }
  }



  editarGuardar() {

    if (this.form.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }
    let cliente: Cliente = this.form.value

    if (cliente.idCliente > 0) {
      this.service.editarCliente(cliente)
        .subscribe(
          res => {
            this.toast.toastExitoso('Cliente editado exitosamente!')
            this.irAtras()
          },
          err => this.toast.toastError(err))
      return
    }

    this.service.agregarCliente(cliente)
      .subscribe(
        res => {
          this.toast.toastExitoso('Cliente creado exitosamente!')
          this.irAtras()
        },
        err => this.toast.toastError(err))
  }

  private initFormVacio() {
    this.form.patchValue({
      idCliente: 0,
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
