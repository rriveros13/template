export interface Venta {
  idVenta: number
  idCliente: number
  nroBoleta: string
  fechaVenta: Date
  montoAbonado: number
  montoTotal: number
  saldoPendiente: number
  facturado: boolean
  finalizado: boolean
}


