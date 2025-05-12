import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Modulo } from '../models/Modulo';

@Injectable({
  providedIn: 'root'
})
export class ModuloService {

  private url: string = `${environment.apiUrl}Modulo`

  constructor(private http: HttpClient) { }

  obtenerModulos() {
    return this.http.get<Modulo[]>(this.url)
  }

  obtenerModuloPorID(idModulo: number) {
    return this.http.get<Modulo>(`${this.url}/obtenerPorId?idMoudlo=${idModulo}`)
  }

  agregarModulo(dto: Modulo) {
    return this.http.post(this.url, dto)
  }
  
  editarModulo(dto: Modulo) {
    return this.http.put(`${this.url}/updateModulo`, dto)
  }

  eliminarModulo(idModulo: number) {
    return this.http.delete(`${this.url}/${idModulo}`)
  }

}
