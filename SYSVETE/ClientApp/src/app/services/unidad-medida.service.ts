import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { UnidadMedida } from '../models/UnidadMedida';

@Injectable({
  providedIn: 'root'
})
export class UnidadMedidaService {

  private url: string = `${environment.apiUrl}UnidadMedida`

  constructor(private http: HttpClient) { }

  obtenerUnidadesMedida() {
    return this.http.get<UnidadMedida[]>(this.url)
  }

  obtenerUnidadMedidaPorId(id: number) {
    return this.http.get<UnidadMedida>(`${this.url}/obtenerPorId?idUnidad=${id}`)
  }

  agregarUnidadMedida(dto: UnidadMedida) {
    return this.http.post(this.url, dto)
  }

  editarUnidadMedida(dto: UnidadMedida) {
    return this.http.put(`${this.url}/updateUnidad`, dto)
  }

  eliminarUnidadMedida(id: number) {
    return this.http.delete(`${this.url}/${id}`)
  }
}
