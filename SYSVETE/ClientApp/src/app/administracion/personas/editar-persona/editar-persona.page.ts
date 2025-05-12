import { Component, OnInit, ViewChild } from '@angular/core';
import { PersonaComponent } from '../../../components/persona/persona.component';
import { Persona } from '../../../models/Persona';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonasService } from '../../../services/personas.service';
import { ToastService } from '../../../helpers/toast.service';

@Component({
  selector: 'app-editar-persona',
  templateUrl: './editar-persona.page.html',
  styleUrls: ['./editar-persona.page.scss'],
})
export class EditarPersonaPage implements OnInit {

  @ViewChild(PersonaComponent) personaComponent: PersonaComponent;

  tituloToolbar: string = 'Persona'
  persona: Persona

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: PersonasService,
    private toast: ToastService
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter() {
    const param = this.route.snapshot.paramMap.get('idPersona')
    var id = +param;

    if (id > 0) {
      this.service.obtenerPersonasPorId(id)
        .subscribe(res => {
          this.tituloToolbar = `Persona ${res.nombre}`
          this.persona = res
        }, error => {
          this.toast.toastError('Error al recuperar Persona.')
          console.log(error)
        })
    }
  }

  obtenerPersona(): Persona {
    return this.personaComponent.retornarPersona();
  }

  editarGuardar() {
    let persona = this.obtenerPersona()
    if (persona.idPersona > 0) {
      return
    }

    this.service.agregarPersona(persona)
      .subscribe(
        res => {
          this.toast.toastExitoso('Persona creada exitosamente!')
          this.irAtras()
        },
        err => this.toast.toastError(err))
  }

  irAtras() {
    this.router.navigate(['../../'], { relativeTo: this.route })
  }

  cancelar() {
    this.irAtras()
  }
}
