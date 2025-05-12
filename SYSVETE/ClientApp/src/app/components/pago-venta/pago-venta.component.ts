import { Component, Input, OnInit } from '@angular/core';
import { PagoVenta } from '../../models/PagoVenta';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { metodoPago } from '../../../app.constants';
import { ToastService } from '../../helpers/toast.service';
import { ModalController } from '@ionic/angular';
import { CustomValidatorsService } from '../../services/custom-validators.service';
import { PagoVentaService } from '../../services/pago-venta.service';
import { VentasService } from '../../services/ventas.service';
import { registerLocaleData } from '@angular/common';
import es from '@angular/common/locales/es';

@Component({
  selector: 'app-pago-venta',
  templateUrl: './pago-venta.component.html',
  styleUrls: ['./pago-venta.component.scss'],
})
export class PagoVentaComponent implements OnInit {

  @Input() pago: PagoVenta
  @Input() idVenta: number

  form: FormGroup;
  metodoPago = metodoPago

  editar: boolean = false;

  totalPagar: number = 0
  totalPagado: number = 0
  pendiente: number = 0

  constructor(
    private fb: FormBuilder,
    private toast: ToastService,
    private modal: ModalController,
    private service: PagoVentaService,
    private serviceVentas: VentasService

  ) { }

  ngOnInit() {
    registerLocaleData(es)
    this.form = this.fb.group({
      idPago: [0, Validators.required],
      idVenta: [this.idVenta, CustomValidatorsService.mayorCero],
      fechaPago: [new Date().toISOString(), Validators.required],
      montoPagado: [0, CustomValidatorsService.mayorCero],
      metodoPago: ['', Validators.required],
    })

    if (this.pago) {
      this.form.patchValue({
        idPago: this.pago.idPago,
        idVenta: this.pago.idVenta,
        fechaPago: new Date(this.pago.fechaPago).toISOString(),
        montoPagado: this.pago.montoPagado,
        metodoPago: this.pago.metodoPago
      })
    }

    this.serviceVentas.obtenerMontoTotal(this.idVenta)
      .subscribe(res => {

        this.totalPagar = res.montoTotal
        this.totalPagado = res.montoAbonado
        this.pendiente = res.saldoPendiente

      }, error => {
        this.toast.toastError('Error al obtener pagos!');
      })
  }

  cerrar() {
    this.dismiss('cancel')
  }

  pagar() {
    if (this.form.invalid) {
      this.toast.toastError('Campos incorrectos')
      return
    }

    let dto: PagoVenta = this.form.value;

    if (this.pago) {
      this.service.editarPagosPorVenta(dto)
        .subscribe(
          res => {
            this.toast.toastExitoso('Pago editado!')
            this.dismiss('done')
          },
          error => { this.toast.toastError(error) })
      return
    }

    this.service.agregarPagosPorVenta(dto)
      .subscribe(
        res => {
          this.toast.toastExitoso('Pago registrado!')
          this.dismiss('done')
        },
        error => { this.toast.toastError(error) })

  }

  private dismiss(role: string, data?: any) {
    this.modal.dismiss(data, role)
  }
}
