import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { TipoInsumo } from '../models/TipoInsumo';

@Injectable({
  providedIn: 'root'
})
export class TipoInsumoService {

  private url: string = `${environment.apiUrl}TipoInsumo`

  constructor(private http: HttpClient) { }

  obtenerTipoInsumo() {
    return this.http.get<TipoInsumo[]>(this.url)
  }

  obtenerTipoInsumoPorId(id: number) {
    return this.http.get<TipoInsumo>(`${this.url}/obtenerPorId?idTipoInsumo=${id}`)
  }

  agregarTipoInsumo(dto: TipoInsumo) {
    return this.http.post(this.url, dto)
  }

  editarTipoInsumo(dto: TipoInsumo) {
    return this.http.put(`${this.url}/updateRol`, dto) //Deberia set updateTipoInsumo
  }

  eliminarTipoInsumo(id: number) {
    return this.http.delete(`${this.url}/${id}`)
  }
}
