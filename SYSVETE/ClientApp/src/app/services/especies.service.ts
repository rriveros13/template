import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Especie } from '../models/Especie';

@Injectable({
  providedIn: 'root'
})
export class EspeciesService {

  private url: string = `${environment.apiUrl}Especie`

  constructor(private http: HttpClient) { }

  obtenerEspecies() {
    return this.http.get<Especie[]>(this.url)
  }

  obtenerEspeciesPorId(idEspecie: number) {
    return this.http.get<Especie>(`${this.url}/obtenerPorId?idEspecie=${idEspecie}`)
  }

  agregarEspecie(especie: Especie) {
    return this.http.post(this.url, especie)
  }

  editarEspecie(especie: Especie) {
    return this.http.put(`${this.url}/updateEspecie`, especie)
  }

  eliminarEspecie(idEspecie: number) {
    return this.http.delete(`${this.url}/${idEspecie}`)
  }


}
