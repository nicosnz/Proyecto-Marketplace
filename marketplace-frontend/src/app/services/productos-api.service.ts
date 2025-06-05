import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IProducto, IProducto2, IProductoAñadir } from './models/IProductos';

@Injectable({
  providedIn: 'root'
})
export class ProductosApiService {
  hhtpCliente = inject(HttpClient);
  private readonly URL = "http://localhost:5038/api/Productos/mis-productos";
  private readonly URL2 = "http://localhost:5038/api/Productos";
  private readonly URL3 = "http://localhost:5038/api/Productos/añadir";
  getMyProducts(){
    return this.hhtpCliente.get<IProducto[]>(this.URL);
  }
  getProducts(){
    return this.hhtpCliente.get<IProducto2[]>(this.URL2);
  }
  postProducto(producto:IProductoAñadir){
    return this.hhtpCliente.post<IProducto>(this.URL3,producto)
  }

}
