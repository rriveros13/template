import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InformesService {

  private url: string = `${environment.apiUrl}Reportes`

  constructor(
    private http: HttpClient
  ) { }

  serviciosRealizados(fechaInicio: string, fechaFin: string, tipoArchivo: string = 'application/pdf') {
    let tipo = ''
    if (tipoArchivo == 'application/pdf') {
      tipo = 'pdf'
    }
    if (tipoArchivo == 'application/vnd.ms-excel') {
      tipo = 'excel'
    }

    return this.http.post(`${this.url}/servicios`,
      { fechaInicio: fechaInicio, fechaFin: fechaFin },
      { observe: 'response', responseType: 'blob' })
  }

  ventasNoFacturadas(tipoArchivo: string = 'application/pdf') {
    let tipo = ''
    if (tipoArchivo == 'application/pdf') {
      tipo = 'pdf'
    }
    if (tipoArchivo == 'application/vnd.ms-excel') {
      tipo = 'excel'
    }

    return this.http.post(`${this.url}/ventasNoFacturadas`,
      null,
      { observe: 'response', responseType: 'blob' })
  }

  deudaProveedor(tipoArchivo: string = 'application/pdf') {
    let tipo = ''
    if (tipoArchivo == 'application/pdf') {
      tipo = 'pdf'
    }
    if (tipoArchivo == 'application/vnd.ms-excel') {
      tipo = 'excel'
    }

    return this.http.post(`${this.url}/deudaProveedor`,
      null,
      { observe: 'response', responseType: 'blob' })
  }

  obtenerComprasPorProveedor(idProveedor: number, tipoArchivo: string = 'application/pdf') {
    let tipo = ''
    if (tipoArchivo == 'application/pdf') {
      tipo = 'pdf'
    }
    if (tipoArchivo == 'application/vnd.ms-excel') {
      tipo = 'excel'
    }

    return this.http.post(`${this.url}/ObtenerComprasPorProveedor?idProveedor=${idProveedor}`,
      null,
      { observe: 'response', responseType: 'blob' })
  }

  ObtenerHistorialClinicoPorPatologia(idPatologia: number, tipoArchivo: string = 'application/pdf') {
    let tipo = ''
    if (tipoArchivo == 'application/pdf') {
      tipo = 'pdf'
    }
    if (tipoArchivo == 'application/vnd.ms-excel') {
      tipo = 'excel'
    }

    return this.http.post(`${this.url}/ObtenerHistorialClinicoPorPatologia?idPatologia=${idPatologia}`,
      null,
      { observe: 'response', responseType: 'blob' })
  }

  abrirPdf(blob: Blob, nombreArchivo: string) {
    let dataType = blob.type;
    let binaryData = [];
    binaryData.push(blob);

    let archivo = new Blob(binaryData, { type: dataType })

    let anchor = document.createElement("a");
    document.body.appendChild(anchor);

    let objectUrl = window.URL.createObjectURL(archivo);

    anchor.href = objectUrl;
    anchor.target = '_blank';
    anchor.download = nombreArchivo;
    anchor.click();

    window.URL.revokeObjectURL(objectUrl);
    return;
  }
}
