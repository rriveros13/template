import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AutenticacionService } from '../services/autenticacion.service';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(
    private autenticacionService: AutenticacionService,
    private router: Router
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(
        catchError(err => {

          if (err.status === 401) {
            this.autenticacionService.cerrarSesion();
            this.router.navigateByUrl('/login')
          }

          if (err.status === 403) {
            this.router.navigateByUrl('/home')
          }

          const error = err.error.mensaje || err.statusText;

          return throwError(error)
        })
      )
  }

}
