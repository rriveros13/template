import { Component, OnInit } from '@angular/core';
import { Proveedor } from '../../models/Proveedor';
import { ProveedoresService } from '../../services/proveedores.service';
import { Router } from '@angular/router';
import { ToastService } from '../../helpers/toast.service';

@Component({
  selector: 'app-proveedores',
  templateUrl: './proveedores.page.html',
  styleUrls: ['./proveedores.page.scss'],
})
export class ProveedoresPage implements OnInit {

  public tituloToolbar: string = 'Proveedores'
  public proveedores: Proveedor[] = []

  constructor(
    private service: ProveedoresService,
    private router: Router,
    private toast: ToastService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerDatos()
  }

  private obtenerDatos() {
    this.service.obtenerProveedores()
      .subscribe(res => {
        this.proveedores = res
      }, error => {
        this.toast.toastError(`Error al obtener proveedores: ${error}`)
      })
  }

  confirmarEliminar(proveedor: Proveedor) {
    this.toast.alertConfirmarAccion(`Borrar ${proveedor.persona.nombre}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(proveedor.idProveedor)
        }
      })
  }

  private borrar(id: number) {
    this.service.eliminarProveedor(id)
      .subscribe(
        res => {
          this.toast.toastExitoso('Cliente proveedor!')
          this.obtenerDatos()
        },
        error => { this.toast.toastError(error) }
      )
  }
}
