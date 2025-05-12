import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Procedimiento } from '../models/Procedimiento';

@Injectable({
  providedIn: 'root'
})
export class ProcedimietosService {

  private url: string = `${environment.apiUrl}Procedimiento`

  constructor(private http: HttpClient) { }

  obtenerProcedimientos() {
    return this.http.get<Procedimiento[]>(this.url)
  }

  obtenerProcedimientoPorId(id: number) {
    return this.http.get<Procedimiento>(`${this.url}/obtenerPorId?idProcedimiento=${id}`)
  }

  agregarProcedimiento(dto: Procedimiento) {
    return this.http.post(`${this.url}`, dto)
  }

  editarProcedimiento(dto: Procedimiento) {
    return this.http.put(`${this.url}/updateProcedimiento`, dto)
  }

  eliminarProcedimiento(id: number) {
    return this.http.delete(`${this.url}/${id}`)
  }
}
