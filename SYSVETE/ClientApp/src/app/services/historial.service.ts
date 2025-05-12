import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Historial } from '../models/Historial';

@Injectable({
  providedIn: 'root'
})
export class HistorialService {

  private url: string = `${environment.apiUrl}HistorialClinico`

  constructor(private http: HttpClient) { }

  obtenerHistorialPorPaciente(idPaciente: number) {
    return this.http.get<Historial[]>(`${this.url}/obtenerPorPaciente?idPaciente=${idPaciente}`)
  }

  obtenerHistorialPorId(id: number) {
    return this.http.get<Historial>(`${this.url}/obtenerPorId?idHistorial=${id}`)
  }

  agregarHistorial(dto: Historial) {
    return this.http.post(this.url, dto)
  }

  facturarServicios(idCliente: number, idVenta: number) {
    return this.http.post(`${this.url}/FacturarServicios?idCliente=${idCliente}&idVenta=${idVenta}`, null)
  }

  editarHistorial(dto: Historial) {
    return this.http.put(`${this.url}/updateHistorial`, dto)
  }

  eliminarHistorial(id: number) {
    return this.http.delete(`${this.url}?idHistorial=${id}`)
  }
}
