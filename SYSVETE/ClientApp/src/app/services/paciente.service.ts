import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Paciente } from '../models/Paciente';

@Injectable({
  providedIn: 'root'
})
export class PacienteService {

  private url: string = `${environment.apiUrl}Paciente`
  constructor(private http: HttpClient) { }

  obtenerPacientes() {
    return this.http.get<Paciente[]>(this.url)
  }

  obtenerPacientePorID(idPaciente: number) {
    return this.http.get<Paciente>(`${this.url}/obtenerPorId?idPaciente=${idPaciente}`)
  }

  agregarPaciente(dto: Paciente) {
    return this.http.post(this.url, dto)
  }

  editarPaciente(dto: Paciente) {
    return this.http.put(`${this.url}/updatePaciente`, dto)
  }

  eliminarPaciente(idPaciente: number) {
    return this.http.delete(`${this.url}?idPaciente=${idPaciente}`)
  }
}
