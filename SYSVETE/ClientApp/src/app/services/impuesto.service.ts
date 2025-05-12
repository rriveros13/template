import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Impuesto } from '../models/Impuesto';

@Injectable({
  providedIn: 'root'
})
export class ImpuestoService {

  private url: string = `${environment.apiUrl}Impuesto`

  constructor(private http: HttpClient) { }

  obtenerImpuestos() {
    return this.http.get<Impuesto[]>(this.url)
  }

  obtenerImpuestoPorId(id: number) {
    return this.http.get<Impuesto>(`${this.url}/obtenerPorId?idImpuesto=${id}`)
  }

  agregarImpuesto(dto: Impuesto) {
    return this.http.post(this.url, dto)
  }

  editarImpuesto(dto: Impuesto) {
    return this.http.put(`${this.url}/updateImpuesto`, dto)
  }

  eliminarImpuesto(id: number) {
    return this.http.delete(`${this.url}/${id}`)
  }
}
