import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Permiso } from '../models/Permiso';

@Injectable({
  providedIn: 'root'
})
export class PermisosService {

  private url: string = `${environment.apiUrl}Permiso`

  constructor(
    private http: HttpClient
  ) { }

  obtenerPermisos() {
    return this.http.get<Permiso[]>(`${this.url}`)
  }

  obtenerPermisoPorId(idPermiso: number) {
    return this.http.get<Permiso>(`${this.url}/obtenerPorId?idPermiso=${idPermiso}`)
  }

  obtenerPermisosPorRol(idRol: number) {
  }

  nuevoPermiso(dto: Permiso) {
    return this.http.post(`${this.url}/addPermiso`, dto)
  }

  editarPermiso(dto: Permiso) {
    return this.http.put(`${this.url}/updatePermiso`, dto)
  }

  eliminarPermiso(idPermiso: number) {
    return this.http.delete(`${this.url}/${idPermiso}`)
  }
}
