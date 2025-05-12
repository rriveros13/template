import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { VentaDetalle } from '../models/VentaDetalle';

@Injectable({
  providedIn: 'root'
})
export class VentaDetalleService {

  private url: string = `${environment.apiUrl}VentaDetalle`

  constructor(private http: HttpClient) { }

  obtenerVentasDetalle(idventa: number) {
    return this.http.get<VentaDetalle[]>(`${this.url}/obtenerPorCabecera?idVenta=${idventa}`)
  }

  obtenerVentasDetallePorId(idDetalle: number) {
    return this.http.get<VentaDetalle>(`${this.url}/obtenerPorId?idVenta=${idDetalle}`)
  }

  agregarVentasDetalle(detalle: VentaDetalle) {
    return this.http.post(`${this.url}`, detalle)
  }

  editarVentasDetalle(dto: VentaDetalle) {
    return this.http.put(`${this.url}/updateVenta`, dto)
  }

  borrarVentasDetalle(idDetalle: number) {
    return this.http.delete(`${this.url}/${idDetalle}`)
  }
}
