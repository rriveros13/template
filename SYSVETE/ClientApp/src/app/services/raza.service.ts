import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Raza } from '../models/Raza';

@Injectable({
  providedIn: 'root'
})
export class RazaService {

  private url: string = `${environment.apiUrl}Raza`

  constructor(private http: HttpClient) { }

  obtenerRazas() {
    return this.http.get<Raza[]>(this.url)
  }

  obtenerRazaPorId(id: number) {
    return this.http.get<Raza>(`${this.url}/obtenerPorId?idRaza=${id}`)
  }

  agregarRaza(dto: Raza) {
    return this.http.post(this.url, dto)
  }

  editarRaza(dto: Raza) {
    return this.http.put(`${this.url}/updateRaza`, dto)
  }

  eliminarRaza(id: number) {
    return this.http.delete(`${this.url}/${id}`)
  }
}
