import { Component, OnInit } from '@angular/core';
import { Paciente } from '../models/Paciente';
import { PacienteService } from '../services/paciente.service';
import { ToastService } from '../helpers/toast.service';
import { of } from 'rxjs';

@Component({
  selector: 'app-historial',
  templateUrl: './historial.page.html',
  styleUrls: ['./historial.page.scss'],
})
export class HistorialPage implements OnInit {

  public tituloToolbar: string = 'Historial'

  pacientes: Paciente[] = []
  filtroPacientes: Paciente[] = []

  constructor(
    private pacienteServ: PacienteService,
    private toast: ToastService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerDatos()
  }

  private obtenerDatos() {
    this.pacienteServ.obtenerPacientes()
      .subscribe(res => {
        this.pacientes = res
        this.filtroPacientes = res;
      }, error => {
        this.toast.toastError(`Error al obtener pacientes: ${error}`)
      })
  }

  filtrar(ev: any) {
    let tmp = ev.detail.value
    if (tmp) {
      if (tmp.length > 0) {
        this.filtroPacientes = this.pacientes.filter(item => item.nombre.toLowerCase().indexOf(tmp) > -1
          || item.idClienteNavigation.idPersonaNavigation.nombre.toLowerCase().indexOf(tmp) > -1
          || item.idClienteNavigation.idPersonaNavigation.apellido.toLowerCase().indexOf(tmp) > -1)
        return
      }
    }
    this.filtroPacientes = this.pacientes
  }
}
