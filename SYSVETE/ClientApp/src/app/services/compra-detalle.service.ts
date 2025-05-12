import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CompraDetalle } from '../models/CompraDetalle';

@Injectable({
  providedIn: 'root'
})
export class CompraDetalleService {

  private url: string = `${environment.apiUrl}CompraDetalle`

  constructor(private http: HttpClient) { }

  obtenerComprasDetalle(idCompra: number) {
    return this.http.get<CompraDetalle[]>(`${this.url}/${idCompra}`)
  }

  obtenerCompraDetallePorId(idDetalle: number) {
    return this.http.get<CompraDetalle>(`${this.url}/obtenerPorId?idDetalle=${idDetalle}`)
  }

  agregarCompraDetalle(detalle: CompraDetalle) {
    return this.http.post(`${this.url}/addCompraDetalle`, detalle)
  }

  editarCompraDetalle(compra: CompraDetalle) {
    return this.http.put(`${this.url}/updateCompraDetalle`, compra)
  }

  borrarCompraDetalle(idCompra: number) {
    return this.http.delete(`${this.url}?idDetalle=${idCompra}`)
  }
}
