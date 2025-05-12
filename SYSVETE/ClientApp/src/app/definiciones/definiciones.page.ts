import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-definiciones',
  templateUrl: './definiciones.page.html',
  styleUrls: ['./definiciones.page.scss'],
})
export class DefinicionesPage implements OnInit {

  tituloToolbar: string = 'Definiciones'

  menuOpciones = [
    {
      titulo: 'Especie',
      descripcion: 'ABM Especie',
      ruta: './especies'
    },
    {
      titulo: 'Razas',
      descripcion: 'ABM Razas',
      ruta: './razas'
    },
    {
      titulo: 'Tipo de Insumos',
      descripcion: 'ABM Tipo de Insumos',
      ruta: './tipo-insumo'
    },
    {
      titulo: 'Impuestos',
      descripcion: 'ABM Inpuestos',
      ruta: './impuesto'
    },
    {
      titulo: 'Unidad de Medida',
      descripcion: 'ABM Unidades de Medida',
      ruta: './unidad-medida'
    },
    {
      titulo: 'Presentaciones',
      descripcion: 'ABM Presentaciones',
      ruta: './presentacion'
    },
    {
      titulo: 'Patologias',
      descripcion: 'ABM Patologias',
      ruta: './patologias'
    },
    {
      titulo: 'Vacunas',
      descripcion: 'ABM Vacunas',
      ruta: './vacunas'
    },
    {
      titulo: 'Insumos',
      descripcion: 'ABM Insumos',
      ruta: './insumos'
    },
  ]

  constructor() { }

  ngOnInit() {
  }

}
