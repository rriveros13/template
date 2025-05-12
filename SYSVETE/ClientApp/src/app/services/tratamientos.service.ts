import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Tratamiento } from '../models/Tratamiento';

@Injectable({
  providedIn: 'root'
})
export class TratamientosService {

  private url: string = `${environment.apiUrl}Tratamiento`

  constructor(private http: HttpClient) { }

  obtenerTratamientos() {
    return this.http.get<Tratamiento[]>(this.url)
  }

  obtenerTratamientoPorID(idTratamiento: number) {
    return this.http.get<Tratamiento>(`${this.url}/obtenerPorId?idTratamiento=${idTratamiento}`)
  }

  agregarTratamiento(dto: Tratamiento) {
    return this.http.post(this.url, dto)
  }

  editarTratamiento(dto: Tratamiento) {
    return this.http.put(`${this.url}/updateTratamiento`, dto)
  }

  eliminarTratamiento(id: number) {
    return this.http.delete(`${this.url}/${id}`)
  }
}
