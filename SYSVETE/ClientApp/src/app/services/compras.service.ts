import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Compra } from '../models/Compra';

@Injectable({
  providedIn: 'root'
})
export class ComprasService {

  private url: string = `${environment.apiUrl}Compra`

  constructor(private http: HttpClient) { }

  obtenerCompras() {
    return this.http.get<Compra[]>(this.url)
  }

  obtenerCompraPorId(idCompra: number) {
    return this.http.get<Compra>(`${this.url}/obtenerPorId?idCompra=${idCompra}`)
  }

  obtenerCompraConMontosPorId(idCompra: number) {
    return this.http.get<Compra>(`${this.url}/obtenerMontos?idCompra=${idCompra}`)
  }

  agregarCompra(compra: Compra) {
    return this.http.post(`${this.url}`, compra)
  }

  editarCompra(compra: Compra) {
    return this.http.put(`${this.url}/updateCompra`, compra)
  }

  finalizar(idCompra: number) {
    return this.http.put(`${this.url}/finalizar`, idCompra)
  }

  borrarCompra(idCompra: number) {
    return this.http.delete(`${this.url}?idCompra=${idCompra}`)
  }
}
