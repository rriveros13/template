import { Component, OnInit } from '@angular/core';
import { PersonasService } from '../../services/personas.service';
import { Persona } from '../../models/Persona';
import { ToastService } from '../../helpers/toast.service';

@Component({
  selector: 'app-personas',
  templateUrl: './personas.page.html',
  styleUrls: ['./personas.page.scss'],
})
export class PersonasPage implements OnInit {

  personas: Persona[] = []
  tituloToolbar: string = 'Personas'

  constructor(
    private service: PersonasService,
    private toast: ToastService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.obtenerPersonas();
  }

  obtenerPersonas() {
    this.service.obtenerPersonas()
      .subscribe(res => this.personas = res,
        error => {
          this.toast.toastError(`${error}`)
        })
  }
}
