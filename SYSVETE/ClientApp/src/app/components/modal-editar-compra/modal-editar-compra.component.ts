import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CompraDetalle } from '../../models/CompraDetalle';
import { Insumo } from '../../models/Insumo';
import { InsumoService } from '../../services/insumo.service';
import { ToastService } from '../../helpers/toast.service';
import { CompraDetalleService } from '../../services/compra-detalle.service';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-modal-editar-compra',
  templateUrl: './modal-editar-compra.component.html',
  styleUrls: ['./modal-editar-compra.component.scss'],
})
export class ModalEditarCompraComponent implements OnInit {

  @Input() detalle: string

  formDetalle: FormGroup;
  insumos: Insumo[] = [];
  constructor(
    private fb: FormBuilder,
    private insumoService: InsumoService,
    private toast: ToastService,
    private service: CompraDetalleService,
    private modal: ModalController,
  ) { }

  ngOnInit() {

    this.insumoService.obtenerInsumos()
      .subscribe(res => {
        this.insumos = res

        let detalle: CompraDetalle = JSON.parse(this.detalle);

        this.formDetalle.patchValue({
          idCompraDetalle: detalle.idCompraDetalle,
          idCompra: detalle.idCompra,
          idInsumo: detalle.idInsumo,
          descripcion: detalle.descripcion,
          precio: detalle.precio,
          cantidad: detalle.cantidad
        })

      }, error => {
        this.toast.toastError(`Error al obtener insumos: ${error}`)
      })

    this.formDetalle = this.fb.group({
      idCompraDetalle: [0, Validators.required],
      idCompra: [0, Validators.required],
      idInsumo: [0, Validators.required],
      descripcion: ['', Validators.required],
      precio: [0, Validators.required],
      cantidad: [0, Validators.required],
    })

  }

  editar() {
    if (this.formDetalle.invalid) {
      this.toast.toastError('Campos no validos')
      return
    }

    let detalle: CompraDetalle = this.formDetalle.value

    this.service.editarCompraDetalle(detalle)
      .subscribe(
        res => {
          this.toast.toastExitoso('Detalle editado exitosamente!')
          this.dismiss('done')
        },
        err => this.toast.toastError(err))
  }

  cancelar() {
    this.dismiss('cancel')
  }

  private dismiss(role, data?) {
    this.modal.dismiss(data, role)
  }
}
