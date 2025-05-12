import { Component, OnInit } from '@angular/core';
import { ToastService } from '../helpers/toast.service';
import { Cliente } from '../models/Cliente';
import { ClientesService } from '../services/clientes.service';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.page.html',
  styleUrls: ['./clientes.page.scss'],
})
export class ClientesPage implements OnInit {

  public tituloToolbar: string = 'Clientes'

  clientes: Cliente[] = []

  constructor(
    private service: ClientesService,
    private toast: ToastService,
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerDatos();
  }

  private obtenerDatos() {
    this.service.obtenerClientes()
      .subscribe(res => {
        this.clientes = res
      }, error => {
        this.toast.toastError(`Error al obtener clientes: ${error}`)
      })
  }

  confirmarEliminar(cliente: Cliente) {
    this.toast.alertConfirmarAccion(`Borrar ${cliente.idPersonaNavigation.nombre}`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(cliente.idCliente)
        }
      })
  }

  private borrar(id: number) {
    this.service.eliminarCliente(id)
      .subscribe(
        res => {
          this.toast.toastExitoso('Cliente borrado!')
          this.obtenerDatos()
        },
        error => { this.toast.toastError(error) }
      )
  }
}
