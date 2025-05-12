import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Persona } from '../models/Persona';

@Injectable({
  providedIn: 'root'
})
export class PersonasService {

  private url: string = `${environment.apiUrl}Personas`

  constructor(private http: HttpClient) { }

  obtenerPersonas() {
    return this.http.get<Persona[]>(this.url)
  }

  obtenerPersonasPorId(id: number) {
    return this.http.get<Persona>(`${this.url}/obtenerPorId?idPersona=${id}`)
  }

  agregarPersona(dto: Persona) {
    return this.http.post(this.url, dto)
  }
}
