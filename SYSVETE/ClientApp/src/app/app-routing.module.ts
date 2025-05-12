import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './helpers/auth.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    loadChildren: () => import('./home/home.module').then(m => m.HomePageModule)
  },
  {
    path: 'administracion',
    loadChildren: () => import('./administracion/administracion.module').then(m => m.AdministracionPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'usuarios',
    loadChildren: () => import('./usuarios/usuarios.module').then(m => m.UsuariosPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'login',
    loadChildren: () => import('./login/login.module').then(m => m.LoginPageModule),
  },
  {
    path: 'definiciones',
    loadChildren: () => import('./definiciones/definiciones.module').then(m => m.DefinicionesPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'clientes',
    loadChildren: () => import('./clientes/clientes.module').then(m => m.ClientesPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'mascotas',
    loadChildren: () => import('./mascotas/mascotas.module').then(m => m.MascotasPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'compras',
    loadChildren: () => import('./compras/compras.module').then( m => m.ComprasPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'servicios',
    loadChildren: () => import('./servicios/servicios.module').then(m => m.ServiciosPageModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'historial',
    loadChildren: () => import('./historial/historial.module').then( m => m.HistorialPageModule)
  },
  {
    path: 'ventas',
    loadChildren: () => import('./ventas/ventas.module').then( m => m.VentasPageModule)
  },
  {
    path: 'reportes',
    loadChildren: () => import('./reportes/reportes.module').then( m => m.ReportesPageModule)
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
