import { Component, OnInit } from '@angular/core';
import { Historial } from '../../models/Historial';
import { HistorialService } from '../../services/historial.service';
import { ToastService } from '../../helpers/toast.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-historial-paciente',
  templateUrl: './historial-paciente.page.html',
  styleUrls: ['./historial-paciente.page.scss'],
})
export class HistorialPacientePage implements OnInit {

  historialPaciente: Historial[] = []
  filtroHis: Historial[] = []
  tituloToolbar: string = 'Consulta'

  idPaciente: number = 0

  desde: Date
  hasta: Date = new Date();

  constructor(
    private service: HistorialService,
    private toast: ToastService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {

    const param = this.route.snapshot.paramMap.get('idPaciente')
    this.idPaciente = +param;

    this.obtenerDatos()
  }

  private obtenerDatos() {
    this.service.obtenerHistorialPorPaciente(this.idPaciente)
      .subscribe(res => {
        this.historialPaciente = res
        this.filtroHis = res
        let paciente = res[0].idPacienteNavigation.nombre
        this.tituloToolbar = `Consulta a ${paciente}`
      }, error => {
        this.toast.toastError(`Error al obtener historia de paciente: ${error}`)
      })
  }

  filtroDesde(ev) {
    if (ev.detail.value) {
      this.desde = new Date(ev.detail.value)
      this.aplicarFiltro()
    }
  }

  filtroHasta(ev) {
    if (ev.detail.value) {
      this.hasta = new Date(ev.detail.value)
      this.aplicarFiltro()
    }
  }

  aplicarFiltro() {
    this.filtroHis = this.historialPaciente.filter(registro => {

      const filtroFecha = !this.desde || !this.hasta ||
        (new Date(registro.fecha) >= this.desde && new Date(registro.fecha) <= this.hasta);
      return filtroFecha;
    });

  }
}
