import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastService } from '../helpers/toast.service';
import { AutenticacionService } from '../services/autenticacion.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  loginForm: FormGroup

  constructor(
    private fb: FormBuilder,
    private auth: AutenticacionService,
    private router: Router,
    private toast: ToastService,
  ) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      usuario: ['', Validators.required],
      contrasena: ['', Validators.required]
    })
  }

  ionViewWillEnter() {

  }

  /**
   *Retorna los datos del form
   */
  get datosLogin() {
    return this.loginForm.controls;
  }

  async iniciarSesion() {
    if (this.loginForm.invalid) {
      return
    }

    const loader = await this.toast.modalDeEspera('Iniciando sesion...')

    loader.present()

    this.auth.iniciarSesion(this.datosLogin.usuario.value, this.datosLogin.contrasena.value)
      .subscribe(
        res => {
          loader.dismiss()
          this.router.navigate(['/home'])
          this.toast.toastExitoso('Inicio Correcto')
        },
        error => {
          this.toast.toastError(`Inicio Incorrecto: ${error}`)
          loader.dismiss()
          console.log(error)
        }

      )
  }
}
