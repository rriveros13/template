import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastService } from '../../helpers/toast.service';
import { StockInsumosService } from '../../services/stock-insumos.service';
import { StockInsumo } from '../../models/StockInsumo';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-ajuste-insumo',
  templateUrl: './ajuste-insumo.component.html',
  styleUrls: ['./ajuste-insumo.component.scss'],
})
export class AjusteInsumoComponent implements OnInit {

  @Input() ajuste: StockInsumo
  form: FormGroup

  constructor(
    private fb: FormBuilder,
    private toats: ToastService,
    private service: StockInsumosService,
    private modal: ModalController,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      ajuste: [0, Validators.required]
    })
  }

  ajustarInsumo() {
    if (this.form.invalid) {
      return
    }

    let cantidadAjuste: number = this.form.value.ajuste;
    this.ajuste.cantidadActual = cantidadAjuste

    this.service.actualizarStockPorInsumo(this.ajuste)
      .subscribe(res => {
        this.toats.toastExitoso('Stock ajustado');
        this.dismiss('done')
      }, error => {
        this.toats.toastError(`${error}`);
      })
  }

  cerrar() {
    this.dismiss('cancel')
  }

  private dismiss(role: string, data?: any) {
    this.modal.dismiss(data, role)
  }
}
