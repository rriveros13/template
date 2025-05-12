import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-servicios',
  templateUrl: './servicios.page.html',
  styleUrls: ['./servicios.page.scss'],
})
export class ServiciosPage implements OnInit {

  tituloToolbar: string = 'Servicios'

  menuOpciones = [
    {
      titulo: 'Tratamientos',
      descripcion: 'ABM Tratamientos',
      ruta: './tratamientos'
    },
    {
      titulo: 'Procedimientos',
      descripcion: 'ABM Procedimientos',
      ruta: './procedimientos'
    },
  ]

  constructor() { }

  ngOnInit() {
  }

}
