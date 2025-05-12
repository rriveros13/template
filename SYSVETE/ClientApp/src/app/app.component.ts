import { Component } from '@angular/core';
import { UsuarioLogueado } from './models/Usuario';
import { AutenticacionService } from './services/autenticacion.service';
@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  public appPages = [
    { title: 'Inicio', url: '/home', icon: 'home' },
    { title: 'Definiciones', url: '/definiciones', icon: 'build' },
    { title: 'Clientes', url: '/clientes', icon: 'people' },
    { title: 'Paciente', url: '/mascotas', icon: 'fish' },
    { title: 'Servicios', url: '/servicios', icon: 'fitness' },
    { title: 'Historial', url: '/historial', icon: 'medical' },
    { title: 'Compras', url: '/compras', icon: 'cash' },
    { title: 'Ventas', url: '/ventas', icon: 'cash' },
    { title: 'Reportes', url: '/reportes', icon: 'podium' },
    { title: 'Usuarios', url: '/usuarios', icon: 'people' },
    { title: 'Administracion', url: '/administracion', icon: 'settings' },
  ];

  usuarioActual: UsuarioLogueado;

  constructor(
    private auth: AutenticacionService,
  ) {
    this.auth.usuarioActual
      .subscribe(usuario => this.usuarioActual = usuario);
  }

  cerrarSesion() {
    this.auth.cerrarSesion()
  }
}
