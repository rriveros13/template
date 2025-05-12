import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Patologia } from '../models/Patologia';

@Injectable({
  providedIn: 'root'
})
export class PatologiasService {

  private url: string = `${environment.apiUrl}Patologia`

  constructor(private http: HttpClient) { }

  obtenerPatologias() {
    return this.http.get<Patologia[]>(this.url)
  }

  obtenerPatologiaPorID(idPatologia: number) {
    return this.http.get<Patologia>(`${this.url}/obtenerPorId?idPatologia=${idPatologia}`)
  }

  agregarPatologia(dto: Patologia) {
    return this.http.post(this.url, dto)
  }

  editarPatologia(dto: Patologia) {
    return this.http.put(`${this.url}/updatePatologia`, dto)
  }

  eliminarPatologia(idPatologia: number) {
    return this.http.delete(`${this.url}/${idPatologia}`)
  }
}
