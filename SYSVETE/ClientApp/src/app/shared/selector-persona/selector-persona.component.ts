import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { PersonasService } from '../../services/personas.service';
import { Persona } from '../../models/Persona';
import { ToastService } from '../../helpers/toast.service';
import { ModalController } from '@ionic/angular';
import { ModalPersonaComponent } from '../modal-persona/modal-persona.component';

@Component({
  selector: 'app-selector-persona',
  templateUrl: './selector-persona.component.html',
  styleUrls: ['./selector-persona.component.scss'],
})
export class SelectorPersonaComponent implements OnInit, OnChanges {

  @Input() selectedPersona: number
  @Output() evSeleccion: EventEmitter<number> = new EventEmitter<number>();

  personas: Persona[] = []
  constructor(
    private service: PersonasService,
    private toast: ToastService,
    private modal: ModalController
  ) { }

  ngOnChanges(changes: SimpleChanges): void {
   
  }

  ngOnInit() {
    this.service.obtenerPersonas()
      .subscribe(res => this.personas = res,
        error => {
          this.toast.toastError(`${error}`)
        })
  }

  seleccionarPersona(ev: any) {
    let idPersona = ev.detail.value
    this.evSeleccion.emit(idPersona)
  }

  async modalPersona() {
    const modal = await this.modal.create({
      component: ModalPersonaComponent
    })

    modal.onDidDismiss()
      .then(res => {
        if (res.role == 'done') {
          this.service.obtenerPersonas()
            .subscribe(res => this.personas = res,
              error => {
                this.toast.toastError(`${error}`)
              })
        }
      })

    return await modal.present()
  }
}
