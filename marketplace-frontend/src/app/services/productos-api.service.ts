import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IProducto, IProducto2, IProductoAñadir, IProductoEditar } from './models/IProductos';

@Injectable({
  providedIn: 'root'
})
export class ProductosApiService {
  hhtpCliente = inject(HttpClient);
  private readonly URL = "http://localhost:5038/api/Productos/mis-productos";
  private readonly URL2 = "http://localhost:5038/api/Productos";
  private readonly URL3 = "http://localhost:5038/api/Productos/añadir";
  private readonly URL4 = "http://localhost:5038/api/Productos/editar";
  getMyProducts(){
    return this.hhtpCliente.get<IProducto[]>(this.URL);
  }
  getProducts(){
    return this.hhtpCliente.get<IProducto2[]>(this.URL2);
  }
  postProducto(producto:IProductoAñadir){
    return this.hhtpCliente.post<IProducto>(this.URL3,producto)
  }
  putProducto(producto:IProductoEditar){
    return this.hhtpCliente.put<IProducto>(this.URL4,producto)
  }

}
