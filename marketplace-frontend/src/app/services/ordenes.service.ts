import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ICarritoProductos } from './models/IProductos';
import { IUsuarioCompra } from './models/IUsuarios';

@Injectable({
  providedIn: 'root'
})
export class OrdenesService {
  httpCliente = inject(HttpClient)
  private readonly URL = "http://localhost:5038/api/Ordenes/añadir";

  postOrden(productosCarrito:ICarritoProductos[],personaCompra:IUsuarioCompra){
    const body = {
      productoCarrito:productosCarrito,
      persona:personaCompra
    };
    return this.httpCliente.post(this.URL,body);
  }
}
