import { Component, OnInit, ViewChild } from '@angular/core';
import { Cliente } from '../../models/Cliente';
import { Insumo } from '../../models/Insumo';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProveedoresService } from '../../services/proveedores.service';
import { ClientesService } from '../../services/clientes.service';
import { InsumoService } from '../../services/insumo.service';
import { ToastService } from '../../helpers/toast.service';
import { VentasService } from '../../services/ventas.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IonSelect, ModalController } from '@ionic/angular';
import { ImpuestoService } from '../../services/impuesto.service';
import { Impuesto } from '../../models/Impuesto';
import { Venta } from '../../models/Venta';
import { VentaDetalleService } from '../../services/venta-detalle.service';
import { VentaDetalle } from '../../models/VentaDetalle';
import { registerLocaleData } from '@angular/common';
import es from '@angular/common/locales/es';
import { HistorialService } from '../../services/historial.service';


@Component({
  selector: 'app-editar-venta',
  templateUrl: './editar-venta.page.html',
  styleUrls: ['./editar-venta.page.scss'],
})
export class EditarVentaPage implements OnInit {

  @ViewChild('impuestoSelect', { static: false }) impuestoSelect: IonSelect

  tituloToolbar: string = 'Venta'
  cliente: Cliente[] = [];
  insumos: Insumo[] = [];
  impuestos: Impuesto[] = [];
  detalles: VentaDetalle[] = [];

  formVenta: FormGroup;
  formDetalle: FormGroup;

  accordion: string[] = ['a1']
  multiple: boolean = true;

  edicion: boolean = false;
  total: number = 0;

  idVenta: number = 0;

  finalizado: boolean = false

  puedeFinalizar: boolean = false

  constructor(
    private clienteService: ClientesService,
    private insumoService: InsumoService,
    private impuestoService: ImpuestoService,
    private toast: ToastService,
    private fb: FormBuilder,
    private service: VentasService,
    private route: ActivatedRoute,
    private router: Router,
    private servDetalle: VentaDetalleService,
    private servHis: HistorialService,
  ) { }

  ngOnInit() {
    registerLocaleData(es)

    this.formVenta = this.fb.group({
      idVenta: [0, Validators.required],
      nroBoleta: [0, Validators.required],
      idCliente: [0, Validators.required],
      fechaVenta: [new Date().toISOString(), Validators.required],
    })

    this.formDetalle = this.fb.group({
      idVentaDetalle: [0, Validators.required],
      idVenta: [0, Validators.required],
      idInsumo: [0, Validators.required],
      descripcion: ['', Validators.required],
      precio: [0, Validators.required],
      cantidad: [0, Validators.required],
    })
  }

  ionViewWillEnter() {
    this.obtenerDatos()

    const param = this.route.snapshot.paramMap.get('idVenta')
    this.idVenta = +param;
    this.finalizado = false
    if (this.idVenta > 0) {
      this.finalizado = true
      this.service.obtenerVentaPorId(this.idVenta)
        .subscribe(res => {
          this.tituloToolbar = `Venta Nro ${res.idVenta}`
          this.formVenta.patchValue({
            idVenta: res.idVenta,
            idCliente: res.idCliente,
            nroBoleta: res.nroBoleta,
            fechaVenta: new Date(res.fechaVenta).toISOString(),
          })
          this.accordion = ['a1', 'a2']
          this.edicion = true;
          this.finalizado = res.finalizado;
          this.obtenerDetalles(res.idVenta)
        }, error => {
          this.toast.toastError('Error al recuperar venta.')
        })
    }
  }

  private obtenerDatos() {
    this.clienteService.obtenerClientes()
      .subscribe(res => {
        this.cliente = res
      }, error => {
        this.toast.toastError(`Error al obtener clientes: ${error}`)
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

  agregarDetalles() {
    if (this.formDetalle.invalid) {
      this.toast.toastError('Complete los campos en detalle')
      return
    }

    let dto: VentaDetalle = this.formDetalle.value

    dto.idVenta = this.idVenta

    this.servDetalle.agregarVentasDetalle(dto)
      .subscribe(
        res => {
          this.toast.toastExitoso('Detalle agregado exitosamente!')
          this.obtenerDetalles(this.idVenta)
        },
        err => this.toast.toastError(err))
    return;
  }

  editarGuardar() {
    if (this.formVenta.invalid) {
      this.toast.toastError('Complete los Campos de venta')
      return
    }

    let venta: Venta = this.formVenta.value

    if (venta.idVenta > 0) {
      this.service.editar(venta)
        .subscribe(
          res => {
            this.toast.toastExitoso('Venta creada exitosamente!')
          },
          err => this.toast.toastError(err))

      return;
    }

    this.service.agregar(venta)
      .subscribe(
        res => {
          this.toast.toastExitoso('Venta editada exitosamente!')
          this.router.navigate([`../../editar-venta/${res}`], { relativeTo: this.route });
        },
        err => this.toast.toastError(err))
  }

  obtenerDetalles(id) {
    this.servDetalle.obtenerVentasDetalle(id)
      .subscribe(res => {
        this.detalles = res
        this.total = 0
        this.detalles.forEach(item => {
          this.total += (item.cantidad * item.precio)
        })

        this.puedeFinalizar = this.detalles.length > 0 ? true : false;

        const totalesPorImpuesto: { descripcion: string, total: number }[] = [];
        //this.detalles.forEach(detalle => {
        //  const { insumo, precio, cantidad } = detalle;

        //  const contribucion = (precio * cantidad) / insumo.impuesto.valor;

        //  const totalExistente = totalesPorImpuesto.find(t => t.descripcion === insumo.impuesto.descripcion);

        //  if (totalExistente) {
        //    totalExistente.total += contribucion;
        //  } else {
        //    totalesPorImpuesto.push({ descripcion: insumo.impuesto.descripcion, total: contribucion });
        //  }
        //});

      }, error => {
        this.toast.toastError('Error al recuperar detalles.')
      })
  }

  confirmarborrarDetalle(detalle: VentaDetalle) {
    this.toast.alertConfirmarAccion(`Borrar detalle`)
      .then(res => {
        if (res.role == 'done') {
          this.borrar(detalle.idVentaDetalle)
        }
      })
  }

  borrar(id: number) {
    this.servDetalle.borrarVentasDetalle(id)
      .subscribe(
        res => {
          this.toast.toastExitoso('Detalle borrado!')
          this.obtenerDetalles(this.idVenta)
        },
        error => { this.toast.toastError(error) }
      )
  }

  irAtras() {
    this.formVenta.reset()
    this.router.navigate(['../../'], { relativeTo: this.route })
  }

  seleccionInsumo(ev: any) {
    let idInsumo = ev.detail.value
    if (idInsumo) {
      let impuesto = this.insumos.find(i => i.idInsumo == idInsumo).idImpuesto
      this.impuestoSelect.value = impuesto
    }
  }

  facturarServicis() {

    if (this.formVenta.invalid) {
      this.toast.toastError('Complete los Campos de venta')
      return
    }

    let venta: Venta = this.formVenta.value
    this.servHis.facturarServicios(venta.idCliente, venta.idVenta)
      .subscribe(
        res => {
          this.toast.toastExitoso('Proceso terminado!')
          this.obtenerDetalles(venta.idVenta);
        },
        err => this.toast.toastError(err))
  }

  confirmarFinalizar() {
    this.toast.alertConfirmarAccion('Finalizar Venta', 'Al finalizar esta venta no podra volver a modificarla. Esta seguro de continuar?')
      .then(res => {
        if (res.role == 'done') {
          this.finalizar()
        }
      })
  }

  finalizar() {
    this.service.finalizar(this.idVenta)
      .subscribe(res => {
        this.toast.toastExitoso('Proceso finalizado!')
        this.irAtras()
      }, err => this.toast.toastError(err))
  }
}
