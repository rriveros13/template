import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { StockInsumo } from '../models/StockInsumo';

@Injectable({
  providedIn: 'root'
})
export class StockInsumosService {

  private url: string = `${environment.apiUrl}StockInsumos`

  constructor(private http: HttpClient) { }

  obtenerstock() {
    return this.http.get<StockInsumo[]>(this.url)
  }

  obtenerStockPorId(id: number) {
    return this.http.get<StockInsumo>(`${this.url}/obtenerPorId?idStcok=${id}`)
  }
  
  obtenerStockPorInsumo(id: number) {
    return this.http.get<StockInsumo>(`${this.url}/obteberPorInsumo?idInsumo=${id}`)
  }

  actualizarStockPorInsumo(dto: StockInsumo) {
    return this.http.put(`${this.url}/updateStock`, dto)
  }

  eliminarStock(id: number) {
    return this.http.delete(`${this.url}/${id}`)
  }
}
