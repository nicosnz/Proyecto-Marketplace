import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ICarritoProductos } from './models/productos/ICarritoProductos';
import { IVentasUsuario } from './models/ordenes/IVentasUsuario';
import { environment } from '../../environments/environment';
import { IUsuarioCompra } from './models/usuarios/IUsuarioCompra';
import { IComprasUsuario } from './models/ordenes/IComprasUsuario';

@Injectable({
  providedIn: 'root'
})
export class OrdenesServiceApiService {
  httpCliente = inject(HttpClient)
  private readonly URL_AÑADIR_ORDEN = `${environment.urlApi}/Ordenes/añadir`;
  private readonly URL_OBTENER_COMPRAS_USUARIO = `${environment.urlApi}/Ordenes/mis-compras`;
  private readonly URL_OBTENER_VENTAS_USUARIO = `${environment.urlApi}/Ordenes/mis-ventas`;

  postOrden(productosCarrito:ICarritoProductos[],personaCompra:IUsuarioCompra){
    const body = {
      productoCarrito:productosCarrito,
      persona:personaCompra
    };
    return this.httpCliente.post(this.URL_AÑADIR_ORDEN,body);
  }
  getMisCompras(){
    return this.httpCliente.get<IComprasUsuario[]>(this.URL_OBTENER_COMPRAS_USUARIO);
  }
  getMisVentas(){
    return this.httpCliente.get<IVentasUsuario[]>(this.URL_OBTENER_VENTAS_USUARIO);
  }
}
