import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../helpers/toast.service';
import { Cliente } from '../../models/Cliente';
import { Paciente } from '../../models/Paciente';
import { Raza } from '../../models/Raza';
import { ClientesService } from '../../services/clientes.service';
import { CustomValidatorsService } from '../../services/custom-validators.service';
import { PacienteService } from '../../services/paciente.service';
import { RazaService } from '../../services/raza.service';

@Component({
  selector: 'app-editar-mascota',
  templateUrl: './editar-mascota.page.html',
  styleUrls: ['./editar-mascota.page.scss'],
})
export class EditarMascotaPage implements OnInit {

  public tituloToolbar: string = 'Pacientes'

  form: FormGroup;

  sexos = ['M', 'H']

  razas: Raza[] = [];

  clientes: Cliente[] = []

  constructor(
    private fb: FormBuilder,
    private toast: ToastService,
    private route: ActivatedRoute,
    private router: Router,
    private service: PacienteService,
    private razaService: RazaService,
    private clieneService: ClientesService,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idPaciente: [0, Validators.required],
      idRaza: [null, Validators.required],
      idCliente: [null, Validators.required],
      nombre: ['', Validators.required],
      edad: [1, [Validators.required, CustomValidatorsService.mayorCero]],
      sexo: ['M', Validators.required],
      peso: [0, [Validators.required, CustomValidatorsService.mayorCero]],
    })
  }

  ionViewWillEnter() {

    this.obtenerDatos()
    this.initFormVacio()

    const param = this.route.snapshot.paramMap.get('idPaciente')
    var id = +param;
    if (id > 0) {
      this.service.obtenerPacientePorID(id)
        .subscribe(res => {
          this.tituloToolbar = `Paciente ${res.nombre}`
          this.form.patchValue({
            idPaciente: res.idPaciente,
            idCliente: res.idCliente,
            idRaza: res.idRaza,
            edad: res.edad,
            nombre: res.nombre,
            sexo: res.sexo,
            peso: res.peso,
          })
        },
          error => {
            this.toast.toastError('Error al recuperar paciente.')
          })
    }
  }

  editarGuardar() {
    if (this.form.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    let paciente: Paciente = this.form.value

    if (paciente.idPaciente > 0) {
      this.service.editarPaciente(paciente)
        .subscribe(
          res => {
            this.toast.toastExitoso('Paciente modificado exitosamente!')
            this.irAtras()
          },
          err => this.toast.toastError(err))
      return
    }

    this.service.agregarPaciente(paciente)
      .subscribe(
        res => {
          this.toast.toastExitoso('Paciente creado exitosamente!')
          this.irAtras()
        },
        err => this.toast.toastError(err))
  }

  obtenerDatos() {
    this.razaService.obtenerRazas()
      .subscribe(res => {
        this.razas = res
      },
        err => this.toast.toastError(err))

    this.clieneService.obtenerClientes()
      .subscribe(res => {
        this.clientes = res
      },
        err => this.toast.toastError(err))
  }

  irAtras() {
    this.form.reset()
    this.router.navigate(['../../'], { relativeTo: this.route })
  }

  private initFormVacio() {
    this.form.patchValue({
      idPaciente: 0,
      idCliente: 0,
      idRaza: 0,
      edad: 1,
      nombre: '',
      sexo: 'M',
      peso: 0,
    })
  }
}
