import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'busqueda'
})
export class BusquedaPipe implements PipeTransform {

  transform(items: any[], busquedaTermino: string, filtroProp: string[]): unknown {
    if (!items || !busquedaTermino || !filtroProp || filtroProp.length == 0) {
      return items
    }

    busquedaTermino = busquedaTermino.toLowerCase();

    return items.filter(item => {
      return filtroProp.some(prop => {
        const valor = item[prop]
        if (valor && typeof valor === 'string') {
          return valor.toLowerCase().includes(busquedaTermino)
        }
        return false
      })
    })
  }

}
