import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IProducto, IProducto2, IProductoEditar } from './models/IProductos';
import { ICategoria } from './models/ICategoria';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductosApiService {
  httpCliente = inject(HttpClient);
  private readonly URL_PRODUCTOS_USUARIO = `${environment.urlApi}/Productos/mis-productos`;
  private readonly URL_PRODUCTOS = `${environment.urlApi}/Productos`;
  private readonly URL_AÑADIR_PRODUCTOS = `${environment.urlApi}/Productos/añadir`;
  private readonly URL_EDITAR_PRODUCTO = `${environment.urlApi}/Productos/añadir`;
  private readonly URL_ELIMINAR_PRODUCTO = `${environment.urlApi}/Productos/eliminar`;
  private readonly URL_PRODUCTO = `${environment.urlApi}/Producots/producto`;
  private readonly URL_PRODUCTOS_CATALOGO = `${environment.urlApi}/Productos/catalogo`;
  private readonly URL_CATEGORIAS = `${environment.urlApi}/Productos/categorias`;
  private readonly URL_PRODUCTOS_POR_CATEGORIA = `${environment.urlApi}/Productos/categoria`;
  getMyProducts():Observable<IProducto2[]>{
    return this.httpCliente.get<IProducto2[]>(this.URL_PRODUCTOS_USUARIO);
  }
  getProducts():Observable<IProducto2[]>{
    return this.httpCliente.get<IProducto2[]>(this.URL_PRODUCTOS); 
  }
  postProducto(formData:FormData):Observable<IProducto>{
    return this.httpCliente.post<IProducto>(this.URL_AÑADIR_PRODUCTOS,formData)
  }
  getProduct(idProducto:number){
    return this.httpCliente.get<IProducto>(`${this.URL_PRODUCTO}/${idProducto}`);

  }
  getProductosPorCategoria(idCategoria:number){
    return this.httpCliente.get<IProducto2[]>(`${this.URL_PRODUCTOS_POR_CATEGORIA}/${idCategoria}`);
  }
  getCategorias(){
    return this.httpCliente.get<ICategoria[]>(this.URL_CATEGORIAS)
  }
  getProductsCatalogo(){
    return this.httpCliente.get<IProducto2[]>(this.URL_PRODUCTOS_CATALOGO); 
  }
  putProducto(producto:IProductoEditar){
    return this.httpCliente.put<IProductoEditar>(`${this.URL_EDITAR_PRODUCTO}/${producto.productoId}`,producto)
  
  }
  deleteProducto(idProducto:number){
    return this.httpCliente.delete(`${this.URL_ELIMINAR_PRODUCTO}/${idProducto}`);
  }

}
