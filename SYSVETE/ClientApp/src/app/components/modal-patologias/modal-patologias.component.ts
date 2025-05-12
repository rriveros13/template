import { Component, OnInit } from '@angular/core';
import { Patologia } from '../../models/Patologia';
import { PatologiasService } from '../../services/patologias.service';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-modal-patologias',
  templateUrl: './modal-patologias.component.html',
  styleUrls: ['./modal-patologias.component.scss'],
})
export class ModalPatologiasComponent implements OnInit {

  patologias: Patologia[] = [];
  idPatologia: number

  constructor(
    private service: PatologiasService,
    private modal: ModalController
  ) { }

  ngOnInit() {
    this.service.obtenerPatologias()
      .subscribe(res => {
        this.patologias = res
      })
  }

  seleccion(event) {
    let id = event.detail.value
    if (id) {
      this.idPatologia = id
      this.aceptar()
    }
  }

  aceptar() {
    this.dimiss('done', { idPatologia: this.idPatologia })
  }

  cancelar() {
    this.dimiss('cancel')
  }

  dimiss(role: string, data?: any) {
    this.modal.dismiss(data, role)
  }

}
