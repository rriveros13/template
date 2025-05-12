import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-desde-hasta',
  templateUrl: './desde-hasta.component.html',
  styleUrls: ['./desde-hasta.component.scss'],
})
export class DesdeHastaComponent implements OnInit {

  fInicio: string;
  fFin: string;

  constructor(
    private modal: ModalController
  ) { }



  ngOnInit() {
    let hoy = new Date()
    this.fInicio = `${hoy.getDate()}/${hoy.getMonth() + 1}/${hoy.getFullYear()}`
    this.fFin = `${hoy.getDate()}/${hoy.getMonth() + 1}/${hoy.getFullYear()}`
  }

  desde(ev: any) {
    let tmp = new Date(ev.detail.value)
    let fecha = `${tmp.getDate()}/${tmp.getMonth() + 1}/${tmp.getFullYear()}`
    this.fInicio = fecha
  }

  hasta(ev: any) {
    let tmp = new Date(ev.detail.value)
    let fecha = `${tmp.getDate()}/${tmp.getMonth() + 1}/${tmp.getFullYear()}`
    this.fFin = fecha
  }

  aceptar() {
    this.dimiss('done', { desde: this.fInicio, hasta: this.fFin })
  }

  cancelar() {
    this.dimiss('cancel')
  }

  dimiss(role: string, data?: any) {
    this.modal.dismiss(data, role)
  }
}
