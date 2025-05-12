import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Tratamiento } from '../../../models/Tratamiento';
import { Procedimiento } from '../../../models/Procedimiento';
import { Patologia } from '../../../models/Patologia';
import { Vacuna } from '../../../models/Vacuna';
import { ToastService } from '../../../helpers/toast.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HistorialService } from '../../../services/historial.service';
import { TratamientosService } from '../../../services/tratamientos.service';
import { PatologiasService } from '../../../services/patologias.service';
import { ProcedimietosService } from '../../../services/procedimietos.service';
import { VacunasService } from '../../../services/vacunas.service';
import { Historial } from '../../../models/Historial';

@Component({
  selector: 'app-editar-historial',
  templateUrl: './editar-historial.page.html',
  styleUrls: ['./editar-historial.page.scss'],
})
export class EditarHistorialPage implements OnInit {

  tituloToolbar: string = 'Consulta'
  form: FormGroup;

  tratamientos: Tratamiento[] = [];
  procedimientos: Procedimiento[] = [];
  patologias: Patologia[] = [];
  vacunas: Vacuna[] = [];

  idPaciente: number

  constructor(
    private fb: FormBuilder,
    private toast: ToastService,
    private route: ActivatedRoute,
    private router: Router,
    private service: HistorialService,
    private tratamientoServ: TratamientosService,
    private patologiaServ: PatologiasService,
    private procServ: ProcedimietosService,
    private vacunaServ: VacunasService,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      idTratamiento: [null],
      idPaciente: [null, Validators.required],
      idPatologia: [null],
      idVacuna: [null],
      idProcedimiento: [null],
      descripcion: ['', Validators.required],
      fecha: [new Date().toISOString(), Validators.required],
    })
  }

  ionViewWillEnter() {

    const pidPaciente = this.route.snapshot.paramMap.get('idPaciente')
    this.idPaciente = +pidPaciente;

    const param = this.route.snapshot.paramMap.get('idHistorial')
    var id = +param

    this.initFormVacio()
    this.obtenerDatos()

    if (id > 0) {
      this.service.obtenerHistorialPorId(id)
        .subscribe(res => {
          let paciente = res.idPacienteNavigation.nombre
          this.tituloToolbar = `Consulta a ${paciente}`

          this.form.patchValue({
            idTratamiento: res.idTratamiento,
            idPaciente: res.idPaciente,
            idPatologia: res.idPatologia,
            idVacuna: res.idVacuna,
            idProcedimiento: res.idProcedimiento,
            descripcion: res.descripcion,
            fecha: new Date(res.fecha).toISOString()
          })
        }, error => {
          this.toast.toastError(`Error al obtener historial por id: ${error}`)
        })
    }
  }

  private obtenerDatos() {
    this.tratamientoServ.obtenerTratamientos()
      .subscribe(res => {
        this.tratamientos = res
      }, error => {
        this.toast.toastError(`Error al obtener tratamientos: ${error}`)
      })

    this.procServ.obtenerProcedimientos()
      .subscribe(res => {
        this.procedimientos = res
      }, error => {
        this.toast.toastError(`Error al obtener procedimientos: ${error}`)
      })

    this.patologiaServ.obtenerPatologias()
      .subscribe(res => {
        this.patologias = res
      }, error => {
        this.toast.toastError(`Error al obtener patologias: ${error}`)
      })

    this.vacunaServ.obtenerVacunas()
      .subscribe(res => {
        this.vacunas = res
      }, error => {
        this.toast.toastError(`Error al obtener vacunas: ${error}`)
      })

  }

  irAtras() {
    this.form.reset()
    this.router.navigate(['../../'], { relativeTo: this.route })
  }

  editarGuardar() {

    this.form.get('idPaciente').setValue(this.idPaciente)

    if (this.form.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    let historial: Historial = this.form.value

    if (historial.idHistorial > 0) {
      return
    }

    this.service.agregarHistorial(historial)
      .subscribe(
        res => {
          this.toast.toastExitoso('Historial creado exitosamente!')
          this.irAtras()
        },
        err => this.toast.toastError(err))
  }

  private initFormVacio() {
    this.form.patchValue({
      idTratamiento: null,
      idPaciente: null,
      idPatologia: null,
      idVacuna: null,
      idProcedimiento: null,
      descripcion: '',
      fecha: new Date().toISOString(),
    })
  }
}
