import { Component, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { ToastService } from '../../helpers/toast.service';
import { Persona } from '../../models/Persona';

@Component({
  selector: 'app-persona',
  templateUrl: './persona.component.html',
  styleUrls: ['./persona.component.scss'],
})
export class PersonaComponent implements OnInit, OnChanges {

  @Input() persona: Persona;
  @Input() edicion: boolean = false;

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private toast: ToastService,
  ) { }


  ngOnChanges(changes: SimpleChanges): void {
    if (changes['persona']) {
      this.persona = changes['persona'].currentValue;
      this.setPersona()
    }
  }

  ngOnInit() {
    this.form = this.fb.group({
      idPersona: [0, Validators.required],
      nombre: ['', Validators.required],
      apellido: ['', Validators.required],
      cedula: ['', Validators.required],
      fechaNacimiento: [new Date().toISOString(), Validators.required]
    })

    this.initForm();

    this.setPersona
  }

  private initForm() {
    this.form.patchValue({
      idPersona: 0,
      nombre: '',
      apellido: '',
      cedula: '',
      fechaNacimiento: new Date().toISOString()
    })
  }

  retornarPersona(): Persona {

    if (this.form.invalid) {
      this.toast.toastError('Complete los campos!')
      return
    }

    let model: Persona = this.form.value
    return model;
  }

  private setPersona() {
    if (this.persona) {
      this.form.patchValue({
        idPersona: this.persona.idPersona,
        nombre: this.persona.nombre,
        apellido: this.persona.apellido,
        cedula: this.persona.cedula,
        fechaNacimiento: new Date(this.persona.fechaNacimiento).toISOString()
      })
    }
  }
}
