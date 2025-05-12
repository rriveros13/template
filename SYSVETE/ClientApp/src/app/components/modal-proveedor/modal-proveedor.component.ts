import { Component, OnInit } from '@angular/core';
import { ProveedoresService } from '../../services/proveedores.service';
import { Proveedor } from '../../models/Proveedor';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-modal-proveedor',
  templateUrl: './modal-proveedor.component.html',
  styleUrls: ['./modal-proveedor.component.scss'],
})
export class ModalProveedorComponent implements OnInit {

  proveedores: Proveedor[] = [];
  idProveedor: number

  constructor(
    private service: ProveedoresService,
    private modal: ModalController
  ) { }

  ngOnInit() {
    this.service.obtenerProveedores()
      .subscribe(res => {
        this.proveedores = res
      })
  }

  seleccion(event) {
    let id = event.detail.value
    if (id) {
      this.idProveedor = id
      this.aceptar()
    }
  }

  aceptar() {
    this.dimiss('done', { idProveedor: this.idProveedor })
  }

  cancelar() {
    this.dimiss('cancel')
  }

  dimiss(role: string, data?: any) {
    this.modal.dismiss(data, role)
  }
}
