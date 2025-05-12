import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Usuario } from '../models/Usuario';

@Injectable({
  providedIn: 'root'
})
export class UsuariosService {

  private url: string = `${environment.apiUrl}Usuario`

  constructor(
    private http: HttpClient
  ) { }

  obtenerUsuarios() {
    return this.http.get<Usuario[]>(this.url);
  }

  obtenerUsuariosPorId(idUsuario: number) {
    return this.http.get<Usuario>(`${this.url}/${idUsuario}`);
  }

  insertarUsuario(dto: Usuario) {
    return this.http.post<number>(this.url, dto);
  }

  editarUsuario(dto: Usuario) {
    
  }

  eliminarUsuario(idUsuario: number) {
    return this.http.delete(`${this.url}/${idUsuario}`);
  }
}
