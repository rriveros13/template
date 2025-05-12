import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../helpers/auth.guard';

import { DefinicionesPage } from './definiciones.page';

const routes: Routes = [
  {
    path: '',
    component: DefinicionesPage
  },
  {
    path: 'razas',
    loadChildren: () => import('./razas/razas.module').then(m => m.RazasPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'especies',
    loadChildren: () => import('./especies/especies.module').then( m => m.EspeciesPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'impuesto',
    loadChildren: () => import('./impuesto/impuesto.module').then( m => m.ImpuestoPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'presentacion',
    loadChildren: () => import('./presentacion/presentacion.module').then( m => m.PresentacionPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'tipo-insumo',
    loadChildren: () => import('./tipo-insumo/tipo-insumo.module').then( m => m.TipoInsumoPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'unidad-medida',
    loadChildren: () => import('./unidad-medida/unidad-medida.module').then( m => m.UnidadMedidaPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'insumos',
    loadChildren: () => import('./insumos/insumos.module').then( m => m.InsumosPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'patologias',
    loadChildren: () => import('./patologias/patologias.module').then( m => m.PatologiasPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'vacunas',
    loadChildren: () => import('./vacunas/vacunas.module').then( m => m.VacunasPageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DefinicionesPageRoutingModule {}
