export interface Compra {
  idCompra: number
  idProveedor: number
  nroBoleta: string
  fechaCompra: Date
  facturado: boolean
  montoAbonado: number
  montoTotal: number
  saldoPendiente: number
  tipoCompra: string
  finalizado: boolean
}


