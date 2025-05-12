import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-administracion',
  templateUrl: './administracion.page.html',
  styleUrls: ['./administracion.page.scss'],
})
export class AdministracionPage implements OnInit {

  public tituloToolbar: string = 'Administracion'

  menuOpciones = [{
    titulo: 'Roles',
    descripcion: 'ABM Roles',
    ruta: './roles'
  },
  {
    titulo: 'Permisos',
    descripcion: 'ABM Permisos',
    ruta: './permisos'
  },
  {
    titulo: 'Personas',
    descripcion: 'ABM Personas',
    ruta: './personas'
  }
  ]

  constructor() { }

  ngOnInit() {
  }

}
