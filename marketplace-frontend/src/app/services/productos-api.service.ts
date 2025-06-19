import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IProducto, IProducto2, IProductoEditar } from './models/IProductos';
import { ICategoria } from './models/ICategoria';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductosApiService {
  httpCliente = inject(HttpClient);
  private readonly URL = "http://localhost:5038/api/Productos/mis-productos";
  private readonly URL2 = "http://localhost:5038/api/Productos";
  private readonly URL3 = "http://localhost:5038/api/Productos/añadir";
  private readonly URL4 = "http://localhost:5038/api/Productos/editar";
  private readonly URL5 = "http://localhost:5038/api/Productos/eliminar";
  private readonly URL6 = "http://localhost:5038/api/Productos/producto";
  private readonly URL7 = "http://localhost:5038/api/Productos/catalogo";
  private readonly URL8 = "http://localhost:5038/api/Productos/categorias";
  private readonly URL9 = "http://localhost:5038/api/Productos/categoria";
  getMyProducts():Observable<IProducto2[]>{
    return this.httpCliente.get<IProducto2[]>(this.URL);
  }
  getProduct(idProducto:number){
    return this.httpCliente.get<IProducto>(`${this.URL6}/${idProducto}`);

  }
  getProductosPorCategoria(idCategoria:number){
    return this.httpCliente.get<IProducto2[]>(`${this.URL9}/${idCategoria}`);
  }
  getCategorias(){
    return this.httpCliente.get<ICategoria[]>(this.URL8)
  }
  getProducts(){
    return this.httpCliente.get<IProducto2[]>(this.URL2); 
  }
  getProductsCatalogo(){
    return this.httpCliente.get<IProducto2[]>(this.URL7); 
  }
  postProducto(formData:FormData){
    return this.httpCliente.post<IProducto>(this.URL3,formData)
  }
  putProducto(producto:IProductoEditar){
    return this.httpCliente.put<IProductoEditar>(`${this.URL4}/${producto.productoId}`,producto)
  
  }
  deleteProducto(idProducto:number){
    return this.httpCliente.delete(`${this.URL5}/${idProducto}`);
  }

}
