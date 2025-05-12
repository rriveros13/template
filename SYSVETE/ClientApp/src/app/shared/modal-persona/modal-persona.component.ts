import { Component, OnInit, ViewChild } from '@angular/core';
import { PersonaComponent } from '../../components/persona/persona.component';
import { PersonasService } from '../../services/personas.service';
import { Persona } from '../../models/Persona';
import { ToastService } from '../../helpers/toast.service';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-modal-persona',
  templateUrl: './modal-persona.component.html',
  styleUrls: ['./modal-persona.component.scss'],
})
export class ModalPersonaComponent implements OnInit {

  @ViewChild(PersonaComponent) personaComponent: PersonaComponent;

  constructor(
    private service: PersonasService,
    private toast: ToastService,
    private modal: ModalController
  ) { }

  ngOnInit() {

  }

  private obtenerPersona(): Persona {
    return this.personaComponent.retornarPersona();
  }

  crear() {
    let persona = this.obtenerPersona();
    this.service.agregarPersona(persona)
      .subscribe(
        res => {
          this.toast.toastExitoso('Persona creada exitosamente!')
          this.dismiss('done')
        },
        err => this.toast.toastError(err))
  }

  cancelar() {
    this.dismiss('cancel')
  }

  private dismiss(role: string, data?: any) {
    this.modal.dismiss(data, role)
  }
}
