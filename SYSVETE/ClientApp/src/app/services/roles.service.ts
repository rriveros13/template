import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Rol } from '../models/Rol';

@Injectable({
  providedIn: 'root'
})
export class RolesService {

  private url: string = `${environment.apiUrl}Rol`

  constructor(private http: HttpClient) { }
  /**Obtiene los roles del sistema */
  obtenerRoles() {
    return this.http.get<Rol[]>(`${this.url}`)
  }

  /**Obtiene un un rol por ID */
  obtenerPorId(id: number) {
    return this.http.get<Rol>(`${this.url}/obtenerPorId?idRol=${id}`)
  }

  agregarRol(dto: Rol) {
    return this.http.post(`${this.url}`, dto)
  }

  editarRol(dto: Rol) {
    return this.http.put(`${this.url}/updateRol`, dto)
  }

  borrarRol(id: number) {
    return this.http.delete(`${this.url}/${id}`)
  }
}
