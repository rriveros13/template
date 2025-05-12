import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class CustomValidatorsService {

  constructor() { }

  static esPositivo(control: AbstractControl): ValidationErrors | null {
    const value = control.value;
    if (value < 0) {
      return { numeroNegativo: true };
    }
    return null;
  }

  static mayorCero(control: AbstractControl): ValidationErrors | null {
    const value = control.value
    if (value <= 0) {
      return { menorIgualCero: true }
    }
    return
  }
}
