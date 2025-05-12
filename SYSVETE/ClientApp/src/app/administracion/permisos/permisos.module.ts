import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { PermisosPageRoutingModule } from './permisos-routing.module';

import { PermisosPage } from './permisos.page';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    PermisosPageRoutingModule,
    SharedModule,
  ],
  declarations: [PermisosPage]
})
export class PermisosPageModule { }
