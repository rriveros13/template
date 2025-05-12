import { Component, OnInit } from '@angular/core';
import { InformesService } from '../services/informes.service';
import { ModalController } from '@ionic/angular';
import { DesdeHastaComponent } from '../shared/desde-hasta/desde-hasta.component';
import { ModalProveedorComponent } from '../components/modal-proveedor/modal-proveedor.component';
import { ModalPatologiasComponent } from '../components/modal-patologias/modal-patologias.component';

@Component({
  selector: 'app-reportes',
  templateUrl: './reportes.page.html',
  styleUrls: ['./reportes.page.scss'],
})
export class ReportesPage implements OnInit {

  tituloToolbar: string = 'Reportes'

  reportes = [{
    titulo: 'Reporte servicios',
    descripcion: 'Reporte de servicios realizados',
    funcion: async () => {
      this.verDesdeHasta()
    },
  },
  {
    titulo: 'Ventas no facturadas',
    descripcion: 'Reporte de ventas no facturadas por cliente',
    funcion: async () => {
      this.informe.ventasNoFacturadas()
        .subscribe(res => this.informe.abrirPdf(res.body, 'Ventas_no_facturadas.pdf'))
    },
  },
  {
    titulo: 'Estado de proveedores',
    descripcion: 'Estado de proveedores',
    funcion: async () => {
      this.informe.deudaProveedor()
        .subscribe(res => this.informe.abrirPdf(res.body, 'Estado_de_proveedores.pdf'))
    },
  },
  {
    titulo: 'Compras por proveedor',
    descripcion: 'Compras por proveedor',
    funcion: async () => {
      this.verProveedores()
    },
  },
  {
    titulo: 'Historial de Patologias',
    descripcion: 'Historial de Patologias',
    funcion: async () => {
      this.verPatologias()
    },
  },
  ]

  constructor(
    private informe: InformesService,
    private modal: ModalController,
  ) { }

  ngOnInit() {
  }

  async verDesdeHasta() {
    const modal = await this.modal.create({
      component: DesdeHastaComponent
    })

    modal.onDidDismiss()
      .then(res => {
        if (res.role == 'done') {
          this.informe.serviciosRealizados(res.data.desde, res.data.hasta)
            .subscribe(res => this.informe.abrirPdf(res.body, 'servicio_realizados.pdf'))
        }
      })

    return await modal.present();
  }

  async verProveedores() {
    const modal = await this.modal.create({
      component: ModalProveedorComponent
    })

    modal.onDidDismiss()
      .then(res => {
        if (res.role == 'done') {
          this.informe.obtenerComprasPorProveedor(res.data.idProveedor)
            .subscribe(res => this.informe.abrirPdf(res.body, 'obtenerComprasPorProveedor.pdf'))
        }
      })

    return await modal.present();
  }

  async verPatologias() {
    const modal = await this.modal.create({
      component: ModalPatologiasComponent
    })

    modal.onDidDismiss()
      .then(res => {
        if (res.role == 'done') {
          this.informe.ObtenerHistorialClinicoPorPatologia(res.data.idPatologia)
            .subscribe(res => this.informe.abrirPdf(res.body, 'reporte.pdf'))
        }
      })

    return await modal.present();
  }

  handleClick(index: number): void {
    this.reportes[index].funcion();
  }
}
