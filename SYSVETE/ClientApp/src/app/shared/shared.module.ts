import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { CardListComponent } from './card-list/card-list.component';
import { RouterModule } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { BusquedaPipe } from './pipes/busqueda.pipe';
import { SelectorPersonaComponent } from './selector-persona/selector-persona.component';
import { ModalPersonaComponent } from './modal-persona/modal-persona.component';
import { PersonaComponent } from '../components/persona/persona.component';
import { ReactiveFormsModule } from '@angular/forms';




@NgModule({
  declarations: [
    ToolbarComponent,
    CardListComponent,
    BusquedaPipe,
    PersonaComponent,
    SelectorPersonaComponent,
    ModalPersonaComponent,
  ],
  imports: [
    IonicModule.forRoot(),
    CommonModule,
    RouterModule,
    ReactiveFormsModule
  ],
  exports: [
    ToolbarComponent,
    CardListComponent,
    BusquedaPipe,
    PersonaComponent,
    SelectorPersonaComponent,
    ModalPersonaComponent,
  ]
})
export class SharedModule { }
