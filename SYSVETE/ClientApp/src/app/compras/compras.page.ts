import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-compras',
  templateUrl: './compras.page.html',
  styleUrls: ['./compras.page.scss'],
})
export class ComprasPage implements OnInit {
  tituloToolbar: string = 'Modulo Compras'

  menuOpciones = [
    {
      titulo: 'Proveedores',
      descripcion: 'ABM proveedores',
      ruta: './proveedores'
    },
    {
      titulo: 'Facturas Compra',
      descripcion: 'ABM Compras',
      ruta: './factura-compras'
    },
    {
      titulo: 'Stock de insumos',
      descripcion: 'Stock de insumos',
      ruta: './stock'
    }
  ]
  constructor() { }

  ngOnInit() {
  }

}
