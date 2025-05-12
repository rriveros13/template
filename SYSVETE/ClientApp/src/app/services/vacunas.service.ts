import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Vacuna } from '../models/Vacuna';

@Injectable({
  providedIn: 'root'
})
export class VacunasService {

  private url: string = `${environment.apiUrl}Vacuna`

  constructor(private http: HttpClient) { }

  obtenerVacunas() {
    return this.http.get<Vacuna[]>(this.url)
  }

  obtenerVacunaPorID(id: number) {
    return this.http.get<Vacuna>(`${this.url}/obtenerPorId?idVacuna=${id}`)
  }

  agregarVacuna(dto: Vacuna) {
    return this.http.post(this.url, dto)
  }

  editarVacuna(dto: Vacuna) {
    return this.http.put(`${this.url}/updateVacuna`, dto)
  }

  eliminarVacuna(id: number) {
    return this.http.delete(`${this.url}?idVacuna=${id}`)
  }
}
