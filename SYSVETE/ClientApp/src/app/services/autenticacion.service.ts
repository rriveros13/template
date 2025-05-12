import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';
import { UsuarioLogueado } from '../models/Usuario';

@Injectable({
  providedIn: 'root'
})
export class AutenticacionService {

  private url: string = `${environment.apiUrl}Usuario/autenticar`
  private usuarioActualSubject: BehaviorSubject<UsuarioLogueado>

  public usuarioActual: Observable<UsuarioLogueado>;

  //Key para identificar el usuario logeado en el local storage
  private usuarioKey: string = 'usuario-sysvete';

  constructor(private http: HttpClient) {
    //Se inicia el estado si es que ya hay una sesion iniciada
    this.usuarioActualSubject = new BehaviorSubject<UsuarioLogueado>(JSON.parse(localStorage.getItem(this.usuarioKey)))
    this.usuarioActual = this.usuarioActualSubject.asObservable();
  }
  /**
  *Retorna el usuario actual
  */
  public get getUsuarioActual() {
    return this.usuarioActualSubject.value;
  }

  iniciarSesion(usuario: string, contrasena: string) {
    return this.http.post<any>(this.url, { usuario, contrasena })
      .pipe(map(usuario => {
        if (usuario) {
          localStorage.setItem(this.usuarioKey, JSON.stringify(usuario))
          this.usuarioActualSubject.next(usuario);
          return usuario;
        }
      }))
  }

  cerrarSesion() {
    localStorage.removeItem(this.usuarioKey);
    this.usuarioActualSubject.next(null);
  }
}
