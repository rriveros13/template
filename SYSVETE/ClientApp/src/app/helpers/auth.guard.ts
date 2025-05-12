import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AutenticacionService } from '../services/autenticacion.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,
    private autenticacionService: AutenticacionService
  ) {

  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    const usuarioActual = this.autenticacionService.getUsuarioActual;
    if (usuarioActual) {
      return true;
    }

    this.router.navigate(['/login'])
    return false;
  }
  
}
