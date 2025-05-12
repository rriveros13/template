import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Insumo } from '../models/Insumo';

@Injectable({
  providedIn: 'root'
})
export class InsumoService {

  private url: string = `${environment.apiUrl}Insumo`

  constructor(private http: HttpClient) { }

  obtenerInsumos() {
    return this.http.get<Insumo[]>(this.url)
  }

  obtenerInsumosPorId(id: number) {
    return this.http.get<Insumo>(`${this.url}/obtenerPorId?idInsumo=${id}`)
  }

  agregarInsumos(dto: Insumo) {
    return this.http.post(this.url, dto)
  }

  editarInsumos(dto: Insumo) {
    return this.http.put(`${this.url}/updateRol`, dto) //Deberia ser updateInsumo
  }

  eliminarInsumos(id: number) {
    return this.http.delete(`${this.url}/${id}`)
  }
}
