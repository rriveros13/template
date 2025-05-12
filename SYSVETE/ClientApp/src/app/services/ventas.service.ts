import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Venta } from '../models/Venta';

@Injectable({
  providedIn: 'root'
})
export class VentasService {

  private url: string = `${environment.apiUrl}Venta`

  constructor(private http: HttpClient) { }

  obtenerVenta() {
    return this.http.get<Venta[]>(this.url)
  }

  obtenerVentaPorId(id: number) {
    return this.http.get<Venta>(`${this.url}/obtenerPorId?idVenta=${id}`)
  }
  
  obtenerMontoTotal(id: number) {
    return this.http.get<Venta>(`${this.url}/obtenerMontos?idVenta=${id}`)
  }

  agregar(venta: Venta) {
    return this.http.post(`${this.url}`, venta)
  }

  editar(dto: Venta) {
    return this.http.post(`${this.url}`, dto)
  }

  finalizar(idCompra: number) {
    return this.http.put(`${this.url}/finalizr`, idCompra)
  }

  borrar(id: number) {
    return this.http.delete(`${this.url}/${id}`)
  }
}
