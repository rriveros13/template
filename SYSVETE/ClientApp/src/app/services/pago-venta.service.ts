import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { PagoVenta } from '../models/PagoVenta';

@Injectable({
  providedIn: 'root'
})
export class PagoVentaService {

  private url: string = `${environment.apiUrl}PagoVenta`
  constructor(private http: HttpClient) { }

  obtenerPagosVenta() {
    return this.http.get<PagoVenta[]>(this.url)
  }

  obtenerPagosPorID(idPago: number) {
    return this.http.get<PagoVenta>(`${this.url}/obtenerPorId?idPago=${idPago}`)
  }

  obtenerPagosPorVenta(idVenta: number) {
    return this.http.get<PagoVenta[]>(`${this.url}/obtenerPorIdVenta?idVenta=${idVenta}`)
  }

  agregarPagosPorVenta(dto: PagoVenta) {
    return this.http.post(this.url, dto)
  }

  editarPagosPorVenta(dto: PagoVenta) {
    return this.http.put(`${this.url}/updatePagoVenta`, dto)
  }

  eliminarPagosPorVenta(idPago: number) {
    return this.http.delete(`${this.url}/${idPago}`)
  }
}
