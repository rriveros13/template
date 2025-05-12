export interface VentaDetalle {
  idVentaDetalle: number
  idVenta: number
  idInsumo: number
  idHistorial: number
  fechaVenta: Date
  descripcion: string
  precio: number
  cantidad: number
  idInsumoNavigation: any
}
