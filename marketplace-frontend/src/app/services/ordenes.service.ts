import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ICarritoProductos } from './models/IProductos';
import { IUsuarioCompra } from './models/IUsuarios';
import { IMisCompras, IMisVentas } from './models/IOrdenes';

@Injectable({
  providedIn: 'root'
})
export class OrdenesService {
  httpCliente = inject(HttpClient)
  private readonly URL = "http://localhost:5038/api/Ordenes/añadir";
  private readonly URL2 = "http://localhost:5038/api/Ordenes/mis-compras";
  private readonly URL3 = "http://localhost:5038/api/Ordenes/mis-ventas";

  postOrden(productosCarrito:ICarritoProductos[],personaCompra:IUsuarioCompra){
    const body = {
      productoCarrito:productosCarrito,
      persona:personaCompra
    };
    return this.httpCliente.post(this.URL,body);
  }
  getMisCompras(){
    return this.httpCliente.get<IMisCompras[]>(this.URL2);
  }
  getMisVentas(){
    return this.httpCliente.get<IMisVentas[]>(this.URL3);
  }
}
