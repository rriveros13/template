import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Cliente } from '../models/Cliente';

@Injectable({
  providedIn: 'root'
})
export class ClientesService {

  private url: string = `${environment.apiUrl}Cliente`

  constructor(private http: HttpClient) { }

  obtenerClientes() {
    return this.http.get<Cliente[]>(this.url)
  }

  obtenerClientePorId(idCliente: number) {
    return this.http.get<Cliente>(`${this.url}/obtenerPorId?idCliente=${idCliente}`)
  }

  agregarCliente(cliente: Cliente) {
    return this.http.post(this.url, cliente)
  }

  editarCliente(cliente: Cliente) {
    return this.http.put(`${this.url}/updateCliente`, cliente)
  }

  eliminarCliente(idCliente: number) {
    return this.http.delete(`${this.url}?idCliente=${idCliente}`)
  }
}
