import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Proveedor } from '../models/Proveedor';

@Injectable({
  providedIn: 'root'
})
export class ProveedoresService {

  private url: string = `${environment.apiUrl}Proveedor`

  constructor(private http: HttpClient) { }

  obtenerProveedores() {
    return this.http.get<Proveedor[]>(this.url)
  }

  obtenerProveedorPorId(id: number) {
    return this.http.get<Proveedor>(`${this.url}/obtenerPorId?idProveedor=${id}`)
  }

  agregarProveedor(dto: Proveedor) {
    return this.http.post(`${this.url}/addProveedor`,  dto)
  }

  editarProveedor(dto: Proveedor) {
    return this.http.put(`${this.url}/updateProveedor`, dto)
  }

  eliminarProveedor(id: number) {
    return this.http.delete(`${this.url}/${id}`)
  }
}
