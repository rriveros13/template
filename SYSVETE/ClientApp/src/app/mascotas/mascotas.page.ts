import { Component, OnInit } from '@angular/core';
import { ToastService } from '../helpers/toast.service';
import { Paciente } from '../models/Paciente';
import { PacienteService } from '../services/paciente.service';

@Component({
  selector: 'app-mascotas',
  templateUrl: './mascotas.page.html',
  styleUrls: ['./mascotas.page.scss'],
})
export class MascotasPage implements OnInit {

  public tituloToolbar: string = 'Pacientes'

  pacientes: Paciente[] = []

  constructor(
    private service: PacienteService,
    private toast: ToastService,
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerDatos();
  }

  private obtenerDatos() {
    this.service.obtenerPacientes()
      .subscribe(res => {
        this.pacientes = res
      }, error => {
        this.toast.toastError(`Error al obtener pacientes: ${error}`)
      })
  }

  confirmarEliminar(paciente: Paciente) {
    this.toast.alertConfirmarAccion(`Borrar ${paciente.nombre}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(paciente.idPaciente)
        }
      })
  }

  private borrar(id: number) {
    this.service.eliminarPaciente(id)
      .subscribe(
        res => {
          this.toast.toastExitoso('Paciente borrado!')
          this.obtenerDatos()
        },
        error => { this.toast.toastError(error) }
      )
  }
}
