import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AutenticacionService } from '../services/autenticacion.service';
import { environment } from '../../environments/environment';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(
    private autenticacionService: AutenticacionService
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const usuarioActual = this.autenticacionService.getUsuarioActual;
    const logeado = usuarioActual && usuarioActual.token; //Si usuario actual no es nulo y tiene un token, entonces esta logeado
    const esApiUrl = req.url.startsWith(environment.apiUrl);

    if (logeado && esApiUrl) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${usuarioActual.token}`
        }
      })
    }

    return next.handle(req);
  }

}
