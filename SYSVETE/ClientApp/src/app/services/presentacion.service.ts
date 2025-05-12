import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Presentacion } from '../models/Presentacion';

@Injectable({
  providedIn: 'root'
})
export class PresentacionService {

  private url: string = `${environment.apiUrl}Presentacion`

  constructor(private http: HttpClient) { }

  obtenerPresentaciones() {
    return this.http.get<Presentacion[]>(this.url)
  }

  obtenerPresentacionPorId(id: number) {
    return this.http.get<Presentacion>(`${this.url}/obtenerPorId?idPresentacion=${id}`)
  }

  agregarPresentacion(dto: Presentacion) {
    return this.http.post(this.url, dto)
  }

  editarPresentacion(dto: Presentacion) {
    return this.http.put(`${this.url}/updatePresentacion`, dto)
  }

  eliminarPresentacion(id: number) {
    return this.http.delete(`${this.url}/${id}`)
  }
}
