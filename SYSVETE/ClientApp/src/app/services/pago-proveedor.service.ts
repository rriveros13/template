import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { DeudaProveedor } from '../models/DeudaProveedor';

@Injectable({
  providedIn: 'root'
})
export class PagoProveedorService {

  private url: string = `${environment.apiUrl}DeudaProveedor`
  constructor(private http: HttpClient) { }

  obtenerDeudas() {
    return this.http.get<DeudaProveedor[]>(this.url)
  }

  obtenerDeudasPorID(idDeuda: number) {
    return this.http.get<DeudaProveedor>(`${this.url}/obtenerPorId?idDeuda=${idDeuda}`)
  }

  obtenerDeudasPorCompra(idCompra: number) {
    return this.http.get<DeudaProveedor[]>(`${this.url}/obtenerPorIdCompra?idCompra=${idCompra}`)
  }

  agregarPagoDeuda(dto: DeudaProveedor) {
    return this.http.post(this.url, dto)
  }

  editarPagoDeuda(dto: DeudaProveedor) {
    return this.http.put(`${this.url}/updateDeudaProveedor`, dto)
  }

  eliminarPagoDeuda(idDeuda: number) {
    return this.http.delete(`${this.url}/${idDeuda}`)
  }
}
