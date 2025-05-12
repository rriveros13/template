import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Compra } from '../../models/Compra';
import { ToastService } from '../../helpers/toast.service';
import { CustomValidatorsService } from '../../services/custom-validators.service';
import { metodoPago } from '../../../app.constants';
import { ModalController } from '@ionic/angular';
import { DeudaProveedor } from '../../models/DeudaProveedor';
import { PagoProveedorService } from '../../services/pago-proveedor.service';
import { ComprasService } from '../../services/compras.service';
import { registerLocaleData } from '@angular/common';
import es from '@angular/common/locales/es';

@Component({
  selector: 'app-pago-proveedor',
  templateUrl: './pago-proveedor.component.html',
  styleUrls: ['./pago-proveedor.component.scss'],
})
export class PagoProveedorComponent implements OnInit {

  @Input() compra: Compra
  @Input() deuda: DeudaProveedor

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
    private service: PagoProveedorService,
    private compraService: ComprasService
  ) { }

  ngOnInit() {
    registerLocaleData(es)
    this.form = this.fb.group({
      idDeuda: [0, Validators.required],
      idCompra: [this.compra.idCompra, CustomValidatorsService.mayorCero],
      fechaPago: [new Date().toISOString(), Validators.required],
      montoPagado: [0, CustomValidatorsService.mayorCero],
      metodoPago: ['', Validators.required],
    })

    if (this.deuda) {
      this.editar = true;
      this.form.patchValue({
        idDeuda: this.deuda.idDeuda,
        idCompra: this.deuda.idCompra,
        fechaPago: new Date(this.deuda.fechaPago).toISOString(),
        montoPagado: this.deuda.montoPagado,
        metodoPago: this.deuda.metodoPago
      })
    }

    this.compraService.obtenerCompraConMontosPorId(this.compra.idCompra)
      .subscribe(res => {
        this.totalPagar = res.montoTotal
        this.totalPagado = res.montoAbonado
        this.pendiente = res.saldoPendiente

      }, error => {
        this.toast.toastError('Error al obtener pago de deudas!');
      })
  }

  cerrar() {
    this.dismiss('cancel')
  }

  pagarDeuda() {
    if (this.form.invalid) {
      this.toast.toastError('Campos incorrectos')
      return
    }

    let data: DeudaProveedor = this.form.value
    data.metodoPago = data.metodoPago
      .trimLeft()
      .trimRight()

    if (this.deuda) {
      this.service.editarPagoDeuda(data)
        .subscribe(
          res => {
            this.toast.toastExitoso('Pago editado!')
            this.dismiss('done')
          },
          error => { this.toast.toastError(error) }
        )
      return
    }

    this.service.agregarPagoDeuda(data)
      .subscribe(
        res => {
          this.toast.toastExitoso('Pago registrado!')
          this.dismiss('done')
        },
        error => { this.toast.toastError(error) }
      )
  }

  private dismiss(role: string, data?: any) {
    this.modal.dismiss(data, role)
  }
}
