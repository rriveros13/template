import { Component, OnInit, ViewChild } from '@angular/core';
import { ProveedoresService } from '../../../services/proveedores.service';
import { Proveedor } from '../../../models/Proveedor';
import { ToastService } from '../../../helpers/toast.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Compra } from '../../../models/Compra';
import { ComprasService } from '../../../services/compras.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Insumo } from '../../../models/Insumo';
import { InsumoService } from '../../../services/insumo.service';
import { CompraDetalle } from '../../../models/CompraDetalle';
import { CompraDetalleService } from '../../../services/compra-detalle.service';
import { registerLocaleData } from '@angular/common';
import es from '@angular/common/locales/es';
import { IonSelect, ModalController } from '@ionic/angular';
import { ModalEditarCompraComponent } from '../../../components/modal-editar-compra/modal-editar-compra.component';
import { Impuesto } from '../../../models/Impuesto';
import { ImpuestoService } from '../../../services/impuesto.service';
import { TipoCompra } from '../../../../app.constants';

@Component({
  selector: 'app-editar-compra',
  templateUrl: './editar-compra.page.html',
  styleUrls: ['./editar-compra.page.scss'],
})
export class EditarCompraPage implements OnInit {

  @ViewChild('impuestoSelect', { static: false }) impuestoSelect: IonSelect

  tituloToolbar: string = 'Compra'
  proveedores: Proveedor[] = [];
  insumos: Insumo[] = [];
  detalles: CompraDetalle[] = [];

  tiposCompras = TipoCompra

  formCompra: FormGroup;
  formDetalle: FormGroup;

  accordion: string[] = ['a1', 'a2']
  multiple: boolean = true;

  edicion: boolean = false;
  total: number = 0;

  idCompra: number = 0;

  impuestos: Impuesto[] = [];

  detallesImpuesto = [];

  finalizado: boolean = false;

  puedeFinalizar: boolean = false

  constructor(
    private provService: ProveedoresService,
    private insumoService: InsumoService,
    private toast: ToastService,
    private fb: FormBuilder,
    private service: ComprasService,
    private servDetalle: CompraDetalleService,
    private route: ActivatedRoute,
    private router: Router,
    private modal: ModalController,
    private impuestoService: ImpuestoService,
  ) { }

