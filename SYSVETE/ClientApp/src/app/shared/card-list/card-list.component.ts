import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.scss'],
})
export class CardListComponent implements OnInit {

  @Input() menuOpciones: menuOpcion[] = []

  constructor() { }

  ngOnInit() {}

}

interface menuOpcion {
  titulo: string
  descripcion: string,
  ruta: string
}
