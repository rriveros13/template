import { Injectable } from '@angular/core';
import { AlertController, LoadingController, ToastController } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  private millisegundos: number = 3000
  private posicion: 'bottom'


  constructor(
    private toastController: ToastController,
    private loadignController: LoadingController,
    private alertController: AlertController,
  ) { }

  /**
   * Mensaje de informacion para el usuario
   * @param msg
   */
  async toastInfo(msg: string) {
    const toast = await this.toastController.create({
      message: msg,
      duration: this.millisegundos,
      position: this.posicion,
      buttons: [
        {
          role: 'cancel'
        }
      ]
    })

    return await toast.present()
  }

  /**
   * Mensaje exitoso para el usuario
   * @param msg
   */
  async toastExitoso(msg: string) {
    const toast = await this.toastController.create({
      message: msg,
      duration: this.millisegundos,
      position: this.posicion,
      color: 'success',
      buttons: [
        {
          role: 'cancel',
          icon: 'close'
        }
      ]
    })

    return await toast.present()
  }

  /**
   * Mensaje de error para el usuario
   * @param msg
   */
  async toastError(msg: string) {
    const toast = await this.toastController.create({
      message: msg,
      duration: this.millisegundos,
      position: this.posicion,
      color: 'danger',
      buttons: [
        {
          role: 'cancel',
          icon: 'close'
        }
      ]
    })

    return await toast.present()
  }

  /**
   * Retorna constructor del modal de espera, para levantar usar present(), para cerrar dismiss()
   * @param msg
   */
  modalDeEspera(msg?: string) {

    return this.loadignController.create({
      message: msg ? msg : 'Cargando...'
    })
  }
  /**
   * Mensaje de alerta para confirmar accion, retorna el rol 'done' si acepto o 'cancel' si cancelo
   * @param titulo
   * @param msg
   */
  async alertConfirmarAccion(titulo: string, msg?: string) {
    const alert = await this.alertController.create({
      header: titulo,
      message: msg ? msg : 'Desea continuar?',
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel'
        },
        {
          text: 'Aceptar',
          role: 'done'
        }
      ]
    })

    await alert.present();

    return await alert.onDidDismiss();
  }
}