  ngOnInit() {

    registerLocaleData(es)
    this.formCompra = this.fb.group({
      idCompra: [0, Validators.required],
      idProveedor: [0, Validators.required],
      nroBoleta: ['', Validators.required],
      fechaCompra: [new Date().toISOString(), Validators.required],
      tipoCompra: ['', Validators.required],
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

  ionViewWillEnter() {
    this.edicion = false;
    this.obtenerDatos();

    const param = this.route.snapshot.paramMap.get('idCompra')
    this.idCompra = +param;

    this.initFormCompra();
    this.initFormDetalle();

    if (this.idCompra > 0) {
      this.service.obtenerCompraPorId(this.idCompra)
        .subscribe(res => {
          this.tituloToolbar = `Compra Nro ${res.idCompra}`
          this.formCompra.patchValue({
            idCompra: res.idCompra,
            idProveedor: res.idProveedor,
            nroBoleta: res.nroBoleta,
            fechaCompra: new Date(res.fechaCompra).toISOString(),
            tipoCompra: res.tipoCompra,
          })
          this.accordion = ['a1', 'a2']
          this.edicion = true;
          this.finalizado = res.finalizado
          this.obtenerDetalles(res.idCompra)
        }, error => {
          this.toast.toastError('Error al recuperar compra.')
        })
    }
  }

  private obtenerDatos() {
    this.provService.obtenerProveedores()
      .subscribe(res => {
        this.proveedores = res
      }, error => {
        this.toast.toastError(`Error al obtener proveedores: ${error}`)
      })

    this.insumoService.obtenerInsumos()
      .subscribe(res => {
        this.insumos = res
      }, error => {
        this.toast.toastError(`Error al obtener insumos: ${error}`)
      })

    this.impuestoService.obtenerImpuestos()
      .subscribe(res => {
        this.impuestos = res
      }, error => {
        this.toast.toastError(`Error al obtener impuestos: ${error}`)
      })
  }

  private initFormDetalle() {
    this.formCompra.patchValue({
      idCompraDetalle: 0,
      idCompra: 0,
      idInsumo: 0,
      descripcion: '',
      precio: 0,
      cantidad: 0
    })
  }

  private initFormCompra() {
    this.formCompra.patchValue({
      idCompra: 0,
      idProveedor: 0,
      nroBoleta: '',
      fechaCompra: new Date().toISOString(),
      tipoCompra: '',
    })
  }

  obtenerDetalles(idCompra: number) {
    this.servDetalle.obtenerComprasDetalle(idCompra)
      .subscribe(res => {
        this.detalles = res
        this.puedeFinalizar = this.detalles.length > 0 ? true : false;
        this.total = 0
        this.detalles.forEach(item => {
          this.total += (item.precio * item.cantidad)
        })
        const totalesPorImpuesto: { descripcion: string, total: number }[] = [];
        this.detalles.forEach(detalle => {
          const { insumo, precio, cantidad } = detalle;

          const contribucion = (precio * cantidad) / insumo.impuesto.valor;

          const totalExistente = totalesPorImpuesto.find(t => t.descripcion === insumo.impuesto.descripcion);

          if (totalExistente) {
            totalExistente.total += contribucion;
          } else {
            totalesPorImpuesto.push({ descripcion: insumo.impuesto.descripcion, total: contribucion });
          }
        });
        this.detallesImpuesto = totalesPorImpuesto
      }, error => {
        this.toast.toastError('Error al recuperar detalles.')
      })
  }

  editarGuardar() {
    if (this.formCompra.invalid) {
      this.toast.toastError('Complete los Campos de compra')
      return
    }

    let compra: Compra = this.formCompra.value

    if (compra.idCompra > 0) {
      this.service.editarCompra(compra)
        .subscribe(
          res => {
            this.toast.toastExitoso('Compra editada exitosamente!')
          },
          err => this.toast.toastError(err))

      return;
    }

    this.service.agregarCompra(compra)
      .subscribe(
        res => {
          //this.toast.toastExitoso('Compra editada exitosamente!')
          this.router.navigate([`../../editar-compra/${res}`], { relativeTo: this.route });
        },
        err => this.toast.toastError(err))

    this.accordion = ['a1', 'a2']
  }

  agregarDetalles() {
    if (this.formDetalle.invalid) {
      this.toast.toastError('Complete los campos en detalle')
      return
    }

    let detalle: CompraDetalle = this.formDetalle.value
    detalle.idCompra = this.formCompra.get('idCompra').value

    this.servDetalle.agregarCompraDetalle(detalle)
      .subscribe(
        res => {
          this.toast.toastExitoso('Detalle agregado exitosamente!')
          this.obtenerDetalles(this.idCompra)
        },
        err => this.toast.toastError(err))
    return;
  }

  irAtras() {
    this.formCompra.reset()
    this.router.navigate(['../../'], { relativeTo: this.route })
  }

  async editarDetalle(detalle: CompraDetalle) {
    const modal = await this.modal.create({
      component: ModalEditarCompraComponent,
      componentProps: {
        detalle: JSON.stringify(detalle)
      }
    })

    modal.onDidDismiss()
      .then(res => {
        if (res.role == 'done') {
          this.obtenerDetalles(this.idCompra);
        }
      })

    return await modal.present();
  }

  confirmarborrarDetalle(detalle: CompraDetalle) {
    this.toast.alertConfirmarAccion(`Borrar detalle`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(detalle.idCompraDetalle)
        }
      })
  }

  borrar(id: number) {
    this.servDetalle.borrarCompraDetalle(id)
      .subscribe(
        res => {
          this.toast.toastExitoso('Detalle borrado!')
          this.obtenerDetalles(this.idCompra)
        },
        error => { this.toast.toastError(error) }
      )
  }

  seleccionInsumo(ev: any) {
    let idInsumo = ev.detail.value
    if (idInsumo) {
      let impuesto = this.insumos.find(i => i.idInsumo == idInsumo).idImpuesto
      this.impuestoSelect.value = impuesto
    }
  }

  confirmarFinalizar() {
    this.toast.alertConfirmarAccion('Finalizar Compra', 'Al finalizar esta compra no podra volver a modificarla. Esta seguro de continuar?')
      .then(res => {
        if (res.role == 'done') {
          this.finalizar()
        }
      })
  }

  finalizar() {
    this.service.finalizar(this.idCompra)
      .subscribe(res => {
        this.toast.toastExitoso('Proceso finalizado!')
        this.irAtras()
      }, err => this.toast.toastError(err))
  }
}
